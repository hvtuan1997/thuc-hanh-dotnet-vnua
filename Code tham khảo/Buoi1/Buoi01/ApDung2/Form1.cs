using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApDung2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRing_Click(object sender, EventArgs e)
        {
            MessageBox.Show("báo động khẩn cấp");
        }

        string pass = "";

        private void btn1_Click(object sender, EventArgs e)
        {
            pass += "1";
            txtPass.Text = pass;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            pass += "2";
            txtPass.Text = pass;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            pass += "3";
            txtPass.Text = pass;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            pass += "4";
            txtPass.Text = pass;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            pass += "5";
            txtPass.Text = pass;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            pass += "6";
            txtPass.Text = pass;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            pass += "7";
            txtPass.Text = pass;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            pass += "8";
            txtPass.Text = pass;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            pass += "9";
            txtPass.Text = pass;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            pass = "";
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string tenNhom = "";
            string ketQua = "";
            if (pass == "1496" || pass == "2673")
            {
                tenNhom = "Phát triển công nghệ";
                ketQua = "Chấp nhận";
            }
            else if (pass == "7462")
            {
                tenNhom = "Nghiên cứu viên";
                ketQua = "Chấp nhận";
            }
            else if (pass == "8884" || pass == "3842" || pass == "3383")
            {
                tenNhom = "Thiết kế mô hình";
                ketQua = "Chấp nhận";
            }
            else
            {
                tenNhom = "Không có";
                ketQua = "Từ chối";
            }

            // lay ngay thang hien tai
            string ngayGio = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // tao 1 dong du lieu cua listview
            ListViewItem lvi=new ListViewItem(ngayGio);
            lvi.SubItems.Add(tenNhom);
            lvi.SubItems.Add(ketQua);

            // gan dong du lieu moi vao listview
            lsvDanhSach.Items.Add(lvi);

            pass = "";
            txtPass.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lsvDanhSach.Items.Clear();
        }
    }
}
