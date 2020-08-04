using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.Notify
{
    public class Notify
    {
        private const string COUNT_NOTIFY_LD = @"NV_Notify_CountNotify_LD";
        //private const string COUNT_NOTIFY_TP = @"Notify_CountNotify_TP";
        //private const string COUNT_NOTIFY_CV = @"Notify_CountNotify_CV";

        private const string COUNT_KEHOACHTT_CANDUYET_LD = @"NV_Notify_KeHoachTTCanDuyet_LD";


        private const string PARAM_CANBOID = "@CanBoID";
        private const string PARAM_COQUANID = "@CoQuanID";
        private const string PARAM_PHONGBANID = "@PhongBanID";

        public int Count_NhiemVu(int coQuanID, int roleID, int phongBanID)
        {
            int result = 0;

            SqlParameter[] parameters = new SqlParameter[] {  
                new SqlParameter(PARAM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARAM_PHONGBANID, SqlDbType.Int)
            };

            parameters[0].Value = coQuanID;
            parameters[1].Value = phongBanID;

            try
            {
                string storeName = "";
                if (roleID == 1)
                    storeName = COUNT_NOTIFY_LD;
                //else if (roleID == 2)
                //    storeName = COUNT_NOTIFY_TP;
                //else
                //    storeName = COUNT_NOTIFY_CV;

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, storeName, parameters))
                {
                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();
                }
            }
            catch { throw; }
            return result;
        }

        public int Count_KeHoachTTCanDuyet(int coQuanID, int roleID, int phongBanID)
        {
            int result = 0;

            SqlParameter[] parameters = new SqlParameter[] {   
                new SqlParameter(PARAM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARAM_PHONGBANID, SqlDbType.Int)
            };

            parameters[0].Value = coQuanID;
            parameters[1].Value = phongBanID;

            try
            {
                string storeName = "";
                if (roleID == 1)
                    storeName = COUNT_KEHOACHTT_CANDUYET_LD;
                else if (roleID == 2)
                {
                    //storeName = COUNT_DTCANPHANXL_TP;
                }
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, storeName, parameters))
                {
                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();
                }
            }
            catch { throw; }
            return result;
        }
    }
}
