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
            ViewBag.UserName = UserName;
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
        public ActionResult ConfirmBill(string maHoaDon, string UserName)
        {
            HoaDon hoaDon = db.HoaDon.First(x => x.MaHoaDon == maHoaDon);
            hoaDon.TrangThaiDonHang = "Đang vận chuyển";
            db.SaveChanges();
            return RedirectToAction("ViewBills", "Bill", new { UserName = UserName });
        }
        public ActionResult CancelBill(string maHoaDon, string UserName)
        {
            HoaDon hoaDon = db.HoaDon.First(x => x.MaHoaDon == maHoaDon);
            hoaDon.TrangThaiDonHang = "Đơn hàng bị huỷ";
            db.SaveChanges();
            return RedirectToAction("ViewBills", "Bill", new { UserName = UserName });
        }
    }
}