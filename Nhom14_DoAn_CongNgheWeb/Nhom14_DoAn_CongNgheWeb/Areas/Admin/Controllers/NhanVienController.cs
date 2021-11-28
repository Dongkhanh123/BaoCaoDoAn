using Nhom14_DoAn_CongNgheWeb.Connection;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/NhanVien
        public ActionResult IndexNhanVien(string Search, string currenFilter, int? page)
        {
            //var lstproduct = db.SANPHAMs.ToList();
            var lstproduct = new List<NHANVIEN>();
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
                lstproduct = db.NHANVIENs.Where(n => n.HOTEN.Contains(Search)).ToList();

            }
            else
            {
                lstproduct = db.NHANVIENs.ToList();

            }
            ViewBag.CurrentFilter = Search;

            int pageSize = 4;
            int PageNumber = (page ?? 1);
            lstproduct = lstproduct.OrderByDescending(n => n.MANV).ToList();
            return View(lstproduct.ToPagedList(PageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NHANVIEN nv, HttpPostedFileBase uploadhinh)
        {


            db.NHANVIENs.Add(nv);
            db.SaveChanges();


            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = int.Parse(db.NHANVIENs.ToList().Last().MANV.ToString());

                string _FileName = Path.GetFileName(uploadhinh.FileName);
                string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                uploadhinh.SaveAs(_path);

                NHANVIEN unv = db.NHANVIENs.FirstOrDefault(x => x.MANV == id);
                unv.hinh = _FileName;
                db.SaveChanges();
            }


            return RedirectToAction("IndexNhanVien");
        }
        public ActionResult XemNhanVien(int Id)
        {
            NHANVIEN empl = db.NHANVIENs.Single(e => e.MANV == Id);
            if (empl == null)
            {
                return HttpNotFound();

            }
            return View(empl);
            //var lstProduct = db.SANPHAMs.Where(n => n.MAHANG == Id).FirstOrDefault();
            //return View(lstProduct);
        }
        [HttpGet]
        public ActionResult DeleteNhanVien(int Id = 0)
        {
            NHANVIEN empl = db.NHANVIENs.Single(d => d.MANV == Id);
            if (empl == null)
            {

                return HttpNotFound();

            }
            return View(empl);


            //var obj = db.tbl_Deparment.Where(n => n.DeptId == id).FirstOrDefault();
            //return View(obj);
        }
        [HttpPost, ActionName("DeleteNhanVien")]
        public ActionResult DeleteNhanVienConfirm(int Id = 0)
        {

            NHANVIEN empl = db.NHANVIENs.Single(d => d.MANV == Id);
            if (empl == null)
            {

                return HttpNotFound();

            }
            db.Entry(empl).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("IndexNhanVien", "NhanVien");

        }
        public ActionResult SuaNhanVien(int Id = 0)
        {
            NHANVIEN dept = /*db.tbl_Deparment.Single(d => d.DeptId == id);*/ db.NHANVIENs.Single(d => d.MANV == Id);
            if (dept == null)
            {
                return HttpNotFound();


            }
            return View(dept);
        }
        [HttpPost]
        public ActionResult SuaNhanVien(NHANVIEN dept)
        {
            if (ModelState.IsValid)
            {
                db.NHANVIENs.Attach(dept);
                db.Entry(dept).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexNhanVien", "NhanVien");

            }
            return View(dept);
        }
    }
}