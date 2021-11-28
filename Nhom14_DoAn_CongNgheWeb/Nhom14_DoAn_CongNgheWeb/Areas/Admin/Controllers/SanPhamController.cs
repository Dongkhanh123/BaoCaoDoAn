using Nhom14_DoAn_CongNgheWeb.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Nhom14_DoAn_CongNgheWeb.Common;
using static Nhom14_DoAn_CongNgheWeb.Common.ListtoDataTableConverter;
using PagedList;
using OfficeOpenXml;
using System.Drawing;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/SanPham
        public ActionResult IndexSanPham(string Search, string currenFilter, int? page)
        {
            //var lstproduct = db.SANPHAMs.ToList();
            var lstproduct = new List<SANPHAM>();
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
                lstproduct = db.SANPHAMs.Where(n => n.TENHANG.Contains(Search)).ToList();

            }
            else
            {
                lstproduct = db.SANPHAMs.ToList();

            }
            ViewBag.CurrentFilter = Search;

            int pageSize = 4;
            int PageNumber = (page ?? 1);
            lstproduct = lstproduct.OrderByDescending(n => n.MAHANG).ToList();
            return View(lstproduct.ToPagedList(PageNumber, pageSize));
        }
       

    [HttpGet]
    public ActionResult CreateSanPham()
    {
        this.LoadData();
        return View();
    }
    [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateSanPham(SANPHAM obj, HttpPostedFileBase uploadhinh)
        {
            db.SANPHAMs.Add(obj);
            db.SaveChanges();


            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = int.Parse(db.SANPHAMs.ToList().Last().MAHANG.ToString());

                string _FileName = Path.GetFileName(uploadhinh.FileName);
                string _path = Path.Combine(Server.MapPath("~/IPHONE"), _FileName);
                uploadhinh.SaveAs(_path);

                SANPHAM unv = db.SANPHAMs.FirstOrDefault(x => x.MAHANG == id);
                unv.HINH = _FileName;
                db.SaveChanges();
            }


            return RedirectToAction("IndexSanPham");

            //    try
            //    {
            //        if (obj.ImageUpload != null)
            //        {
            //        string _FileName = Path.GetFileName(uploadhinh.FileName);
            //        string _path = Path.Combine(Server.MapPath("~/IPHONE"), _FileName);
            //        uploadhinh.SaveAs(_path);
            //        unv.hinh = _FileName;
            //        //    string fileName = Path.GetFileNameWithoutExtension(obj.ImageUpload.FileName);
            //        //    string extension = Path.GetExtension(obj.ImageUpload.FileName);
            //        //    fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            //        //    obj.HINH = fileName;
            //        //    obj.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/DIEN THOAI/IPHONE"), fileName));

            //    }
            //        db.SANPHAMs.Add(obj);
            //        db.SaveChanges();
            //        return RedirectToAction("IndexSanPham");
            //    }
            //    catch (Exception)
            //    {
            //        return View();
            //    }


            //return View(obj);
        }
        public ActionResult XemSanPham(int Id)
        {
            SANPHAM empl = db.SANPHAMs.Single(e => e.MAHANG == Id);
            if (empl == null)
            {
                return HttpNotFound();

            }
            return View(empl);
            //var lstProduct = db.SANPHAMs.Where(n => n.MAHANG == Id).FirstOrDefault();
            //return View(lstProduct);
        }
        [HttpGet]
        public ActionResult DeleteSanPham(int Id = 0)
        {
            SANPHAM empl = db.SANPHAMs.Single(d => d.MAHANG == Id);
            if (empl == null)
            {

                return HttpNotFound();

            }
            return View(empl);


            //var obj = db.tbl_Deparment.Where(n => n.DeptId == id).FirstOrDefault();
            //return View(obj);
        }
        [HttpPost, ActionName("DeleteSanPham")]
        public ActionResult DeleteSanPhameConfirm(int Id = 0)
        {

            SANPHAM empl = db.SANPHAMs.Single(d => d.MAHANG == Id);
            if (empl == null)
            {

                return HttpNotFound();

            }
            db.Entry(empl).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("IndexSanPham", "SanPham");

        }
        public ActionResult SuaSanPham(int Id = 0)
        {
            SANPHAM dept = /*db.tbl_Deparment.Single(d => d.DeptId == id);*/ db.SANPHAMs.Single(d => d.MAHANG == Id);
            if (dept == null)
            {
                return HttpNotFound();


            }
            return View(dept);
        }
        [HttpPost]
        public ActionResult SuaSanPham(SANPHAM dept)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Attach(dept);
                db.Entry(dept).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexSanPham", "SanPham");

            }
            return View(dept);
        }
        void LoadData()
        {
            Common cm = new Common();
            var lstcat = db.DANHMUCs.ToList();
            ListtoDataTableConverter abc = new ListtoDataTableConverter();
            DataTable dtCategory = abc.ToDataTable(lstcat);
            ViewBag.ListCategory = cm.ToSelectList(dtCategory, "MADANHMUC", "TENDANHMUC");

            var lstBrand = db.MATHANGs.ToList();
            DataTable dtBrands = abc.ToDataTable(lstBrand);
            ViewBag.ListBrands = cm.ToSelectList(dtBrands, "MAMATHANG", "TENMATHANG");

            //loai san pham
            List<ProductType> lstproductTy = new List<ProductType>();

            ProductType objProductType = new ProductType();
            objProductType.Id = 1;
            objProductType.Name = "Giảm Giá Sốc";
            lstproductTy.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 2;
            objProductType.Name = "Đề Xuất";
            lstproductTy.Add(objProductType);

            DataTable dtProdcutType = abc.ToDataTable(lstproductTy);
            ViewBag.ProductType = cm.ToSelectList(dtProdcutType, "id", "Name");
        }
        public void ExportToExcel()
        {
            List<SANPHAM> lst = db.SANPHAMs.ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "TÊN BẢNG";
            ws.Cells["B1"].Value = "NHÂN VIÊN";
            ws.Cells["A2"].Value = "BẢNG BÁO CÁO ";
            ws.Cells["B2"].Value = "BÁO CÁO 1";
            ws.Cells["A3"].Value = "NGÀY";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy}at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MAHANG";
            ws.Cells["B6"].Value = "TENHANG";
            ws.Cells["C6"].Value = "HANGSX";
            ws.Cells["D6"].Value = "MOTANGAN";
            ws.Cells["E6"].Value = "MOTADAYDU";
            ws.Cells["F6"].Value = "NUOCSX";
            ws.Cells["G6"].Value = "GIASP";
            ws.Cells["H6"].Value = "GIAMGIA";
            ws.Cells["I6"].Value = "GHICHU";
            ws.Cells["J6"].Value = "HINH";
            ws.Cells["K6"].Value = "MAMATHANG";
            ws.Cells["L6"].Value = "MADANHMUC";
            ws.Cells["M6"].Value = "TYPEID";
            int rowStasr = 7;
            foreach (var item in lst)
            {
                if (item.MAHANG < 30)
                {
                    ws.Row(rowStasr).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStasr).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }
                ws.Cells[string.Format("A{0}", rowStasr)].Value = item.MAHANG;
                ws.Cells[string.Format("B{0}", rowStasr)].Value = item.TENHANG;
                ws.Cells[string.Format("C{0}", rowStasr)].Value = item.HANGSX;
                ws.Cells[string.Format("D{0}", rowStasr)].Value = item.MOTANGAN;
                ws.Cells[string.Format("E{0}", rowStasr)].Value = item.MOTADAYDU;
                ws.Cells[string.Format("F{0}", rowStasr)].Value = item.NUOCSX;
                ws.Cells[string.Format("G{0}", rowStasr)].Value = item.GIASP;
                ws.Cells[string.Format("H{0}", rowStasr)].Value = item.GIAMGIA;
                ws.Cells[string.Format("I{0}", rowStasr)].Value = item.GHICHU;
                ws.Cells[string.Format("J{0}", rowStasr)].Value = item.HINH;
                ws.Cells[string.Format("K{0}", rowStasr)].Value = item.MAMATHANG;
                ws.Cells[string.Format("L{0}", rowStasr)].Value = item.MADANHMUC;
                ws.Cells[string.Format("M{0}", rowStasr)].Value = item.TYPEID;
                rowStasr++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filname=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }


    
}
}