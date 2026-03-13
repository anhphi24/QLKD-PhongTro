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

namespace QLKDPhongTro
{
    public partial class frmHoaDonChiTiet : Form
    {
        public static Image SavedImage { get; set; }
        public static string LuuGhiChuTk { get; set; }
        public static string LuuGhiChu { get; set; }

        public static string MaPhongTro { get; set; }
        public static DateTime dtpThangNamHD { get; set; }

        public frmHoaDonChiTiet()
        {
            InitializeComponent();
            LoadSetUp();
        }

        // Hàm thực hiện kết nối và truy vấn cơ sở dữ liệu
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

        // Hàm thiết lập dữ liệu ban đầu cho các thành phần giao diện
        private void LoadSetUp()
        {
            // Lấy thông tin số phòng
            string query = "SELECT SoPhong FROM tblPhongTro WHERE MaPhongTro = '" + MaPhongTro + "'";
            DataTable soPhong = KetnoiDatabase(query);
            txtSoPhongTro.Text = $"{soPhong.Rows[0]["SoPhong"]}";

            // Lấy tên khách thuê
            string query1 = "SELECT STRING_AGG(HoTen, ', ') AS TenKhachThue FROM tblKhachThue WHERE MaPhongTro = '" + MaPhongTro + "'";
            DataTable hoTen = KetnoiDatabase(query1);
            txtHoTen.Text = (hoTen.Rows.Count > 0 ? hoTen.Rows[0]["TenKhachThue"].ToString() : "");

            // Lấy số điện thoại khách thuê
            string query2 = "SELECT STRING_AGG(SoDienThoai, ', ') AS sdt FROM tblKhachThue WHERE MaPhongTro = '" + MaPhongTro + "'";
            DataTable sdt = KetnoiDatabase(query2);
            txtSDT.Text = (sdt.Rows.Count > 0 ? sdt.Rows[0]["sdt"].ToString() : "");

            // Lấy thông tin tòa nhà
            string query3 = "SELECT ToaNha FROM tblPhongTro WHERE MaPhongTro = '" + MaPhongTro + "'";
            DataTable toaNha = KetnoiDatabase(query3);
            txtToaNha.Text = $"{toaNha.Rows[0]["ToaNha"]}";

            // Hiển thị tháng/năm hóa đơn
            lblThangNamHD.Text = "HÓA ĐƠN THU TIỀN PHÒNG " + dtpThangNamHD.ToString("MM/yyyy");

            // Kiểm tra và cập nhật chi tiết hóa đơn
            string query4 = "SELECT MaHoaDon FROM tblHoaDon WHERE MaPhongTro = '" + MaPhongTro + "'";
            DataTable maHoaDon = KetnoiDatabase(query4);

            HienThiTrangThaiHoaDon();

            CheckChiTietHoaDon(maHoaDon);
            CapNhatChiTietHoaDon();
        }

        // Hàm kiểm tra và thêm chi tiết hóa đơn nếu cần
        private void CheckChiTietHoaDon(DataTable maHoaDon)
        {
            if (maHoaDon.Rows.Count > 0)
            {
                int maHoaDonCT = Convert.ToInt32(maHoaDon.Rows[0]["MaHoaDon"]);
                string truncateQuery = "TRUNCATE TABLE tblChiTietHoaDon;";
                KetnoiDatabase(truncateQuery, null);

                string insertQuery = @"INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, ChiSoCu, ChiSoMoi, SoLuong, DonGia, ThanhTien)
                              VALUES (@MaHoaDonCT, N'Tiền phòng', NULL, NULL, 0, 0, 0);";
                SqlParameter[] insertParameters = { new SqlParameter("@MaHoaDonCT", maHoaDonCT) };
                KetnoiDatabase(insertQuery, insertParameters);
            }
        }
        public void HienThiTrangThaiHoaDon()
        {
            DateTime thangNam = dtpThangNamHD;

            string query5 = @"
        SELECT TrangThai 
        FROM tblHoaDon 
        WHERE MaPhongTro = @MaPhongTro
              AND MONTH(ThangNam) = @Month 
              AND YEAR(ThangNam) = @Year";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaPhongTro", MaPhongTro),
        new SqlParameter("@Month", thangNam.Month),
        new SqlParameter("@Year", thangNam.Year)
            };

