using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;



namespace QLKDPhongTro
{
    public partial class frmFormApp : Form
    {
        public frmFormApp()
        {
            InitializeComponent();
        }
        private void FormApp_Load(object sender, EventArgs e)
        {
            btnPhongTro_Click(sender, e);
        }

        // Hàm kết nối database chung
        private DataTable KetnoiDatabase(string query, SqlParameter[] parameters = null)
        {
            string connectionString = "Server=DESKTOP-923DSF9\\SQLEXPRESS;Database=QLKDPhongTro;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                if (parameters != null)
                {
                    dataAdapter.SelectCommand.Parameters.AddRange(parameters);
                }
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
        }

        // Phương thức thực thi câu lệnh SQL với SqlParameter
        private int ThucThiLenhSQL(string query, SqlParameter[] parameters = null)
        {
            string connectionString = "Server=DESKTOP-923DSF9\\SQLEXPRESS;Database=QLKDPhongTro;Trusted_Connection=True;";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Hiển thị phòng trọ
        private void btnPhongTro_Click(object sender, EventArgs e)
        {
            pnDienNuoc.Visible = false;
            pnPhongTro.Visible = true;
            pnKhachThue.Visible = false;
            pnHopDong.Visible = false;
            pnHoaDon.Visible = false;
            pnThongKe.Visible = false;

            dgvPhongTro.DataSource = HienThiBangPhongTro();

            dgvPhongTro.Columns["MaPhongTro"].HeaderText = "Mã Phòng Trọ";
            dgvPhongTro.Columns["ToaNha"].HeaderText = "Tòa Nhà";
            dgvPhongTro.Columns["Tang"].HeaderText = "Tầng";
            dgvPhongTro.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvPhongTro.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            dgvPhongTro.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvPhongTro.Columns["MoTa"].HeaderText = "Mô Tả";
            dgvPhongTro.Columns["DanhSachKhachThue"].HeaderText = "Tên Thành Viên";

            clearPhongTro();
        }

        // Hiển thị khách thuê
        private void btnKhachThue_Click(object sender, EventArgs e)
        {
            pnDienNuoc.Visible = false;
            pnPhongTro.Visible = false;
            pnKhachThue.Visible = true;
            pnHopDong.Visible = false;
            pnHoaDon.Visible = false;
            pnThongKe.Visible = false;

            dgvKhachThue.DataSource = HienThiBangKhachHang();

            dgvKhachThue.Columns["MaPhongTro"].HeaderText = "Mã Phòng";
            dgvKhachThue.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvKhachThue.Columns["QueQuan"].HeaderText = "Quê Quán";
            dgvKhachThue.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvKhachThue.Columns["CCCD"].HeaderText = "CMND/CCCD";
            dgvKhachThue.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvKhachThue.Columns["GioiTinh"].HeaderText = "Giới Tính";
            CbbMaPhongTroKT();
            ClearKH();
        }

        // Hiển thị hợp đồng
        private void btnHopDong_Click_1(object sender, EventArgs e)
        {
            pnDienNuoc.Visible = false;
            pnPhongTro.Visible = false;
            pnKhachThue.Visible = false;
            pnHopDong.Visible = true;
            pnHoaDon.Visible = false;
            pnThongKe.Visible = false;

            dgvHopDong.DataSource = HienthiBangHopDong();

            dgvHopDong.Columns["MaHopDong"].HeaderText = "Mã Hợp Đồng";
            dgvHopDong.Columns["HoTenNguoiKy"].HeaderText = "Tên Khách";
            dgvHopDong.Columns["TienCoc"].HeaderText = "Tiền Cọc";
            dgvHopDong.Columns["NgayKy"].HeaderText = "Ngày Ký";
            dgvHopDong.Columns["NgayHetHan"].HeaderText = "Ngày Hết Hạn";
            dgvHopDong.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvHopDong.Columns["GhiChu"].HeaderText = "Ghi Chú";
            DayTenKhachLenCBB();
            ClearHD();
        }

        // Hiển thị điện nước
        private void btnDIenNuoc_Click(object sender, EventArgs e)
        {
            pnDienNuoc.Visible = true;
            pnPhongTro.Visible = false;
            pnKhachThue.Visible = false;
            pnHopDong.Visible = false;
            pnHoaDon.Visible = false;
            pnThongKe.Visible = false;

            DateTime thangNam = dtpThangNamDN.Value;

            dgvDienNuoc.DataSource = HienThiBangDien(thangNam);
            dgvDienNuoc.Columns["MaPhongTro"].HeaderText = "Mã Phòng";
            dgvDienNuoc.Columns["SoDienCu"].HeaderText = "Chỉ số cũ";
            dgvDienNuoc.Columns["SoDienMoi"].HeaderText = "Chỉ số mới";
            dgvDienNuoc.Columns["DonGiaDien"].HeaderText = "Đơn Giá";
            dgvDienNuoc.Columns["TieuThuDien"].HeaderText = "Sử dụng";
            dgvDienNuoc.Columns["TienDien"].HeaderText = "Tiền Điện";
            dgvDienNuoc.Columns["SoDienCu"].DefaultCellStyle.Format = "N0";
            dgvDienNuoc.Columns["SoDienMoi"].DefaultCellStyle.Format = "N0";
            dgvDienNuoc.Columns["DonGiaDien"].DefaultCellStyle.Format = "N0";
            dgvDienNuoc.Columns["TieuThuDien"].DefaultCellStyle.Format = "N0";
            dgvDienNuoc.Columns["TienDien"].DefaultCellStyle.Format = "N0";

            dgvNuoc.DataSource = HienThiBangNuoc(thangNam);
            dgvNuoc.Columns["MaPhongTro"].HeaderText = "Mã Phòng";
            dgvNuoc.Columns["SoNuocCu"].HeaderText = "Chỉ số cũ";
            dgvNuoc.Columns["SoNuocMoi"].HeaderText = "Chỉ số mới";
            dgvNuoc.Columns["DonGiaNuoc"].HeaderText = "Đơn Giá";
            dgvNuoc.Columns["TieuThuNuoc"].HeaderText = "Sử dụng";
            dgvNuoc.Columns["TienNuoc"].HeaderText = "Tiền Nước";
            dgvNuoc.Columns["SoNuocCu"].DefaultCellStyle.Format = "N0";
            dgvNuoc.Columns["SoNuocMoi"].DefaultCellStyle.Format = "N0";
            dgvNuoc.Columns["DonGiaNuoc"].DefaultCellStyle.Format = "N0";
            dgvNuoc.Columns["TieuThuNuoc"].DefaultCellStyle.Format = "N0";
            dgvNuoc.Columns["TienNuoc"].DefaultCellStyle.Format = "N0";

            CapNhatDonGiaDienNuocThangTiepTheoTuDong();
            clearDienNuoc();


        }

        // Hiển thị hóa đơn
        private void HtnHoaDon_Click(object sender, EventArgs e)
        {
            pnPhongTro.Visible = false;
            pnKhachThue.Visible = false;
            pnHopDong.Visible = false;
            pnHoaDon.Visible = true;
            pnDienNuoc.Visible = false;
            pnThongKe.Visible = false;

            DateTime thangNam = dtpThangNamHD.Value;
            dgvHoaDon.DataSource = HienThiBangHoaDon(thangNam);

            dgvHoaDon.Columns["MaPhongTro"].HeaderText = "Mã Phòng";
            dgvHoaDon.Columns["TienPhong"].HeaderText = "Tiền Phòng";
            dgvHoaDon.Columns["TienDien"].HeaderText = "Tiền Điện";
            dgvHoaDon.Columns["TienNuoc"].HeaderText = "Tiền Nước";
            dgvHoaDon.Columns["TienVeSinh"].HeaderText = "Tiền Vệ Sinh";
            dgvHoaDon.Columns["Internet"].HeaderText = "Internet";
            dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền";
            dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvHoaDon.Columns["DichVuKhac"].HeaderText = "Dịch Vụ Khác";
            dgvHoaDon.Columns["GhiChu"].HeaderText = "Ghi Chú";
            dgvHoaDon.Columns["KhuyenMai"].HeaderText = "Khuyến Mãi";

            dgvHoaDon.Columns["MaPhongTro"].Width = 80;
            dgvHoaDon.Columns["TienPhong"].Width = 100;

            clearHoaDon();
        }

        //btn thống kê
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            pnDienNuoc.Visible = false;
            pnPhongTro.Visible = false;
            pnKhachThue.Visible = false;
            pnHopDong.Visible = false;
            pnHoaDon.Visible = false;
            pnThongKe.Visible = true;

            DemSoLuongKhach();
            DemSoLuongPhongTro();
            DemSoLuongHopDong();
            HienThiDoanhThu();
        }

