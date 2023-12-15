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
    public partial class formTacGia : Form
    {
        public formTacGia()
        {
            InitializeComponent();
        }
        public void hienDS_TG()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select sMatacgia as [Ma tac gia],sTentacgia as [Ten tac gia]," +
                    "dNgaysinh as [Ngay sinh],sNoiCT as [Noi cong tac] from tblTacGia", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewTG.DataSource = tb;
                    }
                }
            }
        }

        private int kiemtraMaTG()
        {
            string k = txtMa.Text;
            string sql = "SELECT * FROM tblTacgia WHERE sMatacgia ='" + k.ToString() + "'";
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
        private void formTacGia_Load(object sender, EventArgs e)
        {
            hienDS_TG();

            btnLuu.Enabled = false;
        }

        private void dataGridViewTG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridViewTG.CurrentRow.Cells["Ma tac gia"].Value.ToString();
            txtTen.Text = dataGridViewTG.CurrentRow.Cells["Ten tac gia"].Value.ToString();
            dateNgaySinh.Text = dataGridViewTG.CurrentRow.Cells["Ngay sinh"].Value.ToString();
            txtNoiCT.Text = dataGridViewTG.CurrentRow.Cells["Noi cong tac"].Value.ToString();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            string sqlTimNV = "select sMatacgia as [Ma tac gia],sTentacgia as [Ten tac gia],dNgaysinh as [Ngay sinh],sNoiCT as [Noi cong tac] from tblTacGia WHERE sMatacgia LIKE'%" + txtTimKiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlTimNV, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewTG.DataSource = tb;
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
            dateNgaySinh.ResetText();
            txtNoiCT.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            //btnLuu.Enabled = false;

            txtMa.Enabled = false;
            txtTen.Enabled = false;
            dateNgaySinh.Enabled = false;
            txtNoiCT.Enabled = false;

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
                    cmd.CommandText = "sp_SuaTG";
                    cmd.Parameters.AddWithValue("@MaTG", txtMa.Text);
                    cmd.Parameters.AddWithValue("@TenTG", txtTen.Text);
                    cmd.Parameters.AddWithValue("@Ngaysinh", dateNgaySinh.Text);
                    cmd.Parameters.AddWithValue("@NoiCT", txtNoiCT.Text);
                    cnn.Open();
                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                            MessageBox.Show("Đã sửa thành công !", "Thông báo");
                        else
                            MessageBox.Show("Sửa không thành công !", "Thông báo");
                        hienDS_TG();
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
                if (kiemtraMaTG() == 1)
                {
                    MessageBox.Show("Mã tác giả " + txtMa.Text + " đã có, không thể thêm !", "Thông báo");
                    txtMa.Focus();
                }
                else
                {
                    if (txtMa.Text != "")
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_ThemTG";
                            cmd.Parameters.AddWithValue("@MaTG", txtMa.Text);
                            cmd.Parameters.AddWithValue("@TenTG", txtTen.Text);
                            cmd.Parameters.AddWithValue("@Ngaysinh", dateNgaySinh.Text);
                            cmd.Parameters.AddWithValue("@NoiCT", txtNoiCT.Text);
                            cnn.Open();

                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã thêm thành công tác giả " + txtTen.Text + "!", "Thông báo");
                            }
                            txtMa.ResetText();
                            cnn.Close();
                            hienDS_TG();
                        }
                    }
                    else
                        errorProvider1.SetError(txtMa, "Bạn chưa nhập mã tác giả!");
                }
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            hienDS_TG();
            txtMa.ResetText();
            txtTen.ResetText();
            dateNgaySinh.ResetText();
            txtNoiCT.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtNoiCT.Enabled = true;
            dateNgaySinh.Enabled = true;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_XoaTG";
                    cmd.Parameters.AddWithValue("@MaTG", txtMa.Text);
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
                                dateNgaySinh.ResetText();
                                txtNoiCT.ResetText();
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        hienDS_TG();
                    }
                }
            }
        }
    }
}
