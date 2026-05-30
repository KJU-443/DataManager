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
                folderDialog.Title = "mycar 폴더 안의 아무 파일이나 하나 선택하세요";
                folderDialog.Filter = "모든 파일 (*.*)|*.*";
                folderDialog.CheckFileExists = false;

                if (Directory.Exists(wslBasePath))
                    folderDialog.InitialDirectory = wslBasePath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
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
                folderDialog.Title = "지정할 data 폴더 안의 파일을 아무거나 클릭하고 열기를 누르세요";
                folderDialog.Filter = "모든 파일 (*.*)|*.*";
                folderDialog.CheckFileExists = false;

                if (Directory.Exists(wslTubPath))
                    folderDialog.InitialDirectory = wslTubPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedDir = Path.GetDirectoryName(folderDialog.FileName);
                    string dirName = Path.GetFileName(selectedDir);

                    string dataPath = dirName.Equals("images", StringComparison.OrdinalIgnoreCase)
                        ? Path.GetDirectoryName(selectedDir)
                        : selectedDir;

                    Imgtxt.Text = dataPath;

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
            Imagelst.Items.Clear();

            foreach (string file in imageFiles)
            {
                Imagelst.Items.Add(Path.GetFileName(file));
            }

            if (imageFiles.Length > 0 && currentIndex >= 0)
            {
                Imagelst.SelectedIndex = currentIndex;
            }
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
            Imagelst.SelectedIndex = index;

            string currentImagePath = imageFiles[index];

            try
            {
                Bitmap bmp = await Task.Run(() =>
                {
                    if (!File.Exists(currentImagePath)) return null;
                    using (FileStream fs = new FileStream(currentImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs))
                        return new Bitmap(tempImg);
                }, token);

                if (token.IsCancellationRequested) return;

                // 새 이미지 로드 완료 후 한 번에 교체
                var oldImage = Imagepic.Image;
                Imagepic.Image = bmp;
                Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
                oldImage?.Dispose(); // 교체 후 기존 이미지 해제
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }

            // angle/throttle 라벨 표시
            if (index < catalogRecords.Count)
            {
                AngleFigurelbl.Text = $"angle : {catalogRecords[index].Angle:F3}";
                TrottleFigurelbl.Text = $"throttle : {catalogRecords[index].Throttle:F3}";
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

            DialogResult confirm = MessageBox.Show(
                "현재 프레임을 삭제할까요?\n(이미지는 image_trash 폴더로 이동되고 카탈로그에서도 삭제됩니다.)",
                "데이터 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // image_trash 폴더 생성
                string dataPath = Imgtxt.Text;
                string trashPath = Path.Combine(dataPath, "image_trash");
                if (!Directory.Exists(trashPath))
                    Directory.CreateDirectory(trashPath);

                // 이미지 image_trash로 이동
                string destPath = Path.Combine(trashPath, fileNameOnly);
                if (Imagepic.Image != null)
                {
                    Imagepic.Image.Dispose();
                    Imagepic.Image = null;
                }
                File.Move(currentFilePath, destPath);

                if (!deletedFiles.Contains(fileNameOnly))
                    deletedFiles.Add(fileNameOnly);

                // catalog에서 해당 레코드 삭제
                string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                                 .OrderBy(f => f)
                                                 .ToArray();

                foreach (string catalogFile in catalogFiles)
                {
                    var lines = File.ReadAllLines(catalogFile)
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
                    File.WriteAllLines(catalogFile, lines);
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

                // deletedFiles는 이제 실제 삭제니까 추가 안 해도 됨
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
            string trashPath = Path.Combine(dataPath, "image_trash");

            if (!Directory.Exists(trashPath) || Directory.GetFiles(trashPath, "*.jpg").Length == 0)
            {
                MessageBox.Show("복구할 데이터가 존재하지 않습니다.", "알림");
                return;
            }

            // image_trash에서 가장 마지막 파일 복구
            string lastTrashFile = Directory.GetFiles(trashPath, "*.jpg")
                                            .OrderBy(f => f)
                                            .Last();
            string fileName = Path.GetFileName(lastTrashFile);

            string imagesPath = Path.Combine(dataPath, "images");
            string restorePath = Path.Combine(imagesPath, fileName);

            File.Move(lastTrashFile, restorePath);

            // imageFiles 재구성
            imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                  .OrderBy(f => f)
                                  .ToArray();

            // catalogRecords 재구성
            originalCatalogLines.Clear();
            catalogRecords.Clear();

            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

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

            RefreshImageList();
            Imagebar.Maximum = imageFiles.Length - 1;
            LoadGraph();

            if (currentIndex >= imageFiles.Length)
                currentIndex = imageFiles.Length - 1;

            await ShowImage(currentIndex);
            MessageBox.Show($"[{fileName}] 복구 완료!", "복구 완료");
        }

        private void GoToImage_Click(object sender, EventArgs e)
        {
            playTimer.Stop();
            Form3 form3 = new Form3(imageFiles, deletedFiles, this, Imgtxt.Text);
            form3.ApplyWindowState(this);
            this.Hide();
            form3.Show();
        }

        public void RestoreImage(string fileName)
        {
            string dataPath = Imgtxt.Text;
            string trashPath = Path.Combine(dataPath, "image_trash");
            string imagesPath = Path.Combine(dataPath, "images");

            string trashFilePath = Path.Combine(trashPath, fileName);
            string restorePath = Path.Combine(imagesPath, fileName);

            if (File.Exists(trashFilePath))
            {
                File.Move(trashFilePath, restorePath);

                imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                      .OrderBy(f => f)
                                      .ToArray();

                Imagebar.Maximum = imageFiles.Length - 1;
                RefreshImageList();
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

            double.TryParse(AngleUptxt.Text, out angleMin);
            double.TryParse(AngleDowntxt.Text, out angleMax);

            double.TryParse(TrottleUptxt.Text, out throttleMin);
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

            RefreshImageList();
            LoadGraph();

            if (imageFiles.Length > 0)
            {
                currentIndex = 0;

                Imagebar.Minimum = 0;
                Imagebar.Maximum = imageFiles.Length - 1;

                await ShowImage(currentIndex);

                DelImageFilteringbtn.Text = "이미지 필터링 해제";
                isFiltering = true;
            }
            else
            {
                MessageBox.Show("조건에 맞는 이미지가 없습니다.");
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
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
        }
    } 
}

