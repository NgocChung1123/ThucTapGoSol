using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class BCTongHopXuLyInfo
    {
        public string STT { get; set; }
        public string TenCanBo { get; set; }
        public int SLTiepCongDan { get; set; }
        public int SLDonXuLy { get; set; }
        public int SLDonXuLyTrongHan { get; set; }
        public int SLDonXuLyQuaHan { get; set; }
        public int PhongBanID { get; set; }
        public int CanBoID { get; set; }

        public int Level { get; set; }
    }
}
