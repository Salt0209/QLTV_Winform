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
    public partial class formNXB : Form
    {
        public formNXB()
        {
            InitializeComponent();
        }

        public void hienDS_NXB()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select sMaNXB as [Ma NXB],sTenNXB as [Ten NXB]," +
                    "dDiachi as [Dia chi],sEmail as [Email] from tblNXB", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNXB.DataSource = tb;
                    }
                }
            }
        }

        private int kiemtraMaTL()
        {
            string k = txtMa.Text;
            string sql = "SELECT * FROM tblNXB WHERE sMaNXB ='" + k.ToString() + "'";
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        if (tb.Rows.Count > 0) return 1;
                        else return 0;
                    }
                }
            }
        }
        private void formNXB_Load(object sender, EventArgs e)
        {
            hienDS_NXB();

            btnLuu.Enabled = false;
        }

        private void dataGridViewNXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridViewNXB.CurrentRow.Cells["Ma NXB"].Value.ToString();
            txtTen.Text = dataGridViewNXB.CurrentRow.Cells["Ten NXB"].Value.ToString();
            txtDiachi.Text = dataGridViewNXB.CurrentRow.Cells["Dia chi"].Value.ToString();
            txtEmail.Text = dataGridViewNXB.CurrentRow.Cells["Email"].Value.ToString();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            string sqlTimNV = "Select sMaNXB as [Ma NXB],sTenNXB as [Ten NXB],dDiachi as [Dia chi],sEmail as [Email] from tblNXB WHERE sMaNXB LIKE'%" + txtTimKiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlTimNV, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNXB.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;

            txtMa.ResetText();
            txtTen.ResetText();
            txtDiachi.ResetText();
            txtEmail.ResetText();

            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;

            txtMa.Enabled = false;
            txtTen.Enabled = false;
            txtDiachi.Enabled = false;
            txtEmail.Enabled = false;

            if (txtTimKiem.Text != "")
                txtTimKiem.ResetText();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SuaNXB";
                    cmd.Parameters.AddWithValue("@MaNXB", txtMa.Text);
                    cmd.Parameters.AddWithValue("@TenNXB", txtTen.Text);
                    cmd.Parameters.AddWithValue("@Diachi", txtDiachi.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cnn.Open();
                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                            MessageBox.Show("Đã sửa thành công !", "Thông báo");
                        else
                            MessageBox.Show("Sửa không thành công !", "Thông báo");
                        hienDS_NXB();
                        cnn.Close();
                    }
                }
            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                if (kiemtraMaTL() == 1)
                {
                    MessageBox.Show("Mã thể loại " + txtMa.Text + " đã có, không thể thêm !", "Thông báo");
                    txtMa.Focus();
                }
                else
                {
                    if (txtMa.Text != "")
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_ThemNXB";
                            cmd.Parameters.AddWithValue("@MaNXB", txtMa.Text);
                            cmd.Parameters.AddWithValue("@TenNXB", txtTen.Text);
                            cmd.Parameters.AddWithValue("@Diachi", txtDiachi.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cnn.Open();

                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã thêm thành công NXB " + txtTen.Text + "!", "Thông báo");
                            }
                            txtMa.ResetText();
                            cnn.Close();
                            hienDS_NXB();
                        }
                    }
                    else
                        errorProvider1.SetError(txtMa, "Bạn chưa nhập mã NXB!");
                }
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            hienDS_NXB();
            txtMa.ResetText();
            txtTen.ResetText();
            txtDiachi.ResetText();
            txtEmail.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtDiachi.Enabled = true;
            txtEmail.Enabled = true;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_XoaNXB";
                    cmd.Parameters.AddWithValue("@MaNXB", txtMa.Text);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                txtMa.ResetText();
                                txtTen.ResetText();
                                txtDiachi.ResetText();
                                txtEmail.ResetText();
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        hienDS_NXB();
                    }
                }
            }
        }

    }
}
