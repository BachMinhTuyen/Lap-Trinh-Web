using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FashionShop.Controllers
{
    public class BillController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();

        [Route("lich-su-don-hang")]
        public ActionResult ViewBills(string UserName)
        {
            ViewBag.UserName = UserName;
            List<HoaDon> lst = db.HoaDon.Where(h =>h.TaiKhoan.UserName == UserName).ToList();
            return View(lst);
        }
        public ActionResult Details(string maHoaDon)
        {
            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            if (taiKhoan == null) return RedirectToAction("Login", "Auth");
            
            var chiTietList = db.ChiTietHoaDon
                                .Include(c => c.HoaDon)
                                .Include(c => c.HoaDon.TaiKhoan)
                                .Include(c => c.BienTheSanPham.SanPham)
                                .Where(c => c.HoaDon.MaHoaDon == maHoaDon)
                                .ToList();

            if (!chiTietList.Any())
            {
                return HttpNotFound();
            }

            ViewBag.HoaDon = chiTietList.First().HoaDon;
            ViewBag.UserName = taiKhoan.UserName;

            return View(chiTietList);
        }
        public ActionResult ConfirmBill(string maHoaDon, string UserName)
        {
            HoaDon hoaDon = db.HoaDon.First(x => x.MaHoaDon == maHoaDon);
            hoaDon.TrangThaiDonHang = HoaDon.OrderStatus.Shipping;
            db.SaveChanges();
            return RedirectToAction("ViewBills", "Bill", new { UserName = UserName });
        }
        public ActionResult CancelBill(string maHoaDon, string UserName)
        {
            HoaDon hoaDon = db.HoaDon.First(x => x.MaHoaDon == maHoaDon);
            hoaDon.TrangThaiDonHang = HoaDon.OrderStatus.Cancelled;
            db.SaveChanges();
            return RedirectToAction("ViewBills", "Bill", new { UserName = UserName });
        }
    }
}