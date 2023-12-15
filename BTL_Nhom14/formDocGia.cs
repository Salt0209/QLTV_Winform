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
    public partial class formDocGia : Form
    {
        public formDocGia()
        {
            InitializeComponent();
        }
        public void hienDSSV()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select sMaSV as [Ma SV],sTenSV as [Ho ten]," +
                    "dNgaysinh as [Ngay sinh],sGioitinh as [Gioi tinh]," +
                    "sSDT as [SDT],sEmail as [Email],sCMND as [CMND] from tblSinhVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewSV.DataSource = tb;
                    }
                }
            }
        }
        private void formDocGia_Load(object sender, EventArgs e)
        {
            hienDSSV();

            btnLuu.Enabled = false;
        }

        private void dataGridViewSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                txtMaSV.Text = dataGridViewSV.CurrentRow.Cells["Ma SV"].Value.ToString();
                txtTenSV.Text = dataGridViewSV.CurrentRow.Cells["Ho ten"].Value.ToString();
                dateNgaySinh.Text = dataGridViewSV.CurrentRow.Cells["Ngay sinh"].Value.ToString();
                string gioitinh = dataGridViewSV.CurrentRow.Cells["Gioi tinh"].Value.ToString();
                if (gioitinh == "Nam")
                {
                    radioBtnNam.Checked = true;
                }
                else if (gioitinh == "Nữ")
                {
                    radioBtnNu.Checked = true;
                }
                txtSDT.Text = dataGridViewSV.CurrentRow.Cells["SDT"].Value.ToString();
                txtEmail.Text = dataGridViewSV.CurrentRow.Cells["Email"].Value.ToString();
                txtCMND.Text = dataGridViewSV.CurrentRow.Cells["CMND"].Value.ToString();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            string sqlTimSV = "SELECT sMaSV as [Ma SV],sTenSV as [Ho ten],dNgaysinh as [Ngay sinh]," +
                "sGioitinh as [Gioi tinh],sSDT as [SDT],sEmail as [Email]," +
                "sCMND as [CMND] FROM tblSinhVien WHERE sMaSV LIKE'%" + txtTimKiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlTimSV, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewSV.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            hienDSSV();
            txtMaSV.ResetText();
            txtTenSV.ResetText();
            txtSDT.ResetText();
            txtCMND.ResetText();
            dateNgaySinh.ResetText();
            txtEmail.ResetText();
            txtCMND.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMaSV.Enabled = true;
            txtTenSV.Enabled = true;
            dateNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
            txtEmail.Enabled = true;
            txtCMND.Enabled = true;
            radioBtnNam.Enabled = true;
            radioBtnNu.Enabled = true;
        }

        private int kiemtraMaSV()
        {
            string k = txtMaSV.Text;
            string sql = "SELECT * FROM tblSinhVien WHERE sMaSV ='" + k.ToString() + "'";
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
                if (kiemtraMaSV() == 1)
                {
                    MessageBox.Show("Mã sinh viên " + txtMaSV.Text + " đã có, không thể thêm !", "Thông báo");
                    txtMaSV.Focus();
                }
                else
                {
                    if (txtMaSV.Text != "")
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_ThemSV";
                            cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                            cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text);
                            cmd.Parameters.AddWithValue("@Ngaysinh", dateNgaySinh.Text);
                            if (radioBtnNam.Checked)
                                cmd.Parameters.AddWithValue("@Gioitinh", "Nam");
                            else
                                cmd.Parameters.AddWithValue("@Gioitinh", "Nữ");
                            cmd.Parameters.AddWithValue("@Sdt", txtSDT.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);
                            cnn.Open();

                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã thêm thành công sinh viên " + txtMaSV.Text + "!", "Thông báo");
                            }
                            txtMaSV.ResetText();
                            cnn.Close();
                            hienDSSV();
                        }
                    }
                    else
                        errorProvider1.SetError(txtMaSV, "Bạn chưa nhập mã sinh viên!");
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
                    cmd.CommandText = "sp_SuaSV";
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                    cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text);
                    cmd.Parameters.AddWithValue("@Ngaysinh", dateNgaySinh.Text);
                    if (radioBtnNam.Checked)
                        cmd.Parameters.AddWithValue("@Gioitinh", "Nam");
                    else
                        cmd.Parameters.AddWithValue("@Gioitinh", "Nữ");
                    cmd.Parameters.AddWithValue("@Sdt", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);
                    cnn.Open();
                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                            MessageBox.Show("Đã sửa thành công !", "Thông báo");
                        else
                            MessageBox.Show("Sửa không thành công !", "Thông báo");
                        hienDSSV();
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
                    cmd.CommandText = "sp_XoaSV";
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                txtMaSV.ResetText();
                                txtTenSV.ResetText();
                                dateNgaySinh.ResetText();
                                txtSDT.ResetText();
                                txtEmail.ResetText();
                                txtCMND.ResetText();
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        hienDSSV();
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;

            txtMaSV.ResetText();
            txtTenSV.ResetText();
            dateNgaySinh.ResetText();
            txtSDT.ResetText();
            txtEmail.ResetText();
            txtCMND.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            //btnLuu.Enabled = false;

            txtMaSV.Enabled = false;
            txtTenSV.Enabled = false;
            dateNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
            txtEmail.Enabled = false;
            txtCMND.Enabled = false;
            radioBtnNam.Enabled = false;
            radioBtnNu.Enabled = false;

            if (txtTimKiem.Text != "")
                txtTimKiem.ResetText();
        }
    }
}
