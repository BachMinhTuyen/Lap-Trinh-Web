using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Home
        public ActionResult Index() 
        {
            List<SanPham> lst = db.SanPham.ToList();
            return View(lst);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
    }
}