using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class HinhAnh
    {
        [Key]
        public string MaHinhAnh {  get; set; }
        public string TenHinhAnh {  get; set; }
        public virtual SanPham SanPham { get; set; }

    }
}