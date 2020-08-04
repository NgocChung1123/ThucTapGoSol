using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.LichTiepDan
{
    public class LichTiepDanInfo
    {
        public int IDLichTiep { get; set; }
        public int IDCoQuanTiep { get; set; }
        public int IDCanBoTiep { get; set; }
        public string NDTiep { get; set; }
        public DateTime NgayTiep { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }
        public bool Public { get; set; }

        public string CanBoTiep { get; set; }
        public string CoQuanTiep { get; set; }
        public string NgayTiep_Str { get; set; }
    }
}
