using System;
using System.Collections.Generic;

namespace Com.Gosol.CMS.Model.DonThu
{
    public class DonThuInfo
    {
        public int STT { get; set; }
        public int ID { get; set; }
        public int XuLyDonID { get; set; }
        public string SoDonThu { get; set; }
        public string NgayBanHanh { get; set; }
        public string NgayTiepNhan { get; set; }
        public string CoQuanTiepNhan { get; set; }
        public string CanBoTiepNhan { get; set; }
        public string PhanLoaiDon { get; set; }
        public string NoiDungDon { get; set; }
        public string DoiTuongKhieuNai { get; set; }
        public int CoQuanXuLyID { get; set; }
        public string CoQuanXuLy { get; set; }
        public string PhongBanXuLy { get; set; }
        public string CanBoXuLy { get; set; }
        public string TrangThaiDonThu { get; set; }
        public string NguoiDaiDien { get; set; }
        public string CongKhai { get; set; }
        public string DiaChi { get; set; }
        public string FileQuyetDinh { get; set; }
        public int CoQuanID { get; set; }
        public int CoQuanBanHanhID { get; set; }
        public string CoQuanBanHanh { get; set; }


        public int TrangThaiDonID { get; set; }

        //moi
        public string NgayXuLyStr { get; set; }
        public int HuongXuLyID { get; set; }
        public string HuongXuLy { get; set; }
        public string KQTenLoaiKetQua { get; set; }
        public string KQTenCoQuan { get; set; }
        public string KQNgayRaKQStr { get; set; }
        public string KQSoTien { get; set; }
        public string KQSoDat { get; set; }
        public string KQFileUrl { get; set; }
        public string TH_TenCoQuanThiHanh { get; set; }
        public string TH_NgayThiHanhStr { get; set; }
        public string TH_TienDaThu { get; set; }

        public int CoQuanGiaiQuyetID { get; set; }
        public string CoQuanGiaiQuyet { get; set; }
        public int SoTienPhaiThu { get; set; }
        public int SoDatPhaiThu { get; set; }
        public int SoDoiTuongBiXuLy { get; set; }

        public int XemTruocFile { get; set; }
        public string TenQuyetDinh { get; set; }
        public List<DonThuInfo> ListFile { get; set; }
        public DateTime NgayUp { get; set; }
        public string NgayUp_Str { get; set; }
        public string TenFile { get; set; }
        public string FileURL { get; set; }
    }
}
