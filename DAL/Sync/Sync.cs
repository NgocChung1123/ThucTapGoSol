using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.Sync
{
    public class Sync
    {
        private const string SYNC_HISTORY_GET_HISTORY = @"SyncHistory_GetHistory";
        private const string SYNC_HISTORY_GETBYSEARCH = @"SyncHistory_GetBySearch";
        private const string SYNC_HISTORY_INSERT = @"SyncHistory_Insert";
        private const string SYNC_HISTORY_DETAIL_INSERT = @"SyncHistoryDetail_Insert";

        private const string PARAM_SYNC_HISTORY_ID = "@SyncHistoryID";
        private const string PARAM_SYNC_DATE = "@SyncDate";
        private const string PARAM_IS_SUCCESS = "@IsSuccess";
        private const string PARAM_SYNC_DURATION = "@SyncDuration";
        private const string PARAM_SYNC_ROWS = "@SyncRows";
        private const string PARAM_DESCRIPTION = "@Description";

        private const string PARAM_KEY = "@KeyWord";
        private const string PARAM_START = "@Start";
        private const string PARAM_END = "@End";

        private SyncInfo GetData(SqlDataReader rdr)
        {
            SyncInfo info = new SyncInfo();
            info.SyncHistoryID = Utils.GetInt32(rdr["SyncHistoryID"], 0);
            info.SyncDate = Utils.GetDateTime(rdr["SyncDate"], DateTime.MinValue);
            info.IsSuccess = Utils.ConvertToBoolean(rdr["IsSuccess"], false);
            info.SyncDuration = Utils.ConvertToInt32(rdr["SyncDuration"], 0);
            info.SyncRows = Utils.ConvertToInt64(rdr["SyncRows"], 0);
            info.Description = Utils.GetString(rdr["Description"], string.Empty);
            return info;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms;

            parms = new SqlParameter[]{
                new SqlParameter(PARAM_SYNC_DATE, SqlDbType.DateTime),
                new SqlParameter(PARAM_IS_SUCCESS, SqlDbType.Int),
                new SqlParameter(PARAM_SYNC_DURATION, SqlDbType.Int),
                new SqlParameter(PARAM_SYNC_ROWS, SqlDbType.Int),
                new SqlParameter(PARAM_DESCRIPTION, SqlDbType.NVarChar,500)
            };

            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, SyncInfo SyncInfo)
        {
            parms[0].Value = SyncInfo.SyncDate;
            parms[1].Value = SyncInfo.IsSuccess;
            parms[2].Value = SyncInfo.SyncDuration;
            parms[3].Value = SyncInfo.SyncRows;
            parms[4].Value = SyncInfo.Description;

            if (SyncInfo.SyncDuration == 0) parms[2].Value = DBNull.Value;
            if (SyncInfo.SyncRows == 0) parms[3].Value = DBNull.Value;
        }

        public int Insert(SyncInfo Info)
        {

            object val = null;

            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, Info);

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {

                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, SYNC_HISTORY_INSERT, parameters);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                conn.Close();
            }
            return Utils.ConvertToInt32(val, 0);
        }

        public IList<SyncInfo> GetSyncHistory(QueryFilterInfo info)
        {
            IList<SyncInfo> ListInfo = new List<SyncInfo>();

            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARAM_KEY, SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_START, SqlDbType.Int),
                new SqlParameter(PARAM_END, SqlDbType.Int)
                
            };

            parms[0].Value = info.KeyWord;
            parms[1].Value = info.Start;
            parms[2].Value = info.End;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SYNC_HISTORY_GET_HISTORY, parms))
                {
                    while (dr.Read())
                    {
                        SyncInfo Info = GetData(dr);
                        ListInfo.Add(Info);
                    }
                    dr.Close();
                }
            }
            catch
            {

                throw;
            }

            return ListInfo;
        }
    }
}
