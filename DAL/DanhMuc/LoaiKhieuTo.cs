using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Com.Gosol.CMS.Utility;
using System.Data;
using System.Reflection;
using Com.Gosol.CMS.Model;
using System;

namespace Com.Gosol.CMS.DAL
{
    public class LoaiKhieuTo
    {
        #region Database query string

        private const string SELECT_ALL = @"DM_LoaiKhieuTo_GetAll";
        private const string SELECT_ALL_FOR_AJAX = @"DM_LoaiKhieuTo_GetAllForAjax";
        private const string SELECT_LEVEL1 = @"DM_LoaiKhieuTo_GetAllWithLevel1";
        private const string SELECT_BY_ID = @"DM_LoaiKhieuTo_GetByID";
        private const string SELECT_BY_PARENTID = @"DM_LoaiKhieuTo_GetByParentID";
        private const string DELETE = @"DM_LoaiKhieuTo_Delete";
        private const string UPDATE = @"DM_LoaiKhieuTo_Update";
        private const string INSERT = @"DM_LoaiKhieuTo_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_LOAIKHIEUTOID = @"LoaiKhieuToID";
        private const string PARM_TENLOAIKHIEUTO = @"TenLoaiKhieuTo";
        private const string PARM_LOAIKHIEUTOCHA = @"LoaiKhieuToCha";
        private const string PARM_CAP = @"Cap";
        //private const string PARM_TENDAYDU = @"TenDayDu";
        //private const string PARM_VALID = @"Valid";

        #endregion

        private LoaiKhieuToInfo GetData(SqlDataReader rdr)
        {
            LoaiKhieuToInfo loaiKhieuToInfo = new LoaiKhieuToInfo();
            loaiKhieuToInfo.LoaiKhieuToID = Utils.GetInt32(rdr["LoaiKhieuToID"], 0);
            loaiKhieuToInfo.TenLoaiKhieuTo = Utils.GetString(rdr["TenLoaiKhieuTo"], String.Empty);

            loaiKhieuToInfo.LoaiKhieuToCha = Utils.GetInt32(rdr["LoaiKhieuToCha"], 0);
            loaiKhieuToInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);

            //loaiKhieuToInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
            //loaiKhieuToInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return loaiKhieuToInfo;
        }

        private LoaiKhieuToInfo GetDataForAjax(SqlDataReader rdr)
        {
            LoaiKhieuToInfo loaiKhieuToInfo = new LoaiKhieuToInfo();
            loaiKhieuToInfo.LoaiKhieuToID = Utils.GetInt32(rdr["LoaiKhieuToID"], 0);
            loaiKhieuToInfo.TenLoaiKhieuTo = Utils.GetString(rdr["TenLoaiKhieuTo"], String.Empty);

            loaiKhieuToInfo.LoaiKhieuToCha = Utils.GetInt32(rdr["LoaiKhieuToCha"], 0);
            loaiKhieuToInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);

            //loaiKhieuToInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);

            loaiKhieuToInfo.hasChild = Utils.GetInt32(rdr["hasChild"], 0);

