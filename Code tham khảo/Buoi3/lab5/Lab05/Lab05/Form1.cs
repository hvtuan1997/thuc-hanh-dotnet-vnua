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

namespace Lab05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // chuoi ket noi
        string strCon = @"Data Source=GVND203;Initial Catalog=QLSV;Integrated Security=True";

        // doi tuong ket noi
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

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblSinhVien";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            while (reader.Read())
            {
                // doc du lieu tu csdl
                string masv = reader.GetString(0);
                string tensv=reader.GetString(1);
                string gioiTinh=reader.GetString(2);
                string ngaySinh = reader.GetDateTime(3).ToString("MM/dd/yyyy");
                string queQuan=reader.GetString(4);
                string maLop=reader.GetString(5);

                // tao moi 1 dong du lieu tren listview
                ListViewItem lvi = new ListViewItem(masv);
                lvi.SubItems.Add(tensv);
                lvi.SubItems.Add(gioiTinh);
                lvi.SubItems.Add(ngaySinh);
                lvi.SubItems.Add(queQuan);
                lvi.SubItems.Add(maLop);

                // gan dong moi len listview
                lsvDanhSach.Items.Add(lvi);
            }
            reader.Close();
        }

        // ham tim kiem theo ma
        private void TimKiemTheoMa(string masv)
        {
            MoKetNoi();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblSinhVien where MaSV='"+masv+"'";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            if (reader.Read())
            {
                // doc du lieu tu csdl
                string _masv = reader.GetString(0);
                string tensv = reader.GetString(1);
                string gioiTinh = reader.GetString(2);
                string ngaySinh = reader.GetDateTime(3).ToString("MM/dd/yyyy");
                string queQuan = reader.GetString(4);
                string maLop = reader.GetString(5);

                // tao moi 1 dong du lieu tren listview
                ListViewItem lvi = new ListViewItem(_masv);
                lvi.SubItems.Add(tensv);
                lvi.SubItems.Add(gioiTinh);
                lvi.SubItems.Add(ngaySinh);
                lvi.SubItems.Add(queQuan);
                lvi.SubItems.Add(maLop);

                // gan dong moi len listview
                lsvDanhSach.Items.Add(lvi);
            }
            reader.Close();
        }

        // ham tim kiem theo ten
        private void TimKiemTheoTen(string ten)
        {
            MoKetNoi();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * from tblSinhVien where TenSV like '%"+ten+"%'";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            while (reader.Read())
            {
                // doc du lieu tu csdl
                string masv = reader.GetString(0);
                string tensv = reader.GetString(1);
                string gioiTinh = reader.GetString(2);
                string ngaySinh = reader.GetDateTime(3).ToString("MM/dd/yyyy");
                string queQuan = reader.GetString(4);
                string maLop = reader.GetString(5);

                // tao moi 1 dong du lieu tren listview
                ListViewItem lvi = new ListViewItem(masv);
                lvi.SubItems.Add(tensv);
                lvi.SubItems.Add(gioiTinh);
                lvi.SubItems.Add(ngaySinh);
                lvi.SubItems.Add(queQuan);
                lvi.SubItems.Add(maLop);

                // gan dong moi len listview
                lsvDanhSach.Items.Add(lvi);
            }
            reader.Close();
        }

        // ham them sinh vien
        private void ThemSinhVien()
        {
            MoKetNoi();

            // lay du lieu tu form
            string masv = txtMa.Text.Trim();
            string tensv = txtTen.Text.Trim();
            string gioiTinh = "";
            if (cbGioiTinh.SelectedIndex == 0) gioiTinh = "Nam";
            else gioiTinh = "Nữ";
            string ngaySinh = dtpNgaySinh.Value.Year + "-" + dtpNgaySinh.Value.Month + "-" + dtpNgaySinh.Value.Day;
            string queQuan=txtQueQuan.Text.Trim();
            string maLop=txtMaLop.Text.Trim();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into tblSinhVien values('" + masv + "', N'" + tensv + "', N'" + gioiTinh + "', CONVERT(datetime, '" + ngaySinh + "'), N'" + queQuan + "', '" + maLop + "')";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            int kq=sqlCmd.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Bạn thêm sinh viên thành công!");
                HienThiDanhSachSV();

                XoaDL();

                gbTTCT.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn thêm sinh viên thất bại!");
                XoaDL();
                gbTTCT.Enabled = false;
            }
        }

        // ham xoa du lieu tren form
        private void XoaDL()
        {
            txtMa.Text = "";
            txtTen.Clear();
            txtQueQuan.Clear();
            txtMaLop.Clear();
        }

        // chinh sua thong tin sinh vien
        private void ChinhSuaTTSinhVien()
        {
            MoKetNoi();

            // lay du lieu tu form
            string masv = txtMa.Text.Trim();
            string tensv = txtTen.Text.Trim();
            string gioiTinh = "";
            if (cbGioiTinh.SelectedIndex == 0) gioiTinh = "Nam";
            else gioiTinh = "Nữ";
            string ngaySinh = dtpNgaySinh.Value.Year + "-" + dtpNgaySinh.Value.Month + "-" + dtpNgaySinh.Value.Day;
            string queQuan = txtQueQuan.Text.Trim();
            string maLop = txtMaLop.Text.Trim();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update tblSinhVien set MaSV='" + masv + "',TenSV=N'" + tensv + "',GioiTinh=N'" + gioiTinh + "',NgaySinh=CONVERT(datetime, '" + ngaySinh + "'),QueQuan=N'" + queQuan + "',MaLop='" + maLop + "' where MaSV='" + masv + "'";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            int kq = sqlCmd.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Bạn chỉnh sửa thông tin sinh viên thành công!");
                HienThiDanhSachSV();

                XoaDL();

                gbTTCT.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn chỉnh sửa thông tin sinh viên thất bại!");
                XoaDL();
                gbTTCT.Enabled = false;
            }
            btnSua.Enabled = false;
        }

        // ham xoa sinh vien
        private void XoaSinhVien()
        {
            MoKetNoi();

            string maSV_xoa = lsvDanhSach.SelectedItems[0].SubItems[0].Text.Trim();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from tblSinhVien where MaSV='" + maSV_xoa + "'";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            int kq = sqlCmd.ExecuteNonQuery();

            if (kq > 0)
            {
                MessageBox.Show("Bạn xóa sinh viên thành công!");
                HienThiDanhSachSV();

                XoaDL();

                gbTTCT.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn xóa sinh viên thất bại!");
                XoaDL();
                gbTTCT.Enabled = false;
            }
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDanhSachSV();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            gbTTCT.Enabled = false;

            // nap du lieu cho combobox
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn thoát hay không?", "Hộp thoại", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // lay du lieu tu form
            string masv = txtTkMa.Text.Trim();
            string tensv = txtTkTen.Text.Trim();

            // phan chia truong hop
            if (masv != "" && tensv == "")
            {
                TimKiemTheoMa(masv);
            }
            else if (masv == "" && tensv != "")
            {
                TimKiemTheoTen(tensv);
            }
            else if (masv != "" && tensv != "")
            {
                TimKiemTheoMa(masv);
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu tìm kiếm!");
                txtTkMa.Focus();
            }
        }

        int chucNang = 0;

        private void btnThem_Click(object sender, EventArgs e)
        {
            gbTTCT.Enabled = true;
            chucNang = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            chucNang = 2;
            gbTTCT.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (chucNang == 1)
            {
                ThemSinhVien();
            }
            else if(chucNang == 2) 
            {
                ChinhSuaTTSinhVien();
            }
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0) return;

            ListViewItem lvi = lsvDanhSach.SelectedItems[0];
            txtMa.Text = lvi.SubItems[0].Text.Trim();
            txtTen.Text = lvi.SubItems[1].Text.Trim();
            if (lvi.SubItems[2].Text.Trim()=="Nam") cbGioiTinh.SelectedIndex= 0;
            else cbGioiTinh.SelectedIndex= 1;
            string[] ns = lvi.SubItems[3].Text.Split('/');
            dtpNgaySinh.Value = new DateTime(int.Parse(ns[2]), int.Parse(ns[0]), int.Parse(ns[1]));
            txtQueQuan.Text = lvi.SubItems[4].Text.Trim();
            txtMaLop.Text = lvi.SubItems[5].Text.Trim();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0) return;

            DialogResult result = MessageBox.Show(
                "Bạn có thực sự muốn xóa hay không?",
                "Hộp thoại",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                XoaSinhVien();
            }
        }
    }
}
