using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BTL_Nhom14
{
    public partial class InphieuMuontra : Form
    {
        public InphieuMuontra()
        {
            InitializeComponent();
        }
        private void InDSPM()
        {
            string str = ConfigurationManager.ConnectionStrings["db_qltv"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(str))
            {
                string sql = "SELECT tblMuonTra.sMaMT, tblMuonTra.sMaSV, tblSinhVien.sTenSV, tblSinhVien.sSDT, tblCTMuonTra.sMasach, tblSach.sTensach, tblCTMuonTra.dNgaytra" +
                            " FROM tblMuonTra INNER JOIN" +
                         " tblSinhVien ON tblMuonTra.sMaSV = tblSinhVien.sMaSV INNER JOIN" +
                         " tblCTMuonTra ON tblMuonTra.sMaMT = tblCTMuonTra.sMaMT INNER JOIN" +
                         " tblSach ON tblCTMuonTra.sMasach = tblSach.sMasach"+
                         " where tblMuonTra.sMaMT='"+Truyendulieu.maMT+"'";

                SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
                DataSetPhieuMuon ds = new DataSetPhieuMuon();
                DataTable dt = new DataTable();
                da.Fill(ds);
                CrystalReportPhieuMuonTra objrpt = new CrystalReportPhieuMuonTra();
                objrpt.SetDataSource(ds.Tables[1]);
                crystalReportViewer1.ReportSource = objrpt;
                crystalReportViewer1.Refresh();
            }

        }

        private void InphieuMuontra_Load(object sender, EventArgs e)
        {
            InDSPM();
        }
    }
    }

