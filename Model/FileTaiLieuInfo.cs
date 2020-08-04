using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class FileTaiLieuInfo
    {
        public int FileTaiLieuID { get; set; }
        public string TenFile { get; set; }
        public string TomTat { get; set; }
        public DateTime NgayUp { get; set; }
        public int NguoiUp { get; set; }
        public string FileUrl { get; set; }
    }
}
