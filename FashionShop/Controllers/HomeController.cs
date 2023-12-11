using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsFormsApplication2.Utilities;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Home
        public ActionResult Index() 
        {
            if (!db.TaiKhoan.Any(a => a.UserName == "admin" && a.VaiTro == "Admin"))
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.TenNguoiDung = "Người Quản Trị";
                taiKhoan.UserName = "admin";
                taiKhoan.Password = Password.Create_SHA256("123456");
                taiKhoan.VaiTro = "Admin";
                taiKhoan.Email = "admin123@gmail.com";
                taiKhoan.DiaChi = "Thành Phố Hồ Chí Minh";
                taiKhoan.SoDienThoai = "0123456789";
                db.TaiKhoan.Add(taiKhoan); db.SaveChanges();
            }
            List<SanPham> lst = db.SanPham.ToList();
            return View(lst);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}