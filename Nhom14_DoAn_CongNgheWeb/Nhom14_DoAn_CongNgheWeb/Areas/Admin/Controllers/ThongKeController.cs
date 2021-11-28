using Nhom14_DoAn_CongNgheWeb.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Admin/ThongKe
        public ActionResult IndexThongKe()
        {
            ViewBag.SoNguoitruyCap = HttpContext.Application["SoNguoitruyCap"].ToString(); ;
            ViewBag.SoNguoiDangOline = HttpContext.Application["SoNguoiDangOline"].ToString();
            ViewBag.DoanhThu = ThongKeTongDoanhThu();
            ViewBag.ThongKeDonHang = ThongKeDonHang();
            ViewBag.ThongKeNhanVien = ThongKeNhanVien();
            ViewBag.ThongKeKhachHang = ThongKeKhachHang();
            ViewBag.ThongKeSanPham = ThongKeSanPham();

            //ViewBag.DoanhThuThang = ThongKeTongDoanhThuThang();
            return View();
        }
        public double ThongKeDonHang()
        {
           
            double lisddh = db.HOADONs.Count();
            return lisddh;
            

        }
        public double ThongKeSanPham()
        {

            double lisddh = db.SANPHAMs.Count();
            return lisddh;


        }
        public double ThongKeKhachHang()
        {

            double lisddh = db.KHACHHANGs.Count();
            return lisddh;


        }
        public double ThongKeNhanVien()
        {
            double lisnv = db.NHANVIENs.Count();
            return lisnv;
        }
        public decimal ThongKeTongDoanhThuThang(int thang,int nam)
        {
            var lstdh = db.HOADONs.Where(n => n.NGAYXUATHD.Value.Month == thang && n.NGAYXUATHD.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in lstdh)
            {
                tongtien += decimal.Parse(item.CHITIETHDs.Sum(n => n.SOLUONG * n.GIAGOC).Value.ToString());
            }
            return tongtien;
            //decimal TongDoanhthu = db.CHITIETHDs.Sum(n => n.SOLUONG * n.GIAGOC).Value;
            //return TongDoanhthu;

        }
        public decimal ThongKeTongDoanhThu()
        {
            decimal TongDoanhthu = db.CHITIETHDs.Sum(n =>n.THANHTIEN).Value;
            return TongDoanhthu;
        }
        protected override void Dispose(bool disposing)
        { if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}