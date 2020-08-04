using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class SystemConfigInfo
    {
        public int SystemConfigID { get; set; }
        public String ConfigKey { get; set; }
        public String ConfigValue { get; set; }
        public String Description { get; set; }
    }
}
