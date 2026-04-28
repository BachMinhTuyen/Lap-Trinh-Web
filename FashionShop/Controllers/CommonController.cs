using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class CommonController : Controller
    {
        [ChildActionOnly]
        public ActionResult Header()
        {
            ViewBag.TongSoLuong = CartService.GetTotalQuantity();
            ViewBag.TongTienGioHang = CartService.GetTotalPrice();

            return PartialView("_PartialPage_Header");
        }
    }
}