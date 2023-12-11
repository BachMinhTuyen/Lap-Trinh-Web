using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FashionShop.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Kiểm tra vai trò của người dùng
            if (!IsUserInAdminRole(GetLoggedInUserName(filterContext)))
            {
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
            // Gọi phương thức cha để tiếp tục thực hiện action nếu người dùng có quyền
            base.OnActionExecuting(filterContext);
        }
        private string GetLoggedInUserName(ActionExecutingContext filterContext)
        {
            // Thực hiện logic để lấy tên người dùng đã đăng nhập từ session hoặc cookie
            // Ví dụ: 
            var userName = filterContext.HttpContext.Session["User"] as TaiKhoan;
            return userName.UserName;
        }
        private bool IsUserInAdminRole(string userName)
        {
            using (FashionShopDBContext db = new FashionShopDBContext())
            {
                TaiKhoan user = db.TaiKhoan.FirstOrDefault(u => u.UserName == userName && u.VaiTro == "Admin");
                return user != null;
            }
        }
    }
}