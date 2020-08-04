using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class BaoCao2bDonThuInfo
    {
        public int DonThuID { get; set; }
        public int KetQuaID { get; set; }        
        public int LoaiKetQuaID { get; set; }
        public int HuongGiaiQuyetID { get; set; }
        public int TrangThaiDonID { get; set; }
        public int NhomKNID { get; set; }
        public DateTime NgayNhapDon { get; set; }
        public int LoaiKhieuTo1ID { get; set; }
        public int LoaiKhieuTo2ID { get; set; }
        public int LoaiKhieuTo3ID { get; set; }
        public int LoaiKhieuToID { get; set; }
        public int CoQuanID { get; set; }
        public int ThamQuyenID { get; set; }
        public int SoLuong { get; set; }
        public int SoLan { get; set; }
        public DateTime NgayRaKQ { get; set; }
        public DateTime NgayThuLy { get; set; }
        public int CQTiepNhanID { get; set; }

        public int ThamQuyenCQXuLy { get; set; }
        public int ThamQuyenCQChuyenDon { get; set; }
    }
}
