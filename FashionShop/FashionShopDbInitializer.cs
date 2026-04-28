using FashionShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WindowsFormsApplication2.Utilities;

public class FashionShopDbInitializer : CreateDatabaseIfNotExists<FashionShopDBContext>
{
    protected override void Seed(FashionShopDBContext context)
    {
        // Danh sách Admin cần có
        var admins = new List<TaiKhoan>
        {
            new TaiKhoan {
                TenNguoiDung = "Người Quản Trị",
                UserName = "admin",
                Password = Password.Create_SHA256("123456"),
                VaiTro = "Admin",
                Email = "admin@example.com",
                DiaChi = "Thành Phố Hồ Chí Minh",
                SoDienThoai = "0123456789",
            }
        };

        foreach (var admin in admins)
        {
            if (!context.TaiKhoan.Any(a => a.UserName == admin.UserName))
            {
                context.TaiKhoan.Add(admin);
            }
        }
        context.SaveChanges();
    }
}