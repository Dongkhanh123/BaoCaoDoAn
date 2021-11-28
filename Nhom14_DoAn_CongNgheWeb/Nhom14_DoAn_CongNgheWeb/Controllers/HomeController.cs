using Nhom14_DoAn_CongNgheWeb.Connection;
using Nhom14_DoAn_CongNgheWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Controllers
{
    public class HomeController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Home
        public ActionResult IndexHome()
        {
            HomeModel obj = new HomeModel();
            obj.ListSANPHAM = db.SANPHAMs.ToList();
            obj.ListDANHMUC = db.DANHMUCs.ToList();
            return View(obj);

        }
        public ActionResult IndexGioiThieu()
        {
          
            return View();

        }
        public ActionResult IndexLienHe()
        {

            return View();

        }
        public ActionResult IndexLoad()
        {
            HomeModel obj = new HomeModel();
            obj.ListSANPHAM = db.SANPHAMs.ToList();
            return View(obj);


        }
        

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KHACHHANG _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.KHACHHANGs.FirstOrDefault(s => s.EMAIL == _user.EMAIL);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    _user.NGAYSINH = DateTime.Now;
                    //_user.NGAYDK = DateTime.Now;
                    db.KHACHHANGs.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5
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
                var data = db.KHACHHANGs.Where(s => s.EMAIL.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().TENKH;
                    Session["Email"] = data.FirstOrDefault().EMAIL;
                    Session["id"] = data.FirstOrDefault().MAKH;
                    return RedirectToAction("IndexHome","Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

      
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}