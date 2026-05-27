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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void TrainingStartbtn_Click(object sender, EventArgs e)
        {

        }

        private void GoDatabtn_Click(object sender, EventArgs e)
        {
            // 1. 새 창(Form3) 생성
            Form3 form3 = new Form3();

            // 3. Form3 열기 (이제 창 크기 마구 늘렸다 줄였다 테스트 가능!)
            form3.Show();

            // 4. 현재 창은 잠시 숨기기
            this.Hide();
        }
    }
}
