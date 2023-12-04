using FashionShop.Models;
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
        public ActionResult Register(TaiKhoan taiKhoan, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                if (!db.TaiKhoan.Any(r => r.UserName == taiKhoan.UserName))
                {
                    string password = taiKhoan.Password;
                    if (String.Compare(taiKhoan.Password, frm["ConfirmPassword"]) == 0)
                    {
                        password = Password.Create_SHA256(frm["Password"]);
                        taiKhoan.Password = password;
                        taiKhoan.VaiTro = "User";

                        db.TaiKhoan.Add(taiKhoan);
                        db.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            return RedirectToAction("Register", "Account");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var nguoiDung = db.TaiKhoan.FirstOrDefault(t => t.Email == taiKhoan.Email);
                if (nguoiDung != null)
                {
                    if (nguoiDung.VaiTro == "User")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (nguoiDung.VaiTro == "Admin")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }    
                }
            }
            return RedirectToAction("Login", "Account");
        }
    }
}