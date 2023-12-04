using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class BrandsController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Brands
        public ActionResult BrandsPartial()
        {
            List<ThuongHieu> lst = db.ThuongHieu.Take(10).OrderBy(o => o.MaThuongHieu).ToList();
            List<ThuongHieu> skipItems = db.ThuongHieu.OrderBy(o => o.MaThuongHieu).Skip(10).ToList();
            ViewBag.SkipItems = skipItems;
            return View(lst);
        }
        public ActionResult GetInfoAboutBrand(string maThuongHieu)
        {
            List<SanPham> lst = db.SanPham.Where(r => r.ThuongHieu.MaThuongHieu == maThuongHieu).ToList();
            return View(lst);
        }
    }
}