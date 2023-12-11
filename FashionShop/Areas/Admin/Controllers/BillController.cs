using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Admin/Bill
        public ActionResult GetAllBill()
        {
            List<HoaDon> hd = db.HoaDon.ToList();
            return View(hd);
        }
        [HttpPost]
        public ActionResult GetAllBill(string txtInput_ID)
        {
            List<HoaDon> hd = db.HoaDon.Where(x => x.MaHoaDon == txtInput_ID).ToList();
            return View(hd);
        }
        public ActionResult Details(string maHoaDon)
        {
            List<ChiTietHoaDon> lst = db.ChiTietHoaDon.Where(h => h.HoaDon.MaHoaDon == maHoaDon).ToList();
            HoaDon hd = db.HoaDon.First(h => h.MaHoaDon == maHoaDon);
            ViewBag.TongThanhToan = double.Parse(hd.TongTien.ToString());
            return View(lst);
        }
    }
}