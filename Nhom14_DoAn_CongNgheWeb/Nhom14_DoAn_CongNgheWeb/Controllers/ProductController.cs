using Nhom14_DoAn_CongNgheWeb.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Controllers
{
    public class ProductController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Product
        public ActionResult IndexProduct(int Id)
        {
            var lstproduct = db.SANPHAMs.Where(n => n.MAHANG == Id).FirstOrDefault();
            return View(lstproduct);
        }
    }
}