using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DanhMuc
{
    public class DMThuTucInfo
    {
        public int ThuTucID { get; set; }
        public int LoaiThuTucID { get; set; }
        public string NDThuTuc { get; set; }
        public int Order { get; set; }
        public int Creater { get; set; }
        public DateTime CreateDate { get; set; }
        public int Editer { get; set; }
        public DateTime EditDate { get; set; }

        public string creater_name { get; set; }
        public string editer_name { get; set; }
        public string loaithutuc_name { get; set; }
        public string FileDinhKem { get; set; }


        //join
        public string CoSoPhapLy { get; set; }
    }
}
