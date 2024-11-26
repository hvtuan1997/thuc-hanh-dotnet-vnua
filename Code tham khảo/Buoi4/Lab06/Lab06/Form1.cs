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

namespace Lab06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có thực sự muốn thoát hay không?",
                "Hộp thoại",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) Application.Exit();
        }

        // chuoi ket noi
        string strCon = @"Data Source=GVPM06;Initial Catalog=QLSP;Integrated Security=True";

        // doi tuong ket noi
        SqlConnection sqlCon = null;

        // ham mo ket noi
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // ham hien thi danh sach san pham
        private void HienThiDsSanPham()
        {
            MoKetNoi();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "HienThiDsSanPham";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            while(reader.Read())
            {
                // doc du lieu tu csdl
                string masp=reader.GetString(0);
                string tensp = reader.GetString(1);
                string soLuong = reader.GetInt32(2).ToString();
                string canNang = reader.GetFloat(3).ToString();

                // tao moi 1 dong du lieu tren lsv
                ListViewItem lvi=new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(soLuong);
                lvi.SubItems.Add(canNang);

                // gan dong moi vao lsv
                lsvDanhSach.Items.Add(lvi);
            }
            reader.Close();
        }

        // ham tim kiem theo ma
        private void TimKiemTheoMa(string ma)
        {
            MoKetNoi();

            // doi tuong truy van
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "TimKiemTheoMa";

            // dinh nghia parameter
            SqlParameter parMa = new SqlParameter("@ma", SqlDbType.VarChar);
            parMa.Value = ma;
            sqlCmd.Parameters.Add(parMa);

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            if (reader.Read())
            {
                // doc du lieu tu csdl
                string masp = reader.GetString(0);
                string tensp = reader.GetString(1);
                string soLuong = reader.GetInt32(2).ToString();
                string canNang = reader.GetDouble(3).ToString();

                // tao moi 1 dong du lieu tren lsv
                ListViewItem lvi = new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(soLuong);
                lvi.SubItems.Add(canNang);

                // gan dong moi vao lsv
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
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "TimKiemTheoTen";

            // dinh nghia parameter
            SqlParameter parTen = new SqlParameter("@ten", SqlDbType.NVarChar);
            parTen.Value = ten;
            sqlCmd.Parameters.Add(parTen);

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            SqlDataReader reader = sqlCmd.ExecuteReader();
            lsvDanhSach.Items.Clear();
            while (reader.Read())
            {
                // doc du lieu tu csdl
                string masp = reader.GetString(0);
                string tensp = reader.GetString(1);
                string soLuong = reader.GetInt32(2).ToString();
                string canNang = reader.GetDouble(3).ToString();

                // tao moi 1 dong du lieu tren lsv
                ListViewItem lvi = new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(soLuong);
                lvi.SubItems.Add(canNang);

                // gan dong moi vao lsv
                lsvDanhSach.Items.Add(lvi);
            }
            reader.Close();
        }

        // ham them moi mot san pham
        private void ThemMoiSanPham()
        {
            try
            {
                // doc du lieu tu form
                string masp = txtMa.Text.Trim();
                string tensp = txtTen.Text.Trim();
                int soLuong = Convert.ToInt32(nmupSoLuong.Value);
                double canNang = Convert.ToDouble(txtCanNang.Text.Trim());

                MoKetNoi();

                // doi tuong truy van
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "ThemMoiSanPham";

                // dinh nghia cac parameter
                SqlParameter parMaSP = new SqlParameter("@masp", SqlDbType.VarChar);
                parMaSP.Value = masp;
                sqlCmd.Parameters.Add(parMaSP);

                SqlParameter parTenSP = new SqlParameter("@tensp", SqlDbType.NVarChar);
                parTenSP.Value = tensp;
                sqlCmd.Parameters.Add(parTenSP);

                SqlParameter parSoLuong = new SqlParameter("@soLuong", SqlDbType.Int);
                parSoLuong.Value = soLuong;
                sqlCmd.Parameters.Add(parSoLuong);

                SqlParameter parCanNang = new SqlParameter("@canNang", SqlDbType.Real);
                parCanNang.Value = canNang;
                sqlCmd.Parameters.Add(parCanNang);

                // gan vao ket noi
                sqlCmd.Connection = sqlCon;

                // thuc thi truy van
                int kq = sqlCmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Bạn thêm sản phẩm thành công!");
                    HienThiDsSanPham();
                    XoaDuLieu();
                    gbThongTinCT.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn thêm sản phẩm thất bại!");
                    XoaDuLieu();
                    gbThongTinCT.Enabled = false;
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Bạn bị lỗi như này: " + ex);
                gbThongTinCT.Enabled = false;
                XoaDuLieu();
            }
        }

        // ham chinh sua thong tin san pham
        private void ChinhSuaSanPham()
        {
            try
            {
                // doc du lieu tu form
                string masp = txtMa.Text.Trim();
                string tensp = txtTen.Text.Trim();
                int soLuong = Convert.ToInt32(nmupSoLuong.Value);
                double canNang = Convert.ToDouble(txtCanNang.Text.Trim());

                MoKetNoi();

                // doi tuong truy van
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "ChinhSuaTTSanPham";

                // dinh nghia cac parameter
                SqlParameter parMaSP = new SqlParameter("@masp", SqlDbType.VarChar);
                parMaSP.Value = masp;
                sqlCmd.Parameters.Add(parMaSP);

                SqlParameter parTenSP = new SqlParameter("@tensp", SqlDbType.NVarChar);
                parTenSP.Value = tensp;
                sqlCmd.Parameters.Add(parTenSP);

                SqlParameter parSoLuong = new SqlParameter("@soLuong", SqlDbType.Int);
                parSoLuong.Value = soLuong;
                sqlCmd.Parameters.Add(parSoLuong);

                SqlParameter parCanNang = new SqlParameter("@canNang", SqlDbType.Real);
                parCanNang.Value = canNang;
                sqlCmd.Parameters.Add(parCanNang);

                // gan vao ket noi
                sqlCmd.Connection = sqlCon;

                // thuc thi truy van
                int kq = sqlCmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Bạn chỉnh sửa thông tin sản phẩm thành công!");
                    HienThiDsSanPham();
                    XoaDuLieu();
                    gbThongTinCT.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bạn chỉnh sửa thông tin sản phẩm thất bại!");
                    XoaDuLieu();
                    gbThongTinCT.Enabled = false;
                }

                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bị lỗi như này: " + ex);
                gbThongTinCT.Enabled = false;
                XoaDuLieu();
            }
        }

        // ham xoa san pham
        private void XoaSanPham()
        {
            try
            {
                if (lsvDanhSach.SelectedItems.Count == 0) return;

                // lay ra ma san pham cua dong dau tien trong danh sach lua chon
                string masp = lsvDanhSach.SelectedItems[0].SubItems[0].Text.Trim();

                MoKetNoi();

                // doi tuong truy van
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "XoaSanPham";

                // dinh nghia paramter
                SqlParameter parMa = new SqlParameter("@masp", SqlDbType.VarChar);
                parMa.Value = masp;
                sqlCmd.Parameters.Add(parMa);

                // gan vao ket noi
                sqlCmd.Connection = sqlCon;

                // thuc thi truy van
                int kq = sqlCmd.ExecuteNonQuery();
                if(kq>0)
                {
                    MessageBox.Show("Bạn xóa sản phẩm thành công!");
                    HienThiDsSanPham();
                }
                else
                {
                    MessageBox.Show("Bạn xóa sản phẩm không thành công!");
                    XoaDuLieu();
                }
                XoaDuLieu();
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bị lỗi ở đây nè: " + ex);
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        // ham xoa du lieu tren form
        private void XoaDuLieu()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtCanNang.Clear();
            nmupSoLuong.Value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDsSanPham();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            gbThongTinCT.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // lay du lieu tu form
            string ma = txtTkMa.Text.Trim();
            string ten = txtTkTen.Text.Trim();

            // phan chia
            if (ma != "" && ten == "") TimKiemTheoMa(ma);
            else if (ma == "" && ten != "") TimKiemTheoTen(ten);
            else if(ma!="" && ten!="") TimKiemTheoMa(ma);
            else
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu cần tìm!");
                txtTkMa.Focus();
            }
        }

        // bien chuc nang
        int chucNang = 0;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (chucNang == 1) ThemMoiSanPham();
            else if (chucNang == 2) ChinhSuaSanPham();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            chucNang = 1;

            gbThongTinCT.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            chucNang = 2;

            gbThongTinCT.Enabled = true;
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0) return;

            // lay ra dong duoc chon dau tien
            ListViewItem lvi = lsvDanhSach.SelectedItems[0];

            // dien du lieu tu lsv sang cac control tuong ung o gbTTChiTiet
            txtMa.Text = lvi.SubItems[0].Text.Trim();
            txtTen.Text = lvi.SubItems[1].Text.Trim();
            nmupSoLuong.Value = Convert.ToDecimal(lvi.SubItems[2].Text.Trim());
            txtCanNang.Text = lvi.SubItems[3].Text.Trim();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có thực sự muốn xóa hay không?",
                "Hộp thoại",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if(result == DialogResult.Yes) 
            {
                XoaSanPham();
            }
            else if(result==DialogResult.No)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
    }
}
