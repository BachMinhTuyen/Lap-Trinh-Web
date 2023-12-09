using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.ViewModel
{
    public class CartVM
    {
        FashionShopDBContext db = new FashionShopDBContext();
        public int iCartItem {  get; set; }
        public string sMaSanPham { get; set; }
        public string sTenSanPham { get; set; }
        public string sAnhDaiDien { get; set; }
        public string sTenMau { get; set; }
        public string sTenSize { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        //Init Cart
        public CartVM(int id, string maSanPham, string tenMau, string tenSize)
        {
            iCartItem = id;
            sMaSanPham = maSanPham;
            SanPham sanPham = db.SanPham.Single(s => s.MaSanPham == maSanPham);
            sTenSanPham = sanPham.TenSanPham;
            sAnhDaiDien = sanPham.AnhDaiDien;
            sTenMau = tenMau;
            sTenSize = tenSize;
            double gia;
            if (sanPham.KhuyenMai != 0)
            {
                gia = sanPham.GiaSanPham - (sanPham.GiaSanPham * sanPham.KhuyenMai);
            }
            else
            {
                gia = sanPham.GiaSanPham;
            }    
            dDonGia = gia;
            iSoLuong = 1;
        }
        public CartVM(int id, string maSanPham, string tenMau, string tenSize, int soLuong)
        {
            iCartItem = id;
            sMaSanPham = maSanPham;
            SanPham sanPham = db.SanPham.Single(s => s.MaSanPham == maSanPham);
            sTenSanPham = sanPham.TenSanPham;
            sAnhDaiDien = sanPham.AnhDaiDien;
            sTenMau = tenMau;
            sTenSize = tenSize;
            double gia;
            if (sanPham.KhuyenMai != 0)
            {
                gia = sanPham.GiaSanPham - (sanPham.GiaSanPham * sanPham.KhuyenMai);
            }
            else
            {
                gia = sanPham.GiaSanPham;
            }
            dDonGia = gia;
            iSoLuong = soLuong;
        }
    }
}