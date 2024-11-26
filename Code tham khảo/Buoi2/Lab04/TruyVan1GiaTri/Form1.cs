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

namespace TruyVan1GiaTri
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
            if(sqlCon==null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoKetNoi();

            // doi tuong thuc thi truy van]
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select count(*) from SinhVien";

            // gan vao ket noi
            sqlCmd.Connection = sqlCon;

            // thuc thi truy van
            int kq = (int)sqlCmd.ExecuteScalar();
            MessageBox.Show("So luong sinh vien la: " + kq);
        }
    }
}
