using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = null;
        string strcon = "Data Source=Admin;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
        SqlDataAdapter adp = null;
        DataSet ds = null;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            closeconnect();
            Close();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (con == null) con = new SqlConnection(strcon);
            adp = new SqlDataAdapter("select * from QuanLiKhachSan", con);
            SqlCommandBuilder bulder = new SqlCommandBuilder(adp);
            ds = new DataSet();
            adp.Fill(ds,"quanli");
            gvphong.DataSource = ds.Tables["quanli"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["quanli"].NewRow();
            row["MaPhong"] = txbMa.Text;
            row["TenPhong"] = txbTenPhong.Text;
            row["DonGia"] = txbGia.Text;
            ds.Tables["quanli"].Rows.Add(row);
        }
        int vt = -1;
        

        private void txbGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvphong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt=e.RowIndex;
            DataRow row = ds.Tables["Quanli"].Rows[vt];
            txbMa.Text = row["MaPhong"] + "";
            txbGia.Text = row["DonGia"] + "";
            txbTenPhong.Text = row["TenPhong"] + "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (vt == -1) return;
            DataRow row = ds.Tables[0].Rows[vt];
            row.BeginEdit();
            row["TenPhong"] = txbTenPhong.Text;
            row["DonGia"] = txbGia.Text;
            row.EndEdit();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["Quanli"].Rows[vt];
            row.Delete();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txbGia.Text = "";
            txbMa.Text = "";
            txbTenPhong.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void closeconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
