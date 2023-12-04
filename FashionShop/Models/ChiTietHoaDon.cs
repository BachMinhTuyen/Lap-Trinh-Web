using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public string MaChiTietHoaDon { get; set; }
        public int SoLuongSanPhamDaDat { get; set; }
        public double ThanhTien { get; set; }
        public virtual BienTheSanPham BienTheSanPham { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}