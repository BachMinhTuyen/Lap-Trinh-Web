using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Bắt buộc nhập UserName!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập Password!")]
        [StringLength(255, ErrorMessage = "Phải nhập từ 5 tới 255 ký tự", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập Password!")]
        [StringLength(255, ErrorMessage = "Phải nhập từ 5 tới 255 ký tự", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đầy đủ Họ Và Tên!")]
        public string TenNguoiDung { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Địa Chỉ!")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số Điện Thoại!")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email!")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }
    }
}