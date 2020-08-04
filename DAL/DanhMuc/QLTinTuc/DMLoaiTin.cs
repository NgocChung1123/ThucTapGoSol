using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc
{
    public class DMLoaiTin
    {
        #region Database query string

        private const string GET_ALL = @"DM_LoaiTin_GetAll";
        private const string GET_ALL_PUBLIC = @"DM_LoaiTin_GetAll_Public";
        private const string GET_BY_ID = @"DM_LoaiTin_GetByID";
        private const string GET_BY_SEARCH = @"DM_LoaiTin_GetBySearch";
        private const string COUNT_BY_SEARCH = @"DM_LoaiTin_CountBySearch";

        private const string GET_BY_PARENTID = @"DM_LoaiTin_GetByParentID";
        private const string DELETE = @"DM_LoaiTin_Delete";
        private const string UPDATE = @"DM_LoaiTin_Update";
        private const string INSERT = @"DM_LoaiTin_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_IDLOAITIN = @"IDLoaiTin";
        private const string PARM_PARENTID = @"ParentID";
        private const string PARM_TENLOAITIN = @"TenLoaiTin";
        private const string PARM_GHICHU = @"GhiChu";
        private const string PARM_PUBLIC = @"Public";
        private const string PARM_ORDER = @"Order";
        private const string PARM_CREATER = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";

        private const string PARM_START = @"pStart";
        private const string PARM_END = @"pEnd";
        private const string PARM_KEYWORD = @"pKeyWord";

        #endregion


        #region -- insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_PARENTID, SqlDbType.Int),
                new SqlParameter(PARM_TENLOAITIN, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),
                new SqlParameter(PARM_ORDER, SqlDbType.Int),
                new SqlParameter(PARM_CREATER, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, DMLoaiTinInfo loaiTinInfo)
        {
            parms[0].Value = loaiTinInfo.ParentID;
            if (loaiTinInfo.ParentID == 0)
                parms[0].Value = DBNull.Value;
            parms[1].Value = loaiTinInfo.TenLoaiTin;
            parms[2].Value = loaiTinInfo.GhiChu;
            parms[3].Value = loaiTinInfo.Public;
            parms[4].Value = loaiTinInfo.Order;
            parms[5].Value = loaiTinInfo.Creater;
            parms[6].Value = loaiTinInfo.CreateDate;
        }

        public int Insert(DMLoaiTinInfo loaiTinInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, loaiTinInfo);
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

        #endregion

        #region -- delete

        public int Delete(int loaiTinID)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int), 
            };
            parameters[0].Value = loaiTinID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, DELETE, parameters);
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

        #endregion

        #region -- update

        private SqlParameter[] GetUpdateParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
                new SqlParameter(PARM_PARENTID, SqlDbType.Int),
                new SqlParameter(PARM_TENLOAITIN, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),
                new SqlParameter(PARM_ORDER, SqlDbType.Int),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, DMLoaiTinInfo loaiTinInfo)
        {
            parms[0].Value = loaiTinInfo.IDLoaiTin;
            parms[1].Value = loaiTinInfo.ParentID;
            if (loaiTinInfo.ParentID == 0)
                parms[1].Value = DBNull.Value;
            parms[2].Value = loaiTinInfo.TenLoaiTin;
            parms[3].Value = loaiTinInfo.GhiChu;
            parms[4].Value = loaiTinInfo.Public;
            parms[5].Value = loaiTinInfo.Order;
            parms[6].Value = loaiTinInfo.Editer;
            parms[7].Value = loaiTinInfo.EditDate;
        }

        public int Update(DMLoaiTinInfo loaiTinInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, loaiTinInfo);
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, UPDATE, parameters);
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

        #endregion

        #region -- get data
        public DMLoaiTinInfo GetData(SqlDataReader rdr)
        {
            DMLoaiTinInfo info = new DMLoaiTinInfo();
            info.IDLoaiTin = Utils.ConvertToInt32(rdr["IDLoaiTin"], 0);
            info.ParentID = Utils.ConvertToInt32(rdr["ParentID"], 0);
            info.TenLoaiTin = Utils.ConvertToString(rdr["TenLoaiTin"], string.Empty);
            info.GhiChu = Utils.ConvertToString(rdr["GhiChu"], string.Empty);
            info.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            info.Order = Utils.ConvertToInt32(rdr["Order"], 0);
            info.Creater = Utils.ConvertToInt32(rdr["Creater"], 0);
            info.CreateDate = Utils.ConvertToDateTime(rdr["CreateDate"], DateTime.MinValue);
            info.Editer = Utils.ConvertToInt32(rdr["Editer"], 0);
            info.EditDate = Utils.ConvertToDateTime(rdr["EditDate"], DateTime.MinValue);
            return info;

        }
        #endregion

        #region -- get by id
        public DMLoaiTinInfo GetLoaiTinByID(int loaiTinID)
        {
            DMLoaiTinInfo diachiInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    diachiInfo = GetData(dr);
                    diachiInfo.NguoiTao = Utils.ConvertToString(dr["NguoiTao"], string.Empty);
                    diachiInfo.NguoiSua = Utils.ConvertToString(dr["NguoiSua"], string.Empty);
                }
                dr.Close();
            }
            return diachiInfo;
        }
        #endregion

        #region -- get by search

        public List<DMLoaiTinInfo> GetLoaiTinBySearch(string keySearch,int start,int end)
        {
            List<DMLoaiTinInfo> loaiTins = new List<DMLoaiTinInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,100) ,
                 new SqlParameter(PARM_START, SqlDbType.Int) ,
                  new SqlParameter(PARM_END, SqlDbType.Int) 
            };
            parameters[0].Value = keySearch;
            parameters[1].Value = start;
            parameters[2].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parameters))
                {
                    while (dr.Read())
                    {
                        DMLoaiTinInfo loaiTinInfo = GetData(dr);
                        loaiTinInfo.NguoiTao = Utils.ConvertToString(dr["NguoiTao"],string.Empty);
                        loaiTinInfo.NguoiSua = Utils.ConvertToString(dr["NguoiSua"],string.Empty);
                        loaiTinInfo.TenLoaiTinCha = Utils.ConvertToString(dr["TenLoaiTinCha"], string.Empty);
                        loaiTins.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiTins;
        }

        public int CountSearch(string keyword)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,100)            
            };
            parameters[0].Value = keyword;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_BY_SEARCH, parameters))
                {

                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();
                }
            }
            catch
            {

                throw;
            }
            return result;
        }
        #endregion

        #region -- get all

        public List<DMLoaiTinInfo> GetAllLoaiTin()
        {
            List<DMLoaiTinInfo> loaiTins = new List<DMLoaiTinInfo>();
            
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        DMLoaiTinInfo loaiTinInfo = GetData(dr);
                        //loaiTinInfo.NguoiTao = Utils.ConvertToString(dr["NguoiTao"], string.Empty);
                        //loaiTinInfo.NguoiSua = Utils.ConvertToString(dr["NguoiSua"], string.Empty);
                        loaiTins.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiTins;
        }

        public List<DMLoaiTinInfo> GetAllLoaiTin_Public()
        {
            List<DMLoaiTinInfo> loaiTins = new List<DMLoaiTinInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL_PUBLIC, null))
                {
                    while (dr.Read())
                    {
                        DMLoaiTinInfo loaiTinInfo = GetData(dr);
                        //loaiTinInfo.NguoiTao = Utils.ConvertToString(dr["NguoiTao"], string.Empty);
                        //loaiTinInfo.NguoiSua = Utils.ConvertToString(dr["NguoiSua"], string.Empty);
                        loaiTins.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiTins;
        }
        #endregion
    }
}
