using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using System.Data;

namespace Com.Gosol.CMS.DAL
{
    public class ThemeConfig
    {
        //Su dung de goi StoreProcedure

        private const string GET_THEME = @"ThemeConfig_GetTheme";
        private const string UPDATE = @"ThemeConfig_Update";
        private const string INSERT = @"ThemeConfig_Insert";  

        //Ten cac bien dau vao
        private const string PARAM_THEMECONFIG_ID = "@ThemeConfigID";
        private const string PARAM_TENDONVI = "@UnitName";
        private const string PARAM_LOGO = "@UnitLogo";
        private const string PARAM_TEN_GIAODIEN = "@UnitThemeName";
        private const string PARAM_HOMEPHONE = "@HomePhone";
        private const string PARAM_PHONE = "@Phone";

        private ThemeConfigInfo GetData(SqlDataReader dr)
        {
            ThemeConfigInfo cInfo = new ThemeConfigInfo();
            cInfo.ThemeConfigID = Utils.ConvertToInt32(dr["ThemeConfigID"].ToString(), 0);
            cInfo.UnitName = Utils.ConvertToString(dr["UnitName"].ToString(), string.Empty);
            cInfo.UnitLogo = Utils.ConvertToString(dr["UnitLogo"].ToString(), string.Empty);
            cInfo.UnitThemeName = Utils.ConvertToString(dr["UnitThemeName"].ToString(), string.Empty);
            cInfo.HomePhone = Utils.ConvertToString(dr["HomePhone"], string.Empty);
            cInfo.Phone = Utils.ConvertToString(dr["Phone"], string.Empty);
            
            return cInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter(PARAM_TENDONVI, SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_LOGO,SqlDbType.NVarChar,250),
                new SqlParameter(PARAM_TEN_GIAODIEN,SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_HOMEPHONE,SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_PHONE,SqlDbType.NVarChar,50)
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, ThemeConfigInfo cInfo)
        {

            parms[0].Value = cInfo.UnitName;
            parms[1].Value = cInfo.UnitLogo;
            parms[2].Value = cInfo.UnitThemeName;
            parms[3].Value = cInfo.HomePhone;
            parms[4].Value = cInfo.Phone;
        }

        private SqlParameter[] GetUpdateParms()
        {
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter(PARAM_THEMECONFIG_ID, SqlDbType.Int),
                new SqlParameter(PARAM_TENDONVI, SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_LOGO,SqlDbType.NVarChar,250),
                new SqlParameter(PARAM_TEN_GIAODIEN,SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_HOMEPHONE,SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_PHONE,SqlDbType.NVarChar,50)
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, ThemeConfigInfo cInfo)
        {

            parms[0].Value = cInfo.ThemeConfigID;
            parms[1].Value = cInfo.UnitName;
            parms[2].Value = cInfo.UnitLogo;
            parms[3].Value = cInfo.UnitThemeName;
            parms[4].Value = cInfo.HomePhone;
            parms[5].Value = cInfo.Phone;
        }

        public ThemeConfigInfo GetTheme()
        {

            ThemeConfigInfo cInfo = null;            

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_THEME, null))
                {

                    if (dr.Read())
                    {
                        cInfo = GetData(dr);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return cInfo;
        }
       
        public int Update(ThemeConfigInfo cInfo)
        {

            int val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, cInfo);

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {

                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, UPDATE, parameters);
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
            return val;
        }

        public int Insert(ThemeConfigInfo cInfo)
        {

            int val = 0;

            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, cInfo);

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {

                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, INSERT, parameters);
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
            return val;
        }     
    }
}
