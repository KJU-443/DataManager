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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Imgtxt = new System.Windows.Forms.TextBox();
            this.SelectImgbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Foldertxt = new System.Windows.Forms.TextBox();
            this.SelectFolderbtn = new System.Windows.Forms.Button();
            this.Imagebar = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OpenGraphBrowserbtn = new System.Windows.Forms.Button();
            this.RefreshGraphbtn = new System.Windows.Forms.Button();
            this.Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GoTrainbtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Imagebar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.GoTrainbtn, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.95238F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.04762F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 534F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1418, 1006);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 46);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1412, 55);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Imgtxt);
            this.panel2.Controls.Add(this.SelectImgbtn);
            this.panel2.Location = new System.Drawing.Point(709, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(700, 49);
            this.panel2.TabIndex = 1;
            // 
            // Imgtxt
            // 
            this.Imgtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Imgtxt.Location = new System.Drawing.Point(284, 0);
            this.Imgtxt.Name = "Imgtxt";
            this.Imgtxt.Size = new System.Drawing.Size(413, 44);
            this.Imgtxt.TabIndex = 1;
            // 
            // SelectImgbtn
            // 
            this.SelectImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelectImgbtn.Location = new System.Drawing.Point(6, 0);
            this.SelectImgbtn.Name = "SelectImgbtn";
            this.SelectImgbtn.Size = new System.Drawing.Size(275, 49);
            this.SelectImgbtn.TabIndex = 0;
            this.SelectImgbtn.Text = "이미지 폴더 선택";
            this.SelectImgbtn.UseVisualStyleBackColor = true;
            this.SelectImgbtn.Click += new System.EventHandler(this.SelectImgbtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Foldertxt);
            this.panel1.Controls.Add(this.SelectFolderbtn);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 49);
            this.panel1.TabIndex = 0;
            // 
            // Foldertxt
            // 
            this.Foldertxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Foldertxt.Location = new System.Drawing.Point(284, 0);
            this.Foldertxt.Name = "Foldertxt";
            this.Foldertxt.Size = new System.Drawing.Size(413, 44);
            this.Foldertxt.TabIndex = 1;
            // 
            // SelectFolderbtn
            // 
            this.SelectFolderbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelectFolderbtn.Location = new System.Drawing.Point(3, -1);
            this.SelectFolderbtn.Name = "SelectFolderbtn";
            this.SelectFolderbtn.Size = new System.Drawing.Size(275, 51);
            this.SelectFolderbtn.TabIndex = 0;
            this.SelectFolderbtn.Text = "폴더 지정";
            this.SelectFolderbtn.UseVisualStyleBackColor = true;
            this.SelectFolderbtn.Click += new System.EventHandler(this.SelectFolderbtn_Click);
            // 
            // Imagebar
            // 
            this.Imagebar.Location = new System.Drawing.Point(3, 642);
            this.Imagebar.Name = "Imagebar";
            this.Imagebar.Size = new System.Drawing.Size(1412, 40);
            this.Imagebar.TabIndex = 1;
            this.Imagebar.Scroll += new System.EventHandler(this.Imagebar_Scroll);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.Graph);
            this.panel3.Location = new System.Drawing.Point(3, 688);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1412, 315);
            this.panel3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OpenGraphBrowserbtn);
            this.groupBox2.Controls.Add(this.RefreshGraphbtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(1042, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 312);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "그래프 관련 설정";
            // 
            // OpenGraphBrowserbtn
            // 
            this.OpenGraphBrowserbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OpenGraphBrowserbtn.Location = new System.Drawing.Point(13, 118);
            this.OpenGraphBrowserbtn.Name = "OpenGraphBrowserbtn";
            this.OpenGraphBrowserbtn.Size = new System.Drawing.Size(346, 51);
            this.OpenGraphBrowserbtn.TabIndex = 12;
            this.OpenGraphBrowserbtn.Text = "   ";
            this.OpenGraphBrowserbtn.UseVisualStyleBackColor = true;
            this.OpenGraphBrowserbtn.Click += new System.EventHandler(this.OpenGraphBrowserbtn_Click);
            // 
            // RefreshGraphbtn
            // 
            this.RefreshGraphbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RefreshGraphbtn.Location = new System.Drawing.Point(13, 52);
            this.RefreshGraphbtn.Name = "RefreshGraphbtn";
            this.RefreshGraphbtn.Size = new System.Drawing.Size(346, 51);
            this.RefreshGraphbtn.TabIndex = 11;
            this.RefreshGraphbtn.Text = "그래프 새로고침";
            this.RefreshGraphbtn.UseVisualStyleBackColor = true;
            this.RefreshGraphbtn.Click += new System.EventHandler(this.RefreshGraphbtn_Click);
            // 
            // Graph
            // 
            chartArea5.Name = "ChartArea1";
            this.Graph.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.Graph.Legends.Add(legend5);
            this.Graph.Location = new System.Drawing.Point(0, 0);
            this.Graph.Name = "Graph";
            series5.BorderWidth = 3;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.Graph.Series.Add(series5);
            this.Graph.Size = new System.Drawing.Size(1036, 315);
            this.Graph.TabIndex = 0;
            this.Graph.Text = "chart1";
            this.Graph.Click += new System.EventHandler(this.Graph_Click);
            // 
            // GoTrainbtn
            // 
            this.GoTrainbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoTrainbtn.Location = new System.Drawing.Point(3, 3);
            this.GoTrainbtn.Name = "GoTrainbtn";
            this.GoTrainbtn.Size = new System.Drawing.Size(173, 37);
            this.GoTrainbtn.TabIndex = 3;
            this.GoTrainbtn.Text = "훈련";
            this.GoTrainbtn.UseVisualStyleBackColor = true;
            this.GoTrainbtn.Click += new System.EventHandler(this.GoTrainbtn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.5543F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.4457F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Imagepic, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 117);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1418, 535);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
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
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(1045, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 529);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "이미지 관련 설정";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Reversebtn
            // 
            this.Reversebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Reversebtn.Location = new System.Drawing.Point(13, 375);
            this.Reversebtn.Name = "Reversebtn";
            this.Reversebtn.Size = new System.Drawing.Size(171, 51);
            this.Reversebtn.TabIndex = 14;
            this.Reversebtn.Text = "<<";
            this.Reversebtn.UseVisualStyleBackColor = true;
            this.Reversebtn.Click += new System.EventHandler(this.Reversebtn_Click);
            // 
            // Plsybtn
            // 
            this.Plsybtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Plsybtn.Location = new System.Drawing.Point(188, 375);
            this.Plsybtn.Name = "Plsybtn";
            this.Plsybtn.Size = new System.Drawing.Size(171, 51);
            this.Plsybtn.TabIndex = 13;
            this.Plsybtn.Text = ">>";
            this.Plsybtn.UseVisualStyleBackColor = true;
            this.Plsybtn.Click += new System.EventHandler(this.Plsybtn_Click);
            // 
            // NextImgbtn
            // 
            this.NextImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NextImgbtn.Location = new System.Drawing.Point(188, 293);
            this.NextImgbtn.Name = "NextImgbtn";
            this.NextImgbtn.Size = new System.Drawing.Size(171, 51);
            this.NextImgbtn.TabIndex = 12;
            this.NextImgbtn.Text = ">";
            this.NextImgbtn.UseVisualStyleBackColor = true;
            this.NextImgbtn.Click += new System.EventHandler(this.NextImgbtn_Click);
            // 
            // PreviousImgbtn
            // 
            this.PreviousImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PreviousImgbtn.Location = new System.Drawing.Point(13, 293);
            this.PreviousImgbtn.Name = "PreviousImgbtn";
            this.PreviousImgbtn.Size = new System.Drawing.Size(171, 51);
            this.PreviousImgbtn.TabIndex = 11;
            this.PreviousImgbtn.Text = "<";
            this.PreviousImgbtn.UseVisualStyleBackColor = true;
            this.PreviousImgbtn.Click += new System.EventHandler(this.PreviousImgbtn_Click);
            // 
            // PlayAndStopbtn
            // 
            this.PlayAndStopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PlayAndStopbtn.Location = new System.Drawing.Point(13, 457);
            this.PlayAndStopbtn.Name = "PlayAndStopbtn";
            this.PlayAndStopbtn.Size = new System.Drawing.Size(346, 51);
            this.PlayAndStopbtn.TabIndex = 10;
            this.PlayAndStopbtn.Text = "재생";
            this.PlayAndStopbtn.UseVisualStyleBackColor = true;
            this.PlayAndStopbtn.Click += new System.EventHandler(this.PlayAndStopbtn_Click);
            // 
            // OpenImgBrowserbtn
            // 
            this.OpenImgBrowserbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OpenImgBrowserbtn.Location = new System.Drawing.Point(13, 47);
            this.OpenImgBrowserbtn.Name = "OpenImgBrowserbtn";
            this.OpenImgBrowserbtn.Size = new System.Drawing.Size(346, 51);
            this.OpenImgBrowserbtn.TabIndex = 1;
            this.OpenImgBrowserbtn.Text = "브라우저로 이미지 열기";
            this.OpenImgBrowserbtn.UseVisualStyleBackColor = true;
            this.OpenImgBrowserbtn.Click += new System.EventHandler(this.OpenImgBrowserbtn_Click);
            // 
            // ImgAddbtn
            // 
            this.ImgAddbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImgAddbtn.Location = new System.Drawing.Point(239, 129);
            this.ImgAddbtn.Name = "ImgAddbtn";
            this.ImgAddbtn.Size = new System.Drawing.Size(120, 51);
            this.ImgAddbtn.TabIndex = 2;
            this.ImgAddbtn.Text = "추가";
            this.ImgAddbtn.UseVisualStyleBackColor = true;
            this.ImgAddbtn.Click += new System.EventHandler(this.ImgAddbtn_Click);
            // 
            // ImgDeletebtn
            // 
            this.ImgDeletebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImgDeletebtn.Location = new System.Drawing.Point(113, 129);
            this.ImgDeletebtn.Name = "ImgDeletebtn";
            this.ImgDeletebtn.Size = new System.Drawing.Size(120, 51);
            this.ImgDeletebtn.TabIndex = 4;
            this.ImgDeletebtn.Text = "삭제";
            this.ImgDeletebtn.UseVisualStyleBackColor = true;
            this.ImgDeletebtn.Click += new System.EventHandler(this.ImgDeletebtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(8, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "이미지";
            // 
            // DoubleSpeedbtn
            // 
            this.DoubleSpeedbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DoubleSpeedbtn.Location = new System.Drawing.Point(188, 211);
            this.DoubleSpeedbtn.Name = "DoubleSpeedbtn";
            this.DoubleSpeedbtn.Size = new System.Drawing.Size(171, 51);
            this.DoubleSpeedbtn.TabIndex = 5;
            this.DoubleSpeedbtn.Text = "배속";
            this.DoubleSpeedbtn.UseVisualStyleBackColor = true;
            this.DoubleSpeedbtn.Click += new System.EventHandler(this.DoubleSpeedbtn_Click);
            // 
            // DoubleSpeedtxt
            // 
            this.DoubleSpeedtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DoubleSpeedtxt.Location = new System.Drawing.Point(13, 213);
            this.DoubleSpeedtxt.Name = "DoubleSpeedtxt";
            this.DoubleSpeedtxt.Size = new System.Drawing.Size(171, 44);
            this.DoubleSpeedtxt.TabIndex = 2;
            // 
            // Imagepic
            // 
            this.Imagepic.BackColor = System.Drawing.Color.White;
            this.Imagepic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Imagepic.Location = new System.Drawing.Point(3, 3);
            this.Imagepic.Name = "Imagepic";
            this.Imagepic.Size = new System.Drawing.Size(1036, 529);
            this.Imagepic.TabIndex = 3;
            this.Imagepic.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 1030);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "DataManager V1.0";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SelectFolderbtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox Imgtxt;
        private System.Windows.Forms.Button SelectImgbtn;
        private System.Windows.Forms.TextBox Foldertxt;
        private System.Windows.Forms.TrackBar Imagebar;
        private System.Windows.Forms.TextBox DoubleSpeedtxt;
        private System.Windows.Forms.Button DoubleSpeedbtn;
        private System.Windows.Forms.Button ImgDeletebtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ImgAddbtn;
        private System.Windows.Forms.Button OpenImgBrowserbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button PreviousImgbtn;
        private System.Windows.Forms.Button PlayAndStopbtn;
        private System.Windows.Forms.Button Reversebtn;
        private System.Windows.Forms.Button Plsybtn;
        private System.Windows.Forms.Button NextImgbtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graph;
        private System.Windows.Forms.PictureBox Imagepic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button OpenGraphBrowserbtn;
        private System.Windows.Forms.Button RefreshGraphbtn;
        private System.Windows.Forms.Button GoTrainbtn;
    }
}

