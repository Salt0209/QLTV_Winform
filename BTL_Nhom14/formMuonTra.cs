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
    public partial class formMuonTra : Form
    {
        private const string V = "MM/dd/yyyy";
        string constr = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
        public formMuonTra()
        {
            InitializeComponent();
        }
        public void rgLoadMT()
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM vwDSMuonTra", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewMT.DataSource = tb;
                    }
                }
            }
        }

        private void laySV()
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblSinhVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblSinhVien");
                        ad.Fill(tb);
                        comboBoxSV.DataSource = tb;
                        comboBoxSV.DisplayMember = "sTenSV";
                        comboBoxSV.ValueMember = "sMaSV";
                    }
                }
            }
        }

        private void layNV()
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblNhanVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblNhanVien");
                        ad.Fill(tb);
                        comboBoxNV.DataSource = tb;
                        comboBoxNV.DisplayMember = "sTenNV";
                        comboBoxNV.ValueMember = "sMaNV";
                    }
                }
            }
        }

        private void muontra_Load(object sender, EventArgs e)
        {
            rgLoadMT();
            laySV();
            layNV();

            layMT();
            layCTMT();

            rgLoadCTMT();

        }

        private void dataGridViewMT_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int numrow;
            numrow = e.RowIndex;
            txtMaMT.Text = dataGridViewMT.Rows[numrow].Cells[0].Value.ToString();
            comboBoxSV.Text = dataGridViewMT.Rows[numrow].Cells[2].Value.ToString();
            comboBoxNV.Text = dataGridViewMT.Rows[numrow].Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridViewMT.Rows[numrow].Cells[5].Value.ToString();



            int currenIndex = dataGridViewMT.CurrentCell.RowIndex;
            string userID = dataGridViewMT.Rows[currenIndex].Cells[0].Value.ToString();
            string sqlFindCTMT = "SELECT sMaMT AS [Mã mượn trả],tblSach.sMaSach AS [Mã sách],sTensach AS [Tên sách],dNgaytra AS [Ngày trả] FROM tblSach,tblCTMuonTra WHERE tblSach.sMasach = tblCTMuonTra.sMasach AND sMaMT IN (SELECT sMaMT FROM tblMuonTra WHERE sMaMT ='" + userID + "')";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindCTMT, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewCTMT.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            comboBoxSV.ResetText();
            comboBoxNV.ResetText();
            txtMaMT.ResetText();
            dateTimePicker1.ResetText();

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {


            txtMaMT.ResetText();
            dateTimePicker1.ResetText();


        }

        private int kiemtra()//Ktra mã mượn trả tồn tại ở tblMuonTra
        {
            string k = txtMaMT.Text;
            string sql = "SELECT * FROM tblMuonTra WHERE sMaMT ='" + k.ToString() + "'";

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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                if (kiemtra() == 1)
                {
                    MessageBox.Show("Mã mượn trả " + txtMaMT.Text + " đã có, không thể thêm !", "Thông báo");
                    txtMaMT.Focus();
                }
                else
                {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "prThemMuonTra";
                            cmd.Parameters.AddWithValue("@MaMT", txtMaMT.Text);
                            cmd.Parameters.AddWithValue("@MaSV", comboBoxSV.SelectedValue);
                            cmd.Parameters.AddWithValue("@MaNV", comboBoxNV.SelectedValue);
                            cmd.Parameters.AddWithValue("@Ngaymuon", dateTimePicker1.Text);
                            cnn.Open();

                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã thêm thành công phiếu mượn trả mã " + txtMaMT.Text + "!", "Thông báo");
                                layMT();
                            }
                            txtMaMT.ResetText();
                            cnn.Close();
                            rgLoadMT();
                        }                     
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prSuaMuonTra";
                    cmd.Parameters.AddWithValue("@MaMT", txtMaMT.Text);
                    cmd.Parameters.AddWithValue("@MaSV", comboBoxSV.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaNV", comboBoxNV.SelectedValue);
                    cmd.Parameters.AddWithValue("@Ngaymuon", dateTimePicker1.Text);
                    cnn.Open();
                    DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int kq = cmd.ExecuteNonQuery();
                        if (kq > 0)
                            MessageBox.Show("Đã sửa thành công !", "Thông báo");
                        else
                            MessageBox.Show("Sửa không thành công !", "Thông báo");
                        rgLoadMT();
                        cnn.Close();
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prXoaMuonTra";
                    cmd.Parameters.AddWithValue("@MaMT", txtMaMT.Text);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {

                        txtMaMT.ResetText();
                        dateTimePicker1.ResetText(); ;

                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                            {
                                MessageBox.Show("Đã xóa thành công !", "Thông báo");
                                rgLoadCTMT();
                                layMT();
                            }
                            else
                            {
                                MessageBox.Show("Xóa không thành công !", "Thông báo");
                            }

                            cnn.Close();
                        }
                        rgLoadMT();
                    }
                }
            }
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string sqlFindCTMT = "SELECT sMaMT AS [Mã mượn trả],tblSach.sMaSach AS [Mã sách],sTensach AS [Tên sách],dNgaytra AS [Ngày trả] FROM tblSach,tblCTMuonTra WHERE tblSach.sMasach = tblCTMuonTra.sMasach AND sMaMT IN (SELECT sMaMT FROM tblMuonTra WHERE sMaMT LIKE'%" + txtTimkiem.Text + "%')";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindCTMT, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewCTMT.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }

            string sqlFindMT = "SELECT sMaMT AS [Mã mượn trả],tblSinhVien.sMaSV AS [Mã sinh viên],sTenSV AS [Tên sinh viên],tblNhanVien.sMaNV AS [Mã nhân viên],sTenNV AS [Tên nhân viên],dNgaymuon AS [Ngày mượn] FROM tblMuonTra,tblNhanVien,tblSinhVien WHERE tblMuonTra.sMaSV = tblSinhVien.sMaSV AND tblMuonTra.sMaNV = tblNhanVien.sMaNV AND sMaMT LIKE'%" + txtTimkiem.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindMT, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewMT.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        //Phần chi tiết phiếu mượn trả

        private void layMT()
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblMuonTra", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMuonTra");
                        ad.Fill(tb);
                        comboBoxMaMT.DataSource = tb;
                        comboBoxMaMT.DisplayMember = "sTenMT";
                        comboBoxMaMT.ValueMember = "sMaMT";
                    }
                }
            }
        }

        private void layCTMT()
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblSach", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblSach");
                        ad.Fill(tb);
                        comboBoxTenSach.DataSource = tb;
                        comboBoxTenSach.DisplayMember = "sTensach";
                        comboBoxTenSach.ValueMember = "sMasach";
                    }
                }
            }
        }

        private void dataGridViewCTMT_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int numrow;
            numrow = e.RowIndex;
            comboBoxMaMT.Text = dataGridViewCTMT.Rows[numrow].Cells[0].Value.ToString();
            comboBoxTenSach.Text = dataGridViewCTMT.Rows[numrow].Cells[2].Value.ToString();
            //dateTimePicker2.Text= dataGridViewCTMT.Rows[numrow].Cells[3].Value.ToString();

            if(dataGridViewCTMT.Rows[numrow].Cells[3].Value.ToString() == "")
            {
                radioButtonChuatra.Checked = true;
                radioButtonDaTra.Checked = false;
                dateTimePicker2.Enabled = false;
            }
            else if(dataGridViewCTMT.Rows[numrow].Cells[3].Value.ToString() != "")
            {
                radioButtonChuatra.Checked = false;
                radioButtonDaTra.Checked = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.Text = dataGridViewCTMT.Rows[numrow].Cells[3].Value.ToString();
            }
        }

        private int kiemtra1()//ktra tổn tại mã mượn trả trên tblCTMuonTra
        {
            string mamt = Convert.ToString(comboBoxMaMT.SelectedValue);
            string sql = "SELECT * FROM tblCTMuonTra WHERE sMaMT ='" + mamt + "'";

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

        private int kiemtra2()//ktra tên sách tồn tại hay không
        {
            string tensach = Convert.ToString(comboBoxTenSach.SelectedValue);
            string mamt = Convert.ToString(comboBoxMaMT.SelectedValue);
            string sql = "SELECT * FROM tblCTMuonTra,tblSach WHERE tblSach.sMasach = tblCTMuonTra.sMasach AND tblSach.sTensach = N'" + tensach + "'";

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

        public void rgLoadCTMT()
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT sMaMT AS [Mã mượn trả],tblSach.sMasach AS [Mã sách],tblSach.sTensach AS [Tên sách],dNgaytra AS [Ngày trả] FROM tblSach,tblCTMuonTra WHERE tblSach.sMasach = tblCTMuonTra.sMasach", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewCTMT.DataSource = tb;
                    }
                }
            }
        }

        //kiem tra ma sach va ten tac gia cung trung
        private int kiemtramamt_tensach()//ktra trùng mã mượn trả và tên sách
        {
            string mamt = Convert.ToString(comboBoxMaMT.SelectedValue);
            string tensach = Convert.ToString(comboBoxTenSach.SelectedValue);
            string sql = "SELECT * FROM tblCTMuonTra,tblSach WHERE tblCTMuonTra.sMasach = tblSach.sMasach AND sMaMT = '" + mamt + "'AND tblSach.sMasach = '" + tensach + "'";

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

        //kiem tra ma mt trung nhung ten sach khong trung
        private int kiemtramamttrung_tensachkhongtrung()
        {
            string mamt = Convert.ToString(comboBoxMaMT.SelectedValue);
            string tensach = Convert.ToString(comboBoxTenSach.SelectedValue);
            string sql = "SELECT * FROM tblCTMuonTra,tblSach WHERE tblCTMuonTra.sMasach = tblSach.sMasach AND sMaMT = '" + mamt + "'AND tblSach.sMasach != '" + tensach + "'";

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

        private int kiemtrangaytra()
        {
            if (radioButtonDaTra.Checked == true)
            {
                string ngaytra = Convert.ToString(dateTimePicker2.Text);
                string sql = "SELECT * FROM tblMuonTra,tblCTMuonTra WHERE tblMuonTra.sMaMT = tblCTMuonTra.sMaMT AND tblCTMuonTra.sMaMT='"+ comboBoxMaMT.SelectedValue+"' AND dNgaymuon >'" + ngaytra + "'";
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
            else return 0;
            
        }

        private void buttonSave2_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                //kiem tra ma sach va ten tac gia cung trung
                if (kiemtramamt_tensach() == 1)
                {
                    MessageBox.Show("Mã mượn trả " + comboBoxMaMT.Text + ", tên sách " + comboBoxTenSach.Text + " đã có, không thể thêm !", "Thông báo");
                    comboBoxTenSach.Focus();
                    comboBoxMaMT.Focus();
                }
                else
                {
                    //kiem tra ma mt trung nhung ten sach khong trung
                    if (kiemtramamttrung_tensachkhongtrung() == 1)
                    {
                        if (kiemtrangaytra() == 1)
                        {
                            MessageBox.Show("Ngày trả nhỏ hơn ngày mượn, không thể thêm", "Thông báo");
                            dateTimePicker2.Focus();
                        }
                        else
                        {
                            using (SqlCommand cmd = cnn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "prThemCTMT";
                                cmd.Parameters.AddWithValue("@MaMT", comboBoxMaMT.SelectedValue);
                                cmd.Parameters.AddWithValue("@Masach", comboBoxTenSach.SelectedValue);
                                cmd.Parameters.AddWithValue("@Ngaytra", dateTimePicker2.Text);
                                cnn.Open();

                                int kq = cmd.ExecuteNonQuery();
                                if (kq > 0)
                                {
                                    MessageBox.Show("Đã thêm thành công mã mượn trả " + comboBoxMaMT.Text + ", tên sách " + comboBoxTenSach.Text + " !", "Thông báo");
                                }
                                cnn.Close();
                                rgLoadCTMT();
                            }
                                
                           
                        }
                                                   
                    }
                }
            }
        }

        private void buttonAdd2_Click(object sender, EventArgs e)
        {
            //rgLoadCTMT();
            //dateTimePicker2.Enabled = true;
            comboBoxMaMT.ResetText();
            comboBoxTenSach.ResetText();
            dateTimePicker2.ResetText();
        }

        private void txtTimkiem2_TextChanged(object sender, EventArgs e)
        {
            string sqlFindCTMT = "SELECT sMaMT AS [Mã mượn trả],tblSach.sMaSach AS [Mã sách],sTensach AS [Tên sách],dNgaytra AS [Ngày trả] FROM tblSach,tblCTMuonTra WHERE tblSach.sMasach = tblCTMuonTra.sMasach AND sMaMT IN (SELECT sMaMT FROM tblMuonTra WHERE sMaMT LIKE'%" + txtTimkiem2.Text + "%')";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindCTMT, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewCTMT.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }

            string sqlFindMT = "SELECT sMaMT AS [Mã mượn trả],tblSinhVien.sMaSV AS [Mã sinh viên],sTenSV AS [Tên sinh viên],tblNhanVien.sMaNV AS [Mã nhân viên],sTenNV AS [Tên nhân viên],dNgaymuon AS [Ngày mượn] FROM tblMuonTra,tblNhanVien,tblSinhVien WHERE tblMuonTra.sMaSV = tblSinhVien.sMaSV AND tblMuonTra.sMaNV = tblNhanVien.sMaNV AND sMaMT LIKE'%" + txtTimkiem2.Text + "%'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindMT, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewMT.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void buttonUpdate2_Click(object sender, EventArgs e)
        {
            if (radioButtonDaTra.Checked == true && kiemtrangaytra() == 1)
            {
                MessageBox.Show("Ngày trả nhỏ hơn ngày mượn, không thể sửa", "Thông báo");
                dateTimePicker2.Focus();
            }
            else
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prSuaCTMT";
                        cmd.Parameters.AddWithValue("@MaMT", comboBoxMaMT.SelectedValue);
                        cmd.Parameters.AddWithValue("@Masach1", comboBoxTenSach.SelectedValue);
                        cmd.Parameters.AddWithValue("@Masach2", comboBoxTenSach.SelectedValue);
                        //cmd.Parameters.AddWithValue("@Ngaytra", dateTimePicker2.Text);
                        if(radioButtonDaTra.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Ngaytra", dateTimePicker2.Text);
                        }else if(radioButtonChuatra.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Ngaytra",DBNull.Value);//set ngày trả về null
                        }
                        cnn.Open();
                        DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            int kq = cmd.ExecuteNonQuery();
                            if (kq > 0)
                                MessageBox.Show("Đã sửa thành công !", "Thông báo");
                            else
                                MessageBox.Show("Sửa không thành công !", "Thông báo");
                            rgLoadCTMT();
                            cnn.Close();
                        }
                    }
                }
            }


        }

        private void buttonDelete2_Click(object sender, EventArgs e)
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prXoaCTMT";
                    cmd.Parameters.AddWithValue("@MaMT", comboBoxMaMT.SelectedValue);
                    cmd.Parameters.AddWithValue("@Masach", comboBoxTenSach.SelectedValue);
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        /*DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridView1.DataSource = tb;*/

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
                        rgLoadCTMT();
                    }
                }
            }
        }

        //In ds mượn trả
        private void buttonIn_Click(object sender, EventArgs e)
        {
            Truyendulieu.maMT = txtMaMT.Text;

            InphieuMuontra f1 = new InphieuMuontra();
            f1.Show();
        }

        //DANH SÁCH ERROR PROVIDER
        private void comboBoxMaMT_Validating(object sender, CancelEventArgs e)
        {
            if(comboBoxMaMT.Text == "")
            {
                errorProvider1.SetError(comboBoxMaMT, "Bạn phải lựa chọn");
            }else errorProvider1.SetError(comboBoxMaMT, "");
        }

        private void txtMaMT_Validating(object sender, CancelEventArgs e)
        {
            if (txtMaMT.Text == "")
            {
                errorProvider1.SetError(txtMaMT, "Bạn chưa nhập mã phiếu mượn trả !");
            }
            else errorProvider1.SetError(txtMaMT, "");
        }

        private void comboBoxTenSach_Validating(object sender, CancelEventArgs e)
        {
            if(comboBoxTenSach.Text == "")
            {
                errorProvider1.SetError(comboBoxTenSach, "Bạn phải lựa chọn");
            }else errorProvider1.SetError(comboBoxTenSach, "");
        }

        private void radioButtonDaTra_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonDaTra.Checked == true)
            {
                dateTimePicker2.Enabled = true;
            }
        }

        private void radioButtonChuatra_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonChuatra.Checked == true)
            {
                dateTimePicker2.Enabled = false;
            }
        }

        private void comboBoxSV_Validating(object sender, CancelEventArgs e)
        {
            if (comboBoxTenSach.Text == "")
            {
                errorProvider1.SetError(comboBoxSV,"Bạn phải lựa chọn");
            }
            else errorProvider1.SetError(comboBoxSV, "");
        }

        private void comboBoxNV_Validating(object sender, CancelEventArgs e)
        {
            if (comboBoxTenSach.Text == "")
            {
                errorProvider1.SetError(comboBoxNV, "Bạn phải lựa chọn");
            }
            else errorProvider1.SetError(comboBoxNV, "");
        }
    }

}

