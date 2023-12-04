using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class FashionShopDBContext : DbContext
    {
        public FashionShopDBContext() :base("myConnectionString") { }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<ThuongHieu> ThuongHieu { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<BienTheSanPham> BienTheSanPham { get; set; }
        public DbSet<HinhAnh> HinhAnh { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<YeuThich> YeuThich { get; set; }
    }
}