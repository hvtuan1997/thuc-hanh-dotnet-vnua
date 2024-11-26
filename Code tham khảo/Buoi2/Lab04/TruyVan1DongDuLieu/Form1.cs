using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace TruyVan1DongDuLieu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // chuoi ket noi
        string strCon = @"Data Source=GVPM06;Initial Catalog=QLSV1;Integrated Security=True";

        // doi tuong ket noi
        SqlConnection sqlCon = null;

        // ham mo ket noi
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if(sqlCon.State==ConnectionState.Closed) sqlCon.Open();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            MoKetNoi();

            // doi tuong thuc thi truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from SinhVien where MaSV = '" + txtNhapMa.Text.Trim() + "'";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            if (reader.Read())
            {
                // doc du lieu tu CSDL
                string ten = reader.GetString(1);
                string gioiTinh=reader.GetString(2);
                string ngaySinh = reader.GetDateTime(3).ToString("MM/dd/yyyy");
                string queQuan=reader.GetString(4);
                string maLop = reader.GetString(5);

                // gan du lieu len giao dien
                txtHoten.Text = ten;
                txtGioiTinh.Text = gioiTinh;
                txtNgaySinh.Text = ngaySinh;
                txtQueQuan.Text = queQuan;
                txtMaLop.Text = maLop;
            }
            reader.Close();
        }
    }
}
