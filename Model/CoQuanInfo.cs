using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class CoQuanInfo
    {
        public int CoQuanID { get; set; }
        public String TenCoQuan { get; set; }
        public int CoQuanChaID { get; set; }
        public int CapID { get; set; }
        public int ThamQuyenID { get; set; }
        public int TinhID { get; set; }
        public int HuyenID { get; set; }
        public int XaID { get; set; }
        public int WorkFlowID { get; set; }
        public int WFTienHanhTTID { get; set; }

        //public bool CapUBND { get; set; }
        //public bool CapThanhTra { get; set; }
        public bool SuDungPM { get; set; }
        public string MaCQ { get; set; }
        //public bool SuDungQuyTrinh { get; set; }
        //public bool QuyTrinhVanThuTiepNhan { get; set; }
        //public bool SuDungQuyTrinhGQ { get; set; }
        //public bool QTVanThuTiepDan { get; set; }
        public bool CQCoHieuLuc { get; set; }


        public int hasChild { get; set; }

        // ke hoach thanh tra anhnt

        public int KeHoachThanhTraID { get; set; }
        public string TenKeHoachThanhTra { get; set; }
        public int PhanLoaiThanhTraID1 { get; set; }
        public int NamThanhTra { get; set; }
        public int TongSoVuViec { get; set; }
        public int TongSoVuViecChongCheo { get; set; }
        public int TongSoVuViecHanhChinh { get; set; }
        public int TongSoVuViecChuyenNghanh { get; set; }
        public string KeHoachThanhTraIDString { get; set; }
        public string KeHoachThanhTraHanhChinhIDString { get; set; }
        public string KeHoachThanhTraChuyenNganhIDString { get; set; }
        public int PhanLoaiThanhTraID { get; set; }
        public int PhanLoaiThanhTraHanhChinhID { get; set; }
        public int PhanLoaiThanhTraChuyenNganhID { get; set; }
    }
}
