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
    public class AccountController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                if (!db.TaiKhoan.Any(r => r.UserName == register.UserName))
                {
                    string passwordHash = Password.Create_SHA256(register.ConfirmPassword);
                    var user = new TaiKhoan()
                    {
                        UserName = register.UserName,
                        Password = passwordHash,
                        TenNguoiDung = register.TenNguoiDung,
                        DiaChi = register.DiaChi,
                        Email = register.Email,
                        SoDienThoai = register.SoDienThoai,
                        VaiTro = "User"
                    };
                    
                    db.TaiKhoan.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Register", "Account");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var nguoiDung = db.TaiKhoan.FirstOrDefault(t => t.UserName == login.UserName);
                if (nguoiDung != null)
                {
                    string passwordHash = nguoiDung.Password;
                    if (Password.verify(login.Password, passwordHash, "sha256") == true)
                    {
                        Session["User"] = nguoiDung;

                        if (nguoiDung.VaiTro == "User")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (nguoiDung.VaiTro == "Admin")
                        {
                            return Redirect("/Admin/Dashboard/Index");
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied", "Home");
                        }
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
        public new ActionResult Profile()
        { 
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
                return View(taiKhoan);
            }    
        }
        [HttpPost]
        public new ActionResult Profile(FormCollection form)
        {
            TaiKhoan tk = Session["User"] as TaiKhoan;
            string userName = tk.UserName;
            TaiKhoan taiKhoan = db.TaiKhoan.First(x => x.UserName == userName);
            taiKhoan.TenNguoiDung = form["TenNguoiDung"];
            taiKhoan.DiaChi = form["DiaChi"];
            taiKhoan.Email = form["Email"];
            taiKhoan.SoDienThoai = form["SoDienThoai"];
            db.SaveChanges();
            Session["User"] = taiKhoan;
            return RedirectToAction("Profile", "Account");
        }
        public ActionResult ChangePassword(FormCollection form)
        {
            return View();
        }
    }
}