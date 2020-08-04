using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class CanBoInfo
    {
        public int CanBoID { get; set; }
        public string TenCanBo { get; set; }
        public DateTime NgaySinh { get; set; }
        public int GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public int CoQuanID { get; set; }
        public int QuyenKy { get; set; }
        public String Email { get; set; }
        public String DienThoai { get; set; }
        public int ChucVuID { get; set; }
        public int CheckCapNhat { get; set; }
        public int RoleID { get; set; }
        public int PhongBanID { get; set; }
        public string TenCoQuan { get; set; }
        public string RoleName { get; set; }
        public int VaiTroXacMinh { get; set; }
        public string TenChucVu { get; set; }
        //public string phongban { get; set; }
    }
}
