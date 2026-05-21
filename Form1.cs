using DataManager_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;    // 추가1
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataManager
{
    public partial class Form1 : Form
    {
        //추가
        private string selectedFolderPath = ""; 
        private string[] imageFiles = Array.Empty<string>();
        private int currentIndex = 0;
        private Timer playTimer = new Timer();
        private bool isReverse = false;

        private bool isPlaying = false;
        private double currentSpeed = 1.0;
        private double[] speedLevels = { 0.25, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };
        private int speedIndex = 2;
        //

        public Form1()
        {
            InitializeComponent();

            playTimer.Interval = 100;
            playTimer.Tick += new EventHandler(PlayTimer_Tick);

            DoubleSpeedtxt.Text = "1.0x";

            // 배속 드롭다운 메뉴 생성
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

        private void GoTrainbtn_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

            this.Hide();
        }

        private void SelectFolderbtn_Click(object sender, EventArgs e)
        {
            string windowsUser = Environment.UserName;
            string wslBasePath = $@"\\wsl.localhost\Ubuntu-22.04\home\{windowsUser}\mycar";

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Config 폴더를 선택하세요 (mycar 경로)";
                folderDialog.ShowNewFolderButton = false;

                if (Directory.Exists(wslBasePath))
                    folderDialog.SelectedPath = wslBasePath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderDialog.SelectedPath;
                    Foldertxt.Text = selectedFolderPath;
                }
            }
        }

        private void SelectImgbtn_Click(object sender, EventArgs e)
        {
            string windowsUser = Environment.UserName;
            string wslTubPath = $@"\\wsl.localhost\Ubuntu-22.04\home\{windowsUser}\mycar\data";

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Tub 폴더를 선택하세요 (data 경로)";
                folderDialog.ShowNewFolderButton = false;

                if (Directory.Exists(wslTubPath))
                    folderDialog.SelectedPath = wslTubPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string dataPath = folderDialog.SelectedPath;
                    Imgtxt.Text = dataPath;

                    string imagesPath = Path.Combine(dataPath, "images");
                    if (Directory.Exists(imagesPath))
                    {
                        // string[] 제거 → 클래스 필드에 저장
                        imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                              .OrderBy(f => f)
                                              .ToArray();
                        if (imageFiles.Length > 0)
                        {
                            currentIndex = 0;
                            ShowImage(currentIndex);
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

        private void ShowImage(int index)   // 이미지 재생 버튼을 위함
        {
            if (imageFiles.Length == 0) return;
            Imagepic.Image = Image.FromFile(imageFiles[index]);
            Imagepic.SizeMode = PictureBoxSizeMode.Zoom;

            // trackBar 설정
            Imagebar.Minimum = 0;
            Imagebar.Maximum = imageFiles.Length - 1;
            Imagebar.Value = index;
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (!isReverse)
            {
                if (currentIndex < imageFiles.Length - 1)
                    currentIndex++;
                else
                    playTimer.Stop();
            }
            else
            {
                if (currentIndex > 0)
                    currentIndex--;
                else
                    playTimer.Stop();
            }
            ShowImage(currentIndex);
        }

        private void PreviousImgbtn_Click(object sender, EventArgs e)   // < 버튼
        {
            if (imageFiles.Length == 0) return;
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowImage(currentIndex);
            }
        }

        private void Reversebtn_Click(object sender, EventArgs e)   // << 버튼
        {
            if (imageFiles.Length == 0) return;
            isReverse = true;
            isPlaying = true;
            playTimer.Start();
            PlayAndStopbtn.Text = "정지";
        }

        private void NextImgbtn_Click(object sender, EventArgs e)   // > 버튼
        {
            if (imageFiles.Length == 0) return;
            if (currentIndex < imageFiles.Length - 1)
            {
                currentIndex++;
                ShowImage(currentIndex);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Plsybtn_Click(object sender, EventArgs e)  // >> 버튼
        {
            if (imageFiles.Length == 0) return;
            isReverse = false;
            isPlaying = true;
            playTimer.Start();
            PlayAndStopbtn.Text = "정지";
        }

        private void PlayAndStopbtn_Click(object sender, EventArgs e)   // '재생' 버튼
        {
            if (imageFiles.Length == 0) return;

            if (isPlaying)
            {
                // 재생 중이면 정지
                playTimer.Stop();
                isPlaying = false;
                PlayAndStopbtn.Text = "재생";
            }
            else
            {
                // 정지 중이면 앞으로 연속재생
                isReverse = false;
                playTimer.Start();
                isPlaying = true;
                PlayAndStopbtn.Text = "정지";
            }
        }

        private void Imagebar_Scroll(object sender, EventArgs e)
        {
            currentIndex = Imagebar.Value;
            ShowImage(currentIndex);
        }

        private void DoubleSpeedbtn_Click(object sender, EventArgs e)
        {
            DoubleSpeedbtn.ContextMenuStrip.Show(DoubleSpeedbtn, new Point(0, DoubleSpeedbtn.Height));
        }

        private void ImgDeletebtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            string fileToDelete = imageFiles[currentIndex];

            DialogResult confirm = MessageBox.Show(
                $"현재 이미지를 삭제할까요?\n{fileToDelete}",
                "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // 파일 삭제
                File.Delete(fileToDelete);

                // imageFiles 배열에서 제거
                imageFiles = imageFiles.Where(f => f != fileToDelete).ToArray();

                if (imageFiles.Length == 0)
                {
                    Imagepic.Image = null;
                    Imagebar.Value = 0;
                    return;
                }

                // 삭제 후 인덱스 조정
                if (currentIndex >= imageFiles.Length)
                    currentIndex = imageFiles.Length - 1;

                ShowImage(currentIndex);
            }
        }

        private void ImgAddbtn_Click(object sender, EventArgs e)
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

                    // 현재 images 폴더 경로
                    string imagesPath = Path.GetDirectoryName(imageFiles[0]);

                    // 새 파일명 생성 (현재 인덱스 바로 뒤 순서로)
                    string newFileName = Path.Combine(imagesPath, Path.GetFileName(selectedFile));

                    // 같은 폴더가 아니면 복사
                    if (selectedFile != newFileName)
                        File.Copy(selectedFile, newFileName, overwrite: true);

                    // imageFiles 배열 재정렬 (현재 인덱스 바로 뒤에 삽입)
                    List<string> fileList = imageFiles.ToList();
                    fileList.Insert(currentIndex + 1, newFileName);
                    imageFiles = fileList.ToArray();

                    // 추가한 이미지로 이동
                    currentIndex = currentIndex + 1;
                    ShowImage(currentIndex);
                }
            }
        }
    }
}
