using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class BienTheSanPham
    {
        [Key]
        public string MaBienThe { get; set; }
        public string TenMau { get; set; }
        public string TenSize { get; set; }
        public int SoLuong { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}