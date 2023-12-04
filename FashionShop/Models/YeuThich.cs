using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class YeuThich
    {
        [Key]
        public string MaYeuThich { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}