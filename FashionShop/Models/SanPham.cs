using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class SanPham
    {
        [Key]
        public string MaSanPham {  get; set; }
        public string TenSanPham {  get; set; }
        public double KhuyenMai {  get; set; }
        public double GiaSanPham { get; set; }
        public string AnhDaiDien {  get; set; }
        public string MoTaSanPham {  get; set; }
        public virtual LoaiSanPham LoaiSanPham {  get; set; }
        public virtual ThuongHieu ThuongHieu {  get; set; }
    }
}