using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class ThuongHieu
    {
        [Key]
        public string MaThuongHieu { get; set; }
        public string TenThuongHieu { get; set; }
        public string XuatXu {  get; set; }
    }
}