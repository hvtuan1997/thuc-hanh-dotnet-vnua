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

namespace KetNoiCSDL
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
            // rong thi tao moi
            if(sqlCon==null)
                sqlCon = new SqlConnection(strCon);

            // dong thi mo
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
                MessageBox.Show("Ket noi thanh cong!");
            }
        }

        // ham dong ket noi
        private void DongKetNoi()
        { 
            if(sqlCon!=null && sqlCon.State==ConnectionState.Open)
                sqlCon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoKetNoi();
        }
    }
}
