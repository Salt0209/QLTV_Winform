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
    public partial class inTopSachDuocMuon : Form
    {
        public inTopSachDuocMuon()
        {
            InitializeComponent();
        }

        private void inTopSachDuocMuon_Load(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            layDS_TheloaiSach();

            using (SqlConnection cnn = new SqlConnection(str))
            {
                string sql = "select TOP 5 tblSach.sTensach, COUNT(tblMuonTra.sMaMT) AS[So_luot_duoc_muon]" +
                    " from tblCTSach inner join tblSach on tblCTSach.sMasach = tblSach.sMasach" +
                    " inner join tblCTMuonTra on tblCTMuonTra.sMasach = tblSach.sMasach" +
                    " inner join tblMuonTra on tblMuonTra.sMaMT = tblCTMuonTra.sMaMT" +

                    " group by tblSach.sTensach order by COUNT(tblMuonTra.sMaMT) desc";

                SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CrystalReportTopSachDuocMuon objrpt = new CrystalReportTopSachDuocMuon();
                objrpt.SetDataSource(dt);
                cryRptThongKeSLSachMuon.ReportSource = objrpt;
                cryRptThongKeSLSachMuon.Refresh();
            }
        }
        private void layDS_TheloaiSach()
        {
            string str = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from tblTheLoai", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable t = new DataTable();
                        DataView v = new DataView(t);
                        ad.Fill(t);
                        cbTheLoaiSach.DataSource = t;
                        cbTheLoaiSach.DisplayMember = "sTenloai";
                        cbTheLoaiSach.ValueMember = "sMaloai";
                    }
                }
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ThongKeSLSachDuocMuon";
                    cmd.Parameters.AddWithValue("@theloai", cbTheLoaiSach.SelectedValue.ToString());
                    //cmd.Parameters.Add("@theloai", SqlDbType.NVarChar).Value = cbTheLoaiSach.SelectedValue.ToString();

                    using (SqlDataAdapter ad = new SqlDataAdapter())
                    {
                        ad.SelectCommand = cmd;
                        DataTable tb = new System.Data.DataTable();
                        ad.Fill(tb);
                        CrystalReportTopSachDuocMuon rpt = new CrystalReportTopSachDuocMuon();
                        rpt.SetDataSource(tb);
                        cryRptThongKeSLSachMuon.ReportSource = rpt;
                        cryRptThongKeSLSachMuon.Refresh();
                    }
                }
            }
        }

    }
}
