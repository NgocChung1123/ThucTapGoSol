using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc
{
    public class DMLoaiThuTuc
    {
        #region Database query string

        private const string GET_PARENTS = @"DM_LoaiThuTuc_GetParents";
        private const string GET_CHILDENTS = @"DM_LoaiThuTuc_GetChildents";
        private const string GET_BY_PARENTID = @"DM_LoaiThuTuc_GetByParentID";
        private const string GET_BY_SEARCH = @"DM_LoaiThuTuc_GetBySearch";
        private const string GET_PARENTID_BY_SEARCH = @"DM_LoaiThuTuc_GetParentIDBySearch";
        private const string GET_CHILDRENTID_BY_SEARCH = @"DM_LoaiThuTuc_GetChildrentIDBySearch";
        private const string GET_BY_ID = @"DM_LoaiThuTuc_GetByID";
        private const string GET_ALL = @"DM_LoaiThuTuc_GetAll";
        private const string GET_ALL_FRONTEND = @"DM_LoaiThuTuc_GetAll_Frontend";


        private const string COUNT_BY_SEARCH = @"DM_LoaiThuTuc_CountBySearch";
        private const string COUNT = @"DM_LoaiThuTuc_Count";

        private const string DELETE = @"DM_LoaiThuTuc_Delete";
        private const string UPDATE = @"DM_LoaiThuTuc_Update";
        private const string INSERT = @"DM_LoaiThuTuc_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_LOAITHUTUCID = @"LoaiThuTucID";
        private const string PARM_PARENTID = @"ParentID";
        private const string PARM_TENLOAITHUTUC = @"TenLoaiThuTuc";
        private const string PARM_GHICHU = @"GhiChu";
        private const string PARM_PUBLIC = @"Public";
        private const string PARM_CREATER = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";
        private const string PARM_FILEURL = @"FileUrl";
        private const string PARM_FILENAME = @"FileName";

        private const string PARM_START = @"pStart";
        private const string PARM_END = @"pEnd";
        private const string PARM_KEYWORD = @"pKeyWord";
        private const string PARM_PARENTID_KEY = @"pParentID";

        #endregion


        #region // Get
        private DMLoaiThuTucInfo GetData(SqlDataReader rdr)
        {
            DMLoaiThuTucInfo loaiThuTucInfo = new DMLoaiThuTucInfo();
            loaiThuTucInfo.LoaiThuTucID = Utils.GetInt32(rdr["LoaiThuTucID"], 0);
            loaiThuTucInfo.TenLoaiThuTuc = Utils.GetString(rdr["TenLoaiThuTuc"], String.Empty);
            loaiThuTucInfo.ParentID = Utils.GetInt32(rdr["ParentID"], 0);
            loaiThuTucInfo.GhiChu = Utils.GetString(rdr["GhiChu"], String.Empty);
            loaiThuTucInfo.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            loaiThuTucInfo.Creater = Utils.GetInt32(rdr["Creater"], 0);
            loaiThuTucInfo.FileUrl = Utils.GetString(rdr["FileUrl"], string.Empty);
            loaiThuTucInfo.FileName = Utils.GetString(rdr["FileName"], string.Empty);
            loaiThuTucInfo.creater_name = Utils.GetString(rdr["creater_name"], string.Empty);
            loaiThuTucInfo.parent_name = Utils.GetString(rdr["parent_name"], string.Empty);
            loaiThuTucInfo.editer_name = Utils.GetString(rdr["editer_name"], string.Empty);

            //loaiThuTucInfo.has_childs = Utils.GetInt32(rdr["has_child"], 0);

            return loaiThuTucInfo;
        }

        private DMLoaiThuTucInfo Get(SqlDataReader rdr)
        {
            DMLoaiThuTucInfo loaiThuTucInfo = new DMLoaiThuTucInfo();
            loaiThuTucInfo.LoaiThuTucID = Utils.GetInt32(rdr["LoaiThuTucID"], 0);
            loaiThuTucInfo.TenLoaiThuTuc = Utils.GetString(rdr["TenLoaiThuTuc"], String.Empty);
            loaiThuTucInfo.ParentID = Utils.GetInt32(rdr["ParentID"], 0);
            loaiThuTucInfo.GhiChu = Utils.GetString(rdr["GhiChu"], String.Empty);
            loaiThuTucInfo.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            loaiThuTucInfo.Creater = Utils.GetInt32(rdr["Creater"], 0);
            loaiThuTucInfo.FileUrl = Utils.GetString(rdr["FileUrl"], String.Empty);
            loaiThuTucInfo.FileName = Utils.GetString(rdr["FileName"], String.Empty);

            return loaiThuTucInfo;
        }

        public IList<DMLoaiThuTucInfo> GetParents()
        {
            IList<DMLoaiThuTucInfo> loaiThuTuc = new List<DMLoaiThuTucInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTS, null))
                {
                    while (dr.Read())
                    {
                        DMLoaiThuTucInfo loaiThuTucInfo = GetData(dr);
                        loaiThuTuc.Add(loaiThuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return loaiThuTuc;
        }
        //public IList<DMLoaiThuTucInfo> GetChildents()
        //{
        //    IList<DMLoaiThuTucInfo> loaiThuTuc = new List<DMLoaiThuTucInfo>();
        //    try
        //    {
        //        using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_CHILDENTS, null))
        //        {
        //            while (dr.Read())
        //            {
        //                DMLoaiThuTucInfo loaiThuTucInfo = GetData(dr);
        //                loaiThuTuc.Add(loaiThuTucInfo);
        //            }
        //            dr.Close();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return loaiThuTuc;
        //}
        public List<DMLoaiThuTucInfo> GetByParentID(int parentID)
        {
            List<DMLoaiThuTucInfo> loaiThuTucs = new List<DMLoaiThuTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int) 
            };
            parameters[0].Value = parentID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_PARENTID, parameters))
            {
                while (dr.Read())
                {
                    DMLoaiThuTucInfo loaiThuTucInfo = new DMLoaiThuTucInfo();
                    loaiThuTucInfo.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                    loaiThuTucInfo.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                    loaiThuTucInfo.LoaiThuTucID = Utils.ConvertToInt32(dr["LoaiThuTucID"], 0);
                    loaiThuTucInfo.TenLoaiThuTuc = Utils.ConvertToString(dr["TenLoaiThuTuc"], string.Empty);
                    loaiThuTucInfo.GhiChu = Utils.ConvertToString(dr["GhiChu"], string.Empty);
                    loaiThuTucInfo.Public = Utils.ConvertToBoolean(dr["Public"], false);
                    loaiThuTucInfo.Creater = Utils.ConvertToInt32(dr["Creater"], 0);
                    loaiThuTucInfo.Editer = Utils.ConvertToInt32(dr["Editer"], 0);
                    loaiThuTucInfo.has_childs = Utils.ConvertToInt32(dr["has_child"], 0);
                    loaiThuTucInfo.FileUrl = Utils.ConvertToString(dr["FileUrl"], string.Empty);
                    loaiThuTucInfo.FileName = Utils.ConvertToString(dr["FileName"], string.Empty);

                    loaiThuTucs.Add(loaiThuTucInfo);
                }
                dr.Close();
            }
            return loaiThuTucs;
        }
        public List<DMLoaiThuTucInfo> GetLoaiThuTucBySearch(string keySearch, int start, int end)
        {
            List<DMLoaiThuTucInfo> loaiThuTucs = new List<DMLoaiThuTucInfo>();
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
                        DMLoaiThuTucInfo loaiThuTucInfo = GetData(dr);
                        loaiThuTucInfo.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        loaiThuTucInfo.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        loaiThuTucInfo.parent_name = Utils.ConvertToString(dr["parent_name"], string.Empty);
                        loaiThuTucs.Add(loaiThuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiThuTucs;
        }
        public List<DMLoaiThuTucInfo> GetParentIDBySearch(string keySearch, int start, int end)
        {
            List<DMLoaiThuTucInfo> loaiThuTucs = new List<DMLoaiThuTucInfo>();
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
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTID_BY_SEARCH, parameters))
                {
                    while (dr.Read())
                    {
                        DMLoaiThuTucInfo loaiThuTucInfo = GetData(dr);
                        loaiThuTucInfo.parent_name = Utils.ConvertToString(dr["parent_name"], string.Empty);
                        loaiThuTucInfo.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        loaiThuTucInfo.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        loaiThuTucs.Add(loaiThuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return loaiThuTucs;
        }
        public DMLoaiThuTucInfo GetByID(int id)
        {
            DMLoaiThuTucInfo loaiThuTucs = new DMLoaiThuTucInfo();
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int)
            };
            parameters[0].Value = id;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        loaiThuTucs = GetData(dr);
                        loaiThuTucs.parent_name = Utils.ConvertToString(dr["parent_name"], string.Empty);
                        loaiThuTucs.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        loaiThuTucs.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        loaiThuTucs.CoSoPhapLy = Utils.ConvertToString(dr["CoSoPhapLy"], string.Empty);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return loaiThuTucs;
        }
        public List<DMLoaiThuTucInfo> GetAll()
        {
            List<DMLoaiThuTucInfo> info = new List<DMLoaiThuTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        DMLoaiThuTucInfo infos = Get(dr);
                        info.Add(infos);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return info;
        }
        public List<DMLoaiThuTucInfo> GetAll_Frontend()
        {
            List<DMLoaiThuTucInfo> info = new List<DMLoaiThuTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL_FRONTEND, null))
                {
                    while (dr.Read())
                    {
                        DMLoaiThuTucInfo infos = Get(dr);
                        info.Add(infos);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return info;
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

        public int Count()
        {
            int result = 0;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT))
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

        #region // insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_PARENTID, SqlDbType.Int),
                new SqlParameter(PARM_TENLOAITHUTUC, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),
                new SqlParameter(PARM_CREATER, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_FILEURL, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_FILENAME, SqlDbType.NVarChar,50),
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, DMLoaiThuTucInfo loaiThuTucInfo)
        {
            parms[0].Value = loaiThuTucInfo.ParentID;
            if (loaiThuTucInfo.ParentID == 0)
                parms[0].Value = -1;
            parms[1].Value = loaiThuTucInfo.TenLoaiThuTuc;
            parms[2].Value = loaiThuTucInfo.GhiChu;
            parms[3].Value = loaiThuTucInfo.Public;
            parms[4].Value = loaiThuTucInfo.Creater;
            parms[5].Value = loaiThuTucInfo.CreateDate;
            parms[6].Value = loaiThuTucInfo.FileUrl;
            parms[7].Value = loaiThuTucInfo.FileName;
        }

        public int Insert(DMLoaiThuTucInfo loaiThuTucInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, loaiThuTucInfo);
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

        #region // delete

        public int Delete(int id)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int), 
            };
            parameters[0].Value = id;
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

        #region // update

        private SqlParameter[] GetUpdateParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int),
                new SqlParameter(PARM_PARENTID, SqlDbType.Int),
                new SqlParameter(PARM_TENLOAITHUTUC, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_FILEURL, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_FILENAME, SqlDbType.NVarChar,50),
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, DMLoaiThuTucInfo loaiThuTicInfo)
        {
            parms[0].Value = loaiThuTicInfo.LoaiThuTucID;
            parms[1].Value = loaiThuTicInfo.ParentID;
            if (loaiThuTicInfo.ParentID == 0)
                parms[1].Value = DBNull.Value;
            parms[2].Value = loaiThuTicInfo.TenLoaiThuTuc;
            parms[3].Value = loaiThuTicInfo.GhiChu;
            parms[4].Value = loaiThuTicInfo.Public;
            parms[5].Value = loaiThuTicInfo.Editer;
            parms[6].Value = loaiThuTicInfo.EditDate;
            parms[7].Value = loaiThuTicInfo.FileUrl;
            parms[8].Value = loaiThuTicInfo.FileName;
        }

        public int Update(DMLoaiThuTucInfo loaiThuTucInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, loaiThuTucInfo);
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
    }
}
