//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nhom14_DoAn_CongNgheWeb.Connection
{
    using System;
    using System.Collections.Generic;
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CHITIETHDs = new HashSet<CHITIETHD>();
        }
    
        public int MAHANG { get; set; }
        public string TENHANG { get; set; }
        public string HANGSX { get; set; }
        public string MOTANGAN { get; set; }
        public string MOTADAYDU { get; set; }
        public string NUOCSX { get; set; }
        public Nullable<double> GIASP { get; set; }
        public Nullable<double> GIAMGIA { get; set; }
        public string GHICHU { get; set; }
        public string HINH { get; set; }
        public Nullable<int> MAMATHANG { get; set; }
        public Nullable<int> MADANHMUC { get; set; }
        public Nullable<int> TYPEID { get; set; }
    
        public virtual DANHMUC DANHMUC { get; set; }
        public virtual MATHANG MATHANG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }
    }
}
