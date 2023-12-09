using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Product
        public ActionResult GetAllProduct(int page = 1)
        {
            List<SanPham> lst = db.SanPham.ToList();

            //Paging
            int NoOfRecordPerPage = 15;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = lst.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(lst);
        }
        public ActionResult GetAllProduct_List(int page = 1)
        {
            List<SanPham> lst = db.SanPham.ToList();

            //Paging
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = lst.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(lst);
        }
        public ActionResult GetNewProduct(int page = 1)
        {
            List<SanPham> lst = db.SanPham.ToList();

            //Paging
            int NoOfRecordPerPage = 15;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = lst.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(lst);
        }
        public ActionResult SearchProduct(string txtBox_SearchInput, int page = 1)
        {
            List<SanPham> lst = db.SanPham.Where(s => s.TenSanPham.Contains(txtBox_SearchInput)).ToList();

            //Paging
            int NoOfRecordPerPage = 15;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = lst.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(lst);
        }
        public ActionResult GetProductDetails(string maSanPham)
        {
            SanPham lst = db.SanPham.FirstOrDefault(s => s.MaSanPham == maSanPham);   
            List<BienTheSanPham> bienThe = db.BienTheSanPham.Where(s => s.SanPham.MaSanPham == maSanPham).ToList();
            List<HinhAnh> hinhAnh = db.HinhAnh.Where(s => s.SanPham.MaSanPham == maSanPham).ToList();

            List<string> mauSac = db.BienTheSanPham.Where(s => s.SanPham.MaSanPham == maSanPham).Select(g => g.TenMau).Distinct().ToList();
            ViewBag.HinhAnh = hinhAnh;
            ViewBag.BienThe = bienThe;
            return View(lst);
        }
    }
}