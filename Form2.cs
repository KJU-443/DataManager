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
        }

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

            Form1 form1 = new Form1();
            form1.Show();

            this.Close();
        }

        private void SortMethodlbl_Click(object sender, EventArgs e)
        {
        }

        // ★ 중복을 제거하고 비동기 스트림을 안전하게 파이프라이닝하는 핵심 메서드입니다.
        private void TrainingStartbtn_Click_1(object sender, EventArgs e)
        {
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
            Massagelbl.Text = "AI 기계학습 모델 최적화 진행 중...";
            Massagelbl.ForeColor = Color.OrangeRed;

            try
            {
                string workingDirectory = realWorkingDirectory;

                // 혹시나 Form1에서 폴더 선택을 안 하고 넘어왔을 때를 대비한 안전장치
                if (string.IsNullOrEmpty(workingDirectory))
                {
                    MessageBox.Show("Form1에서 올바른 mycar 폴더 경로가 지정되지 않았습니다.\n이전 화면으로 돌아가서 폴더를 먼저 지정해 주세요.", "경로 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Massagelbl.Text = "학습 엔진 연동 오류";
                    Massagelbl.ForeColor = Color.Red;
                    return;
                }

                // 💡 [핵심 보완] 특정 유저 이름 대신 물결표(~)와 기본 계정 설정을 활용해 경로를 동적으로 구성합니다.
                // -u tutu 옵션을 제거하여 WSL이 기본 사용자로 로그인하게 만듭니다.
                // /home/tutu/... 주소 대신 가상환경과 스크립트 위치를 현재 로그인된 유저의 홈 디렉터리(~/) 기준으로 통일합니다.
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

                donkeyTrainProcess.OutputDataReceived += (s, args) =>
                {
                    if (args.Data != null)
                    {
                        // 🔥 [핸들 에러 방지] UI 도화지(핸들)가 정상적으로 살아있을 때만 안전하게 화면을 갱신합니다.
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                Traninglst.Items.Add(args.Data);
                                Traninglst.TopIndex = Traninglst.Items.Count - 1;
                            }));
                        }
                    }
                };

                donkeyTrainProcess.ErrorDataReceived += (s, args) =>
                {
                    if (args.Data != null)
                    {
                        // 🔥 [핸들 에러 방지] 에러 로그 출력 시에도 동일한 안전장치를 적용합니다.
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
                    // 🔥 [핸들 에러 방지] 훈련이 끝난 시점에 프로그램 창이 켜져 있는지 체크합니다.
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            Massagelbl.Text = "AI 시스템 훈련 성공!";
                            Massagelbl.ForeColor = Color.Green;

                            string firstImagePath = "";
                            string imagesFolder = System.IO.Path.Combine(dataPath, "images");
                            if (System.IO.Directory.Exists(imagesFolder))
                            {
                                var imgs = System.IO.Directory.GetFiles(imagesFolder, "*.jpg").OrderBy(f => f).ToArray();
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
    }
}