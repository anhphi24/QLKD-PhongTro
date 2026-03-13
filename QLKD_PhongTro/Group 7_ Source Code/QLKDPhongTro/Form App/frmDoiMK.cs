// Thư viện sử dụng
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static QLKDPhongTro.frmDangNhap;

namespace QLKDPhongTro
{
    public partial class frmDoiMK : Form
    {
        // Khởi tạo form đổi mật khẩu
        public frmDoiMK()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(CaiDatTK_FormClosing);
        }

        // Hàm thực thi câu lệnh SQL
        private object ThucThiLenhSQL(string query, SqlParameter[] parameters = null, bool isScalar = false)
        {
            string connectionString = "Server=DESKTOP-923DSF9\\SQLEXPRESS;Database=QLKDPhongTro;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();

                        if (isScalar)
                        {
                            return cmd.ExecuteScalar(); 
                        }
                        else
                        {
                            return cmd.ExecuteNonQuery(); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thực thi câu lệnh SQL: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // Hàm xử lý khi form tải
        private void CaiDatTK_Load(object sender, EventArgs e)
        {
            txtTenDangNhapCD.Text = UserSession.Username; 

            txtMatKhauCuCD.Clear();
            txtMatKhauCD.Clear(); 
            txtXacNhanMKCD.Clear(); 
        }

        // Hàm xử lý khi form đóng
        private void CaiDatTK_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtMatKhauCuCD.Clear();
            txtMatKhauCD.Clear();
            txtXacNhanMKCD.Clear();
        }

        // Hàm kiểm tra mật khẩu có hợp lệ
        public static bool KiemTraMK(string password)
        {
            if (password.Length < 6) 
            {
                return false;
            }
            bool hasUpperCase = password.Any(char.IsUpper); 
            bool hasDigit = password.Any(char.IsDigit); 
            return hasUpperCase || hasDigit;
        }

        // Hàm xử lý khi nhấn nút Đồng Ý để đổi mật khẩu
        private void btnDongYCD_Click(object sender, EventArgs e)
        {
            string tenDangNhapCD = txtTenDangNhapCD.Text.Trim();
            string matKhauCuCD = txtMatKhauCuCD.Text.Trim();
            string matKhauCD = txtMatKhauCD.Text.Trim();
            string xnMatKhauCD = txtXacNhanMKCD.Text.Trim();

            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrEmpty(tenDangNhapCD) || string.IsNullOrEmpty(matKhauCuCD))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tên đăng nhập và mật khẩu cũ
            string query = "SELECT COUNT(*) FROM tblDangNhap WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TenDangNhap", tenDangNhapCD),
                new SqlParameter("@MatKhau", matKhauCuCD)
            };

            int result = Convert.ToInt32(ThucThiLenhSQL(query, parameters, true));
            if (result <= 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu
            if (string.IsNullOrEmpty(matKhauCD) || string.IsNullOrEmpty(xnMatKhauCD))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới và xác nhận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraMK(matKhauCD))
            {
                MessageBox.Show("Mật khẩu mới không hợp lệ! Mật khẩu phải có ít nhất 6 ký tự, bao gồm ít nhất một chữ cái viết hoa hoặc một chữ số.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauCD == matKhauCuCD)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauCD != xnMatKhauCD)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            string updateQuery = "UPDATE tblDangNhap SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] updateParameters =
            {
                new SqlParameter("@MatKhauMoi", matKhauCD),
                new SqlParameter("@TenDangNhap", tenDangNhapCD)
            };

            int rowsAffected = Convert.ToInt32(ThucThiLenhSQL(updateQuery, updateParameters));
            if (rowsAffected > 0)
            {
                MessageBox.Show("Cập nhật mật khẩu thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm xử lý khi nhấn nút Hủy bỏ
        private void btnHuyboCD_Click(object sender, EventArgs e)
        {
            Close();
            txtMatKhauCD.Text = "";
            txtMatKhauCuCD.Text = "";
            txtXacNhanMKCD.Text = "";
        }

        // Hàm xử lý sự kiện vẽ giao diện (không sử dụng trong trường hợp này)
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
