using Nhom14_DoAn_CongNgheWeb.Connection;
using Nhom14_DoAn_CongNgheWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Controllers
{
    public class ShopeController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: Shope
      
        public ActionResult Index()
        {
            return View();
        }
        public List<Cart> LayGioHang()
        {
            List<Cart> lstgiohang = Session["GioHang"] as List<Cart>;
            if (lstgiohang == null)
            {
                lstgiohang = new List<Cart>();
                Session["GioHang"] = lstgiohang;


            }
            return lstgiohang;
        }
        //public ActionResult ThemGioHang(int ms, string strURL)
        //{
        //    List<Cart> lstgiohang = LayGioHang();
        //    Cart sanpham = lstgiohang.Find(sp => sp.masanpham == ms);
        //    if (sanpham == null)
        //    {
        //        sanpham = new Cart(ms);
        //        lstgiohang.Add(sanpham);
        //        return Redirect(strURL);

        //    }
        //    else
        //    {
        //        sanpham.soluong++;
        //        return Redirect(strURL);
        //    }


        //}
        public ActionResult ThemGioHang(int id, int Soluong)
        {
            if (Session["GioHang"] == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart(id) { Product = db.SANPHAMs.Find(id), soluong = Soluong });
                Session["GioHang"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["GioHang"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].soluong += Soluong;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new Cart(id) { Product = db.SANPHAMs.Find(id), soluong = Soluong });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["GioHang"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<Cart> cart = (List<Cart>)Session["GioHang"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.MAHANG.Equals(id))
                    return i;
            return -1;
        }
        private int Tongsoluong()
        {
            int tsl = 0;
            List<Cart> lstgiohang = Session["GioHang"] as List<Cart>;
            if (lstgiohang != null)
            {
                tsl = lstgiohang.Sum(sq => sq.soluong);

            }
            return tsl;

        }
        private double TongThanhTien()
        {
            double ttt = 0;
            List<Cart> lstgiohang = Session["GioHang"] as List<Cart>;
            if (lstgiohang != null)
            {
                ttt += lstgiohang.Sum(sp => sp.dThanhTien);

            }
            return ttt;

        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("IndexHome", "Home");

            }
            List<Cart> lstgiohang = LayGioHang();
            ViewBag.TongSoLuong = Tongsoluong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstgiohang);


        }
        public ActionResult GioHangpartial()
        {
            ViewBag.TongSoLuong = Tongsoluong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView("IndexHome", "Home");

        }
        public ActionResult XoaGioHang(int MaSP)
        {
            List<Cart> lstgiohang = LayGioHang();
            Cart sp = lstgiohang.Single(s => s.masanpham == MaSP);
            if (sp != null)
            {
                lstgiohang.RemoveAll(s => s.masanpham == MaSP);
                return RedirectToAction("GioHang", "Shope");

            }
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("IndexHome", "Home");


            }
            return RedirectToAction("GioHang", "Shope");

        }
        public ActionResult XoaGioHang_All()
        {
            List<Cart> lstgiohang = LayGioHang();
            lstgiohang.Clear();
            return RedirectToAction("IndexHome", "Home");

        }
        public ActionResult CapNhatGioHang(int MaSP, FormCollection f)
        {
            List<Cart> lstgiohang = LayGioHang();
            Cart sp = lstgiohang.Single(s => s.masanpham == MaSP);
            if (sp != null)
            {
                sp.soluong = int.Parse(f["txtSoLuong"].ToString());


            }
            return RedirectToAction("GioHang", "Shope");


        }
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["id"] == null || Session["id"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("IndexHome", "Home");
            }
            //Thêm đơn hàng
            HOADON ddh = new HOADON();
            KHACHHANG kh = new KHACHHANG();
            kh.MAKH= int.Parse(Session["id"].ToString());
            //KHACHHANG kh = (KHACHHANG)Session["id"];

            //KHACHHANG kh1 =kh1.int.Parse(Session["id"].ToString());
            List<Cart> gh = LayGioHang();
            ddh.MAKH = kh.MAKH;
            ddh.NGAYXUATHD = DateTime.Now;
            db.HOADONs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CHITIETHD ctDH = new CHITIETHD();
                ctDH.MAHD = ddh.MAHD;
                ctDH.MAHANG = item.masanpham;
                ctDH.SOLUONG = item.soluong;
                ctDH.GIAGOC = (decimal)item.gia;
                ctDH.THANHTIEN = (decimal)item.dThanhTien;
                db.CHITIETHDs.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("IndexHome", "Home");
        }
    }
}