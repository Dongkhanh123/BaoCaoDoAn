using Nhom14_DoAn_CongNgheWeb.Connection;
using Nhom14_DoAn_CongNgheWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Controllers
{
    public class TimKiemController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/TimKiem
        public ActionResult KQTimkiem(string sTuKhoa)
        {
            var lstsanpham = db.SANPHAMs.Where(n => n.TENHANG.Contains(sTuKhoa));
            return View(lstsanpham.OrderBy(n => n.TENHANG));
        }
       
    }
}