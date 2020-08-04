using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class CanBoJoinInfo
    {
        public int CanBoID { get; set; }
        public string TenCanBo { get; set; }
        public DateTime NgaySinh { get; set; }
        public int GioiTinh { get; set; }
        public string DiaChi { get; set; }        
        public int CoQuanID { get; set; }
        public int QuyenKy { get; set; }
        public string TenCoQuan { get; set; }
        public String Email { get; set; }
        public String DienThoai { get; set; }
    }
}
