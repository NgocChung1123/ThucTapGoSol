using System;
using System.Web;

namespace Com.Gosol.CMS.Security
{
    public enum ChucNangEnum
    {
        QuanLyNguoiDungNhomNguoiDung = 3,
        QuanLyNguoiDung = 4,
        QuanLyNhomNguoiDung = 5,
        QuanLyChucNang = 6,
        KhaiBaoCanBo = 7,
        KhaiBaoBoPhanDonVi = 8,
        KhaiBaoThamSoHeThong = 9,
        SaoLuuVaPhucHoiDuLieu = 10,
        NhatKyHeThong = 11,
        ThayDoiGiaoDien = 12,
        DongBoDuLieu = 13,


        DanhMucDanToc = 22,
        DanhMucQuocTich = 23,
        DanhMucTinhHuyenXa = 24,
        DanhMucCoQuanDonVi = 27,
        DanhMucThamQuyen = 28,
        DanhMucChucVu = 29,
        DanhMucPhanLoaiThanhTra = 30,
        NVThanhTra = 31,
        TongHopKeHoachThanhTra = 32,
        LapKeHoachThanhTra = 33,
        DSKeHoachThanhTra = 34,
        DSKeHoachThanhTraCanDuyet = 35,

        NVTienHanhTT = 36,
        LapDoan_RaQDThanhTra = 37,
        TienHanhThanhTra = 38,
        DuyetKeHoachTienHanhTT = 39,
        DuyetBaoKetQuaTT = 40,
        XDKeHoachTienHanhTT = 41,
        LapCuocTTDotXuat = 42,
        DSCuocTTDotXuat = 43,

        TKKeHoachThanhTra = 46,
        TKVuViecChongCheo = 47,
        DM_LoaiThuTuc = 66,

        //new
        DanhMuc = 20,
        DanhMucLoaiTin = 21,
        QuanLyTinTuc = 60,
        DanhMucLinhVuc = 61,
        LichTiepDan = 62,
        CauHoi = 63,
        HoiDap = 64,
        TraLoiCauHoi = 65,
        DanhMucLoaiThuTuc = 66,
        DanhMucThuTuc = 67,
        DonThu = 68,
        QuanLyDonThu = 69,
        LichSuTraCuu = 70,
        QuanLyQuyetDinhGiaiQuyet = 71,
        QuanLyVanBanTraLoi = 72,

    }

};

namespace Com.Gosol.CMS.Utility
{
    public enum LogType
    {
        Create = 1,
        Edit = 2,
        Delete = 3,
        Other = 4
    }

    public enum CapQuanLy
    {
        CapSoNganh = 1,
        CapUBNDHuyen = 2,
        CapUBNDXa = 3,
        CapUBNDTinh = 4,
        CapTrungUong = 5,
        CapPhong = 11,
        CapTinh = 12
    }

    public enum MessageType
    {
        Error = 1,
        Success = 2
    }

    public enum DMLoaiDoiTuongKN
    {
        CaNhan = 1,
        TapThe = 2,
        CoQuanToChuc = 9
    }
    public enum EnumLoaiTinTuc
    {
        TinNoiBat = 1005,
        TinKinhTe = 1006,
        TinXaHoi = 1007,
        TinVanHoa = 1008
    }
    public enum EnumAdministrator
    {
        NguoiDungID = 18,
        CanBoID = 20,
    }
    public enum EnumLoaiVanBan
    {
        VanBanTraLoi = 24,
        QuyetDinhGiaiQuyet = 2,
    }

    public enum EnumModule
    {
        Menu = 1,
        Sidebar = 2,
    }
    public enum EnumDanhMucModule
    {
        Menu_TinTuc = 1,
        Menu_TraCuuDonThu = 2,
        Menu_LichTiepDan = 3,
        Menu_TrinhTuThuTuc = 4,
        Menu_HoiDap = 5,
        Sidebar_TinTuc = 6,
        Sidebar_DanhMucTinTuc = 7,
        Sidebar_LienKetTrang = 8,
        Sidebar_TraCuuDonThu = 9,
        Sidebar_LichTiepCongDan = 10,
        Sidebar_TrinhTuThuTuc = 11,
        Sidebar_HoiDap = 12,
        Sidebar_ThongKeTruyCap = 13,
        Sidebar_SoLieuToanTinh = 14,
    }
};