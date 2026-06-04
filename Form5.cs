using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataManager
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        
        public class DoNotShowDialog : Form
        {
            public bool DoNotShowAgain => chkDoNotShow.Checked;

            private Label lblMessage;
            private CheckBox chkDoNotShow;
            private Button btnYes;
            private Button btnNo;

            public DoNotShowDialog(string message)
            {
                this.Text = "데이터 삭제";
                this.Size = new Size(380, 200);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                lblMessage = new Label()
                {
                    Text = message,
                    Location = new Point(20, 20),
                    Size = new Size(330, 60),
                    AutoSize = false
                };

                chkDoNotShow = new CheckBox()
                {
                    Text = "다시 묻지 않음",
                    Location = new Point(20, 95),
                    AutoSize = true
                };

                btnYes = new Button()
                {
                    Text = "예",
                    Location = new Point(190, 120),
                    Size = new Size(75, 30),
                    DialogResult = DialogResult.OK
                };

                btnNo = new Button()
                {
                    Text = "아니오",
                    Location = new Point(275, 120),
                    Size = new Size(75, 30),
                    DialogResult = DialogResult.Cancel
                };

                this.Controls.AddRange(new Control[] { lblMessage, chkDoNotShow, btnYes, btnNo });
                this.AcceptButton = btnYes;
                this.CancelButton = btnNo;
            }
        }


    }
}
