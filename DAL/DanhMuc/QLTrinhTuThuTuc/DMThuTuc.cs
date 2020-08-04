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
    public class DMThuTuc
    {

        #region Database query string

        private const string GET_LOAITHUTUC = @"DM_ThuTuc_GetLoaiThuTuc";
        private const string GET_LOAITHUTUCCON = @"DM_ThuTuc_GetLoaiThuTucCon";
        private const string GET_BY_LOAITHUTUCID = @"DM_ThuTuc_GetByLoaiThuTucID";
        private const string GET_BY_ID = @"DM_ThuTuc_GetByID";
        private const string GET_ALL = @"DM_ThuTuc_GetAll";
        private const string GET_BY_SEARCH = @"DM_ThuTuc_GetBySearch";
        private const string GET_BY_ORDER = @"DM_ThuTuc_GetByOrder";



        private const string COUNT_BY_SEARCH = @"DM_ThuTuc_CountBySearch";
        private const string COUNT = @"DM_ThuTuc_Count";

        private const string DELETE = @"DM_ThuTuc_Delete";
        private const string UPDATE = @"DM_ThuTuc_Update";
        private const string INSERT = @"DM_ThuTuc_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_THUTUCID = @"ThuTucID";
        private const string PARM_LOAITHUTUCID = @"pLoaiThuTucID";
        private const string PARM_NDTHUTUC = @"NDThuTuc";
        private const string PARM_ORDER = @"Order";
        private const string PARM_CREATER = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";
        private const string PARM_FILE = @"FileDinhKem";

        private const string PARM_START = @"pStart";
        private const string PARM_END = @"pEnd";
        private const string PARM_KEYWORD = @"pKeyWord";

        #endregion


        #region // Get
        private DMThuTucInfo GetData(SqlDataReader rdr)
        {
            DMThuTucInfo ThuTucInfo = new DMThuTucInfo();
            ThuTucInfo.ThuTucID = Utils.GetInt32(rdr["ThuTucID"], 0);
            ThuTucInfo.LoaiThuTucID = Utils.GetInt32(rdr["LoaiThuTucID"], 0);
            ThuTucInfo.NDThuTuc = Utils.GetString(rdr["NDThuTuc"], String.Empty);
            ThuTucInfo.Order = Utils.GetInt32(rdr["Order"], 0);
            ThuTucInfo.Creater = Utils.GetInt32(rdr["Creater"], 0);
            ThuTucInfo.Editer = Utils.GetInt32(rdr["Editer"], 0);
            ThuTucInfo.creater_name = Utils.GetString(rdr["creater_name"], string.Empty);
            ThuTucInfo.editer_name = Utils.GetString(rdr["editer_name"], string.Empty);
            ThuTucInfo.loaithutuc_name = Utils.GetString(rdr["loaithutuc_name"], string.Empty);

            return ThuTucInfo;
        }
        private DMThuTucInfo Get(SqlDataReader rdr)
        {
            DMThuTucInfo ThuTucInfo = new DMThuTucInfo();
            ThuTucInfo.ThuTucID = Utils.GetInt32(rdr["ThuTucID"], 0);
            ThuTucInfo.LoaiThuTucID = Utils.GetInt32(rdr["LoaiThuTucID"], 0);
            ThuTucInfo.NDThuTuc = Utils.GetString(rdr["NDThuTuc"], String.Empty);
            ThuTucInfo.Order = Utils.GetInt32(rdr["Order"], 0);
            ThuTucInfo.Creater = Utils.GetInt32(rdr["Creater"], 0);
            ThuTucInfo.Editer = Utils.GetInt32(rdr["Editer"], 0);
            ThuTucInfo.loaithutuc_name = Utils.GetString(rdr["loaithutuc_name"], string.Empty);

            return ThuTucInfo;
        }
        public List<DMThuTucInfo> GetByLoaiThuTucID(int id)
        {
            List<DMThuTucInfo> ThuTucs = new List<DMThuTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int) ,
            };
            parameters[0].Value = id;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_LOAITHUTUCID, parameters))
                {
                    while (dr.Read())
                    {
                        DMThuTucInfo ThuTucInfo = GetData(dr);
                        ThuTucInfo.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        ThuTucInfo.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        ThuTucInfo.loaithutuc_name = Utils.ConvertToString(dr["loaithutuc_name"], string.Empty);
                        ThuTucs.Add(ThuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return ThuTucs;
        }
        public DMThuTucInfo GetByOrder(int loaithutuc_id)
        {
            DMThuTucInfo ThuTucs = new DMThuTucInfo();
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int)
            };
            parameters[0].Value = loaithutuc_id;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ORDER, parameters))
                {
                    if (dr.Read())
                    {
                        ThuTucs = GetData(dr);
                        ThuTucs.Order = Utils.ConvertToInt32(dr["Order"], 0);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return ThuTucs;
        }
        public List<DMThuTucInfo> GetLoaiThuTucCon(int id)
        {
            List<DMThuTucInfo> ThuTucs = new List<DMThuTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int) ,
                  new SqlParameter(PARM_END, SqlDbType.Int) 
            };
            parameters[0].Value = id;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_LOAITHUTUCCON, parameters))
                {
                    while (dr.Read())
                    {
                        DMThuTucInfo ThuTucInfo = GetData(dr);
                        ThuTucInfo.loaithutuc_name = Utils.ConvertToString(dr["loaithutuc_name"], string.Empty);
                        ThuTucs.Add(ThuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return ThuTucs;
        }
        public List<DMThuTucInfo> GetLoaiThuTuc()
        {
            List<DMThuTucInfo> loaiThuTucs = new List<DMThuTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_LOAITHUTUC, null))
                {
                    while (dr.Read())
                    {
                        DMThuTucInfo thutucInfo = Get(dr);
                        loaiThuTucs.Add(thutucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiThuTucs;
        }
        public DMThuTucInfo GetByID(int id)
        {
            DMThuTucInfo ThuTucs = new DMThuTucInfo();
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(PARM_THUTUCID, SqlDbType.Int)
            };
            parameters[0].Value = id;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        ThuTucs = GetData(dr);
                        ThuTucs.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        ThuTucs.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return ThuTucs;
        }
        public List<DMThuTucInfo> GetAll()
        {
            List<DMThuTucInfo> info = new List<DMThuTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        DMThuTucInfo infos = GetData(dr);
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
        public List<DMThuTucInfo> GetBySearch(string keySearch, int start, int end)
        {
            List<DMThuTucInfo> loaiThuTucs = new List<DMThuTucInfo>();
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
                        DMThuTucInfo thuTucInfo = GetData(dr);
                        thuTucInfo.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        thuTucInfo.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        thuTucInfo.loaithutuc_name = Utils.ConvertToString(dr["loaithutuc_name"], string.Empty);
                        loaiThuTucs.Add(thuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiThuTucs;
        }
        public List<DMThuTucInfo> GetBySearch1(string keySearch, int start, int end)
        {
            List<DMThuTucInfo> loaiThuTucs = new List<DMThuTucInfo>();
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
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DM_ThuTuc_GetBySearch_1", parameters))
                {
                    while (dr.Read())
                    {
                        DMThuTucInfo thuTucInfo = GetData(dr);
                        thuTucInfo.creater_name = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        thuTucInfo.editer_name = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        thuTucInfo.loaithutuc_name = Utils.ConvertToString(dr["loaithutuc_name"], string.Empty);
                        thuTucInfo.CoSoPhapLy = Utils.ConvertToString(dr["CoSoPhapLy"], string.Empty);
                        thuTucInfo.FileDinhKem = Utils.ConvertToString(dr["FileDinhKem"], string.Empty);
                        loaiThuTucs.Add(thuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return loaiThuTucs;
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

        public int CountSearch1(string keyword)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,100)
            };
            parameters[0].Value = keyword;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DM_ThuTuc_CountBySearch_1", parameters))
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
                new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int),
                new SqlParameter(PARM_ORDER, SqlDbType.Int),
                new SqlParameter(PARM_NDTHUTUC, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_CREATER, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_FILE, SqlDbType.NVarChar,500)
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, DMThuTucInfo ThuTucInfo)
        {
            parms[0].Value = ThuTucInfo.LoaiThuTucID;
            parms[1].Value = ThuTucInfo.Order;
            parms[2].Value = ThuTucInfo.NDThuTuc;
            parms[3].Value = ThuTucInfo.Creater;
            parms[4].Value = ThuTucInfo.CreateDate;
            parms[5].Value = ThuTucInfo.FileDinhKem;
        }

        public int Insert(DMThuTucInfo ThuTucInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, ThuTucInfo);
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
                new SqlParameter(PARM_THUTUCID, SqlDbType.Int), 
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
                new SqlParameter(PARM_THUTUCID, SqlDbType.Int),
                new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int),
                new SqlParameter(PARM_ORDER, SqlDbType.Int),
                new SqlParameter(PARM_NDTHUTUC, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_FILE, SqlDbType.NVarChar,500)
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, DMThuTucInfo thuTucInfo)
        {
            parms[0].Value = thuTucInfo.ThuTucID;
            parms[1].Value = thuTucInfo.LoaiThuTucID;
            parms[2].Value = thuTucInfo.Order;
            parms[3].Value = thuTucInfo.NDThuTuc;
            parms[4].Value = thuTucInfo.Editer;
            parms[5].Value = thuTucInfo.EditDate;
            parms[6].Value = thuTucInfo.FileDinhKem;
        }

        public int Update(DMThuTucInfo ThuTucInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, ThuTucInfo);
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



        public List<DMThuTucInfo> GetStepsByThuTuc(int id)
        {
            List<DMThuTucInfo> steps = new List<DMThuTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(PARM_LOAITHUTUCID, SqlDbType.Int) ,
            };
            parameters[0].Value = id;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DM_ThuTuc_GetByLoaiThuTucID_AndChild", parameters))
                {
                    while (dr.Read())
                    {
                        DMThuTucInfo ThuTucInfo = new DMThuTucInfo();
                        ThuTucInfo.ThuTucID = Utils.GetInt32(dr["ThuTucID"], 0);
                        ThuTucInfo.LoaiThuTucID = Utils.GetInt32(dr["LoaiThuTucID"], 0);
                        ThuTucInfo.NDThuTuc = Utils.GetString(dr["NDThuTuc"], String.Empty);
                        ThuTucInfo.Order = Utils.GetInt32(dr["Order"], 0);
                        ThuTucInfo.Creater = Utils.GetInt32(dr["Creater"], 0);
                        ThuTucInfo.Editer = Utils.GetInt32(dr["Editer"], 0);
                        ThuTucInfo.FileDinhKem = Utils.ConvertToString(dr["FileDinhKem"], string.Empty);
                        steps.Add(ThuTucInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return steps;
        }

        public int GetNumSteps(int id)
        {
            int val=0;
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter(@"LoaiThuTucID", SqlDbType.Int) ,
            };
            parameters[0].Value = id;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DM_ThuTuc_GetNumSteps_ByLoaiThuTuc", parameters))
                {
                    if (dr.Read())
                    {
                        val = Utils.GetInt32(dr["CountSteps"], 0);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return val;
        }
    }
}
