using Nhom14_DoAn_CongNgheWeb.Connection;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexKhachHang(string Search, string currenFilter, int? page)
        {
            //var lstproduct = db.SANPHAMs.ToList();
            var lstproduct = new List<KHACHHANG>();
            if (Search != null)
            {
                page = 1;

            }
            else
            {
                Search = currenFilter;

            }
            if (!string.IsNullOrEmpty(Search))
            {
                lstproduct = db.KHACHHANGs.Where(n => n.TENKH.Contains(Search)).ToList();

            }
            else
            {
                lstproduct = db.KHACHHANGs.ToList();

            }
            ViewBag.CurrentFilter = Search;

            int pageSize = 4;
            int PageNumber = (page ?? 1);
            lstproduct = lstproduct.OrderByDescending(n => n.MAKH).ToList();
            return View(lstproduct.ToPagedList(PageNumber, pageSize));
        }

    }
}