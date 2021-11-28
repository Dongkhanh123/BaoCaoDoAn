using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nhom14_DoAn_CongNgheWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                 new { controller = "Home", action = "IndexHome", id = UrlParameter.Optional },
                 new[] { "Nhom14_DoAn_CongNgheWeb.Controllers" }
            );
           
        }
    }
}
