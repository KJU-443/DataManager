using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace DataManager
{
    public partial class Form3 : Form
    {
        private string[] imageFiles;
        private List<string> deletedFiles;
        private Form1 parentForm;
        private List<Panel> selectedCards = new List<Panel>();

        private bool isAllSelected = false;

        private Dictionary<Control, Color> originalBackColors = new Dictionary<Control, Color>();
        private Dictionary<Control, Color> originalForeColors = new Dictionary<Control, Color>();
        private bool colorssaved = false;

        private string dataPath = "";

        public Form3(string[] images, List<string> deleted, Form1 parent, string data)
        {
            InitializeComponent();
            imageFiles = images;
            deletedFiles = deleted;
            parentForm = parent;
            dataPath = data;

            // ImageRestorebtn.Click += new EventHandler(ImageRestorebtn_Click);
            // SellectAllbtn.Click += new EventHandler(SellectAllbtn_Click);

            LoadDeletedImages();

            if (Form1.isDarkMode) ApplyTheme(this);
        }

        // 표시용
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D))
            {
                Form1.isDarkMode = !Form1.isDarkMode;
                ApplyTheme(this);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApplyTheme(Form form)
        {
            if (!colorssaved)
            {
                SaveOriginalColors(form.Controls);
                colorssaved = true;
            }
            if (Form1.isDarkMode)
            {
                form.BackColor = Color.FromArgb(30, 30, 30);
                ApplyThemeToControls(form.Controls,
                    Color.FromArgb(30, 30, 30), Color.White, Color.FromArgb(60, 60, 60));
            }
            else
            {
                form.BackColor = SystemColors.Control;
                RestoreOriginalColors(form.Controls);
            }
        }

        private void SaveOriginalColors(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                originalBackColors[ctrl] = ctrl.BackColor;
                originalForeColors[ctrl] = ctrl.ForeColor;
                if (ctrl.Controls.Count > 0)
                    SaveOriginalColors(ctrl.Controls);
            }
        }

        private void RestoreOriginalColors(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (originalBackColors.ContainsKey(ctrl))
                    ctrl.BackColor = originalBackColors[ctrl];
                if (originalForeColors.ContainsKey(ctrl))
                    ctrl.ForeColor = originalForeColors[ctrl];
                if (ctrl.Controls.Count > 0)
                    RestoreOriginalColors(ctrl.Controls);
            }
        }

        private void ApplyThemeToControls(Control.ControlCollection controls,
            Color backColor, Color foreColor, Color buttonBack)
        {
            foreach (Control ctrl in controls)
            {
                // Tag가 "noTheme"이면 건드리지 않음
                if (ctrl.Tag?.ToString() == "noTheme")
                {
                    if (ctrl.Controls.Count > 0)
                        ApplyThemeToControls(ctrl.Controls, backColor, foreColor, buttonBack);
                    continue;
                }

                ctrl.ForeColor = foreColor;
                if (ctrl is Button)
                    ctrl.BackColor = buttonBack;
                else if (ctrl is PictureBox)
                { }
                else
                    ctrl.BackColor = backColor;

                if (ctrl.Controls.Count > 0)
                    ApplyThemeToControls(ctrl.Controls, backColor, foreColor, buttonBack);
            }
        }
        // 표시용


        private void LoadDeletedImages()
        {
            flowLayoutPanel1.Controls.Clear();
            selectedCards.Clear();

            string dataPath = parentForm.Imgtxt.Text;
            string trashPath = Path.Combine(dataPath, "image_trash");

            if (!Directory.Exists(trashPath) || Directory.GetFiles(trashPath, "*.jpg").Length == 0)
            {
                Massagelbl.Text = "복구할 이미지가 없어요.";
                Massagelbl.ForeColor = Color.Gray;
                return;
            }

            string[] trashFiles = Directory.GetFiles(trashPath, "*.jpg").OrderBy(f => f).ToArray();

            foreach (string imgPath in trashFiles)
            {
                flowLayoutPanel1.Controls.Add(CreateCard(imgPath));
            }

            Massagelbl.Text = "복구할 이미지를 선택하세요.";
            Massagelbl.ForeColor = Color.Blue;
        }

     
        private void GoDatabtn_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            if (Form1.isDarkMode) parentForm.ApplyThemePublic();
            this.Close();
        }


        private Panel CreateCard(string imagePath)
        {
            Panel card = new Panel
            {
                Width = 160,
                Height = 200,
                Margin = new Padding(5),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = imagePath
            };

            PictureBox pic = new PictureBox
            {
                Width = 150,
                Height = 150,
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.LightGray,
                Tag = imagePath
            };

            if (File.Exists(imagePath))
            {
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image tempImg = System.Drawing.Image.FromStream(fs))
                    pic.Image = new Bitmap(tempImg);
            }

            Label lbl = new Label
            {
                Text = Path.GetFileName(imagePath),
                Location = new Point(5, 158),
                Width = 140,
                Height = 35,
                Font = new Font("Arial", 8f),
                TextAlign = ContentAlignment.MiddleCenter
            };

            EventHandler selectHandler = (s, e) => ToggleSelect(card);
            card.Click += selectHandler;
            pic.Click += selectHandler;
            lbl.Click += selectHandler;

            card.Controls.Add(pic);
            card.Controls.Add(lbl);
            return card;
        }

        private void ToggleSelect(Panel card)
        {
            if (selectedCards.Contains(card))
            {
                selectedCards.Remove(card);
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;

                // 체크 아이콘 제거
                var checkLabel = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "checkLbl");
                if (checkLabel != null) card.Controls.Remove(checkLabel);
            }
            else
            {
                selectedCards.Add(card);
                card.BackColor = Color.FromArgb(200, 230, 255);
                card.BorderStyle = BorderStyle.FixedSingle;

                // 체크 아이콘 추가
                Label checkLbl = new Label
                {
                    Name = "checkLbl",
                    Text = "✔",
                    Font = new Font("Arial", 16f, FontStyle.Bold),
                    ForeColor = Color.RoyalBlue,
                    BackColor = Color.Transparent,
                    Location = new Point(120, 5),
                    Size = new Size(35, 35),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                card.Controls.Add(checkLbl);
                checkLbl.BringToFront();
                checkLbl.Click += (s, e) => ToggleSelect(card);
            }
        }

        private void SellectAllbtn_Click(object sender, EventArgs e)
        {
            if (isAllSelected)
            {
                // 전체 선택 해제
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c is Panel card)
                    {
                        card.BackColor = Color.White;
                        card.BorderStyle = BorderStyle.FixedSingle;
                        var checkLabel = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "checkLbl");
                        if (checkLabel != null) card.Controls.Remove(checkLabel);
                    }
                }
                selectedCards.Clear();
                isAllSelected = false;
            }
            else
            {
                // 기존 선택 초기화 후 전체 선택
                selectedCards.Clear();
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c is Panel card)
                    {
                        selectedCards.Add(card);
                        card.BackColor = Color.FromArgb(200, 230, 255);
                        card.BorderStyle = BorderStyle.FixedSingle;

                        // 이미 체크 아이콘 있으면 추가 안 함
                        if (card.Controls.OfType<Label>().All(l => l.Name != "checkLbl"))
                        {
                            Label checkLbl = new Label
                            {
                                Name = "checkLbl",
                                Text = "✔",
                                Font = new Font("Arial", 16f, FontStyle.Bold),
                                ForeColor = Color.RoyalBlue,
                                BackColor = Color.Transparent,
                                Location = new Point(120, 5),
                                Size = new Size(35, 35),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            card.Controls.Add(checkLbl);
                            checkLbl.BringToFront();
                            checkLbl.Click += (s, ev) => ToggleSelect(card);
                        }
                    }
                }
                isAllSelected = true;
            }

        }

        private void ImageRestorebtn_Click(object sender, EventArgs e)
        {
            if (selectedCards.Count == 0)
            {
                Massagelbl.Text = "복구할 이미지를 먼저 선택해주세요.";
                Massagelbl.ForeColor = Color.OrangeRed;
                return;
            }

            foreach (Panel card in selectedCards.ToList())
            {
                string imgPath = card.Tag?.ToString();
                if (string.IsNullOrEmpty(imgPath)) continue;

                string fileName = Path.GetFileName(imgPath); // 여기서 선언
                parentForm.RestoreImage(fileName);
                flowLayoutPanel1.Controls.Remove(card);
            }

            selectedCards.Clear();
            isAllSelected = false;
            Massagelbl.Text = "복구 완료!";
            Massagelbl.ForeColor = Color.Green;
        }

        private void TypeChoicelbl_Click(object sender, EventArgs e)
        {

        }

        private void Massagelbl_Click(object sender, EventArgs e)
        {

        }

        public void ApplyWindowState(Form previousForm)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = previousForm.Location;
            this.Size = previousForm.Size;
            this.WindowState = previousForm.WindowState;
        }

        

    }
}
