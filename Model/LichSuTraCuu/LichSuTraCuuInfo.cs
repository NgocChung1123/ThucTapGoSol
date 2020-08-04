using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.LichSuTraCuu
{
    public class LichSuTraCuuInfo
    {
        public int LichSuTraCuuID { get; set; }
        public int XuLyDonID { get; set; }
        public string SoDonThu { get; set; }
        public DateTime NgayTiepNhan { get; set; }
        public string PhanLoaiDon { get; set; }
        public string NoiDungDon { get; set; }
        public string DoiTuongKhieuNai { get; set; }
        public string HuongXuLy { get; set; }
        public string CoQuanXuLy { get; set; }
        public string CanBoXuLy { get; set; }
        public string CoQuanTiepNhan { get; set; }
        public string CanBoTiepNhan { get; set; }
        public string CMND { get; set; }
        public string NguoiDaiDien { get; set; }
        public string DiaChi { get; set; }
        public int CoQuanID { get; set; }

        public DateTime NgayTraCuu { get; set; }
        public string TrangThaiDonThu { get; set; }
    }
}
