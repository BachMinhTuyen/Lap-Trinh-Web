using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class UserController : Controller
    {

        FashionShopDBContext db = new FashionShopDBContext();

        [Route("ho-so-nguoi-dung")]
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
    }
}