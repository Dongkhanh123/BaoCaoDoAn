using Nhom14_DoAn_CongNgheWeb.Connection;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers
{
    public class ChiTietHoaDonController : Controller
    {
        // GET: Admin/ChiTietHoaDon
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/NhanVien
        public ActionResult IndexCTHD(string Search, string currenFilter, int? page)
        {
            //var lstproduct = db.SANPHAMs.ToList();
            var lstproduct = new List<CHITIETHD>();
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
                lstproduct = db.CHITIETHDs.Where(n => n.MAHANG.ToString().Contains(Search)).ToList();

            }
            else
            {
                lstproduct = db.CHITIETHDs.ToList();

            }
            ViewBag.CurrentFilter = Search;

            int pageSize = 4;
            int PageNumber = (page ?? 1);
            lstproduct = lstproduct.OrderByDescending(n => n.MACHITIETHD).ToList();
            return View(lstproduct.ToPagedList(PageNumber, pageSize));
        }

    }
}