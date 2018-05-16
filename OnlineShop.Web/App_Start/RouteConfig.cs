using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}" ,
                new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Login" ,
                url: "dang-nhap" ,
                defaults: new { controller = "Account" , action = "Login" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register" ,
                url: "dang-ky" ,
                defaults: new { controller = "Account" , action = "Register" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "About" ,
                url: "gioi-thieu" ,
                defaults: new { controller = "About" , action = "Index" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product Category" ,
                url: "{alias}/pc-{id}" ,
                defaults: new { controller = "Product" , action = "Category" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail" ,
                url: "{alias}/p-{id}" ,
                defaults: new { controller = "Product" , action = "Detail" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Search" ,
                url: "tim-kiem" ,
                defaults: new { controller = "Product" , action = "Search" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Tag" ,
                url: "tag/{tagId}" ,
                defaults: new { controller = "Product" , action = "GetProductsByTag" , tagId = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Page" ,
                url: "page/{alias}" ,
                defaults: new { controller = "Page" , action = "Index" , alias = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Contact" ,
                url: "lien-he" ,
                defaults: new { controller = "Contact" , action = "Index" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default" ,
                url: "{controller}/{action}/{id}" ,
                defaults: new { controller = "Home" , action = "Index" , id = UrlParameter.Optional } ,
                namespaces: new string[] { "OnlineShop.Web.Controllers" }
            );
        }
    }
}
