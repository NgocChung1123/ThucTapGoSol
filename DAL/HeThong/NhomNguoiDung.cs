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
    public class NhomNguoiDung
    {
        #region Database query string

        private const string SELECT_ALL = @"NhomNguoiDung_GetAll";
        private const string SELECT_BY_ID = @"NhomNguoiDung_GetByID";
        private const string DELETE = @"NhomNguoiDung_Delete";
        private const string UPDATE = @"NhomNguoiDung_Update";
        private const string INSERT = @"NhomNguoiDung_Insert";

        private const string GET_BY_PAGE = @"NhomNguoiDung_GetByPage";
        private const string GET_ALL_JOIN = @"NhomNguoiDung_GetAllJoin";
        private const string GET_BY_SEARCH = @"NhomNguoiDung_GetBySearch";
        private const string GET_BY_COQUAN_ID = @"NhomNguoiDung_GetByCoQuanID";

        private const string GET_BY_CHUCNANG = "NhomNguoiDung_GetByChucNang";

        private const string COUNT_ALL = @"NhomNguoiDung_CountAll";
        private const string COUNT_SEARCH = @"NhomNguoiDung_CountSearch";

        private const string SELECT_BY_USER = @"NhomNguoiDung_GetByUser";

        private const string ADD_ROLE = @"NhomNguoiDung_AddRole";
        private const string UPDATE_ROLE = @"NhomNguoiDung_UpdateRole";
        private const string REMOVE_ROLE = @"NhomNguoiDung_RemoveRole";

        #endregion

        #region paramaters constant

        private const string PARM_NHOMNGUOIDUNGID = @"NhomNguoiDungID";
        private const string PARM_TENNHOM = @"TenNhom";
        private const string PARM_GHICHU = @"GhiChu";
        private const string PARM_COQUANID = @"CoQuanID";

        private const string PARM_START = @"Start";
        private const string PARM_END = @"End";
        private const string PARM_KEYWORD = @"Keyword";

        private const string PARM_NGUOIDUNGID = @"NguoiDungID";
        private const string PARM_CHUCNANGID = "ChucNangID";
        private const string PARM_QUYEN = "Quyen";

        #endregion

        private NhomNguoiDungInfo GetData(SqlDataReader rdr)
        {
            NhomNguoiDungInfo nhomNguoiDungInfo = new NhomNguoiDungInfo();
            nhomNguoiDungInfo.NhomNguoiDungID = Utils.GetInt32(rdr["NhomNguoiDungID"], 0);
            nhomNguoiDungInfo.TenNhom = Utils.GetString(rdr["TenNhom"], String.Empty);
            nhomNguoiDungInfo.GhiChu = Utils.GetString(rdr["GhiChu"], String.Empty);
            return nhomNguoiDungInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_TENNHOM, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_COQUANID, SqlDbType.Int),
            }; 
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, NhomNguoiDungInfo nhomNguoiDungInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in nhomNguoiDungInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && proInfo.Name != "NhomNguoiDungID")
                {
                    parms[index].Value = proInfo.GetValue(nhomNguoiDungInfo, null);
                    index++;
                }
            }
        }

        private SqlParameter[] GetUpdateParms()
        {
            List<SqlParameter> parms = GetInsertParms().ToList();
            parms.Insert(0, new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int));
            return parms.ToArray();
        }

        private void SetUpdateParms(SqlParameter[] parms, NhomNguoiDungInfo nhomNguoiDungInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in nhomNguoiDungInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead)
                {
                    parms[index].Value = proInfo.GetValue(nhomNguoiDungInfo, null);
                    index++;
                }
            }
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
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        public int CountSearch(String keyword)
        {
            int result = 0;
            SqlParameter parm = new SqlParameter(PARM_KEYWORD, SqlDbType.Int);
            parm.Value = keyword;
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

        public IList<NhomNguoiDungInfo> GetBySearch(String keyword, int start, int end)
        {
            
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;

            IList<NhomNguoiDungInfo> nhomList = new List<NhomNguoiDungInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parm))
                {
                    while (dr.Read())
                    {
                        NhomNguoiDungInfo nhomInfo = GetData(dr);
                        nhomList.Add(nhomInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nhomList;
        }

        public IList<NhomNguoiDungInfo> GetByCoQuanID(String keyword, int start, int end, int coQuanID)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int),
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            parm[3].Value = coQuanID;

            IList<NhomNguoiDungInfo> nhomList = new List<NhomNguoiDungInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_COQUAN_ID, parm))
                {
                    while (dr.Read())
                    {
                        NhomNguoiDungInfo nhomInfo = GetData(dr);
                        nhomInfo.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        nhomList.Add(nhomInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nhomList;
        }

        public IList<NhomNguoiDungInfo> GetByPage(int start, int end)
        {
            IList<NhomNguoiDungInfo> nhomList = new List<NhomNguoiDungInfo>();
            
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
                        NhomNguoiDungInfo nhomInfo = GetData(dr);
                        nhomList.Add(nhomInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nhomList;
        }

        public IList<NhomNguoiDungInfo> GetAll()
        {
            IList<NhomNguoiDungInfo> nhomnguoidungs = new List<NhomNguoiDungInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL, null))
                {
                    while (dr.Read())
                    {
                        NhomNguoiDungInfo nhomNguoiDungInfo = GetData(dr);
                        nhomnguoidungs.Add(nhomNguoiDungInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return nhomnguoidungs;
        }

        public IList<NhomNguoiDungInfo> GetByUser(int userID)
        {
            IList<NhomNguoiDungInfo> nhomnguoidungs = new List<NhomNguoiDungInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int) 
            };
            parameters[0].Value = userID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_USER, parameters))
                {
                    while (dr.Read())
                    {
                        NhomNguoiDungInfo nhomNguoiDungInfo = GetData(dr);
                        nhomnguoidungs.Add(nhomNguoiDungInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return nhomnguoidungs;
        }

        public IList<NhomNguoiDungInfo> GetByChucNang(int cnID)
        {
            IList<NhomNguoiDungInfo> nhomnguoidungs = new List<NhomNguoiDungInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int) 
            };
            parameters[0].Value = cnID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_CHUCNANG, parameters))
                {
                    while (dr.Read())
                    {
                        NhomNguoiDungInfo nhomNguoiDungInfo = GetData(dr);
                        nhomnguoidungs.Add(nhomNguoiDungInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return nhomnguoidungs;
        }

        public NhomNguoiDungInfo GetByID(int nhomnguoidungID)
        {
            NhomNguoiDungInfo nhomNguoiDungInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int) 
            };
            parameters[0].Value = nhomnguoidungID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        nhomNguoiDungInfo = GetData(dr);
                        nhomNguoiDungInfo.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return nhomNguoiDungInfo;
        }

        public int Delete(int nhomnguoidungID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int) };
            parameters[0].Value = nhomnguoidungID;
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

        public int Update(NhomNguoiDungInfo nhomNguoiDungInfo)
        {
            int val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, nhomNguoiDungInfo);
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


        public int Insert(NhomNguoiDungInfo nhomNguoiDungInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, nhomNguoiDungInfo);
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
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                conn.Close();
            }
            return Convert.ToInt32(val);
        }

        public int AddChucNang(int nhomID, int chucNangID, int quyen)
        {
            object val = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int),
                new SqlParameter(PARM_QUYEN, SqlDbType.Int)
            };
            parameters[0].Value = nhomID;
            parameters[1].Value = chucNangID;
            parameters[2].Value = quyen;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, ADD_ROLE, parameters);
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

        public int UpdateChucNang(int nhomID, int chucNangID, int quyen)
        {
            object val = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int),
                new SqlParameter(PARM_QUYEN, SqlDbType.Int)
            };
            parameters[0].Value = nhomID;
            parameters[1].Value = chucNangID;
            parameters[2].Value = quyen;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, UPDATE_ROLE, parameters);
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

        public int RemoveChucNang(int nhomID, int chucNangID)
        {
            object val = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int)                
            };
            parameters[0].Value = nhomID;
            parameters[1].Value = chucNangID;            
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, REMOVE_ROLE, parameters);
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
