using DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataManager_2
{
    public partial class Form2 : Form
    {
        private Process donkeyTrainProcess = null;
        private string realWorkingDirectory = "";
        private static List<(string time, string path, string imagePath)> trainingHistory
            = new List<(string, string, string)>();
        private string dataPath = ""; // 추가

        // 🎯 현재 훈련 상태를 추적하는 변수
        private bool isTraining = false;

        // 테마 전환 시 원래 색상 복원용 딕셔너리
        private Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();
        private Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        private bool colorssaved = false;


        public Form2(string path, string data)
        {
            InitializeComponent();

            realWorkingDirectory = path;
            dataPath = data; // 저장

            SortMethodcom.SelectedIndex = 0;
            Massagelbl.Text = "학습 시스템 준비 완료. '훈련 시작' 버튼을 누르세요.";
            Massagelbl.ForeColor = Color.Blue;

            this.TrainingStartbtn.Click -= this.TrainingStartbtn_Click_1;
            this.TrainingStartbtn.Click += new System.EventHandler(this.TrainingStartbtn_Click_1);

            // SortMethodcom 이벤트 연결 - 생성자에 추가
            this.SortMethodcom.SelectedIndexChanged += new System.EventHandler(this.SortMethodcom_SelectedIndexChanged);

            // 기존 훈련 이력 표시
            Traninglst.Items.Clear();
            if (trainingHistory.Count > 0)
            {
                Traninglst.Items.Add("=== 훈련 완료 이력 ===");
                foreach (var entry in trainingHistory)
                {
                    Traninglst.Items.Add($"[{entry.time}] {entry.path}");
                }
                Traninglst.TopIndex = Traninglst.Items.Count - 1;
            }

            if (Form1.isDarkMode) ApplyTheme(this);
        }

        // 표시용
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D))
            {
                Form1.isDarkMode = !Form1.isDarkMode;
                ApplyTheme(this);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApplyTheme(Form form)
        {
            if (!colorssaved)
            {
                SaveOriginalColors(form.Controls);
                colorssaved = true;
            }

            if (Form1.isDarkMode)
            {
                form.BackColor = Color.FromArgb(30, 30, 30);
                ApplyThemeToControls(form.Controls,
                    Color.FromArgb(30, 30, 30), Color.White, Color.FromArgb(60, 60, 60));
            }
            else
            {
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
        // 표시용

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void GoDatabtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (donkeyTrainProcess != null && !donkeyTrainProcess.HasExited)
                {
                    if (donkeyTrainProcess.Handle != IntPtr.Zero)
                    {
                        donkeyTrainProcess.Kill();
                    }
                }
            }
            catch (InvalidOperationException)
            {
                donkeyTrainProcess = null;
            }
            catch (Exception)
            {
            }

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

            this.Close();
        }

        private void SortMethodlbl_Click(object sender, EventArgs e)
        {
        }

        // ★ 버튼 하나로 시작과 종료를 토글하고, 실시간 로그를 파싱해 레이블을 갱신하는 핵심 메서드입니다.
        private void TrainingStartbtn_Click_1(object sender, EventArgs e)
        {
            // 🛑 1. 현재 훈련 중인데 버튼을 누른 경우 -> 훈련 "멈추기" 처리
            if (isTraining)
            {
                try
                {
                    if (donkeyTrainProcess != null && !donkeyTrainProcess.HasExited)
                    {
                        donkeyTrainProcess.Kill(); // 리눅스 프로세스 강제 종료
                    }
                }
                catch (Exception) { }

                isTraining = false;
                TrainingStartbtn.Text = "훈련 시작";
                Massagelbl.Text = "사용자에 의해 학습이 중단되었습니다.";
                Massagelbl.ForeColor = Color.Red;
                Traninglst.Items.Add("[안내] 훈련이 중간에 중단되었습니다.");
                Traninglst.TopIndex = Traninglst.Items.Count - 1;

                // 🆕 [안전장치 추가]: 중간에 강제로 멈춰도 결과 페이지용 기록 리스트에 유효 데이터 등록
                string firstImagePath = "";
                try
                {
                    // images 폴더가 따로 없으므로 dataPath 본진 폴더에서 직접 사진을 찾습니다.
                    if (System.IO.Directory.Exists(dataPath))
                    {
                        var imgs = System.IO.Directory.GetFiles(dataPath, "*.jpg").OrderBy(f => f).ToArray();
                        if (imgs.Length > 0) firstImagePath = imgs[0];
                    }
                }
                catch { } // 이미지 경로 파싱 실패 시 터지지 않도록 예외 무시

                // 리스트뷰 및 결과창 구분을 위해 경로에 (중단됨) 식별 명시 추가
                trainingHistory.Add((DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dataPath + " (중단됨)", firstImagePath));
                RefreshListBox(); // 현재 화면 리스트박스 갱신

                return;
            }

            // ▶️ 2. 훈련 중이 아닐 때 누른 경우 -> 훈련 "시작" 처리
            try
            {
                if (donkeyTrainProcess != null && !donkeyTrainProcess.HasExited)
                {
                    MessageBox.Show("이미 Donkeycar AI 모델 훈련이 백그라운드에서 진행 중입니다.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (InvalidOperationException)
            {
                donkeyTrainProcess = null;
            }

            Traninglst.Items.Clear();
            Traninglst.Items.Add("WSL Linux 서브시스템을 초기화하는 중...");

            // 상태값 및 라벨 초기화
            isTraining = true;
            TrainingStartbtn.Text = "훈련 멈추기";
            Massagelbl.Text = "AI 기계학습 모델 최적화 진행 중...";
            Massagelbl.ForeColor = Color.OrangeRed;
            TrainFigurelbl.Text = "0%";       // 진행률 초기화
            TrainWronglbl.Text = "0.0000";   // 오답률 초기화

            try
            {
                string workingDirectory = realWorkingDirectory;

                if (string.IsNullOrEmpty(workingDirectory))
                {
                    MessageBox.Show("Form1에서 올바른 mycar 폴더 경로가 지정되지 않았습니다.\n이전 화면으로 돌아가서 폴더를 먼저 지정해 주세요.", "경로 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isTraining = false;
                    TrainingStartbtn.Text = "훈련 시작";
                    Massagelbl.Text = "학습 엔진 연동 오류";
                    Massagelbl.ForeColor = Color.Red;
                    return;
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "wsl",
                    Arguments = "-d Ubuntu-22.04 bash -c \"cd ~/mycar && ~/miniconda3/envs/e2e_env/bin/python3 ~/mycar/train.py --tub=~/mycar/data --model=~/mycar/models/mypilot.h5\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                donkeyTrainProcess = new Process { StartInfo = startInfo };
                donkeyTrainProcess.EnableRaisingEvents = true;

                // 💡 리눅스가 실시간으로 보내주는 출력 문자열을 검사하여 데이터 추출
                donkeyTrainProcess.OutputDataReceived += (s, args) =>
                {
                    if (args.Data != null)
                    {
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                Traninglst.Items.Add(args.Data);
                                Traninglst.TopIndex = Traninglst.Items.Count - 1;

                                string logLine = args.Data;

                                // [패턴 A] "Epoch 5/20" 문자열을 분석하여 진행률 계산
                                if (logLine.Contains("Epoch") && logLine.Contains("/"))
                                {
                                    try
                                    {
                                        string cleanText = logLine.Replace("Epoch", "").Trim();
                                        string[] parts = cleanText.Split(' ')[0].Split('/');

                                        if (parts.Length == 2)
                                        {
                                            double currentEpoch = double.Parse(parts[0]);
                                            double totalEpoch = double.Parse(parts[1]);
                                            int percentage = (int)((currentEpoch / totalEpoch) * 100);

                                            TrainFigurelbl.Text = $"훈련 진행률 : {percentage}% (Epoch {currentEpoch}/{totalEpoch})";
                                        }
                                    }
                                    catch { }
                                }

                                // [패턴 B] loss값과 val_loss값을 추적하여 오답률 레이벨에 반영
                                if (logLine.Contains("loss:") || logLine.Contains("val_loss:"))
                                {
                                    try
                                    {
                                        if (logLine.Contains("val_loss:"))
                                        {
                                            int idx = logLine.IndexOf("val_loss:");
                                            string subStr = logLine.Substring(idx + 9).Trim();
                                            string valLossNum = subStr.Split(' ')[0];

                                            TrainWronglbl.Text = valLossNum;
                                        }
                                        else if (logLine.Contains("loss:"))
                                        {
                                            int idx = logLine.IndexOf("loss:");
                                            string subStr = logLine.Substring(idx + 5).Trim();
                                            string lossNum = subStr.Split(' ')[0];

                                            TrainWronglbl.Text = $"훈련 오답률 : {lossNum}";
                                        }
                                    }
                                    catch { }
                                }
                            }));
                        }
                    }
                };

                donkeyTrainProcess.ErrorDataReceived += (s, args) =>
                {
                    if (args.Data != null)
                    {
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                Traninglst.Items.Add($"[파이썬 예외 발생]: {args.Data}");
                                Traninglst.TopIndex = Traninglst.Items.Count - 1;
                            }));
                        }
                    }
                };

                donkeyTrainProcess.Exited += (s, args) =>
                {
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if (!isTraining) return;

                            isTraining = false;
                            TrainingStartbtn.Text = "훈련 시작";
                            Massagelbl.Text = "AI 시스템 훈련 성공!";
                            Massagelbl.ForeColor = Color.Green;
                            TrainFigurelbl.Text = "훈련 진행률 : 100%"; // 완료 시 진행률 100% 명시

                            string firstImagePath = "";
                            string imagesFolder = System.IO.Path.Combine(dataPath, "images");
                            if (System.IO.Directory.Exists(imagesFolder))
                            {
                                var imgs = System.IO.Directory.GetFiles(imagesFolder, "*.jpg").OrderBy(f => f).ToArray();
                                if (imgs.Length > 0) firstImagePath = imgs[0];
                            }
                            else if (System.IO.Directory.Exists(dataPath)) // 백업 경로 탐색
                            {
                                var imgs = System.IO.Directory.GetFiles(dataPath, "*.jpg").OrderBy(f => f).ToArray();
                                if (imgs.Length > 0) firstImagePath = imgs[0];
                            }

                            trainingHistory.Add((DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dataPath, firstImagePath));
                            MessageBox.Show("자율주행 AI 학습이 성공적으로 완료되었습니다!", "훈련 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            RefreshListBox();
                        }));
                    }
                };

                donkeyTrainProcess.Start();
                donkeyTrainProcess.BeginOutputReadLine();
                donkeyTrainProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                isTraining = false;
                TrainingStartbtn.Text = "훈련 시작";
                Massagelbl.Text = "학습 엔진 연동 오류";
                Massagelbl.ForeColor = Color.Red;
                Traninglst.Items.Add($"[치명적 오류] 시스템 실행 거부: {ex.Message}");
                MessageBox.Show($"WSL 환경에서 파이썬 학습 실행에 실패했습니다:\n{ex.Message}", "연동 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Traninglst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SortMethodcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = SortMethodcom.SelectedItem?.ToString();
            if (selected == "Tabel")
            {
                ShowTableView();
            }
            else if (selected == "2-Tabel")
            {
                Show2TableView();
            }
            else if (selected == "Card")
            {
                ShowCardView();
            }
        }

        private void ShowTableView()
        {
            Traninglst.Visible = true;
            Traninglst.Dock = DockStyle.Fill;

            // 기존 동적 컨트롤 제거
            foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList())
                tableLayoutPanel1.Controls.Remove(c);

            RefreshListBox();
        }

        private void Show2TableView()
        {
            Traninglst.Visible = false;

            foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList())
                tableLayoutPanel1.Controls.Remove(c);

            Panel dynamicPanel = new Panel { Name = "dynamicPanel", Dock = DockStyle.Fill };
            tableLayoutPanel1.Controls.Add(dynamicPanel, 0, 2);

            int half = (trainingHistory.Count + 1) / 2;

            ListBox left = new ListBox { Dock = DockStyle.Left, Width = dynamicPanel.Width / 2 };
            ListBox right = new ListBox { Dock = DockStyle.Fill };

            left.Items.Add("=== 훈련 완료 이력 ===");
            right.Items.Add("=== 훈련 완료 이력 ===");

            for (int i = 0; i < trainingHistory.Count; i++)
            {
                string entry = $"[{trainingHistory[i].time}] {trainingHistory[i].path}";
                if (i < half)
                    left.Items.Add(entry);
                else
                    right.Items.Add(entry);
            }

            dynamicPanel.Controls.Add(right);
            dynamicPanel.Controls.Add(left);
        }

        private void ShowCardView()
        {
            Traninglst.Visible = false;

            foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList())
                tableLayoutPanel1.Controls.Remove(c);

            Panel dynamicPanel = new Panel { Name = "dynamicPanel", Dock = DockStyle.Fill, AutoScroll = true };
            tableLayoutPanel1.Controls.Add(dynamicPanel, 0, 2);

            int cardWidth = 320;
            int cardHeight = 220;
            int cols = 2;

            for (int i = 0; i < trainingHistory.Count; i++)
            {
                var entry = trainingHistory[i];
                int col = i % cols;
                int row = i / cols;

                Panel card = new Panel
                {
                    Width = cardWidth,
                    Height = cardHeight,
                    Location = new Point(col * (cardWidth + 10) + 10, row * (cardHeight + 10) + 10),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                };

                // 이미지
                PictureBox pic = new PictureBox
                {
                    Width = 160,
                    Height = 120,
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.LightGray
                };

                if (!string.IsNullOrEmpty(entry.imagePath) && File.Exists(entry.imagePath))
                {
                    using (FileStream fs = new FileStream(entry.imagePath, FileMode.Open, FileAccess.Read))
                    using (Image tempImg = Image.FromStream(fs))
                        pic.Image = new Bitmap(tempImg);
                }

                // 텍스트
                Label lbl = new Label
                {
                    Text = $"{entry.time}\n{entry.path}",
                    Location = new Point(180, 10),
                    Width = 130,
                    Height = 120,
                    Font = new Font("Arial", 8f)
                };

                card.Controls.Add(pic);
                card.Controls.Add(lbl);
                dynamicPanel.Controls.Add(card);
            }
        }

        private void RefreshListBox()
        {
            Traninglst.Items.Clear();
            if (trainingHistory.Count > 0)
            {
                Traninglst.Items.Add("=== 훈련 완료 이력 ===");
                foreach (var entry in trainingHistory)
                    Traninglst.Items.Add($"[{entry.time}] {entry.path}");
                Traninglst.TopIndex = Traninglst.Items.Count - 1;
            }
        }

        private void TrainingStartbtn_Click(object sender, EventArgs e)
        {

        }

        public void ApplyWindowState(Form previousForm)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = previousForm.Location;
            this.Size = previousForm.Size;
            this.WindowState = previousForm.WindowState;
        }
        public void ApplyThemePublic()
        {
            ApplyTheme(this);
        }
        private void TrainWronglbl_Click(object sender, EventArgs e)
        {

        }

        private void GoToResultbtn_Click(object sender, EventArgs e)
        {
            Form4 existingForm4 = Application.OpenForms.OfType<Form4>().FirstOrDefault();
            if (existingForm4 != null)
            {
                existingForm4.Show();
                if (Form1.isDarkMode) existingForm4.ApplyThemePublic();
            }
            else
            {
                // 🆕 핵심 연동 지점: 새 결과 윈도우(Form4) 인스턴스를 생성할 때, 현재 화면의 주행 데이터 본진 주소(dataPath)를 선물상자에 담아 토스합니다!
                Form4 form4 = new Form4(this.dataPath);
                form4.ApplyWindowState(this);
                form4.Show();
                if (Form1.isDarkMode) form4.ApplyThemePublic();
            }
            this.Hide();
        }
    }
}