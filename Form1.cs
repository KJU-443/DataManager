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
        private Timer playTimer = new Timer();
        private bool isReverse = false;
        private bool isPlaying = false;
        private double currentSpeed = 1.0;
        private double[] speedLevels = { 0.25, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };
        private int speedIndex = 2;

        // 복구(Restore) 기능을 위한 메모리 백업 저장소
        private List<string> originalCatalogLines = new List<string>();
        private List<string> deletedFiles = new List<string>();

        public Form1()
        {
            InitializeComponent();
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
            string catalogPath = Path.Combine(dataPath, "catalog_0.catalog");

            if (File.Exists(catalogPath) && originalCatalogLines.Count > 0)
            {
                try
                {
                    var finalLines = originalCatalogLines
                        .Where(line => !deletedFiles.Any(deletedFile => line.Contains(deletedFile)))
                        .ToList();

                    File.WriteAllLines(catalogPath, finalLines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"카탈로그 데이터셋 저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Form2 form2 = new Form2(selectedFolderPath, Imgtxt.Text); form2.Show();
            this.Hide();
        }
        private void SelectImgbtn_Click(object sender, EventArgs e)
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

                    string catalogPath = Path.Combine(dataPath, "catalog_0.catalog");
                    if (File.Exists(catalogPath))
                    {
                        originalCatalogLines = File.ReadAllLines(catalogPath).ToList();
                        deletedFiles.Clear();
                    }

                    string imagesPath = Path.Combine(dataPath, "images");
                    if (Directory.Exists(imagesPath))
                    {
                        imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                              .OrderBy(f => f)
                                              .ToArray();
                        if (imageFiles.Length > 0)
                        {
                            currentIndex = 0;
                            Imagebar.Minimum = 0;
                            Imagebar.Maximum = imageFiles.Length - 1;
                            ShowImage(currentIndex);
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

        private void ShowImage(int index)
        {
            if (imageFiles.Length == 0) return;

            if (Imagepic.Image != null)
            {
                Imagepic.Image.Dispose();
                Imagepic.Image = null;
            }

            string currentImagePath = imageFiles[index];
            if (File.Exists(currentImagePath))
            {
                using (FileStream fs = new FileStream(currentImagePath, FileMode.Open, FileAccess.Read))
                {
                    using (Image tempImg = Image.FromStream(fs))
                    {
                        Imagepic.Image = new Bitmap(tempImg);
                    }
                }
            }

            Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
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
            ShowImage(currentIndex);
        }

        private void PreviousImgbtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;
            playTimer.Stop();
            isPlaying = false;
            PlayAndStopbtn.Text = "재생";
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowImage(currentIndex);
            }
        }

        private void NextImgbtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;
            playTimer.Stop();
            isPlaying = false;
            PlayAndStopbtn.Text = "재생";
            if (currentIndex < imageFiles.Length - 1)
            {
                currentIndex++;
                ShowImage(currentIndex);
            }
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

        private void Imagebar_Scroll(object sender, EventArgs e)
        {
            currentIndex = Imagebar.Value;
            ShowImage(currentIndex);
        }

        private void DoubleSpeedbtn_Click(object sender, EventArgs e)
        {
            DoubleSpeedbtn.ContextMenuStrip.Show(DoubleSpeedbtn, new Point(0, DoubleSpeedbtn.Height));
        }

        private void Imagebar_Scroll_1(object sender, EventArgs e)
        {
            currentIndex = Imagebar.Value;
            ShowImage(currentIndex);
        }

        private void ImgDeletebtn_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0) return;

            string currentFilePath = imageFiles[currentIndex];
            string fileNameOnly = Path.GetFileName(currentFilePath);

            DialogResult confirm = MessageBox.Show(
                "현재 프레임을 화면 및 학습 데이터셋에서 제외할까요?\n(실물 이미지 파일은 삭제되지 않습니다.)",
                "데이터 제외", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (Imagepic.Image != null)
                {
                    Imagepic.Image.Dispose();
                    Imagepic.Image = null;
                }

                if (!deletedFiles.Contains(fileNameOnly))
                    deletedFiles.Add(fileNameOnly);
                imageFiles = imageFiles.Where(f => f != currentFilePath).ToArray();

                if (imageFiles.Length == 0)
                {
                    Imagebar.Value = 0;
                    MessageBox.Show("데이터셋 내에 남은 프레임이 없습니다.", "알림");
                    return;
                }

                if (currentIndex >= imageFiles.Length)
                    currentIndex = imageFiles.Length - 1;

                Imagebar.Maximum = imageFiles.Length - 1;
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
                    string imagesPath = Path.GetDirectoryName(imageFiles[0]);
                    string newFileName = Path.Combine(imagesPath, Path.GetFileName(selectedFile));

                    if (selectedFile != newFileName)
                        File.Copy(selectedFile, newFileName, overwrite: true);

                    List<string> fileList = imageFiles.ToList();
                    fileList.Insert(currentIndex + 1, newFileName);
                    imageFiles = fileList.ToArray();

                    currentIndex = currentIndex + 1;
                    ShowImage(currentIndex);
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
            if (string.IsNullOrEmpty(Imgtxt.Text)) return;

            string dataPath = Imgtxt.Text;
            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

            List<double> angles = new List<double>();
            List<double> throttles = new List<double>();

            foreach (string catalogFile in catalogFiles)
            {
                string[] lines = File.ReadAllLines(catalogFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    JObject json = JObject.Parse(line);
                    angles.Add((double)json["user/angle"]);
                    throttles.Add((double)json["user/throttle"]);
                }
            }

            Graph.Series.Clear();
            Graph.ChartAreas[0].AxisX.Title = "Index";
            Graph.ChartAreas[0].AxisY.Title = "Value";
            Graph.ChartAreas[0].AxisY.Minimum = -1.0;
            Graph.ChartAreas[0].AxisY.Maximum = 1.0;

            Series angleSeries = new Series("Angle");
            angleSeries.ChartType = SeriesChartType.Line;
            angleSeries.Color = Color.CornflowerBlue;
            for (int i = 0; i < angles.Count; i++)
                angleSeries.Points.AddXY(i, angles[i]);

            Series throttleSeries = new Series("Throttle");
            throttleSeries.ChartType = SeriesChartType.Line;
            throttleSeries.Color = Color.OrangeRed;
            for (int i = 0; i < throttles.Count; i++)
                throttleSeries.Points.AddXY(i, throttles[i]);

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
            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

            List<double> angles = new List<double>();
            List<double> throttles = new List<double>();

            foreach (string catalogFile in catalogFiles)
            {
                string[] lines = File.ReadAllLines(catalogFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    JObject json = JObject.Parse(line);
                    angles.Add((double)json["user/angle"]);
                    throttles.Add((double)json["user/throttle"]);
                }
            }

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

        private void Restorebtn_Click_1(object sender, EventArgs e)
        {
            if (deletedFiles.Count == 0)
            {
                MessageBox.Show("복구할 데이터(제외된 프레임)가 존재하지 않습니다.", "알림");
                return;
            }

            string lastExcludedFile = deletedFiles[deletedFiles.Count - 1];
            deletedFiles.RemoveAt(deletedFiles.Count - 1);

            string dataPath = Imgtxt.Text;
            string imagesPath = Path.Combine(dataPath, "images");

            if (Directory.Exists(imagesPath))
            {
                imageFiles = Directory.GetFiles(imagesPath, "*.jpg")
                                      .OrderBy(f => f)
                                      .Where(f => !deletedFiles.Contains(Path.GetFileName(f)))
                                      .ToArray();

                Imagebar.Maximum = imageFiles.Length - 1;
                MessageBox.Show($"[{lastExcludedFile}] 주행 프레임이 성공적으로 복구되었습니다.", "복구 완료");

                if (currentIndex >= imageFiles.Length)
                    currentIndex = imageFiles.Length - 1;

                ShowImage(currentIndex);
            }
        }

        private void GoToImage_Click(object sender, EventArgs e)
        {
            // 1. 새 창(Form3) 생성
            Form3 form3 = new Form3();

            // 3. Form3 열기 (이제 창 크기 마구 늘렸다 줄였다 테스트 가능!)
            form3.Show();

            // 4. 현재 창은 잠시 숨기기
            this.Hide();
        }
    } 
}