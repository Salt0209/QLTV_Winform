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
    public partial class formTheLoaiSach : Form
    {
        public formTheLoaiSach()
        {
            InitializeComponent();
        }
        public void hienDS_TheLoai()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select sMaloai as [Ma loai]," +
                    "sTenloai as [Ten loai sach] from tblTheLoai", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewTheLoai.DataSource = tb;
                    }
                }
            }
        }

        private int kiemtraMaTL()
        {
            string k = txtMaTheLoai.Text;
            string sql = "SELECT * FROM tblTheloai WHERE sMaloai ='" + k.ToString() + "'";
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


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_XoaTL";
                    cmd.Parameters.AddWithValue("@Matheloai", txtMaTheLoai.Text);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                txtMaTheLoai.ResetText();
                                txtTenTheLoai.ResetText();
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        hienDS_TheLoai();
                    }
                }
            }
        }
        private void TheLoaiSach_Load(object sender, EventArgs e)
        {
            hienDS_TheLoai();

            btnLuu.Enabled = false;
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;

            txtMaTheLoai.ResetText();
            txtTenTheLoai.ResetText();

            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;

            txtMaTheLoai.Enabled = false;
            txtTenTheLoai.Enabled = false;

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
                    cmd.CommandText = "sp_SuaTL";
                    cmd.Parameters.AddWithValue("@Matheloai", txtMaTheLoai.Text);
                    cmd.Parameters.AddWithValue("@Tentheloai", txtTenTheLoai.Text);
                    cnn.Open();
                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                            MessageBox.Show("Đã sửa thành công !", "Thông báo");
                        else
                            MessageBox.Show("Sửa không thành công !", "Thông báo");
                        hienDS_TheLoai();
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
                    MessageBox.Show("Mã thể loại " + txtMaTheLoai.Text + " đã có, không thể thêm !", "Thông báo");
                    txtMaTheLoai.Focus();
                }
                else
                {
                    if (txtMaTheLoai.Text != "")
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_ThemTL";
                            cmd.Parameters.AddWithValue("@Matheloai", txtMaTheLoai.Text);
                            cmd.Parameters.AddWithValue("@Tentheloai", txtTenTheLoai.Text);
                            cnn.Open();

                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã thêm thành công thể loại " + txtTenTheLoai.Text + "!", "Thông báo");
                            }
                            txtMaTheLoai.ResetText();
                            cnn.Close();
                            hienDS_TheLoai();
                        }
                    }
                    else
                        errorProvider1.SetError(txtMaTheLoai, "Bạn chưa nhập mã thể loại!");
                }
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            hienDS_TheLoai();
            txtMaTheLoai.ResetText();
            txtTenTheLoai.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMaTheLoai.Enabled = true;
            txtTenTheLoai.Enabled = true;
        }

        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            string sqlTimNV = "Select sMaloai as [Ma loai],sTenloai as [Ten loai sach] from tblTheLoai WHERE sMaloai LIKE'%" + txtTimKiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlTimNV, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewTheLoai.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void dataGridViewTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaTheLoai.Text = dataGridViewTheLoai.CurrentRow.Cells["Ma loai"].Value.ToString();
            txtTenTheLoai.Text = dataGridViewTheLoai.CurrentRow.Cells["Ten loai sach"].Value.ToString();
        }
    }
}
