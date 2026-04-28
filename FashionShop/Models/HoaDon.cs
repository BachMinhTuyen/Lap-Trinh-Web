using System;
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
        public double PhiVanChuyen { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayDatMuaHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string PhuongThucVanChuyen { get; set; }
        public string TrangThaiDonHang { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }

        public static class OrderStatus
        {
            public const string Pending = "Chờ xác nhận";
            public const string Confirmed = "Đã xác nhận";
            public const string Processing = "Đã chuẩn bị";
            public const string Shipping = "Đang giao";
            public const string Completed = "Đã nhận";
            public const string Cancelled = "Đã hủy";
        }
    }
}