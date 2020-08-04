using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DanhMuc
{
    public class DMLinhVucInfo
    {
        public int IDLinhVuc { get; set; }
        public string TenLinhVuc { get; set; }
        public string GhiChu { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public bool Public { get; set; }
    }
}
