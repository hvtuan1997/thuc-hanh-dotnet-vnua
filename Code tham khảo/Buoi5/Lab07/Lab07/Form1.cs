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
using System.Net.Http.Headers;

namespace Lab07
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

        SqlDataAdapter adapter= null;
        DataSet ds = null;

        // ham mo ket noi
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // ham hien thi danh sach sinh vien
        private void HienThiDSSinhVien()
        {
            MoKetNoi();

            // lenh truy van
            string query = "select * from tblSinhVien";

            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder= new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "tblSinhVien");
            dgvDanhSach.DataSource = ds.Tables["tblSinhVien"];
        }

        // ham xoa du lieu
        private void XoaDuLieu()
        {
            txtma.Clear();
            txtTen.Clear();
            txtQueQuan.Clear();
            txtMaLop.Clear();
        }

        // ham tim kiem theo ma
        private void TimKiemTheoMa(string ma)
        {
            MoKetNoi();

            // lenh truy van
            string query = "select * from tblSinhVien where MaSV = '"+ma+"'";

            adapter = new SqlDataAdapter(query, sqlCon);
            ds = new DataSet();
            adapter.Fill(ds, "tblTkMa");
            dgvDanhSach.DataSource = ds.Tables["tblTkMa"];
        }

        // ham tim kiem theo ten
        private void TimKiemTheoTen(string ten)
        {
            MoKetNoi();

            // lenh truy van
            string query = "select * from tblSinhVien where TenSV Like '%"+ten+"%'";

            adapter = new SqlDataAdapter(query, sqlCon);
            ds = new DataSet();
            adapter.Fill(ds, "tblTkTen");
            dgvDanhSach.DataSource = ds.Tables["tblTkTen"];
        }

        // ham them sinh vien
        private void ThemSinhVien()
        {
            try
            {
                MoKetNoi();

                DataRow row = ds.Tables["tblSinhVien"].NewRow();
                row["MaSV"] = txtma.Text.Trim();
                row["TenSV"] = txtTen.Text.Trim();
                if (cbGioiTinh.SelectedIndex == 0) row["GioiTinh"] = "Nam";
                else if (cbGioiTinh.SelectedIndex == 1) row["GioiTinh"] = "Nữ";
                row["NgaySinh"] = dtpNgaySinh.Value.Year + "-" + dtpNgaySinh.Value.Month + "-" + dtpNgaySinh.Value.Day;
                row["QueQuan"] = txtQueQuan.Text.Trim();
                row["MaLop"] = txtMaLop.Text.Trim();

                ds.Tables["tblSinhVien"].Rows.Add(row);
                int kq = adapter.Update(ds.Tables["tblSinhVien"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm sinh viên thành công!");
                    HienThiDSSinhVien();
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên không thành công!");
                }
                XoaDuLieu();
                gbTTCT.Enabled = false;
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi ở đây nè " + ex);
            }
        }

        // ham chinh sua thong tin sinh vien
        private void ChinhSuaTTSinhVien()
        {
            try
            {
                if (vt == -1) return;
                MoKetNoi();

                DataRow row = ds.Tables["tblSinhVien"].Rows[vt];
                row.BeginEdit();
                row["MaSV"] = txtma.Text.Trim();
                row["TenSV"] = txtTen.Text.Trim();
                if (cbGioiTinh.SelectedIndex == 0) row["GioiTinh"] = "Nam";
                else if (cbGioiTinh.SelectedIndex == 1) row["GioiTinh"] = "Nữ";
                row["NgaySinh"] = dtpNgaySinh.Value.Year + "-" + dtpNgaySinh.Value.Month + "-" + dtpNgaySinh.Value.Day;
                row["QueQuan"] = txtQueQuan.Text.Trim();
                row["MaLop"] = txtMaLop.Text.Trim();
                row.EndEdit();

                int kq = adapter.Update(ds.Tables["tblSinhVien"]);
                if (kq > 0)
                {
                    MessageBox.Show("Chỉnh sửa thông tin sinh viên thành công!");
                    HienThiDSSinhVien();
                }
                else
                {
                    MessageBox.Show("hỉnh sửa thông tin sinh viên không thành công!");
                }
                XoaDuLieu();
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                gbTTCT.Enabled = false;
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi ở đây nè: " + ex);
            }
        }

        // ham xoa sinh vien
        private void XoaSinhvien()
        {
            try
            {
                if (vt == -1) return;
                MoKetNoi();
                DataRow row = ds.Tables["tblSinhVien"].Rows[vt];
                row.Delete();
                int kq = adapter.Update(ds.Tables["tblSinhVien"]);
                if (kq > 0)
                {
                    MessageBox.Show("Xóa sinh viên thành công!");
                    HienThiDSSinhVien();
                }
                else
                {
                    MessageBox.Show("Xóa sinh viên không thành công!");
                }
                XoaDuLieu();
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                gbTTCT.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ở đây nè: " + ex);
            }
        }

        private void btnTHoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát hay không?", "Hộp thoại", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDSSinhVien();

            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
            cbGioiTinh.SelectedIndex = 0;

            gbTTCT.Enabled = false;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ma = txtTkMa.Text.Trim();
            string ten = txtTkTen.Text.Trim();

            if (ma != "" && ten == "") TimKiemTheoMa(ma);
            else if (ma == "" && ten != "") TimKiemTheoTen(ten);
            else if (ma != "" && ten != "") TimKiemTheoMa(ma);
            else
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu cần tìm!");
                txtTkMa.Focus();
            }
        }

        int chucNang = 0;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (chucNang == 1)
                ThemSinhVien();
            else if (chucNang == 2)
                ChinhSuaTTSinhVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            gbTTCT.Enabled = true;
            chucNang = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            gbTTCT.Enabled = true;
            chucNang = 2;
        }

        int vt = -1;

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            if (vt == -1) return;

            DataRow row = ds.Tables["tblSinhVien"].Rows[vt];
            txtma.Text = row["MaSV"].ToString().Trim();
            txtTen.Text = row["TenSV"].ToString().Trim();
            if (row["GioiTinh"].ToString().Trim() == "Nam") cbGioiTinh.SelectedIndex = 0;
            else cbGioiTinh.SelectedIndex = 1;
            string[] a = row["NgaySinh"].ToString().Trim().Split(' ');
            string[] b = a[0].Split('/');
            dtpNgaySinh.Value = new DateTime(int.Parse(b[2]), int.Parse(b[0]), int.Parse(b[1]));
            txtQueQuan.Text = row["QueQuan"].ToString().Trim();
            txtMaLop.Text = row["MaLop"].ToString().Trim();

            btnSua.Enabled= true;
            btnXoa.Enabled= true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (vt == -1) return;
            DialogResult result= MessageBox.Show("Bạn xác định xóa sinh viên này hay không?", "Hộp thoại", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result==DialogResult.Yes)
            {
                XoaSinhvien();
            }
            else
            {
                btnXoa.Enabled = false;
            }
        }
    }
}
