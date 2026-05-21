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
using Newtonsoft.Json.Linq; // 추가
using System.Windows.Forms.DataVisualization.Charting;  //추가

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

        private void OpenImgBrowserbtn_Click(object sender, EventArgs e)    // 이미지 브라우저 열기
        {
            if (imageFiles.Length == 0) return;

            // 임시 HTML 파일 생성
            string tempHtmlPath = Path.Combine(Path.GetTempPath(), "donkey_viewer.html");

            // 이미지 경로 리스트를 JS 배열로 변환
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
    <div id='controls'>← → 방향키로 이미지 넘기기</div>
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

            // 기본 브라우저로 열기
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

            // 그래프 초기화
            Graph.Series.Clear();
            Graph.ChartAreas[0].AxisX.Title = "Index";
            Graph.ChartAreas[0].AxisY.Title = "Value";
            Graph.ChartAreas[0].AxisY.Minimum = -1.0;
            Graph.ChartAreas[0].AxisY.Maximum = 1.0;

            // Angle 시리즈
            Series angleSeries = new Series("Angle");
            angleSeries.ChartType = SeriesChartType.Line;
            angleSeries.Color = Color.CornflowerBlue;
            for (int i = 0; i < angles.Count; i++)
                angleSeries.Points.AddXY(i, angles[i]);

            // Throttle 시리즈
            Series throttleSeries = new Series("Throttle");
            throttleSeries.ChartType = SeriesChartType.Line;
            throttleSeries.Color = Color.OrangeRed;
            for (int i = 0; i < throttles.Count; i++)
                throttleSeries.Points.AddXY(i, throttles[i]);

            Graph.Series.Add(angleSeries);
            Graph.Series.Add(throttleSeries);
        }

        private void Graph_Click(object sender, EventArgs e)
        {

        }

        private void RefreshGraphbtn_Click(object sender, EventArgs e)
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
    }
}
