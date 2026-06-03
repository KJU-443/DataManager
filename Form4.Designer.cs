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
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Imagepic
            // 
            this.Imagepic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagepic.BackColor = System.Drawing.Color.White;
            this.Imagepic.Location = new System.Drawing.Point(16, 16);
            this.Imagepic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Imagepic.Name = "Imagepic";
            this.Imagepic.Size = new System.Drawing.Size(1355, 1023);
            this.Imagepic.TabIndex = 6;
            this.Imagepic.TabStop = false;
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
            this.groupBox1.Location = new System.Drawing.Point(1378, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(481, 1023);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "이미지 관련 설정";
            // 
            // ImageNumberlbl
            // 
            this.ImageNumberlbl.AutoSize = true;
            this.ImageNumberlbl.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageNumberlbl.ForeColor = System.Drawing.Color.Black;
            this.ImageNumberlbl.Location = new System.Drawing.Point(118, 319);
            this.ImageNumberlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ImageNumberlbl.Name = "ImageNumberlbl";
            this.ImageNumberlbl.Size = new System.Drawing.Size(212, 42);
            this.ImageNumberlbl.TabIndex = 17;
            this.ImageNumberlbl.Text = "(1000/1000)";
            // 
            // GoToImage
            // 
            this.GoToImage.BackColor = System.Drawing.Color.PowderBlue;
            this.GoToImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoToImage.ForeColor = System.Drawing.Color.Black;
            this.GoToImage.Location = new System.Drawing.Point(17, 189);
            this.GoToImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GoToImage.Name = "GoToImage";
            this.GoToImage.Size = new System.Drawing.Size(450, 68);
            this.GoToImage.TabIndex = 16;
            this.GoToImage.Tag = "noTheme";
            this.GoToImage.Text = "복구 이미지 리스트 보기";
            this.GoToImage.UseVisualStyleBackColor = false;
            // 
            // Restorebtn
            // 
            this.Restorebtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Restorebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Restorebtn.ForeColor = System.Drawing.Color.Black;
            this.Restorebtn.Location = new System.Drawing.Point(17, 417);
            this.Restorebtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Restorebtn.Name = "Restorebtn";
            this.Restorebtn.Size = new System.Drawing.Size(144, 68);
            this.Restorebtn.TabIndex = 15;
            this.Restorebtn.Tag = "noTheme";
            this.Restorebtn.Text = "복구";
            this.Restorebtn.UseVisualStyleBackColor = false;
            // 
            // Reversebtn
            // 
            this.Reversebtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Reversebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Reversebtn.ForeColor = System.Drawing.Color.Black;
            this.Reversebtn.Location = new System.Drawing.Point(18, 781);
            this.Reversebtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Reversebtn.Name = "Reversebtn";
            this.Reversebtn.Size = new System.Drawing.Size(222, 68);
            this.Reversebtn.TabIndex = 14;
            this.Reversebtn.Text = "<<";
            this.Reversebtn.UseVisualStyleBackColor = false;
            // 
            // Plsybtn
            // 
            this.Plsybtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Plsybtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Plsybtn.ForeColor = System.Drawing.Color.Black;
            this.Plsybtn.Location = new System.Drawing.Point(244, 781);
            this.Plsybtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Plsybtn.Name = "Plsybtn";
            this.Plsybtn.Size = new System.Drawing.Size(222, 68);
            this.Plsybtn.TabIndex = 13;
            this.Plsybtn.Text = ">>";
            this.Plsybtn.UseVisualStyleBackColor = false;
            // 
            // NextImgbtn
            // 
            this.NextImgbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.NextImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NextImgbtn.ForeColor = System.Drawing.Color.Black;
            this.NextImgbtn.Location = new System.Drawing.Point(244, 655);
            this.NextImgbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NextImgbtn.Name = "NextImgbtn";
            this.NextImgbtn.Size = new System.Drawing.Size(222, 68);
            this.NextImgbtn.TabIndex = 12;
            this.NextImgbtn.Text = ">";
            this.NextImgbtn.UseVisualStyleBackColor = false;
            // 
            // PreviousImgbtn
            // 
            this.PreviousImgbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.PreviousImgbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PreviousImgbtn.ForeColor = System.Drawing.Color.Black;
            this.PreviousImgbtn.Location = new System.Drawing.Point(18, 655);
            this.PreviousImgbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PreviousImgbtn.Name = "PreviousImgbtn";
            this.PreviousImgbtn.Size = new System.Drawing.Size(222, 68);
            this.PreviousImgbtn.TabIndex = 11;
            this.PreviousImgbtn.Text = "<";
            this.PreviousImgbtn.UseVisualStyleBackColor = false;
            // 
            // PlayAndStopbtn
            // 
            this.PlayAndStopbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.PlayAndStopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.PlayAndStopbtn.Location = new System.Drawing.Point(17, 908);
            this.PlayAndStopbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlayAndStopbtn.Name = "PlayAndStopbtn";
            this.PlayAndStopbtn.Size = new System.Drawing.Size(450, 68);
            this.PlayAndStopbtn.TabIndex = 10;
            this.PlayAndStopbtn.Tag = "noTheme";
            this.PlayAndStopbtn.Text = "재생";
            this.PlayAndStopbtn.UseVisualStyleBackColor = false;
            // 
            // OpenImgBrowserbtn
            // 
            this.OpenImgBrowserbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.OpenImgBrowserbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OpenImgBrowserbtn.ForeColor = System.Drawing.Color.Black;
            this.OpenImgBrowserbtn.Location = new System.Drawing.Point(17, 63);
            this.OpenImgBrowserbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OpenImgBrowserbtn.Name = "OpenImgBrowserbtn";
            this.OpenImgBrowserbtn.Size = new System.Drawing.Size(450, 68);
            this.OpenImgBrowserbtn.TabIndex = 1;
            this.OpenImgBrowserbtn.Tag = "noTheme";
            this.OpenImgBrowserbtn.Text = "브라우저로 이미지 열기";
            this.OpenImgBrowserbtn.UseVisualStyleBackColor = false;
            // 
            // ImgAddbtn
            // 
            this.ImgAddbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ImgAddbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImgAddbtn.ForeColor = System.Drawing.Color.Black;
            this.ImgAddbtn.Location = new System.Drawing.Point(311, 417);
            this.ImgAddbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImgAddbtn.Name = "ImgAddbtn";
            this.ImgAddbtn.Size = new System.Drawing.Size(156, 68);
            this.ImgAddbtn.TabIndex = 2;
            this.ImgAddbtn.Tag = "noTheme";
            this.ImgAddbtn.Text = "추가";
            this.ImgAddbtn.UseVisualStyleBackColor = false;
            // 
            // ImgDeletebtn
            // 
            this.ImgDeletebtn.BackColor = System.Drawing.Color.DarkSalmon;
            this.ImgDeletebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImgDeletebtn.ForeColor = System.Drawing.Color.Black;
            this.ImgDeletebtn.Location = new System.Drawing.Point(162, 417);
            this.ImgDeletebtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImgDeletebtn.Name = "ImgDeletebtn";
            this.ImgDeletebtn.Size = new System.Drawing.Size(147, 68);
            this.ImgDeletebtn.TabIndex = 4;
            this.ImgDeletebtn.Tag = "noTheme";
            this.ImgDeletebtn.Text = "삭제";
            this.ImgDeletebtn.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 316);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 44);
            this.label2.TabIndex = 3;
            this.label2.Text = "이미지";
            // 
            // DoubleSpeedbtn
            // 
            this.DoubleSpeedbtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.DoubleSpeedbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DoubleSpeedbtn.ForeColor = System.Drawing.Color.Black;
            this.DoubleSpeedbtn.Location = new System.Drawing.Point(244, 536);
            this.DoubleSpeedbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoubleSpeedbtn.Name = "DoubleSpeedbtn";
            this.DoubleSpeedbtn.Size = new System.Drawing.Size(222, 68);
            this.DoubleSpeedbtn.TabIndex = 5;
            this.DoubleSpeedbtn.Text = "배속";
            this.DoubleSpeedbtn.UseVisualStyleBackColor = false;
            // 
            // DoubleSpeedtxt
            // 
            this.DoubleSpeedtxt.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.DoubleSpeedtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DoubleSpeedtxt.ForeColor = System.Drawing.Color.Black;
            this.DoubleSpeedtxt.Location = new System.Drawing.Point(18, 544);
            this.DoubleSpeedtxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoubleSpeedtxt.Name = "DoubleSpeedtxt";
            this.DoubleSpeedtxt.Size = new System.Drawing.Size(221, 50);
            this.DoubleSpeedtxt.TabIndex = 2;
            // 
            // Imagebar
            // 
            this.Imagebar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagebar.Location = new System.Drawing.Point(16, 1060);
            this.Imagebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Imagebar.Name = "Imagebar";
            this.Imagebar.Size = new System.Drawing.Size(1843, 90);
            this.Imagebar.TabIndex = 8;
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 1160);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1829, 184);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // UserThrottlebar
            // 
            this.UserThrottlebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserThrottlebar.Location = new System.Drawing.Point(1465, 130);
            this.UserThrottlebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UserThrottlebar.Name = "UserThrottlebar";
            this.UserThrottlebar.Size = new System.Drawing.Size(360, 15);
            this.UserThrottlebar.TabIndex = 11;
            // 
            // PilotThrottlebar
            // 
            this.PilotThrottlebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PilotThrottlebar.Location = new System.Drawing.Point(552, 130);
            this.PilotThrottlebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PilotThrottlebar.Name = "PilotThrottlebar";
            this.PilotThrottlebar.Size = new System.Drawing.Size(357, 15);
            this.PilotThrottlebar.TabIndex = 10;
            // 
            // UserAnglebar
            // 
            this.UserAnglebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserAnglebar.Location = new System.Drawing.Point(1465, 38);
            this.UserAnglebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UserAnglebar.Name = "UserAnglebar";
            this.UserAnglebar.Size = new System.Drawing.Size(360, 15);
            this.UserAnglebar.TabIndex = 9;
            // 
            // PilotAnglelbl
            // 
            this.PilotAnglelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PilotAnglelbl.AutoSize = true;
            this.PilotAnglelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PilotAnglelbl.Location = new System.Drawing.Point(365, 20);
            this.PilotAnglelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PilotAnglelbl.Name = "PilotAnglelbl";
            this.PilotAnglelbl.Size = new System.Drawing.Size(179, 51);
            this.PilotAnglelbl.TabIndex = 4;
            this.PilotAnglelbl.Text = "+00.000";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "pilot / angle";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 92);
            this.label3.TabIndex = 1;
            this.label3.Text = "pilot / throttle";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(917, 92);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 92);
            this.label5.TabIndex = 3;
            this.label5.Text = "user / throttle";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(917, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 51);
            this.label4.TabIndex = 2;
            this.label4.Text = "user / angle";
            // 
            // PilotThrottlelbl
            // 
            this.PilotThrottlelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PilotThrottlelbl.AutoSize = true;
            this.PilotThrottlelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PilotThrottlelbl.Location = new System.Drawing.Point(365, 112);
            this.PilotThrottlelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PilotThrottlelbl.Name = "PilotThrottlelbl";
            this.PilotThrottlelbl.Size = new System.Drawing.Size(179, 51);
            this.PilotThrottlelbl.TabIndex = 5;
            this.PilotThrottlelbl.Text = "+00.000";
            // 
            // UserAnglelbl
            // 
            this.UserAnglelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UserAnglelbl.AutoSize = true;
            this.UserAnglelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserAnglelbl.Location = new System.Drawing.Point(1278, 20);
            this.UserAnglelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserAnglelbl.Name = "UserAnglelbl";
            this.UserAnglelbl.Size = new System.Drawing.Size(179, 51);
            this.UserAnglelbl.TabIndex = 6;
            this.UserAnglelbl.Text = "+00.000";
            // 
            // UserThrottlelbl
            // 
            this.UserThrottlelbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UserThrottlelbl.AutoSize = true;
            this.UserThrottlelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserThrottlelbl.Location = new System.Drawing.Point(1278, 112);
            this.UserThrottlelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserThrottlelbl.Name = "UserThrottlelbl";
            this.UserThrottlelbl.Size = new System.Drawing.Size(179, 51);
            this.UserThrottlelbl.TabIndex = 7;
            this.UserThrottlelbl.Text = "+00.000";
            // 
            // PilotAnglebar
            // 
            this.PilotAnglebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PilotAnglebar.Location = new System.Drawing.Point(552, 38);
            this.PilotAnglebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PilotAnglebar.Name = "PilotAnglebar";
            this.PilotAnglebar.Size = new System.Drawing.Size(357, 15);
            this.PilotAnglebar.TabIndex = 8;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1875, 1373);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Imagebar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Imagepic);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.Imagepic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagebar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Imagepic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ImageNumberlbl;
        private System.Windows.Forms.Button GoToImage;
        private System.Windows.Forms.Button Restorebtn;
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
    }
}