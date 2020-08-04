using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DanhMuc
{
    public class DMLoaiThuTucInfo
    {
        public int LoaiThuTucID { get; set; }
        public int ParentID { get; set; }
        public string TenLoaiThuTuc { get; set; }
        public string GhiChu { get; set; }
        public string CoSoPhapLy { get; set; }
        public bool Public { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }

        public string creater_name { get; set; }
        public string editer_name { get; set; }
        public string parent_name { get; set; }
        public int has_childs { get; set; }
    }
}
