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
    public partial class FormDangNhap : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Dispose();
                this.Close();
            }
        }

        private void buttonDangNhap_Click(object sender, EventArgs e)
        {
            if (txtDangnhap.Text == "" && txtMatkhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản và mật khẩu !", "Thông báo !");
                txtDangnhap.Focus();
            }
            else if (txtDangnhap.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản ! ", "Thông báo !");
                txtDangnhap.Focus();
            }
            else if (txtMatkhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu !", "Thông báo !");
                txtMatkhau.Focus();
            }
            else
            {
                string dangnhap = Convert.ToString("SELECT sTenNV FROM tblNhanVien WHERE sMaNV =");
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlkt = "Select count(*) from tblDangNhap WHERE sMaNV='" + txtDangnhap.Text + "' and sMatkhau='" + txtMatkhau.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlkt, conn);
                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    //MessageBox.Show("Đăng nhập thành công !", "Thông báo");
                    TrangChu.taikhoan = txtDangnhap.Text;
                    TrangChu f1 = new TrangChu();
                    this.Hide();
                    f1.Show();

                }
                else
                {
                    //dem++;
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng !", "Thông báo");
                    txtDangnhap.Focus();
                    /*if(dem == 5)
                    {
                        this.Hide();
                        this.Close();
                    }*/
                }
                conn.Close();
            }
        }

        private void checkBoxHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHienMatKhau.Checked)
                txtMatkhau.UseSystemPasswordChar = true;
            else
                txtMatkhau.UseSystemPasswordChar = false;
        }

        private void txtDangnhap_Validating(object sender, CancelEventArgs e)
        {
            if (txtDangnhap.Text == "")
                errorProvider1.SetError(txtDangnhap, "Bạn chưa nhập tên đăng nhập!");
            else
                errorProvider1.SetError(txtDangnhap, "");
        }

        private void txtMatkhau_Validating(object sender, CancelEventArgs e)
        {
            if (txtMatkhau.Text == "")
                errorProvider1.SetError(txtMatkhau, "Bạn chưa nhập mật khẩu!");
            else
                errorProvider1.SetError(txtMatkhau, "");
        }
    }
}
