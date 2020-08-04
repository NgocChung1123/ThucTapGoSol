using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DonThu
{
    public class DonThuJson
    {
        public int DonThuID { get; set; }
        public int NhomKNID { get; set; }
        public int DoiTuongBiKNID { get; set; }
        public int LoaiKhieuToID { get; set; }
        public int LoaiKhieuTo1ID { get; set; }
        public int LoaiKhieuTo2ID { get; set; }
        public int LoaiKhieuTo3ID { get; set; }

        public string NoiDungDon { get; set; }

        public Boolean TrungDon { get; set; }
        //public int CoQuanID { get; set; }

        public int TinhID { get; set; }
        public int HuyenID { get; set; }
        public int XaID { get; set; }

        public String TenLoaiDoiTuong { get; set; }
        public int SoLanTrung { get; set; }

        //for show List
        public DateTime NgayNhapDon { get; set; }
        public string NgayNhapDonStr { get; set; }
        public DateTime NgayPhan { get; set; }
        public string NgayPhanStr { get; set; }
        public string NgayTiepNhan { get; set; }
        public string HoTen { get; set; }
        public string HoTenStr { get; set; }
        public string DiaChi { get; set; }
        public string DiaChiCT { get; set; }
        public string TenLoaiKhieuTo { get; set; }
        public string TenCanBo { get; set; }
        public string TenCoQuan { get; set; }
        public string TenXa { get; set; }
        public string TenHuyen { get; set; }
        public string TenTinh { get; set; }
        public int SoLan { get; set; }
        public int LanGiaiQuyet { get; set; }
        public int TrangThaiDonID { get; set; }
        public string TrangThaiDonThu { get; set; }
        public int CoQuanID { get; set; }
        public int XuLyDonID { get; set; }
        public int SoLuong { get; set; }
        public string CMND { get; set; }
        public string SoDonThu { get; set; }
        public DateTime NgayVietDon { get; set; }
        public DateTime NgayQuaHanGQ { get; set; }
        public int CanBoTiepNhapID { get; set; }
        public String LyDo { get; set; }
        public string NgayQuaHanGQStr { get; set; }
        public string TenPhongBanTiepNhan { get; set; }
        public DateTime NgayXuLy { get; set; }
        public string NgayXuLyStr { get; set; }
        public DateTime HanXuLy { get; set; }
        public string HanXuLyStr { get; set; }
        public string TenCanBoXuLy { get; set; }
        public DateTime NgayChuyenDon { get; set; }
        public DateTime NgayChuyenDonSangCQKhac { get; set; }
        public string NgayChuyenDonSangCQKhacStr { get; set; }
        public string CoQuanChuyenDonDi { get; set; }
        public string NgayChuyenDonStr { get; set; }
        public int VaiTro { get; set; }
        public string VaiTroStr { get; set; }
        public string CQDaGiaiQuyetID { get; set; }
        public string TenPhongBanXuLy { get; set; }
        public string HuongXuLy { get; set; }
        public string SoCongVan { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }

        public String TenLoaiKhieuTo1 { get; set; }
        public String TenLoaiKhieuTo2 { get; set; }
        public String TenLoaiKhieuTo3 { get; set; }

        public string TenHuongGiaiQuyet { get; set; }
        public string NoiDungHuongDan { get; set; }
        public string TenCanBoTiepNhan { get; set; }
        public string KetQuaGiaiQuyet { get; set; }

        public string CQDaGiaiQuyet { get; set; }
        public string TenLoaiKetQua { get; set; }

        public int NguonDonDen { get; set; }
        public string TenNguonDonDen { get; set; }
        public string TenCQChuyenDonDen { get; set; }
        public string TenCoQuanTiepNhan { get; set; }
        public string TenCoQuanGQ { get; set; }
        public string TenCoQuanXL { get; set; }

        public string SoCVBaoCaoGQ { get; set; }
        public DateTime NgayCVBaoCaoGQ { get; set; }
        public string NgayCVBaoCaoGQStr { get; set; }
        public string TenCQPhan { get; set; }

        public string YKienXuLy { get; set; }
        public string YKienGiaiQuyet { get; set; }
        public int DonThuGocID { get; set; }

        public List<DoiTuongKNInfo> lsDoiTuongKN { get; set; }
        public NhomKNInfo NhomKNInfo { get; set; }
        public List<XuLyDonInfo> lsFileYKienXuLy { get; set; }
        public KetQuaInfo KetQuaGQInfo { get; set; }
        public ThiHanhInfo KetQuaThiHanhInfo { get; set; }
    }
}
