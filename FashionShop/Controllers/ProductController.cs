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
            SanPham sanPham = db.SanPham.FirstOrDefault(s => s.MaSanPham == maSanPham);   
            List<BienTheSanPham> bienThe = db.BienTheSanPham.Where(s => s.SanPham.MaSanPham == maSanPham).ToList();
            List<HinhAnh> hinhAnh = db.HinhAnh.Where(s => s.SanPham.MaSanPham == maSanPham).ToList();

            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            YeuThich yeuThich;
            if (taiKhoan == null)
            {
                yeuThich = null;
            }
            else
            {
                yeuThich = db.YeuThich.FirstOrDefault(s => s.SanPham.MaSanPham == sanPham.MaSanPham && s.TaiKhoan.UserName == taiKhoan.UserName);
            }
            

            //List<string> mauSac = db.BienTheSanPham.Where(s => s.SanPham.MaSanPham == maSanPham).Select(g => g.TenMau).Distinct().ToList();
            ViewBag.HinhAnh = hinhAnh;
            ViewBag.BienThe = bienThe;
            ViewBag.YeuThich = yeuThich;
            return View(sanPham);
        }
        private bool KiemTraMaYeuThich(string maYeuThich)
        {
            List<YeuThich> lst = db.YeuThich.ToList();
            foreach (var item in lst)
                if (item.MaYeuThich.Contains(maYeuThich))
                    return false; //đã tồn tại
            return true;// không tồn tại
        }
        public ActionResult FavouriteProduct(string maSanPham)
        {
            TaiKhoan tk = Session["User"] as TaiKhoan;
            TaiKhoan taiKhoan = db.TaiKhoan.FirstOrDefault(t => t.UserName == tk.UserName);

            SanPham sanPham = db.SanPham.First(x => x.MaSanPham == maSanPham);

            if (tk == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<YeuThich> lst = db.YeuThich.ToList();
            int maYT = 0;
            if (lst.Count() == 0)
            {
                YeuThich yeuThich = new YeuThich();
                yeuThich.MaYeuThich = "YT" + maYT;
                yeuThich.TaiKhoan = taiKhoan;
                yeuThich.SanPham = sanPham;

                db.YeuThich.Add(yeuThich);
                db.SaveChanges();
                return RedirectToAction("GetProductDetails", "Product", new { maSanPham = maSanPham });
            }

            foreach (var item in lst)
            {
                maYT = int.Parse(item.MaYeuThich.Substring(2, item.MaYeuThich.Length - 2));
                if (KiemTraMaYeuThich("YT" + maYT))
                {
                    YeuThich yeuThich = new YeuThich();
                    yeuThich.MaYeuThich = "YT" + maYT;
                    yeuThich.TaiKhoan = taiKhoan;
                    yeuThich.SanPham = sanPham;

                    db.YeuThich.Add(yeuThich);
                    db.SaveChanges();
                    return RedirectToAction("GetProductDetails", "Product", new { maSanPham = maSanPham });
                    //break;
                }
                maYT++;
            }

            int index = 0;
            foreach(var item in lst)
            {
                int temp = int.Parse(item.MaYeuThich.Substring(2, item.MaYeuThich.Length - 2));
                if (temp > index) 
                    index = temp;
            }

            YeuThich y = new YeuThich();
            y.MaYeuThich = "YT" + maYT;
            y.TaiKhoan = taiKhoan;
            y.SanPham = sanPham;

            db.YeuThich.Add(y);
            db.SaveChanges();

            return RedirectToAction("GetProductDetails", "Product", new { maSanPham = maSanPham });
        }
        public ActionResult NotFavouriteProduct(string maSanPham)
        {
            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            SanPham sanPham = db.SanPham.First(x => x.MaSanPham == maSanPham);
            if (taiKhoan == null)
            {
                return RedirectToAction("Login", "Account");
            }
            YeuThich yeuThich = db.YeuThich.FirstOrDefault(s => s.SanPham.MaSanPham == sanPham.MaSanPham && s.TaiKhoan.UserName == taiKhoan.UserName);

            db.YeuThich.Remove(yeuThich);
            db.SaveChanges();
            return RedirectToAction("GetProductDetails", "Product", new { maSanPham = maSanPham });
        }
        public ActionResult FavouriteProductList(int page = 1)
        {
            TaiKhoan taiKhoan = Session["User"] as TaiKhoan;
            if (taiKhoan == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<YeuThich> lst = db.YeuThich.Where(x => x.TaiKhoan.UserName == taiKhoan.UserName).ToList();

            //Paging
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = lst.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(lst);
        }
    }
}