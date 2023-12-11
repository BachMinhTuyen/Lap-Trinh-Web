using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class BillController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Bill
        public ActionResult ViewBills(string UserName)
        {
            List<HoaDon> lst = db.HoaDon.Where(h =>h.TaiKhoan.UserName == UserName).ToList();
            return View(lst);
        }
        public ActionResult Details(string maHoaDon)
        {
            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            ViewBag.UserName = taiKhoan.UserName;
            List<ChiTietHoaDon> lst = db.ChiTietHoaDon.Where(h => h.HoaDon.MaHoaDon == maHoaDon).ToList();
            return View(lst);
        }
    }
}