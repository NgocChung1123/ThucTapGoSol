using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.Model
{
    public class SyncInfo
    {
        //sync history
        public int SyncHistoryID { get; set; }
        public DateTime SyncDate { get; set; }
        public bool IsSuccess { get; set; }
        public int SyncDuration { get; set; }
        public long SyncRows { get; set; }
        public string Description { get; set; }

        //Sync history detail
        public int SyncHistoryDetailID { get; set; }
        public string ObjectName { get; set; }
        public int LastSyncID { get; set; }
        public DateTime SyncHisDetailDate { get; set; }
    }
}
