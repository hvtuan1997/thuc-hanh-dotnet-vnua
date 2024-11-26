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

namespace TruyvanNhieuDongDL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // chuoi ket noi
        string strCon = @"Data Source=GVPM06;Initial Catalog=QLSV1;Integrated Security=True";

        // doi tuong truy van
        SqlConnection sqlCon = null;

        // ham mo ket noi
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // ham hien thi danh sach sinh vien
        private void HienThiDanhSachSV()
        {
            MoKetNoi();

            // doi tuong thuc thi truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from SinhVien";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            while (reader.Read())
            {
                // doc du lieu tu CSDL
                string maSV = reader.GetString(0).Trim();
                string hoTen = reader.GetString(1).Trim();
                string gioiTinh = reader.GetString(2).Trim();
                string ngaySinh = reader.GetDateTime(3).ToString("MM/dd/yyyy");
                string queQuan = reader.GetString(4).Trim();
                string maLop = reader.GetString(5).Trim();

                // tao 1 dong du lieu moi
                ListViewItem lvi = new ListViewItem(maSV);
                lvi.SubItems.Add(hoTen);
                lvi.SubItems.Add(gioiTinh);
                lvi.SubItems.Add(ngaySinh);
                lvi.SubItems.Add(queQuan);
                lvi.SubItems.Add(maLop);

                // gan dong du lieu moi vao listview
                lsvDanhSach.Items.Add(lvi);
            }
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDanhSachSV();
        }
    }
}
