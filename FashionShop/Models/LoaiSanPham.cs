using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class LoaiSanPham
    {
        [Key]
        public string MaLoai {  get; set; }
        public string TenLoai { get; set; }
    }
}