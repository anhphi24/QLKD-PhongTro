# Hệ Thống Quản Lý Kinh Doanh Phòng Trọ

## 📋 Mô Tả Dự Án

Hệ thống quản lý kinh doanh phòng trọ là ứng dụng Windows Forms được phát triển bằng C# .NET Framework 4.7.2, giúp quản lý toàn bộ hoạt động kinh doanh phòng trọ một cách hiệu quả và chuyên nghiệp.

## ✨ Tính Năng Chính

### 🔐 Quản Lý Người Dùng
- Đăng nhập/Đăng ký tài khoản
- Đổi mật khẩu
- Quản lý thông tin người dùng

### 🏠 Quản Lý Phòng Trọ
- Thêm, sửa, xóa thông tin phòng trọ
- Quản lý tòa nhà, tầng, số phòng
- Quản lý giá phòng và mô tả

### 👥 Quản Lý Khách Thuê
- Thêm, sửa, xóa thông tin khách thuê
- Quản lý thông tin cá nhân (Họ tên, giới tính, ngày sinh, quê quán)
- Quản lý số điện thoại và CCCD
- Liên kết khách thuê với phòng trọ

### 📄 Quản Lý Hợp Đồng
- Tạo và quản lý hợp đồng thuê phòng
- Quản lý tiền cọc
- Theo dõi ngày ký và ngày hết hạn hợp đồng
- Ghi chú hợp đồng

### 💰 Quản Lý Hóa Đơn
- Tạo và quản lý hóa đơn theo tháng
- Quản lý điện nước (chỉ số cũ/mới, đơn giá, tiêu thụ)
- Quản lý các dịch vụ khác (vệ sinh, internet, dịch vụ khác)
- Áp dụng khuyến mãi
- Theo dõi trạng thái thanh toán
- Xem chi tiết hóa đơn

### 📊 Báo Cáo & Xuất Dữ Liệu
- Xuất dữ liệu ra file Excel
- Xem báo cáo chi tiết

## 🛠️ Công Nghệ Sử Dụng

- **Ngôn ngữ**: C# (.NET Framework 4.7.2)
- **Giao diện**: Windows Forms
- **Cơ sở dữ liệu**: Microsoft SQL Server
- **Thư viện**:
  - Microsoft.Office.Interop.Excel (xuất Excel)
  - System.Configuration.ConfigurationManager
  - System.Windows.Forms.DataVisualization (biểu đồ)

## 📦 Yêu Cầu Hệ Thống

### Phần Mềm Cần Thiết
- **Visual Studio 2017 trở lên** (hoặc IDE hỗ trợ .NET Framework 4.7.2)
- **SQL Server** (SQL Server Express hoặc bản đầy đủ)
- **Microsoft Office Excel** (để xuất file Excel)

### Hệ Điều Hành
- Windows 7 trở lên
- .NET Framework 4.7.2 hoặc cao hơn

## 🚀 Hướng Dẫn Cài Đặt

### Bước 1: Cài Đặt Database

1. Mở **SQL Server Management Studio (SSMS)**
2. Kết nối với SQL Server instance của bạn
3. Mở file `QlKDPhongTro (1).sql`
4. Thực thi script để tạo database và các bảng cần thiết

```sql
-- Script sẽ tự động tạo:
-- - Database: QLKDPhongTro
-- - Các bảng: tblDangNhap, tblPhongTro, tblKhachThue, tblHopDong, tblDienNuoc, tblHoaDon, tblChiTietHoaDon
-- - Dữ liệu mẫu
```

### Bước 2: Cấu Hình Connection String

Cần cập nhật connection string tại các vị trí sau trong source code:

1. **Form Đăng Nhập** (`frmDangNhap.cs`):
   - Dòng 21: Cập nhật `Server` và `Database` trong connection string

2. **Form App** (`frmFormApp.cs`):
   - Dòng 28: Connection string cho `KetnoiDatabase()`
   - Dòng 46: Connection string cho `ThucThiLenhSQL()`

3. **Form Hóa Đơn Chi Tiết** (`frmHoaDonChiTiet.cs`):
   - Dòng đầu code: Cập nhật connection string

4. **Form Cài Đặt** (nếu có):
   - Cập nhật connection string

