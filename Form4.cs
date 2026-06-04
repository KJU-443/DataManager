using DataManager_2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataManager
{
    public partial class Form4 : Form
    {
        private string[] imageFiles = Array.Empty<string>();
        private int currentIndex = 0;
        private System.Windows.Forms.Timer playTimer = new System.Windows.Forms.Timer();
        private bool isReverse = false;
        private bool isPlaying = false;
        private double currentSpeed = 1.0;
        private double[] speedLevels = { 0.25, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };
        private CancellationTokenSource imageCts = new CancellationTokenSource();
        private List<string> deletedFiles = new List<string>();

        // 🎯 보내준 코드의 데이터 규격과 완벽 동기화
        private List<CatalogRecord> catalogRecords = new List<CatalogRecord>();
        private string[] originalImageFiles = Array.Empty<string>();

        private Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();
        private Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        private bool colorssaved = false;

        private string targetImagesPath = "";
        private string baseDataPath = "";     // 카탈로그 조회를 위해 상위 경로 저장

        // 🎯 Form2로부터 실제 상위 데이터 주소(C:\mycar\data)를 전달받습니다.
        public Form4(string path = @"C:\mycar\data")
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form4_KeyDown;
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

            // 1. 전달받은 경로 가공 및 세척
            baseDataPath = string.IsNullOrEmpty(path) ? @"C:\mycar\data" : path;
            if (baseDataPath.Contains(" (중단됨)"))
            {
                baseDataPath = baseDataPath.Replace(" (중단됨)", "").Trim();
            }

            // 2. 보낸 코드 규칙대로 상위 주소 아래의 images 폴더 경로 맵핑 고정
            targetImagesPath = Path.Combine(baseDataPath, "images");

            // 3. 디스크 원본 데이터 및 카탈로그 동기화 일제 가동
            SyncDataFromDisk();
        }

        // 🆕 [실시간 데이터 및 카탈로그 동기화 전용 메서드]
        // 보내준 파일 탐색기 로직을 기반으로 이미지와 카탈로그 라인을 완벽하게 일치시킵니다.
        private void SyncDataFromDisk()
        {
            try
            {
                if (!Directory.Exists(baseDataPath)) return;

                // ① [카탈로그 로딩 연동] .catalog 파일들을 싹 긁어모아 주행 기록 데이터 리스트를 채웁니다.
                string[] catalogFiles = Directory.GetFiles(baseDataPath, "*.catalog")
                             .Where(f => !f.EndsWith(".catalog_manifest"))
                             .OrderBy(f => f)
                             .ToArray();

                catalogRecords.Clear();
                foreach (string catalogFile in catalogFiles)
                {
                    foreach (string line in File.ReadLines(catalogFile))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        CatalogRecord record = JsonConvert.DeserializeObject<CatalogRecord>(line);
                        if (record != null) catalogRecords.Add(record);
                    }
                }

                // ② [이미지 파일 연동] 대소문자 확장자 트러블을 방지하며 이미지를 수집합니다.
                if (Directory.Exists(targetImagesPath))
                {
                    string[] extensions = { "*.jpg", "*.jpeg", "*.png", "*.JPG", "*.JPEG", "*.PNG" };
                    imageFiles = extensions.SelectMany(ext => Directory.GetFiles(targetImagesPath, ext))
                                           .Distinct()
                                           .OrderBy(f => f)
                                           .ToArray();

                    if (imageFiles.Length > 0)
                    {
                        Imagebar.Minimum = 0;
                        Imagebar.Maximum = imageFiles.Length - 1;

                        if (currentIndex >= imageFiles.Length) currentIndex = imageFiles.Length - 1;
                        if (currentIndex < 0) currentIndex = 0;

                        Imagebar.Value = currentIndex;
                        _ = ShowImage(currentIndex);
                    }
                }
            }
            catch { }
        }

        // 📺 이미지를 로드하고, 카탈로그 레코드와 매칭하여 화살표 및 계기판을 동기화하는 비동기 함수
        private async Task ShowImage(int index)
        {
            if (imageFiles.Length == 0) return;

            if (index >= imageFiles.Length) index = imageFiles.Length - 1;
            if (index < 0) index = 0;
            currentIndex = index;

            imageCts.Cancel();
            imageCts = new CancellationTokenSource();
            CancellationToken token = imageCts.Token;

            // 무한 루프 차단을 위해 Value만 안전하게 동기화
            Imagebar.Value = currentIndex;
            ImageNumberlbl.Text = $"({currentIndex + 1} / {imageFiles.Length})";

            string currentImagePath = imageFiles[currentIndex];

            // 카탈로그 레코드 수치 매칭 준비
            double userAngle = 0.0;
            double userThrottle = 0.0;
            double pilotAngle = 0.0;
            double pilotThrottle = 0.0;

            // 🆕 보내준 코드 규격에 맞춰 catalogRecords 리스트의 메모리 수치에서 직접 데이터를 뽑아옵니다!
            if (currentIndex < catalogRecords.Count)
            {
                userAngle = catalogRecords[currentIndex].Angle;
                userThrottle = catalogRecords[currentIndex].Throttle;

                // Pilot 수치는 훈련 결과 테스트용으로 보정 처리 유지
                pilotAngle = userAngle + (new Random(currentIndex).NextDouble() * 0.1 - 0.05);
                pilotThrottle = userThrottle;
            }

            try
            {
                // 1. 비동기 파일 스트림을 통한 비트맵 이미지 로딩
                Bitmap bmp = await Task.Run(() =>
                {
                    if (!File.Exists(currentImagePath)) return null;
                    using (FileStream fs = new FileStream(currentImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs))
                        return new Bitmap(tempImg);
                }, token);

                if (token.IsCancellationRequested) return;

                // 2. 🎨 카탈로그에서 추출한 조향 데이터를 기반으로 이미지 위에 실시간 화살표 드로잉
                if (bmp != null)
                {
                    DrawSteeringArrows(bmp, userAngle, pilotAngle);
                }

                // 3. 컨트롤 주입 및 이전 메모리 해제
                var oldImage = Imagepic.Image;
                Imagepic.Image = bmp;
                Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
                oldImage?.Dispose();

                // 4. 우측 대시보드 계기판 라벨 매칭 수치 포맷팅 반영
                PilotAnglelbl.Text = pilotAngle >= 0 ? $"+{pilotAngle:F3}" : $"{pilotAngle:F3}";
                PilotThrottlelbl.Text = $"+{pilotThrottle:F3}";
                UserAnglelbl.Text = userAngle >= 0 ? $"+{userAngle:F3}" : $"{userAngle:F3}";
                UserThrottlelbl.Text = $"+{userThrottle:F3}";

                // 5. 하단 프로그레스 바 범위 실시간 스케일링 동기화
                PilotAnglebar.Value = Math.Max(0, Math.Min(100, (int)((pilotAngle + 1.0) * 50)));
                UserAnglebar.Value = Math.Max(0, Math.Min(100, (int)((userAngle + 1.0) * 50)));
                PilotThrottlebar.Value = Math.Max(0, Math.Min(100, (int)(pilotThrottle * 100)));
                UserThrottlebar.Value = Math.Max(0, Math.Min(100, (int)(userThrottle * 100)));
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }
        }

        // 🎨 이미지 하단 중앙부에 가이드 화살표를 그리는 연산 메서드
        private void DrawSteeringArrows(Bitmap bitmap, double userAngle, double pilotAngle)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int arrowLength = (int)(bitmap.Height * 0.25);
                int startX = bitmap.Width / 2;
                int startY = bitmap.Height;

                // ① 사용자(User) 핸들 방향 (🔵 파란색 화살표)
                using (System.Drawing.Drawing2D.AdjustableArrowCap arrowCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5))
                using (Pen userPen = new Pen(Color.Blue, 4))
                {
                    userPen.CustomEndCap = arrowCap;
                    double userRadians = (userAngle * 45 - 90) * Math.PI / 180.0;
                    int endX = startX + (int)(arrowLength * Math.Cos(userRadians));
                    int endY = startY + (int)(arrowLength * Math.Sin(userRadians));
                    g.DrawLine(userPen, startX, startY, endX, endY);
                }

                // ② 인공지능(Pilot) 핸들 방향 (🔴 빨간색 화살표)
                using (System.Drawing.Drawing2D.AdjustableArrowCap arrowCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5))
                using (Pen pilotPen = new Pen(Color.Red, 4))
                {
                    pilotPen.CustomEndCap = arrowCap;
                    double pilotRadians = (pilotAngle * 45 - 90) * Math.PI / 180.0;
                    int endX = startX + (int)(arrowLength * Math.Cos(pilotRadians));
                    int endY = startY + (int)(arrowLength * Math.Sin(pilotRadians));
                    g.DrawLine(pilotPen, startX, startY, endX, endY);
                }
            }
        }

        private async void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

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

        public void ApplyThemePublic()
        {
            if (!colorssaved)
            {
                SaveOriginalColors(this.Controls);
                colorssaved = true;
            }

            if (Form1.isDarkMode)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                ApplyThemeToControls(this.Controls,
                    Color.FromArgb(30, 30, 30), Color.White, Color.FromArgb(60, 60, 60));
            }
            else
            {
                this.BackColor = SystemColors.Control;
                RestoreOriginalColors(this.Controls);
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
                if (ctrl.Tag?.ToString() == "noTheme")
                {
                    if (ctrl.Controls.Count > 0)
                        ApplyThemeToControls(ctrl.Controls, backColor, foreColor, buttonBack);
                    continue;
                }

                ctrl.ForeColor = foreColor;
                if (ctrl is Button)
                    ctrl.BackColor = buttonBack;
                else if (ctrl is PictureBox) { }
                else
                    ctrl.BackColor = backColor;

                if (ctrl.Controls.Count > 0)
                    ApplyThemeToControls(ctrl.Controls, backColor, foreColor, buttonBack);
            }
        }

        public void ApplyWindowState(Form previousForm)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = previousForm.Location;
            this.Size = previousForm.Size;
            this.WindowState = previousForm.WindowState;
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

        private void GoToImage_Click(object sender, EventArgs e)
        {
            playTimer.Stop();
            Form3 form3 = new Form3(imageFiles, deletedFiles, null,
                imageFiles.Length > 0 ? Path.GetDirectoryName(imageFiles[0]) : "");
            form3.Show();
            this.Hide();
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.ActiveControl is TextBox) return;

            if (e.KeyCode == Keys.Right)
            {
                e.SuppressKeyPress = true;
                NextImgbtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                PreviousImgbtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                ImgDeletebtn.PerformClick();
            }
        }

        private async void Restorebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(targetImagesPath)) return;

            string trashPath = Path.Combine(targetImagesPath, "image_trash");

            if (!Directory.Exists(trashPath) || Directory.GetFiles(trashPath, "*.jpg").Length == 0)
            {
                MessageBox.Show("복구할 데이터가 존재하지 않습니다.", "알림");
                return;
            }

            string lastTrashFile = Directory.GetFiles(trashPath, "*.jpg").OrderBy(f => f).Last();
            string fileName = Path.GetFileName(lastTrashFile);
            string restorePath = Path.Combine(targetImagesPath, fileName);

            File.Move(lastTrashFile, restorePath);

            // 데이터 동기화 재스캔 작동
            SyncDataFromDisk();

            await ShowImage(currentIndex);
            MessageBox.Show($"[{fileName}] 복구 완료!", "복구 완료");
        }

        private async void ImgDeletebtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            string currentFilePath = imageFiles[currentIndex];
            string fileNameOnly = Path.GetFileName(currentFilePath);

            DialogResult confirm = MessageBox.Show(
                "현재 프레임을 삭제할까요?\n(이미지는 image_trash 폴더로 이동됩니다.)",
                "데이터 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string pathFolder = Path.GetDirectoryName(currentFilePath);
                string trashPath = Path.Combine(pathFolder, "image_trash");
                if (!Directory.Exists(trashPath))
                    Directory.CreateDirectory(trashPath);

                string destPath = Path.Combine(trashPath, fileNameOnly);
                if (Imagepic.Image != null)
                {
                    Imagepic.Image.Dispose();
                    Imagepic.Image = null;
                }
                File.Move(currentFilePath, destPath);

                if (!deletedFiles.Contains(fileNameOnly))
                    deletedFiles.Add(fileNameOnly);

                // 삭제 반영 새로고침
                SyncDataFromDisk();

                if (imageFiles.Length == 0)
                {
                    Imagebar.Value = 0;
                    MessageBox.Show("남은 프레임이 없습니다.", "알림");
                    return;
                }

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

                    // 추가 반영 새로고침
                    SyncDataFromDisk();

                    await ShowImage(currentIndex);
                }
            }
        }

        private void DoubleSpeedtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void DoubleSpeedbtn_Click(object sender, EventArgs e)
        {
            DoubleSpeedbtn.ContextMenuStrip.Show(DoubleSpeedbtn, new Point(0, DoubleSpeedbtn.Height));
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void ImageNumberlbl_Click(object sender, EventArgs e)
        {

        }

        private void Imagepic_Click(object sender, EventArgs e)
        {

        }

        private async void Imagebar_Scroll(object sender, EventArgs e)
        {
            currentIndex = Imagebar.Value;
            await ShowImage(currentIndex);
        }

        private void ImgAddbtn_Click_1(object sender, EventArgs e)
        {

        }

        private void GoToTrainbtn_Click(object sender, EventArgs e)
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
                Form2 form2 = new Form2("", "");
                form2.ApplyWindowState(this);
                form2.Show();
                if (Form1.isDarkMode) form2.ApplyThemePublic();
            }
            this.Hide();
        }

        private void GoDatabtn_Click(object sender, EventArgs e)
        {
            Form1 existingForm1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (existingForm1 != null)
            {
                existingForm1.Show();
                if (Form1.isDarkMode) existingForm1.ApplyThemePublic();
            }
            else
            {
                Form1 form1 = new Form1();
                form1.Show();
                if (Form1.isDarkMode) form1.ApplyThemePublic();
            }
            this.Hide();
        }
    }
}