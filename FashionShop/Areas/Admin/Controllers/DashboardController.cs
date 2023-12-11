using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Filters;
using FashionShop.Models;

namespace FashionShop.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class DashboardController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAccountInfo()
        {
            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            return View(taiKhoan);
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return Redirect("/Account/Login");
        }
        public ActionResult Welcome()
        {
            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            return View(taiKhoan);
        }
    }
}