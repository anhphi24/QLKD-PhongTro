using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLKDPhongTro
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            pnDangKy.Visible = false;
            pnDangNhap.Visible = true;
        }

        // Hàm thực hiện lệnh SQL
        private int ThucThiLenhSQL(string query, SqlParameter[] parameters = null, bool isScalar = false)
        {
            string connectionString = "Server=DESKTOP-923DSF9\\SQLEXPRESS;Database=QLKDPhongTro;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
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
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Sự kiện load form Đăng Nhập
        private void DangNhap_Load(object sender, EventArgs e)
        {
            if (chbHienThiMK.Checked)
            {
                txtMatKhauDN.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauDN.PasswordChar = '*';
            }
        }

        // Sự kiện khi nhấn vào liên kết "Đăng Ký"
        private void lblDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnDangKy.Visible = true;
            pnDangNhap.Visible = false;
        }

        // Sự kiện khi nhấn vào liên kết "Đăng Nhập"
        private void lblDangNhap_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnDangKy.Visible = false;
            pnDangNhap.Visible = true;
        }

        // Hàm kiểm tra định dạng email hợp lệ
        private bool KiemTraEmailHopLe(string email)
        {
            string pattern = "^[a-zA-Z0-9]+@gmail\\.com$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        // Hàm kiểm tra mật khẩu hợp lệ
        public static bool KiemTraMK(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasDigit = password.Any(char.IsDigit);

            return hasUpperCase && hasDigit;
        }


        // Sự kiện khi nhấn vào nút Đăng Ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenDangNhapDK = txtTendangnhapDK.Text.Trim();
            string emailDK = txtEmailDK.Text.Trim();
            string matKhauDK = txtMatkhauDK.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhapDK) || string.IsNullOrEmpty(emailDK) || string.IsNullOrEmpty(matKhauDK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (!KiemTraMK(matKhauDK))
            {
                MessageBox.Show("Mật khẩu không hợp lệ! Mật khẩu phải có ít nhất 6 ký tự, bao gồm ít nhất một chữ cái viết hoa hoặc một chữ số.");
                return;
            }

            string checkQuery = "SELECT COUNT(*) FROM tblDangNhap WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] checkParameters = { new SqlParameter("@TenDangNhap", tenDangNhapDK) };

            int count = ThucThiLenhSQL(checkQuery, checkParameters, true);
            if (count > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                return;
            }

            if (!KiemTraEmailHopLe(emailDK))
            {
                MessageBox.Show("Email không hợp lệ! Vui lòng nhập email với định dạng đúng.");
                return;
            }

            string checkEmailQuery = "SELECT COUNT(*) FROM tblDangNhap WHERE Email = @Email";
            SqlParameter[] checkEmailParameters = { new SqlParameter("@Email", emailDK) };

            int emailCount = ThucThiLenhSQL(checkEmailQuery, checkEmailParameters, true);
            if (emailCount > 0)
            {
                MessageBox.Show("Email đã được sử dụng. Vui lòng chọn email khác.");
                return;
            }

            string insertQuery = "INSERT INTO tblDangNhap (TenDangNhap, Email, MatKhau) VALUES (@TenDangNhap, @Email, @MatKhau)";
            SqlParameter[] insertParameters =
            {
        new SqlParameter("@TenDangNhap", tenDangNhapDK),
        new SqlParameter("@Email", emailDK),
        new SqlParameter("@MatKhau", matKhauDK)
    };

            int rowsAffected = ThucThiLenhSQL(insertQuery, insertParameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Đăng ký thành công!");
                pnDangKy.Visible = false;
                pnDangNhap.Visible = true;
                txtTenDangNhapDN.Text = txtTendangnhapDK.Text;
                txtMatKhauDN.Text = "";
            }
            else
            {
                MessageBox.Show("Lỗi khi đăng ký.");
            }
        }

        private void txtXnmkDK_TextChanged(object sender, EventArgs e)
        {
            if (txtMatkhauDK.Text != txtXnmkDK.Text)
            {
                errorProvider1.SetError(txtXnmkDK, "Mật khẩu không trùng khớp!!");
                timer1.Stop();
                timer1.Start();
            }
            else
            {
                errorProvider1.SetError(txtXnmkDK, string.Empty);
                timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            errorProvider1.SetError(txtXnmkDK, string.Empty);
        }

        // Sự kiện nhấn nút Đăng Nhập
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string tenDangNhapDN = txtTenDangNhapDN.Text.Trim();
            string matKhauDN = txtMatKhauDN.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhapDN) || string.IsNullOrEmpty(matKhauDN))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT COUNT(*) FROM tblDangNhap WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            SqlParameter[] parameters =
            {
        new SqlParameter("@TenDangNhap", tenDangNhapDN),
        new SqlParameter("@MatKhau", matKhauDN)
    };

            int result = ThucThiLenhSQL(query, parameters, true);

            if (result > 0)
            {
                UserSession.Username = tenDangNhapDN;
                frmFormApp formApp = new frmFormApp();
                formApp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }

        // Sự kiện thay đổi trạng thái checkbox hiển thị mật khẩu
        private void chbHienThiMK_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbHienThiMK.Checked)
            {
                txtMatKhauDN.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauDN.PasswordChar = '*';
            }
        }

        // Sự kiện nhấn vào liên kết "Quên mật khẩu"
        private void lblQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Chức năng quên mật khẩu hiện đang trong quá trình phát triển.");
        }

        // Sự kiện nhấn vào nút Facebook
        private void btnFacebook_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dịch vụ Facebook đang phát triển, xin vui lòng quay lại sau!");
        }

        // Sự kiện nhấn vào nút Google
        private void btnGoogle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dịch vụ Google đang phát triển, xin vui lòng quay lại sau!");
        }

        // Lớp UserSession lưu trữ thông tin phiên làm việc của người dùng
        public static class UserSession
        {
            public static string Username { get; set; }
            public static string Password { get; set; }
        }

        // Sự kiện khi nhấn Enter trong trường mật khẩu đăng nhập
        private void txtMatKhauDN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        // Sự kiện vẽ giao diện của panel Đăng Nhập
        private void pnDangNhap_Paint(object sender, PaintEventArgs e)
        {

        }

        // Sự kiện khi nhấn Enter hoặc nhập ký tự không hợp lệ trong tên đăng nhập
        private void txtTenDangNhapDN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Kí tự bạn nhập không hợp lệ! Vui lòng nhập lại.");
            }
        }

        // Sự kiện khi nhấn Enter hoặc nhập ký tự không hợp lệ trong trường tên đăng ký
        private void txtTendangnhapDK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Kí tự bạn nhập không hợp lệ! Vui lòng nhập lại.");
            }
        }

        // Sự kiện khi nhấn Enter hoặc nhập ký tự không hợp lệ trong trường email đăng ký
        private void txtEmailDK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
           
        }

        // Sự kiện khi nhấn Enter trong trường mật khẩu đăng ký
        private void txtMatkhauDK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        // Sự kiện khi nhấn Enter trong trường xác nhận mật khẩu đăng ký
        private void txtXnmkDK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        // Sự kiện khi form Đăng Nhập đóng lại
        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
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

        private void txtMatkhauDK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
