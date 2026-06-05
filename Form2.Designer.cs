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
            this.GoDatabtn = new System.Windows.Forms.Button();
            this.radioOverwrite = new System.Windows.Forms.RadioButton();
            this.radioNew = new System.Windows.Forms.RadioButton();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1843, 1341);
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
            this.panel2.Location = new System.Drawing.Point(4, 68);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1835, 96);
            this.panel2.TabIndex = 7;
            // 
            // SortMethodlbl
            // 
            this.SortMethodlbl.AutoSize = true;
            this.SortMethodlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SortMethodlbl.Location = new System.Drawing.Point(17, 15);
            this.SortMethodlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SortMethodlbl.Name = "SortMethodlbl";
            this.SortMethodlbl.Size = new System.Drawing.Size(286, 55);
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
            this.SortMethodcom.Location = new System.Drawing.Point(342, 21);
            this.SortMethodcom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SortMethodcom.Name = "SortMethodcom";
            this.SortMethodcom.Size = new System.Drawing.Size(498, 50);
            this.SortMethodcom.TabIndex = 9;
            // 
            // TrainingStartbtn
            // 
            this.TrainingStartbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrainingStartbtn.BackColor = System.Drawing.Color.DarkSalmon;
            this.TrainingStartbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrainingStartbtn.Location = new System.Drawing.Point(1018, 3);
            this.TrainingStartbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TrainingStartbtn.Name = "TrainingStartbtn";
            this.TrainingStartbtn.Size = new System.Drawing.Size(811, 92);
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
            this.Traninglst.ItemHeight = 24;
            this.Traninglst.Location = new System.Drawing.Point(4, 172);
            this.Traninglst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Traninglst.Name = "Traninglst";
            this.Traninglst.Size = new System.Drawing.Size(1835, 1112);
            this.Traninglst.TabIndex = 8;
            this.Traninglst.SelectedIndexChanged += new System.EventHandler(this.Traninglst_SelectedIndexChanged);
            // 
            // Massagelbl
            // 
            this.Massagelbl.AutoSize = true;
            this.Massagelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Massagelbl.Location = new System.Drawing.Point(4, 1288);
            this.Massagelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Massagelbl.Name = "Massagelbl";
            this.Massagelbl.Size = new System.Drawing.Size(127, 31);
            this.Massagelbl.TabIndex = 9;
            this.Massagelbl.Text = "경고 텍스트";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.radioNew);
            this.panel1.Controls.Add(this.radioOverwrite);
            this.panel1.Controls.Add(this.GoToResultbtn);
            this.panel1.Controls.Add(this.GoDatabtn);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1835, 56);
            this.panel1.TabIndex = 10;
            // 
            // GoToResultbtn
            // 
            this.GoToResultbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoToResultbtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoToResultbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoToResultbtn.Location = new System.Drawing.Point(1523, 5);
            this.GoToResultbtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GoToResultbtn.Name = "GoToResultbtn";
            this.GoToResultbtn.Size = new System.Drawing.Size(307, 51);
            this.GoToResultbtn.TabIndex = 20;
            this.GoToResultbtn.Tag = "noTheme";
            this.GoToResultbtn.Text = "훈련 결과 페이지로 가기";
            this.GoToResultbtn.UseVisualStyleBackColor = false;
            this.GoToResultbtn.Click += new System.EventHandler(this.GoToResultbtn_Click);
            // 
            // GoDatabtn
            // 
            this.GoDatabtn.BackColor = System.Drawing.Color.PowderBlue;
            this.GoDatabtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GoDatabtn.Location = new System.Drawing.Point(6, 5);
            this.GoDatabtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GoDatabtn.Name = "GoDatabtn";
            this.GoDatabtn.Size = new System.Drawing.Size(287, 51);
            this.GoDatabtn.TabIndex = 3;
            this.GoDatabtn.Tag = "noTheme";
            this.GoDatabtn.Text = "데이터 페이지로 가기";
            this.GoDatabtn.UseVisualStyleBackColor = false;
            this.GoDatabtn.Click += new System.EventHandler(this.GoDatabtn_Click);
            // 
            // radioOverwrite
            // 
            this.radioOverwrite.AutoSize = true;
            this.radioOverwrite.Location = new System.Drawing.Point(602, 18);
            this.radioOverwrite.Name = "radioOverwrite";
            this.radioOverwrite.Size = new System.Drawing.Size(371, 28);
            this.radioOverwrite.TabIndex = 21;
            this.radioOverwrite.TabStop = true;
            this.radioOverwrite.Text = "기존 모델 덮어쓰기(mypilot.h5)";
            this.radioOverwrite.UseVisualStyleBackColor = true;
            // 
            // radioNew
            // 
            this.radioNew.AutoSize = true;
            this.radioNew.Location = new System.Drawing.Point(1045, 18);
            this.radioNew.Name = "radioNew";
            this.radioNew.Size = new System.Drawing.Size(361, 28);
            this.radioNew.TabIndex = 22;
            this.radioNew.TabStop = true;
            this.radioNew.Text = "날짜 별로 새로 모델 생성하기";
            this.radioNew.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1875, 1373);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Button GoToResultbtn;
        private System.Windows.Forms.RadioButton radioNew;
        private System.Windows.Forms.RadioButton radioOverwrite;
    }
}