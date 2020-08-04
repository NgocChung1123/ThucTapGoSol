using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class KetQuaInfo
    {
        public int KetQuaID { get; set; }
        public int LoaiKetQuaID { get; set; }
        public int CanBoID { get; set; }
        public int CoQuanID { get; set; }
        public DateTime NgayRaKQ { get; set; }
        public string NgayRaKQStr { get; set; }
        public int SoTien { get; set; }
        public int SoDat { get; set; }
        public int SoNguoiDuocTraQuyenLoi { get; set; }
        public int SoDoiTuongBiXuLy { get; set; }
        public int SoDoiTuongDaBiXuLy { get; set; }
        public int XuLyDonID { get; set; }
        public string FileUrl { get; set; }
        public int PhanTichKQ { get; set; }
        public int KetQuaGQLan2 { get; set; }

        public string TenLoaiKetQua { get; set; }
        public string TenCanBo { get; set; }
        public string TenCoQuan { get; set; }
    }
}
