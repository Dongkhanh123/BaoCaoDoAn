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
    
    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            this.CHITIETHDs = new HashSet<CHITIETHD>();
        }
    
        public int MAHD { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<int> MANV { get; set; }
        public string NOIGIAO { get; set; }
        public Nullable<decimal> TONGTIEN { get; set; }
        public Nullable<System.DateTime> NGAYXUATHD { get; set; }
        public string emailship { get; set; }
        public string sodienthoai { get; set; }
        public string tenkhachhang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
