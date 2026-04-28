namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BienTheSanPhams",
                c => new
                    {
                        MaBienThe = c.String(nullable: false, maxLength: 128),
                        TenMau = c.String(),
                        TenSize = c.String(),
                        SoLuong = c.Int(nullable: false),
                        SanPham_MaSanPham = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaBienThe)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_MaSanPham)
                .Index(t => t.SanPham_MaSanPham);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaSanPham = c.String(nullable: false, maxLength: 128),
                        TenSanPham = c.String(),
                        KhuyenMai = c.Double(nullable: false),
                        GiaSanPham = c.Double(nullable: false),
                        AnhDaiDien = c.String(),
                        MoTaSanPham = c.String(),
                        LoaiSanPham_MaLoai = c.String(maxLength: 128),
                        ThuongHieu_MaThuongHieu = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaSanPham)
                .ForeignKey("dbo.LoaiSanPhams", t => t.LoaiSanPham_MaLoai)
                .ForeignKey("dbo.ThuongHieux", t => t.ThuongHieu_MaThuongHieu)
                .Index(t => t.LoaiSanPham_MaLoai)
                .Index(t => t.ThuongHieu_MaThuongHieu);
            
            CreateTable(
                "dbo.LoaiSanPhams",
                c => new
                    {
                        MaLoai = c.String(nullable: false, maxLength: 128),
                        TenLoai = c.String(),
                    })
                .PrimaryKey(t => t.MaLoai);
            
            CreateTable(
                "dbo.ThuongHieux",
                c => new
                    {
                        MaThuongHieu = c.String(nullable: false, maxLength: 128),
                        TenThuongHieu = c.String(),
                        XuatXu = c.String(),
                    })
                .PrimaryKey(t => t.MaThuongHieu);
            
            CreateTable(
                "dbo.ChiTietHoaDons",
                c => new
                    {
                        MaChiTietHoaDon = c.String(nullable: false, maxLength: 128),
                        SoLuongSanPhamDaDat = c.Int(nullable: false),
                        ThanhTien = c.Double(nullable: false),
                        BienTheSanPham_MaBienThe = c.String(maxLength: 128),
                        HoaDon_MaHoaDon = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaChiTietHoaDon)
                .ForeignKey("dbo.BienTheSanPhams", t => t.BienTheSanPham_MaBienThe)
                .ForeignKey("dbo.HoaDons", t => t.HoaDon_MaHoaDon)
                .Index(t => t.BienTheSanPham_MaBienThe)
                .Index(t => t.HoaDon_MaHoaDon);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        MaHoaDon = c.String(nullable: false, maxLength: 128),
                        TongTien = c.Double(nullable: false),
                        PhiVanChuyen = c.Double(nullable: false),
                        GhiChu = c.String(),
                        NgayDatMuaHang = c.DateTime(nullable: false),
                        PhuongThucThanhToan = c.String(),
                        PhuongThucVanChuyen = c.String(),
                        TrangThaiDonHang = c.String(),
                        TaiKhoan_UserName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaHoaDon)
                .ForeignKey("dbo.TaiKhoans", t => t.TaiKhoan_UserName)
                .Index(t => t.TaiKhoan_UserName);
            
            CreateTable(
                "dbo.TaiKhoans",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 255),
                        VaiTro = c.String(),
                        TenNguoiDung = c.String(nullable: false),
                        DiaChi = c.String(nullable: false),
                        SoDienThoai = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.HinhAnhs",
                c => new
                    {
                        MaHinhAnh = c.String(nullable: false, maxLength: 128),
                        TenHinhAnh = c.String(),
                        SanPham_MaSanPham = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaHinhAnh)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_MaSanPham)
                .Index(t => t.SanPham_MaSanPham);
            
            CreateTable(
                "dbo.YeuThiches",
                c => new
                    {
                        MaYeuThich = c.String(nullable: false, maxLength: 128),
                        SanPham_MaSanPham = c.String(maxLength: 128),
                        TaiKhoan_UserName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaYeuThich)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_MaSanPham)
                .ForeignKey("dbo.TaiKhoans", t => t.TaiKhoan_UserName)
                .Index(t => t.SanPham_MaSanPham)
                .Index(t => t.TaiKhoan_UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YeuThiches", "TaiKhoan_UserName", "dbo.TaiKhoans");
            DropForeignKey("dbo.YeuThiches", "SanPham_MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.HinhAnhs", "SanPham_MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietHoaDons", "HoaDon_MaHoaDon", "dbo.HoaDons");
            DropForeignKey("dbo.HoaDons", "TaiKhoan_UserName", "dbo.TaiKhoans");
            DropForeignKey("dbo.ChiTietHoaDons", "BienTheSanPham_MaBienThe", "dbo.BienTheSanPhams");
            DropForeignKey("dbo.BienTheSanPhams", "SanPham_MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "ThuongHieu_MaThuongHieu", "dbo.ThuongHieux");
            DropForeignKey("dbo.SanPhams", "LoaiSanPham_MaLoai", "dbo.LoaiSanPhams");
            DropIndex("dbo.YeuThiches", new[] { "TaiKhoan_UserName" });
            DropIndex("dbo.YeuThiches", new[] { "SanPham_MaSanPham" });
            DropIndex("dbo.HinhAnhs", new[] { "SanPham_MaSanPham" });
            DropIndex("dbo.HoaDons", new[] { "TaiKhoan_UserName" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "HoaDon_MaHoaDon" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "BienTheSanPham_MaBienThe" });
            DropIndex("dbo.SanPhams", new[] { "ThuongHieu_MaThuongHieu" });
            DropIndex("dbo.SanPhams", new[] { "LoaiSanPham_MaLoai" });
            DropIndex("dbo.BienTheSanPhams", new[] { "SanPham_MaSanPham" });
            DropTable("dbo.YeuThiches");
            DropTable("dbo.HinhAnhs");
            DropTable("dbo.TaiKhoans");
            DropTable("dbo.HoaDons");
            DropTable("dbo.ChiTietHoaDons");
            DropTable("dbo.ThuongHieux");
            DropTable("dbo.LoaiSanPhams");
            DropTable("dbo.SanPhams");
            DropTable("dbo.BienTheSanPhams");
        }
    }
}
