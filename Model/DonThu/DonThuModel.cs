using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DonThu
{
    public  class DonThuModel
    {
        //json convert attr
        public int XuLyDonID { get; set; }
        public string SoDonThu { get; set; }
        public int CoQuanID { get; set; }
        public string NgayNhapDonStr { get; set; }
        public string TenCanBoTiepNhan { get; set; }
        public string FileBase64 { get; set; }
        public string FileName { get; set; }
        public string TrangThaiDonThu { get; set; }
        public int TrangThaiDonID { get; set; }
        public string NgayVietDon { get; set; }
        public string CanBoTiepNhapID { get; set; }
        public string TenPhongBanTiepNhan { get; set; }
        public string NgayXuLy { get; set; }
        public string HanXuLy { get; set; }
        public string TenCanBoXuLy { get; set; }
        public string CoQuanChuyenDonDi { get; set; }
        public string TenCoQuanTiepNhan { get; set; }
        public string TenCoQuanGQ { get; set; }
        public string TenCoQuanXL { get; set; }
        public string NoiDungDon { get; set; }
        public int CoQuanBanHanhID { get; set; }
        public string HuongXuLy { get; set; }
        public int CoQuanXuLyID { get; set; }
        public string PhongBanXuLy { get; set; }
        public int CoQuanGiaiQuyetID { get; set; }
        public int SoTienPhaiThu { get; set; }
        public int SoDatPhaiThu { get; set; }
        public int SoDoiTuongBiXuLy { get; set; }
       // public string KetQuaThiHanhInfo { get; set; }
        public List< DoiTuongKhieuNaiModel> lsDoiTuongKN { get; set; }

        public List<XuLyDonInfo> lsFileYKienXuLy { get; set; }
        public List<FileHoSoInfo> lsFileQuyetDinhGD { get; set; }
        public NhomKhieuNaiModel NhomKNInfo { get; set; }

    }
}
