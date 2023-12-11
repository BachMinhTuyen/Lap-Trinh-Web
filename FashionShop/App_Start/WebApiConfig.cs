using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FashionShop
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Loại bỏ định dạng XML và chỉ sử dụng định dạng JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
