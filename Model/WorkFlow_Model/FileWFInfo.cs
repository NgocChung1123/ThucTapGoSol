using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.WorkFlow_Model
{
    public class FileWFInfo
    {
        public int FileWFID { get; set; }
        public string TenFile { get; set; }
        public string FileWFUrl { get; set; }
        public int NguoiUp { get; set; }
        public int TransitionHistoryID { get; set; }
    }
}
