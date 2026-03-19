# :warning: Lập Trình Web - Web Development :100:

:pushpin: Bấm **[tại đây](http://www.fashionshop.somee.com/)** để xem ngay website :sparkles:

## Hướng dẫn cài đặt

## 1. Chuẩn bị môi trường

Đảm bảo máy bạn đã cài đặt các công cụ sau:

* [Visual Studio](https://visualstudio.microsoft.com/downloads/) (phiên bản 2019 trở lên)
* [Microsoft SQL Server - Sandard Developer edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (phiên bản 2022 trở lên)
* [SQL Server Management Studio - SSMS](https://learn.microsoft.com/en-us/ssms/install/install) 

> SSMS là một phần mềm giao diện (GUI) giúp người dùng quản lý, cấu hình và điều khiển hệ quản trị cơ sở dữ liệu SQL Server một cách dễ dàng thay vì phải gõ lệnh thủ công. 

### 2. Tải và Mở dự án

1. Tải code từ GitHub hoặc copy thư mục dự án về máy.

```bash
git clone https://github.com/BachMinhTuyen/Lap-Trinh-Web.git
```

2. Mở thư mục vừa tải về và mở ```FashionShop.sln``` file

3. Khôi phục NuGet Packages

**Cách 1:** 

Chuột phải vào Solution 'FashionShop' ở cột bên phải (Solution Explorer). Chọn Restore NuGet Packages.

**Cách 2:** 

Mở Tools > NuGet Package Manager > Package Manager Console và gõ lệnh:
```
Update-Package -Reinstall
```
4. Kiểm tra kết nối Cơ sở dữ liệu (Database)

> [!NOTE]
> Đã cập nhật - bổ sung file sql tạo bảng (kèm dữ liệu)
> **Đường dẫn:** /SQL/FashionShopDB.sql

Mở ```Web.config``` file và cấu hình dòng ```connectionString=""``` để trỏ đúng vào tên Server SQL, tên database trên máy **(Tự động tạo các bảng tương ứng)**

Ví dụ:

```xml
<connectionStrings>
  <add name="myConnectionString" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=FashionShopDB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Trong đó:

* ```.\SQLEXPRESS```  là địa chỉ máy chủ SQL
* ```FashionShopDB``` là tên Database
* ```User ID=myUser;Password=myPassword;``` là tài khoản SQL (nếu không dùng ```Integrated Security```)
---

:pushpin: **Dự án này đã sử dụng** :sparkles: 

1. :triangular_flag_on_post: Ngôn ngữ lập trình: **C#, HTML, CSS, Javascript** :heavy_check_mark:
2. :triangular_flag_on_post: Sử dụng mô hình **ASP NET - MVC 5** kết hợp **EntityFramework** và một số thư viện, famework khác :heavy_check_mark:
3. :triangular_flag_on_post: Sử dụng **[EmailJS](https://www.emailjs.com/)** để gửi thông tin đơn hàng về email :heavy_check_mark:
4. :triangular_flag_on_post: Sử dụng **[ChartJS](https://www.chartjs.org/)** để thống kê doanh thu theo biểu đồ :heavy_check_mark:
5.  :triangular_flag_on_post: Sử dụng **[API Open Province](https://provinces.open-api.vn/) 63 tỉnh thành phố ở Việt Nam** :heavy_check_mark:
6. :triangular_flag_on_post: Có chức năng **đăng nhập cơ bản**. Xác minh danh tính **(verify identity)** và phân quyền **user - admin** :heavy_check_mark:
7. :triangular_flag_on_post: Các **trang html** được xây dựng :heavy_check_mark:

- **Trang web dành cho User**
  - :palm_tree: Trang Chủ :partly_sunny:
  - :palm_tree: Giới Thiệu :partly_sunny:
  - :palm_tree: Danh Sách Sản Phẩm :partly_sunny:
  - :palm_tree: Chi Tiết Sản Phẩm :partly_sunny:
  - :palm_tree: Giỏ Hàng :partly_sunny:
  - :palm_tree: Thanh Toán :partly_sunny:
  - :palm_tree: Đăng nhập / Đăng ký :partly_sunny:
  - :palm_tree: Hồ Sơ Người Dùng :partly_sunny:
  - :palm_tree: Đơn Hàng Đã Đặt :partly_sunny:
  - :palm_tree: Sản Phẩm Yêu Thích :partly_sunny:

> [!WARNING]
> Hiện tại trang admin vẫn chưa hoàn thiện
> **Tài khoản và mật khẩu lưu trữ tại /SQL/FashionShopDB.sql**

- **Trang web dành cho Admin** 
  - :palm_tree: Trang Tổng Quan :partly_sunny:
  - :palm_tree: Thống Kê Doang Thu :partly_sunny:
  - :palm_tree: Quản Lý Sản Phẩm :partly_sunny:
  - :palm_tree: Quản Lý Đơn Hàng :partly_sunny:
  - :palm_tree: Quản Lý Tài Khoản :partly_sunny:
