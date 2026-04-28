using FashionShop.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CartService
{
    public static int GetTotalQuantity()
    {
        var cart = HttpContext.Current.Session["Cart"] as List<CartVM>;
        return cart != null ? cart.Sum(x => x.iSoLuong) : 0;
    }

    public static double GetTotalPrice()
    {
        var cart = HttpContext.Current.Session["Cart"] as List<CartVM>;
        return cart != null ? cart.Sum(x => x.dThanhTien) : 0;
    }
}