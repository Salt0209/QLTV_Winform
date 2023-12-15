
namespace BTL_Nhom14
{
    partial class inTopSachDuocMuon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbTheLoaiSach = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChon = new System.Windows.Forms.Button();
            this.cryRptThongKeSLSachMuon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cbTheLoaiSach
            // 
            this.cbTheLoaiSach.FormattingEnabled = true;
            this.cbTheLoaiSach.Location = new System.Drawing.Point(120, 21);
            this.cbTheLoaiSach.Name = "cbTheLoaiSach";
            this.cbTheLoaiSach.Size = new System.Drawing.Size(121, 21);
            this.cbTheLoaiSach.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Chọn loại sách";
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(280, 19);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(75, 23);
            this.btnChon.TabIndex = 5;
            this.btnChon.Text = "Chọn";
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // cryRptThongKeSLSachMuon
            // 
            this.cryRptThongKeSLSachMuon.ActiveViewIndex = -1;
            this.cryRptThongKeSLSachMuon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cryRptThongKeSLSachMuon.Cursor = System.Windows.Forms.Cursors.Default;
            this.cryRptThongKeSLSachMuon.Location = new System.Drawing.Point(8, 66);
            this.cryRptThongKeSLSachMuon.Name = "cryRptThongKeSLSachMuon";
            this.cryRptThongKeSLSachMuon.Size = new System.Drawing.Size(784, 365);
            this.cryRptThongKeSLSachMuon.TabIndex = 4;
            // 
            // inTopSachDuocMuon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbTheLoaiSach);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.cryRptThongKeSLSachMuon);
            this.Name = "inTopSachDuocMuon";
            this.Text = "inTopSachDuocMuon";
            this.Load += new System.EventHandler(this.inTopSachDuocMuon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTheLoaiSach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChon;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer cryRptThongKeSLSachMuon;
    }
}