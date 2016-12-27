using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Wx.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.txt/{*pathInfo}");
            routes.MapRoute(
                name: "demo",
                url: "demo/{action}/{id}",
                defaults: new { controller = "demo", action = "SdkTest", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Html",
                url: "{controller}/{action}.html/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Json",
                url: "{controller}/{action}.json/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}