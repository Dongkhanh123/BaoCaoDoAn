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
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public int MANV { get; set; }
        public string HOTEN { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string GIOITINH { get; set; }
        public string SDT { get; set; }
        public string DCHI { get; set; }
        public Nullable<System.DateTime> NGAYVL { get; set; }
        public Nullable<decimal> LUONG { get; set; }
        public Nullable<decimal> PHUCAP { get; set; }
        public string CHUCVU { get; set; }
        public string Password { get; set; }
        public string EMAIL { get; set; }
        public string Quyen { get; set; }
        public string hinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}
