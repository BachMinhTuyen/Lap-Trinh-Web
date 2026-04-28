using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FashionShop.Controllers
{
    public class ProductTypeController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        [Route("danh-muc-san-pham")]
        public ActionResult GetInfoAboutProductType(string maLoaiSanPham, int page = 1)
        {
            List<SanPham> lst = db.SanPham.Where(r => r.LoaiSanPham.MaLoai == maLoaiSanPham).ToList();
            ViewBag.MaLoaiSanPham = maLoaiSanPham;
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