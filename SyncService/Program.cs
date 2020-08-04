using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.NetworkInformation;

namespace SyncService
{
    public class Program
    {
        private const string MSG_SERVER_UNAVAILABLE = "Không thể kết nối đến Server";

        static void Main(string[] args)
        {
            DataQuery dataQuery = new DataQuery();

            DataTable serverList = dataQuery.GetSyncServers();

            foreach (DataRow row in serverList.Rows)
            {
                var logMsg = string.Empty;
                var isServerActive = (bool) row["IsActive"];
                if (isServerActive)
                {
                    string serverIP = (string)row["SyncServerIP"];
                    //Kiem tra ket noi voi server
                    var ping = new Ping();
                    try
                    {
                        var reply = ping.Send(serverIP, 60 * 1000); // 1 minute time out (in ms)
                    }
                    catch
                    {
                        logMsg = MSG_SERVER_UNAVAILABLE;                        
                    }

                    string pushUrl = @"http://" + row["SyncServerIP"] + "/" + Constant.SYNC_URL;

                    string[] syncObjectList = Constant.SYNC_OBJECTS.Split(',');

                    for (int i = 0; i < syncObjectList.Length; i++)
                    {
                        string objectName = syncObjectList[i].Trim();

                        int lastSyncID = dataQuery.GetLastSyncID(objectName);

                        DataTable syncData = dataQuery.GetSyncData(lastSyncID, objectName);
                    }

                    

                }                
            }
        }
    }
}
