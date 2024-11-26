using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThucHanh1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCong_Click(object sender, EventArgs e)
        {
            // lay du lieu tu form
            double a = Convert.ToDouble(txtNhapA.Text.Trim());
            double b = Convert.ToDouble(txtNhapB.Text.Trim());

            // tinh tong
            double c = a + b;

            // hien thi ket qua
            txtKetQua.Text = c.ToString();
        }

        private void btnChia_Click(object sender, EventArgs e)
        {
            // lay du lieu tu form
            double a = Convert.ToDouble(txtNhapA.Text.Trim());
            double b = Convert.ToDouble(txtNhapB.Text.Trim());

            // kiem tra b = 0
            if (b == 0)
            {
                MessageBox.Show("Ban nhap sai roi. Nhap lai di!");
                txtNhapB.Focus();
                return;
            }

            // tinh chia
            double c = a / b;

            // hien thi ket qua
            txtKetQua.Text = c.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtNhapA.Clear();
            txtNhapB.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Ban co thuc su muon thoat hay khong?",
                "Hop thoai", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