            DataTable dtTrangThai = KetnoiDatabase(query5, parameters);

            
                string trangThai = dtTrangThai.Rows[0]["TrangThai"].ToString();
                txtTrangThai.Text = ""+ trangThai; 
            
        }

        // Hàm cập nhật chi tiết hóa đơn dựa trên dữ liệu phòng trọ và dịch vụ
        public void CapNhatChiTietHoaDon()
        {
            DateTime thangNam = dtpThangNamHD;
            string tienPhong = @"
            UPDATE tblChiTietHoaDon
            SET ThanhTien = p.GiaPhong
            FROM tblChiTietHoaDon ct
            JOIN tblHoaDon h ON ct.MaHoaDonCT = h.MaHoaDon
            JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
            WHERE ct.TenDichVu = N'Tiền phòng';";
            KetnoiDatabase(tienPhong);


            string query = @"

        INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, ChiSoCu, ChiSoMoi, SoLuong, DonGia, ThanhTien)
        SELECT 
            h.MaHoaDon, 
            N'Tiền Điện', 
            dn.SoDienCu AS ChiSoCu, 
            dn.SoDienMoi AS ChiSoMoi, 
            (dn.SoDienMoi - dn.SoDienCu) AS SoLuong,
            dn.DonGiaDien AS DonGia,
            ((dn.SoDienMoi - dn.SoDienCu) * dn.DonGiaDien) AS ThanhTien
        FROM tblHoaDon h
        JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
        JOIN tblDienNuoc dn ON dn.MaPhongTro = p.MaPhongTro
        WHERE 
            MONTH(h.ThangNam) = MONTH(dn.ThangNam)
            AND YEAR(h.ThangNam) = YEAR(dn.ThangNam)
            AND MONTH(h.ThangNam) = @Month 
            AND YEAR(h.ThangNam) = @Year
            AND h.MaPhongTro = @MaPhongTro;



        INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, ChiSoCu, ChiSoMoi, SoLuong, DonGia, ThanhTien)
        SELECT 
            h.MaHoaDon, 
            N'Tiền Nước', 
            dn.SoNuocCu AS ChiSoCu, 
            dn.SoNuocMoi AS ChiSoMoi, 
            (dn.SoNuocMoi - dn.SoNuocCu) AS SoLuong,
            dn.DonGiaNuoc AS DonGia,
            ((dn.SoNuocMoi - dn.SoNuocCu) * dn.DonGiaNuoc) AS ThanhTien
        FROM tblHoaDon h
        JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
        JOIN tblDienNuoc dn ON dn.MaPhongTro = p.MaPhongTro
        WHERE 
            MONTH(h.ThangNam) = MONTH(dn.ThangNam)
            AND YEAR(h.ThangNam) = YEAR(dn.ThangNam)
            AND MONTH(h.ThangNam) = @Month 
            AND YEAR(h.ThangNam) = @Year
            AND h.MaPhongTro = @MaPhongTro;


            INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, SoLuong, DonGia, ThanhTien)
            SELECT 
                h.MaHoaDon,
                N'Tiền Internet',
                1,
                h.Internet,
                h.Internet
            FROM tblHoaDon h
            JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
            WHERE 
                MONTH(h.ThangNam) = @Month 
                AND YEAR(h.ThangNam) = @Year
                AND h.MaPhongTro = @MaPhongTro;



            INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, SoLuong, DonGia, ThanhTien)
            SELECT h.MaHoaDon, N'Tiền vệ sinh', 1, h.TienVeSinh, h.TienVeSinh
            FROM tblHoaDon h
            JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
            WHERE 
                MONTH(h.ThangNam) = @Month 
                AND YEAR(h.ThangNam) = @Year
                AND h.MaPhongTro = @MaPhongTro;
            


            INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, SoLuong, DonGia, ThanhTien)
            SELECT h.MaHoaDon,N'TIền dịch vụ khác', 1, h.DichVuKhac, h.DichVuKhac
            FROM tblHoaDon h
            JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
            WHERE 
                MONTH(h.ThangNam) = @Month 
                AND YEAR(h.ThangNam) = @Year
                AND h.MaPhongTro = @MaPhongTro;



            INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, SoLuong, DonGia, ThanhTien)
            SELECT h.MaHoaDon, N'Khuyến mãi', 1, h.KhuyenMai, h.KhuyenMai
            FROM tblHoaDon h
            JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
            WHERE 
                MONTH(h.ThangNam) =@Month
                AND YEAR(h.ThangNam) = @Year
                AND h.MaPhongTro = @MaPhongTro;



    INSERT INTO tblChiTietHoaDon (MaHoaDonCT, TenDichVu, ThanhTien)
    SELECT TOP 1
        h.MaHoaDon, 
        N'Tổng tiền', 
        ((p.GiaPhong + ((dn.SoDienMoi - dn.SoDienCu) * dn.DonGiaDien) + ((dn.SoNuocMoi - dn.SoNuocCu) * dn.DonGiaNuoc) +h.TienVeSinh + h.DichVuKhac + h.Internet) - h.KhuyenMai) AS ThanhTien
      FROM tblHoaDon h
                JOIN tblPhongTro p ON h.MaPhongTro = p.MaPhongTro
                JOIN tblDienNuoc dn ON dn.MaPhongTro = p.MaPhongTro
                WHERE 
                    MONTH(h.ThangNam) = MONTH(dn.ThangNam)
                    AND YEAR(h.ThangNam) = YEAR(dn.ThangNam)
                    AND MONTH(h.ThangNam) = @Month 
                    AND YEAR(h.ThangNam) = @Year
                    AND h.MaPhongTro = @MaPhongTro;";

            SqlParameter[] parameters = new SqlParameter[]
            {
    new SqlParameter("@Month", thangNam.Month),
    new SqlParameter("@Year", thangNam.Year),
    new SqlParameter("@MaPhongTro", MaPhongTro)
            };

            KetnoiDatabase(query, parameters);

        }

        public static void SetUp(string maPhongTro, DateTime dtpThangNamHD)
        {
            MaPhongTro = maPhongTro;
            frmHoaDonChiTiet.dtpThangNamHD = dtpThangNamHD;
        }

        // Xử lý khi form hóa đơn chi tiết được tải
        private void HoaDonChiTiet_Load(object sender, EventArgs e)
        {
            if (LuuGhiChuTk != null)
            {
                txtGhiChuTK.Text = LuuGhiChuTk;
            }
            else
            {
                txtGhiChuTK.Text = "Nhập ghi chú...";
            }

            if (LuuGhiChu != null)
            {
                txtGhiChu.Text = LuuGhiChu;
            }
            else
            {
                txtGhiChu.Text = "Nhập ghi chú...";
            }

            if (SavedImage != null)
            {
                btnChonAnhQR.BackgroundImage = new Bitmap(SavedImage);
                btnChonAnhQR.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btnChonAnhQR.Text = "Nhấp để chọn ảnh";
            }
            HienThiChiTiet();
            foreach (DataGridViewColumn column in dgvChiTietHoaDon.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // Xử lý khi người dùng chọn ảnh QR để thêm vào hóa đơn
        private void btnChonAnhQR_Click_1(object sender, EventArgs e)
        {
            if (SavedImage == null)
            {
               
                btnChonAnhQR.TextAlign = ContentAlignment.MiddleCenter;
                btnChonAnhQR.BackgroundImage = null; 
                btnChonAnhQR.BackgroundImageLayout = ImageLayout.None;
            }

            using (OpenFileDialog openFileDiaLog = new OpenFileDialog())
            {
                openFileDiaLog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDiaLog.Title = "Chọn ảnh";
                if (openFileDiaLog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Image image = Image.FromFile(openFileDiaLog.FileName))
                        {
                            btnChonAnhQR.BackgroundImage = new Bitmap(image);
                            btnChonAnhQR.BackgroundImageLayout = ImageLayout.Stretch;
                            btnChonAnhQR.Text = string.Empty;
                            SavedImage = new Bitmap(image);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        // Đóng form khi nhấn nút Hủy Bỏ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Hiển thị thông tin chi tiết hóa đơn trong DataGridView
        private void HienThiChiTiet()
        {
            string query = @"
SELECT 
    STT, 
    TenDichVu, 
    FORMAT(ChiSoCu, 'N0') AS ChiSoCu, 
    FORMAT(ChiSoMoi, 'N0') AS ChiSoMoi, 
    SoLuong, 
    FORMAT(DonGia, 'N0') AS DonGia, 
    FORMAT(ThanhTien, 'N0') AS ThanhTien 
FROM 
    tblChiTietHoaDon;
";

            // Kết nối và lấy dữ liệu từ cơ sở dữ liệu
            DataTable dt = KetnoiDatabase(query);
            dgvChiTietHoaDon.DataSource = dt;
            dgvChiTietHoaDon.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dgvChiTietHoaDon.Columns["ChiSoCu"].HeaderText = "Chỉ Số Cũ";
            dgvChiTietHoaDon.Columns["ChiSoMoi"].HeaderText = "Chỉ Số Mới";
            dgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTietHoaDon.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChiTietHoaDon.Columns["ThanhTien"].HeaderText = "Thành Tiền";

            dgvChiTietHoaDon.Columns["STT"].Width = 45;
            dgvChiTietHoaDon.Columns["TenDichVu"].Width = 130;
        }
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang trong quá trình phát triển.", "Thông báo", MessageBoxButtons.OK);
        }


        // Xử lý sự kiện khi người dùng nhấn vào nội dung trong DataGridView
        private void dgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            foreach (DataGridViewRow row in dgvChiTietHoaDon.Rows)
            {
                row.Height = 100;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

       
        private void txtGhiChuTK_TextChanged(object sender, EventArgs e)
        {
            LuuGhiChuTk = txtGhiChuTK.Text;
        }

        private void btnLuuGhiChu_Click(object sender, EventArgs e)
        {
           
        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {
            LuuGhiChu = txtGhiChu.Text;
        }
    }
}
