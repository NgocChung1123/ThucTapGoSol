using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.DonThu
{
    public class NhomKhieuNaiModel
    {
        public int NhomKNID { get; set; }
        public string TenCQ { get; set; }
        public int SoLuong { get; set; }
        public int LoaiDoiTuongKNID { get; set; }
        public string StringLoaiDoiTuongKN { get; set; }
        public string DiaChiCQ { get; set; }
        public string DaiDienPhapLy { get; set; }
        public string DuocUyQuyen { get; set; }

    }
}
