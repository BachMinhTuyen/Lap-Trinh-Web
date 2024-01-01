using FashionShop.Filters;
using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class StatisticalController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Admin/Statistical
        public ActionResult GetStatistics()
        {
            var doanhThuTheoNgay = db.HoaDon
        .GroupBy(hd => hd.NgayDatMuaHang.Date)
        .Select(g => new
        {
            Ngay = g.Key,
            TongDoanhThu = g.Sum(h => h.TongTien)
        })
        .OrderBy(x => x.Ngay)
        .ToList();

            return View(doanhThuTheoNgay);
        }
    }
}