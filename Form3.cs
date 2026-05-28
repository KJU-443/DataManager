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

        public Form3(string[] images, List<string> deleted, Form1 parent)
        {
            InitializeComponent();
            imageFiles = images;
            deletedFiles = deleted;
            parentForm = parent;

            // ImageRestorebtn.Click += new EventHandler(ImageRestorebtn_Click);
            // SellectAllbtn.Click += new EventHandler(SellectAllbtn_Click);

            LoadDeletedImages();
        }

        private void LoadDeletedImages()
        {
            flowLayoutPanel1.Controls.Clear();
            selectedCards.Clear();

            if (deletedFiles.Count == 0)
            {
                Massagelbl.Text = "복구할 이미지가 없어요.";
                Massagelbl.ForeColor = Color.Gray;
                return;
            }

            string imagesFolder = imageFiles.Length > 0
                ? Path.GetDirectoryName(imageFiles[0])
                : "";

            foreach (string fileName in deletedFiles)
            {
                string fullPath = Path.Combine(imagesFolder, fileName);
                flowLayoutPanel1.Controls.Add(CreateCard(fullPath));
            }

            Massagelbl.Text = "복구할 이미지를 선택하세요.";
            Massagelbl.ForeColor = Color.Blue;
        }

     
        private void GoDatabtn_Click(object sender, EventArgs e)
        {
            parentForm.Show();
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
                string fileName = Path.GetFileName(imgPath);

                deletedFiles.Remove(fileName);
                parentForm.RestoreImage(imgPath); // Form1에 직접 반영

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
    }
}