**Cú pháp connection string:**
```csharp
string connectionString = "Server=TEN_SERVER\\SQLEXPRESS;Database=QLKDPhongTro;Trusted_Connection=True;";
// Hoặc sử dụng SQL Authentication:
string connectionString = "Server=TEN_SERVER\\SQLEXPRESS;Database=QLKDPhongTro;User Id=sa;Password=mat_khau;";
```

### Bước 3: Cài Đặt Dependencies

1. Mở solution file `QLKDPhongTro.sln` trong Visual Studio
2. Visual Studio sẽ tự động restore các NuGet packages:
   - `Microsoft.Office.Interop.Excel.15.0.4795.1001`
   - `System.Configuration.ConfigurationManager.9.0.0`

### Bước 4: Build và Chạy

1. Chọn **Build > Build Solution** (hoặc nhấn `Ctrl+Shift+B`)
2. Chọn **Debug > Start Debugging** (hoặc nhấn `F5`)
3. Ứng dụng sẽ khởi động với form đăng nhập

## 🔑 Thông Tin Đăng Nhập Mặc Định

Sau khi chạy script SQL, bạn có thể đăng nhập với:

- **Tên đăng nhập**: `admin`
- **Mật khẩu**: `admin`

> ⚠️ **Lưu ý**: Nên đổi mật khẩu ngay sau lần đăng nhập đầu tiên!

## 📁 Cấu Trúc Thư Mục

```
QLKDPhongTro/
├── Form App/
│   ├── frmDangNhap.cs          # Form đăng nhập/đăng ký
│   ├── frmFormApp.cs           # Form chính của ứng dụng
│   ├── frmHoaDonChiTiet.cs     # Form chi tiết hóa đơn
│   └── frmDoiMK.cs             # Form đổi mật khẩu
├── Properties/                 # Cấu hình và resources
├── Resources/                  # Hình ảnh và tài nguyên
├── packages/                   # NuGet packages
├── bin/                        # File thực thi sau khi build
├── Program.cs                  # Entry point của ứng dụng
├── QLKDPhongTro.csproj        # File project
└── QLKDPhongTro.sln           # Solution file
```

## 🗄️ Cấu Trúc Database

### Các Bảng Chính

- **tblDangNhap**: Quản lý tài khoản người dùng
- **tblPhongTro**: Thông tin phòng trọ
- **tblKhachThue**: Thông tin khách thuê
- **tblHopDong**: Hợp đồng thuê phòng
- **tblDienNuoc**: Quản lý điện nước
- **tblHoaDon**: Hóa đơn tổng hợp
- **tblChiTietHoaDon**: Chi tiết các dịch vụ trong hóa đơn

## 📝 Ghi Chú Quan Trọng

### Cấu Hình Server Database

Theo file `Note.txt`, cần cập nhật connection string tại **5 vị trí**:
- Form đăng nhập: **1 chỗ**
- Form App: **2 chỗ**
- Form chi tiết hóa đơn: **1 chỗ**
- Form cài đặt: **1 chỗ**

Tất cả đều nằm ở **đầu file code**, trong các hàm kết nối database.

## 🐛 Xử Lý Lỗi Thường Gặp

### Lỗi kết nối database
- **Nguyên nhân**: Connection string không đúng hoặc SQL Server chưa khởi động
- **Giải pháp**: Kiểm tra tên server, database name và đảm bảo SQL Server đang chạy

### Lỗi không xuất được Excel
- **Nguyên nhân**: Chưa cài đặt Microsoft Office Excel
- **Giải pháp**: Cài đặt Microsoft Office hoặc sử dụng Office Interop Runtime

### Lỗi build project
- **Nguyên nhân**: Thiếu NuGet packages
- **Giải pháp**: Right-click solution > Restore NuGet Packages

## 👥 Nhóm Phát Triển

**Group 7 - ITE1264E B05E**

## 📄 Tài Liệu Bổ Sung

- `Group 7_PDF Report.pdf`: Báo cáo PDF
- `Group 7_PPT Report.pptx`: Báo cáo PowerPoint
- `Group 7_Word Report.docx`: Báo cáo Word
- `Note.txt`: Ghi chú về cấu hình


**Phiên bản**: 1.0  
**Ngày cập nhật**: 2024

