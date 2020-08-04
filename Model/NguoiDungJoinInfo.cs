using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class NguoiDungJoinInfo
    {
        public int NguoiDungID { get; set; }
        public string TenNguoiDung { get; set; }
        public string MatKhau { get; set; }
        public string GhiChu { get; set; }
        public int TrangThai { get; set; }
        public int CanBoID { get; set; }
        public string TenCanBo { get; set; }
        public string TenCoQuan { get; set; }
    }
}
