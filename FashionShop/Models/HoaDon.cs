﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FashionShop.Models
{
    public class HoaDon
    {
        [Key]
        public string MaHoaDon { get; set; }
        public double TongTien { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayDatMuaHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}