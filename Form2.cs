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
        private static List<(string time, string path, string imagePath)> trainingHistory = new List<(string, string, string)>();
        private string dataPath = "";
        private bool isTraining = false;

        private Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();
        private Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        private bool colorssaved = false;

        public Form2(string path, string data)
        {
            InitializeComponent();
            realWorkingDirectory = path;
            dataPath = data;

            SortMethodcom.SelectedIndex = 0;
            Massagelbl.Text = "학습 시스템 준비 완료.";
            Massagelbl.ForeColor = Color.Blue;

            this.TrainingStartbtn.Click -= this.TrainingStartbtn_Click_1;
            this.TrainingStartbtn.Click += new System.EventHandler(this.TrainingStartbtn_Click_1);
            this.SortMethodcom.SelectedIndexChanged += new System.EventHandler(this.SortMethodcom_SelectedIndexChanged);

            RefreshListBox();
            if (Form1.isDarkMode) ApplyTheme(this);
        }

        // ==========================================================
        // 훈련 제어 로직 (안정화 완료)
        // ==========================================================
        private void TrainingStartbtn_Click_1(object sender, EventArgs e)
        {
            if (isTraining)
            {
                // [안전 종료] 강제 종료(Kill) 대신 pkill -2 (SIGINT)를 보내 파이썬에 저장 시간 부여
                try
                {
                    if (donkeyTrainProcess != null && !donkeyTrainProcess.HasExited)
                    {
                        Process.Start("wsl", "-d Ubuntu-22.04 bash -c \"pkill -2 -f train.py\"");
                        Traninglst.Items.Add("[안내] 훈련 중단 신호 전달 완료. 모델 저장 대기 중...");
                    }
                }
                catch (Exception ex) { Traninglst.Items.Add($"[오류] 중단 신호 실패: {ex.Message}"); }
            }
            else
            {
                StartTraining();
            }
        }

        private void StartTraining()
        {
            isTraining = true;
            TrainingStartbtn.Text = "훈련 멈추기";
            Traninglst.Items.Clear();

            string modelFileName = radioOverwrite.Checked ? "mypilot.h5" : $"mypilot_{DateTime.Now:yyyyMMdd_HHmm}.h5";

            // 💡 핵심: 리눅스 환경변수 $HOME을 직접 사용하여 경로 문제를 원천 봉쇄
            // mkdir -p를 사용할 때도 $HOME을 활용함
            string wslCommand = $"mkdir -p $HOME/mycar/models && " +
                                $"cd $HOME/mycar && " +
                                $"~/miniconda3/envs/e2e_env/bin/python3 train.py --tubs $HOME/mycar/data --model $HOME/mycar/models/{modelFileName}";

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "wsl.exe",
                    // 💡 -e bash -c 를 통해 명령어를 직접 쉘에 전달
                    Arguments = $"-d Ubuntu-22.04 -e bash -c \"{wslCommand}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                donkeyTrainProcess = new Process { StartInfo = startInfo };
                donkeyTrainProcess.EnableRaisingEvents = true;

                donkeyTrainProcess.Exited += (s, args) => this.Invoke(new Action(() => HandleTrainingStop(modelFileName)));

                // 상세 에러 로그 추적
                donkeyTrainProcess.OutputDataReceived += (s, args) => {
                    if (args.Data != null) this.Invoke(new Action(() => Traninglst.Items.Add(args.Data)));
                };
                donkeyTrainProcess.ErrorDataReceived += (s, args) => {
                    if (args.Data != null) this.Invoke(new Action(() => Traninglst.Items.Add($"[에러로그]: {args.Data}")));
                };

                donkeyTrainProcess.Start();
                donkeyTrainProcess.BeginOutputReadLine();
                donkeyTrainProcess.BeginErrorReadLine();
            }
            catch (Exception ex) { MessageBox.Show("실행 오류: " + ex.Message); isTraining = false; }
        }

        private void HandleTrainingStop(string fileName)
        {
            if (!isTraining) return;
            isTraining = false;

            // 파이썬 종료 후 파일 시스템 반영 대기
            Task.Run(async () =>
            {
                await Task.Delay(2000);

                this.Invoke(new Action(() =>
                {
                    Traninglst.Items.Add($"[완료] 저장된 모델: {fileName}");
                    TrainingStartbtn.Text = "훈련 시작";
                    RefreshListBox();

                    // 결과창으로 이동
                    GoToResultbtn_Click(null, null);
                }));
            });
        }

        // ==========================================================
        // UI 로직 및 기존 기능 전체
        // ==========================================================
        private void RefreshListBox() { Traninglst.Items.Clear(); Traninglst.Items.Add("=== 훈련 완료 이력 ==="); foreach (var e in trainingHistory) Traninglst.Items.Add($"[{e.time}] {e.path}"); }
        private void ApplyTheme(Form f) { if (!colorssaved) { SaveOriginalColors(f.Controls); colorssaved = true; } if (Form1.isDarkMode) { f.BackColor = Color.FromArgb(30, 30, 30); ApplyThemeToControls(f.Controls, Color.FromArgb(30, 30, 30), Color.White, Color.FromArgb(60, 60, 60)); } else { f.BackColor = SystemColors.Control; RestoreOriginalColors(f.Controls); } }
        private void SaveOriginalColors(Control.ControlCollection c) { foreach (Control ctrl in c) { originalBackColors[ctrl] = ctrl.BackColor; originalForeColors[ctrl] = ctrl.ForeColor; if (ctrl.Controls.Count > 0) SaveOriginalColors(ctrl.Controls); } }
        private void RestoreOriginalColors(Control.ControlCollection c) { foreach (Control ctrl in c) { if (originalBackColors.ContainsKey(ctrl)) ctrl.BackColor = originalBackColors[ctrl]; if (originalForeColors.ContainsKey(ctrl)) ctrl.ForeColor = originalForeColors[ctrl]; if (ctrl.Controls.Count > 0) RestoreOriginalColors(ctrl.Controls); } }
        private void ApplyThemeToControls(Control.ControlCollection c, Color b, Color f, Color btn) { foreach (Control ctrl in c) { if (ctrl.Tag?.ToString() == "noTheme") continue; ctrl.ForeColor = f; if (ctrl is Button) ctrl.BackColor = btn; else if (!(ctrl is PictureBox)) ctrl.BackColor = b; if (ctrl.Controls.Count > 0) ApplyThemeToControls(ctrl.Controls, b, f, btn); } }
        protected override bool ProcessCmdKey(ref Message m, Keys k) { if (k == (Keys.Control | Keys.D)) { Form1.isDarkMode = !Form1.isDarkMode; ApplyTheme(this); return true; } return base.ProcessCmdKey(ref m, k); }
        private void GoDatabtn_Click(object sender, EventArgs e) { Form1 f = Application.OpenForms.OfType<Form1>().FirstOrDefault() ?? new Form1(); f.Show(); if (Form1.isDarkMode) f.ApplyThemePublic(); this.Close(); }
        private void GoToResultbtn_Click(object sender, EventArgs e) { Form4 f4 = new Form4(dataPath); f4.ApplyWindowState(this); f4.Show(); this.Hide(); }
        private void SortMethodcom_SelectedIndexChanged(object sender, EventArgs e) { string s = SortMethodcom.SelectedItem?.ToString(); if (s == "Tabel") ShowTableView(); else if (s == "2-Tabel") Show2TableView(); else if (s == "Card") ShowCardView(); }
        private void ShowTableView() { Traninglst.Visible = true; Traninglst.Dock = DockStyle.Fill; foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList()) tableLayoutPanel1.Controls.Remove(c); RefreshListBox(); }
        private void Show2TableView() { Traninglst.Visible = false; foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList()) tableLayoutPanel1.Controls.Remove(c); Panel dp = new Panel { Name = "dynamicPanel", Dock = DockStyle.Fill }; tableLayoutPanel1.Controls.Add(dp, 0, 2); ListBox l = new ListBox { Dock = DockStyle.Left, Width = dp.Width / 2 }; ListBox r = new ListBox { Dock = DockStyle.Fill }; l.Items.Add("=== 이력(좌) ==="); r.Items.Add("=== 이력(우) ==="); int h = (trainingHistory.Count + 1) / 2; for (int i = 0; i < trainingHistory.Count; i++) { string s = $"[{trainingHistory[i].time}] {trainingHistory[i].path}"; if (i < h) l.Items.Add(s); else r.Items.Add(s); } dp.Controls.Add(r); dp.Controls.Add(l); }
        private void ShowCardView() { Traninglst.Visible = false; foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList()) tableLayoutPanel1.Controls.Remove(c); Panel dp = new Panel { Name = "dynamicPanel", Dock = DockStyle.Fill, AutoScroll = true }; tableLayoutPanel1.Controls.Add(dp, 0, 2); for (int i = 0; i < trainingHistory.Count; i++) { var e = trainingHistory[i]; Panel card = new Panel { Width = 300, Height = 200, Location = new Point((i % 2) * 310 + 10, (i / 2) * 210 + 10), BorderStyle = BorderStyle.FixedSingle }; PictureBox p = new PictureBox { Width = 150, Height = 100, SizeMode = PictureBoxSizeMode.Zoom }; if (File.Exists(e.imagePath)) p.Image = Image.FromFile(e.imagePath); Label l = new Label { Text = e.time, Dock = DockStyle.Bottom }; card.Controls.Add(p); card.Controls.Add(l); dp.Controls.Add(card); } }
        public void ApplyWindowState(Form p) { this.StartPosition = FormStartPosition.Manual; this.Location = p.Location; this.Size = p.Size; this.WindowState = p.WindowState; }
        public void ApplyThemePublic() { ApplyTheme(this); }
        private void TrainingStartbtn_Click(object sender, EventArgs e) { }
        private void TrainWronglbl_Click(object sender, EventArgs e) { }
        private void Traninglst_SelectedIndexChanged(object sender, EventArgs e) { }
        private void SortMethodlbl_Click(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
    }
}