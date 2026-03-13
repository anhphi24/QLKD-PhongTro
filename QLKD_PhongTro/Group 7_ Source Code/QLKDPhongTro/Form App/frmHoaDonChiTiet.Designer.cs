namespace QLKDPhongTro
{
    partial class frmHoaDonChiTiet
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
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnHuyBo = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtGhiChuTK = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dgvChiTietHoaDon = new System.Windows.Forms.DataGridView();
            this.btnChonAnhQR = new System.Windows.Forms.Button();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtToaNha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSoPhongTro = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblThangNamHD = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLuuGhiChu = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.BackColor = System.Drawing.Color.Red;
            this.btnInHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInHoaDon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnInHoaDon.Location = new System.Drawing.Point(495, 812);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(104, 32);
            this.btnInHoaDon.TabIndex = 21;
            this.btnInHoaDon.Text = "In hóa đơn";
            this.btnInHoaDon.UseVisualStyleBackColor = false;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.BackColor = System.Drawing.Color.White;
            this.btnHuyBo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuyBo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyBo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuyBo.ForeColor = System.Drawing.Color.Black;
            this.btnHuyBo.Location = new System.Drawing.Point(620, 812);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(104, 32);
            this.btnHuyBo.TabIndex = 21;
            this.btnHuyBo.Text = "Hủy bỏ";
            this.btnHuyBo.UseVisualStyleBackColor = false;
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGhiChu.Location = new System.Drawing.Point(122, 665);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(582, 127);
            this.txtGhiChu.TabIndex = 22;
            this.txtGhiChu.Text = "+ Vui lòng thanh toán từ ngày 1 ngày mùng 5 hàng tháng.\r\n+ Khoản thanh toán sau t" +
    "hời gian quy định là nộp chậm và tiền phạt nộp chậm được tính 100.000/1 ngày. (N" +
    "hấp vô để sửa)\r\n\r\n\r\n\r\n";
            this.txtGhiChu.TextChanged += new System.EventHandler(this.txtGhiChu_TextChanged);
            // 
            // txtGhiChuTK
            // 
            this.txtGhiChuTK.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGhiChuTK.Location = new System.Drawing.Point(164, 231);
            this.txtGhiChuTK.Multiline = true;
            this.txtGhiChuTK.Name = "txtGhiChuTK";
            this.txtGhiChuTK.Size = new System.Drawing.Size(522, 149);
            this.txtGhiChuTK.TabIndex = 22;
            this.txtGhiChuTK.Text = "Thông tin tài khoản nhận tiền: \r\nNgân Hàng: MB Bank\r\nSTK: 123456789\r\nChủ tài khoả" +
    "n: NGUYEN VAN VIET\r\n\r\n\r\n";
            this.txtGhiChuTK.TextChanged += new System.EventHandler(this.txtGhiChuTK_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.dgvChiTietHoaDon);
            this.panel1.Controls.Add(this.txtGhiChuTK);
            this.panel1.Controls.Add(this.btnChonAnhQR);
            this.panel1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.panel1.Location = new System.Drawing.Point(17, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 389);
            this.panel1.TabIndex = 23;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox2.Location = new System.Drawing.Point(569, 205);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 27;
            this.textBox2.Text = "Đơn vị: VND";
            // 
            // dgvChiTietHoaDon
            // 
            this.dgvChiTietHoaDon.AllowUserToResizeRows = false;
            this.dgvChiTietHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietHoaDon.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvChiTietHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHoaDon.Location = new System.Drawing.Point(-1, -1);
            this.dgvChiTietHoaDon.Name = "dgvChiTietHoaDon";
            this.dgvChiTietHoaDon.Size = new System.Drawing.Size(687, 200);
            this.dgvChiTietHoaDon.TabIndex = 25;
            this.dgvChiTietHoaDon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietHoaDon_CellContentClick);
            // 
            // btnChonAnhQR
            // 
            this.btnChonAnhQR.BackColor = System.Drawing.Color.White;
            this.btnChonAnhQR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonAnhQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonAnhQR.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnChonAnhQR.ForeColor = System.Drawing.Color.Black;
            this.btnChonAnhQR.Location = new System.Drawing.Point(2, 231);
            this.btnChonAnhQR.Name = "btnChonAnhQR";
            this.btnChonAnhQR.Size = new System.Drawing.Size(156, 149);
            this.btnChonAnhQR.TabIndex = 21;
            this.btnChonAnhQR.UseVisualStyleBackColor = false;
            this.btnChonAnhQR.Click += new System.EventHandler(this.btnChonAnhQR_Click_1);
            // 
            // lblSoPhong
            // 
            this.lblSoPhong.AutoSize = true;
            this.lblSoPhong.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoPhong.Location = new System.Drawing.Point(19, 94);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.Size = new System.Drawing.Size(0, 20);
            this.lblSoPhong.TabIndex = 24;
            // 
            // txtSDT
            // 
            this.txtSDT.BackColor = System.Drawing.Color.White;
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSDT.ForeColor = System.Drawing.Color.Black;
            this.txtSDT.Location = new System.Drawing.Point(79, 174);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(625, 22);
            this.txtSDT.TabIndex = 27;
            // 
            // txtToaNha
            // 
            this.txtToaNha.BackColor = System.Drawing.Color.White;
            this.txtToaNha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtToaNha.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtToaNha.ForeColor = System.Drawing.Color.Black;
            this.txtToaNha.Location = new System.Drawing.Point(103, 94);
            this.txtToaNha.Name = "txtToaNha";
            this.txtToaNha.Size = new System.Drawing.Size(601, 20);
            this.txtToaNha.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(411, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Ngày xuất hóa đơn:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dateTimePicker1.Location = new System.Drawing.Point(562, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(125, 27);
            this.dateTimePicker1.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(55, 671);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "Ghi chú:";
            // 
            // txtSoPhongTro
            // 
            this.txtSoPhongTro.BackColor = System.Drawing.Color.White;
            this.txtSoPhongTro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSoPhongTro.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoPhongTro.ForeColor = System.Drawing.Color.Black;
            this.txtSoPhongTro.Location = new System.Drawing.Point(117, 120);
            this.txtSoPhongTro.Name = "txtSoPhongTro";
            this.txtSoPhongTro.Size = new System.Drawing.Size(587, 20);
            this.txtSoPhongTro.TabIndex = 32;
            // 
            // txtHoTen
            // 
            this.txtHoTen.BackColor = System.Drawing.Color.White;
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtHoTen.ForeColor = System.Drawing.Color.Black;
            this.txtHoTen.Location = new System.Drawing.Point(135, 146);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(569, 22);
            this.txtHoTen.TabIndex = 27;
            // 
            // lblThangNamHD
            // 
            this.lblThangNamHD.BackColor = System.Drawing.Color.White;
            this.lblThangNamHD.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblThangNamHD.ForeColor = System.Drawing.Color.Black;
            this.lblThangNamHD.Location = new System.Drawing.Point(0, 9);
            this.lblThangNamHD.Name = "lblThangNamHD";
            this.lblThangNamHD.Size = new System.Drawing.Size(724, 34);
            this.lblThangNamHD.TabIndex = 31;
            this.lblThangNamHD.Text = "HÓA ĐƠN THANH TOÁN TIỀN PHÒNG";
            this.lblThangNamHD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(37, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "SĐT:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(37, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 34;
            this.label4.Text = "Khách thuê:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(37, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "Phòng số:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(37, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 34;
            this.label5.Text = "Địa chỉ:";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.BackColor = System.Drawing.Color.White;
            this.txtTrangThai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTrangThai.ForeColor = System.Drawing.Color.Black;
            this.txtTrangThai.Location = new System.Drawing.Point(122, 202);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(334, 20);
            this.txtTrangThai.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(37, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 33;
            this.label7.Text = "Trạng thái:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // btnLuuGhiChu
            // 
            this.btnLuuGhiChu.Location = new System.Drawing.Point(414, 818);
            this.btnLuuGhiChu.Name = "btnLuuGhiChu";
            this.btnLuuGhiChu.Size = new System.Drawing.Size(75, 23);
            this.btnLuuGhiChu.TabIndex = 28;
            this.btnLuuGhiChu.UseVisualStyleBackColor = true;
            this.btnLuuGhiChu.Click += new System.EventHandler(this.btnLuuGhiChu_Click);
            // 
            // frmHoaDonChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(725, 867);
            this.Controls.Add(this.btnLuuGhiChu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.txtSoPhongTro);
            this.Controls.Add(this.lblThangNamHD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.txtToaNha);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblSoPhong);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnHuyBo);
            this.Controls.Add(this.btnInHoaDon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHoaDonChiTiet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.HoaDonChiTiet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtGhiChuTK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnChonAnhQR;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtToaNha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSoPhongTro;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.DataGridView dgvChiTietHoaDon;
        private System.Windows.Forms.Label lblThangNamHD;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLuuGhiChu;
    }
}