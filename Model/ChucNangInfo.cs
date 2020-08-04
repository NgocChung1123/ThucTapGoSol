using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class ChucNangInfo
    {
        public int ChucNangID { get; set; }
        public string TenChucNang { get; set; }
        public int ChucNangChaID { get; set; }
        public int Quyen { get; set; }

        //property dung trong hien thi dang tree sau nay, khong select tu db
        public int Level { get; set; }
        public bool HasChild { get; set; }
        public String TenChucNangCha { get; set; }
    }
}
