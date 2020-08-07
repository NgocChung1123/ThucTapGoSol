using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DanhMuc
{
    public class DMTinTucInfo
    {
        public int IDTinTuc { get; set; }
        public int IDLoaiTin { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public bool laTinHot { get; set; }
        public bool Public { get; set; }
        public string ImageUrl { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiSua { get; set; }
        public string TenLoaiTin { get; set; }
    }

    public class ChiTietTinTucInfo
    {
        public List<DMTinTucInfo> ChiTietTinTuc { get; set; }
        public DMLoaiTinInfo LoaiTinTuc { get; set; }
    }
}
