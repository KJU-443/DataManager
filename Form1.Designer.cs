namespace DataManager
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OpenGraphBrowserbtn = new System.Windows.Forms.Button();
            this.RefreshGraphbtn = new System.Windows.Forms.Button();
            this.Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GoTrainbtn = new System.Windows.Forms.Button();
            this.Imagebar = new System.Windows.Forms.TrackBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ImageFilteringbtn = new System.Windows.Forms.Button();
            this.TrottleDowntxt = new System.Windows.Forms.TextBox();
            this.None1lbl = new System.Windows.Forms.Label();
            this.TrottleUptxt = new System.Windows.Forms.TextBox();
            this.TrottleTextlbl = new System.Windows.Forms.Label();
            this.AngleDowntxt = new System.Windows.Forms.TextBox();
            this.None2lbl = new System.Windows.Forms.Label();
            this.AngleTextlbl = new System.Windows.Forms.Label();
            this.AngleUptxt = new System.Windows.Forms.TextBox();
            this.TrottleFigurelbl = new System.Windows.Forms.Label();
            this.AngleFigurelbl = new System.Windows.Forms.Label();
            this.Imagelst = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GoToImage = new System.Windows.Forms.Button();
            this.Restorebtn = new System.Windows.Forms.Button();
            this.Reversebtn = new System.Windows.Forms.Button();
            this.Plsybtn = new System.Windows.Forms.Button();
            this.NextImgbtn = new System.Windows.Forms.Button();
            this.PreviousImgbtn = new System.Windows.Forms.Button();
            this.PlayAndStopbtn = new System.Windows.Forms.Button();
            this.OpenImgBrowserbtn = new System.Windows.Forms.Button();
            this.ImgAddbtn = new System.Windows.Forms.Button();
            this.ImgDeletebtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DoubleSpeedbtn = new System.Windows.Forms.Button();
            this.DoubleSpeedtxt = new System.Windows.Forms.TextBox();
            this.Imagepic = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Foldertxt = new System.Windows.Forms.TextBox();
            this.Imgtxt = new System.Windows.Forms.TextBox();
            this.SelectImgbtn = new System.Windows.Forms.Button();
            this.SelectFolderbtn = new System.Windows.Forms.Button();
            this.ImageNumberlbl = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.GoTrainbtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Imagebar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1418, 1023);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.Graph);
            this.panel3.Location = new System.Drawing.Point(3, 706);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1412, 314);
            this.panel3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.OpenGraphBrowserbtn);
            this.groupBox2.Controls.Add(this.RefreshGraphbtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(1042, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 312);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "그래프 관련 설정";
            // 
            // OpenGraphBrowserbtn
            // 
            this.OpenGraphBrowserbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.OpenGraphBrowserbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OpenGraphBrowserbtn.ForeColor = System.Drawing.Color.Black;
            this.OpenGraphBrowserbtn.Location = new System.Drawing.Point(13, 118);
            this.OpenGraphBrowserbtn.Name = "OpenGraphBrowserbtn";
            this.OpenGraphBrowserbtn.Size = new System.Drawing.Size(346, 51);
            this.OpenGraphBrowserbtn.TabIndex = 12;
            this.OpenGraphBrowserbtn.Tag = "noTheme";
            this.OpenGraphBrowserbtn.Text = "브라우저로 그래프 열기";
            this.OpenGraphBrowserbtn.UseVisualStyleBackColor = false;
            this.OpenGraphBrowserbtn.Click += new System.EventHandler(this.OpenGraphBrowserbtn_Click);
            // 
            // RefreshGraphbtn
            // 
            this.RefreshGraphbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.RefreshGraphbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RefreshGraphbtn.ForeColor = System.Drawing.Color.Black;
            this.RefreshGraphbtn.Location = new System.Drawing.Point(13, 52);
            this.RefreshGraphbtn.Name = "RefreshGraphbtn";
            this.RefreshGraphbtn.Size = new System.Drawing.Size(346, 51);
            this.RefreshGraphbtn.TabIndex = 11;
            this.RefreshGraphbtn.Tag = "noTheme";
            this.RefreshGraphbtn.Text = "그래프 새로고침";
            this.RefreshGraphbtn.UseVisualStyleBackColor = false;
            this.RefreshGraphbtn.Click += new System.EventHandler(this.RefreshGraphbtn_Click_1);
            // 
            // Graph
            // 
            this.Graph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.Graph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Graph.Legends.Add(legend1);
            this.Graph.Location = new System.Drawing.Point(0, -1);
            this.Graph.Name = "Graph";
            this.Graph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Graph.Series.Add(series1);
            this.Graph.Size = new System.Drawing.Size(1035, 315);
            this.Graph.TabIndex = 0;
            this.Graph.Text = "chart1";
            // 
            // GoTrainbtn
            // 
            this.GoTrainbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoTrainbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoTrainbtn.ForeColor = System.Drawing.Color.Black;
            this.GoTrainbtn.Location = new System.Drawing.Point(3, 3);
            this.GoTrainbtn.Name = "GoTrainbtn";
            this.GoTrainbtn.Size = new System.Drawing.Size(173, 36);
            this.GoTrainbtn.TabIndex = 3;
            this.GoTrainbtn.Tag = "noTheme";
            this.GoTrainbtn.Text = "훈련";
            this.GoTrainbtn.UseVisualStyleBackColor = false;
            this.GoTrainbtn.Click += new System.EventHandler(this.GoTrainbtn_Click);
            // 
            // Imagebar
            // 
            this.Imagebar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagebar.Location = new System.Drawing.Point(3, 668);
            this.Imagebar.Name = "Imagebar";
            this.Imagebar.Size = new System.Drawing.Size(1412, 32);
            this.Imagebar.TabIndex = 1;
            this.Imagebar.Scroll += new System.EventHandler(this.Imagebar_Scroll_1);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.ImageFilteringbtn);
            this.panel4.Controls.Add(this.TrottleDowntxt);
            this.panel4.Controls.Add(this.None1lbl);
            this.panel4.Controls.Add(this.TrottleUptxt);
            this.panel4.Controls.Add(this.TrottleTextlbl);
            this.panel4.Controls.Add(this.AngleDowntxt);
            this.panel4.Controls.Add(this.None2lbl);
            this.panel4.Controls.Add(this.AngleTextlbl);
            this.panel4.Controls.Add(this.AngleUptxt);
            this.panel4.Controls.Add(this.TrottleFigurelbl);
            this.panel4.Controls.Add(this.AngleFigurelbl);
            this.panel4.Controls.Add(this.Imagelst);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.Imagepic);
            this.panel4.Location = new System.Drawing.Point(3, 111);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1412, 551);
            this.panel4.TabIndex = 4;
            // 
            // ImageFilteringbtn
            // 
            this.ImageFilteringbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImageFilteringbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.ImageFilteringbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImageFilteringbtn.ForeColor = System.Drawing.Color.Black;
            this.ImageFilteringbtn.Location = new System.Drawing.Point(9, 405);
            this.ImageFilteringbtn.Name = "ImageFilteringbtn";
            this.ImageFilteringbtn.Size = new System.Drawing.Size(259, 51);
            this.ImageFilteringbtn.TabIndex = 17;
            this.ImageFilteringbtn.Tag = "noTheme";
            this.ImageFilteringbtn.Text = "이미지 필터링 하기";
            this.ImageFilteringbtn.UseVisualStyleBackColor = false;
            this.ImageFilteringbtn.Click += new System.EventHandler(this.ImageFilteringbtn_Click);
            // 
            // TrottleDowntxt
            // 
            this.TrottleDowntxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TrottleDowntxt.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrottleDowntxt.Location = new System.Drawing.Point(201, 354);
            this.TrottleDowntxt.Name = "TrottleDowntxt";
            this.TrottleDowntxt.Size = new System.Drawing.Size(67, 39);
            this.TrottleDowntxt.TabIndex = 17;
            // 
            // None1lbl
            // 
            this.None1lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.None1lbl.AutoSize = true;
            this.None1lbl.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.None1lbl.Location = new System.Drawing.Point(170, 357);
            this.None1lbl.Name = "None1lbl";
            this.None1lbl.Size = new System.Drawing.Size(30, 31);
            this.None1lbl.TabIndex = 16;
            this.None1lbl.Text = "~";
            // 
            // TrottleUptxt
            // 
            this.TrottleUptxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TrottleUptxt.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrottleUptxt.Location = new System.Drawing.Point(102, 354);
            this.TrottleUptxt.Name = "TrottleUptxt";
            this.TrottleUptxt.Size = new System.Drawing.Size(67, 39);
            this.TrottleUptxt.TabIndex = 15;
            // 
            // TrottleTextlbl
            // 
            this.TrottleTextlbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TrottleTextlbl.AutoSize = true;
            this.TrottleTextlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrottleTextlbl.Location = new System.Drawing.Point(3, 357);
            this.TrottleTextlbl.Name = "TrottleTextlbl";
            this.TrottleTextlbl.Size = new System.Drawing.Size(94, 29);
            this.TrottleTextlbl.TabIndex = 14;
            this.TrottleTextlbl.Text = "throttle";
            // 
            // AngleDowntxt
            // 
            this.AngleDowntxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AngleDowntxt.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AngleDowntxt.Location = new System.Drawing.Point(201, 310);
            this.AngleDowntxt.Name = "AngleDowntxt";
            this.AngleDowntxt.Size = new System.Drawing.Size(67, 39);
            this.AngleDowntxt.TabIndex = 12;
            // 
            // None2lbl
            // 
            this.None2lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.None2lbl.AutoSize = true;
            this.None2lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.None2lbl.Location = new System.Drawing.Point(170, 313);
            this.None2lbl.Name = "None2lbl";
            this.None2lbl.Size = new System.Drawing.Size(28, 29);
            this.None2lbl.TabIndex = 11;
            this.None2lbl.Text = "~";
            // 
            // AngleTextlbl
            // 
            this.AngleTextlbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AngleTextlbl.AutoSize = true;
            this.AngleTextlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AngleTextlbl.Location = new System.Drawing.Point(26, 313);
            this.AngleTextlbl.Name = "AngleTextlbl";
            this.AngleTextlbl.Size = new System.Drawing.Size(78, 29);
            this.AngleTextlbl.TabIndex = 10;
            this.AngleTextlbl.Text = "angle";
            // 
            // AngleUptxt
            // 
            this.AngleUptxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AngleUptxt.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AngleUptxt.Location = new System.Drawing.Point(102, 310);
            this.AngleUptxt.Name = "AngleUptxt";
            this.AngleUptxt.Size = new System.Drawing.Size(67, 39);
            this.AngleUptxt.TabIndex = 9;
            // 
            // TrottleFigurelbl
            // 
            this.TrottleFigurelbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TrottleFigurelbl.AutoSize = true;
            this.TrottleFigurelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrottleFigurelbl.Location = new System.Drawing.Point(3, 509);
            this.TrottleFigurelbl.Name = "TrottleFigurelbl";
            this.TrottleFigurelbl.Size = new System.Drawing.Size(129, 29);
            this.TrottleFigurelbl.TabIndex = 8;
            this.TrottleFigurelbl.Text = "throttle : 0";
            // 
            // AngleFigurelbl
            // 
            this.AngleFigurelbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AngleFigurelbl.AutoSize = true;
            this.AngleFigurelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.AngleFigurelbl.Location = new System.Drawing.Point(26, 465);
            this.AngleFigurelbl.Name = "AngleFigurelbl";
            this.AngleFigurelbl.Size = new System.Drawing.Size(113, 29);
            this.AngleFigurelbl.TabIndex = 7;
            this.AngleFigurelbl.Text = "angle : 0";
            this.AngleFigurelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AngleFigurelbl.Click += new System.EventHandler(this.label1_Click);
            // 
            // Imagelst
            // 
            this.Imagelst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Imagelst.Font = new System.Drawing.Font("한컴 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Imagelst.FormattingEnabled = true;
            this.Imagelst.ItemHeight = 26;
            this.Imagelst.Location = new System.Drawing.Point(3, 6);
            this.Imagelst.Name = "Imagelst";
            this.Imagelst.Size = new System.Drawing.Size(265, 264);
            this.Imagelst.TabIndex = 6;
            this.Imagelst.SelectedIndexChanged += new System.EventHandler(this.Imagelst_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ImageNumberlbl);
            this.groupBox1.Controls.Add(this.GoToImage);
            this.groupBox1.Controls.Add(this.Restorebtn);
            this.groupBox1.Controls.Add(this.Reversebtn);
            this.groupBox1.Controls.Add(this.Plsybtn);
            this.groupBox1.Controls.Add(this.NextImgbtn);
            this.groupBox1.Controls.Add(this.PreviousImgbtn);
            this.groupBox1.Controls.Add(this.PlayAndStopbtn);
            this.groupBox1.Controls.Add(this.OpenImgBrowserbtn);
            this.groupBox1.Controls.Add(this.ImgAddbtn);
            this.groupBox1.Controls.Add(this.ImgDeletebtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DoubleSpeedbtn);
            this.groupBox1.Controls.Add(this.DoubleSpeedtxt);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(1042, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 547);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "이미지 관련 설정";
            // 
            // GoToImage
            // 
            this.GoToImage.BackColor = System.Drawing.Color.PowderBlue;
            this.GoToImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoToImage.ForeColor = System.Drawing.Color.Black;
            this.GoToImage.Location = new System.Drawing.Point(13, 119);
            this.GoToImage.Name = "GoToImage";
            this.GoToImage.Size = new System.Drawing.Size(346, 51);
            this.GoToImage.TabIndex = 16;
            this.GoToImage.Tag = "noTheme";
            this.GoToImage.Text = "복구 이미지 리스트 보기";
            this.GoToImage.UseVisualStyleBackColor = false;
            this.GoToImage.Click += new System.EventHandler(this.GoToImage_Click);
            // 
            // Restorebtn
            // 
            this.Restorebtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Restorebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Restorebtn.ForeColor = System.Drawing.Color.Black;
            this.Restorebtn.Location = new System.Drawing.Point(13, 220);
            this.Restorebtn.Name = "Restorebtn";
            this.Restorebtn.Size = new System.Drawing.Size(111, 51);
            this.Restorebtn.TabIndex = 15;
            this.Restorebtn.Tag = "noTheme";
            this.Restorebtn.Text = "복구";
            this.Restorebtn.UseVisualStyleBackColor = false;
            this.Restorebtn.Click += new System.EventHandler(this.Restorebtn_Click_1);
            // 
            // Reversebtn
            // 
            this.Reversebtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Reversebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Reversebtn.ForeColor = System.Drawing.Color.Black;
            this.Reversebtn.Location = new System.Drawing.Point(14, 413);
            this.Reversebtn.Name = "Reversebtn";
            this.Reversebtn.Size = new System.Drawing.Size(171, 51);
            this.Reversebtn.TabIndex = 14;
            this.Reversebtn.Text = "<<";
            this.Reversebtn.UseVisualStyleBackColor = false;
            this.Reversebtn.Click += new System.EventHandler(this.Reversebtn_Click);
            // 
            // Plsybtn
            // 
            this.Plsybtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Plsybtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Plsybtn.ForeColor = System.Drawing.Color.Black;
            this.Plsybtn.Location = new System.Drawing.Point(188, 413);
            this.Plsybtn.Name = "Plsybtn";
            this.Plsybtn.Size = new System.Drawing.Size(171, 51);
            this.Plsybtn.TabIndex = 13;
            this.Plsybtn.Text = ">>";
            this.Plsybtn.UseVisualStyleBackColor = false;
            this.Plsybtn.Click += new System.EventHandler(this.Plsybtn_Click);
            // 
            // NextImgbtn
            // 
            this.NextImgbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.NextImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NextImgbtn.ForeColor = System.Drawing.Color.Black;
            this.NextImgbtn.Location = new System.Drawing.Point(188, 349);
            this.NextImgbtn.Name = "NextImgbtn";
            this.NextImgbtn.Size = new System.Drawing.Size(171, 51);
            this.NextImgbtn.TabIndex = 12;
            this.NextImgbtn.Text = ">";
            this.NextImgbtn.UseVisualStyleBackColor = false;
            this.NextImgbtn.Click += new System.EventHandler(this.NextImgbtn_Click);
            // 
            // PreviousImgbtn
            // 
            this.PreviousImgbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.PreviousImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PreviousImgbtn.ForeColor = System.Drawing.Color.Black;
            this.PreviousImgbtn.Location = new System.Drawing.Point(14, 349);
            this.PreviousImgbtn.Name = "PreviousImgbtn";
            this.PreviousImgbtn.Size = new System.Drawing.Size(171, 51);
            this.PreviousImgbtn.TabIndex = 11;
            this.PreviousImgbtn.Text = "<";
            this.PreviousImgbtn.UseVisualStyleBackColor = false;
            this.PreviousImgbtn.Click += new System.EventHandler(this.PreviousImgbtn_Click);
            // 
            // PlayAndStopbtn
            // 
            this.PlayAndStopbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.PlayAndStopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PlayAndStopbtn.Location = new System.Drawing.Point(13, 477);
            this.PlayAndStopbtn.Name = "PlayAndStopbtn";
            this.PlayAndStopbtn.Size = new System.Drawing.Size(346, 51);
            this.PlayAndStopbtn.TabIndex = 10;
            this.PlayAndStopbtn.Tag = "noTheme";
            this.PlayAndStopbtn.Text = "재생";
            this.PlayAndStopbtn.UseVisualStyleBackColor = false;
            this.PlayAndStopbtn.Click += new System.EventHandler(this.PlayAndStopbtn_Click);
            // 
            // OpenImgBrowserbtn
            // 
            this.OpenImgBrowserbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.OpenImgBrowserbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OpenImgBrowserbtn.ForeColor = System.Drawing.Color.Black;
            this.OpenImgBrowserbtn.Location = new System.Drawing.Point(13, 47);
            this.OpenImgBrowserbtn.Name = "OpenImgBrowserbtn";
            this.OpenImgBrowserbtn.Size = new System.Drawing.Size(346, 51);
            this.OpenImgBrowserbtn.TabIndex = 1;
            this.OpenImgBrowserbtn.Tag = "noTheme";
            this.OpenImgBrowserbtn.Text = "브라우저로 이미지 열기";
            this.OpenImgBrowserbtn.UseVisualStyleBackColor = false;
            this.OpenImgBrowserbtn.Click += new System.EventHandler(this.OpenImgBrowserbtn_Click);
            // 
            // ImgAddbtn
            // 
            this.ImgAddbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ImgAddbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImgAddbtn.ForeColor = System.Drawing.Color.Black;
            this.ImgAddbtn.Location = new System.Drawing.Point(239, 221);
            this.ImgAddbtn.Name = "ImgAddbtn";
            this.ImgAddbtn.Size = new System.Drawing.Size(120, 51);
            this.ImgAddbtn.TabIndex = 2;
            this.ImgAddbtn.Tag = "noTheme";
            this.ImgAddbtn.Text = "추가";
            this.ImgAddbtn.UseVisualStyleBackColor = false;
            this.ImgAddbtn.Click += new System.EventHandler(this.ImgAddbtn_Click);
            // 
            // ImgDeletebtn
            // 
            this.ImgDeletebtn.BackColor = System.Drawing.Color.DarkSalmon;
            this.ImgDeletebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImgDeletebtn.ForeColor = System.Drawing.Color.Black;
            this.ImgDeletebtn.Location = new System.Drawing.Point(125, 221);
            this.ImgDeletebtn.Name = "ImgDeletebtn";
            this.ImgDeletebtn.Size = new System.Drawing.Size(113, 51);
            this.ImgDeletebtn.TabIndex = 4;
            this.ImgDeletebtn.Tag = "noTheme";
            this.ImgDeletebtn.Text = "삭제";
            this.ImgDeletebtn.UseVisualStyleBackColor = false;
            this.ImgDeletebtn.Click += new System.EventHandler(this.ImgDeletebtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "이미지";
            // 
            // DoubleSpeedbtn
            // 
            this.DoubleSpeedbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.DoubleSpeedbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DoubleSpeedbtn.ForeColor = System.Drawing.Color.Black;
            this.DoubleSpeedbtn.Location = new System.Drawing.Point(188, 285);
            this.DoubleSpeedbtn.Name = "DoubleSpeedbtn";
            this.DoubleSpeedbtn.Size = new System.Drawing.Size(171, 51);
            this.DoubleSpeedbtn.TabIndex = 5;
            this.DoubleSpeedbtn.Text = "배속";
            this.DoubleSpeedbtn.UseVisualStyleBackColor = false;
            this.DoubleSpeedbtn.Click += new System.EventHandler(this.DoubleSpeedbtn_Click);
            // 
            // DoubleSpeedtxt
            // 
            this.DoubleSpeedtxt.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.DoubleSpeedtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DoubleSpeedtxt.ForeColor = System.Drawing.Color.Black;
            this.DoubleSpeedtxt.Location = new System.Drawing.Point(14, 291);
            this.DoubleSpeedtxt.Name = "DoubleSpeedtxt";
            this.DoubleSpeedtxt.Size = new System.Drawing.Size(171, 39);
            this.DoubleSpeedtxt.TabIndex = 2;
            // 
            // Imagepic
            // 
            this.Imagepic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagepic.BackColor = System.Drawing.Color.White;
            this.Imagepic.Location = new System.Drawing.Point(274, 3);
            this.Imagepic.Name = "Imagepic";
            this.Imagepic.Size = new System.Drawing.Size(761, 546);
            this.Imagepic.TabIndex = 5;
            this.Imagepic.TabStop = false;
            this.Imagepic.Click += new System.EventHandler(this.Imagepic_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Foldertxt);
            this.panel1.Controls.Add(this.Imgtxt);
            this.panel1.Controls.Add(this.SelectImgbtn);
            this.panel1.Controls.Add(this.SelectFolderbtn);
            this.panel1.Location = new System.Drawing.Point(3, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1412, 54);
            this.panel1.TabIndex = 5;
            // 
            // Foldertxt
            // 
            this.Foldertxt.BackColor = System.Drawing.Color.White;
            this.Foldertxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Foldertxt.ForeColor = System.Drawing.Color.Black;
            this.Foldertxt.Location = new System.Drawing.Point(284, 15);
            this.Foldertxt.Name = "Foldertxt";
            this.Foldertxt.Size = new System.Drawing.Size(413, 26);
            this.Foldertxt.TabIndex = 4;
            // 
            // Imgtxt
            // 
            this.Imgtxt.BackColor = System.Drawing.Color.White;
            this.Imgtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Imgtxt.ForeColor = System.Drawing.Color.Black;
            this.Imgtxt.Location = new System.Drawing.Point(988, 16);
            this.Imgtxt.Name = "Imgtxt";
            this.Imgtxt.Size = new System.Drawing.Size(413, 26);
            this.Imgtxt.TabIndex = 5;
            // 
            // SelectImgbtn
            // 
            this.SelectImgbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.SelectImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelectImgbtn.ForeColor = System.Drawing.Color.Black;
            this.SelectImgbtn.Location = new System.Drawing.Point(707, 0);
            this.SelectImgbtn.Name = "SelectImgbtn";
            this.SelectImgbtn.Size = new System.Drawing.Size(275, 55);
            this.SelectImgbtn.TabIndex = 3;
            this.SelectImgbtn.Text = "이미지 폴더 선택";
            this.SelectImgbtn.UseVisualStyleBackColor = false;
            this.SelectImgbtn.Click += new System.EventHandler(this.SelectImgbtn_Click);
            // 
            // SelectFolderbtn
            // 
            this.SelectFolderbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.SelectFolderbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelectFolderbtn.ForeColor = System.Drawing.Color.Black;
            this.SelectFolderbtn.Location = new System.Drawing.Point(3, 0);
            this.SelectFolderbtn.Name = "SelectFolderbtn";
            this.SelectFolderbtn.Size = new System.Drawing.Size(275, 55);
            this.SelectFolderbtn.TabIndex = 2;
            this.SelectFolderbtn.Text = "폴더 지정";
            this.SelectFolderbtn.UseVisualStyleBackColor = false;
            this.SelectFolderbtn.Click += new System.EventHandler(this.SelectFolderbtn_Click_1);
            // 
            // ImageNumberlbl
            // 
            this.ImageNumberlbl.AutoSize = true;
            this.ImageNumberlbl.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageNumberlbl.ForeColor = System.Drawing.Color.Black;
            this.ImageNumberlbl.Location = new System.Drawing.Point(82, 181);
            this.ImageNumberlbl.Name = "ImageNumberlbl";
            this.ImageNumberlbl.Size = new System.Drawing.Size(159, 31);
            this.ImageNumberlbl.TabIndex = 17;
            this.ImageNumberlbl.Text = "(1000/1000)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1442, 1047);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "DataManager V1.0";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar Imagebar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graph;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button OpenGraphBrowserbtn;
        private System.Windows.Forms.Button RefreshGraphbtn;
        private System.Windows.Forms.Button GoTrainbtn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Reversebtn;
        private System.Windows.Forms.Button Plsybtn;
        private System.Windows.Forms.Button NextImgbtn;
        private System.Windows.Forms.Button PreviousImgbtn;
        private System.Windows.Forms.Button PlayAndStopbtn;
        private System.Windows.Forms.Button OpenImgBrowserbtn;
        private System.Windows.Forms.Button ImgAddbtn;
        private System.Windows.Forms.Button ImgDeletebtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DoubleSpeedbtn;
        private System.Windows.Forms.TextBox DoubleSpeedtxt;
        private System.Windows.Forms.PictureBox Imagepic;
        private System.Windows.Forms.TextBox Foldertxt;
        private System.Windows.Forms.Button SelectFolderbtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Imgtxt;
        private System.Windows.Forms.Button SelectImgbtn;
        private System.Windows.Forms.Button Restorebtn;
        private System.Windows.Forms.Button GoToImage;
        private System.Windows.Forms.Label AngleFigurelbl;
        private System.Windows.Forms.ListBox Imagelst;
        private System.Windows.Forms.TextBox AngleDowntxt;
        private System.Windows.Forms.Label None2lbl;
        private System.Windows.Forms.Label AngleTextlbl;
        private System.Windows.Forms.TextBox AngleUptxt;
        private System.Windows.Forms.Label TrottleFigurelbl;
        private System.Windows.Forms.Label TrottleTextlbl;
        private System.Windows.Forms.Button ImageFilteringbtn;
        private System.Windows.Forms.TextBox TrottleDowntxt;
        private System.Windows.Forms.Label None1lbl;
        private System.Windows.Forms.TextBox TrottleUptxt;
        private System.Windows.Forms.Label ImageNumberlbl;
    }
}