            //loaiKhieuToInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return loaiKhieuToInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                //new SqlParameter(PARM_LOAIKHIEUTOID, SqlDbType.Int),
                new SqlParameter(PARM_TENLOAIKHIEUTO, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_LOAIKHIEUTOCHA, SqlDbType.Int),
                new SqlParameter(PARM_CAP, SqlDbType.Int),
                //new SqlParameter(PARM_TENDAYDU, SqlDbType.NVarChar, 150)
                
            }; 
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, LoaiKhieuToInfo loaiKhieuToInfo)
        {
            int index = 0;
            
            foreach (PropertyInfo proInfo in loaiKhieuToInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && (proInfo.Name != "LoaiKhieuToID" && proInfo.Name != "hasChild"))
                {
                    parms[index].Value = proInfo.GetValue(loaiKhieuToInfo, null);
                    index++;
                }
            }
        }

        private SqlParameter[] GetUpdateParms()
        {
            List<SqlParameter> parms = GetInsertParms().ToList();
            parms.Insert(0, new SqlParameter(PARM_LOAIKHIEUTOID, SqlDbType.Int));
            return parms.ToArray();
        }

        private void SetUpdateParms(SqlParameter[] parms, LoaiKhieuToInfo loaiKhieuToInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in loaiKhieuToInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && proInfo.Name != "hasChild")
                {
                    parms[index].Value = proInfo.GetValue(loaiKhieuToInfo, null);
                    index++;
                }
            }
        }

        //check dia chi da ton tai
        private const string CHECK_EXISTS_LOAIKHIEUTO = @"DM_LoaiKhieuTo_CheckExistsName";
        public Boolean checkExistsLoaiKhieuTo(string tenLoaiKhieuTo, int loaiKhieuToCha, int loaiKhieuToId)
        {
            bool valid = false;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENLOAIKHIEUTO, SqlDbType.NVarChar),
                new SqlParameter(PARM_LOAIKHIEUTOCHA, SqlDbType.Int), 
                new SqlParameter(PARM_LOAIKHIEUTOID, SqlDbType.Int)
            };
            parameters[0].Value = tenLoaiKhieuTo;
            parameters[1].Value = loaiKhieuToCha;
            parameters[2].Value = loaiKhieuToId;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, CHECK_EXISTS_LOAIKHIEUTO, parameters))
            {
                while (dr.Read())
                {
                    valid = Utils.GetInt32(dr["isExists"], 0) > 0 ? true : false;
                }
                dr.Close();
            }
            return valid;
        }

        public IList<LoaiKhieuToInfo> GetLoaiKhieuTos()
        {
            IList<LoaiKhieuToInfo> loaiKhieuTos = new List<LoaiKhieuToInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL, null))
            {
                while (dr.Read())
                {
                    LoaiKhieuToInfo loaiKhieuToInfo = GetData(dr);
                    loaiKhieuTos.Add(loaiKhieuToInfo);
                }
                dr.Close();
            }
            return loaiKhieuTos;
        }

        public IList<LoaiKhieuToInfo> GetLoaiKhieuToForAjax()
        {
            IList<LoaiKhieuToInfo> loaiKhieuTos = new List<LoaiKhieuToInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL_FOR_AJAX, null))
            {
                while (dr.Read())
                {
                    LoaiKhieuToInfo loaiKhieuToInfo = GetDataForAjax(dr);
                    loaiKhieuTos.Add(loaiKhieuToInfo);
                }
                dr.Close();
            }
            return loaiKhieuTos;
        }

        //lay loai khieu to level 1 (cha = null)
        public IList<LoaiKhieuToInfo> GetLoaiKhieuToWithLevel1()
        {
            IList<LoaiKhieuToInfo> loaiKhieuTos = new List<LoaiKhieuToInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_LEVEL1, null))
            {
                while (dr.Read())
                {
                    LoaiKhieuToInfo loaiKhieuToInfo = GetData(dr);
                    loaiKhieuTos.Add(loaiKhieuToInfo);
                }
                dr.Close();
            }
            return loaiKhieuTos;
        }

        //Dia Chi Suggestion
        private const string LOAIKHIEUTO_SUGGESTION = @"DM_LoaiKhieuTo_GetSuggestion";
        public IList<LoaiKhieuToInfo> GetLoaiKhieuToSuggestion(string tenLoaiKhieuTo)
        {
            IList<LoaiKhieuToInfo> loaiKhieuTos = new List<LoaiKhieuToInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENLOAIKHIEUTO, SqlDbType.NVarChar) 
            };
            parameters[0].Value = tenLoaiKhieuTo;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, LOAIKHIEUTO_SUGGESTION, parameters))
            {
                while (dr.Read())
                {
                    LoaiKhieuToInfo loaiKhieuToInfo = GetData(dr);
                    loaiKhieuTos.Add(loaiKhieuToInfo);
                }
                dr.Close();
            }
            return loaiKhieuTos;
        }

        //Dia Chi Search
        private const string LOAIKHIEUTO_SEARCH = @"DM_LoaiKhieuTo_GetLoaiKhieuTo_Search";
        public IList<LoaiKhieuToInfo> GetLoaiKhieuToSearch(string keySearch)
        {
            IList<LoaiKhieuToInfo> loaiKhieuTos = new List<LoaiKhieuToInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENLOAIKHIEUTO, SqlDbType.NVarChar) 
            };
            parameters[0].Value = keySearch;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, LOAIKHIEUTO_SEARCH, parameters))
            {
                while (dr.Read())
                {
                    LoaiKhieuToInfo loaiKhieuToInfo = GetData(dr);
                    loaiKhieuTos.Add(loaiKhieuToInfo);
                }
                dr.Close();
            }
            return loaiKhieuTos;
        }

        public IList<LoaiKhieuToInfo> GetLoaiKhieuToByParentID(int loaiKhieuToID)
        {
            IList<LoaiKhieuToInfo> loaiKhieuTos = new List<LoaiKhieuToInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_LOAIKHIEUTOID, SqlDbType.Int) 
            };
            parameters[0].Value = loaiKhieuToID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_PARENTID, parameters))
            {
                while (dr.Read())
                {
                    LoaiKhieuToInfo loaiKhieuToInfo = GetDataForAjax(dr);
                    loaiKhieuTos.Add(loaiKhieuToInfo);
                }
                dr.Close();
            }
            return loaiKhieuTos;
        }

        public LoaiKhieuToInfo GetLoaiKhieuToByID(int loaiKhieuToID)
        {
            LoaiKhieuToInfo loaiKhieuToInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_LOAIKHIEUTOID, SqlDbType.Int) 
            };
            parameters[0].Value = loaiKhieuToID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    loaiKhieuToInfo = GetData(dr);
                }
                dr.Close();
            }
            return loaiKhieuToInfo;
        }

        public int Delete(int loaiKhieuToID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_LOAIKHIEUTOID, SqlDbType.Int) };
            parameters[0].Value = loaiKhieuToID;
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

        public int Update(LoaiKhieuToInfo loaiKhieuToInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, loaiKhieuToInfo);
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


        public int Insert(LoaiKhieuToInfo loaiKhieuToInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, loaiKhieuToInfo);
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
        private const string GET_LOAI_KHIEU_TO_CHA = @"DM_LoaiKhieuTo_Get_LoaiKhieuToCha";
        public IList<LoaiKhieuToInfo> GetLoaiKhieuToCha()
        {
            IList<LoaiKhieuToInfo> LsNN = new List<LoaiKhieuToInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_LOAI_KHIEU_TO_CHA, null))
                {

                    while (dr.Read())
                    {

                        LoaiKhieuToInfo NNInfo = GetData(dr);
                        LsNN.Add(NNInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return LsNN;
        }
        //public IList<TrangThaiDonInfo> GetAll()
        //{
        //    IList<TrangThaiDonInfo> trangthais = new List<TrangThaiDonInfo>();
        //    try
        //    {
        //        using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
        //        {

        //            while (dr.Read())
        //            {

        //                TrangThaiDonInfo cInfo = GetData(dr);
        //                trangthais.Add(cInfo);
        //            }
        //            dr.Close();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return trangthais;
        //}

    }
}
