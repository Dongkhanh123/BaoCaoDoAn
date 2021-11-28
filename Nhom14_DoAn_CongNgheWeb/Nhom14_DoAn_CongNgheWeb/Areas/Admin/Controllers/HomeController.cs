using Nhom14_DoAn_CongNgheWeb.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/Home
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = db.NHANVIENs.Where(s => s.EMAIL.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().HOTEN;
                    Session["Email"] = data.FirstOrDefault().EMAIL;
                    Session["id"] = data.FirstOrDefault().MANV;
                    return RedirectToAction("IndexAdmin", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("IndexAdmin");
                }
            }
            return View();
        }
        
        public ActionResult Ajax()
        {
            return View();
        }
        public ActionResult NhanVienPartial()
        {
            var lstNhanVien = db.NHANVIENs.ToList();
            return PartialView(lstNhanVien);
        }
        public ActionResult XemChiTiet(int ms)
        {
            NHANVIEN sach = db.NHANVIENs.Single(s => s.MANV == ms);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }


    }
}