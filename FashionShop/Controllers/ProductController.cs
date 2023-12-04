using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Product
        public ActionResult GetAllProduct()
        {
            List<SanPham> lst = db.SanPham.ToList();
            return View(lst);
        }
        public ActionResult GetNewProduct()
        {
            //List<SanPham> lst = db.SanPham.OrderByDescending(o => o.MaSanPham).Take(25).ToList();
            List<SanPham> lst = db.SanPham.ToList();
            return View(lst);
        }
        public ActionResult SearchProduct(string txtBox_SearchInput)
        {
            List<SanPham> lst = db.SanPham.Where(s => s.TenSanPham.Contains(txtBox_SearchInput)).ToList();
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