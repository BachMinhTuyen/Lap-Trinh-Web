using FashionShop.Models;
using FashionShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();

        [Route("")]
        public ActionResult Index() 
        {
            List<SanPham> lst = db.SanPham.ToList();
            return View(lst);
        }

        [ChildActionOnly]
        public ActionResult BrandPartial()
        {
            var brands = db.ThuongHieu.ToList();
            return PartialView("_components/_BrandPartial", brands);
        }

        [ChildActionOnly]
        public ActionResult CategoryPartial()
        {
            var categories = db.LoaiSanPham.ToList();
            return PartialView("_components/_CategoryPartial", categories);
        }

        [Route("ve-chung-toi")]
        public ActionResult AboutUs()
        {
            return View();
        }
        [Route("lien-he")]
        public ActionResult ContactUs()
        {
            return View();
        }
        [Route("tu-choi-quyen-truy-cap")]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}