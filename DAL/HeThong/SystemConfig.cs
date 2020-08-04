using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Com.Gosol.CMS.Utility;
using System.Data;
using System.Reflection;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model;
using System;
namespace Com.Gosol.CMS.DAL
{
    public class SystemConfig
    {
        #region Database query string

        private const string GET_ALL = @"SystemConfig_GetAll";
        private const string GET_BY_ID = @"SystemConfig_GetByID";
        private const string DELETE = @"SystemConfig_Delete";
        private const string UPDATE = @"SystemConfig_Update";
        private const string INSERT = @"SystemConfig_Insert";

        private const string COUNT_ALL = "SystemConfig_CountAll";
        private const string COUNT_SEARCH = "SystemConfig_CountSearch";
        private const string GET_BY_PAGE = "SystemConfig_GetByPage";
        private const string GET_BY_SEARCH = "SystemConfig_GetBySearch";

        private const string GET_BY_KEY = "SystemConfig_GetByKey";

        #endregion

        #region paramaters constant

        private const string PARM_SYSTEMCONFIGID = @"SystemConfigID";
        private const string PARM_CONFIGKEY = @"ConfigKey";
        private const string PARM_CONFIGVALUE = @"ConfigValue";
        private const string PARM_DESCRIPTION = @"Description";

        private const string PARM_START = @"Start";
        private const string PARM_END = @"End";
        private const string PARM_KEYWORD = @"Keyword";

        #endregion

        public int CountAll()
        {
            int result = 0;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_ALL, null))
                {
                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();

                }
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        public int CountSearch(String keyword)
        {
            int result = 0;
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 200)                
            };

            parm[0].Value = keyword;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_SEARCH, parm))
                {
                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();

                }
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        public IList<SystemConfigInfo> GetBySearch(String keyword, int start, int end)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };

            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;

            IList<SystemConfigInfo> configList = new List<SystemConfigInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parm))
                {
                    while (dr.Read())
                    {
                        SystemConfigInfo configInfo = GetData(dr);
                        configList.Add(configInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return configList;
        }

        public IList<SystemConfigInfo> GetByPage(int start, int end)
        {
            IList<SystemConfigInfo> configList = new List<SystemConfigInfo>();

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)               
            };
            parm[0].Value = start;
            parm[1].Value = end;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_PAGE, parm))
                {
                    while (dr.Read())
                    {
                        SystemConfigInfo configInfo = GetData(dr);
                        configList.Add(configInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return configList;
        }

        private SystemConfigInfo GetData(SqlDataReader rdr)
        {
            SystemConfigInfo systemConfigInfo = new SystemConfigInfo();
            systemConfigInfo.SystemConfigID = Utils.GetInt32(rdr["SystemConfigID"], 0);
            systemConfigInfo.ConfigKey = Utils.GetString(rdr["ConfigKey"], String.Empty);
            systemConfigInfo.ConfigValue = Utils.GetString(rdr["ConfigValue"], String.Empty);
            systemConfigInfo.Description = Utils.GetString(rdr["Description"], String.Empty);
            return systemConfigInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_CONFIGKEY, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_CONFIGVALUE, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_DESCRIPTION, SqlDbType.NVarChar, 200)
            }; return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, SystemConfigInfo systemConfigInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in systemConfigInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && proInfo.Name != "SystemConfigID")
                {
                    parms[index].Value = proInfo.GetValue(systemConfigInfo, null);
                    index++;
                }
            }
        }

        private SqlParameter[] GetUpdateParms()
        {
            List<SqlParameter> parms = GetInsertParms().ToList();
            parms.Insert(0, new SqlParameter(PARM_SYSTEMCONFIGID, SqlDbType.Int));
            return parms.ToArray();
        }

        private void SetUpdateParms(SqlParameter[] parms, SystemConfigInfo systemConfigInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in systemConfigInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead)
                {
                    parms[index].Value = proInfo.GetValue(systemConfigInfo, null);
                    index++;
                }
            }
        }

        public IList<SystemConfigInfo> GetAll()
        {
            IList<SystemConfigInfo> systemconfigs = new List<SystemConfigInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    
                    while (dr.Read())
                    {
                        SystemConfigInfo systemConfigInfo = GetData(dr);
                        systemconfigs.Add(systemConfigInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return systemconfigs;
        }

        public SystemConfigInfo GetByID(int systemconfigID)
        {
            SystemConfigInfo systemConfigInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_SYSTEMCONFIGID, SqlDbType.Int) };
            parameters[0].Value = systemconfigID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        systemConfigInfo = GetData(dr);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return systemConfigInfo;
        }

        public SystemConfigInfo GetPageSize(int systemconfigID)
        {
            SystemConfigInfo systemConfigInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_SYSTEMCONFIGID, SqlDbType.Int) };
            parameters[0].Value = systemconfigID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        systemConfigInfo = GetData(dr);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return systemConfigInfo;
        }

        public SystemConfigInfo GetByKey(String configKey)
        {
            SystemConfigInfo systemConfigInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_CONFIGKEY, SqlDbType.NVarChar, 200)
            };
            parameters[0].Value = configKey;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_KEY, parameters))
            {
                if (dr.Read())
                {
                    systemConfigInfo = GetData(dr);
                }
                dr.Close();
            }
            return systemConfigInfo;
        }

        public int Delete(int systemconfigID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_SYSTEMCONFIGID, SqlDbType.Int) };
            parameters[0].Value = systemconfigID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, DELETE, parameters);
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

        public int Update(SystemConfigInfo systemConfigInfo)
        {
            int val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, systemConfigInfo);
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


        public int Insert(SystemConfigInfo systemConfigInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, systemConfigInfo);
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, INSERT, parameters);
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
