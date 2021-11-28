using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 new[] { "Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers" }
            );
        }
    }
}