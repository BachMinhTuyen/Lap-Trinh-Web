using FashionShop.Filters;
using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Admin/Product
        public ActionResult GetProduct()
        {
            List<SanPham> p = db.SanPham.ToList();
            return View(p);
        }
        [HttpPost]
        public ActionResult GetProduct(string txtInput_ID)
        {
            List<SanPham> p = db.SanPham.Where(x => x.MaSanPham == txtInput_ID).ToList();
            return View(p);
        }
        public ActionResult Details(string maSanPham)
        {
            List<BienTheSanPham> bienThe = db.BienTheSanPham.Where(x => x.SanPham.MaSanPham == maSanPham).ToList();
            ViewBag.SanPham = db.SanPham.First(x => x.MaSanPham == maSanPham);
            return View(bienThe);
        }
        public ActionResult CreateProduct()
        {
            List<ThuongHieu> brands = db.ThuongHieu.ToList();
            ViewBag.LoaiSanPham = db.LoaiSanPham.ToList();
            return View(brands);
        }
        [HttpPost]
        public ActionResult CreateProduct(FormCollection form)
        {
            List<SanPham> lst = db.SanPham.ToList();
            int i = 0;
            foreach (var item in lst)
            {
                int iNew = int.Parse(item.MaSanPham.Substring(2, item.MaSanPham.Length - 2));
                if (iNew > i)
                    i = iNew;
            }

            int index = i;
            //int index = lst.Count();
            int maSP = index + 1;
            
            SanPham sanPham = new SanPham()
            {
                MaSanPham = "SP" + maSP,
                TenSanPham = form["Inputtxt_TenSanPham"],
                GiaSanPham = double.Parse(form["Inputtxt_GiaSanPham"]),
                KhuyenMai = double.Parse(form["Inputtxt_KhuyenMai"]) / 100,
                AnhDaiDien = form["Inputtxt_FileImage"],
                MoTaSanPham = form["Inputtxt_MoTa"]
            };

            string maThuongHieu = form["select_ThuongHieu"];
            string maLoaiSanPham = form["select_LoaiSanPham"];

            ThuongHieu thuongHieu = db.ThuongHieu.First(t => t.MaThuongHieu == maThuongHieu);
            LoaiSanPham loaiSP = db.LoaiSanPham.First(t => t.MaLoai == maLoaiSanPham);
            sanPham.ThuongHieu = thuongHieu;
            sanPham.LoaiSanPham = loaiSP;

            db.SanPham.Add(sanPham);
            db.SaveChanges();

            return RedirectToAction("GetProduct", "Product");
        }
        public ActionResult CreateProductCustom(string maSanPham)
        {
            SanPham sanPham = db.SanPham.First(s => s.MaSanPham == maSanPham);
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult CreateProductCustom(string maSanPham, FormCollection form)
        {
            List<BienTheSanPham> danhSachBienThe = db.BienTheSanPham.ToList();
            int i = 0;
            foreach (var item in danhSachBienThe)
            {
                int iNew = int.Parse(item.MaBienThe.Substring(2, item.MaBienThe.Length - 2));
                if (iNew > i)
                    i = iNew;
            }

            int index = i;
            int maBT = index + 1;

            List<BienTheSanPham> lst = db.BienTheSanPham.Where(s => s.SanPham.MaSanPham == maSanPham).ToList();

            string tenMau = form["Inputtxt_TenMau"];
            string tenSize = form["Inputtxt_TenSize"];
            foreach (var item in lst)
            {
                if (string.Compare(tenMau, item.TenMau) == 0 && string.Compare(tenSize, item.TenSize) == 0)
                {
                    ViewBag.Message = "Thất Bại";
                    return RedirectToAction("Details", "Product", new { maSanPham = maSanPham });
                }
            }

            BienTheSanPham bt = new BienTheSanPham()
            {
                MaBienThe = "BT" + maBT,
                TenMau = form["Inputtxt_TenMau"],
                TenSize = form["Inputtxt_TenSize"],
                SoLuong = int.Parse(form["Inputtxt_SoLuong"])
            };
            SanPham sanPham = db.SanPham.Single(s => s.MaSanPham == maSanPham);
            bt.SanPham = sanPham;

            db.BienTheSanPham.Add(bt);
            db.SaveChanges();
            return RedirectToAction("Details", "Product", new { maSanPham = maSanPham });
        }
        public ActionResult DeleteProductCustom(string maSanPham, string maBienThe)
        {
            BienTheSanPham bienThe = db.BienTheSanPham.First(s => s.MaBienThe == maBienThe);
            db.BienTheSanPham.Remove(bienThe);
            db.SaveChanges();
            return RedirectToAction("Details", "Product", new { maSanPham = maSanPham });
        }
        public ActionResult UpdateProduct()
        {
            List<SanPham> p = db.SanPham.ToList();
            return View(p);
        }
        public ActionResult DeleteProduct(string maSanPham)
        {
            List<BienTheSanPham> lst = db.BienTheSanPham.Where(s => s.SanPham.MaSanPham == maSanPham).ToList();
            foreach(var item in lst)
            {
                db.BienTheSanPham.Remove(item);
            }
            db.SaveChanges();

            SanPham sp = db.SanPham.First(s => s.MaSanPham == maSanPham);
            db.SanPham.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("GetProduct", "Product");
        }
    }
}