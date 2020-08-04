using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class DiaChiInfo
    {
        public int DiaChiID { get; set; }
        public String TenDiaChi { get; set; }
        public int DiaChiCha { get; set; }
        public int Cap { get; set; }
        public String TenDayDu { get; set; }

        public int hasChild { get; set; }
    }
}
