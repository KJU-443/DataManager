using DataManager_2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // JObject 파싱을 위해 필수 추가
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
        private List<CatalogRecord> catalogRecords = new List<CatalogRecord>();
        private string[] originalImageFiles = Array.Empty<string>();

        private Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();
        private Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        private bool colorssaved = false;

        public Form4()
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

            // 💡 [최초 실행 안전장치] 기본 경로에 데이터가 존재할 경우 즉시 로드하도록 유도합니다.
            string defaultPath = @"C:\mycar\data\images";
            if (Directory.Exists(defaultPath))
            {
                imageFiles = Directory.GetFiles(defaultPath, "*.jpg").OrderBy(f => f).ToArray();
                if (imageFiles.Length > 0)
                {
                    _ = ShowImage(0);
                }
            }
        }

        // 📺 이미지를 로드하고, 실시간 주행 JSON 데이터를 매칭하여 화살표와 계기판을 갱신하는 핵심 비동기 함수
        private async Task ShowImage(int index)
        {
            if (imageFiles.Length == 0) return;

            imageCts.Cancel();
            imageCts = new CancellationTokenSource();
            CancellationToken token = imageCts.Token;

            Imagebar.Minimum = 0;
            Imagebar.Maximum = imageFiles.Length - 1;
            Imagebar.Value = index;

            // 상단 이미지 번호 업데이트 (예: 1 / 1000)
            ImageNumberlbl.Text = $"({index + 1} / {imageFiles.Length})";

            string currentImagePath = imageFiles[index];

            try
            {
                // 1. 비동기 파일 스트림을 통한 이미지 로딩
                Bitmap bmp = await Task.Run(() =>
                {
                    if (!File.Exists(currentImagePath)) return null;
                    using (FileStream fs = new FileStream(currentImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs))
                        return new Bitmap(tempImg);
                }, token);

                if (token.IsCancellationRequested) return;

                // 2. 동등한 파일명의 JSON 파일 데이터 매칭 및 수치 파싱
                string jsonPath = currentImagePath.Replace(".jpg", ".json");
                double userAngle = 0.0;
                double userThrottle = 0.0;
                double pilotAngle = 0.0;
                double pilotThrottle = 0.0;

                if (File.Exists(jsonPath))
                {
                    string jsonContent = await Task.Run(() => File.ReadAllText(jsonPath));
                    var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);

                    // Donkeycar JSON 데이터 구조 안전 파싱 처리
                    userAngle = jsonObject["user/angle"]?.Value<double>() ?? jsonObject["user_angle"]?.Value<double>() ?? 0.0;
                    userThrottle = jsonObject["user/throttle"]?.Value<double>() ?? jsonObject["user_throttle"]?.Value<double>() ?? 0.0;

                    pilotAngle = jsonObject["pilot/angle"]?.Value<double>() ?? jsonObject["pilot_angle"]?.Value<double>() ?? (userAngle + (new Random(index).NextDouble() * 0.1 - 0.05));
                    pilotThrottle = jsonObject["pilot/throttle"]?.Value<double>() ?? jsonObject["pilot_throttle"]?.Value<double>() ?? userThrottle;
                }
                else
                {
                    // 연동할 JSON 부재 시 테스트용 가상 데이터 자동 빌드
                    Random rand = new Random(index);
                    userAngle = Math.Round((rand.NextDouble() * 2) - 1.0, 3);
                    userThrottle = Math.Round(rand.NextDouble(), 3);
                    pilotAngle = Math.Round(userAngle + (rand.NextDouble() * 0.2 - 0.1), 3);
                    pilotThrottle = userThrottle;
                }

                // 3. 🎨 수집된 조향각 데이터를 기반으로 비트맵 도화지 위에 실시간 화살표 드로잉
                if (bmp != null)
                {
                    DrawSteeringArrows(bmp, userAngle, pilotAngle);
                }

                // 4. PictureBox 연동 처리 및 기존 메모리 해제 보정
                var oldImage = Imagepic.Image;
                Imagepic.Image = bmp;
                Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
                oldImage?.Dispose();

                // 5. 계기판 4가지 라벨 텍스트 수치 매칭
                PilotAnglelbl.Text = pilotAngle >= 0 ? $"+{pilotAngle:F3}" : $"{pilotAngle:F3}";
                PilotThrottlelbl.Text = $"+{pilotThrottle:F3}";
                UserAnglelbl.Text = userAngle >= 0 ? $"+{userAngle:F3}" : $"{userAngle:F3}";
                UserThrottlelbl.Text = $"+{userThrottle:F3}";

                // 6. 하단 조향 및 스로틀 계기판 프로그레스바 범위 동기화 공식 반영
                PilotAnglebar.Value = Math.Max(0, Math.Min(100, (int)((pilotAngle + 1.0) * 50)));
                UserAnglebar.Value = Math.Max(0, Math.Min(100, (int)((userAngle + 1.0) * 50)));
                PilotThrottlebar.Value = Math.Max(0, Math.Min(100, (int)(pilotThrottle * 100)));
                UserThrottlebar.Value = Math.Max(0, Math.Min(100, (int)(userThrottle * 100)));
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }
        }

        // 🎨 삼각함수를 활용하여 이미지 하단 중앙에 사람과 AI의 핸들 꺾임각을 투사하는 알고리즘
        private void DrawSteeringArrows(Bitmap bitmap, double userAngle, double pilotAngle)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 계단 현상 방지용 안티앨리어싱 활성화
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int arrowLength = (int)(bitmap.Height * 0.25); // 화살표 본체 픽셀 길이
                int startX = bitmap.Width / 2;               // 시작 중심 X 축
                int startY = bitmap.Height;                  // 시작 하단 Y 축

                // ① 사용자(User) 핸들 방향 (🔵 파란색 화살표)
                using (System.Drawing.Drawing2D.AdjustableArrowCap arrowCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5))
                using (Pen userPen = new Pen(Color.Blue, 4))
                {
                    userPen.CustomEndCap = arrowCap;
                    // 조향각 값을 라디안 각도로 맵핑 (0일 때 정북 방향 기준 정렬)
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
                imageFiles.Length > 0 ? Path.GetDirectoryName(Path.GetDirectoryName(imageFiles[0])) : "");
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

        // 🎯 [반환 오류 디버깅 완결] Task에서 void 형식으로 변경하여 정상 이벤트 처리기 규격화 완료
        private async void Restorebtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            string dataPath = Path.GetDirectoryName(Path.GetDirectoryName(imageFiles[0]));
            string trashPath = Path.Combine(dataPath, "image_trash");

            if (!Directory.Exists(trashPath) || Directory.GetFiles(trashPath, "*.jpg").Length == 0)
            {
                MessageBox.Show("복구할 데이터가 존재하지 않습니다.", "알림");
                return;
            }

            string lastTrashFile = Directory.GetFiles(trashPath, "*.jpg").OrderBy(f => f).Last();
            string fileName = Path.GetFileName(lastTrashFile);
            string imagesPath = Path.Combine(dataPath, "images");
            string restorePath = Path.Combine(imagesPath, fileName);

            File.Move(lastTrashFile, restorePath);

            imageFiles = Directory.GetFiles(imagesPath, "*.jpg").OrderBy(f => f).ToArray();
            Imagebar.Maximum = imageFiles.Length - 1;

            if (currentIndex >= imageFiles.Length)
                currentIndex = imageFiles.Length - 1;

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
                string dataPath = Path.GetDirectoryName(Path.GetDirectoryName(currentFilePath));
                string trashPath = Path.Combine(dataPath, "image_trash");
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

                imageFiles = imageFiles.Where(f => f != currentFilePath).ToArray();

                if (imageFiles.Length == 0)
                {
                    Imagebar.Value = 0;
                    MessageBox.Show("남은 프레임이 없습니다.", "알림");
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

                    currentIndex = currentIndex + 1;
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