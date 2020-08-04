using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class DoiTuongKNInfo
    {
        public int DoiTuongKNID { get; set; }
        public String HoTen { get; set; }
        public String CMND { get; set; }
        public int HuyenID { get; set; }
        public int TinhID { get; set; }
        public int DanTocID { get; set; }
        public int QuocTichID { get; set; }
        public string SoDienThoai { get; set; }

        public int GioiTinh { get; set; }
        public String NgheNghiep { get; set; }
        public int XaID { get; set; }
        public String DiaChiCT { get; set; }

        public int NhomKNID { get; set; }
        public string TenTinh { get; set; }
        public string TenHuyen { get; set; }
        public string TenXa { get; set; }        
        public string TenQuocTich { get; set; }
        public string TenDanToc { get; set; }
    }
}
