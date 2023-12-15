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
    public partial class formSach : Form
    {
        public formSach()
        {
            InitializeComponent();
        }

        public void layDS_Sach()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select sMasach as [Ma sach],sTensach as [Ten sach],sTacgia as [Tac gia]," +
                    "sMaNXB as [Ma NXB],iNamXB as [Nam xuat ban],sTinhtrangsach as [Tinh trang],sMaloai as [Ma loai] from tblSach", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewSach.DataSource = tb;
                    }
                }
            }
        }

        private void layDS_Tacgia()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblTacGia", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("TG");
                        ad.Fill(tb);
                        cbTacgia.DataSource = tb;
                        cbTacgia.DisplayMember = "sTentacgia";
                        cbTacgia.ValueMember = "sMatacgia";
                    }
                }
            }
        }

        private void layDS_Theloai()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblTheLoai", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("TL");
                        ad.Fill(tb);
                        cbLoaiSach.DataSource = tb;
                        cbLoaiSach.DisplayMember = "sTenloai";
                        cbLoaiSach.ValueMember = "sMaloai";
                    }
                }
            }
        }

        private void layDS_NXB()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblNXB", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("NXB");
                        ad.Fill(tb);
                        cbNXB.DataSource = tb;
                        cbNXB.DisplayMember = "sTenNXB";
                        cbNXB.ValueMember = "sMaNXB";
                    }
                }
            }
        }

        private void dataGridViewSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSach.Text = dataGridViewSach.CurrentRow.Cells["Ma sach"].Value.ToString();
            txtTenSach.Text = dataGridViewSach.CurrentRow.Cells["Ten sach"].Value.ToString();
            cbTacgia.Text = dataGridViewSach.CurrentRow.Cells["Tac gia"].Value.ToString();
            cbNXB.Text = dataGridViewSach.CurrentRow.Cells["Ma NXB"].Value.ToString();
            txtNamXB.Text = dataGridViewSach.CurrentRow.Cells["Nam xuat ban"].Value.ToString();
            txtTinhtrang.Text = dataGridViewSach.CurrentRow.Cells["Tinh trang"].Value.ToString();
            cbLoaiSach.Text = dataGridViewSach.CurrentRow.Cells["Ma loai"].Value.ToString();
        }

        private void formSach_Load(object sender, EventArgs e)
        {
            layDS_Sach();
            layDS_Tacgia();
            layDS_NXB();
            layDS_Theloai();

            btnLuu.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;

            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã sách !", "Thông báo");
            }

            layDS_Sach();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            layDS_Sach();
            txtMaSach.ResetText();
            txtTenSach.ResetText();
            txtNamXB.ResetText();
            txtTinhtrang.ResetText();

            btnLuu.Enabled = true;
        }

        private int kiemtra()
        {
            string k = txtMaSach.Text;
            string sql = "SELECT * FROM tblSach WHERE sMasach ='" + k.ToString() + "'";
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

        private int kiemtramasach_tentacgia()
        {
            string masach = txtMaSach.Text;
            string tentg = Convert.ToString(cbTacgia.SelectedValue);
            string sql = "SELECT * FROM tblSach,tblTacGia WHERE tblSach.sTacgia = tblTacGia.sMatacgia AND sMasach= '" + masach + "' AND tblTacGia.sTentacgia= '" + tentg + "'";
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

        private int kiemtramasachtrung_tentacgiakhongtrung()
        {
            string masach = txtMaSach.Text;
            string tentg = Convert.ToString(cbTacgia.SelectedValue);
            string sql = "SELECT * FROM tblSach,tblTacGia WHERE tblSach.sTacgia = tblTacGia.sMatacgia AND sMasach= '" + masach + "' AND tblTacGia.sTentacgia != '" + tentg + "'";
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                if (kiemtramasach_tentacgia() == 1)
                {
                    MessageBox.Show("Mã sách " + txtMaSach.Text + ", tên tác giả " + cbTacgia.SelectedValue + " đã có, không thể thêm !", "Thông báo");
                    txtMaSach.Focus();
                    cbTacgia.Focus();
                }
                else
                {
                    if (kiemtramasachtrung_tentacgiakhongtrung() == 1)
                    {
                        MessageBox.Show("Mã sách " + txtMaSach.Text + " đã có, không thể thêm !", "Thông báo");
                        txtMaSach.Focus();
                    }
                    else
                    {
                        if (txtMaSach.Text != "")
                        {
                            using (SqlCommand cmd = cnn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "sp_ThemSach";
                                cmd.Parameters.AddWithValue("@Masach", txtMaSach.Text);
                                cmd.Parameters.AddWithValue("@Tensach", txtTenSach.Text);
                                cmd.Parameters.AddWithValue("@Matacgia", cbTacgia.SelectedValue);
                                cmd.Parameters.AddWithValue("@MaNXB", cbNXB.SelectedValue);
                                cmd.Parameters.AddWithValue("@NamXB", txtNamXB.Text);
                                cmd.Parameters.AddWithValue("@Tinhtrang", txtTinhtrang.Text);
                                cmd.Parameters.AddWithValue("@Maloai", cbLoaiSach.SelectedValue);
                                cnn.Open();

                                int kq = cmd.ExecuteNonQuery();
                                if (kq > 0)
                                {
                                    MessageBox.Show("Đã thêm thành công sách mã " + txtMaSach.Text + "!", "Thông báo");
                                }
                                txtMaSach.ResetText();
                                cnn.Close();
                                layDS_Sach();
                            }
                        }
                        else
                            errorProvider1.SetError(txtMaSach, "Bạn chưa nhập mã sách !");
                    }
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SuaSach";
                    cmd.Parameters.AddWithValue("@Masach", txtMaSach.Text);
                    cmd.Parameters.AddWithValue("@Tensach", txtTenSach.Text);
                    cmd.Parameters.AddWithValue("@Matacgia", cbTacgia.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaNXB", cbNXB.SelectedValue);
                    cmd.Parameters.AddWithValue("@NamXB", txtNamXB.Text);
                    cmd.Parameters.AddWithValue("@Tinhtrang", txtTinhtrang.Text);
                    cmd.Parameters.AddWithValue("@Maloai", cbLoaiSach.SelectedValue);
                    cnn.Open();

                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                            MessageBox.Show("Đã sửa thành công !", "Thông báo");
                        else
                            MessageBox.Show("Sửa không thành công !", "Thông báo");
                        layDS_Sach();
                        cnn.Close();
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
                    cmd.CommandText = "sp_XoaSach";
                    cmd.Parameters.AddWithValue("@Masach", txtMaSach.Text);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        /*DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridView1.DataSource = tb;*/

                        txtMaSach.ResetText();
                        txtTenSach.ResetText();
                        txtNamXB.ResetText();
                        txtTinhtrang.ResetText();

                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        layDS_Sach();
                    }
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            string sqlFindSach = "select sMasach as [Ma sach],sTensach as [Ten sach],sTacgia as [Tac gia],sMaNXB as [Ma NXB],iNamXB as [Nam xuat ban],sTinhtrangsach as [Tinh trang],sMaloai as [Ma loai] from tblSach where sMasach LIKE'%" + txtTimKiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindSach, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewSach.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }          
        }
    }
}
