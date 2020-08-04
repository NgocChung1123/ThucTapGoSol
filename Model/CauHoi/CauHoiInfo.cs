using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.CauHoi
{
    public class CauHoiInfo
    {
        public int IDCauHoi { get; set; }
        public int IDLinhVuc { get; set; }
        public string NDCauHoi { get; set; }
        public bool IsCauHoiHopLe { get; set; }
        public string GhiChu { get; set; }
        public int CreaterId { get; set; }
        public string CreaterName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }

        // linh vuc 
        public string TenLinhVuc { get; set; }
        public int IDTraLoi { get; set; }
        public string NguoiTraLoi { get; set; }
        public string NDTraLoi { get; set; }

        public string TenCanBo { get; set; }
        public bool Public { get; set; }
    }
}
