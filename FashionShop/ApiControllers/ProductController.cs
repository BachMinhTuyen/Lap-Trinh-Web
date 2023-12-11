using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FashionShop.ApiControllers
{
    public class ProductController : ApiController
    {
        FashionShopDBContext db = new FashionShopDBContext();
        [HttpGet]
         public List<SanPham> GetProduct()
        {
            List<SanPham> lst = db.SanPham.ToList();
            return lst;
        }
        [HttpGet]
        public SanPham GetProductByID(string maSanPham)
        {
            SanPham sanPham = db.SanPham.First(s => s.MaSanPham == maSanPham);
            return sanPham;
        }
        [HttpPost]
        public void PostProduct(SanPham sanPhamMoi)
        {
            List<SanPham> lst = db.SanPham.ToList();
            int i = 0;
            foreach (var item in lst)
            {
                int iNew = int.Parse(item.MaSanPham.Substring(2, item.MaSanPham.Length - 2));
                if (iNew > i)
                    i = iNew;
            }

            int index = i;
            int maSP = index + 1;
            sanPhamMoi.MaSanPham = "SP" + maSP;

            string maThuongHieu = sanPhamMoi.ThuongHieu.MaThuongHieu;
            ThuongHieu th = db.ThuongHieu.First(x => x.MaThuongHieu == maThuongHieu);
            sanPhamMoi.ThuongHieu = th;

            string loaiSanPham = sanPhamMoi.LoaiSanPham.MaLoai;
            LoaiSanPham loaiSP = db.LoaiSanPham.First(x => x.MaLoai == loaiSanPham);
            sanPhamMoi.LoaiSanPham = loaiSP;

            db.SanPham.Add(sanPhamMoi);
            db.SaveChanges();
        }
        [HttpPut]
        public void PutProduct(SanPham sanPham)
        {
            SanPham sanPhamCu = db.SanPham.Where(row => row.MaSanPham == sanPham.MaSanPham).FirstOrDefault();
            sanPhamCu.TenSanPham = sanPham.TenSanPham;
            sanPhamCu.KhuyenMai = sanPham.KhuyenMai;
            sanPhamCu.GiaSanPham = sanPham.GiaSanPham;
            sanPhamCu.AnhDaiDien = sanPham.AnhDaiDien;
            sanPhamCu.ThuongHieu = sanPham.ThuongHieu;
            sanPhamCu.LoaiSanPham = sanPham.LoaiSanPham;

            db.SaveChanges();
        }
        [HttpDelete]
        public void DeleteProduct(string maSanPham)
        {
            SanPham sanPham = db.SanPham.Where(row => row.MaSanPham == maSanPham).FirstOrDefault();
            db.SanPham.Remove(sanPham);
            db.SaveChanges();
        }
    }   
}
