using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class FileHoSoInfo
    {
        public int FileHoSoID { get; set; }
        public String TenFile { get; set; }
        public String TomTat { get; set; }
        public DateTime NgayUp { get; set; }
        public string NgayUp_str { get; set; }
        public int NguoiUp { get; set; }
        public string NgayUps { get; set; }
        public String FileURL { get; set; }
        public int XuLyDonID { get; set; }
        public int DonThuID { get; set; }
        public bool IsBaoMat { get; set; }
        public int ChuyenGiaiQuyetID { get; set; }
        public int KetQuaID { get; set; }

        public string FileBase64 { get; set; }
    }

}
