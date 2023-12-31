using FashionShop.Filters;
using FashionShop.Models;
using FashionShop.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsFormsApplication2.Utilities;

namespace FashionShop.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class AccountController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Admin/Account
        public ActionResult GetAllAccount()
        {
            List<TaiKhoan> taiKhoan = db.TaiKhoan.ToList();
            return View(taiKhoan);
        }
        [HttpPost]
        public ActionResult GetAllAccount(string txtInput_ID)
        {
            List<TaiKhoan> taiKhoan = db.TaiKhoan.Where(x => x.UserName == txtInput_ID).ToList();
            return View(taiKhoan);
        }
        public ActionResult CreateAccount()
        {
            return View();
        }
        public ActionResult DeleteAccount(string username)
        {
            
                TaiKhoan tk = db.TaiKhoan.First(x=>x.UserName == username);
                tk.VaiTro = "undefined";
                db.SaveChanges();
                return RedirectToAction("GetAllAccount", "Account");

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
                        VaiTro = "Admin"
                    };

                    db.TaiKhoan.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("GetAllAccount", "Account");
                }
            }
            return RedirectToAction("CreateAccount", "Account");
        }
    }
}