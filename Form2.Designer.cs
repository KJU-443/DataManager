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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SortMethodlbl = new System.Windows.Forms.Label();
            this.SortMethodcom = new System.Windows.Forms.ComboBox();
            this.TrainingStartbtn = new System.Windows.Forms.Button();
            this.Massagelbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioNew = new System.Windows.Forms.RadioButton();
            this.radioOverwrite = new System.Windows.Forms.RadioButton();
            this.GoToResultbtn = new System.Windows.Forms.Button();
            this.GoDatabtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Traninglst = new System.Windows.Forms.ListBox();
            this.lossGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TrainingProgresslbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lastlosslbl = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lossGraph)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.Massagelbl, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
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
            this.TrainingStartbtn.Size = new System.Drawing.Size(623, 69);
            this.TrainingStartbtn.TabIndex = 7;
            this.TrainingStartbtn.Tag = "noTheme";
            this.TrainingStartbtn.Text = "훈련 시작";
            this.TrainingStartbtn.UseVisualStyleBackColor = false;
            this.TrainingStartbtn.Click += new System.EventHandler(this.TrainingStartbtn_Click);
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
            this.panel1.Controls.Add(this.radioNew);
            this.panel1.Controls.Add(this.radioOverwrite);
            this.panel1.Controls.Add(this.GoToResultbtn);
            this.panel1.Controls.Add(this.GoDatabtn);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1412, 42);
            this.panel1.TabIndex = 10;
            // 
            // radioNew
            // 
            this.radioNew.AutoSize = true;
            this.radioNew.Location = new System.Drawing.Point(804, 14);
            this.radioNew.Margin = new System.Windows.Forms.Padding(2);
            this.radioNew.Name = "radioNew";
            this.radioNew.Size = new System.Drawing.Size(273, 22);
            this.radioNew.TabIndex = 22;
            this.radioNew.TabStop = true;
            this.radioNew.Text = "날짜 별로 새로 모델 생성하기";
            this.radioNew.UseVisualStyleBackColor = true;
            // 
            // radioOverwrite
            // 
            this.radioOverwrite.AutoSize = true;
            this.radioOverwrite.Location = new System.Drawing.Point(463, 14);
            this.radioOverwrite.Margin = new System.Windows.Forms.Padding(2);
            this.radioOverwrite.Name = "radioOverwrite";
            this.radioOverwrite.Size = new System.Drawing.Size(284, 22);
            this.radioOverwrite.TabIndex = 21;
            this.radioOverwrite.TabStop = true;
            this.radioOverwrite.Text = "기존 모델 덮어쓰기(mypilot.h5)";
            this.radioOverwrite.UseVisualStyleBackColor = true;
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
            this.GoToResultbtn.Click += new System.EventHandler(this.GoToResultbtn_Click);
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
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 129);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1412, 834);
            this.panel3.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.Traninglst, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1412, 834);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // Traninglst
            // 
            this.Traninglst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Traninglst.FormattingEnabled = true;
            this.Traninglst.ItemHeight = 18;
            this.Traninglst.Location = new System.Drawing.Point(3, 3);
            this.Traninglst.Name = "Traninglst";
            this.Traninglst.Size = new System.Drawing.Size(700, 828);
            this.Traninglst.TabIndex = 0;
            // 
            // lossGraph
            // 
            chartArea4.Name = "ChartArea1";
            this.lossGraph.ChartAreas.Add(chartArea4);
            this.lossGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.lossGraph.Legends.Add(legend4);
            this.lossGraph.Location = new System.Drawing.Point(0, 0);
            this.lossGraph.Name = "lossGraph";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.lossGraph.Series.Add(series4);
            this.lossGraph.Size = new System.Drawing.Size(694, 466);
            this.lossGraph.TabIndex = 11;
            this.lossGraph.Text = "chart1";
            this.lossGraph.Click += new System.EventHandler(this.lossGraph_Click);
            // 
            // TrainingProgresslbl
            // 
            this.TrainingProgresslbl.AutoSize = true;
            this.TrainingProgresslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TrainingProgresslbl.Location = new System.Drawing.Point(3, 49);
            this.TrainingProgresslbl.Name = "TrainingProgresslbl";
            this.TrainingProgresslbl.Size = new System.Drawing.Size(249, 40);
            this.TrainingProgresslbl.TabIndex = 12;
            this.TrainingProgresslbl.Text = "훈련 진행률: 0%";
            this.TrainingProgresslbl.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(3, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 40);
            this.label1.TabIndex = 11;
            this.label1.Text = "훈련 오답률";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lastlosslbl
            // 
            this.lastlosslbl.AutoSize = true;
            this.lastlosslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lastlosslbl.Location = new System.Drawing.Point(310, 141);
            this.lastlosslbl.Name = "lastlosslbl";
            this.lastlosslbl.Size = new System.Drawing.Size(206, 40);
            this.lastlosslbl.TabIndex = 14;
            this.lastlosslbl.Text = "최종 오답률 : ";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(709, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.60736F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.39264F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(700, 828);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TrainingProgresslbl);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.lastlosslbl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(694, 174);
            this.panel4.TabIndex = 12;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lossGraph);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 183);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(694, 466);
            this.panel5.TabIndex = 13;
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
            this.Load += new System.EventHandler(this.Form2_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lossGraph)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button GoToResultbtn;
        private System.Windows.Forms.RadioButton radioNew;
        private System.Windows.Forms.RadioButton radioOverwrite;
        private System.Windows.Forms.DataVisualization.Charting.Chart lossGraph;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Traninglst;
        private System.Windows.Forms.Label TrainingProgresslbl;
        private System.Windows.Forms.Label lastlosslbl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panel5;
    }
}