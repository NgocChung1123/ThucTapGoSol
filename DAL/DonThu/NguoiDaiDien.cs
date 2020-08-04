using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Com.Gosol.CMS.Utility;
using System.Data;
using System.Reflection;
using Com.Gosol.CMS.Model;
using System;
using Com.Gosol.CMS.Model.DonThu;


namespace Com.Gosol.CMS.DAL.DonThu
{
    public class NguoiDaiDien
    {
        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@DonThuID", SqlDbType.Int),
                new SqlParameter("@HoTen", SqlDbType.NVarChar),
                new SqlParameter("@DanToc", SqlDbType.NVarChar),
                new SqlParameter("@DiaChi", SqlDbType.NVarChar),
                
            };
            return parms;
        }
        private void SetInsertParms(SqlParameter[] parms, NguoiDaiDienInfo NguoiDaiDienInfo)
        {
            parms[0].Value = NguoiDaiDienInfo.DonThuID;
            parms[1].Value = NguoiDaiDienInfo.HoTen;
            parms[2].Value = NguoiDaiDienInfo.DanToc;
            parms[3].Value = NguoiDaiDienInfo.DiaChi;
        }
        public int Insert(NguoiDaiDienInfo NguoiDaiDienInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, NguoiDaiDienInfo);
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "NguoiDaiDien_Insert", parameters);
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
            return Convert.ToInt32(val);
        }
    }
}
