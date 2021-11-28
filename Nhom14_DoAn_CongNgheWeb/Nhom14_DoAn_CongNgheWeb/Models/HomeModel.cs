using Nhom14_DoAn_CongNgheWeb.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom14_DoAn_CongNgheWeb.Models
{
    public class HomeModel
    {
        public List<SANPHAM> ListSANPHAM { get; set; }
        public List<DANHMUC> ListDANHMUC { get; set; }
        public List<MATHANG> ListMATHANG { get; set; }


    }
}