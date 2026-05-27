namespace DataManager
{
    partial class Form3
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TypeChoicelbl = new System.Windows.Forms.Label();
            this.SortMethodcom = new System.Windows.Forms.ComboBox();
            this.ImageDeletebtn = new System.Windows.Forms.Button();
            this.GoDatabtn = new System.Windows.Forms.Button();
            this.Massagelbl = new System.Windows.Forms.Label();
            this.ImageRestorebtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SellectAllbtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.GoDatabtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Massagelbl, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1418, 1006);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.SellectAllbtn);
            this.panel2.Controls.Add(this.ImageRestorebtn);
            this.panel2.Controls.Add(this.TypeChoicelbl);
            this.panel2.Controls.Add(this.SortMethodcom);
            this.panel2.Controls.Add(this.ImageDeletebtn);
            this.panel2.Location = new System.Drawing.Point(3, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1412, 72);
            this.panel2.TabIndex = 7;
            // 
            // TypeChoicelbl
            // 
            this.TypeChoicelbl.AutoSize = true;
            this.TypeChoicelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TypeChoicelbl.Location = new System.Drawing.Point(13, 11);
            this.TypeChoicelbl.Name = "TypeChoicelbl";
            this.TypeChoicelbl.Size = new System.Drawing.Size(271, 40);
            this.TypeChoicelbl.TabIndex = 10;
            this.TypeChoicelbl.Text = "이미지 리스트 선택";
            // 
            // SortMethodcom
            // 
            this.SortMethodcom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SortMethodcom.FormattingEnabled = true;
            this.SortMethodcom.Items.AddRange(new object[] {
            "이미지 삭제",
            "이미지 복구"});
            this.SortMethodcom.Location = new System.Drawing.Point(290, 15);
            this.SortMethodcom.Name = "SortMethodcom";
            this.SortMethodcom.Size = new System.Drawing.Size(384, 40);
            this.SortMethodcom.TabIndex = 9;
            // 
            // ImageDeletebtn
            // 
            this.ImageDeletebtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageDeletebtn.BackColor = System.Drawing.Color.DarkSalmon;
            this.ImageDeletebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImageDeletebtn.Location = new System.Drawing.Point(1204, 2);
            this.ImageDeletebtn.Name = "ImageDeletebtn";
            this.ImageDeletebtn.Size = new System.Drawing.Size(205, 69);
            this.ImageDeletebtn.TabIndex = 7;
            this.ImageDeletebtn.Text = "이미지 삭제";
            this.ImageDeletebtn.UseVisualStyleBackColor = false;
            this.ImageDeletebtn.Click += new System.EventHandler(this.TrainingStartbtn_Click);
            // 
            // GoDatabtn
            // 
            this.GoDatabtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoDatabtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoDatabtn.Location = new System.Drawing.Point(3, 3);
            this.GoDatabtn.Name = "GoDatabtn";
            this.GoDatabtn.Size = new System.Drawing.Size(173, 38);
            this.GoDatabtn.TabIndex = 3;
            this.GoDatabtn.Text = "데이터";
            this.GoDatabtn.UseVisualStyleBackColor = false;
            this.GoDatabtn.Click += new System.EventHandler(this.GoDatabtn_Click);
            // 
            // Massagelbl
            // 
            this.Massagelbl.AutoSize = true;
            this.Massagelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Massagelbl.Location = new System.Drawing.Point(3, 966);
            this.Massagelbl.Name = "Massagelbl";
            this.Massagelbl.Size = new System.Drawing.Size(93, 25);
            this.Massagelbl.TabIndex = 9;
            this.Massagelbl.Text = "경고 텍스트";
            // 
            // ImageRestorebtn
            // 
            this.ImageRestorebtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageRestorebtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ImageRestorebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ImageRestorebtn.Location = new System.Drawing.Point(993, 2);
            this.ImageRestorebtn.Name = "ImageRestorebtn";
            this.ImageRestorebtn.Size = new System.Drawing.Size(205, 69);
            this.ImageRestorebtn.TabIndex = 11;
            this.ImageRestorebtn.Text = "이미지 복구";
            this.ImageRestorebtn.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 129);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1412, 834);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // SellectAllbtn
            // 
            this.SellectAllbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SellectAllbtn.BackColor = System.Drawing.Color.LightGray;
            this.SellectAllbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SellectAllbtn.Location = new System.Drawing.Point(782, 2);
            this.SellectAllbtn.Name = "SellectAllbtn";
            this.SellectAllbtn.Size = new System.Drawing.Size(205, 69);
            this.SellectAllbtn.TabIndex = 12;
            this.SellectAllbtn.Text = "전체 선택";
            this.SellectAllbtn.UseVisualStyleBackColor = false;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 1030);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ImageRestorebtn;
        private System.Windows.Forms.Label TypeChoicelbl;
        private System.Windows.Forms.ComboBox SortMethodcom;
        private System.Windows.Forms.Button ImageDeletebtn;
        private System.Windows.Forms.Button GoDatabtn;
        private System.Windows.Forms.Label Massagelbl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button SellectAllbtn;
    }
}