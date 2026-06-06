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
            InitializeLossChart();
            realWorkingDirectory = path;
            dataPath = data;
            radioOverwrite.Checked = true;

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
                        ResetGraph();
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

            string wslCommand = $"mkdir -p $HOME/mycar/models && " +
                                $"cd $HOME/mycar && " +
                                $"~/miniconda3/envs/e2e_env/bin/python3 train.py --tubs $HOME/mycar/data --model $HOME/mycar/models/{modelFileName}";

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "wsl.exe",
                    Arguments = $"-d Ubuntu-22.04 -e bash -c \"{wslCommand}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                donkeyTrainProcess = new Process { StartInfo = startInfo };
                donkeyTrainProcess.EnableRaisingEvents = true;

                donkeyTrainProcess.Exited += (s, args) => this.Invoke(new Action(() => HandleTrainingStop(modelFileName)));

                donkeyTrainProcess.OutputDataReceived += (s, args) => {
                    if (args.Data != null)
                    {
                        if (this.IsHandleCreated && !this.IsDisposed)  // IsDisposed 체크 추가
                        {
                            try
                            {
                                this.Invoke(new Action(() => {
                                    if (this.IsDisposed) return;  // 다시 한 번 체크

                                    Traninglst.Items.Add(args.Data);

                                    // 진행률 파싱
                                    if (args.Data.Contains("Epoch") && args.Data.Contains("/"))
                                    {
                                        try
                                        {
                                            string cleanText = args.Data.Replace("Epoch", "").Trim();
                                            string[] parts = cleanText.Split(' ')[0].Split('/');

                                            if (parts.Length == 2)
                                            {
                                                double currentEpoch = double.Parse(parts[0]);
                                                double totalEpoch = double.Parse(parts[1]);
                                                int percentage = (int)((currentEpoch / totalEpoch) * 100);

                                                TrainingProgresslbl.Text = $"훈련 진행률: {percentage}%";
                                            }
                                        }
                                        catch { }
                                    }

                                    // 오답률 파싱
                                    if (args.Data.Contains("loss:"))
                                    {
                                        try
                                        {
                                            string lossStr = args.Data.Split(new[] { "loss:" }, StringSplitOptions.None)[1].Trim().Split(' ')[0];
                                            if (double.TryParse(lossStr, out double lossVal))
                                            {
                                                UpdateLossGraph(lossVal);
                                            }
                                        }
                                        catch { }
                                    }
                                }));
                            }
                            catch (ObjectDisposedException)
                            {
                                // Form이 닫혀서 무시
                            }
                        }
                    }
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

            TrainingProgresslbl.Text = "훈련 진행률: 0%";
            ResetGraph();

            Task.Run(async () =>
            {
                await Task.Delay(2000);

                this.Invoke(new Action(() =>
                {
                    Traninglst.Items.Add($"[완료] 저장된 모델: {fileName}");
                    TrainingStartbtn.Text = "훈련 시작";
                    RefreshListBox();

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
        private void ShowTableView(){
            foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList())
                tableLayoutPanel1.Controls.Remove(c);

            Traninglst.Visible = true;
            chartPanel.Visible = true;
            Traninglst.Dock = DockStyle.Left;
            Traninglst.Width = tableLayoutPanel1.Width / 2;
        }
        private void Show2TableView(){
            Traninglst.Visible = false;
            chartPanel.Visible = false;

            foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList())
                tableLayoutPanel1.Controls.Remove(c);

            Panel dp = new Panel { Name = "dynamicPanel", Dock = DockStyle.Fill };
            tableLayoutPanel1.Controls.Add(dp, 0, 2);

            ListBox l = new ListBox { Dock = DockStyle.Left, Width = dp.Width / 2 };
            ListBox r = new ListBox { Dock = DockStyle.Fill };

            l.Items.Add("=== 이력(좌) ===");
            r.Items.Add("=== 이력(우) ===");

            int h = (trainingHistory.Count + 1) / 2;
            for (int i = 0; i < trainingHistory.Count; i++)
            {
                string s = $"[{trainingHistory[i].time}] {trainingHistory[i].path}";
                if (i < h) l.Items.Add(s);
                else r.Items.Add(s);
            }
            dp.Controls.Add(r);
            dp.Controls.Add(l);
        }
        private void ShowCardView(){
            Traninglst.Visible = false;
            chartPanel.Visible = false;

            foreach (Control c in tableLayoutPanel1.Controls.OfType<Panel>().Where(p => p.Name == "dynamicPanel").ToList())
                tableLayoutPanel1.Controls.Remove(c);

            Panel dp = new Panel { Name = "dynamicPanel", Dock = DockStyle.Fill, AutoScroll = true };
            tableLayoutPanel1.Controls.Add(dp, 0, 2);

            for (int i = 0; i < trainingHistory.Count; i++)
            {
                var e = trainingHistory[i];
                Panel card = new Panel { Width = 300, Height = 200, Location = new Point((i % 2) * 310 + 10, (i / 2) * 210 + 10), BorderStyle = BorderStyle.FixedSingle };
                PictureBox p = new PictureBox { Width = 150, Height = 100, SizeMode = PictureBoxSizeMode.Zoom };

                if (File.Exists(e.imagePath)) p.Image = Image.FromFile(e.imagePath);

                Label lbl = new Label { Text = e.time, Dock = DockStyle.Bottom };
                card.Controls.Add(p);
                card.Controls.Add(lbl);
                dp.Controls.Add(card);
            }
        }
        public void ApplyWindowState(Form p) { this.StartPosition = FormStartPosition.Manual; this.Location = p.Location; this.Size = p.Size; this.WindowState = p.WindowState; }
        public void ApplyThemePublic() { ApplyTheme(this); }
        private void TrainingStartbtn_Click(object sender, EventArgs e) { }
        private void TrainWronglbl_Click(object sender, EventArgs e) { }
        private void Traninglst_SelectedIndexChanged(object sender, EventArgs e) { }
        private void SortMethodlbl_Click(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lossGraph_Click(object sender, EventArgs e)
        {

        }

        private void InitializeLossChart()
        {
            lossGraph.Series.Clear();
            lossGraph.ChartAreas.Clear();
            lossGraph.ChartAreas.Add("Area1");

            var series = new System.Windows.Forms.DataVisualization.Charting.Series("LossSeries");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series.Color = Color.Red;
            series.BorderWidth = 2;

            lossGraph.Series.Add(series);
            lossGraph.ChartAreas[0].AxisX.Title = "Epoch";
            lossGraph.ChartAreas[0].AxisY.Title = "Loss";
        }

        private void UpdateLossGraph(double lossValue)
        {
            // 차트 데이터 추가
            lossGraph.Series["LossSeries"].Points.AddY(lossValue);

            // 화면에 표시할 데이터 범위 제한 (최근 50개만 표시 등)
            if (lossGraph.Series["LossSeries"].Points.Count > 50)
            {
                lossGraph.Series["LossSeries"].Points.RemoveAt(0);
            }

            // 축 자동 조정
            lossGraph.ChartAreas[0].RecalculateAxesScale();
        }

        private void ResetGraph()
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(() => {
                    if (lossGraph.Series.Contains(lossGraph.Series["LossSeries"]))
                    {
                        lossGraph.Series["LossSeries"].Points.Clear();
                    }
                }));
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}