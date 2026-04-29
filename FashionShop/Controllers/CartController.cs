using FashionShop.Models;
using FashionShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        FashionShopDBContext db = new FashionShopDBContext();

        [Route("gio-hang")]
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                return View();
            }

            List<CartVM> lst = GetCart();

            ViewBag.TenMau = TempData["TenMau"];
            ViewBag.TenSize = TempData["TenSize"];
            ViewBag.BienThe = db.BienTheSanPham.ToList();
            ViewBag.TongThanhTien = CartService.GetTotalPrice();
            return View(lst);
        }
        private List<CartVM> GetCart()
        {
            List<CartVM> lst = Session["Cart"] as List<CartVM>;
            if (lst == null)
            {
                lst = new List<CartVM>();
                Session["Cart"] = lst;
            }
            return lst;
        }

        [HttpPost]
        public JsonResult AddToCart(string maSanPham, string tenMau, string tenSize, int soLuong)
        {
            try
            {
                List<CartVM> lst = GetCart();

                var existingItem = lst.FirstOrDefault(s => s.sMaSanPham == maSanPham
                                              && s.sTenMau == tenMau
                                              && s.sTenSize == tenSize);

                if (existingItem == null)
                {
                    var sp = db.SanPham.FirstOrDefault(x => x.MaSanPham == maSanPham);
                    if (sp == null) return Json(new { success = false, message = "Sản phẩm không tồn tại!" });

                    var newItem = new CartVM(lst.Count + 1, maSanPham, tenMau, tenSize, soLuong);
                    lst.Add(newItem);
                }
                else
                {
                    existingItem.iSoLuong += soLuong; // Cộng thêm số lượng khách chọn
                }

                Session["Cart"] = lst;

                return Json(new
                {
                    success = true,
                    totalItems = lst.Sum(x => x.iSoLuong),
                    message = "Đã thêm sản phẩm vào giỏ hàng!"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateCart(int id, int soLuong)
        {
            var lst = GetCart();
            var item = lst.FirstOrDefault(c => c.iCartItem == id);

            if (item != null)
            {
                item.iSoLuong = soLuong;
                // Trả về tổng tiền mới để cập nhật UI bằng JS (nếu cần)
                var total = lst.Sum(x => x.dThanhTien);
                return Json(new { success = true, totalAmount = total });
            }

            return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
        }

        [HttpPost]
        public JsonResult DeleteCartItem(int id)
        {
            var lst = GetCart();
            var item = lst.FirstOrDefault(i => i.iCartItem == id);

            if (item != null)
            {
                lst.Remove(item);
                if (lst.Count == 0) Session["Cart"] = null;

                return Json(new
                {
                    success = true,
                    cartCount = lst.Sum(x => x.iSoLuong),
                    totalAmount = lst.Sum(x => x.dThanhTien)
                });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult BuyNow(string maSanPham, string tenMau, string tenSize, int soLuong)
        {
            try
            {
                CartVM buyNowItem = new CartVM(0, maSanPham, tenMau, tenSize, soLuong);

                if (buyNowItem != null && !string.IsNullOrEmpty(buyNowItem.sTenSanPham))
                {
                    Session["BuyNowItem"] = buyNowItem;
                    return Json(new { success = true, redirectUrl = Url.Action("Checkout", "Cart") });
                }
                return Json(new { success = false, message = "Sản phẩm không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            
        }

        [Route("thanh-toan-don-hang")]
        public ActionResult Checkout()
        {
            List<CartVM> checkoutList = new List<CartVM>();
            if (Session["BuyNowItem"] != null)
            {
                checkoutList.Add(Session["BuyNowItem"] as CartVM);
            }
            else if (Session["Cart"] != null)
            {
                checkoutList = Session["Cart"] as List<CartVM>;
            }
            if (checkoutList == null || checkoutList.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.TongSoLuong = checkoutList.Sum(x => x.iSoLuong);
            ViewBag.TongThanhTien = checkoutList.Sum(x => x.dThanhTien);

            TaiKhoan tk = Session["User"] as TaiKhoan;

            if (tk == null)
            {
                tk = new TaiKhoan();
            }

            return View(tk);
        }
        
        [HttpPost]
        public ActionResult ProcessOrder(string HoTen, string SoDienThoai, string Email, string DiaChiChiTiet,
                                string phuongThucThanhToan, string phuongThucVanChuyen, string GhiChu)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<CartVM> listToOrder = (Session["BuyNowItem"] != null)
                        ? new List<CartVM> { Session["BuyNowItem"] as CartVM }
                        : GetCart();

                    if (!listToOrder.Any()) return RedirectToAction("Index", "Home");

                    double phiVanChuyen = (phuongThucVanChuyen == "Giao hàng nhanh") ? 30000 : 15000;
                    double tongTienHang = listToOrder.Sum(x => x.dThanhTien);
                    double tongThanhToan = tongTienHang + phiVanChuyen;

                    string maHoaDon = "HD" + DateTime.Now.ToString("yyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

                    TaiKhoan tkSession = Session["User"] as TaiKhoan;
                    var currentTk = tkSession != null ? db.TaiKhoan.Find(tkSession.UserName) : null;

                    HoaDon hd = new HoaDon
                    {
                        MaHoaDon = maHoaDon,
                        NgayDatMuaHang = DateTime.Now,
                        TongTien = tongThanhToan,
                        PhiVanChuyen = phiVanChuyen,
                        GhiChu = GhiChu,
                        PhuongThucThanhToan = phuongThucThanhToan,
                        PhuongThucVanChuyen = phuongThucVanChuyen,
                        TrangThaiDonHang = HoaDon.OrderStatus.Pending,
                        TaiKhoan = currentTk
                    };
                    db.HoaDon.Add(hd);

                    foreach (var item in listToOrder)
                    {
                        var bienThe = db.BienTheSanPham.FirstOrDefault(b =>
                            b.SanPham.MaSanPham == item.sMaSanPham &&
                            b.TenMau == item.sTenMau &&
                            b.TenSize == item.sTenSize);

                        if (bienThe == null || bienThe.SoLuong < item.iSoLuong)
                            throw new Exception($"Sản phẩm {item.sTenSanPham} ({item.sTenSize}/{item.sTenMau}) đã hết hàng!");

                        string maChiTiet = "CTHD" + DateTime.Now.Ticks.ToString().Substring(10);

                        ChiTietHoaDon chiTiet = new ChiTietHoaDon
                        {
                            MaChiTietHoaDon = maChiTiet,
                            HoaDon = hd,
                            SoLuongSanPhamDaDat = item.iSoLuong,
                            ThanhTien = item.dThanhTien,
                            BienTheSanPham = bienThe
                        };
                         
                        bienThe.SoLuong -= item.iSoLuong;
                        db.ChiTietHoaDon.Add(chiTiet);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    if (Session["BuyNowItem"] != null) Session["BuyNowItem"] = null;
                    else Session["Cart"] = null;

                    return RedirectToAction("OrderSuccess", "Cart", new { maHoaDon = maHoaDon });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Lỗi đặt hàng: " + ex.Message;
                    return RedirectToAction("Checkout");
                }
            }
        }
        [Route("dat-hang-thanh-cong")]
        public ActionResult OrderSuccess(string maHoaDon)
        {
            var hoadon = db.HoaDon.FirstOrDefault(h => h.MaHoaDon == maHoaDon);
            if (hoadon == null) return RedirectToAction("Index", "Home");
            return View(hoadon);
        }
    }
}