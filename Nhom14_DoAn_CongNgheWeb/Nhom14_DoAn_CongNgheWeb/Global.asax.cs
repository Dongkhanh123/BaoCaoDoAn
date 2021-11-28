using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nhom14_DoAn_CongNgheWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Application["SoNguoitruyCap"] = 0;
            Application["SoNguoiDangOline"] = 0;
        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["SoNguoitruyCap"] = (int)Application["SoNguoitruyCap"] + 1;
            Application["SoNguoiDangOline"] = (int)Application["SoNguoiDangOline"] + 1;
            Application.UnLock();

        }
        protected void Session_End()
        {
            Application.Lock();
            Application["SoNguoiDangOline"] = (int)Application["SoNguoiDangOline"] - 1;
            Application.UnLock();

        }
    }
}
