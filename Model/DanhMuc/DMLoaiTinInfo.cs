using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DanhMuc
{
    public class DMLoaiTinInfo
    {
        public int IDLoaiTin { get; set; }
        public int ParentID { get; set; }
        public string TenLoaiTin { get; set; }
        public string GhiChu { get; set; }
        public bool Public { get; set; }
        public int Order { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public string TenLoaiTinCha { get; set; }

    }
}
