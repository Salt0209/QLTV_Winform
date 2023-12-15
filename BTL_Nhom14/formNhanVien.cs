using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace BTL_Nhom14
{
    public partial class formNhanVien : Form
    {
        public formNhanVien()
        {
            InitializeComponent();
        }

        public void hienDSNV()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select sMaNV as [Ma NV],sTenNV as [Ho ten]," +
                    "dNgaysinh as [Ngay sinh],sGioitinh as [Gioi tinh],sSDT as [SDT]," +
                    "dNgayvaolam as [Ngay vao lam],sCMND as [CMND] from tblNhanVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNV.DataSource = tb;
                    }
                }
            }
        }

        private void formNhanVien_Load(object sender, EventArgs e)
        {
            hienDSNV();

            btnLuu.Enabled = false;

        }

        private void dataGridViewNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dataGridViewNV.CurrentRow.Cells["Ma NV"].Value.ToString();
            txtTenNV.Text = dataGridViewNV.CurrentRow.Cells["Ho ten"].Value.ToString();
            dateNgaySinh.Text = dataGridViewNV.CurrentRow.Cells["Ngay sinh"].Value.ToString();
            string gioitinh = dataGridViewNV.CurrentRow.Cells["Gioi tinh"].Value.ToString();
            if(gioitinh == "Nam")
            {
                radioBtnNam.Checked = true;
            }
            else if(gioitinh == "Nữ")
            {
                radioBtnNu.Checked = true;
            }
            txtSDT.Text = dataGridViewNV.CurrentRow.Cells["SDT"].Value.ToString();
            dateNgayvaolam.Text = dataGridViewNV.CurrentRow.Cells["Ngay vao lam"].Value.ToString();
            txtCMND.Text = dataGridViewNV.CurrentRow.Cells["CMND"].Value.ToString();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            string sqlTimNV = "SELECT sMaNV AS [Ma NV],sTenNV AS [Ho ten],dNgaysinh AS [Ngay sinh],sGioitinh AS [Gioi tinh], sSDT AS [SDT], dNgayvaolam AS [Ngay vao lam],sCMND AS [CMND] FROM tblNhanVien WHERE sMaNV LIKE'%" + txtTimKiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlTimNV, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNV.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            hienDSNV();
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtSDT.ResetText();
            txtCMND.ResetText();
            dateNgaySinh.ResetText();
            dateNgayvaolam.ResetText();
            txtCMND.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            dateNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
            dateNgayvaolam.Enabled = true;
            txtCMND.Enabled = true;
            radioBtnNam.Enabled = true;
            radioBtnNu.Enabled = true;
        }

        private int kiemtraMaNV()
        {
            string k = txtMaNV.Text;
            string sql = "SELECT * FROM tblNhanVien WHERE sMaNV ='" + k.ToString() + "'";
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
                if (kiemtraMaNV() == 1)
                {
                    MessageBox.Show("Mã nhân viên " + txtMaNV.Text + " đã có, không thể thêm !", "Thông báo");
                    txtMaNV.Focus();
                }
                else
                {
                    if (txtMaNV.Text != "")
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_ThemNV";
                            cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                            cmd.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                            cmd.Parameters.AddWithValue("@Ngaysinh", /*Convert.ToDateTime(*/dateNgaySinh.Text/*)*/);
                            if (radioBtnNam.Checked)
                                cmd.Parameters.AddWithValue("@Gioitinh", "Nam");
                            else
                                cmd.Parameters.AddWithValue("@Gioitinh", "Nữ");
                            cmd.Parameters.AddWithValue("@Sdt", txtSDT.Text);
                            cmd.Parameters.AddWithValue("@Ngayvaolam", dateNgayvaolam.Text);
                            cmd.Parameters.AddWithValue("@CMND", txtCMND.Text);
                            cnn.Open();

                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã thêm thành công nhân viên " + txtMaNV.Text + "!", "Thông báo");
                            }
                            txtMaNV.ResetText();
                            cnn.Close();
                            hienDSNV();
                        }
                    }
                    else
                        errorProvider1.SetError(txtMaNV, "Bạn chưa nhập mã nhân viên!");
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
                    cmd.CommandText = "sp_SuaNV";
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@Ngaysinh", /*Convert.ToDateTime(*/dateNgaySinh.Text/*)*/);
                    if (radioBtnNam.Checked)
                        cmd.Parameters.AddWithValue("@Gioitinh", "Nam");
                    else
                        cmd.Parameters.AddWithValue("@Gioitinh", "Nữ");
                    cmd.Parameters.AddWithValue("@Sdt", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@Ngayvaolam", dateNgayvaolam.Text);
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
                        hienDSNV();
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
                    cmd.CommandText = "sp_XoaNV";
                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                txtMaNV.ResetText();
                                txtTenNV.ResetText();
                                dateNgaySinh.ResetText();
                                txtSDT.ResetText();
                                dateNgayvaolam.ResetText();
                                txtCMND.ResetText();
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        hienDSNV();
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;

            txtMaNV.ResetText();
            txtTenNV.ResetText();
            dateNgaySinh.ResetText();
            txtSDT.ResetText();
            dateNgayvaolam.ResetText();
            txtCMND.ResetText();

            //btnXoa.Enabled = false;
            //btnSua.Enabled = false;
            //btnLuu.Enabled = false;

            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            dateNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
            dateNgayvaolam.Enabled = false;
            txtCMND.Enabled = false;
            radioBtnNam.Enabled = false;
            radioBtnNu.Enabled = false;

            if (txtTimKiem.Text != "")
                txtTimKiem.ResetText();
        }

        private void txtTenNV_Validating(object sender, CancelEventArgs e)
        {
            



        }

        private void button1_Click(object sender, EventArgs e)
        {
            string time = "tg" + dateNgaySinh.Value.Day + "/" + dateNgaySinh.Value.Month + "/" + dateNgaySinh.Value.Year;
            MessageBox.Show("abc" + time);
        }

        private void dateNgaySinh_Validating(object sender, CancelEventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan Time = now.Subtract(dateNgaySinh.Value);
            int tuoi = Time.Days / 365;
            if (tuoi < 18)
            {
                errorProvider1.SetError(dateNgaySinh, "Tuoi phai lon hon 18");
            }
            else
            {
                errorProvider1.SetError(dateNgaySinh, "");
            }
        }
    }
}
