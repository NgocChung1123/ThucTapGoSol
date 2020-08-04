using Com.Gosol.CMS.Model.WorkFlow_Model;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.WorkFlow_DAL
{
    public class FileWF
    {
        private const string FILEWF_INSERT = @"WF_FileWF_Insert";

        private const string PARAM_FILE_WF_ID = "@FileWFID";
        private const string PARAM_TENFILE = "@TenFile";
        private const string PARAM_FILE_URL = "@FileWFUrl";
        private const string PARAM_NGUOIUP = "@NguoiUp";
        private const string PARAM_TRANSITION_HISTORY_ID = "@TransitionHistoryID";

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter(PARAM_TENFILE,SqlDbType.NVarChar,500),
                new SqlParameter(PARAM_FILE_URL,SqlDbType.NVarChar,500),
                new SqlParameter(PARAM_NGUOIUP,SqlDbType.Int),
                new SqlParameter(PARAM_TRANSITION_HISTORY_ID,SqlDbType.Int)
                };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, FileWFInfo info)
        {

            parms[0].Value = info.TenFile;
            parms[1].Value = info.FileWFUrl;
            parms[2].Value = info.NguoiUp;
            parms[3].Value = info.TransitionHistoryID;

        }

        public int Insert(FileWFInfo info)
        {

            object val = null;

            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, info);

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {

                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, FILEWF_INSERT, parameters);
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
    }
}