        //Hàm đổi mật khẩu
        private void btnCaiDatTK_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is frmDoiMK)
                {
                    form.BringToFront();
                    return;
                }
            }
            frmDoiMK doiMK = new frmDoiMK();
            doiMK.ShowDialog();
        }

        //Lệnh thoát chương trinh, mở form
        private void FormApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?",
                                                    "Xác nhận thoát",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //btn Đăng xuất
        private void btnDangXuat_Click_1(object sender, EventArgs e)
        {
            frmDangNhap dangNhap = new frmDangNhap();
            dangNhap.Show();

            this.Hide();
        }

        //Link Gop y
        private void linkGopY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://forms.gle/xiAVUM1TEVvCA3Eg6",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở liên kết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///////CODE PHÒNG TRỌ        CODE PHÒNG TRỌ       CODE PHÒNG TRỌ        CODE PHÒNG TRỌ        CODE PHÒNG TRỌ         CODE PHÒNG TRỌ       CODE PHÒNG TRỌ     CODE PHÒNG TRỌ
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Hiển thị bảng phòng trọ
        private DataTable HienThiBangPhongTro()
        {
            string keyword = txtTimKiemPT.Text.Trim();

            string query = @"
SELECT  
    p.MaPhongTro,
    p.ToaNha,
    p.Tang,
    p.SoPhong,
    FORMAT(p.GiaPhong, 'N0') AS GiaPhong,
    CASE 
        WHEN COUNT(k.MaKhachThue) > 0 THEN N'Đang cho thuê' 
        ELSE N'Trống' 
    END AS TrangThai,
    STRING_AGG(k.HoTen, ', ') AS DanhSachKhachThue,
    p.MoTa
FROM 
    tblPhongTro p
LEFT JOIN 
    tblKhachThue k ON p.MaPhongTro = k.MaPhongTro
GROUP BY 
    p.MaPhongTro, p.ToaNha, p.Tang, p.SoPhong, p.GiaPhong, p.MoTa
HAVING 
    p.MaPhongTro LIKE '%' + @Keyword + '%'
    OR p.MoTa LIKE '%' + @Keyword + '%'
    OR p.SoPhong LIKE '%' + @Keyword + '%'
    OR p.Tang LIKE '%' + @Keyword + '%'
    OR 
    (CASE 
        WHEN COUNT(k.MaKhachThue) > 0 THEN N'Đang cho thuê' 
        ELSE N'Trống' 
    END) LIKE '%' + @Keyword + '%';";

            SqlParameter[] parameters = { new SqlParameter("@Keyword", keyword) };

            DataTable dt = KetnoiDatabase(query, parameters);
            dgvPhongTro.DataSource = dt;

            return dt;
        }

        // Lọc và hiển thị phòng trọ khi thay đổi nội dung ô tìm kiếm
        private void txtTimKiemPT_TextChanged(object sender, EventArgs e)
        {
            HienThiBangPhongTro();
        }

        // Xóa nội dung các trường nhập liệu phòng trọ
        private void clearPhongTro()
        {
            txtTangPT.Text = "";
            txtGiaphongPT.Text = "";
            txtMaphongtroPT.Text = "";
            txtMotaPT.Text = "";
            txtSoPhongPT.Text = "";
            txtTimKiemPT.Text = "";
            txtToaNhaPT.Text = "";
            lblTrangThaiPT.Text = "";
            txtTenThanhVienPhong.Text = "";
        }

        // Kiểm tra mã phòng trọ đã tồn tại hay chưa
        private bool KiemTraMaPhongTro(string maPhongTro)
        {
            string query = "SELECT COUNT(*) FROM tblPhongTro WHERE MaPhongTro = @MaPhongTro";
            SqlParameter[] parameters = { new SqlParameter("@MaPhongTro", maPhongTro) };

            return Convert.ToInt32(KetnoiDatabase(query, parameters).Rows[0][0]) > 0;
        }

        // Lấy thông tin từ hàng được chọn trong bảng phòng trọ
        private void dgvPhongTro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvPhongTro.Rows[e.RowIndex].Selected = true;
            }

            if (e.RowIndex >= 0 && e.RowIndex < dgvPhongTro.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvPhongTro.Rows[e.RowIndex];

                txtMaphongtroPT.Text = selectedRow.Cells["MaPhongTro"].Value?.ToString();
                txtMotaPT.Text = selectedRow.Cells["MoTa"].Value?.ToString();
                txtGiaphongPT.Text = selectedRow.Cells["GiaPhong"].Value?.ToString();
                lblTrangThaiPT.Text = selectedRow.Cells["TrangThai"].Value?.ToString();
                txtToaNhaPT.Text = selectedRow.Cells["ToaNha"].Value?.ToString();
                txtTangPT.Text = selectedRow.Cells["Tang"].Value?.ToString();
                txtSoPhongPT.Text = selectedRow.Cells["SoPhong"].Value?.ToString();
                txtTenThanhVienPhong.Text = selectedRow.Cells["DanhSachKhachThue"].Value?.ToString();
            }
        }

        // Chỉnh sửa thông tin phòng trọ
        private void btnSuaPT_Click(object sender, EventArgs e)
        {
            string query = @"
UPDATE tblPhongTro SET 
    GiaPhong = @GiaPhong,
    MoTa = @MoTa,
    ToaNha = @ToaNha,
    Tang = @Tang,
    SoPhong = @SoPhong
WHERE MaPhongTro = @MaPhongTro";

            string fmTienPhong = txtGiaphongPT.Text.Replace(",", "").Replace(" ", "");
            decimal tienPhong = decimal.Parse(fmTienPhong);

            SqlParameter[] parameters = {
                new SqlParameter("@MaPhongTro", txtMaphongtroPT.Text),
                new SqlParameter("@GiaPhong", tienPhong),
                new SqlParameter("@MoTa", txtMotaPT.Text),
                new SqlParameter("@ToaNha", txtToaNhaPT.Text),
                new SqlParameter("@Tang", txtTangPT.Text),
                new SqlParameter("@SoPhong", txtSoPhongPT.Text)
            };

            if (ThucThiLenhSQL(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật phòng trọ thành công!");
                dgvPhongTro.DataSource = HienThiBangPhongTro();
                clearPhongTro();
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật phòng trọ, vui lòng kiểm tra lại.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm mới một phòng trọ
        private void btnThemPT_Click(object sender, EventArgs e)
        {
            if (KiemTraMaPhongTro(txtMaphongtroPT.Text))
            {
                MessageBox.Show("Mã phòng trọ đã tồn tại. Vui lòng chọn mã khác.");
                return;
            }

            string fmTienPhong = txtGiaphongPT.Text.Replace(",", "").Replace(" ", "");
            decimal tienPhong = decimal.Parse(fmTienPhong);

            string query = @"
                    INSERT INTO tblPhongTro (MaPhongTro, GiaPhong, MoTa, ToaNha, Tang, SoPhong)
                    VALUES (@MaPhongTro, @GiaPhong, @MoTa, @ToaNha, @Tang, @SoPhong)";

            SqlParameter[] parameters = {
                new SqlParameter("@MaPhongTro", txtMaphongtroPT.Text),
                new SqlParameter("@GiaPhong", tienPhong),
                new SqlParameter("@MoTa", txtMotaPT.Text),
                new SqlParameter("@ToaNha", txtToaNhaPT.Text),
                new SqlParameter("@Tang", txtTangPT.Text),
                new SqlParameter("@SoPhong", txtSoPhongPT.Text)
            };

            if (ThucThiLenhSQL(query, parameters) > 0)
            {
                MessageBox.Show("Thêm phòng trọ thành công!");
                dgvPhongTro.DataSource = HienThiBangPhongTro();
                clearPhongTro();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm phòng trọ, vui lòng kiểm tra lại.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa các phòng trọ được chọn
        private void btnXoaPT_Click(object sender, EventArgs e)
        {
            if (dgvPhongTro.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng trọ cần xóa.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa các phòng trọ đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No) return;

            string query = "DELETE FROM tblPhongTro WHERE MaPhongTro = @MaPhongTro";
            int rowsAffected = 0;

            foreach (DataGridViewRow row in dgvPhongTro.SelectedRows)
            {
                string maPhongTro = row.Cells["MaPhongTro"].Value.ToString();
                SqlParameter[] parameters = { new SqlParameter("@MaPhongTro", maPhongTro) };
                rowsAffected += ThucThiLenhSQL(query, parameters);
            }

            if (rowsAffected > 0)
            {
                MessageBox.Show($"Đã xóa {rowsAffected} phòng trọ thành công.");
                dgvPhongTro.DataSource = HienThiBangPhongTro();
            }
            else
            {
                MessageBox.Show("Không thể xóa phòng trọ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xuất thông tin phòng trọ ra file Excel
        private void btnXuatPhongTro_Click_1(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)dgvPhongTro.DataSource;

            // Kiểm tra dữ liệu trong DataGridView
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo ứng dụng Excel
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Không thể khởi động Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            // Thêm tiêu đề bảng
            worksheet.Cells[2, 2] = "Bảng Thông Tin Phòng Trọ";
            var titleRange = worksheet.Range["B2", "H2"];
            titleRange.Merge();
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange.Font.Bold = true;
            titleRange.Font.Size = 14;

            // Thêm tiêu đề cột
            string[] columnHeaders = { "Mã Phòng", "Tòa Nhà", "Tầng", "Số Phòng", "Giá Phòng", "Tên Thành Viên Phòng", "Mã Hợp Đồng", "Trạng Thái", "Ghi Chú" };
            for (int i = 0; i < columnHeaders.Length; i++)
            {
                worksheet.Cells[4, i + 2] = columnHeaders[i];
            }

            var headerRange = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, columnHeaders.Length + 1]];
            headerRange.Font.Bold = true;
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 5, j + 2] = dataTable.Rows[i][j];
                }
            }

            // Tự động điều chỉnh kích thước cột
            worksheet.Columns.AutoFit();

            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*",
                Title = "Lưu file Excel",
                FileName = "PhongTro.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất phòng trọ thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(false);
                    excelApp.Quit();
                }
            }
            else
            {
                workbook.Close(false);
                excelApp.Quit();
            }

            // Giải phóng bộ nhớ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

        }

        // Chỉ cho phép nhập số trong ô giá phòng
        private void txtGiaphongPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Định dạng giá phòng khi người dùng nhập
        private void txtGiaphongPT_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtGiaphongPT.Text.Replace(",", ""), out decimal value))
            {
                txtGiaphongPT.Text = string.Format("{0:N0}", value);
                txtGiaphongPT.SelectionStart = txtGiaphongPT.Text.Length;
            }
        }


        ///////CODE KHÁCH HÀNG      CODE KHÁCH HÀNG     CODE KHÁCH HÀNG     CODE KHÁCH HÀNG     CODE KHÁCH HÀNG     CODE KHÁCH HÀNG     CODE KHÁCH HÀNG     CODE KHÁCH HÀNG     CODE KHÁCH HÀNG 
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Hiện thị bảng khách hàng
        private DataTable HienThiBangKhachHang()
        {
            string keywordKT = txtTimKiemKT.Text.Trim();
            string query = @"
        SELECT 
            kt.MaPhongTro,
            kt.HoTen, 
            kt.GioiTinh,
            kt.NgaySinh, 
            kt.SoDienThoai, 
            kt.CCCD,
            kt.QueQuan  
        FROM 
            tblKhachThue kt
        INNER JOIN 
            tblPhongTro pt ON kt.MaPhongTro = pt.MaPhongTro
        WHERE 
            kt.QueQuan LIKE '%' + @keywordKT + '%' OR
            kt.MaPhongTro LIKE '%' + @keywordKT + '%' OR
            kt.HoTen LIKE '%' + @keywordKT + '%' OR
            kt.SoDienThoai LIKE '%' + @keywordKT + '%' OR
            kt.GioiTinh LIKE '%' + @keywordKT + '%' OR
            kt.CCCD LIKE '%' + @keywordKT + '%';";

            SqlParameter[] parameters = {
        new SqlParameter("@keywordKT", keywordKT)
    };

            DataTable dt = KetnoiDatabase(query, parameters);
            dgvKhachThue.DataSource = dt;

            return dt;
        }


        private void txtTimKiemKT_TextChanged(object sender, EventArgs e)
        {
            HienThiBangKhachHang();
        }
        private void ClearKH()
        {
            cbbMaPhongTroKT.Text = "";
            txtHotenKH.Clear();
            txtCccdKH.Clear();
            txtSdtKH.Clear();
            cbbGioiTinhKH.SelectedIndex = 0;
            txtTimKiemKT.Text = "";
            txtQuequanKH.Clear();

        }

        // Đẩy dữ liệu mã phòng trọ lên comboxbot
        private void CbbMaPhongTroKT()
        {
            string query = "SELECT MaPhongTro FROM tblPhongTro";

            DataTable dataTable = KetnoiDatabase(query, null);
            cbbMaPhongTroKT.DataSource = dataTable;
            cbbMaPhongTroKT.DisplayMember = "MaPhongTro";
            cbbMaPhongTroKT.ValueMember = "MaPhongTro";
            cbbMaPhongTroKT.SelectedIndex = -1;

        }
        // Cái này để đẩy nên các ở textbox
        private void dgvKhachThue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvKhachThue.Rows[e.RowIndex].Selected = true;
            }
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachThue.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvKhachThue.Rows[e.RowIndex];
                cbbMaPhongTroKT.Text = selectedRow.Cells["MaPhongTro"].Value?.ToString();
                txtHotenKH.Text = selectedRow.Cells["HoTen"].Value?.ToString();
                txtCccdKH.Text = selectedRow.Cells["CCCD"].Value?.ToString();
                txtQuequanKH.Text = selectedRow.Cells["QueQuan"].Value?.ToString();
                txtSdtKH.Text = selectedRow.Cells["SoDienThoai"].Value?.ToString();

                // Xử lý Ngày Sinh
                if (DateTime.TryParse(selectedRow.Cells["NgaySinh"].Value?.ToString(), out DateTime ngaySinh))
                {
                    dtpNgaySinhKH.Value = ngaySinh;
                }
                else
                {
                    dtpNgaySinhKH.Value = DateTime.Now;
                }

                // Xử lý Giới Tính
                string gioiTinh = selectedRow.Cells["GioiTinh"].Value?.ToString();
                if (!string.IsNullOrEmpty(gioiTinh) && cbbGioiTinhKH.Items.Contains(gioiTinh))
                {
                    cbbGioiTinhKH.SelectedItem = gioiTinh;
                }
                else
                {
                    cbbGioiTinhKH.SelectedIndex = 0;
                }
            }
        }

        //Xóa khách Thuê
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            List<(string HoTen, string CCCD)> khachThueList = new List<(string HoTen, string CCCD)>();

            // Nếu không có hàng nào được chọn, kiểm tra các ô nhập liệu
            if (dgvKhachThue.SelectedRows.Count == 0)
            {
                string hoTen = txtHotenKH.Text;
                string cccd = txtCccdKH.Text;

                if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(cccd))
                {
                    MessageBox.Show("Vui lòng chọn khách thuê trong danh sách hoặc nhập đầy đủ Họ Tên và CCCD để xóa.");
                    return;
                }

                khachThueList.Add((hoTen, cccd));
            }
            else
            {
                foreach (DataGridViewRow row in dgvKhachThue.SelectedRows)
                {
                    string hoTen = row.Cells["HoTen"].Value.ToString();
                    string cccd = row.Cells["CCCD"].Value.ToString();
                    khachThueList.Add((hoTen, cccd));
                }
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {khachThueList.Count} khách thuê đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                foreach (var khachThue in khachThueList)
                {
                    string query = "DELETE FROM tblKhachThue WHERE HoTen = @HoTen AND CCCD = @CCCD";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                            new SqlParameter("@HoTen", khachThue.HoTen),
                            new SqlParameter("@CCCD", khachThue.CCCD)
                    };

                    int rowsAffected = ThucThiLenhSQL(query, parameters);

                    if (rowsAffected <= 0)
                    {
                        MessageBox.Show($"Không thể xóa khách thuê với Họ Tên: {khachThue.HoTen} và CCCD: {khachThue.CCCD}. Vui lòng kiểm tra lại.");
                        return;
                    }
                }

                MessageBox.Show("Xóa khách thuê thành công!");
                dgvKhachThue.DataSource = HienThiBangKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi xóa khách thuê: {ex.Message}");
            }
        }

        //Cập nhật khách thuê
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            string gioiTinh = cbbGioiTinhKH.SelectedItem?.ToString() ?? "Nam";

            if (string.IsNullOrWhiteSpace(cbbMaPhongTroKT.Text) || string.IsNullOrWhiteSpace(txtCccdKH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phòng trọ hoặc CCCD để cập nhật thông tin.");
                return;
            }
            if (!KiemTraMaPhongTro(cbbMaPhongTroKT.Text))
            {
                MessageBox.Show("Mã phòng trọ không tồn tại. Vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                if (string.IsNullOrWhiteSpace(txtHotenKH.Text) || string.IsNullOrWhiteSpace(txtSdtKH.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách thuê.");
                return;
            }

            string query = @"
        UPDATE tblKhachThue
        SET  MaPhongTro = @MaPhongTro,
            CCCD = @CCCD,
            HoTen = @HoTen,
            SoDienThoai = @SoDienThoai,
            QueQuan = @QueQuan,
            GioiTinh = @GioiTinh,
            NgaySinh = @NgaySinh
        WHERE 
            MaPhongTro = @MaPhongTro OR CCCD = @CCCD";

            SqlParameter[] parameters = {
        new SqlParameter("@HoTen", txtHotenKH.Text),
        new SqlParameter("@SoDienThoai", txtSdtKH.Text),
        new SqlParameter("@QueQuan", txtQuequanKH.Text),
        new SqlParameter("@GioiTinh", gioiTinh),
        new SqlParameter("@NgaySinh", dtpNgaySinhKH.Value),
        new SqlParameter("@MaPhongTro", cbbMaPhongTroKT.Text),
        new SqlParameter("@CCCD", txtCccdKH.Text)
    };
            try
            {
                int rowsAffected = ThucThiLenhSQL(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thông tin khách thuê thành công!");
                    dgvKhachThue.DataSource = HienThiBangKhachHang();
                    ClearKH();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật thông tin khách thuê.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin khách thuê: {ex.Message}");
            }
        }

        //Thêm khách thuê
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            string gioiTinh = cbbGioiTinhKH.SelectedItem?.ToString() ?? "Nam";

            if (string.IsNullOrWhiteSpace(txtHotenKH.Text) || string.IsNullOrWhiteSpace(txtCccdKH.Text) ||
                string.IsNullOrWhiteSpace(txtSdtKH.Text) || string.IsNullOrWhiteSpace(cbbMaPhongTroKT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách thuê.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!KiemTraMaPhongTro(cbbMaPhongTroKT.Text))
            {
                MessageBox.Show("Mã phòng trọ không tồn tại. Vui lòng chọn mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (KiemTraTonTaiCCCD(txtCccdKH.Text))
            {
                MessageBox.Show("Mã căn cước công dân đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string query = @"
            INSERT INTO tblKhachThue (MaPhongTro, HoTen, GioiTinh, NgaySinh, QueQuan, SoDienThoai, CCCD) 
            VALUES (@MaPhongTro, @HoTen, @GioiTinh, @NgaySinh, @QueQuan, @SoDienThoai, @CCCD)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                   new SqlParameter("@HoTen", txtHotenKH.Text),
                    new SqlParameter("@SoDienThoai", txtSdtKH.Text),
                    new SqlParameter("@QueQuan", txtQuequanKH.Text),
                    new SqlParameter("@GioiTinh", gioiTinh),
                    new SqlParameter("@NgaySinh", dtpNgaySinhKH.Value),
                    new SqlParameter("@MaPhongTro", cbbMaPhongTroKT.Text),
                    new SqlParameter("@CCCD", txtCccdKH.Text)
                };
                int rowsAffected = ThucThiLenhSQL(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm khách thuê thành công!");
                    dgvKhachThue.DataSource = HienThiBangKhachHang();
                    ClearKH();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm khách thuê.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }

        private bool KiemTraTonTaiCCCD(string cccd)
        {
            string query = "SELECT COUNT(*) FROM tblKhachThue WHERE CCCD = @CCCD";
            SqlParameter[] parameters = { new SqlParameter("@CCCD", cccd) };
            return Convert.ToInt32(KetnoiDatabase(query, parameters).Rows[0][0]) > 0;
        }

        //Xuất bảng khách thuê ra exec
        private void btnXuatKhachThue_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)dgvKhachThue.DataSource;
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đề xuất", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Không thể khởi động được excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            //Them tieu de
            worksheet.Cells[2, 2] = "Bảng thông tin khách thuê";
            var titleRange = worksheet.Range["B2", "H2"];
            titleRange.Merge();
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange.Font.Bold = true;
            titleRange.Font.Size = 14;

            // Thêm tiêu đề cột
            string[] columHeaders = { "Mã Phòng", "Họ Tên", "Giới Tính", "Ngày Sinh", "SĐT", "CCCD", "Quê Quán" };
            for (int i = 0; i < columHeaders.Length; i++)
            {
                worksheet.Cells[4, i + 2] = columHeaders[i];
            }

            // Định dạng tiêu đề cột (font bold)
            var headerRange = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, columHeaders.Length + 1]];
            headerRange.Font.Bold = true;
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; // Căn giữa nội dung cột

            // Xuất dữ liệu (bắt đầu từ dòng 4)
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 5, j + 2] = dataTable.Rows[i][j];
                }
            }

            // Tự động điều chỉnh kích thước cột
            worksheet.Columns.AutoFit();

            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*",
                Title = "Lưu file Excel",
                FileName = "KhachThue.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất thông tin khách thuê thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(false);
                    excelApp.Quit();
                }
            }
            else
            {
                workbook.Close(false);
                excelApp.Quit();
            }

            // Giải phóng bộ nhớ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private void txtCccdKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSdtKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtHotenKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Kí tự bạn nhập không hợp lệ! Vui lòng nhập lại.");
            }
        }

        private void txtQuequanKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Kí tự bạn nhập không hợp lệ! Vui lòng nhập lại.");
            }
        }


        ///////CODE HỢP ĐỒNG CODE HỢP ĐỒNG    CODE HỢP ĐỒNG  CODE HỢP ĐỒNG   CODE HỢP ĐỒNG   CODE HỢP ĐỒNG  CCODE HỢP ĐỒNG  CODE HỢP ĐỒNG   CODE HỢP ĐỒNG   CODE HỢP ĐỒNG 
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Hiện thị bảng hợp đồng
        private DataTable HienthiBangHopDong()
        {
            string keywordHD = txtTimKiemHD.Text.Trim();

            string query = @"SELECT
        MaHopDong,
        HoTenNguoiKy,
        FORMAT(TienCoc, 'N0') AS TienCoc,
        NgayKy,
        NgayHetHan,
        CASE
            WHEN NgayHetHan < GETDATE() THEN N'Hết hạn'
            WHEN DATEDIFF(DAY, GETDATE(), NgayHetHan) <= 30 THEN N'Sắp hết hạn'
            ELSE N'Còn hiệu lực'
        END AS TrangThai,
        GhiChu
    FROM tblHopDong
    WHERE MaHopDong LIKE '%' + @KeywordHD + '%' OR
          HoTenNguoiKy LIKE '%' + @KeywordHD + '%' OR
          CAST(TienCoc AS NVARCHAR) LIKE '%' + @KeywordHD + '%';";

            SqlParameter[] parameters = { new SqlParameter("@KeywordHD", keywordHD) };

            DataTable dtResult = KetnoiDatabase(query, parameters);
            dgvHopDong.DataSource = dtResult;

            return dtResult;
        }
        
        //Dùng để gọi tìm kiếm
        private void txtTimKiemHD_TextChanged(object sender, EventArgs e)
        {
            HienthiBangHopDong();
        }

        private bool KiemTraMaHopDong(string maHopDong)
        {
            string query = "SELECT COUNT(*) FROM tblHopDong WHERE MaHopDong = @MaHopDong";
            SqlParameter[] parameters = { new SqlParameter("@MaHopDong", maHopDong) };
            return Convert.ToInt32(KetnoiDatabase(query, parameters).Rows[0][0]) > 0;
        }

        private void ClearHD()
        {
            txtMahopdongHD.Clear();
            cbbHotenKhachThueHD.Text = "";
            txtTiencocHD.Clear();
        }

        //Đẩy tên khách lên combobox
        private void DayTenKhachLenCBB()
        {
            string query = "SELECT HoTen FROM tblKhachThue";

            DataTable dataTable = KetnoiDatabase(query, null); 
                cbbHotenKhachThueHD.DataSource = dataTable;
                cbbHotenKhachThueHD.DisplayMember = "HoTen"; 
                cbbHotenKhachThueHD.ValueMember = "HoTen"; 
                cbbHotenKhachThueHD.SelectedIndex = -1;
        }

        //Chọn và bôi đen toàn bộ hàm
        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvHopDong.Rows[e.RowIndex].Selected = true;
            }

            if (e.RowIndex >= 0 && e.RowIndex < dgvHopDong.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvHopDong.Rows[e.RowIndex];

                txtMahopdongHD.Text = selectedRow.Cells["MaHopDong"].Value?.ToString();
                cbbHotenKhachThueHD.Text = selectedRow.Cells["HoTenNguoiKy"].Value?.ToString();
                txtTiencocHD.Text = selectedRow.Cells["TienCoc"].Value?.ToString();
                txtTrangthaiHD.Text = selectedRow.Cells["TrangThai"].Value?.ToString();
                txtGhiChuHopDong.Text = selectedRow.Cells["GhiChu"].Value.ToString();
                if (DateTime.TryParse(selectedRow.Cells["NgayKy"].Value?.ToString(), out DateTime ngayKy))
                {
                    dtpNgaykyHD.Value = ngayKy;
                }
                else
                {
                    dtpNgaykyHD.Value = DateTime.Now;
                }

                if (DateTime.TryParse(selectedRow.Cells["NgayHetHan"].Value?.ToString(), out DateTime ngayHetHan))
                {
                    dtpNgayhethanHD.Value = ngayHetHan;
                }
                else
                {
                    dtpNgayhethanHD.Value = DateTime.Now;
                }
            }
        }

        //Cập nhập hợp đồng
        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMahopdongHD.Text) || string.IsNullOrWhiteSpace(cbbHotenKhachThueHD.Text) || string.IsNullOrEmpty(txtTiencocHD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!KiemTraMaHopDong(txtMahopdongHD.Text))
            {
                MessageBox.Show("Mã hợp đồng không tồn tại. Vui lòng kiểm tra lại.");
                return;
            }
            if (!KiemTraTenKhachThue(cbbHotenKhachThueHD.Text))
            {
                MessageBox.Show("Tên khách thuê không tồn tại. Vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string fmTienCoc = txtTiencocHD.Text.Replace(",", "").Replace(" ", "");
                decimal tienCoc = decimal.Parse(fmTienCoc); 

                string query = "UPDATE tblHopDong SET " +
                               "HoTenNguoiKy = @HoTenNguoiKy, " +
                               "TienCoc = @TienCoc, " +
                               "NgayKy = @NgayKy, " +
                               "GhiChu = @GhiChu, " +
                               "NgayHetHan = @NgayHetHan " +
                               "WHERE MaHopDong = @MaHopDong";

                SqlParameter[] parameters =
                    {
                        new SqlParameter("@HoTenNguoiKy", cbbHotenKhachThueHD.Text),
                        new SqlParameter("@TienCoc", tienCoc),
                        new SqlParameter("@GhiChu", txtGhiChuHopDong.Text),
                        new SqlParameter("@NgayKy", dtpNgaykyHD.Value),
                        new SqlParameter("@NgayHetHan",dtpNgayhethanHD.Value),
                        new SqlParameter("@MaHopDong", txtMahopdongHD.Text)
                    };

                int rowsAffected = ThucThiLenhSQL(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật hợp đồng thành công!");
                    dgvHopDong.DataSource = HienthiBangHopDong();
                    ClearHD();
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được cập nhật. Vui lòng kiểm tra lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        //Thêm hợp đồng
        private void btnThemHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMahopdongHD.Text) || string.IsNullOrWhiteSpace(cbbHotenKhachThueHD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (KiemTraMaHopDong(txtMahopdongHD.Text))
            {
                MessageBox.Show("Mã hợp đồng này đã tồn tại. Vui lòng nhập mã hợp đồng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
             if (!KiemTraTenKhachThue(cbbHotenKhachThueHD.Text))
            {
                MessageBox.Show("Tên khách thuê không tồn tại. Vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string fmTienCoc = txtTiencocHD.Text.Replace(",", "").Replace(" ", "");
                if (!decimal.TryParse(fmTienCoc, out decimal tienCoc))
                {
                    MessageBox.Show("Số tiền cọc không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = @"
            INSERT INTO tblHopDong (MaHopDong, TienCoc, HoTenNguoiKy, GhiChu, NgayKy, NgayHetHan) 
            VALUES (@MaHopDong, @TienCoc, @HoTenNguoiKy, @GhiChu, @NgayKy, @NgayHetHan)";

                SqlParameter[] parameters = {
            new SqlParameter("@MaHopDong", txtMahopdongHD.Text),
            new SqlParameter("@TienCoc", tienCoc),
            new SqlParameter("@HoTenNguoiKy", cbbHotenKhachThueHD.Text),
            new SqlParameter("@GhiChu", txtGhiChuHopDong.Text ?? string.Empty),
            new SqlParameter("@NgayKy", dtpNgaykyHD.Value),
            new SqlParameter("@NgayHetHan", dtpNgayhethanHD.Value)
                               };

                int rowsAffected = ThucThiLenhSQL(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvHopDong.DataSource = HienthiBangHopDong();

                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm hợp đồng. Không có dòng nào bị ảnh hưởng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Xóa hợp đồng
        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            List<string> maHopDongList = new List<string>();

            if (dgvHopDong.SelectedRows.Count == 0)
            {
                string maHopDong = txtMahopdongHD.Text;

                if (string.IsNullOrWhiteSpace(maHopDong))
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng trong danh sách hoặc nhập mã hợp đồng để xóa.");
                    return;
                }

                maHopDongList.Add(maHopDong);
            }
            else
            {
                foreach (DataGridViewRow row in dgvHopDong.SelectedRows)
                {
                    maHopDongList.Add(row.Cells["MaHopDong"].Value.ToString());
                }
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {maHopDongList.Count} hợp đồng đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No) return;

            string query = "DELETE FROM tblHopDong WHERE MaHopDong = @MaHopDong";
            int rowsAffected = 0;

            foreach (string maHopDong in maHopDongList)
            {
                SqlParameter[] parameters = { new SqlParameter("@MaHopDong", maHopDong) };
                rowsAffected += ThucThiLenhSQL(query, parameters);
            }

            if (rowsAffected > 0)
            {
                MessageBox.Show($"Xóa {rowsAffected} hợp đồng thành công!");
                dgvHopDong.DataSource = HienthiBangHopDong();
                ClearHD();
            }
            else
            {
                MessageBox.Show("Không thể xóa hợp đồng. Vui lòng kiểm tra lại.");
            }
        }

        //Xuất hợp đồng ra excel
        private void btnXuatHopDong_Click(object sender, EventArgs e)
        {
            //Kiem tra du lieu trong datagirlview
            DataTable dataTable = (DataTable)dgvHopDong.DataSource;
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo ứng dụng Excel
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Không thể khởi động Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            //them tieu de bang
            worksheet.Cells[2, 2] = "Bảng Thanh Toán Hóa Đơn";
            var titleRange = worksheet.Range["B2", "L2"];
            titleRange.Merge();
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange.Font.Bold = true;
            titleRange.Font.Size = 14;

            //Thêm tiêu đề cột 
            string[] columnHeaders = { "Hợp đồng", "Tên người ký", "Tiền cọc", "Ngày ký", "Ngày hết hạn", "Trạng thái", "Ghi chú" };
            for (int i = 0; i < columnHeaders.Length; i++)
            {
                worksheet.Cells[4, i + 2] = columnHeaders[i];
            }

            var tieuDeCot = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, columnHeaders.Length + 1]];
            tieuDeCot.Font.Bold = true;
            tieuDeCot.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 5, j + 2] = dataTable.Rows[i][j];
                }
            }

            // Tự động điều chỉnh kích thước cột
            worksheet.Columns.AutoFit();


            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*",
                Title = "Lưu file Excel",
                FileName = "HopDong.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(false);
                    excelApp.Quit();
                }
            }
            else
            {
                workbook.Close(false);
                excelApp.Quit();
            }

            // Giải phóng bộ nhớ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }


        private bool KiemTraTenKhachThue(string tenKhachThue)
        {
            string query = "SELECT COUNT(*) FROM tblKhachThue WHERE HoTen = @HoTen";
            SqlParameter[] parameters = { new SqlParameter("@HoTen", tenKhachThue) };

            return Convert.ToInt32(KetnoiDatabase(query, parameters).Rows[0][0]) > 0;
        }
        private void txtTiencocHD_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTiencocHD.Text.Replace(",", ""), out decimal value))
            {
                txtTiencocHD.Text = string.Format("{0:N0}", value);
                txtTiencocHD.SelectionStart = txtTiencocHD.Text.Length;
            }
        }
        private void txtTiencocHD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        ///////CODE ĐIỆN NƯỚC  CODE ĐIỆN NƯỚC    CODE ĐIỆN NƯỚC   CODE ĐIỆN NƯỚC   CODE ĐIỆN NƯỚC   CODE ĐIỆN NƯỚC   CODE ĐIỆN NƯỚC  CODE ĐIỆN NƯỚC   CODE ĐIỆN NƯỚC   CODE ĐIỆN NƯỚC  
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Hiện thị bảng điện
        private DataTable HienThiBangDien(DateTime thangNam)
        {
            string keywordDien = txtTimKiemDN.Text;
            int month = thangNam.Month, year = thangNam.Year;
            int nextMonth = month == 12 ? 1 : month + 1;
            int nextYear = month == 12 ? year + 1 : year;

            string query = @"
-- Lấy đơn giá điện hiện tại theo mã phòng trọ
DECLARE @DonGiaDienHienTai TABLE (MaPhongTro NVARCHAR(50), DonGiaDien INT, SoDienMoi INT);
INSERT INTO @DonGiaDienHienTai
SELECT MaPhongTro, DonGiaDien, SoDienMoi
FROM tblDienNuoc
WHERE MONTH(ThangNam) = @Month AND YEAR(ThangNam) = @Year;

-- Cập nhật hoặc thêm dữ liệu tháng tiếp theo
IF DATEFROMPARTS(@Year, @Month, 1) >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
BEGIN
    MERGE tblDienNuoc AS target
    USING @DonGiaDienHienTai AS source
    ON target.MaPhongTro = source.MaPhongTro AND MONTH(target.ThangNam) = @NextMonth AND YEAR(target.ThangNam) = @NextYear
    WHEN MATCHED THEN 
        UPDATE SET target.SoDienCu = source.SoDienMoi, target.DonGiaDien = source.DonGiaDien
    WHEN NOT MATCHED THEN
        INSERT (MaPhongTro, ThangNam, SoDienCu, DonGiaDien)
        VALUES (source.MaPhongTro, DATEFROMPARTS(@NextYear, @NextMonth, 1), source.SoDienMoi, source.DonGiaDien);
END

-- Lấy dữ liệu hiển thị
SELECT MaPhongTro, SoDienCu, SoDienMoi, DonGiaDien, TieuThuDien, TienDien
FROM tblDienNuoc
WHERE MONTH(ThangNam) = @Month AND YEAR(ThangNam) = @Year
AND MaPhongTro LIKE '%' + @KeywordDien + '%'
ORDER BY MaPhongTro;";

            return KetnoiDatabase(query, new SqlParameter[]
            {
        new SqlParameter("@Month", month),
        new SqlParameter("@Year", year),
        new SqlParameter("@NextMonth", nextMonth),
        new SqlParameter("@NextYear", nextYear),
        new SqlParameter("@KeywordDien", keywordDien)
            });
        }

        //Hiện thị bảng nước
        private DataTable HienThiBangNuoc(DateTime thangNam)
        {
            string keywordNuoc = txtTimKiemDN.Text;
            int month = thangNam.Month, year = thangNam.Year;
            int nextMonth = month == 12 ? 1 : month + 1;
            int nextYear = month == 12 ? year + 1 : year;

            string query = @"
-- Lấy đơn giá nước hiện tại theo mã phòng trọ
DECLARE @DonGiaNuocHienTai TABLE (MaPhongTro NVARCHAR(50), DonGiaNuoc INT, SoNuocMoi INT);
INSERT INTO @DonGiaNuocHienTai
SELECT MaPhongTro, DonGiaNuoc, SoNuocMoi
FROM tblDienNuoc
WHERE MONTH(ThangNam) = @Month AND YEAR(ThangNam) = @Year;

-- Cập nhật hoặc thêm dữ liệu tháng tiếp theo
IF DATEFROMPARTS(@Year, @Month, 1) >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
BEGIN
    MERGE tblDienNuoc AS target
    USING @DonGiaNuocHienTai AS source
    ON target.MaPhongTro = source.MaPhongTro AND MONTH(target.ThangNam) = @NextMonth AND YEAR(target.ThangNam) = @NextYear
    WHEN MATCHED THEN 
        UPDATE SET target.SoNuocCu = source.SoNuocMoi, target.DonGiaNuoc = source.DonGiaNuoc
    WHEN NOT MATCHED THEN
        INSERT (MaPhongTro, ThangNam, SoNuocCu, DonGiaNuoc)
        VALUES (source.MaPhongTro, DATEFROMPARTS(@NextYear, @NextMonth, 1), source.SoNuocMoi, source.DonGiaNuoc);
END

-- Lấy dữ liệu hiển thị
SELECT MaPhongTro, SoNuocCu, SoNuocMoi, DonGiaNuoc, TieuThuNuoc, TienNuoc
FROM tblDienNuoc
WHERE MONTH(ThangNam) = @Month AND YEAR(ThangNam) = @Year
AND MaPhongTro LIKE '%' + @KeywordNuoc + '%'
ORDER BY MaPhongTro;";

            return KetnoiDatabase(query, new SqlParameter[]
            {
        new SqlParameter("@Month", month),
        new SqlParameter("@Year", year),
        new SqlParameter("@NextMonth", nextMonth),
        new SqlParameter("@NextYear", nextYear),
        new SqlParameter("@KeywordNuoc", keywordNuoc)
            });
        }

        //Tự động cập nhập đơn giá điện nước cho tháng sau
        private void CapNhatDonGiaDienNuocThangTiepTheoTuDong()
        {
            DateTime thangHienTai = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime thangTiepTheo = thangHienTai.AddMonths(1);

            string query = @"
-- Lấy đơn giá điện và nước hiện tại theo mã phòng trọ
DECLARE @DonGiaDienHienTai TABLE (MaPhongTro NVARCHAR(50), DonGiaDien INT, SoDienMoi INT);
DECLARE @DonGiaNuocHienTai TABLE (MaPhongTro NVARCHAR(50), DonGiaNuoc INT, SoNuocMoi INT);

INSERT INTO @DonGiaDienHienTai
SELECT MaPhongTro, DonGiaDien, SoDienMoi
FROM tblDienNuoc
WHERE MONTH(ThangNam) = @CurrentMonth AND YEAR(ThangNam) = @CurrentYear;

INSERT INTO @DonGiaNuocHienTai
SELECT MaPhongTro, DonGiaNuoc, SoNuocMoi
FROM tblDienNuoc
WHERE MONTH(ThangNam) = @CurrentMonth AND YEAR(ThangNam) = @CurrentYear;

-- Xóa dữ liệu cũ của tháng tiếp theo
MERGE tblDienNuoc AS target
USING (
    SELECT MaPhongTro, SoDienMoi AS SoDienCu, DonGiaDien, SoNuocMoi AS SoNuocCu, DonGiaNuoc
    FROM tblDienNuoc
    WHERE MONTH(ThangNam) = @CurrentMonth AND YEAR(ThangNam) = @CurrentYear
) AS source
ON target.MaPhongTro = source.MaPhongTro AND MONTH(target.ThangNam) = @NextMonth AND YEAR(target.ThangNam) = @NextYear
WHEN MATCHED THEN
    UPDATE SET target.SoDienCu = source.SoDienCu, target.DonGiaDien = source.DonGiaDien, target.SoNuocCu = source.SoNuocCu, target.DonGiaNuoc = source.DonGiaNuoc
WHEN NOT MATCHED THEN
    INSERT (MaPhongTro, ThangNam, SoDienCu, DonGiaDien, SoNuocCu, DonGiaNuoc)
    VALUES (source.MaPhongTro, DATEFROMPARTS(@NextYear, @NextMonth, 1), source.SoDienCu, source.DonGiaDien, source.SoNuocCu, source.DonGiaNuoc);
";

            try
            {
                ThucThiLenhSQL(query, new SqlParameter[]
                {
            new SqlParameter("@CurrentMonth", thangHienTai.Month),
            new SqlParameter("@CurrentYear", thangHienTai.Year),
            new SqlParameter("@NextMonth", thangTiepTheo.Month),
            new SqlParameter("@NextYear", thangTiepTheo.Year)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật đơn giá điện/nước: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Clear ô nhập nhập
        private void clearDienNuoc()
        {
            txtDonGiaNuoc.Text = "";
            txtDonGiaDien.Text = "";
            txtSoDienCu.Text = "";
            txtSoDienMoi.Text = "";
            txtSoNuocCu.Text = "";
            txtSoNuocMoi.Text = "";
            txtTienDienDN.Text = "";
            txtTienNuocDN.Text = "";
            txtMaPhongTroDN.Text = "";
            txtTieuThuDien.Text = "";
            txtTieuThuNuoc.Text = "";

        }
     
        //Cập nhập chỉ số điện nước 
        private void btnCapNhapChiSoDN_Click(object sender, EventArgs e)
        {
            string maPhongTro = txtMaPhongTroDN.Text.Trim();
            if (string.IsNullOrEmpty(maPhongTro))
            {
                MessageBox.Show("Vui lòng chọn mã phòng trọ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSoDienMoi.Text) || string.IsNullOrWhiteSpace(txtDonGiaDien.Text) ||
                string.IsNullOrWhiteSpace(txtSoNuocCu.Text) || string.IsNullOrWhiteSpace(txtSoNuocMoi.Text) ||
                string.IsNullOrWhiteSpace(txtDonGiaNuoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin điện và nước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soDienMoi = int.Parse(txtSoDienMoi.Text);
            int donGiaDien = int.Parse(txtDonGiaDien.Text);
            int soNuocMoi = int.Parse(txtSoNuocMoi.Text);
            int donGiaNuoc = int.Parse(txtDonGiaNuoc.Text);
            int soNuocCu = int.Parse(txtSoNuocCu.Text);
            int soDienCu = int.Parse(txtSoDienCu.Text);

            // Kiểm tra điều kiện: số mới phải lớn hơn số cũ
            if (soDienMoi <= soDienCu)
            {
                MessageBox.Show("Chỉ số điện mới phải lớn hơn chỉ số điện cũ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (soNuocMoi <= soNuocCu)
            {
                MessageBox.Show("Chỉ số nước mới phải lớn hơn chỉ số nước cũ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime thangNam = dtpThangNamDN.Value;

            string queryDien = @"
    MERGE tblDienNuoc AS Target
    USING (SELECT @MaPhongTro AS MaPhongTro, @Month AS Month, @Year AS Year) AS Source
    ON Target.MaPhongTro = Source.MaPhongTro 
       AND MONTH(Target.ThangNam) = Source.Month 
       AND YEAR(Target.ThangNam) = Source.Year
    WHEN MATCHED THEN
        UPDATE SET 
            Target.SoDienMoi = @SoDienMoi,
            Target.DonGiaDien = @DonGiaDien
    WHEN NOT MATCHED THEN
        INSERT (MaPhongTro, ThangNam, SoDienMoi, DonGiaDien)
        VALUES (@MaPhongTro, DATEFROMPARTS(@Year, @Month, 1), @SoDienMoi, @DonGiaDien);";

            string queryNuoc = @"
    MERGE tblDienNuoc AS Target
    USING (SELECT @MaPhongTro AS MaPhongTro, @Month AS Month, @Year AS Year) AS Source
    ON Target.MaPhongTro = Source.MaPhongTro 
       AND MONTH(Target.ThangNam) = Source.Month 
       AND YEAR(Target.ThangNam) = Source.Year
    WHEN MATCHED THEN
        UPDATE SET 
            Target.SoNuocMoi = @SoNuocMoi,
            Target.DonGiaNuoc = @DonGiaNuoc
    WHEN NOT MATCHED THEN
        INSERT (MaPhongTro, ThangNam, SoNuocMoi, DonGiaNuoc)
        VALUES (@MaPhongTro, DATEFROMPARTS(@Year, @Month, 1), @SoNuocMoi, @DonGiaNuoc);";

            SqlParameter[] parametersDien = {
        new SqlParameter("@MaPhongTro", maPhongTro),
        new SqlParameter("@SoDienMoi", soDienMoi),
        new SqlParameter("@DonGiaDien", donGiaDien),
        new SqlParameter("@Month", thangNam.Month),
        new SqlParameter("@Year", thangNam.Year)
    };

            SqlParameter[] parametersNuoc = {
        new SqlParameter("@MaPhongTro", maPhongTro),
        new SqlParameter("@SoNuocMoi", soNuocMoi),
        new SqlParameter("@DonGiaNuoc", donGiaNuoc),
        new SqlParameter("@Month", thangNam.Month),
        new SqlParameter("@Year", thangNam.Year)
    };

            int rowsAffectedDien = ThucThiLenhSQL(queryDien, parametersDien);
            int rowsAffectedNuoc = ThucThiLenhSQL(queryNuoc, parametersNuoc);

            if (rowsAffectedDien > 0 || rowsAffectedNuoc > 0)
            {
                MessageBox.Show("Cập nhật chỉ số thành công!");
                btnDIenNuoc_Click(sender, e);
                clearDienNuoc();
            }
            else
            {
                MessageBox.Show("Không tìm thấy phòng trọ hoặc dữ liệu cho tháng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Xuất bảng điẹn ra excex
        private void btnXuatbangDien_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)dgvDienNuoc.DataSource;
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đề xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo ứng dụng Excel
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Không thể khởi động Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            // Thêm tiêu đề bảng
            worksheet.Cells[2, 2] = "Bảng Tiền Điện";
            var tieude = worksheet.Range["B2", "G2"];
            tieude.Merge();
            tieude.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            tieude.Font.Bold = true;
            tieude.Font.Size = 14;

            string[] tencot = { "Mã phòng", "Số cũ", "Số mới", "Đơn giá", "Tiêu thụ", "Thành tiền" };
            for (int i = 0; i < tencot.Length; i++)
            {
                worksheet.Cells[4, i + 2] = tencot[i];
            }

            var hang = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, tencot.Length + 1]];
            hang.Font.Bold = true;
            hang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Duyệt và ghi dữ liệu từ DataTable vào Excel
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 5, j + 2] = dataTable.Rows[i][j];
                }
            }

            // Tự động điều chỉnh kích thước cột
            worksheet.Columns.AutoFit();

            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*",
                Title = "Lưu file Excel",
                FileName = "BangDien.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất bảng điện thành công.", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(false);
                    excelApp.Quit();
                }
            }
            else
            {
                workbook.Close(false);
                excelApp.Quit();
            }

            // Giải phóng bộ nhớ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        //Xuất bảng nước ra excex
        private void btnXuatbangNuoc_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)dgvDienNuoc.DataSource;
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đề xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo ứng dụng Excel
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Không thể khởi động Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            // Thêm tiêu đề bảng
            worksheet.Cells[2, 2] = "Bảng Tiền Nước";
            var tieude = worksheet.Range["B2", "G2"];
            tieude.Merge();
            tieude.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            tieude.Font.Bold = true;
            tieude.Font.Size = 14;

            string[] tencot = { "Mã phòng", "Số cũ", "Số mới", "Đơn giá", "Tiêu thụ", "Thành tiền" };
            for (int i = 0; i < tencot.Length; i++)
            {
                worksheet.Cells[4, i + 2] = tencot[i];
            }

            var hang = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, tencot.Length + 1]];
            hang.Font.Bold = true;
            hang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Duyệt và ghi dữ liệu từ DataTable vào Excel
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 5, j + 2] = dataTable.Rows[i][j];
                }
            }
            worksheet.Columns.AutoFit();

            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*",
                Title = "Lưu file Excel",
                FileName = "BangNuoc.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(false);
                    excelApp.Quit();
                }
            }
            else
            {
                workbook.Close(false);
                excelApp.Quit();
            }

            // Giải phóng bộ nhớ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        //chọn 1 ô bôi đen toàn bộ  hàng 
        private void dgvDienNuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvDienNuoc.Rows[e.RowIndex];

                txtMaPhongTroDN.Text = row.Cells["MaPhongTro"].Value.ToString();
                txtSoDienCu.Text = row.Cells["SoDienCu"].Value.ToString();
                txtSoDienMoi.Text = row.Cells["SoDienMoi"].Value.ToString();
                txtDonGiaDien.Text = row.Cells["DonGiaDien"].Value.ToString();
                txtTienDienDN.Text = row.Cells["TienDien"].Value.ToString();
                txtTieuThuDien.Text = row.Cells["TieuThuDien"].Value.ToString();


                foreach (DataGridViewRow nuocRow in dgvNuoc.Rows)
                {
                    if (nuocRow.Cells["MaPhongTro"].Value.ToString() == row.Cells["MaPhongTro"].Value.ToString())
                    {
                        dgvNuoc.ClearSelection();
                        nuocRow.Selected = true;


                        txtSoNuocCu.Text = nuocRow.Cells["SoNuocCu"].Value.ToString();
                        txtSoNuocMoi.Text = nuocRow.Cells["SoNuocMoi"].Value.ToString();
                        txtDonGiaNuoc.Text = nuocRow.Cells["DonGiaNuoc"].Value.ToString();
                        txtTieuThuNuoc.Text = nuocRow.Cells["TieuThuNuoc"].Value.ToString();
                        txtTienNuocDN.Text = nuocRow.Cells["TienNuoc"].Value.ToString();
                        break;
                    }
                }

                dgvDienNuoc.ClearSelection();
                row.Selected = true;
            }
        }

        private void dgvNuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvNuoc.Rows[e.RowIndex];
                txtMaPhongTroDN.Text = row.Cells["MaPhongTro"].Value.ToString();
                txtSoNuocCu.Text = row.Cells["SoNuocCu"].Value.ToString();
                txtSoNuocMoi.Text = row.Cells["SoNuocMoi"].Value.ToString();
                txtDonGiaNuoc.Text = row.Cells["DonGiaNuoc"].Value.ToString();
                txtTieuThuNuoc.Text = row.Cells["TieuThuNuoc"].Value.ToString();
                txtTienNuocDN.Text = row.Cells["TienNuoc"].Value.ToString();


                foreach (DataGridViewRow dienRow in dgvDienNuoc.Rows)
                {
                    if (dienRow.Cells["MaPhongTro"].Value.ToString() == row.Cells["MaPhongTro"].Value.ToString())
                    {
                        dgvDienNuoc.ClearSelection();
                        dienRow.Selected = true;

                        // Populate electric-related textboxes
                        txtSoDienCu.Text = dienRow.Cells["SoDienCu"].Value.ToString();
                        txtSoDienMoi.Text = dienRow.Cells["SoDienMoi"].Value.ToString();
                        txtDonGiaDien.Text = dienRow.Cells["DonGiaDien"].Value.ToString();
                        txtTienDienDN.Text = dienRow.Cells["TienDien"].Value.ToString();
                        txtTieuThuDien.Text = dienRow.Cells["TieuThuDien"].Value.ToString();
                        break;
                    }
                }

                dgvNuoc.ClearSelection();
                row.Selected = true;
            }
        }

        private void dtpThangNamDN_ValueChanged(object sender, EventArgs e)
        {
            btnDIenNuoc_Click(sender, e);
        }

        private void txtTimKiemDN_TextChanged_1(object sender, EventArgs e)
        {
            btnDIenNuoc_Click(sender, e);

        }
        private void txtSoNuocMoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtSoDienMoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDonGiaDien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDonGiaNuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        ///////CODE HÓA ĐƠN         CODE HÓA ĐƠN        CODE HÓA ĐƠN        CODE HÓA ĐƠN        CODE HÓA ĐƠN        CODE HÓA ĐƠN
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Hiện thị bảng hóa đơn
        private DataTable HienThiBangHoaDon(DateTime thangNam)
        {
            string keyword = txtTimKiemHoaDon.Text.Trim();
            string query = @"
SELECT 
    p.MaPhongTro,
    FORMAT(p.GiaPhong, 'N0') AS TienPhong,
    FORMAT(ISNULL(d.TienDien, 0), 'N0') AS TienDien,
    FORMAT(ISNULL(d.TienNuoc, 0), 'N0') AS TienNuoc,
    FORMAT(ISNULL(hd.TienVeSinh, 0), 'N0') AS TienVeSinh,
    FORMAT(ISNULL(hd.Internet, 0), 'N0') AS Internet,
    FORMAT(ISNULL(hd.DichVuKhac, 0), 'N0') AS DichVuKhac,
    FORMAT(ISNULL(hd.KhuyenMai, 0), 'N0') AS KhuyenMai,
    FORMAT(
        (p.GiaPhong + ISNULL(d.TienDien, 0) + ISNULL(d.TienNuoc, 0) 
        + ISNULL(hd.TienVeSinh, 0) + ISNULL(hd.Internet, 0) 
        + ISNULL(hd.DichVuKhac, 0) - ISNULL(hd.KhuyenMai, 0)), 'N0') AS TongTien,
    ISNULL(hd.TrangThai, 'Chưa thanh toán') AS TrangThai,
    hd.GhiChu
FROM tblPhongTro p
LEFT JOIN tblDienNuoc d ON p.MaPhongTro = d.MaPhongTro 
    AND MONTH(d.ThangNam) = @Month AND YEAR(d.ThangNam) = @Year
LEFT JOIN tblHoaDon hd ON p.MaPhongTro = hd.MaPhongTro 
    AND MONTH(hd.ThangNam) = @Month AND YEAR(hd.ThangNam) = @Year
WHERE 
    (p.MaPhongTro LIKE '%' + @Keyword + '%' 
     OR hd.TrangThai LIKE '%' + @Keyword + '%' 
     OR hd.GhiChu LIKE '%' + @Keyword + '%');";

            DataTable dtResult = KetnoiDatabase(query, new SqlParameter[]
            {
        new SqlParameter("@Month", thangNam.Month),
        new SqlParameter("@Year", thangNam.Year),
        new SqlParameter("@Keyword", keyword)
            });

            dgvHoaDon.DataSource = dtResult ?? new DataTable();

            return dtResult;
        }

        //Cập nhật các chỉ số hóa đơn cho tháng tiếp theo
        private void CapNhatTuDong()
        {
            DateTime thangHienTai = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime nextMonth = thangHienTai.AddMonths(1);

            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells["MaPhongTro"].Value != null)
                {
                    string maPhongTro = row.Cells["MaPhongTro"].Value.ToString();

                    if (!decimal.TryParse(row.Cells["TienVeSinh"].Value?.ToString().Replace(",", ""), out decimal tienVeSinh))
                        tienVeSinh = 0;
                    if (!decimal.TryParse(row.Cells["Internet"].Value?.ToString().Replace(",", ""), out decimal internet))
                        internet = 0;

                    string updateNextQuery = @"
MERGE tblHoaDon AS target
USING (
    SELECT @MaPhongTro AS MaPhongTro, @NextThangNam AS ThangNam, 
           @TienVeSinh AS TienVeSinh, @Internet AS Internet
) AS source
ON target.MaPhongTro = source.MaPhongTro AND MONTH(target.ThangNam) = @NextMonth AND YEAR(target.ThangNam) = @NextYear
WHEN MATCHED THEN
    UPDATE SET target.TienVeSinh = source.TienVeSinh,
               target.Internet = source.Internet
WHEN NOT MATCHED THEN
    INSERT (MaPhongTro, ThangNam, TienVeSinh, Internet)
    VALUES (source.MaPhongTro, source.ThangNam, source.TienVeSinh, source.Internet);
";

                    SqlParameter[] parameters =
                    {
                new SqlParameter("@MaPhongTro", maPhongTro),
                new SqlParameter("@NextThangNam", nextMonth),
                new SqlParameter("@TienVeSinh", tienVeSinh),
                new SqlParameter("@Internet", internet),
                new SqlParameter("@NextMonth", nextMonth.Month),
                new SqlParameter("@NextYear", nextMonth.Year)
            };

                    ThucThiLenhSQL(updateNextQuery, parameters);
                }
            }
        }



        //Cập nhật hóa đơn
        private void btnCapNhatHoaDon_Click(object sender, EventArgs e)
        {
            DateTime thangNam = dtpThangNamHD.Value;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaPhongTroHoaDon.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã phòng trọ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuyển đổi giá trị số từ chuỗi
            decimal tienVeSinh = decimal.Parse(txtTienVeSinhHD.Text.Replace(",", "").Replace(" ", ""));
            decimal tienInternet = decimal.Parse(txtTienInternet.Text.Replace(",", "").Replace(" ", ""));
            decimal tienDVKhac = decimal.Parse(txtDichVuKhacHoaDon.Text.Replace(",", "").Replace(" ", ""));
            decimal tienKhuyenMai = decimal.Parse(txtKhuyenMai.Text.Replace(",", "").Replace(" ", ""));

            string query = @"
    IF EXISTS (
        SELECT 1
        FROM tblHoaDon
        WHERE MaPhongTro = @MaPhongTro
              AND MONTH(ThangNam) = @Month
              AND YEAR(ThangNam) = @Year
    )
    BEGIN
        UPDATE tblHoaDon
        SET TienVeSinh = @TienVeSinh,
            DichVuKhac = @DichVuKhac,
            KhuyenMai = @KhuyenMai,
            GhiChu = @GhiChu,
            Internet = @Internet,
            TrangThai = @TrangThai
        WHERE MaPhongTro = @MaPhongTro
              AND MONTH(ThangNam) = @Month
              AND YEAR(ThangNam) = @Year
    END
    ELSE
    BEGIN
        INSERT INTO tblHoaDon (MaPhongTro, ThangNam, TienVeSinh, DichVuKhac, KhuyenMai, GhiChu, Internet, TrangThai)
        VALUES (@MaPhongTro, @ThangNam, @TienVeSinh, @DichVuKhac, @KhuyenMai, @GhiChu, @Internet, @TrangThai)
    END;";

            // Tạo danh sách tham số
            SqlParameter[] parameters =
            {
        new SqlParameter("@MaPhongTro", txtMaPhongTroHoaDon.Text),
        new SqlParameter("@ThangNam", thangNam),
        new SqlParameter("@TienVeSinh", tienVeSinh),
        new SqlParameter("@DichVuKhac", tienDVKhac),
        new SqlParameter("@KhuyenMai", tienKhuyenMai),
        new SqlParameter("@GhiChu", txtGhiChuHoaDon.Text),
        new SqlParameter("@Internet", tienInternet),
        new SqlParameter("@TrangThai", txtTrangThaiHoaDon.Text),
        new SqlParameter("@Month", thangNam.Month),
        new SqlParameter("@Year", thangNam.Year)
    };

            // Thực thi truy vấn
            int rows = ThucThiLenhSQL(query, parameters);

            if (rows > 0)
            {
                MessageBox.Show("Cập nhật hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Làm mới dữ liệu hiển thị
                HtnHoaDon_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn hoặc không thể cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //Tìm kiếm hóa đơn
        private void txtTimKiemHoaDon_TextChanged(object sender, EventArgs e)
        {

            DateTime thangNam = dtpThangNamHD.Value;
            HienThiBangHoaDon(thangNam);
        }

        //Clear ô nhập hóa đơn
        private void clearHoaDon()
        {
            txtDichVuKhacHoaDon.Text = "";
            txtGhiChuHoaDon.Clear();
            txtMaPhongTroHoaDon.Clear();
            txtTienDienHoaDon.Clear();
            txtTienInternet.Clear();
            txtTienNuocHoaDon.Clear();
            txtTienPhongHoaDon.Clear();
           
            txtTienVeSinhHD.Clear();
            txtTongTienHD.Clear();
            txtTimKiemHoaDon.Text = "";
        }

        //Chuyển tiếp hóa đơn khi chuyển tháng
        private void dtpThangNamHD_ValueChanged(object sender, EventArgs e)
        {
            CapNhatTuDong();
            HtnHoaDon_Click(sender, e);
        }

        //Bôi đen toàn bộ hàng khi chọn 1 ô
        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHoaDon.Rows.Count)
            {
                dgvHoaDon.Rows[e.RowIndex].Selected = true;

                DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];

                txtKhuyenMai.Text = row.Cells["KhuyenMai"].Value?.ToString() ?? string.Empty;
                txtMaPhongTroHoaDon.Text = row.Cells["MaPhongTro"].Value?.ToString() ?? string.Empty;
                txtTienPhongHoaDon.Text = row.Cells["TienPhong"].Value?.ToString() ?? string.Empty;
                txtTienDienHoaDon.Text = row.Cells["TienDien"].Value?.ToString() ?? string.Empty;
                txtTienNuocHoaDon.Text = row.Cells["TienNuoc"].Value?.ToString() ?? string.Empty;
                txtTienVeSinhHD.Text = row.Cells["TienVeSinh"].Value?.ToString() ?? string.Empty;
                txtTienInternet.Text = row.Cells["Internet"].Value?.ToString() ?? string.Empty;
                txtTongTienHD.Text = row.Cells["TongTien"].Value?.ToString() ?? string.Empty;
                txtTrangThaiHoaDon.Text = row.Cells["TrangThai"].Value?.ToString() ?? string.Empty;
                txtGhiChuHoaDon.Text = row.Cells["GhiChu"].Value?.ToString() ?? string.Empty;
                txtDichVuKhacHoaDon.Text = row.Cells["DichVuKhac"].Value?.ToString() ?? string.Empty;
            }
        }

        //Xuất hóa đơn ra excex
        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu trong DataGridView
            DataTable dataTable = (DataTable)dgvHoaDon.DataSource;
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo ứng dụng Excel
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Không thể khởi động Excel.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            // Thêm tiêu đề bảng
            worksheet.Cells[2, 2] = "Bảng Thanh Toán Hóa Đơn";
            var titleRange = worksheet.Range["B2", "L2"];
            titleRange.Merge();
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange.Font.Bold = true;
            titleRange.Font.Size = 14;

            // Thêm tiêu đề cột
            string[] columnHeaders = { "Mã Phòng", "Tiền Phòng", "Tiền Điện", "Tiền Nước", "Tiền Vệ Sinh", "Internet", "Khuyến Mãi", "Dịch Vụ Khác", "Tổng Tiền", "Trạng Thái", "Ghi Chú" };
            for (int i = 0; i < columnHeaders.Length; i++)
            {
                worksheet.Cells[4, i + 2] = columnHeaders[i];
            }

            var headerRange = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, columnHeaders.Length + 1]];
            headerRange.Font.Bold = true;
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 5, j + 2] = dataTable.Rows[i][j];
                }
            }

            // Tự động điều chỉnh kích thước cột
            worksheet.Columns.AutoFit();


            // Hiển thị hộp thoại lưu file
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*",
                Title = "Lưu file Excel",
                FileName = "HoaDon.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workbook.Close(false);
                    excelApp.Quit();
                }
            }
            else
            {
                workbook.Close(false);
                excelApp.Quit();
            }

            // Giải phóng bộ nhớ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        //Xem chi tiết hóa đơn
        private void btnXemChiTietHoaDon_Click(object sender, EventArgs e)
        {

            if (dgvHoaDon.SelectedRows.Count > 0)
            {

                DataGridViewRow row = dgvHoaDon.SelectedRows[0];

                string maPhongTro = row.Cells["MaPhongTro"].Value?.ToString();
                DateTime thangNamHD = dtpThangNamHD.Value;
                frmHoaDonChiTiet.SetUp(maPhongTro, thangNamHD);

                frmHoaDonChiTiet frmChiTiet = new frmHoaDonChiTiet();
                frmChiTiet.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void txtTienVeSinhHD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTienInternet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDichVuKhacHoaDon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtKhuyenMai_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtTienVeSinhHD_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTienVeSinhHD.Text.Replace(",", ""), out decimal value))
            {
                txtTienVeSinhHD.Text = string.Format("{0:N0}", value);
                txtTienVeSinhHD.SelectionStart = txtGiaphongPT.Text.Length;
            }
        }

        private void txtTienInternet_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTienInternet.Text.Replace(",", ""), out decimal value))
            {
                txtTienInternet.Text = string.Format("{0:N0}", value);
                txtTienInternet.SelectionStart = txtTienInternet.Text.Length;
            }
        }
        private void txtDichVuKhacHoaDon_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtDichVuKhacHoaDon.Text.Replace(",", ""), out decimal value))
            {
                txtDichVuKhacHoaDon.Text = string.Format("{0:N0}", value);
                txtDichVuKhacHoaDon.SelectionStart = txtDichVuKhacHoaDon.Text.Length;
            }
        }
        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtKhuyenMai.Text.Replace(",", ""), out decimal value))
            {
                txtKhuyenMai.Text = string.Format("{0:N0}", value);
                txtKhuyenMai.SelectionStart = txtKhuyenMai.Text.Length;
            }
        }
       


        ///////CODE THỐNG KÊ        CODE THỐNG KÊ       CODE THỐNG KÊ       CODE THỐNG KÊ       CODE THỐNG KÊ       CODE THỐNG KÊ   CODE THỐNG KÊ
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Đêm số lượng khách
        private void DemSoLuongKhach()
        {
            string queryNam = "SELECT COUNT(*) FROM tblKhachThue kt " +
                       "INNER JOIN tblPhongTro pt ON kt.MaPhongTro = pt.MaPhongTro " +
                       "WHERE kt.GioiTinh = 'Nam';";
            string queryNu = "SELECT COUNT(*) FROM tblKhachThue kt " +
                      "INNER JOIN tblPhongTro pt ON kt.MaPhongTro = pt.MaPhongTro " +
                      "WHERE kt.GioiTinh = N'Nữ';";
            string query1 = "SELECT COUNT(*) FROM tblKhachThue kt " +
                    "INNER JOIN tblPhongTro pt ON kt.MaPhongTro = pt.MaPhongTro " +
                    "WHERE kt.CCCD IS NOT NULL;";

            DataTable resultNam = KetnoiDatabase(queryNam);
            lblKhachNamTK.Text = $"Khách nam: {resultNam.Rows[0][0]} người";

            DataTable resultNu = KetnoiDatabase(queryNu);
            lblKhachThueNuTK.Text = $"Khách nữ: {resultNu.Rows[0][0]} người";


            DataTable result1 = KetnoiDatabase(query1);
            lblTongKhachTK.Text = $"Tổng {result1.Rows[0][0]} khách thuê";

        }

        //Đếm số lượng phòng trọ
        private void DemSoLuongPhongTro()
        {
            string slPhongTrong = @"
           SELECT 
             COUNT(*) 
             FROM tblPhongTro p
             LEFT JOIN tblKhachThue k ON p.MaPhongTro = k.MaPhongTro
             WHERE 
            NOT EXISTS (
                SELECT 1 
                FROM tblKhachThue kt 
                WHERE kt.MaPhongTro = p.MaPhongTro);";

            DataTable phongTrong = KetnoiDatabase(slPhongTrong);
            lblPhongTrongTK.Text = $"Trống: {phongTrong.Rows[0][0]} phòng";

            string slPhongDangThue = @"SELECT 
               COUNT(DISTINCT p.MaPhongTro)
            FROM 
               tblPhongTro p
            INNER JOIN 
               tblKhachThue k ON p.MaPhongTro = k.MaPhongTro;";
            DataTable phongDangThue = KetnoiDatabase(slPhongDangThue);
            lblPhongThueTK.Text = $"Đang cho thuê: {phongDangThue.Rows[0][0]} phòng";

            string slPhong = @"SELECT COUNT(*) FROM tblPhongTro;";
            DataTable tongPhong = KetnoiDatabase(slPhong);
            lblTongPhongTK.Text = $"Tổng {tongPhong.Rows[0][0]} phòng";
        }

        //Đếm số lượng hợp đồng
        private void DemSoLuongHopDong()
        {
            string query = @"
            SELECT COUNT(*)
            FROM tblHopDong
            WHERE DATEDIFF(DAY, GETDATE(), NgayHetHan) <= 30 
                  AND DATEDIFF(DAY, GETDATE(), NgayHetHan) > 0;";

            DataTable sapHetHan = KetnoiDatabase(query);
            lblHDSapHetHanTK.Text = $"Sắp hết hạn: {sapHetHan.Rows[0][0]}";

            string tongHD = @"SELECT COUNT(*) FROM tblHopDong;";
            DataTable tongHopDong = KetnoiDatabase(tongHD);
            lblTongHDTK.Text = $"Tổng {tongHopDong.Rows[0][0]} hợp đồng";

            string queryHetHan = @"
            SELECT COUNT(*)
            FROM tblHopDong
            WHERE NgayHetHan < GETDATE();";

            DataTable hetHan = KetnoiDatabase(queryHetHan);
            lblHDHetHanTk.Text = $"Hết hạn: {hetHan.Rows[0][0]}";

            string queryConHieuLuc = @"
            SELECT COUNT(*)
            FROM tblHopDong
            WHERE NgayHetHan > GETDATE();";

            DataTable conHieuLuc = KetnoiDatabase(queryConHieuLuc);
            lblHDHieuLucTK.Text = $"Còn hiệu lực: {conHieuLuc.Rows[0][0]}";


        }

        //HIện thị bảng doanh thu + biểu đồ
        private void HienThiDoanhThu()
        {
            try
            {
                int Year = dtpThangNamTK.Value.Year;

                string query = $@"
WITH DanhSachThang AS (
    SELECT CAST(DATEADD(MONTH, n, '{Year}-01-01') AS DATE) AS ThangNam
    FROM (VALUES (0), (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11)) AS X(n)
),
DoanhThuPhong AS (
    SELECT 
        FORMAT(ThangNam, 'MM - yyyy') AS ThangNam, 
        SUM(GiaPhong) AS TongDoanhThuTienPhong
    FROM 
        tblPhongTro PT
    JOIN 
        tblHoaDon HD ON PT.MaPhongTro = HD.MaPhongTro
    GROUP BY 
        FORMAT(ThangNam, 'MM - yyyy')
),
DoanhThuDienNuoc AS (
    SELECT 
        FORMAT(ThangNam, 'MM - yyyy') AS ThangNam, 
        SUM(TienNuoc) AS TongDoanhThuTienNuoc,
        SUM(TienDien) AS TongDoanhThuTienDien
    FROM 
        tblDienNuoc
    GROUP BY 
        FORMAT(ThangNam, 'MM - yyyy')
),
DoanhThuDichVu AS (
    SELECT 
        FORMAT(ThangNam, 'MM - yyyy') AS ThangNam, 
        SUM(DichVuKhac) + SUM(TienVeSinh) + SUM(Internet) AS TongDoanhThuDichVu
    FROM 
        tblHoaDon
    GROUP BY 
        FORMAT(ThangNam, 'MM - yyyy')
)
SELECT 
    FORMAT(T.ThangNam, 'MM - yyyy') AS ThangNam,
    ISNULL(P.TongDoanhThuTienPhong, 0) AS TongDoanhThuTienPhong,
    ISNULL(DN.TongDoanhThuTienNuoc, 0) AS TongDoanhThuTienNuoc,
    ISNULL(DN.TongDoanhThuTienDien, 0) AS TongDoanhThuTienDien,
    ISNULL(D.TongDoanhThuDichVu, 0) AS TongDoanhThuDichVu,
    ISNULL(P.TongDoanhThuTienPhong, 0) 
        + ISNULL(DN.TongDoanhThuTienNuoc, 0) 
        + ISNULL(DN.TongDoanhThuTienDien, 0) 
        + ISNULL(D.TongDoanhThuDichVu, 0) AS TongDoanhThu
FROM 
    DanhSachThang T
LEFT JOIN 
    DoanhThuPhong P ON FORMAT(T.ThangNam, 'MM - yyyy') = P.ThangNam
LEFT JOIN 
    DoanhThuDienNuoc DN ON FORMAT(T.ThangNam, 'MM - yyyy') = DN.ThangNam
LEFT JOIN 
    DoanhThuDichVu D ON FORMAT(T.ThangNam, 'MM - yyyy') = D.ThangNam
ORDER BY 
    T.ThangNam;
";


                // Thực thi truy vấn và hiển thị dữ liệu
                DataTable dt = KetnoiDatabase(query);

                dgvTongDoanhThu.DataSource = dt;
                dgvTongDoanhThu.Columns["ThangNam"].HeaderText = "Tháng Năm";
                dgvTongDoanhThu.Columns["TongDoanhThuTienPhong"].HeaderText = "Tổng Tiền Phòng";
                dgvTongDoanhThu.Columns["TongDoanhThuTienNuoc"].HeaderText = "Tổng Tiền Nước";
                dgvTongDoanhThu.Columns["TongDoanhThuTienDien"].HeaderText = "Tổng Tiền Điện";
                dgvTongDoanhThu.Columns["TongDoanhThuDichVu"].HeaderText = "Tổng Tiền Dịch Vụ";
                dgvTongDoanhThu.Columns["TongDoanhThu"].HeaderText = "Tổng Doanh Thu";

                dgvTongDoanhThu.Columns["TongDoanhThuTienPhong"].DefaultCellStyle.Format = "#,##0";
                dgvTongDoanhThu.Columns["TongDoanhThuTienNuoc"].DefaultCellStyle.Format = "#,##0";
                dgvTongDoanhThu.Columns["TongDoanhThuTienDien"].DefaultCellStyle.Format = "#,##0";
                dgvTongDoanhThu.Columns["TongDoanhThuDichVu"].DefaultCellStyle.Format = "#,##0";
                dgvTongDoanhThu.Columns["TongDoanhThu"].DefaultCellStyle.Format = "#,##0";

                chartTongDoanhThu.Series.Clear();
                var DoanhThuTong = chartTongDoanhThu.Series.Add("Tổng Doanh Thu");
                DoanhThuTong.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                foreach (DataRow row in dt.Rows)
                {
                    string month = row["ThangNam"].ToString();
                    if (decimal.TryParse(row["TongDoanhThu"].ToString(), out decimal revenue))
                    {
                        DoanhThuTong.Points.AddXY(month, revenue);
                    }
                }

                // Hiển thị dữ liệu trong biểu đồ Điện và Nước
                chartDienNuoc.Series.Clear();

                var cotDien = chartDienNuoc.Series.Add("Tiền Điện");
                cotDien.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                var cotNuoc = chartDienNuoc.Series.Add("Tiền Nước");
                cotNuoc.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                chartTongDoanhThu.Titles.Clear();
                var titleTongDoanhThu = chartTongDoanhThu.Titles.Add("Tổng Doanh Thu Theo Tháng");
                titleTongDoanhThu.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                chartDienNuoc.Titles.Clear();
                var titleDienNuoc = chartDienNuoc.Titles.Add("Doanh Thu Tiền Điện và Tiền Nước");
                titleDienNuoc.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                foreach (DataRow row in dt.Rows)
                {
                    string month = row["ThangNam"].ToString();
                    if (decimal.TryParse(row["TongDoanhThuTienNuoc"].ToString(), out decimal waterRevenue) &&
                        decimal.TryParse(row["TongDoanhThuTienDien"].ToString(), out decimal electricityRevenue))
                    {
                        cotDien.Points.AddXY(month, electricityRevenue);
                        cotNuoc.Points.AddXY(month, waterRevenue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void dtpThangNamTK_ValueChanged(object sender, EventArgs e)
        {
            HienThiDoanhThu();
        }

        

    }


}





