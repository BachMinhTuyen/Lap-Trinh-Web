using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult RevenueChart(DateTime? fromDate, DateTime? toDate)
        {
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                // Mặc định lấy dữ liệu cho 30 ngày gần nhất nếu không có giá trị nhập vào
                fromDate = DateTime.Now.Date.AddDays(-30);
                toDate = DateTime.Now.Date;
            }

            var query = db.HoaDon
            .Where(hd => DbFunctions.TruncateTime(hd.NgayDatMuaHang) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(hd.NgayDatMuaHang) <= DbFunctions.TruncateTime(toDate))
            .GroupBy(hd => DbFunctions.TruncateTime(hd.NgayDatMuaHang))
            .Select(g => new { Ngay = g.Key, TongTien = g.Sum(h => h.TongTien) })
            .OrderBy(g => g.Ngay)
            .ToList();

            var formattedQuery = query.Select(item => new { Ngay = item.Ngay?.ToString("yyyy-MM-dd"), item.TongTien });

            ViewBag.DoanhThuTheoNgay = formattedQuery;


            return View();
        }
    }
}