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
using System.Threading;
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
        private System.Windows.Forms.Timer playTimer = new System.Windows.Forms.Timer();
        private bool isReverse = false;
        private bool isPlaying = false;
        private double currentSpeed = 1.0;
        private double[] speedLevels = { 0.25, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };
        private int speedIndex = 2;

        private CancellationTokenSource imageCts = new CancellationTokenSource();

        // 복구(Restore) 기능을 위한 메모리 백업 저장소
        private List<string> originalCatalogLines = new List<string>();
        private List<string> deletedFiles = new List<string>();

        private List<CatalogRecord> catalogRecords = new List<CatalogRecord>();

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

            // 전체 catalog 파일 가져오기
            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                             .Where(f => !f.EndsWith(".catalog_manifest"))
                                             .OrderBy(f => f)
                                             .ToArray();

            if (catalogFiles.Length > 0 && originalCatalogLines.Count > 0)
            {
                try
                {
                    // 삭제된 파일 목록 기준으로 필터링
                    var finalLines = originalCatalogLines
                        .Where(line => !deletedFiles.Any(deletedFile => line.Contains(deletedFile)))
                        .ToList();

                    // 각 catalog 파일에 해당하는 라인만 분배해서 저장
                    foreach (string catalogFile in catalogFiles)
                    {
                        string catalogFileName = Path.GetFileNameWithoutExtension(catalogFile);

                        // 해당 catalog 번호 추출 (예: catalog_0 → 0)
                        int catalogIndex = int.Parse(catalogFileName.Split('_')[1]);

                        // 해당 catalog에 속하는 라인만 필터링 (_index 기준)
                        var linesForThisCatalog = finalLines
                            .Where(line =>
                            {
                                if (string.IsNullOrWhiteSpace(line)) return false;
                                try
                                {
                                    JObject json = JObject.Parse(line);
                                    int index = (int)json["_index"];
                                    // catalog당 1000개 기준 (동키카 기본값)
                                    return index / 1000 == catalogIndex;
                                }
                                catch { return false; }
                            })
                            .ToList();

                        File.WriteAllLines(catalogFile, linesForThisCatalog);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"카탈로그 데이터셋 저장 중 오류 발생: {ex.Message}", "오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Form2 form2 = new Form2(selectedFolderPath, Imgtxt.Text);
            form2.Show();
            this.Hide();
        }
        private async void SelectImgbtn_Click(object sender, EventArgs e)
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

                    string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog")
                                 .Where(f => !f.EndsWith(".catalog_manifest"))
                                 .OrderBy(f => f)
                                 .ToArray();

                    originalCatalogLines.Clear();
                    deletedFiles.Clear();
                    catalogRecords.Clear();

                    foreach (string catalogFile in catalogFiles)
                    {
                        foreach (string line in File.ReadLines(catalogFile))
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;
                            originalCatalogLines.Add(line);
                            CatalogRecord record = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogRecord>(line);
                            if (record != null) catalogRecords.Add(record);
                        }
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
                            await ShowImage(currentIndex);
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

                // 새 이미지 로드 완료 후 한 번에 교체
                var oldImage = Imagepic.Image;
                Imagepic.Image = bmp;
                Imagepic.SizeMode = PictureBoxSizeMode.Zoom;
                oldImage?.Dispose(); // 교체 후 기존 이미지 해제
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }

            // angle/throttle 라벨 표시
            if (index < catalogRecords.Count)
            {
                AngleFigurelbl.Text = $"angle : {catalogRecords[index].Angle:F3}";
                TrottleFigurelbl.Text = $"throttle : {catalogRecords[index].Throttle:F3}";
            }
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

        private void DoubleSpeedbtn_Click(object sender, EventArgs e)
        {
            DoubleSpeedbtn.ContextMenuStrip.Show(DoubleSpeedbtn, new Point(0, DoubleSpeedbtn.Height));
        }

        private async void Imagebar_Scroll_1(object sender, EventArgs e)
        {
            currentIndex = Imagebar.Value;
            await ShowImage(currentIndex);
        }

        private async void ImgDeletebtn_Click(object sender, EventArgs e)
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
            if (catalogRecords.Count == 0) return;

            Graph.Series.Clear();
            Graph.ChartAreas[0].AxisX.Title = "Index";
            Graph.ChartAreas[0].AxisY.Title = "Value";
            Graph.ChartAreas[0].AxisY.Minimum = -1.0;
            Graph.ChartAreas[0].AxisY.Maximum = 1.0;

            Series angleSeries = new Series("Angle");
            angleSeries.ChartType = SeriesChartType.Line;
            angleSeries.Color = Color.CornflowerBlue;

            Series throttleSeries = new Series("Throttle");
            throttleSeries.ChartType = SeriesChartType.Line;
            throttleSeries.Color = Color.OrangeRed;

            for (int i = 0; i < catalogRecords.Count; i++)
            {
                angleSeries.Points.AddXY(i, catalogRecords[i].Angle);
                throttleSeries.Points.AddXY(i, catalogRecords[i].Throttle);
            }

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

        private async void Restorebtn_Click_1(object sender, EventArgs e)
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

                await ShowImage(currentIndex);
            }
        }

        private void GoToImage_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(imageFiles, deletedFiles, this);
            form3.Show();
            this.Hide();
        }

        public void RestoreImage(string imgPath)
        {
            if (!imageFiles.Contains(imgPath))
            {
                imageFiles = imageFiles.Concat(new[] { imgPath })
                                       .OrderBy(f => f)
                                       .ToArray();
                Imagebar.Maximum = imageFiles.Length - 1;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Imagepic_Click(object sender, EventArgs e)
        {

        }
    } 
}

