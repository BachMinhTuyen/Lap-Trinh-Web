using FashionShop.Models;
using FashionShop.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsFormsApplication2.Utilities;

namespace FashionShop.Controllers
{
    public class AuthController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();

        [Route("dang-ky")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("dang-ky")]
        public ActionResult Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                if (!db.TaiKhoan.Any(r => r.UserName == register.TenNguoiDung))
                {
                    string passwordHash = Password.Create_SHA256(register.MatKhau);
                    var user = new TaiKhoan()
                    {
                        UserName = register.TenNguoiDung,
                        Password = passwordHash,
                        TenNguoiDung = register.HoTen,
                        DiaChi = register.DiaChi,
                        Email = register.Email,
                        SoDienThoai = register.SoDienThoai,
                        VaiTro = "User"
                    };
                    
                    db.TaiKhoan.Add(user);
                    db.SaveChanges();
                    TempData["Success"] = "Đăng ký thành công!";
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Register", "Auth");
        }

        [Route("dang-nhap")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("dang-nhap")]
        public ActionResult Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var nguoiDung = db.TaiKhoan.FirstOrDefault(t => t.UserName == login.TenNguoiDung);
                if (nguoiDung != null)
                {
                    string passwordHash = nguoiDung.Password;
                    if (Password.verify(login.MatKhau, passwordHash, "sha256") == true)
                    {
                        Session["User"] = nguoiDung;

                        if (nguoiDung.VaiTro == "User")
                        {
                            TempData["Success"] = "Chào mừng bạn đã trở lại!";
                            return RedirectToAction("Index", "Home");
                        }
                        else if (nguoiDung.VaiTro == "Admin")
                        {
                            TempData["Success"] = "Chào mừng bạn đã trở lại!";
                            return Redirect("/Admin/Dashboard/Index");
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied", "Home");
                        }
                    }
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        [Route("doi-mat-khau")]
        public ActionResult ChangePassword(FormCollection form)
        {
            return View();
        }
    }
}