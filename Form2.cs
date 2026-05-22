using DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DataManager_2
{
    public partial class Form2 : Form
    {
        private Process donkeyTrainProcess = null;

        // ★ Form1에서 넘겨받은 진짜 물리적 mycar 폴더 경로를 저장할 전역 변수
        private string realWorkingDirectory = "";

        // ★ 생성자에서 string path를 매개변수로 받도록 유지합니다.
        public Form2(string path)
        {
            InitializeComponent();

            realWorkingDirectory = path;

            SortMethodcom.SelectedIndex = 0;
            Massagelbl.Text = "학습 시스템 준비 완료. '훈련 시작' 버튼을 누르세요.";
            Massagelbl.ForeColor = Color.Blue;

            // ★ [유령 에러 차단 마스터키] 혹시라도 기존 디자이너나 옛날 빌드에 꼬여있을지 모르는 모든 이벤트를 먼저 깔끔하게 빼줍니다!
            this.TrainingStartbtn.Click -= this.TrainingStartbtn_Click_1;

            // 그 다음 오직 새 코드만 깨끗하게 단독 연결합니다.
            this.TrainingStartbtn.Click += new System.EventHandler(this.TrainingStartbtn_Click_1);
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

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "wsl",
                    // ★ [최종 완성 구문] 전단계에서 완벽히 검증된 가상환경 주소를 기반으로 진짜 주행 데이터(--tubs)가 적재된 경로와 생성될 인공지능 모델 파일의 위치를 매핑했습니다.
                    Arguments = "-d Ubuntu-22.04 -u tutu bash -c \"/home/tutu/miniconda3/envs/e2e_env/bin/python3 /home/tutu/mycar/manage.py train --tubs=~/mycar/data --model=~/mycar/models/model.h5\"",
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
                        this.BeginInvoke(new Action(() =>
                        {
                            Traninglst.Items.Add(args.Data);
                            Traninglst.TopIndex = Traninglst.Items.Count - 1;
                        }));
                    }
                };

                donkeyTrainProcess.ErrorDataReceived += (s, args) =>
                {
                    if (args.Data != null)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            Traninglst.Items.Add($"[파이썬 예외 발생]: {args.Data}");
                            Traninglst.TopIndex = Traninglst.Items.Count - 1;
                        }));
                    }
                };

                donkeyTrainProcess.Exited += (s, args) =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        Traninglst.Items.Add("==================================================");
                        Traninglst.Items.Add("Donkeycar AI 모델 훈련이 최종 완료되었습니다.");
                        Massagelbl.Text = "AI 시스템 훈련 성공!";
                        Massagelbl.ForeColor = Color.Green;
                        MessageBox.Show("자율주행 AI 학습이 성공적으로 완료되었습니다!", "훈련 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
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
    }
}