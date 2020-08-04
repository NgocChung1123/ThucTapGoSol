using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class MenuInfo
    {
        public int MenuID { get; set; }
        public String TenMenu { get; set; }
        public String MenuUrl { get; set; }
        public int MenuChaID { get; set; }
        public int ChucNangID { get; set; }
        public String ImageUrl { get; set; }
    }
}
