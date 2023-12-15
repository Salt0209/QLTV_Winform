using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_Nhom14
{
    public partial class TrangChu : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
        public TrangChu()
        {
            InitializeComponent();
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formNhanVien f1 = new formNhanVien();
            f1.MdiParent = this;
            f1.Show();
        }

        private void độcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formDocGia f1 = new formDocGia();
            f1.MdiParent = this;
            f1.Show();
        }

        private void thểLoạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formTheLoaiSach f1 = new formTheLoaiSach();
            f1.MdiParent = this;
            f1.Show();
        }

        private void nhàXuấtBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formNXB f1 = new formNXB();
            f1.MdiParent = this;
            f1.Show();
        }

        private void tácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formTacGia f1 = new formTacGia();
            f1.MdiParent = this;
            f1.Show();
        }

        private void sáchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formSach f1 = new formSach();
            f1.MdiParent = this;
            f1.Show();
        }

        private void phiếuMượntrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            formMuonTra f1 = new formMuonTra();
            f1.MdiParent = this;
            f1.Show();
        }

        private void sáchĐượcMượnNhiềuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            inTopSachDuocMuon f1 = new inTopSachDuocMuon();
            f1.MdiParent = this;
            f1.Show();
        }
        public static string taikhoan;
        public static string matkhau;
        private void TrangChu_Load(object sender, EventArgs e)
        {          
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT sTenNV FROM tblNhanVien WHERE sMaNV = '" + taikhoan + "'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblNhanVien");
                        ad.Fill(tb);
                        //dataGridView1.DataSource = tb;
                    }
                }
            }
            txtUser.Text = "Xin chào " + taikhoan;
            txtUser.Enabled = false;
        }

        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng form không?", "formclosing", MessageBoxButtons
                .YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxTrangChu.Hide();
            FormDangNhap f1 = new FormDangNhap();
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Dispose();
                //f1.MdiParent = this;
                f1.Show();
                this.Close();
            }
            else
                f1.Hide();
            //f1.Close();
        }
    }
}
