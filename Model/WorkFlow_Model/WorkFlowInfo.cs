using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model.WorkFlow_Model
{
    public class WorkFlowInfo
    {
        public int WorkFlowID { get; set; }
        public string WorkFlowName { get; set; }
        public string WorkFlowCode { get; set; }
    }

    public class CommandInfo
    {
        public int CommandIndex { get; set; }
        public int CommandID { get; set; }
        public string CommandCode { get; set; }
        public string CommandName { get; set; }
    }
}
