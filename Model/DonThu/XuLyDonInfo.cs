using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class XuLyDonInfo
    {
        public int XuLyDonID { get; set; }
        public string SoDonThu { get; set; }
        public int DonThuID { get; set; }
        public int SoLan { get; set; }
        public DateTime NgayNhapDon {get;set;}
        //public DateTime NgayCapNhat { get; set; }
        public DateTime NgayQuaHan { get; set; }
        public int NguonDonDenID { get; set; }
        public int CQChuyenDonID { get; set; }
        public int CQChuyenDonDenID { get; set; }
        public string SoCongVan { get; set; }
        public DateTime NgayChuyenDon { get; set; }
        public DateTime NgayCQKhacChuyenDonDen { get; set; }
        public Boolean GapLanhDao { get; set; }
        public DateTime NgayGapLanhDao { get; set; }
        public Boolean ThuocThamQuyen { get; set; }
        public Boolean DuDieuKien { get; set; }
        public int HuongGiaiQuyetID { get; set; }
        public string NoiDungHuongDan { get; set; }
        public int CQTiepNhanID { get; set; }
        public int CanBoXuLyID { get; set; }
        public int CanBoKyID { get; set; }
        public string CQDaGiaiQuyetID { get; set; }
        public int CQGiaiQuyetTiepID { get; set; }
        public int TrangThaiDonID { get; set; }
        public int PhanTichKQID { get; set; }
        public int CanBoTiepNhapID { get; set; }
        public int CoQuanID { get; set; }
        public string CQNgoaiHeThong { get; set; }
        public string TenCanBoPhuTrach { get; set; }
        public string TenCoQuanGiaiQuyet { get; set; }

        public int DuAnID { get; set; }
        public DateTime NgayXuLy { get; set; }
        public DateTime NgayThuLy { get; set; }
        public int CBDuocChonXL { get; set; }
        public int QTTiepNhanDon { get; set; }
        public int DonThuGocID { get; set; }


        //join
        //public String Ghichu { get; set; }
        public int TiepDanKhongDonID { get; set; }
        public Boolean VuViecCu { get; set; }

        public string MaHoSoMotCua { get; set; }
        public string SoBienNhanMotCua { get; set; }
        public DateTime NgayHenTraMotCua { get; set; }

        //Y kien xu ly
        public string FileUrl { get; set; }
        public string TenFile { get; set; }
        public string YKienXuLy { get; set; }
        public string TenCanBoXuLy { get; set; }
        public string HuongXuLy { get; set; }

        public string NgayXuLyStr { get; set; }

        // Y KienGiaiQuyet
        public string YKienGiaiQuyet { get; set; }
        public string TenCanBoGiaiQuyet { get; set; }
        public string HuongGiaiQuyet { get; set; }
        public int CanBoGiaiQuyetID { get; set; }
        public DateTime NgayGiaiQuyet { get; set; }

        public string NgayGiaiQuyetStr { get; set; }

        public string SoCVBaoCaoGQ { get; set; }
        public DateTime NgayCVBaoCaoGQ { get; set; }

        //file y kien xl
        public int FileYKienXuLyID { get; set; }
        public string TenFileYKienXL { get; set; }
        public string TomTat { get; set; }
        public int NguoiUp { get; set; }
        public DateTime NgayUp { get; set; }
        public string NgayUps { get; set; }
        public string FileURL { get; set; }

        public string FileBase64 { get; set; }
        // van ban don doc
        public string TenCoQuanRaVanBan { get; set; }
        public string TenChuDon { get; set; }
        public string NoiDung { get; set; }
        public bool ViewerVBDonDoc { get; set; }
    }
}
