using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class BaoCao2aDonThuInfo
    {
        public int DonThuID { get; set; }
        public DateTime NgayTiep { get; set; }
        public int LoaiKhieuTo1ID { get; set; }
        public int LoaiKhieuTo2ID { get; set; }
        public int LoaiKhieuTo3ID { get; set; }
        public int LoaiKhieuToID { get; set; }
        public bool GapLanhDao { get; set; }
        public DateTime NgayGapLanhDao { get; set; }
        public int CoQuanID { get; set; }
        public int SoLuong { get; set; }        
        public bool VuViecCu { get; set; }
        public int CQTiepNhanID { get; set; }
        public DateTime NgayNhapDon { get; set; }

        public int HuongXuLy { get; set; }
        public int KetQuaID { get; set; }
    }
}
