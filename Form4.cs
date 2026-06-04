using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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

                var oldImage = Imagepic.Image;
                Imagepic.Image = bmp;
                Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
                oldImage?.Dispose();
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }

            
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

        private async void Form4_KeyDown(object sender, KeyEventArgs e)
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

        }
    }
}
