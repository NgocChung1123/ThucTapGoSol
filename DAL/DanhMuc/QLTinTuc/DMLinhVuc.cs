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
    public class DMLinhVuc
    {
        #region Database query string

        private const string GET_ALL = @"DM_LinhVuc_GetAll";
        private const string GET_BY_ID = @"DM_LinhVuc_GetByID";
        private const string GET_BY_SEARCH = @"DM_LinhVuc_GetBySearch";
        private const string COUNT_BY_SEARCH = @"DM_LinhVuc_CountBySearch";

        private const string DELETE = @"DM_LinhVuc_Delete";
        private const string UPDATE = @"DM_LinhVuc_Update";
        private const string INSERT = @"DM_LinhVuc_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_IDLINHVUC = @"IDLinhVuc";
        private const string PARM_TENLINHVUC = @"TenLinhVuc";
        private const string PARM_GHICHU = @"GhiChu";
        private const string PARM_PUBLIC = @"Public";
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
                new SqlParameter(PARM_TENLINHVUC, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_CREATER, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),

            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, DMLinhVucInfo info)
        {
            parms[0].Value = info.TenLinhVuc;
            parms[1].Value = info.GhiChu;
            parms[2].Value = info.Creater;
            parms[3].Value = info.CreateDate;
            if (info.CreateDate == DateTime.MinValue)
                parms[3].Value = DBNull.Value;
            parms[4].Value = info.Public;
        }

        public int Insert(DMLinhVucInfo loaiTinInfo)
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

        public int Delete(int dmLinhVucID)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int),
            };
            parameters[0].Value = dmLinhVucID;
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
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int),
                new SqlParameter(PARM_TENLINHVUC, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),

            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, DMLinhVucInfo info)
        {
            parms[0].Value = info.IDLinhVuc;
            parms[1].Value = info.TenLinhVuc;
            parms[2].Value = info.GhiChu;
            parms[3].Value = info.Editer;
            parms[4].Value = info.EditDate;
            if (info.EditDate == DateTime.MinValue)
                parms[4].Value = DBNull.Value;
            parms[5].Value = info.Public;
        }

        public int Update(DMLinhVucInfo info)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, info);
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
        public DMLinhVucInfo GetData(SqlDataReader rdr)
        {
            DMLinhVucInfo info = new DMLinhVucInfo();
            info.IDLinhVuc = Utils.ConvertToInt32(rdr["IDLinhVuc"], 0);
            info.TenLinhVuc = Utils.ConvertToString(rdr["TenLinhVuc"], string.Empty);
            info.GhiChu = Utils.ConvertToString(rdr["GhiChu"], string.Empty);
            info.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            info.Creater = Utils.ConvertToInt32(rdr["Creater"], 0);
            info.CreateDate = Utils.ConvertToDateTime(rdr["CreateDate"], DateTime.MinValue);
            info.Editer = Utils.ConvertToInt32(rdr["Editer"], 0);
            info.EditDate = Utils.ConvertToDateTime(rdr["EditDate"], DateTime.MinValue);
            return info;

        }
        #endregion

        #region -- get by id
        public DMLinhVucInfo GetDMLinhVucByID(int linhVucID)
        {
            DMLinhVucInfo info = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int),
            };
            parameters[0].Value = linhVucID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    info = GetData(dr);
                }
                dr.Close();
            }
            return info;
        }
        #endregion

        #region -- get by search

        public List<DMLinhVucInfo> GetDMLinhVucBySearch(string keySearch, int start, int end)
        {
            List<DMLinhVucInfo> dmLinhVucs = new List<DMLinhVucInfo>();
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
                        DMLinhVucInfo loaiTinInfo = GetData(dr);
                        dmLinhVucs.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return dmLinhVucs;
        }
        /// <summary>
        /// tìm theo trạng thái và từ khoá
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="TrangThai"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<DMLinhVucInfo> GetDMLinhVucBySearch(string keySearch, int TrangThai, int start, int end)
        {
            List<DMLinhVucInfo> dmLinhVucs = new List<DMLinhVucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
               new SqlParameter("TrangThai", SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,100) ,
                 new SqlParameter(PARM_START, SqlDbType.Int) ,
                  new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = TrangThai;
            parameters[1].Value = keySearch;
            parameters[2].Value = start;
            parameters[3].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DM_LinhVuc_GetBySearch_New", parameters))
                {
                    while (dr.Read())
                    {
                        DMLinhVucInfo loaiTinInfo = GetData(dr);
                        dmLinhVucs.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return dmLinhVucs;
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
        /// <summary>
        /// tính tổng số trang cho tìm kiếm theo trạng thái và từ khoá
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TrangThai"></param>
        /// <returns></returns>
        public int CountSearch(string keyword, int TrangThai)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                 new SqlParameter("TrangThai",SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,100)
            };
            parameters[0].Value = TrangThai;
            parameters[1].Value = keyword;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DM_LinhVuc_CountBySearch_New", parameters))
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

        public List<DMLinhVucInfo> GetAllDMLinhVuc()
        {
            List<DMLinhVucInfo> loaiTins = new List<DMLinhVucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        DMLinhVucInfo loaiTinInfo = GetData(dr);
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
