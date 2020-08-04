using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.HeThong
{
    public class CauHinhModule
    {
        #region Database query string

        private const string GET_ALL = @"CauHinhModule_GetAll";
        private const string GET_BY_ID = @"CauHinhModule_GetByID";
        private const string GET_BY_MA_MODULE = @"CauHinhModule_GetByMaModule";
        private const string GET_BY_SEARCH = @"CauHinhModule_GetBySearch";
        private const string UPDATE_TRANGTHAIHIENTHI = @"CauHinhModule_UpdateTrangThaiHienThi";
        private const string UPDATE_THUTUHIENTHI = @"CauHinhModule_UpdateThuTuHienThi";

        #endregion

        #region paramaters constant

        private const string PARM_MODULEID = @"ModuleID";
        private const string PARM_MUC = @"Muc";
        private const string PARM_MODULE = @"Module";
        private const string PARM_MAMODULE = @"MaModule";
        private const string PARM_TRANGTHAIHIENTHI = @"TrangThaiHienThi";
        private const string PARM_THUTUHIENTHI = @"ThuTuHienThi";
       
        private const string PARM_START = @"Start";
        private const string PARM_END = @"End";
        private const string PARM_KEYWORD = @"Keyword";

        #endregion

        public List<CauHinhModuleInfo> GetAll()
        {
            List<CauHinhModuleInfo> listCauHinhModule = new List<CauHinhModuleInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        CauHinhModuleInfo info = new CauHinhModuleInfo();
                        info.ModuleID = Utils.ConvertToInt32(dr["ModuleID"], 0);
                        info.Muc = Utils.ConvertToString(dr["Muc"], String.Empty);
                        info.Module = Utils.ConvertToInt32(dr["Module"], 0);
                        info.MaModule = Utils.ConvertToString(dr["MaModule"], String.Empty);
                        info.TrangThaiHienThi = Utils.ConvertToBoolean(dr["TrangThaiHienThi"], false);
                        info.ThuTuHienThi = Utils.ConvertToInt32(dr["ThuTuHienThi"], 0);
                        listCauHinhModule.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listCauHinhModule;
        }

        public CauHinhModuleInfo GetByID(int moduleID)
        {
            CauHinhModuleInfo info = new CauHinhModuleInfo();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_MODULEID, SqlDbType.Int) 
            };
            parameters[0].Value = moduleID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        info.ModuleID = Utils.ConvertToInt32(dr["ModuleID"], 0);
                        info.Muc = Utils.ConvertToString(dr["Muc"], String.Empty);
                        info.Module = Utils.ConvertToInt32(dr["Module"], 0);
                        info.MaModule = Utils.ConvertToString(dr["MaModule"], String.Empty);
                        info.TrangThaiHienThi = Utils.ConvertToBoolean(dr["TrangThaiHienThi"], false);
                        info.ThuTuHienThi = Utils.ConvertToInt32(dr["ThuTuHienThi"], 0);
                    }
                    dr.Close();
                }
            }
            catch
            {
                throw;
            }
            return info;
        }

        public CauHinhModuleInfo GetByMaModule(string maModule)
        {
            CauHinhModuleInfo info = new CauHinhModuleInfo();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_MAMODULE, SqlDbType.NVarChar)
            };
            parameters[0].Value = maModule;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_MA_MODULE, parameters))
                {
                    if (dr.Read())
                    {
                        info.ModuleID = Utils.ConvertToInt32(dr["ModuleID"], 0);
                        info.Muc = Utils.ConvertToString(dr["Muc"], String.Empty);
                        info.Module = Utils.ConvertToInt32(dr["Module"], 0);
                        info.MaModule = Utils.ConvertToString(dr["MaModule"], String.Empty);
                        info.TrangThaiHienThi = Utils.ConvertToBoolean(dr["TrangThaiHienThi"], false);
                        info.ThuTuHienThi = Utils.ConvertToInt32(dr["ThuTuHienThi"], 0);
                    }
                    dr.Close();
                }
            }
            catch
            {
                throw;
            }
            return info;
        }

        public List<CauHinhModuleInfo> GetBySearch(String keyword, int start, int end, int module)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int),
                new SqlParameter(PARM_MODULE, SqlDbType.Int),
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            parm[3].Value = module;

            List<CauHinhModuleInfo> listCauHinhModule = new List<CauHinhModuleInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parm))
                {
                    while (dr.Read())
                    {
                        CauHinhModuleInfo info = new CauHinhModuleInfo();
                        info.ModuleID = Utils.ConvertToInt32(dr["ModuleID"], 0);
                        info.Muc = Utils.ConvertToString(dr["Muc"], String.Empty);
                        info.Module = Utils.ConvertToInt32(dr["Module"], 0);
                        if(info.Module == (int)EnumModule.Menu)
                        {
                            info.ModuleStr = "Menu";
                        }
                        else if(info.Module == (int)EnumModule.Sidebar)
                        {
                            info.ModuleStr = "Sidebar";
                        }
                        info.MaModule = Utils.ConvertToString(dr["MaModule"], String.Empty);
                        info.TrangThaiHienThi = Utils.ConvertToBoolean(dr["TrangThaiHienThi"], false);
                        info.ThuTuHienThi = Utils.ConvertToInt32(dr["ThuTuHienThi"], 0);
                        info.Count = Utils.ConvertToInt32(dr["CountNum"], 0);
                        listCauHinhModule.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listCauHinhModule;
        }

        public int UpdateTrangThaiHienThi(CauHinhModuleInfo info)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_MODULEID, SqlDbType.Int),
                new SqlParameter(PARM_TRANGTHAIHIENTHI, SqlDbType.Bit),
            };
            parameters[0].Value = info.ModuleID;
            parameters[1].Value = info.TrangThaiHienThi;

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, UPDATE_TRANGTHAIHIENTHI, parameters);
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

        public int UpdateThuTuHienThi(CauHinhModuleInfo info)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_MODULEID, SqlDbType.Int),
                new SqlParameter(PARM_THUTUHIENTHI, SqlDbType.Int),
            };
            parameters[0].Value = info.ModuleID;
            parameters[1].Value = info.ThuTuHienThi;

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, UPDATE_THUTUHIENTHI, parameters);
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
