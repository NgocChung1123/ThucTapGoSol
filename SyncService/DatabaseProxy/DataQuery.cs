using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.Gosol.CMS.Utility;

namespace SyncService
{
    public class DataQuery
    {
        #region Declare param

        private const string PARM_DOCUMENTID = "@DocumentID";
        private const string PARM_WORKFLOWID = "@WorkflowID";
        private const string PARM_STATEID = "@StateID";
        private const string PARM_DUEDATE = "@DueDate";

        private const string PARM_WORKFLOWCODE = "@WorkflowCode";
        private const string PARM_WORKFLOWNAME = "@WorkflowName";

        private const string PARM_STATE = "@StateID";
        private const string PARM_STATE_NAME = "@StateName";
        private const string PARM_CURRENT_STATE_ID = "@CurrentStateID";
        private const string PARM_NEXT_STATE_ID = "@NextStateID";
        private const string PARM_CURRENT_STATE_NAME = "@CurrentStateName";
        private const string PARM_NEXT_STATE_NAME = "@NextStateName";

        private const string PARM_START_DATE = "@StartDate";
        private const string PARM_END_DATE = "@EndDate";

        private const string PARM_COMMAND_CODE = "@CommandCode";

        private const string PARM_TRANSITION_ID = "@TransitionID";
        private const string PARM_COMMENT = "@Comment";
        private const string PARM_USER_ID = "@UserID";
        private const string PARM_MODIFIED_DATE = "@ModifiedDate";

        #endregion

        private const string QUERY_GET_ALL_SYNC_SERVER = "Select * from SyncServer";

        public DataTable GetSyncServers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SyncServerID", typeof(int));
            dt.Columns.Add("SyncServerName", typeof(string));
            dt.Columns.Add("SyncServerIP", typeof(string));
            dt.Columns.Add("IsActive", typeof(bool));

            IDbConnection dbConnection = DatabaseProxy.CreateDBConnection();
            IDbCommand dbCommand = DatabaseProxy.CreateDBCommand(dbConnection, QUERY_GET_ALL_SYNC_SERVER);

            try
            {
                dbConnection.Open();
                IDataReader dataReader = dbCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    var row = dt.NewRow();
                    row["SyncServerID"] = Utils.ConvertToInt32(dataReader["SyncServerID"], 0);
                    row["SyncServerName"] = Utils.GetString(dataReader["SyncServerName"], string.Empty);
                    row["SyncServerIP"] = Utils.GetString(dataReader["SyncServerIP"], string.Empty);
                    row["IsActive"] = Utils.GetBoolean(dataReader["IsActive"], false);

                    dt.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                dbConnection.Close();
            }

            return dt;
        }

        public int GetLastSyncID(string objectName)
        {
            return 0;
        }

        public DataTable GetSyncData(int lastSyncID, string objectName)
        {
            DataTable dt = new DataTable();

            return dt;
        }
    }
}
