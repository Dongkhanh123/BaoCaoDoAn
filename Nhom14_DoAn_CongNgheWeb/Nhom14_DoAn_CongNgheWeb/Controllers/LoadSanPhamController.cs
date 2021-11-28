using Nhom14_DoAn_CongNgheWeb.Connection;
using Nhom14_DoAn_CongNgheWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Controllers
{
    public class LoadSanPhamController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: LoadSanPham
        public ActionResult IndexLoad()
        {
            HomeModel obj = new HomeModel();
            obj.ListSANPHAM = db.SANPHAMs.ToList();
            obj.ListDANHMUC = db.DANHMUCs.ToList();
            return View(obj);

            
        }
        public ActionResult DanhMucLoad(int Id)
        {
            HomeModel obj = new HomeModel();
            obj.ListMATHANG = db.MATHANGs.ToList();
            /* var lstProduct */
            obj.ListSANPHAM = db.SANPHAMs.Where(n => n.MADANHMUC == Id).ToList();
            return View(obj);

        }
    }
}