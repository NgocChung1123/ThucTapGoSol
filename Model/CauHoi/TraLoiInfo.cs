using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.CauHoi
{
    public class TraLoiInfo
    {
        public int IDTraLoi { get; set; }
        public int IDCauHoi { get; set; }
        public string NDTraLoi { get; set; }
        public int CreaterId { get; set; }
        public string CreaterName { get; set; }
        public DateTime CreateDate { get; set; }
        public string EditerName { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public bool Public { get; set; }
        public DateTime NgayTraLoi { get; set; }
        public DateTime NgaySuaTraLoi { get; set; }

        // cau hoi
        public string NDCauHoi { get; set; }
        public bool IsCauHoiHopLe { get; set; }
        public int IDLinhVuc { get; set; }
        public string TenLinhVuc { get; set; }

        public string NgayHoi_Str { get; set; }
        public string NgayTraLoi_Str { get; set; }

        public string nguoi_hoi { get; set; }
        public string nguoi_traloi { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
    }
}
