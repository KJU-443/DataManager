namespace DataManager_2
{
    partial class Form2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SortMethodlbl = new System.Windows.Forms.Label();
            this.SortMethodcom = new System.Windows.Forms.ComboBox();
            this.TrainingStartbtn = new System.Windows.Forms.Button();
            this.Traninglst = new System.Windows.Forms.ListBox();
            this.Massagelbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GoToResultbtn = new System.Windows.Forms.Button();
            this.TrainFigurelbl = new System.Windows.Forms.Label();
            this.TrainWronglbl = new System.Windows.Forms.Label();
            this.GoDatabtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Traninglst, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Massagelbl, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1418, 1006);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.SortMethodlbl);
            this.panel2.Controls.Add(this.SortMethodcom);
            this.panel2.Controls.Add(this.TrainingStartbtn);
            this.panel2.Location = new System.Drawing.Point(3, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1412, 72);
            this.panel2.TabIndex = 7;
            // 
            // SortMethodlbl
            // 
            this.SortMethodlbl.AutoSize = true;
            this.SortMethodlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SortMethodlbl.Location = new System.Drawing.Point(13, 11);
            this.SortMethodlbl.Name = "SortMethodlbl";
            this.SortMethodlbl.Size = new System.Drawing.Size(213, 40);
            this.SortMethodlbl.TabIndex = 10;
            this.SortMethodlbl.Text = "보기 방법 변경";
            this.SortMethodlbl.Click += new System.EventHandler(this.SortMethodlbl_Click);
            // 
            // SortMethodcom
            // 
            this.SortMethodcom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SortMethodcom.FormattingEnabled = true;
            this.SortMethodcom.Items.AddRange(new object[] {
            "Tabel",
            "2-Tabel",
            "Card"});
            this.SortMethodcom.Location = new System.Drawing.Point(263, 16);
            this.SortMethodcom.Name = "SortMethodcom";
            this.SortMethodcom.Size = new System.Drawing.Size(384, 40);
            this.SortMethodcom.TabIndex = 9;
            // 
            // TrainingStartbtn
            // 
            this.TrainingStartbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrainingStartbtn.BackColor = System.Drawing.Color.DarkSalmon;
            this.TrainingStartbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrainingStartbtn.Location = new System.Drawing.Point(783, 2);
            this.TrainingStartbtn.Name = "TrainingStartbtn";
            this.TrainingStartbtn.Size = new System.Drawing.Size(625, 69);
            this.TrainingStartbtn.TabIndex = 7;
            this.TrainingStartbtn.Tag = "noTheme";
            this.TrainingStartbtn.Text = "훈련 시작";
            this.TrainingStartbtn.UseVisualStyleBackColor = false;
            this.TrainingStartbtn.Click += new System.EventHandler(this.TrainingStartbtn_Click);
            // 
            // Traninglst
            // 
            this.Traninglst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Traninglst.FormattingEnabled = true;
            this.Traninglst.ItemHeight = 18;
            this.Traninglst.Location = new System.Drawing.Point(3, 129);
            this.Traninglst.Name = "Traninglst";
            this.Traninglst.Size = new System.Drawing.Size(1412, 834);
            this.Traninglst.TabIndex = 8;
            this.Traninglst.SelectedIndexChanged += new System.EventHandler(this.Traninglst_SelectedIndexChanged);
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.GoToResultbtn);
            this.panel1.Controls.Add(this.TrainFigurelbl);
            this.panel1.Controls.Add(this.TrainWronglbl);
            this.panel1.Controls.Add(this.GoDatabtn);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1412, 42);
            this.panel1.TabIndex = 10;
            // 
            // GoToResultbtn
            // 
            this.GoToResultbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoToResultbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoToResultbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoToResultbtn.Location = new System.Drawing.Point(1172, 4);
            this.GoToResultbtn.Name = "GoToResultbtn";
            this.GoToResultbtn.Size = new System.Drawing.Size(236, 38);
            this.GoToResultbtn.TabIndex = 20;
            this.GoToResultbtn.Tag = "noTheme";
            this.GoToResultbtn.Text = "훈련 결과 페이지로 가기";
            this.GoToResultbtn.UseVisualStyleBackColor = false;
            // 
            // TrainFigurelbl
            // 
            this.TrainFigurelbl.AutoSize = true;
            this.TrainFigurelbl.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrainFigurelbl.ForeColor = System.Drawing.Color.Black;
            this.TrainFigurelbl.Location = new System.Drawing.Point(397, 7);
            this.TrainFigurelbl.Name = "TrainFigurelbl";
            this.TrainFigurelbl.Size = new System.Drawing.Size(184, 31);
            this.TrainFigurelbl.TabIndex = 19;
            this.TrainFigurelbl.Text = "훈련 진행률 : 0%";
            // 
            // TrainWronglbl
            // 
            this.TrainWronglbl.AutoSize = true;
            this.TrainWronglbl.Font = new System.Drawing.Font("한컴 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrainWronglbl.ForeColor = System.Drawing.Color.Black;
            this.TrainWronglbl.Location = new System.Drawing.Point(800, 7);
            this.TrainWronglbl.Name = "TrainWronglbl";
            this.TrainWronglbl.Size = new System.Drawing.Size(184, 31);
            this.TrainWronglbl.TabIndex = 18;
            this.TrainWronglbl.Text = "훈련 오답률 : 0%";
            this.TrainWronglbl.Click += new System.EventHandler(this.TrainWronglbl_Click);
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
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1442, 1030);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form2";
            this.Text = "TrainingPart V1.0";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button GoDatabtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox SortMethodcom;
        private System.Windows.Forms.Button TrainingStartbtn;
        private System.Windows.Forms.Label Massagelbl;
        private System.Windows.Forms.Label SortMethodlbl;
        private System.Windows.Forms.ListBox Traninglst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TrainFigurelbl;
        private System.Windows.Forms.Label TrainWronglbl;
        private System.Windows.Forms.Button GoToResultbtn;
    }
}