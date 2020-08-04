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
    public class FileTaiLieu
    {
        //Su dung de goi StoreProcedure

        private const string GET_ALL = @"FileTaiLieu_GetAll";
        private const string GET_BY_ID = @"FileTaiLieu_GetByID";
        private const string INSERT = @"FileTaiLieu_Insert";
        private const string UPDATE = @"FileTaiLieu_Update";
        private const string DELETE = @"FileTaiLieu_Delete";

        private const string GET_BY_PAGE = @"FileTaiLieu_GetByPage";
        private const string COUNT_ALL = @"FileTaiLieu_CountAll";
        private const string COUNT_SEARCH = @"FileTaiLieu_CountSearch";
        private const string SEARCH = @"FileTaiLieu_GetBySearch";


        //Ten cac bien dau vao
        private const string PARAM_FILETAILIEU_ID = "@FileTaiLieuID";
        private const string PARAM_TENFILE = "@TenFile";
        private const string PARAM_TOMTAT = "@TomTat";
        private const string PARAM_NGUOIUP = "@NguoiUp";
        private const string PARAM_NGAYUP = "@NgayUp";
        private const string PARAM_FILEURL = "@FileUrl";

        private const string PARAM_KEY = "@Keyword";
        private const string PARAM_START = "@Start";
        private const string PARAM_END = "@End";


        private FileTaiLieuInfo GetData(SqlDataReader dr)
        {
            FileTaiLieuInfo DTInfo = new FileTaiLieuInfo();
            DTInfo.FileTaiLieuID = Utils.GetInt32(dr["FileTaiLieuID"].ToString(), 0);
            DTInfo.TenFile = Utils.GetString(dr["TenFile"].ToString(), string.Empty);
            DTInfo.TomTat = Utils.GetString(dr["TomTat"].ToString(), string.Empty);
            DTInfo.NguoiUp = Utils.GetInt32(dr["NguoiUp"].ToString(), 0);
            DTInfo.NgayUp = Utils.GetDateTime(dr["NgayUp"], DateTime.MinValue);
            DTInfo.FileUrl = Utils.GetString(dr["FileUrl"].ToString(), string.Empty);

            return DTInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter(PARAM_TENFILE, SqlDbType.NVarChar, 150),
                new SqlParameter(PARAM_TOMTAT, SqlDbType.NVarChar, 250),
                new SqlParameter(PARAM_NGUOIUP, SqlDbType.Int),
                new SqlParameter(PARAM_NGAYUP, SqlDbType.DateTime),
                new SqlParameter(PARAM_FILEURL, SqlDbType.NVarChar, 250)
                };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, FileTaiLieuInfo DTInfo)
        {

            parms[0].Value = DTInfo.TenFile;
            parms[1].Value = DTInfo.TomTat;
            parms[2].Value = DTInfo.NguoiUp;
            parms[3].Value = DTInfo.NgayUp;
            parms[4].Value = DTInfo.FileUrl;

        }

        private SqlParameter[] GetUpdateParms()
        {
            SqlParameter[] parms = new SqlParameter[]{
                
                new SqlParameter(PARAM_FILETAILIEU_ID, SqlDbType.Int),
                new SqlParameter(PARAM_TENFILE, SqlDbType.NVarChar, 150),
                new SqlParameter(PARAM_TOMTAT, SqlDbType.NVarChar, 250),
                new SqlParameter(PARAM_NGUOIUP, SqlDbType.Int),
                new SqlParameter(PARAM_NGAYUP, SqlDbType.DateTime),
                new SqlParameter(PARAM_FILEURL, SqlDbType.NVarChar, 250)
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, FileTaiLieuInfo DTInfo)
        {
            parms[0].Value = DTInfo.FileTaiLieuID;
            parms[1].Value = DTInfo.TenFile;
            parms[2].Value = DTInfo.TomTat;
            parms[3].Value = DTInfo.NguoiUp;
            parms[4].Value = DTInfo.NgayUp;
            parms[5].Value = DTInfo.FileUrl;

        }

        public IList<FileTaiLieuInfo> GetAll()
        {
            IList<FileTaiLieuInfo> LsDT = new List<FileTaiLieuInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {

                    while (dr.Read())
                    {

                        FileTaiLieuInfo DTInfo = GetData(dr);
                        LsDT.Add(DTInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return LsDT;
        }

        public FileTaiLieuInfo GetByID(int cID)
        {

            FileTaiLieuInfo DTInfo = null;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARAM_FILETAILIEU_ID,SqlDbType.Int)
            };
            parameters[0].Value = cID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {

                    if (dr.Read())
                    {
                        DTInfo = GetData(dr);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return DTInfo;
        }

        public int Delete(int DT_ID)
        {

            int val = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARAM_FILETAILIEU_ID,SqlDbType.Int)
            };
            parameters[0].Value = DT_ID;
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

        public int Update(FileTaiLieuInfo DTInfo)
        {

            int val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, DTInfo);

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

        public int Insert(FileTaiLieuInfo DTInfo)
        {
            object val;

            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, DTInfo);

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
            return Utils.ConvertToInt32(val, 0);
        }

        //search chua co phan trang
        public IList<FileTaiLieuInfo> Search(string key)
        {
            IList<FileTaiLieuInfo> dantocs = new List<FileTaiLieuInfo>();
            FileTaiLieuInfo cInfo = null;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARAM_KEY,SqlDbType.NVarChar,50)
            };
            parameters[0].Value = key;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SEARCH, parameters))
                {

                    while (dr.Read())
                    {
                        cInfo = GetData(dr);
                        dantocs.Add(cInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

                throw;
            }

            return dantocs;
        }

        //search da co phan trang
        public IList<FileTaiLieuInfo> GetBySearch(string keyword, int page, int start, int end)
        {

            IList<FileTaiLieuInfo> dantocs = new List<FileTaiLieuInfo>();
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARAM_KEY,SqlDbType.NVarChar,50),
                new SqlParameter(PARAM_START,SqlDbType.Int),
                new SqlParameter(PARAM_END,SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = start;
            parameters[2].Value = end;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SEARCH, parameters))
                {
                    while (dr.Read())
                    {
                        FileTaiLieuInfo dtInfo = GetData(dr);
                        dantocs.Add(dtInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

                throw;
            }
            return dantocs;
        }


        public IList<FileTaiLieuInfo> GetByPage(int page, int start, int end)
        {
            

            IList<FileTaiLieuInfo> ls_dantoc = new List<FileTaiLieuInfo>();
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARAM_START,SqlDbType.Int),
                new SqlParameter(PARAM_END,SqlDbType.Int)
            };
            parameters[0].Value = start;
            parameters[1].Value = end;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_PAGE, parameters))
                {
                    while (dr.Read())
                    {
                        FileTaiLieuInfo DTInfo = GetData(dr);
                        ls_dantoc.Add(DTInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

                throw;
            }
            return ls_dantoc;
        }

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
            catch
            {

                throw;
            }
            return result;
        }

        //dem ket qua search duoc
        public int CountSearch(string keyword)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARAM_KEY,SqlDbType.NVarChar,50)            
            };
            parameters[0].Value = keyword;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_SEARCH, parameters))
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
    }
}
