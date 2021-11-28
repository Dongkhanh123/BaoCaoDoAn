using Nhom14_DoAn_CongNgheWeb.Connection;
using Nhom14_DoAn_CongNgheWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom14_DoAn_CongNgheWeb.Controllers
{
    public class ThanhToanHoaDonController : Controller
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        // GET: ThanhToanHoaDon
        public ActionResult IndexThanhToan()
        {
            if (Session["id"] == null)//id user
            {
                return RedirectToAction("Login", "Home");

            }
            else
            {
                var lstCart = (List<Cart>)Session["GioHang"];
                HOADON objod = new HOADON();
                //objod.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objod.MAKH = int.Parse(Session["id"].ToString());
                objod.NGAYXUATHD = DateTime.Now;
              
                db.HOADONs.Add(objod);
                db.SaveChanges();

                int intOrderId = objod.MAHD;
                List<CHITIETHD> lst = new List<CHITIETHD>();
                foreach (var item in lstCart)
                {
                    CHITIETHD obj = new CHITIETHD();
                    obj.SOLUONG = item.soluong;
                    obj.THANHTIEN = Convert.ToDecimal(item.dThanhTien);
                    obj.MAHD = intOrderId;
                    obj.MAHANG = item.Product.MAHANG;
                    lst.Add(obj);

                }
                db.CHITIETHDs.AddRange(lst);
                db.SaveChanges();


            }
            return View();
        }
        [HttpGet]
        public ActionResult Payment()
        {
            if (Session["id"] == null || Session["id"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("IndexHome", "Home");
            }
            var cart = Session["GioHang"];
            var list = new List<Cart>();
            if (cart != null)
            {
                list = (List<Cart>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            if (Session["id"] == null || Session["id"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("IndexHome", "Home");
            }
            var order = new HOADON();
            order.tenkhachhang = shipName;
            order.sodienthoai = mobile;
         
            //order.MANV = manhanvien;
            order.NOIGIAO = address;
            order.emailship = email;
            //order.TONGTIEN = tongtien;
            order.NGAYXUATHD = DateTime.Now;
           

            try
            {

                //Thêm Order               
                db.HOADONs.Add(order);
                db.SaveChanges();
                var id = order.MAHD;

                var cart = (List<Cart>)Session["GioHang"];

                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new CHITIETHD();
                    orderDetail.MAHANG = item.Product.MAHANG;
                    orderDetail.MAHD = id;
                    orderDetail.GIAGOC = Convert.ToDecimal(item.Product.GIASP);
                    orderDetail.SOLUONG = item.soluong;
                    orderDetail.NGAYDAT= DateTime.Now; 
                    orderDetail.NGAYGIAO= DateTime.Now;
                    orderDetail.THANHTIEN = Convert.ToDecimal(item.dThanhTien);
                    db.CHITIETHDs.Add(orderDetail);
                    db.SaveChanges();
                    total += (Convert.ToDecimal((item.Product.GIASP- item.Product.GIASP*(item.Product.GIAMGIA/100)) * item.soluong));
                 


                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
              
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

             

                new MailHelper().SendMail(email, "Đơn hàng mới từ cửa hàng điện thoại", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ cửa hàng điện thoại", content);
                //Xóa hết giỏ hàng
                Session["GioHang"] = null;
            }
            catch (Exception ex)
            {
                //ghi log
                return RedirectToAction("UnSuccess", "ThanhToanHoaDon");
            }
            return RedirectToAction("Success", "ThanhToanHoaDon");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult UnSuccess()
        {
            return View();
        }
       
    }
}