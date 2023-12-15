
namespace BTL_Nhom14
{
    partial class formMuonTra
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
            this.components = new System.ComponentModel.Container();
            this.buttonFind = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonIn = new System.Windows.Forms.Button();
            this.txtMaMT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTimkiem = new System.Windows.Forms.TextBox();
            this.txtTimkiem2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonFind2 = new System.Windows.Forms.Button();
            this.buttonSave2 = new System.Windows.Forms.Button();
            this.buttonDelete2 = new System.Windows.Forms.Button();
            this.buttonUpdate2 = new System.Windows.Forms.Button();
            this.buttonAdd2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridViewCTMT = new System.Windows.Forms.DataGridView();
            this.dataGridViewMT = new System.Windows.Forms.DataGridView();
            this.comboBoxSV = new System.Windows.Forms.ComboBox();
            this.comboBoxNV = new System.Windows.Forms.ComboBox();
            this.comboBoxMaMT = new System.Windows.Forms.ComboBox();
            this.comboBoxTenSach = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.radioButtonDaTra = new System.Windows.Forms.RadioButton();
            this.radioButtonChuatra = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCTMT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(320, 200);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 22);
            this.buttonFind.TabIndex = 20;
            this.buttonFind.Text = "Tìm kiếm";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(320, 99);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.Text = "Lưu";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(320, 161);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 18;
            this.buttonDelete.Text = "Xóa";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(320, 127);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 17;
            this.buttonUpdate.Text = "Sửa";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(320, 69);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 16;
            this.buttonAdd.Text = "Thêm";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(63, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 21);
            this.label8.TabIndex = 15;
            this.label8.Text = "PHIẾU MƯỢN";
            // 
            // buttonIn
            // 
            this.buttonIn.Location = new System.Drawing.Point(320, 12);
            this.buttonIn.Name = "buttonIn";
            this.buttonIn.Size = new System.Drawing.Size(75, 45);
            this.buttonIn.TabIndex = 31;
            this.buttonIn.Text = "In";
            this.buttonIn.UseVisualStyleBackColor = true;
            this.buttonIn.Click += new System.EventHandler(this.buttonIn_Click);
            // 
            // txtMaMT
            // 
            this.txtMaMT.Location = new System.Drawing.Point(122, 71);
            this.txtMaMT.Name = "txtMaMT";
            this.txtMaMT.Size = new System.Drawing.Size(169, 20);
            this.txtMaMT.TabIndex = 37;
            this.txtMaMT.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaMT_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Ngày mượn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Tên nhân viên";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Tên người mượn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Mã phiếu";
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.Location = new System.Drawing.Point(122, 200);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Size = new System.Drawing.Size(169, 20);
            this.txtTimkiem.TabIndex = 44;
            // 
            // txtTimkiem2
            // 
            this.txtTimkiem2.Location = new System.Drawing.Point(514, 413);
            this.txtTimkiem2.Name = "txtTimkiem2";
            this.txtTimkiem2.Size = new System.Drawing.Size(169, 20);
            this.txtTimkiem2.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 372);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Ngày trả";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Tên sách";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 284);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "Mã phiếu";
            // 
            // buttonFind2
            // 
            this.buttonFind2.Location = new System.Drawing.Point(712, 413);
            this.buttonFind2.Name = "buttonFind2";
            this.buttonFind2.Size = new System.Drawing.Size(75, 22);
            this.buttonFind2.TabIndex = 50;
            this.buttonFind2.Text = "Tìm kiếm";
            this.buttonFind2.UseVisualStyleBackColor = true;
            // 
            // buttonSave2
            // 
            this.buttonSave2.Location = new System.Drawing.Point(712, 312);
            this.buttonSave2.Name = "buttonSave2";
            this.buttonSave2.Size = new System.Drawing.Size(75, 23);
            this.buttonSave2.TabIndex = 49;
            this.buttonSave2.Text = "Lưu";
            this.buttonSave2.UseVisualStyleBackColor = true;
            this.buttonSave2.Click += new System.EventHandler(this.buttonSave2_Click);
            // 
            // buttonDelete2
            // 
            this.buttonDelete2.Location = new System.Drawing.Point(712, 374);
            this.buttonDelete2.Name = "buttonDelete2";
            this.buttonDelete2.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete2.TabIndex = 48;
            this.buttonDelete2.Text = "Xóa";
            this.buttonDelete2.UseVisualStyleBackColor = true;
            this.buttonDelete2.Click += new System.EventHandler(this.buttonDelete2_Click);
            // 
            // buttonUpdate2
            // 
            this.buttonUpdate2.Location = new System.Drawing.Point(712, 340);
            this.buttonUpdate2.Name = "buttonUpdate2";
            this.buttonUpdate2.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate2.TabIndex = 47;
            this.buttonUpdate2.Text = "Sửa";
            this.buttonUpdate2.UseVisualStyleBackColor = true;
            this.buttonUpdate2.Click += new System.EventHandler(this.buttonUpdate2_Click);
            // 
            // buttonAdd2
            // 
            this.buttonAdd2.Location = new System.Drawing.Point(712, 282);
            this.buttonAdd2.Name = "buttonAdd2";
            this.buttonAdd2.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd2.TabIndex = 46;
            this.buttonAdd2.Text = "Thêm";
            this.buttonAdd2.UseVisualStyleBackColor = true;
            this.buttonAdd2.Click += new System.EventHandler(this.buttonAdd2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(455, 246);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 21);
            this.label10.TabIndex = 45;
            this.label10.Text = "CHI TIẾT PHIẾU";
            // 
            // dataGridViewCTMT
            // 
            this.dataGridViewCTMT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCTMT.Location = new System.Drawing.Point(416, 34);
            this.dataGridViewCTMT.Name = "dataGridViewCTMT";
            this.dataGridViewCTMT.Size = new System.Drawing.Size(371, 186);
            this.dataGridViewCTMT.TabIndex = 61;
            this.dataGridViewCTMT.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCTMT_CellClick);
            // 
            // dataGridViewMT
            // 
            this.dataGridViewMT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMT.Location = new System.Drawing.Point(24, 246);
            this.dataGridViewMT.Name = "dataGridViewMT";
            this.dataGridViewMT.Size = new System.Drawing.Size(371, 187);
            this.dataGridViewMT.TabIndex = 62;
            this.dataGridViewMT.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMT_CellClick);
            // 
            // comboBoxSV
            // 
            this.comboBoxSV.FormattingEnabled = true;
            this.comboBoxSV.Location = new System.Drawing.Point(122, 101);
            this.comboBoxSV.Name = "comboBoxSV";
            this.comboBoxSV.Size = new System.Drawing.Size(169, 21);
            this.comboBoxSV.TabIndex = 64;
            this.comboBoxSV.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxSV_Validating);
            // 
            // comboBoxNV
            // 
            this.comboBoxNV.FormattingEnabled = true;
            this.comboBoxNV.Location = new System.Drawing.Point(122, 129);
            this.comboBoxNV.Name = "comboBoxNV";
            this.comboBoxNV.Size = new System.Drawing.Size(169, 21);
            this.comboBoxNV.TabIndex = 65;
            this.comboBoxNV.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxNV_Validating);
            // 
            // comboBoxMaMT
            // 
            this.comboBoxMaMT.FormattingEnabled = true;
            this.comboBoxMaMT.Location = new System.Drawing.Point(514, 281);
            this.comboBoxMaMT.Name = "comboBoxMaMT";
            this.comboBoxMaMT.Size = new System.Drawing.Size(169, 21);
            this.comboBoxMaMT.TabIndex = 66;
            this.comboBoxMaMT.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMaMT_Validating);
            // 
            // comboBoxTenSach
            // 
            this.comboBoxTenSach.FormattingEnabled = true;
            this.comboBoxTenSach.Location = new System.Drawing.Point(514, 328);
            this.comboBoxTenSach.Name = "comboBoxTenSach";
            this.comboBoxTenSach.Size = new System.Drawing.Size(169, 21);
            this.comboBoxTenSach.TabIndex = 67;
            this.comboBoxTenSach.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxTenSach_Validating);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(122, 160);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(169, 20);
            this.dateTimePicker1.TabIndex = 68;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(514, 387);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(169, 20);
            this.dateTimePicker2.TabIndex = 71;
            // 
            // radioButtonDaTra
            // 
            this.radioButtonDaTra.AutoSize = true;
            this.radioButtonDaTra.Location = new System.Drawing.Point(514, 367);
            this.radioButtonDaTra.Name = "radioButtonDaTra";
            this.radioButtonDaTra.Size = new System.Drawing.Size(54, 17);
            this.radioButtonDaTra.TabIndex = 73;
            this.radioButtonDaTra.TabStop = true;
            this.radioButtonDaTra.Text = "Đã trả";
            this.radioButtonDaTra.UseVisualStyleBackColor = true;
            this.radioButtonDaTra.CheckedChanged += new System.EventHandler(this.radioButtonDaTra_CheckedChanged);
            // 
            // radioButtonChuatra
            // 
            this.radioButtonChuatra.AutoSize = true;
            this.radioButtonChuatra.Location = new System.Drawing.Point(598, 367);
            this.radioButtonChuatra.Name = "radioButtonChuatra";
            this.radioButtonChuatra.Size = new System.Drawing.Size(65, 17);
            this.radioButtonChuatra.TabIndex = 74;
            this.radioButtonChuatra.TabStop = true;
            this.radioButtonChuatra.Text = "Chưa trả";
            this.radioButtonChuatra.UseVisualStyleBackColor = true;
            this.radioButtonChuatra.CheckedChanged += new System.EventHandler(this.radioButtonChuatra_CheckedChanged);
            // 
            // formMuonTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.radioButtonChuatra);
            this.Controls.Add(this.radioButtonDaTra);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBoxTenSach);
            this.Controls.Add(this.comboBoxMaMT);
            this.Controls.Add(this.comboBoxNV);
            this.Controls.Add(this.comboBoxSV);
            this.Controls.Add(this.dataGridViewMT);
            this.Controls.Add(this.dataGridViewCTMT);
            this.Controls.Add(this.txtTimkiem2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonFind2);
            this.Controls.Add(this.buttonSave2);
            this.Controls.Add(this.buttonDelete2);
            this.Controls.Add(this.buttonUpdate2);
            this.Controls.Add(this.buttonAdd2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTimkiem);
            this.Controls.Add(this.txtMaMT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonIn);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.label8);
            this.Name = "formMuonTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formMuonTra";
            this.Load += new System.EventHandler(this.muontra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCTMT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonIn;
        private System.Windows.Forms.TextBox txtMaMT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTimkiem;
        private System.Windows.Forms.TextBox txtTimkiem2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonFind2;
        private System.Windows.Forms.Button buttonSave2;
        private System.Windows.Forms.Button buttonDelete2;
        private System.Windows.Forms.Button buttonUpdate2;
        private System.Windows.Forms.Button buttonAdd2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridViewCTMT;
        private System.Windows.Forms.DataGridView dataGridViewMT;
        private System.Windows.Forms.ComboBox comboBoxSV;
        private System.Windows.Forms.ComboBox comboBoxNV;
        private System.Windows.Forms.ComboBox comboBoxMaMT;
        private System.Windows.Forms.ComboBox comboBoxTenSach;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.RadioButton radioButtonChuatra;
        private System.Windows.Forms.RadioButton radioButtonDaTra;
    }
}