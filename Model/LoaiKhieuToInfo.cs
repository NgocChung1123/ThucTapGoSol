using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class LoaiKhieuToInfo
    {
        public int LoaiKhieuToID { get; set; }
        public String TenLoaiKhieuTo { get; set; }
        public int LoaiKhieuToCha { get; set; }
        public int Cap { get; set; }

        public int hasChild { get; set; }
    }
}
