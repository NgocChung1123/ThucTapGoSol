using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gosol.CMS.Utility
{
    public class Constant
    {
        public const String EncryptKey = "GoSolutions";

        public const string AgencyName = "Viện hàn lâm KHCNVN";
        public const string DepartmentName = "Văn phòng";

        int pagesize =0;
        public const int PageSize = 5;

        public static readonly DateTime DEFAULT_DATE = DateTime.ParseExact("01/01/1753 12:00:00 AM", "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

        public static readonly int Male = 1;
        public static readonly int Female = 0;

        public static readonly String CAPNHAT = "Cập nhật thông tin ";
        public static readonly String THEMMOI = "Thêm mới thông tin ";
        public static readonly String XOA = "Xóa thông tin ";
        #region == Message ==
        #region == Err Message ==
        public static readonly String DEFAULT_TITLE_ERROR = "defaultError";
        public static readonly String CONTENT_DELETE_ERROR = "Dữ liệu đang được sử dụng. Vui lòng kiểm tra lại trước khi xóa!";
        public static readonly String HEADER_MESSAGE_ERROR = "Thông báo lỗi";
        public static readonly String CONTENT_TRINHKY_ERROR = "Đơn thư chưa có hướng xử lý không thể trình ký";
        public static readonly String CONTENT_MESSAGE_ERROR = "Cập nhật dữ liệu thất bại!";
        public static readonly String CONTENT_PHEDUYET_ERROR = "Thao tác phê duyệt chưa được thực hiện.";
        public static readonly String CONTENT_TRINHKQGIAIQUYET_ERROR = "Chưa thể trình giải quyết.";
        public static readonly String CONTENT_CHONDONTHU_ERROR = "Vui lòng chọn một đơn thư trước!";
        public static readonly String MESSAGE_INSERT_ERROR = "Dữ liệu đã tồn tại. Thêm mới thất bại!";
        public static readonly String MESSAGE_UPDATE_ERROR = "Dữ liệu đã tồn tại. Cập nhật thất bại!";

        #endregion
        #region == Success Message ==
        public static readonly String DEFAULT_TITLE_SUCCESS = "defaultSuccess";
        public static readonly String MESSAGE_INSERT_SUCCESS = "Thêm mới dữ liệu thành công !";
        public static readonly String CONTENT_MESSAGE_SUCCESS = "Cập nhật dữ liệu thành công !";
        public static readonly String CONTENT_DELETE_SUCCESS = "Xóa dữ liệu thành công !";
        public static readonly String CONTENT_DELETE_INFO = "Không thể xóa bản ghi này, loại thủ tục hiển đang có thủ tục con, không thể xóa. Vui lòng kiểm tra lại!";
        public static readonly String CONTENT_INSERT_INFO = "Bạn phải đăng nhập để gửi câu hỏi!";
        public static readonly String CONTENT_QUESTION_SUCCESS = "Gửi câu hỏi thành công!";

        public static readonly String CONTENT_DELETE_ERR = "Đã xảy ra lỗi trong quá trình xóa dữ liệu !";
        public static readonly String CONTENT_TRINHKY_SUCCESS = "Trình lãnh đạo ký thành công!";
        public static readonly String CONTENT_PHEDUYET_SUCCESS = "Phê duyệt thành công!";
        public static readonly String CONTENT_TRINHKQGIAIQUYET_SUCCESS = "Trình giải quyết thành công!";
        public static readonly String CONTENT_DUYETGAP_SUCCESS = "Đồng ý tiếp thành công";
        public static readonly String CONTENT_TUCHOI_SUCCESS = "Từ chối tiếp thành công";
        public static readonly String CONTENT_MESSAGE_EROR = "Đã xảy ra lỗi trong quá trình cập nhật dữ liệu !";
        #endregion

        #endregion
        //ProfileTransferType const
        public static readonly int LengthNoiDungDon = 280;
        public static readonly string ChuoiCuoiNDDon = "...";
        public static readonly int CV = 1;
        public static readonly int Dispatch = 2;

        public static readonly int passGrade = 4;

        public const int Softcover = 1;
        public const int Hardcover = 2;

        public const int TinhTrienKhaiID = 21;

        //Bao cao 2a - LoaiKhieuToID const 
        public const int KhieuNai = 1;
        public const int ToCao = 8;
        public const int PhanAnhKienNghi = 9;
        public const int KienNghi = 62;
        public const int PhanAnh = 23;
        public const int KN_LinhVucHanhChinh = 13;
        public const int KN_LinhVucTuPhap = 2;
        public const int KN_VeDang = 20;
        public const int LinhVucCTVHXH = 15;
        public const int VeTranhChapDatDai = 16;
        public const int VeChinhSach = 17;
        public const int VeNhaTaiSan = 18;
        public const int VeCheDo = 19;
        public const int TC_LinhVucHanhChinh = 10;
        public const int TC_LinhVucTuPhap = 11;
        public const int ThamNhung = 12;
        public const int TC_VeDang = 21;
        public const int TC_LinhVucKhac = 22;

        //Bao cao 2a - LoaiKetQuaID const
        public const int ChuaGiaiQuyet = 0;

        public const int KienNghiXuLyHanhChinh = 8;
        public const int KienNghiThuHoiChoNhaNuoc = 14;
        public const int TraLaiChoCongDan = 15;
        public const int DieuTraKhoiTo = 17;

        public const int CongNhanQDLan1 = 12;
        public const int SuaQDLan1 = 13;
        public const int HuyQDLan1 = 23;

        //Bao cao 2b - HuongGiaiQuyet const
        public const int HuongDanTraLoi = 29;
        public const int ChuyenDon = 30;
        public const int ThuLyGiaiQuyet = 31;

        //Bao cao 2b - Tham quyen giai quyet const
        public const int CQHanhChinhCacCap = 11;
        public const int CQTuPhapCacCap = 14;
        public const int CQDang = 20;


        //Bao cao 2c - Phan tich ket qua khieu nai
        public const int Dung = 1;
        public const int Sai = 2;
        public const int Dung1Phan = 3;

        public const string ToolTip = "Bạn không có quyền sử dụng chức năng này";
        public const string NoCreate = "Bạn không có quyền sử dụng chức năng này";
        public const string NoEdit = "Bạn không có quyền sử dụng chức năng này";
        public const string NoDelete = "Bạn không có quyền sử dụng chức năng này";

        //Hang so dung trong in phieu
        public const int PhieuHuongDan = 1;
        public const int PhieuTraDonKN_HuongDan = 2;
        public const int PhieuChuyenDon_PhanAnhKienNghi = 3;
        public const int PhieuChuyenDonToCao = 4;
        public const int PhieuDeXuatThuLyDon = 5;
        public const int PhieuDeXuatThuLyToCao = 6;
        public const int ThongBaoKhongThuLyGiaiQuyet_GuiCQChuyenDonDen = 7;
        public const int ThongBaoKhongThuLyGiaiQuyet_GuiNguoiKN = 8;
        public const int ThongBaoKhongThuLyGiaiQuyetToCao = 9;
        public const int ThongBaoKhongThuLyGiaiQuyetToCaoTiep = 10;
        public const int ThongBaoThuLyGiaiQuyetKN = 11;
        public const int PhieuNhanHoSo = 12;

        //Nguon don
        public const int DonTrongCoQuan = 0;
        public const int DonDuocPhanXuLy = 1;
        // DoiTuongKhieuNai
        public const string CoQuan = "CoQuan";
        public const string CaNhan = "CaNhan";
        public const string TapThe = "TapThe";

        //Nguon don den
        public const int NguonDon_BuuChinh = 2;
        public const int NguonDon_CoQuanKhacChuyenToi = 3;

        public const string NguonDon_BuuChinhs = "Bưu chính";
        public const string NguonDon_CoQuanKhacs = "Cơ quan khác chuyển tới";
        public const string NguonDon_TrucTieps = "Trực tiếp";

        //StateName
        public const string LD_Phan_GiaiQuyet = "LD phân giải quyết";
        public const string LD_CapDuoi_Phan_GiaiQuyet = "LD cơ quan cấp dưới phân giải quyết";
        public const string TP_Phan_GiaiQuyet = "Phó chánh thanh tra hoặc lãnh đạo phòng phân giải quyết";

        public const string TP_XuLy = "TP xử lý";
        public const string TP_PhanXuLy = "TP phân xử lý";
        public const string LD_PhanXuLy = "LD phân xử lý";
        public const string CV_XuLy = "Chuyên viên xử lý";
        public const string LD_DuyetXuLy = "LD duyệt xử lý";

        public const string LD_Duyet_GiaiQuyet = "LD duyệt giải quyết";
        public const string TP_DuyetGQ = "Phó chánh thanh tra hoặc lãnh đạo phòng duyệt giải quyết";
        public const string LD_CQCapDuoiDuyetGQ = "LD cấp dưới duyệt giải quyết";

        public const string TruongDoan_GiaiQuyet = "Trưởng đoàn giải quyết";
        public const string CV_TiepNhan = "Chuyên viên tiếp nhận";
        public const string Ket_Thuc = "Kết thúc";
        public const string TP_DuyetXuLy = "TP duyệt xử lý";
        public const string RutDon = "Rút đơn";
        public const string CHUYENDON_RAVBDONDOC = "Chuyển đơn hoặc gửi văn bản đôn đốc";

        public const string CV_LAPKEHOACH_THANHTRA = "Chuyên viên lập kế hoạch thanh tra";
        public const string LD_DUYETKEHOACH_THANHTRA = "Lãnh đạo duyệt kế hoạch thanh tra";
        public const string KETTHUC_LAPKEHOACH_THANHTRA ="Kết thúc lập kế hoạch thanh tra";


        //StateOrder
        public const int LD_Phan_GiaiQuyet_Order = 7;

        public static readonly String MSG_INSERT_SUCCESS = "Cập nhật dữ liệu thành công.";
        public static readonly String ERR_UPLOAD = "Xảy ra lỗi trong quá trình upload file.";
        public static readonly String ERR_INSERT = "Xảy ra lỗi trong quá trình cập nhật.";
        public static readonly String ERR_FILENOTFOUND = "Không tìm thấy file bạn cần download";
        public const string IsLanhDao = "IsLanhDao";
        public const string IsTruongPhuong = "IsTruongPhong";
        public const string DeXuatThuLy = "Đề xuất thụ lý";
        public const string ChuyenDons = "Chuyển đơn";

        // LogSystem

        public static readonly String DM_HUONGGIAIQUYET = " danh mục hướng giải quyết";
        public static readonly String DM_COQUAN = " danh mục cơ quan,đơn vị";
        public static readonly String DM_LOAIKETQUA = " danh mục loại kết quả";
        public static readonly String DM_LOAIDOITUONGKN = " danh mục loại đối tượng khiếu nại";
        public static readonly String DM_LOAIDOITUONGBIKN = " danh mục loại đối tượng bị khiếu nại";
        public static readonly String DM_DANTOC = " danh mục dân tộc";
        public static readonly String DM_QUOCTICH = " danh mục quốc tịch";
        public static readonly String DM_TINH = " danh mục tỉnh";
        public static readonly String DM_HUYEN = " danh mục huyện";
        public static readonly String DM_XA = " danh mục xã";
        public static readonly String DM_THAMQUYEN = " danh mục thẩm quyền";
        public static readonly String DM_PHONGBAN = " danh mục phòng ban";
        public static readonly String DM_CHUCVU = " danh mục chức vụ";
        public static readonly String DM_NGUONDONDEN = " danh mục nguồn đơn đến";
        public static readonly String DM_LOAIKHIEUTO = " danh mục loại khiếu tố";
        public static readonly String DM_TRANGTHAIDON = " danh mục trạng thái đơn";


        /*
         * StateCode 
         */
        public const string CV_LAPKHTT_SCode = "CVLapKHTT";
        public const string LD_DuyetKHTT_SCode = "LDDuyetKHTT";
        public const string KTLapKHTT_SCode = "KetThucKHTT";
        public const string LapDoanTT_SCode = "LapDoanTT";
        public const string XDKeHoachTienHanhTT_SCode = "XDKeHoachTienHanhTT";
        public const string DuyetKHTienHanhTT_SCode = "DuyetKHTienHanhTT";
        public const string TienHanhTT_SCode = "TienHanhTT";
        public const string DuyetBCKetQuaTT_SCode = "DuyetBCKetQuaTT";
        public const string TTKiemTraChongCheo = "TTTKiemTraChongCheo";

        public const string LAP_CUOC_THANH_TRA = "LapCuocTT";
        public const string XD_DUTHAOKLTHANHTRA = "XDDuThaoKLThanhTra";
        public const string DUYET_DUTHAOKETLUANTT = "DuyetDuThaoKLThanhTra";
        public const string KET_THUC_TIENHANHTT = "KTTienHanhTT";


        /*Vai trò thành viên*/
        public const int TruongDoan = 1;
        public const int PhoDoan = 2;
        public const int ThuKy = 3;
        public const int ThanhVien = 4;
    }


}
