using FashionShop.Models;
using FashionShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();
        // GET: Cart
        public List<CartVM> GetCart()
        {
            List<CartVM> lst = Session["Cart"] as List<CartVM>;
            if (lst == null)
            {
                lst = new List<CartVM>();
                Session["Cart"] = lst;
            }
            return lst;
        }
        private int Total()
        {
            int sum = 0;
            List<CartVM> lst = Session["Cart"] as List<CartVM>;
            if (lst != null)
            {
                sum = lst.Sum(sp => sp.iSoLuong);
            }
            return sum;
        }
        private double TotalMoney()
        {
            double sum = 0;
            List<CartVM> lst = Session["Cart"] as List<CartVM>;
            if (lst != null)
            {
                sum = lst.Sum(sp => sp.dThanhTien);
            }
            return sum;
        }
        public ActionResult CartPartial()
        {
            ViewBag.TongSoLuong = Total();
            return View();
        }
        // Xem giỏ hàng
        public ActionResult GetCartInfo()
        {
            if (Session["Cart"] == null)
            {
                return View();
            }
            // Lấy thông tin sản phẩm trong giỏ hàng
            List<CartVM> lst = GetCart();

            ViewBag.TenMau = TempData["TenMau"];
            ViewBag.TenSize = TempData["TenSize"];
            ViewBag.BienThe = db.BienTheSanPham.ToList();
            ViewBag.TongThanhTien = TotalMoney();
            return View(lst);
        }
        public ActionResult AddToCart(string maSanPham, string tenMau, string tenSize, int soLuong, string stringURL)
        {
            stringURL = stringURL.TrimEnd(';');
            TempData["TenMau"] = tenMau;
            TempData["TenSize"] = tenSize;

            // Xem giỏ hàng
            List<CartVM> lst = GetCart();

            CartVM sanPham = lst.Find(s => s.sMaSanPham == maSanPham && s.sTenMau == tenMau && s.sTenSize == tenSize);
            int id = lst.Count;
            if (sanPham == null)
            {
                id++;
                sanPham = new CartVM(id, maSanPham, tenMau, tenSize, soLuong);
                lst.Add(sanPham);
                return Redirect(stringURL);
            }
            else
            {
                sanPham.iSoLuong++;
                return Redirect(stringURL);
            }
        }
        public ActionResult UpdateCart(int id, int soLuong)
        {
            List<CartVM> lst = GetCart();
            lst.Find(c => c.iCartItem == id).iSoLuong = soLuong;

            return RedirectToAction("GetCartInfo", "Cart");
        }
        public ActionResult DeleteCartItem(int id)
        {
            List<CartVM> list = GetCart();
            CartVM sp = list.Single(i => i.iCartItem == id);

            if (sp != null)
            {
                list.RemoveAll(s => s.iCartItem == id);
            }
            if (list.Count == 0)
            {
                list = null;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GetCartInfo", "Cart");
        }
        
        //Checkout
        public ActionResult Checkout()
        {
            TaiKhoan tk = Session["User"] as TaiKhoan;
            if (tk != null)
            {
                ViewBag.TongSoLuong = Total();
                ViewBag.TongThanhTien = TotalMoney();
                return View(tk);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult CheckoutWithOneProduct(string maSanPham, string tenMau, string tenSize, int soLuong)
        {
            TaiKhoan tk = Session["User"] as TaiKhoan;
            if (tk != null)
            {
                ViewBag.TenMau = tenMau;
                ViewBag.TenSize = tenSize;
                ViewBag.SoLuong = soLuong;
                ViewBag.SanPham = db.SanPham.First(s => s.MaSanPham == maSanPham);
                return View(tk);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public ActionResult Order(double tongTienThanhToan, string GhiChu, string phuongThucThanhToan,string phuongThucVanChuyen)
        {
            int count = db.HoaDon.Count();
            string maHoaDon = "HD" + (count + 1).ToString();

            // Khởi tạo thông tin hoá đơn
            HoaDon hd = new HoaDon();
            hd.MaHoaDon = maHoaDon;
            hd.NgayDatMuaHang = DateTime.Now;
            hd.TongTien = tongTienThanhToan;
            hd.GhiChu = GhiChu;
            hd.PhuongThucThanhToan = phuongThucThanhToan;
            hd.PhuongThucVanChuyen = phuongThucVanChuyen;

            TaiKhoan tk = Session["User"] as TaiKhoan;
            TaiKhoan TaiKhoan = db.TaiKhoan.FirstOrDefault(t => t.TenNguoiDung == tk.TenNguoiDung);
            hd.TaiKhoan = TaiKhoan;

            db.HoaDon.Add(hd);
            db.SaveChanges();

            // Lấy thông tin sản phẩm trong giỏ hàng
            List<CartVM> lst = GetCart();

            int index = 0;
            foreach (var item in  lst)
            {
                ChiTietHoaDon chiTiet = new ChiTietHoaDon();
                chiTiet.MaChiTietHoaDon = "CTHD" + index;
                chiTiet.HoaDon = hd;
                chiTiet.SoLuongSanPhamDaDat = item.iSoLuong;
                chiTiet.ThanhTien = item.dThanhTien;

                BienTheSanPham bienThe = db.BienTheSanPham.FirstOrDefault(b => b.TenMau == item.sTenMau && b.TenSize == item.sTenSize);
                chiTiet.BienTheSanPham = bienThe;

                SanPham sanPham = db.SanPham.FirstOrDefault(s => s.MaSanPham == item.sMaSanPham);

                db.ChiTietHoaDon.Add(chiTiet);
                index++;
            }
            db.SaveChanges();

            // Xoá giỏ hàng
            Session["Cart"] = null;

            return RedirectToAction("OrderSuccess", "Cart");
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}