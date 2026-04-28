using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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
        [Route("thuong-hieu-san-pham")]
        public ActionResult GetInfoAboutBrand(string maThuongHieu, int page = 1)
        {
            List<SanPham> lst = db.SanPham.Where(r => r.ThuongHieu.MaThuongHieu == maThuongHieu).ToList();

            ViewBag.MaThuongHieu = maThuongHieu;
            //Paging
            int NoOfRecordPerPage = 12;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = lst.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(lst);
        }
    }
}