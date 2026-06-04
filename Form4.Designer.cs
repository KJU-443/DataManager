namespace DataManager
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Imagepic = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ImageNumberlbl = new System.Windows.Forms.Label();
            this.Reversebtn = new System.Windows.Forms.Button();
            this.Plsybtn = new System.Windows.Forms.Button();
            this.NextImgbtn = new System.Windows.Forms.Button();
            this.PreviousImgbtn = new System.Windows.Forms.Button();
            this.PlayAndStopbtn = new System.Windows.Forms.Button();
            this.OpenImgBrowserbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DoubleSpeedbtn = new System.Windows.Forms.Button();
            this.DoubleSpeedtxt = new System.Windows.Forms.TextBox();
            this.Imagebar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.UserThrottlebar = new System.Windows.Forms.ProgressBar();
            this.PilotThrottlebar = new System.Windows.Forms.ProgressBar();
            this.UserAnglebar = new System.Windows.Forms.ProgressBar();
            this.PilotAnglelbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PilotThrottlelbl = new System.Windows.Forms.Label();
            this.UserAnglelbl = new System.Windows.Forms.Label();
            this.UserThrottlelbl = new System.Windows.Forms.Label();
            this.PilotAnglebar = new System.Windows.Forms.ProgressBar();
            this.GoTrainbtn = new System.Windows.Forms.Button();
            this.GoToResultbtn = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.GoToTrainbtn = new System.Windows.Forms.Button();
            this.GoDatabtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Imagepic
            // 
            this.Imagepic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagepic.BackColor = System.Drawing.Color.White;
            this.Imagepic.Location = new System.Drawing.Point(12, 59);
            this.Imagepic.Name = "Imagepic";
            this.Imagepic.Size = new System.Drawing.Size(1042, 720);
            this.Imagepic.TabIndex = 6;
            this.Imagepic.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ImageNumberlbl);
            this.groupBox1.Controls.Add(this.Reversebtn);
            this.groupBox1.Controls.Add(this.Plsybtn);
            this.groupBox1.Controls.Add(this.NextImgbtn);
            this.groupBox1.Controls.Add(this.PreviousImgbtn);
            this.groupBox1.Controls.Add(this.PlayAndStopbtn);
            this.groupBox1.Controls.Add(this.OpenImgBrowserbtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DoubleSpeedbtn);
            this.groupBox1.Controls.Add(this.DoubleSpeedtxt);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(1060, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 767);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "이미지 관련 설정";
            // 
            // ImageNumberlbl
            // 
            this.ImageNumberlbl.AutoSize = true;
            this.ImageNumberlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageNumberlbl.ForeColor = System.Drawing.Color.Black;
            this.ImageNumberlbl.Location = new System.Drawing.Point(91, 239);
            this.ImageNumberlbl.Name = "ImageNumberlbl";
            this.ImageNumberlbl.Size = new System.Drawing.Size(151, 29);
            this.ImageNumberlbl.TabIndex = 17;
            this.ImageNumberlbl.Text = "(1000/1000)";
            // 
            // Reversebtn
            // 
            this.Reversebtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Reversebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Reversebtn.ForeColor = System.Drawing.Color.Black;
            this.Reversebtn.Location = new System.Drawing.Point(14, 586);
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
            this.Plsybtn.Location = new System.Drawing.Point(188, 586);
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
            this.NextImgbtn.Location = new System.Drawing.Point(188, 491);
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
            this.PreviousImgbtn.Location = new System.Drawing.Point(14, 491);
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
            this.PlayAndStopbtn.Location = new System.Drawing.Point(13, 681);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 237);
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
            this.DoubleSpeedbtn.Location = new System.Drawing.Point(188, 402);
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
            this.DoubleSpeedtxt.Location = new System.Drawing.Point(14, 408);
            this.DoubleSpeedtxt.Name = "DoubleSpeedtxt";
            this.DoubleSpeedtxt.Size = new System.Drawing.Size(171, 39);
            this.DoubleSpeedtxt.TabIndex = 2;
            // 
            // Imagebar
            // 
            this.Imagebar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagebar.Location = new System.Drawing.Point(12, 795);
            this.Imagebar.Name = "Imagebar";
            this.Imagebar.Size = new System.Drawing.Size(1418, 69);
            this.Imagebar.TabIndex = 8;
            this.Imagebar.Scroll += new System.EventHandler(this.Imagebar_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.UserThrottlebar, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.PilotThrottlebar, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.UserAnglebar, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.PilotAnglelbl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.PilotThrottlelbl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.UserAnglelbl, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.UserThrottlelbl, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.PilotAnglebar, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 870);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1407, 138);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // UserThrottlebar
            // 
            this.UserThrottlebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserThrottlebar.Location = new System.Drawing.Point(1128, 98);
            this.UserThrottlebar.Name = "UserThrottlebar";
            this.UserThrottlebar.Size = new System.Drawing.Size(276, 11);
            this.UserThrottlebar.TabIndex = 11;
            // 
            // PilotThrottlebar
            // 
            this.PilotThrottlebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PilotThrottlebar.Location = new System.Drawing.Point(425, 98);
            this.PilotThrottlebar.Name = "PilotThrottlebar";
            this.PilotThrottlebar.Size = new System.Drawing.Size(275, 11);
            this.PilotThrottlebar.TabIndex = 10;
            // 
            // UserAnglebar
            // 
            this.UserAnglebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserAnglebar.Location = new System.Drawing.Point(1128, 29);
            this.UserAnglebar.Name = "UserAnglebar";
            this.UserAnglebar.Size = new System.Drawing.Size(276, 11);
            this.UserAnglebar.TabIndex = 9;
            // 
            // PilotAnglelbl
            // 
            this.PilotAnglelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PilotAnglelbl.AutoSize = true;
            this.PilotAnglelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PilotAnglelbl.Location = new System.Drawing.Point(284, 16);
            this.PilotAnglelbl.Name = "PilotAnglelbl";
            this.PilotAnglelbl.Size = new System.Drawing.Size(135, 37);
            this.PilotAnglelbl.TabIndex = 4;
            this.PilotAnglelbl.Text = "+00.000";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "pilot / angle";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 37);
            this.label3.TabIndex = 1;
            this.label3.Text = "pilot / throttle";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(706, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 37);
            this.label5.TabIndex = 3;
            this.label5.Text = "user / throttle";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(706, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 37);
            this.label4.TabIndex = 2;
            this.label4.Text = "user / angle";
            // 
            // PilotThrottlelbl
            // 
            this.PilotThrottlelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PilotThrottlelbl.AutoSize = true;
            this.PilotThrottlelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PilotThrottlelbl.Location = new System.Drawing.Point(284, 85);
            this.PilotThrottlelbl.Name = "PilotThrottlelbl";
            this.PilotThrottlelbl.Size = new System.Drawing.Size(135, 37);
            this.PilotThrottlelbl.TabIndex = 5;
            this.PilotThrottlelbl.Text = "+00.000";
            // 
            // UserAnglelbl
            // 
            this.UserAnglelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UserAnglelbl.AutoSize = true;
            this.UserAnglelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserAnglelbl.Location = new System.Drawing.Point(987, 16);
            this.UserAnglelbl.Name = "UserAnglelbl";
            this.UserAnglelbl.Size = new System.Drawing.Size(135, 37);
            this.UserAnglelbl.TabIndex = 6;
            this.UserAnglelbl.Text = "+00.000";
            // 
            // UserThrottlelbl
            // 
            this.UserThrottlelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UserThrottlelbl.AutoSize = true;
            this.UserThrottlelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserThrottlelbl.Location = new System.Drawing.Point(987, 85);
            this.UserThrottlelbl.Name = "UserThrottlelbl";
            this.UserThrottlelbl.Size = new System.Drawing.Size(135, 37);
            this.UserThrottlelbl.TabIndex = 7;
            this.UserThrottlelbl.Text = "+00.000";
            // 
            // PilotAnglebar
            // 
            this.PilotAnglebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PilotAnglebar.Location = new System.Drawing.Point(425, 29);
            this.PilotAnglebar.Name = "PilotAnglebar";
            this.PilotAnglebar.Size = new System.Drawing.Size(275, 11);
            this.PilotAnglebar.TabIndex = 8;
            // 
            // GoTrainbtn
            // 
            this.GoTrainbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoTrainbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoTrainbtn.ForeColor = System.Drawing.Color.Black;
            this.GoTrainbtn.Location = new System.Drawing.Point(12, 12);
            this.GoTrainbtn.Name = "GoTrainbtn";
            this.GoTrainbtn.Size = new System.Drawing.Size(234, 36);
            this.GoTrainbtn.TabIndex = 10;
            this.GoTrainbtn.Tag = "noTheme";
            this.GoTrainbtn.Text = "훈련 페이지로 가기";
            this.GoTrainbtn.UseVisualStyleBackColor = false;
            // 
            // GoToResultbtn
            // 
            this.GoToResultbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoToResultbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoToResultbtn.Location = new System.Drawing.Point(813, 10);
            this.GoToResultbtn.Name = "GoToResultbtn";
            this.GoToResultbtn.Size = new System.Drawing.Size(241, 38);
            this.GoToResultbtn.TabIndex = 22;
            this.GoToResultbtn.Tag = "noTheme";
            this.GoToResultbtn.Text = "훈련 결과 페이지로 가기";
            this.GoToResultbtn.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.GoToTrainbtn);
            this.panel5.Controls.Add(this.GoDatabtn);
            this.panel5.Location = new System.Drawing.Point(12, 11);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1042, 42);
            this.panel5.TabIndex = 12;
            // 
            // GoToTrainbtn
            // 
            this.GoToTrainbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoToTrainbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoToTrainbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoToTrainbtn.Location = new System.Drawing.Point(803, 4);
            this.GoToTrainbtn.Name = "GoToTrainbtn";
            this.GoToTrainbtn.Size = new System.Drawing.Size(236, 38);
            this.GoToTrainbtn.TabIndex = 20;
            this.GoToTrainbtn.Tag = "noTheme";
            this.GoToTrainbtn.Text = "훈련 페이지로 가기";
            this.GoToTrainbtn.UseVisualStyleBackColor = false;
            this.GoToTrainbtn.Click += new System.EventHandler(this.GoToTrainbtn_Click);
            // 
            // GoDatabtn
            // 
            this.GoDatabtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoDatabtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoDatabtn.Location = new System.Drawing.Point(5, 4);
            this.GoDatabtn.Name = "GoDatabtn";
            this.GoDatabtn.Size = new System.Drawing.Size(221, 38);
            this.GoDatabtn.TabIndex = 3;
            this.GoDatabtn.Tag = "noTheme";
            this.GoDatabtn.Text = "데이터 페이지로 가기";
            this.GoDatabtn.UseVisualStyleBackColor = false;
            this.GoDatabtn.Click += new System.EventHandler(this.GoDatabtn_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 1030);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Imagebar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Imagepic);
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Imagepic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ImageNumberlbl;
        private System.Windows.Forms.Button Reversebtn;
        private System.Windows.Forms.Button Plsybtn;
        private System.Windows.Forms.Button NextImgbtn;
        private System.Windows.Forms.Button PreviousImgbtn;
        private System.Windows.Forms.Button PlayAndStopbtn;
        private System.Windows.Forms.Button OpenImgBrowserbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DoubleSpeedbtn;
        private System.Windows.Forms.TextBox DoubleSpeedtxt;
        private System.Windows.Forms.TrackBar Imagebar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar UserThrottlebar;
        private System.Windows.Forms.ProgressBar PilotThrottlebar;
        private System.Windows.Forms.ProgressBar UserAnglebar;
        private System.Windows.Forms.Label PilotAnglelbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PilotThrottlelbl;
        private System.Windows.Forms.Label UserAnglelbl;
        private System.Windows.Forms.Label UserThrottlelbl;
        private System.Windows.Forms.ProgressBar PilotAnglebar;
        private System.Windows.Forms.Button GoTrainbtn;
        private System.Windows.Forms.Button GoToResultbtn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button GoToTrainbtn;
        private System.Windows.Forms.Button GoDatabtn;
    }
}