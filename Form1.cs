using DataManager_2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static DataManager.Form5;

namespace DataManager
{
    public partial class Form1 : Form
    {
        private string selectedFolderPath = "";
        private string[] imageFiles = Array.Empty<string>();
        private int currentIndex = 0;
        private System.Windows.Forms.Timer playTimer = new System.Windows.Forms.Timer();
        private bool isReverse = false;
        private bool isPlaying = false;
        private double currentSpeed = 1.0;
        private double[] speedLevels = { 0.25, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };

        private CancellationTokenSource imageCts = new CancellationTokenSource();

        // 복구(Restore) 기능을 위한 메모리 백업 저장소
        private List<string> originalCatalogLines = new List<string>();
        private List<string> deletedFiles = new List<string>();

        private List<CatalogRecord> catalogRecords = new List<CatalogRecord>();

        //필터링용
        private string[] originalImageFiles = Array.Empty<string>();
        private List<CatalogRecord> originalCatalogRecords = new List<CatalogRecord>();
        private bool isFiltering = false;

        // 필드 추가 (class 상단)
        private Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();
        private Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        private bool colorssaved = false;
        public static bool isDarkMode = false;

        private int _rangeStart = -1; // -1이면 시작점 미설정 상태

        private bool _suppressDeleteConfirm = false;

        private Bitmap _preloadedBitmap = null;
        private int _preloadedIndex = -1;


        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            playTimer.Interval = 100;
            playTimer.Tick += new EventHandler(PlayTimer_Tick);

            DoubleSpeedtxt.Text = "1.0x";

