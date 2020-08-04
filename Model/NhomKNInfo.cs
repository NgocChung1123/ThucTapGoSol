using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class NhomKNInfo
    {
        public int NhomKNID { get; set; }
        public String TenCQ { get; set; }
        public int SoLuong { get; set; }
        public int LoaiDoiTuongKNID { get; set; }
        public string StringLoaiDoiTuongKN { get; set; }

        public String DiaChiCQ { get; set; }
        public Boolean DaiDienPhapLy { get; set; }
        public Boolean DuocUyQuyen { get; set; }
    }
}
