using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Com.Gosol.CMS.Utility;
using System.Data;
using System.Reflection;
using Com.Gosol.CMS.Model;
using System;
namespace Com.Gosol.CMS.DAL.DanhMuc
{
    public class TransitionHistoryDAL
    {

        private const string SELECT_BYID = @"TransitionHistory_Get_LuongDon";
        private const string GETDUEDATE_BYID = @"TransitionHS_GetDueDate";
        private const string GETBY_KEHOACHTHANHTRAID = @"TransitionHistory_Get_LuongKeHoach";

        private const string PARM_XULYDONID = @"XuLyDonID";
        private const string PARM_CURRENTSTATEID = @"@CurrentStateID";
        private const string PARM_KEHOACHTHANHTRAID = @"KeHoachThanhTraID";

        private TransitionHistoryInfo GetData(SqlDataReader rdr)
        {
            TransitionHistoryInfo  transitonHistory = new TransitionHistoryInfo();
            transitonHistory.BuocThucHien = Utils.GetString(rdr["BuocThucHien"], string.Empty);
            transitonHistory.CanBoThucHien = Utils.GetString(rdr["TenCanBo"], String.Empty);
            transitonHistory.ThoiGianThucHien = Utils.ConvertToDateTime(rdr["DueDate"], DateTime.MinValue);
            transitonHistory.ThaoTac = Utils.GetString(rdr["ThaoTac"], String.Empty);
            transitonHistory.YKienCanBo = Utils.GetString(rdr["YKienCanBo"], String.Empty);
            transitonHistory.DueDate = Utils.ConvertToDateTime(rdr["DueDate"], DateTime.MinValue);
            return transitonHistory;
        }

        private TransitionHistoryInfo GetDataLuongKeHoach(SqlDataReader rdr)
        {
            TransitionHistoryInfo transitonHistory = new TransitionHistoryInfo();
            transitonHistory.BuocThucHien = Utils.GetString(rdr["BuocThucHien"], string.Empty);
            transitonHistory.CanBoThucHien = Utils.GetString(rdr["CanBoThucHien"], String.Empty);
            transitonHistory.ThoiGianThucHien = Utils.ConvertToDateTime(rdr["DueDate"], DateTime.MinValue);
            transitonHistory.ThaoTac = Utils.GetString(rdr["ThaoTac"], String.Empty);
            transitonHistory.DueDate = Utils.ConvertToDateTime(rdr["DueDate"], DateTime.MinValue);
            transitonHistory.FileWFUrl = Utils.GetString(rdr["FileWFUrl"], string.Empty);
            transitonHistory.YKienCanBo = Utils.GetString(rdr["Comment"], string.Empty);
            return transitonHistory;
        }

        private TransitionHistoryInfo GetDueDateData(SqlDataReader rdr)
        {
            TransitionHistoryInfo transitonHistory = new TransitionHistoryInfo();
            transitonHistory.DueDate = Utils.ConvertToDateTime(rdr["DueDate"], DateTime.MinValue);
            return transitonHistory;
        }


        public IList<TransitionHistoryInfo> GetLuongDonByID(int xuLyDonID)
        {
            IList<TransitionHistoryInfo> info = new List<TransitionHistoryInfo>();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_XULYDONID, SqlDbType.Int),
            };
            parm[0].Value = xuLyDonID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BYID, parm))
                {
                    while (dr.Read())
                    {
                        TransitionHistoryInfo tHInfo = GetData(dr);
                        info.Add(tHInfo);
                    }
                    dr.Close();
                }
            }
            catch { throw; }
            return info;
        }

        public IList<TransitionHistoryInfo> GetLuongKeHoachByID(int keHoachThanhTraID)
        {
            IList<TransitionHistoryInfo> info = new List<TransitionHistoryInfo>();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEHOACHTHANHTRAID, SqlDbType.Int),
            };
            parm[0].Value = keHoachThanhTraID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETBY_KEHOACHTHANHTRAID, parm))
                {
                    while (dr.Read())
                    {
                        TransitionHistoryInfo tHInfo = GetDataLuongKeHoach(dr);
                        info.Add(tHInfo);
                    }
                    dr.Close();
                }
            }
            catch { throw; }
            return info;
        }

        public TransitionHistoryInfo GetDueDateByID(int xulydonID, int currentStateID)
        {

            TransitionHistoryInfo DTInfo = null;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_XULYDONID,SqlDbType.Int),
                new SqlParameter(PARM_CURRENTSTATEID,SqlDbType.Int)
            };
            parameters[0].Value = xulydonID;
            parameters[1].Value = currentStateID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETDUEDATE_BYID, parameters))
                {

                    if (dr.Read())
                    {
                        DTInfo = GetDueDateData(dr);                    
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return DTInfo;
        }
        
    }
}