            ContextMenuStrip speedMenu = new ContextMenuStrip();
            foreach (double speed in speedLevels)
            {
                ToolStripMenuItem item = new ToolStripMenuItem($"{speed}x");
                item.Click += (s, e) =>
                {
                    currentSpeed = speed;
                    DoubleSpeedtxt.Text = $"{currentSpeed}x";
                    playTimer.Interval = (int)(100 / currentSpeed);
                };
                speedMenu.Items.Add(item);
            }
            DoubleSpeedbtn.ContextMenuStrip = speedMenu;
        }
        // 표시용 주석
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D))
            {
                isDarkMode = !isDarkMode;
                ApplyTheme(this);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApplyTheme(Form form)
        {
            // 처음 한 번만 원래 색 저장
            if (!colorssaved)
            {
                SaveOriginalColors(form.Controls);
                colorssaved = true;
            }
            
            Color backColor = isDarkMode ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            Color foreColor = isDarkMode ? Color.White : Color.Black;
            Color buttonBack = isDarkMode ? Color.FromArgb(60, 60, 60) : SystemColors.ButtonFace;

            if (isDarkMode)
            {
                form.BackColor = backColor;
                ApplyThemeToControls(form.Controls, backColor, foreColor, buttonBack);
            }
            else
            {
                // 원래 색으로 복구
                form.BackColor = SystemColors.Control;
                RestoreOriginalColors(form.Controls);
            }
        }

        private void SaveOriginalColors(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                originalBackColors[ctrl] = ctrl.BackColor;
                originalForeColors[ctrl] = ctrl.ForeColor;
                if (ctrl.Controls.Count > 0)
                    SaveOriginalColors(ctrl.Controls);
            }
        }
        private void RestoreOriginalColors(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (originalBackColors.ContainsKey(ctrl))
                    ctrl.BackColor = originalBackColors[ctrl];
                if (originalForeColors.ContainsKey(ctrl))
                    ctrl.ForeColor = originalForeColors[ctrl];
                if (ctrl.Controls.Count > 0)
                    RestoreOriginalColors(ctrl.Controls);
            }
        }

        private void ApplyThemeToControls(Control.ControlCollection controls,
            Color backColor, Color foreColor, Color buttonBack)
        {
            foreach (Control ctrl in controls)
            {
                // Tag가 "noTheme"이면 건드리지 않음
                if (ctrl.Tag?.ToString() == "noTheme")
                {
                    if (ctrl.Controls.Count > 0)
                        ApplyThemeToControls(ctrl.Controls, backColor, foreColor, buttonBack);
                    continue;
                }

                ctrl.ForeColor = foreColor;
                if (ctrl is Button)
                    ctrl.BackColor = buttonBack;
                else if (ctrl is PictureBox)
                { }
                else
                    ctrl.BackColor = backColor;

                if (ctrl.Controls.Count > 0)
                    ApplyThemeToControls(ctrl.Controls, backColor, foreColor, buttonBack);
            }
        }
        public void ApplyThemePublic()
        {
            ApplyTheme(this);
        }
        // 표시용 주석


        private void SelectFolderbtn_Click_1(object sender, EventArgs e)
        {
            string windowsUser = Environment.UserName;
            string wslBasePath = $@"\\wsl.localhost\Ubuntu-22.04\home\{windowsUser}\mycar";

            using (OpenFileDialog folderDialog = new OpenFileDialog())
            {
                folderDialog.Title = "mycar 폴더에 들어가서 열기 버튼을 누르세요";

                // 💡 핵심 3줄: 파일이 아니라 폴더를 선택할 수 있도록 속임수를 씁니다.
                folderDialog.ValidateNames = false;       // 파일 이름 검증 안 함
                folderDialog.CheckFileExists = false;     // 파일 존재 체크 안 함
                folderDialog.CheckPathExists = true;      // 폴더 경로만 체크함

                // 💡 [열기] 버튼 누르는 칸에 폴더 이름을 강제로 채워 넣는 설정입니다.
                folderDialog.FileName = "폴더 선택 완료";

                if (Directory.Exists(wslBasePath))
                    folderDialog.InitialDirectory = wslBasePath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // 💡 사용자가 선택한 '폴더 자체'의 경로를 정확하게 추출해 줍니다.
                    selectedFolderPath = Path.GetDirectoryName(folderDialog.FileName);
                    Foldertxt.Text = selectedFolderPath;
                }
            }
        }

        private void GoTrainbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Imgtxt.Text))
            {
                MessageBox.Show("먼저 Tub 폴더를 지정하여 데이터를 로드해 주세요.", "알림");
                return;
            }

            string dataPath = Imgtxt.Text;

            // 전체 catalog 파일 가져오기
            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

            if (catalogFiles.Length > 0 && originalCatalogLines.Count > 0)
            {
                try
                {
                    // 삭제된 파일 목록 기준으로 필터링
                    var finalLines = originalCatalogLines
                        .Where(line => !deletedFiles.Any(deletedFile => line.Contains(deletedFile)))
                        .ToList();

                    // 각 catalog 파일에 해당하는 라인만 분배해서 저장
                    foreach (string catalogFile in catalogFiles)
                    {
                        string catalogFileName = Path.GetFileNameWithoutExtension(catalogFile);

                        // 해당 catalog 번호 추출 (예: catalog_0 → 0)
                        int catalogIndex = int.Parse(catalogFileName.Split('_')[1]);

                        // 해당 catalog에 속하는 라인만 필터링 (_index 기준)
                        var linesForThisCatalog = finalLines
                            .Where(line =>
                            {
                                if (string.IsNullOrWhiteSpace(line)) return false;
                                try
                                {
                                    JObject json = JObject.Parse(line);
                                    int index = (int)json["_index"];
                                    // catalog당 1000개 기준 (동키카 기본값)
                                    return index / 1000 == catalogIndex;
                                }
                                catch { return false; }
                            })
                            .ToList();

                        File.WriteAllLines(catalogFile, linesForThisCatalog);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"카탈로그 데이터셋 저장 중 오류 발생: {ex.Message}", "오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Form2 form2 = new Form2(selectedFolderPath, Imgtxt.Text);
            form2.ApplyWindowState(this);
            this.Hide();
            form2.Show();
        }
        private async void SelectImgbtn_Click(object sender, EventArgs e)
        {
            string windowsUser = Environment.UserName;
            string wslTubPath = $@"\\wsl.localhost\Ubuntu-22.04\home\{windowsUser}\mycar";

            using (OpenFileDialog folderDialog = new OpenFileDialog())
            {
                folderDialog.Title = "images 폴더 안으로 들어가서 [열기]를 누르세요";
                folderDialog.ValidateNames = false;       // 파일 이름 검증 안 함
                folderDialog.CheckFileExists = false;     // 파일 존재 체크 안 함
                folderDialog.CheckPathExists = true;      // 폴더 경로만 체크함

                // 💡 가짜 파일명을 주어 버튼을 활성화시킵니다.
                folderDialog.FileName = "폴더 선택 완료";

                if (Directory.Exists(wslTubPath))
                    folderDialog.InitialDirectory = wslTubPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // 윈도우가 가짜 파일을 생성한 위치(현재 탐색기가 위치한 폴더 경로)를 가져옵니다.
                    string selectedDir = Path.GetDirectoryName(folderDialog.FileName);

                    if (string.IsNullOrEmpty(selectedDir))
                    {
                        selectedDir = folderDialog.FileName;
                    }

                    string dataPath = selectedDir;

                    // 🔥 [완벽 차단 핵심] 
                    // 윈도우 버그 때문에 images 폴더 안으로 빨려 들어갔거나, 
                    // 사용자가 images 폴더를 더블클릭해서 들어간 상태에서 [열기]를 눌렀더라도!
                    // 전체 경로 중 "\images" 글자가 발견되면 무조건 그 앞까지만 잘라내서 
                    // 우리가 진짜로 필요로 하는 '데이터 폴더' 경로를 강제로 만들어 냅니다.
                    int index = dataPath.IndexOf(@"\images", StringComparison.OrdinalIgnoreCase);

                    if (index >= 0)
                    {
                        // 주소에 \images가 포함되어 있다면 딱 그 앞자리까지만 잘라냅니다.
                        dataPath = dataPath.Substring(0, index);
                    }
                    else
                    {
                        // 리눅스 경로 스타일(/images)도 예외 없이 잘라냅니다.
                        index = dataPath.IndexOf("/images", StringComparison.OrdinalIgnoreCase);
                        if (index >= 0)
                        {
                            dataPath = dataPath.Substring(0, index);
                        }
                    }

                    // 💡 [안전장치 2] 만약 윈도우가 images 폴더의 '상위 폴더(데이터 폴더)'에 멈춰서 
                    // images 폴더를 한 번만 클릭하고 열기를 눌러서 멈췄을 때의 처리
                    if (dataPath.EndsWith("폴더 선택 완료", StringComparison.OrdinalIgnoreCase))
                    {
                        dataPath = Path.GetDirectoryName(dataPath);
                    }

                    // 최종적으로 텍스트박스에 images의 윗단계인 '데이터 폴더' 주소를 강제 주입!
                    Imgtxt.Text = dataPath;

                    // ---------------------------------------------------------------
                    // 여기서부터는 기존의 카탈로그/이미지 로딩 로직 (100% 동일)
                    // ---------------------------------------------------------------
                    string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                 .OrderBy(f => f)
                                 .ToArray();

                    originalCatalogLines.Clear();
                    deletedFiles.Clear();
                    catalogRecords.Clear();

                    foreach (string catalogFile in catalogFiles)
                    {
                        foreach (string line in File.ReadLines(catalogFile))
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;
                            originalCatalogLines.Add(line);
                            CatalogRecord record = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                            if (record != null) catalogRecords.Add(record);
                        }
                    }

                    string imagesPath = Path.Combine(dataPath, "images");
                    if (Directory.Exists(imagesPath))
                    {
                        imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                              .OrderBy(f => f)
                                              .ToArray();
                        originalImageFiles = imageFiles.ToArray();
                        originalCatalogRecords = catalogRecords.ToList();

                        RefreshImageList();

                        if (imageFiles.Length > 0)
                        {
                            currentIndex = 0;
                            Imagebar.Minimum = 0;
                            Imagebar.Maximum = imageFiles.Length - 1;
                            await ShowImage(currentIndex);
                            LoadGraph();
                        }
                        else
                        {
                            MessageBox.Show("images 폴더에 이미지가 없어요.", "알림",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("images 폴더를 찾을 수 없어요.", "오류",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RefreshImageList()
        {
            Imagelst.SelectedIndexChanged -= Imagelst_SelectedIndexChanged;

            Imagelst.Items.Clear();

            // imageFiles 자체를 숫자 순서로 정렬
            imageFiles = imageFiles.OrderBy(f =>
            {
                string numStr = Path.GetFileName(f).Split('_')[0];
                return int.TryParse(numStr, out int n) ? n : int.MaxValue;
            }).ToArray();

            Imagelst.BeginUpdate();  // UI 업데이트 중단
            Imagelst.Items.Clear();
            foreach (string file in imageFiles)
            {
                Imagelst.Items.Add(Path.GetFileName(file));
            }
            Imagelst.EndUpdate();    // UI 업데이트 재개

            if (imageFiles.Length > 0 && currentIndex >= 0)
            {
                if (currentIndex >= imageFiles.Length)
                    currentIndex = imageFiles.Length - 1;
                Imagelst.SelectedIndex = currentIndex;
            }

            int currentNum = 0;
            if (currentIndex < imageFiles.Length)
            {
                string numStr = Path.GetFileName(imageFiles[currentIndex]).Split('_')[0];
                int.TryParse(numStr, out currentNum);
            }

            int totalNum = 0;
            if (originalImageFiles.Length > 0)
            {
                string numStr = Path.GetFileName(originalImageFiles[originalImageFiles.Length - 1]).Split('_')[0];
                int.TryParse(numStr, out totalNum);
            }

            ImageNumberlbl.Text = $"({currentNum}/{originalImageFiles.Length})";

            // 이벤트 다시 연결
            Imagelst.SelectedIndexChanged += Imagelst_SelectedIndexChanged;
        }

        private async Task ShowImage(int index)
        {
            if (imageFiles.Length == 0) return;

            imageCts.Cancel();
            imageCts = new CancellationTokenSource();
            CancellationToken token = imageCts.Token;

            Imagebar.Minimum = 0;
            Imagebar.Maximum = imageFiles.Length - 1;
            Imagebar.Value = index;
            if (!isPlaying)
                Imagelst.SelectedIndex = index;

            string currentImagePath = imageFiles[index];

            try
            {
                Bitmap bmp;
                if (_preloadedIndex == index && _preloadedBitmap != null)
                {
                    bmp = _preloadedBitmap;
                    _preloadedBitmap = null;
                    _preloadedIndex = -1;
                }
                else
                {
                    bmp = await Task.Run(() =>
                    {
                        if (!File.Exists(currentImagePath)) return null;
                        using (FileStream fs = new FileStream(currentImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        using (System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs))
                            return new Bitmap(tempImg);
                    }, token);
                }

                if (token.IsCancellationRequested) return;

                var oldImage = Imagepic.Image;
                Imagepic.Image = bmp;
                Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
                oldImage?.Dispose();
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }

            if (index < catalogRecords.Count)
            {
                AngleFigurelbl.Text = $"angle : {catalogRecords[index].Angle:F3}";
                TrottleFigurelbl.Text = $"throttle : {catalogRecords[index].Throttle:F3}";
            }

            int currentNum = 0;
            if (index < imageFiles.Length)
            {
                string numStr = Path.GetFileName(imageFiles[index]).Split('_')[0];
                int.TryParse(numStr, out currentNum);
            }
            ImageNumberlbl.Text = $"({currentNum}/{originalImageFiles.Length})";

            // 다음 이미지 미리 로드
            int nextIndex = index + 1;
            if (nextIndex < imageFiles.Length && nextIndex != _preloadedIndex)
            {
                _preloadedIndex = nextIndex;
                string nextPath = imageFiles[nextIndex];
                _ = Task.Run(() =>
                {
                    try
                    {
                        using (FileStream fs = new FileStream(nextPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        using (System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs))
                        {
                            var bmp = new Bitmap(tempImg);
                            _preloadedBitmap?.Dispose();
                            _preloadedBitmap = bmp;
                        }
                    }
                    catch { }
                });
            }
        }

        private async void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (!isReverse)
            {
                if (currentIndex < imageFiles.Length - 1)
                    currentIndex++;
                else
                {
                    playTimer.Stop();
                    isPlaying = false;
                    PlayAndStopbtn.Text = "재생";
                }
            }
            else
            {
                if (currentIndex > 0)
                    currentIndex--;
                else
                {
                    playTimer.Stop();
                    isPlaying = false;
                    PlayAndStopbtn.Text = "재생";
                }
            }
            await ShowImage(currentIndex);
        }

        private async void PreviousImgbtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;
            playTimer.Stop();
            isPlaying = false;
            PlayAndStopbtn.Text = "재생";
            if (currentIndex > 0) { currentIndex--; await ShowImage(currentIndex); }
        }

        private async void NextImgbtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;
            playTimer.Stop();
            isPlaying = false;
            PlayAndStopbtn.Text = "재생";
            if (currentIndex < imageFiles.Length - 1) { currentIndex++; await ShowImage(currentIndex); }
        }

        private void Reversebtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;
            isReverse = true;
            isPlaying = true;
            playTimer.Start();
            PlayAndStopbtn.Text = "정지";
        }

        private void Plsybtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;
            isReverse = false;
            isPlaying = true;
            playTimer.Start();
            PlayAndStopbtn.Text = "정지";
        }

        private void PlayAndStopbtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            if (isPlaying)
            {
                playTimer.Stop();
                isPlaying = false;
                PlayAndStopbtn.Text = "재생";
            }
            else
            {
                isReverse = false;
                playTimer.Start();
                isPlaying = true;
                PlayAndStopbtn.Text = "정지";
            }
        }

        private void DoubleSpeedbtn_Click(object sender, EventArgs e)
        {
            DoubleSpeedbtn.ContextMenuStrip.Show(DoubleSpeedbtn, new Point(0, DoubleSpeedbtn.Height));
        }

        private async void Imagebar_Scroll_1(object sender, EventArgs e)
        {
            currentIndex = Imagebar.Value;
            await ShowImage(currentIndex);
        }

        private async void ImgDeletebtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            string currentFilePath = imageFiles[currentIndex];
            string fileNameOnly = Path.GetFileName(currentFilePath);

            if (!_suppressDeleteConfirm)
            {
                using (var dialog = new DoNotShowDialog(
                    "현재 프레임을 삭제할까요?\n(이미지는 image_trash 폴더로 이동되고 카탈로그에서도 삭제됩니다.)"))
                {
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    if (dialog.DoNotShowAgain)
                        _suppressDeleteConfirm = true;
                }
            }

            // image_trash 폴더 생성
            string dataPath = Imgtxt.Text;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string trashPath = Path.Combine(dataPath, "image_trash", today);
            if (!Directory.Exists(trashPath))
                Directory.CreateDirectory(trashPath);

            // 이미지 image_trash로 이동
            string destPath = Path.Combine(trashPath, fileNameOnly);
            if (Imagepic.Image != null)
            {
                var old = Imagepic.Image;
                Imagepic.Image = null;
                old.Dispose();
                await Task.Delay(50);
            }
            File.Move(currentFilePath, destPath);

            if (!deletedFiles.Contains(fileNameOnly))
                deletedFiles.Add(fileNameOnly);

            // catalog에서 해당 레코드 삭제 + catalog_trash에 백업
            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

            string catalogTrashPath = Path.Combine(dataPath, $"catalog_trash_{today}.jsonl");

            foreach (string catalogFile in catalogFiles)
            {
                var allLines = File.ReadAllLines(catalogFile).ToList();
                var keepLines = new List<string>();
                var removeLines = new List<string>();

                foreach (string line in allLines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                        if (json?.ImageArray == fileNameOnly)
                            removeLines.Add(line);
                        else
                            keepLines.Add(line);
                    }
                    catch { keepLines.Add(line); }
                }

                File.WriteAllLines(catalogFile, keepLines);
                File.AppendAllLines(catalogTrashPath, removeLines);
            }

            // originalCatalogLines에서도 삭제
            originalCatalogLines = originalCatalogLines
                .Where(line =>
                {
                    if (string.IsNullOrWhiteSpace(line)) return false;
                    try
                    {
                        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                        return json?.ImageArray != fileNameOnly;
                    }
                    catch { return true; }
                })
                .ToList();

            // catalogRecords에서도 삭제
            if (currentIndex < catalogRecords.Count)
                catalogRecords.RemoveAt(currentIndex);

            imageFiles = imageFiles.Where(f => f != currentFilePath).ToArray();
            RefreshImageList();

            if (imageFiles.Length == 0)
            {
                Imagebar.Value = 0;
                MessageBox.Show("데이터셋 내에 남은 프레임이 없습니다.", "알림");
                return;
            }

            if (currentIndex >= imageFiles.Length)
                currentIndex = imageFiles.Length - 1;

            Imagebar.Maximum = imageFiles.Length - 1;
            await ShowImage(currentIndex);
        }
        private async void ImgAddbtn_Click(object sender, EventArgs e)
        {
            string windowsUser = Environment.UserName;
            string wslBasePath = $@"\\wsl.localhost\Ubuntu-22.04\home\{windowsUser}\mycar";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "추가할 이미지를 선택하세요";
                openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png";
                openFileDialog.InitialDirectory = Directory.Exists(wslBasePath) ? wslBasePath : "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    string imagesPath = Path.GetDirectoryName(imageFiles[0]);
                    string newFileName = Path.Combine(imagesPath, Path.GetFileName(selectedFile));

                    if (selectedFile != newFileName)
                        File.Copy(selectedFile, newFileName, overwrite: true);

                    List<string> fileList = imageFiles.ToList();
                    fileList.Insert(currentIndex + 1, newFileName);
                    imageFiles = fileList.ToArray();
                    RefreshImageList();

                    currentIndex = currentIndex + 1;
                    await ShowImage(currentIndex);
                }
            }
        }

        private void OpenImgBrowserbtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            string tempHtmlPath = Path.Combine(Path.GetTempPath(), "donkey_viewer.html");
            string jsArray = string.Join(",\n", imageFiles.Select(f => $"\"{f.Replace("\\", "\\\\")}\""));

            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Image Viewer</title>
    <style>
        body {{ background: #1a1a1a; display: flex; flex-direction: column; align-items: center; justify-content: center; height: 100vh; margin: 0; color: white; font-family: Arial; }}
        img {{ width: 95vw; height: 90vh; object-fit: contain; }}
        #info {{ margin-top: 10px; font-size: 16px; }}
        #controls {{ margin-top: 10px; font-size: 13px; color: #aaa; }}
    </style>
</head>
<body>
    <img id='imgView' src='' />
    <div id='info'></div>
    <div id='controls'>&larr; &rarr; 방향키로 이미지 넘기기</div>
    <script>
        const images = [{jsArray}];
        let index = {currentIndex};

        function showImage(i) {{
            document.getElementById('imgView').src = 'file:///' + images[i].replace(/\\\\/g, '/');
            document.getElementById('info').innerText = (i + 1) + ' / ' + images.length;
        }}

        document.addEventListener('keydown', function(e) {{
            if (e.key === 'ArrowRight' && index < images.length - 1) {{ index++; showImage(index); }}
            if (e.key === 'ArrowLeft' && index > 0) {{ index--; showImage(index); }}
        }});

        showImage(index);
    </script>
</body>
</html>";

            File.WriteAllText(tempHtmlPath, html);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = tempHtmlPath,
                UseShellExecute = true
            });
        }

        private void LoadGraph()
        {
            if (catalogRecords.Count == 0) return;

            Graph.Series.Clear();
            Graph.ChartAreas[0].AxisX.Title = "Index";
            Graph.ChartAreas[0].AxisY.Title = "Value";
            Graph.ChartAreas[0].AxisY.Minimum = -1.0;
            Graph.ChartAreas[0].AxisY.Maximum = 1.0;

            Series angleSeries = new Series("Angle");
            angleSeries.ChartType = SeriesChartType.Line;
            angleSeries.Color = Color.CornflowerBlue;

            Series throttleSeries = new Series("Throttle");
            throttleSeries.ChartType = SeriesChartType.Line;
            throttleSeries.Color = Color.OrangeRed;

            for (int i = 0; i < catalogRecords.Count; i++)
            {
                angleSeries.Points.AddXY(i, catalogRecords[i].Angle);
                throttleSeries.Points.AddXY(i, catalogRecords[i].Throttle);
            }

            Graph.Series.Add(angleSeries);
            Graph.Series.Add(throttleSeries);
        }

        private void RefreshGraphbtn_Click_1(object sender, EventArgs e)
        {
            LoadGraph();
        }

        private void OpenGraphBrowserbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Imgtxt.Text)) return;

            string dataPath = Imgtxt.Text;
            if (catalogRecords.Count == 0) return;

            var angles = catalogRecords.Select(r => r.Angle).ToList();
            var throttles = catalogRecords.Select(r => r.Throttle).ToList();

            string anglesJs = string.Join(",", angles);
            string throttlesJs = string.Join(",", throttles);

            string tempHtmlPath = Path.Combine(Path.GetTempPath(), "donkey_graph.html");

            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Graph Viewer</title>
    <script src=""https://cdn.jsdelivr.net/npm/chart.js""></script>
    <style>
        body {{ background: #1a1a1a; display: flex; flex-direction: column; align-items: center; justify-content: center; height: 100vh; margin: 0; color: white; font-family: Arial; }}
        canvas {{ width: 95vw !important; height: 85vh !important; }}
    </style>
</head>
<body>
    <canvas id='myChart'></canvas>
    <script>
        const labels = Array.from({{length: {angles.Count}}}, (_, i) => i);
        const ctx = document.getElementById('myChart').getContext('2d');
        new Chart(ctx, {{
            type: 'line',
            data: {{
                labels: labels,
                datasets: [
                    {{
                        label: 'Angle',
                        data: [{anglesJs}],
                        borderColor: 'cornflowerblue',
                        borderWidth: 1.5,
                        pointRadius: 0
                    }},
                    {{
                        label: 'Throttle',
                        data: [{throttlesJs}],
                        borderColor: 'orangered',
                        borderWidth: 1.5,
                        pointRadius: 0
                    }}
                ]
            }},
            options: {{
                animation: false,
                scales: {{
                    y: {{
                        min: -1.0,
                        max: 1.0,
                        ticks: {{ color: 'white' }},
                        grid: {{ color: '#444' }}
                    }},
                    x: {{
                        ticks: {{ color: 'white', maxTicksLimit: 10 }},
                        grid: {{ color: '#444' }}
                    }}
                }},
                plugins: {{
                    legend: {{ labels: {{ color: 'white' }} }}
                }}
            }}
        }});
    </script>
</body>
</html>";

            File.WriteAllText(tempHtmlPath, html);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = tempHtmlPath,
                UseShellExecute = true
            });
        }

        private async void Restorebtn_Click_1(object sender, EventArgs e)
        {
            string dataPath = Imgtxt.Text;
            string trashBasePath = Path.Combine(dataPath, "image_trash");

            if (!Directory.Exists(trashBasePath) || !Directory.GetDirectories(trashBasePath).Any(d => Directory.GetFiles(d, "*.jpg").Length > 0))
            {
                MessageBox.Show("복구할 데이터가 존재하지 않습니다.", "알림");
                return;
            }

            // 날짜별 폴더에서 모든 jpg 수집
            string[] trashFiles = Directory.GetDirectories(trashBasePath)
                                           .SelectMany(d => Directory.GetFiles(d, "*.jpg"))
                                           .OrderBy(f => f)
                                           .ToArray();

            DialogResult confirm = MessageBox.Show(
                $"image_trash의 이미지 {trashFiles.Length}개를 전부 복구할까요?",
                "복구 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            string imagesPath = Path.Combine(dataPath, "images");

            // 이미지 복구
            foreach (string trashFile in trashFiles)
            {
                string fileName = Path.GetFileName(trashFile);
                string restorePath = Path.Combine(imagesPath, fileName);
                if (!File.Exists(restorePath))
                    File.Move(trashFile, restorePath);
            }

            // catalog 파일 목록
            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

            // 날짜별 catalog_trash 파일 전부 탐색
            var allCatalogTrashFiles = Directory.GetFiles(dataPath, "catalog_trash_*.jsonl");
            var restoredFileNames = trashFiles.Select(f => Path.GetFileName(f)).ToHashSet();

            var allTrashedLines = allCatalogTrashFiles
                .SelectMany(f => File.ReadAllLines(f).Where(l => !string.IsNullOrWhiteSpace(l)))
                .ToList();

            var linesToRestore = allTrashedLines
                .Where(line =>
                {
                    try
                    {
                        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                        return restoredFileNames.Contains(json?.ImageArray);
                    }
                    catch { return false; }
                }).ToList();

            // 각 날짜별 파일에서 복구된 줄 제거
            foreach (string catalogTrashFile in allCatalogTrashFiles)
            {
                var remaining = File.ReadAllLines(catalogTrashFile)
                                    .Where(l => !string.IsNullOrWhiteSpace(l))
                                    .Where(line =>
                                    {
                                        try
                                        {
                                            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                                            return !restoredFileNames.Contains(json?.ImageArray);
                                        }
                                        catch { return true; }
                                    }).ToList();
                File.WriteAllLines(catalogTrashFile, remaining);
            }

            // 원래 카탈로그에 복구
            foreach (string line in linesToRestore)
            {
                try
                {
                    JObject json = JObject.Parse(line);
                    int idx = (int)json["_index"];
                    int catalogIndex = idx / 1000;

                    string targetCatalog = catalogFiles
                        .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == $"catalog_{catalogIndex}");

                    if (targetCatalog != null)
                        File.AppendAllText(targetCatalog, line + Environment.NewLine);
                }
                catch { }
            }

            // 복구 후 카탈로그 파일 정렬
            foreach (string catalogFile in catalogFiles)
            {
                var lines = File.ReadAllLines(catalogFile)
                                .Where(l => !string.IsNullOrWhiteSpace(l))
                                .OrderBy(line =>
                                {
                                    try
                                    {
                                        JObject json = JObject.Parse(line);
                                        return (int)json["_index"];
                                    }
                                    catch { return int.MaxValue; }
                                })
                                .ToList();
                File.WriteAllLines(catalogFile, lines);
            }

            // imageFiles 재구성
            imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                  .OrderBy(f => f)
                                  .ToArray();
            originalImageFiles = imageFiles.ToArray();

            // catalogRecords 재구성
            originalCatalogLines.Clear();
            catalogRecords.Clear();

            foreach (string catalogFile in catalogFiles)
            {
                foreach (string line in File.ReadLines(catalogFile))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    originalCatalogLines.Add(line);
                    CatalogRecord record = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                    if (record != null) catalogRecords.Add(record);
                }
            }

            originalCatalogRecords = catalogRecords.ToList();

            RefreshImageList();
            Imagebar.Maximum = imageFiles.Length - 1;
            LoadGraph();

            if (currentIndex >= imageFiles.Length)
                currentIndex = imageFiles.Length - 1;

            await ShowImage(currentIndex);

            // 빈 날짜 폴더 삭제
            foreach (string dateDir in Directory.GetDirectories(trashBasePath))
            {
                if (Directory.GetFiles(dateDir, "*.jpg").Length == 0)
                    Directory.Delete(dateDir);
            }

            // 빈 catalog_trash 파일 삭제
            foreach (string catalogTrashFile in Directory.GetFiles(dataPath, "catalog_trash_*.jsonl"))
            {
                if (!File.ReadAllLines(catalogTrashFile).Any(l => !string.IsNullOrWhiteSpace(l)))
                    File.Delete(catalogTrashFile);
            }

            MessageBox.Show($"{trashFiles.Length}개 복구 완료!", "복구 완료");
        }

        private void GoToImage_Click(object sender, EventArgs e)
        {
            playTimer.Stop();
            Form3 form3 = new Form3(imageFiles, deletedFiles, this, Imgtxt.Text);
            form3.ApplyWindowState(this);
            form3.Show();
            this.Hide();
        }

        public void RestoreImage(string fileName)
        {
            string dataPath = Imgtxt.Text;
            string trashBasePath = Path.Combine(dataPath, "image_trash");
            string imagesPath = Path.Combine(dataPath, "images");

            // 날짜별 폴더에서 파일 찾기
            string trashFilePath = Directory.GetDirectories(trashBasePath)
                                            .Select(d => Path.Combine(d, fileName))
                                            .FirstOrDefault(f => File.Exists(f));

            string restorePath = Path.Combine(imagesPath, fileName);

            if (trashFilePath != null)
            {
                File.Move(trashFilePath, restorePath);

                // 날짜별 catalog_trash 파일 전부 탐색
                var allCatalogTrashFiles = Directory.GetFiles(dataPath, "catalog_trash_*.jsonl");
                string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                                 .OrderBy(f => f)
                                                 .ToArray();

                foreach (string catalogTrashFile in allCatalogTrashFiles)
                {
                    var trashedLines = File.ReadAllLines(catalogTrashFile)
                                           .Where(l => !string.IsNullOrWhiteSpace(l))
                                           .ToList();

                    var linesToRestore = trashedLines
                        .Where(line =>
                        {
                            try
                            {
                                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                                return json?.ImageArray == fileName;
                            }
                            catch { return false; }
                        }).ToList();

                    var linesToKeepInTrash = trashedLines.Except(linesToRestore).ToList();

                    foreach (string line in linesToRestore)
                    {
                        try
                        {
                            JObject json = JObject.Parse(line);
                            int idx = (int)json["_index"];
                            int catalogIndex = idx / 1000;

                            string targetCatalog = catalogFiles
                                .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == $"catalog_{catalogIndex}");

                            if (targetCatalog != null)
                                File.AppendAllText(targetCatalog, line + Environment.NewLine);
                        }
                        catch { }
                    }

                    File.WriteAllLines(catalogTrashFile, linesToKeepInTrash);
                }

                imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                      .OrderBy(f => f)
                                      .ToArray();

                Imagebar.Maximum = imageFiles.Length - 1;

                // 빈 날짜 폴더 삭제
                string trashBaseCleanPath = Path.Combine(dataPath, "image_trash");
                foreach (string dateDir in Directory.GetDirectories(trashBasePath))
                {
                    if (Directory.GetFiles(dateDir, "*.jpg").Length == 0)
                        Directory.Delete(dateDir);
                }

                // 빈 catalog_trash 파일 삭제
                foreach (string catalogTrashFile in Directory.GetFiles(dataPath, "catalog_trash_*.jsonl"))
                {
                    if (!File.ReadAllLines(catalogTrashFile).Any(l => !string.IsNullOrWhiteSpace(l)))
                        File.Delete(catalogTrashFile);
                }

                // 빈 catalog_trash 파일 삭제
                foreach (string catalogTrashFile in Directory.GetFiles(dataPath, "catalog_trash_*.jsonl"))
                {
                    if (!File.ReadAllLines(catalogTrashFile).Any(l => !string.IsNullOrWhiteSpace(l)))
                        File.Delete(catalogTrashFile);
                }

                RefreshImageList();
            }

            // 복구 후 카탈로그 파일 정렬
            string[] sortCatalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                                 .OrderBy(f => f)
                                                 .ToArray();

            foreach (string catalogFile in sortCatalogFiles)
            {
                var lines = File.ReadAllLines(catalogFile)
                                .Where(l => !string.IsNullOrWhiteSpace(l))
                                .OrderBy(line =>
                                {
                                    try
                                    {
                                        JObject json = JObject.Parse(line);
                                        return (int)json["_index"];
                                    }
                                    catch { return int.MaxValue; }
                                })
                                .ToList();
                File.WriteAllLines(catalogFile, lines);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Imagepic_Click(object sender, EventArgs e)
        {

        }

        private async void Imagelst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Imagelst.SelectedIndex < 0) return;

            currentIndex = Imagelst.SelectedIndex;

            await ShowImage(currentIndex);
        }

        private async void ImageFilteringbtn_Click(object sender, EventArgs e)
        {
            // 필터 해제 모드
            if (isFiltering)
            {
                imageFiles = originalImageFiles.ToArray();
                catalogRecords = originalCatalogRecords.ToList();

                RefreshImageList();
                LoadGraph();

                if (imageFiles.Length > 0)
                {
                    currentIndex = 0;

                    Imagebar.Minimum = 0;
                    Imagebar.Maximum = imageFiles.Length - 1;

                    await ShowImage(currentIndex);
                }

                AngleUptxt.Text = "";
                AngleDowntxt.Text = "";

                TrottleUptxt.Text = "";
                TrottleDowntxt.Text = "";

                DelImageFilteringbtn.Text = "이미지 필터링 하기";
                isFiltering = false;

                return;
            }

            // 필터 적용 모드
            double angleMin = -999;
            double angleMax = 999;
            double throttleMin = -999;
            double throttleMax = 999;

            if (!string.IsNullOrWhiteSpace(AngleUptxt.Text))
                double.TryParse(AngleUptxt.Text, out angleMin);

            if (!string.IsNullOrWhiteSpace(AngleDowntxt.Text))
                double.TryParse(AngleDowntxt.Text, out angleMax);

            if (!string.IsNullOrWhiteSpace(TrottleUptxt.Text))
                double.TryParse(TrottleUptxt.Text, out throttleMin);

            if (!string.IsNullOrWhiteSpace(TrottleDowntxt.Text))
                double.TryParse(TrottleDowntxt.Text, out throttleMax);

            List<string> filteredImages = new List<string>();
            List<CatalogRecord> filteredRecords = new List<CatalogRecord>();

            for (int i = 0; i < originalCatalogRecords.Count; i++)
            {
                CatalogRecord record = originalCatalogRecords[i];

                bool angleMatch =
                    record.Angle >= angleMin &&
                    record.Angle <= angleMax;

                bool throttleMatch =
                    record.Throttle >= throttleMin &&
                    record.Throttle <= throttleMax;

                if (angleMatch && throttleMatch)
                {
                    filteredRecords.Add(record);

                    if (i < originalImageFiles.Length)
                        filteredImages.Add(originalImageFiles[i]);
                }
            }

            catalogRecords = filteredRecords;
            imageFiles = filteredImages.ToArray();
            LoadGraph();
            RefreshImageList();

            if (imageFiles.Length > 0)
            {
                currentIndex = 0;

                Imagebar.Minimum = 0;
                Imagebar.Maximum = imageFiles.Length - 1;

                await ShowImage(currentIndex);

                DelImageFilteringbtn.Text = "이미지 필터링 해제";
                isFiltering = true;
            }
        }


        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // 텍스트박스 입력 중이면 무시
            if (this.ActiveControl is TextBox)
                return;

            // 오른쪽 방향키
            if (e.KeyCode == Keys.Right)
            {
                e.SuppressKeyPress = true;
                NextImgbtn.PerformClick();
            }

            // 왼쪽 방향키
            else if (e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                PreviousImgbtn.PerformClick();
            }

            // Delete 키
            else if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                ImgDeletebtn.PerformClick();
            }

            // D 키 (범위 삭제)
            else if (e.KeyCode == Keys.D && !e.Control)
            {
                e.SuppressKeyPress = true;

                if (_rangeStart == -1)
                {
                    // 첫 번째 D: 시작점 설정 + UI 변경
                    string numStr = Path.GetFileName(imageFiles[currentIndex]).Split('_')[0];
                    int.TryParse(numStr, out _rangeStart);

                    ImageNumberlbl.ForeColor = Color.Red;
                    Imgstatuslbl.ForeColor = Color.Red;
                    Imgstatuslbl.Text = "삭제중..";
                }
                else
                {
                    // 두 번째 D: 끝점 설정 후 삭제
                    string numStr = Path.GetFileName(imageFiles[currentIndex]).Split('_')[0];
                    int.TryParse(numStr, out int rangeEnd);

                    if (rangeEnd < _rangeStart)
                    {
                        _rangeStart = -1;
                        ImageNumberlbl.ForeColor = Color.Black;
                        Imgstatuslbl.ForeColor = Color.Black;
                        Imgstatuslbl.Text = "이미지";
                        return;
                    }

                    NumUptxt.Text = _rangeStart.ToString();
                    NumDowntxt.Text = rangeEnd.ToString();
                    _rangeStart = -1;

                    // ↓ 추가
                    playTimer.Stop();
                    isPlaying = false;
                    PlayAndStopbtn.Text = "재생";

                    button1_Click(this, EventArgs.Empty);

                    // 삭제 완료 후 UI 복구
                    ImageNumberlbl.ForeColor = Color.Black;
                    Imgstatuslbl.ForeColor = Color.Black;
                    Imgstatuslbl.Text = "이미지";
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            if (!string.IsNullOrWhiteSpace(NumUptxt.Text) && !string.IsNullOrWhiteSpace(NumDowntxt.Text))
            {
                if (!int.TryParse(NumUptxt.Text, out int numFrom) ||
                    !int.TryParse(NumDowntxt.Text, out int numTo))
                {
                    MessageBox.Show("시작/끝 번호를 올바르게 입력해주세요.", "알림");
                    return;
                }

                if (numFrom > numTo)
                {
                    MessageBox.Show("시작 번호가 끝 번호보다 클 수 없습니다.", "알림");
                    return;
                }

                var targetFiles = imageFiles
                    .Where(f =>
                    {
                        string name = Path.GetFileName(f);
                        string numStr = name.Split('_')[0];
                        return int.TryParse(numStr, out int num) && num >= numFrom && num <= numTo;
                    })
                    .ToList();

                if (targetFiles.Count == 0)
                {
                    MessageBox.Show("해당 범위에 이미지가 없습니다.", "알림");
                    return;
                }

                string dataPath = Imgtxt.Text;

                string today = DateTime.Now.ToString("yyyy-MM-dd");
                string trashPath = Path.Combine(dataPath, "image_trash", today);
                if (!Directory.Exists(trashPath))
                    Directory.CreateDirectory(trashPath);

                var deletedFileNames = targetFiles.Select(f => Path.GetFileName(f)).ToHashSet();

                ImageNumberlbl.ForeColor = Color.Red;
                foreach (string filePath in targetFiles)
                {
                    string fileNameOnly = Path.GetFileName(filePath);
                    string destPath = Path.Combine(trashPath, fileNameOnly);

                    Application.DoEvents();

                    if (Imagepic.Image != null)
                    {
                        var old = Imagepic.Image;
                        Imagepic.Image = null;
                        old.Dispose();
                        await Task.Delay(50);
                    }

                    if (File.Exists(filePath))
                        File.Move(filePath, destPath);

                    if (!deletedFiles.Contains(fileNameOnly))
                        deletedFiles.Add(fileNameOnly);
                }
                ImageNumberlbl.ForeColor = Color.Black;

                string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                                 .OrderBy(f => f)
                                                 .ToArray();

                string catalogTrashPath = Path.Combine(dataPath, $"catalog_trash_{today}.jsonl");

                foreach (string catalogFile in catalogFiles)
                {
                    var allLines = File.ReadAllLines(catalogFile).ToList();
                    var keepLines = new List<string>();
                    var removeLines = new List<string>();

                    foreach (string line in allLines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        try
                        {
                            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                            if (deletedFileNames.Contains(json?.ImageArray))
                                removeLines.Add(line);
                            else
                                keepLines.Add(line);
                        }
                        catch { keepLines.Add(line); }
                    }

                    File.WriteAllLines(catalogFile, keepLines);
                    File.AppendAllLines(catalogTrashPath, removeLines);
                }

                originalCatalogLines = originalCatalogLines
                    .Where(line =>
                    {
                        if (string.IsNullOrWhiteSpace(line)) return false;
                        try
                        {
                            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                            return !deletedFileNames.Contains(json?.ImageArray);
                        }
                        catch { return true; }
                    })
                    .ToList();

                originalImageFiles = originalImageFiles
                    .Where(f => !deletedFileNames.Contains(Path.GetFileName(f)))
                    .ToArray();
                originalCatalogRecords = originalCatalogRecords
                    .Where(r => !deletedFileNames.Contains(r.ImageArray))
                    .ToList();

                imageFiles = imageFiles
                    .Where(f => !deletedFileNames.Contains(Path.GetFileName(f)))
                    .ToArray();
                catalogRecords = catalogRecords
                    .Where(r => !deletedFileNames.Contains(r.ImageArray))
                    .ToList();

                NumUptxt.Text = "";
                NumDowntxt.Text = "";

                RefreshImageList();
                LoadGraph();

                if (imageFiles.Length > 0)
                {
                    if (currentIndex >= imageFiles.Length)
                        currentIndex = imageFiles.Length - 1;
                    Imagebar.Minimum = 0;
                    Imagebar.Maximum = imageFiles.Length - 1;
                    await ShowImage(currentIndex);
                }
                else
                {
                    Imagepic.Image = null;
                    Imagebar.Value = 0;
                    MessageBox.Show("모든 이미지가 삭제되었습니다.", "알림");
                }

                return;
            }

            if (!isFiltering)
            {
                MessageBox.Show("먼저 필터링을 적용해주세요.", "알림");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"필터링된 {imageFiles.Length}개의 이미지를 삭제할까요?\n(image_trash 폴더로 이동되고 카탈로그에서도 삭제됩니다.)",
                "일괄 삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string dataPath = Imgtxt.Text;
                string trashPath = Path.Combine(dataPath, "image_trash");
                if (!Directory.Exists(trashPath))
                    Directory.CreateDirectory(trashPath);

                ImageNumberlbl.ForeColor = Color.Red;
                foreach (string filePath in imageFiles)
                {
                    string fileNameOnly = Path.GetFileName(filePath);
                    string destPath = Path.Combine(trashPath, fileNameOnly);

                    Application.DoEvents();

                    if (Imagepic.Image != null)
                    {
                        Imagepic.Image.Dispose();
                        Imagepic.Image = null;
                    }

                    if (File.Exists(filePath))
                        File.Move(filePath, destPath);

                    if (!deletedFiles.Contains(fileNameOnly))
                        deletedFiles.Add(fileNameOnly);
                }
                ImageNumberlbl.ForeColor = Color.Black;

                string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                                 .OrderBy(f => f)
                                                 .ToArray();

                var deletedFileNames = imageFiles.Select(f => Path.GetFileName(f)).ToHashSet();
                string catalogTrashPath = Path.Combine(dataPath, "catalog_trash.jsonl");

                foreach (string catalogFile in catalogFiles)
                {
                    var allLines = File.ReadAllLines(catalogFile).ToList();
                    var keepLines = new List<string>();
                    var removeLines = new List<string>();

                    foreach (string line in allLines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        try
                        {
                            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                            if (deletedFileNames.Contains(json?.ImageArray))
                                removeLines.Add(line);
                            else
                                keepLines.Add(line);
                        }
                        catch { keepLines.Add(line); }
                    }

                    File.WriteAllLines(catalogFile, keepLines);
                    File.AppendAllLines(catalogTrashPath, removeLines);
                }

                originalCatalogLines = originalCatalogLines
                    .Where(line =>
                    {
                        if (string.IsNullOrWhiteSpace(line)) return false;
                        try
                        {
                            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                            return !deletedFileNames.Contains(json?.ImageArray);
                        }
                        catch { return true; }
                    })
                    .ToList();

                originalImageFiles = originalImageFiles
                    .Where(f => !deletedFileNames.Contains(Path.GetFileName(f)))
                    .ToArray();
                originalCatalogRecords = originalCatalogRecords
                    .Where(r => !deletedFileNames.Contains(r.ImageArray))
                    .ToList();

                imageFiles = originalImageFiles.ToArray();
                catalogRecords = originalCatalogRecords.ToList();

                isFiltering = false;
                DelImageFilteringbtn.Text = "이미지 필터링 하기";

                AngleUptxt.Text = "";
                AngleDowntxt.Text = "";
                TrottleUptxt.Text = "";
                TrottleDowntxt.Text = "";

                RefreshImageList();
                LoadGraph();

                if (imageFiles.Length > 0)
                {
                    if (currentIndex >= imageFiles.Length)
                        currentIndex = imageFiles.Length - 1;
                    Imagebar.Minimum = 0;
                    Imagebar.Maximum = imageFiles.Length - 1;
                    await ShowImage(currentIndex);
                }
                else
                {
                    Imagepic.Image = null;
                    Imagebar.Value = 0;
                    MessageBox.Show("모든 이미지가 삭제되었습니다.", "알림");
                }
            }
        }

        private void ImageNumberlbl_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GoDatabtn_Click(object sender, EventArgs e)
        {
            playTimer.Stop();
            Form2 existingForm2 = Application.OpenForms.OfType<Form2>().FirstOrDefault();
            if (existingForm2 != null)
            {
                existingForm2.ApplyWindowState(this);
                existingForm2.Show();
                if (Form1.isDarkMode) existingForm2.ApplyThemePublic();
            }
            else
            {
                Form2 form2 = new Form2(selectedFolderPath, Imgtxt.Text);
                form2.ApplyWindowState(this);
                form2.Show();
                if (Form1.isDarkMode) form2.ApplyThemePublic();
            }
            this.Hide();
        }

        private void GoTrainResultbtn_Click(object sender, EventArgs e)
        {
            playTimer.Stop();
            Form4 existingForm4 = Application.OpenForms.OfType<Form4>().FirstOrDefault();
            if (existingForm4 != null)
            {
                existingForm4.ApplyWindowState(this);
                existingForm4.Show();
                if (Form1.isDarkMode) existingForm4.ApplyThemePublic();
            }
            else
            {
                Form4 form4 = new Form4();
                form4.ApplyWindowState(this);
                form4.Show();
                if (Form1.isDarkMode) form4.ApplyThemePublic();
            }
            this.Hide();
        }


    } 
}

