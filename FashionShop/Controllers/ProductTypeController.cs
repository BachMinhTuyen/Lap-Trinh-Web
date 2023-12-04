using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ProductTypeController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: ProductType
        public ActionResult ProductTypePartial()
        {
            List<LoaiSanPham> lst = db.LoaiSanPham.Take(10).OrderBy(o => o.MaLoai).ToList();
            List<LoaiSanPham> skipItems = db.LoaiSanPham.OrderBy(o => o.MaLoai).Skip(10).ToList();
            ViewBag.SkipItems = skipItems;
            return View(lst);
        }
        public ActionResult GetInfoAboutProductType(string maLoaiSanPham)
        {
            List<SanPham> lst = db.SanPham.Where(r => r.LoaiSanPham.MaLoai == maLoaiSanPham).ToList();
            return View(lst);
        }
    }
}