using Nhom14_DoAn_CongNgheWeb.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom14_DoAn_CongNgheWeb.Models
{
    public class Cart
    {
        QL_CUAHANGDIENTHOAIEntities db = new QL_CUAHANGDIENTHOAIEntities();
        public SANPHAM Product { get; set; }
        public int masanpham { get; set; }
        public string tensanpham { get; set; }
        public string hinh { get; set; }
        public double gia { get; set; }
        public double giamgia { get; set; }
        //public double giagiam { get; set; }
        public int soluong { get; set; }
        public double dThanhTien
        { 
            get { return soluong * (gia- gia * (giamgia/100)); }
            
            

        }
        public Cart(int Masanpham)
        {

            masanpham = Masanpham;
            SANPHAM sach = db.SANPHAMs.Single(s => s.MAHANG == masanpham);
            tensanpham = sach.TENHANG;
            hinh = sach.HINH;
            gia = double.Parse(sach.GIASP.ToString());
            giamgia = double.Parse(sach.GIAMGIA.ToString());
            //giagiam = double.Parse(sach.GIAMGIA.ToString());
            soluong = 1;

        }

    }
}