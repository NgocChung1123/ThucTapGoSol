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
    public class ChucNang
    {
        #region Database query string

        private const string GET_ALL = @"ChucNang_GetAll";
        private const string GET_BY_ID = @"ChucNang_GetByID";
        private const string DELETE = @"ChucNang_Delete";
        private const string UPDATE = @"ChucNang_Update";
        private const string INSERT = @"ChucNang_Insert";

        private const string GET_BY_GROUP = @"ChucNang_GetByGroup";
        private const string GET_PARENTS = @"ChucNang_GetParents";
        private const string GET_PARENTS_FOR_ADMINCHILD = @"ChucNang_GetParents_For_AdminChild";
        private const string GET_CHILDS = @"ChucNang_GetChilds";
        private const string GET_CHILDS_FOR_ADMINCHILD = @"ChucNang_GetChilds_For_AdminChild";

        private const string GET_BY_PAGE = @"ChucNang_GetByPage";
        private const string GET_ALL_JOIN = @"ChucNang_GetAllJoin";
        private const string GET_BY_SEARCH = @"ChucNang_GetBySearch";

        private const string COUNT_ALL = @"ChucNang_CountAll";
        private const string COUNT_SEARCH = @"ChucNang_CountSearch";

        private const string GET_BY_TEST = @"A_Test";

        #endregion

        #region paramaters constant

        private const string PARM_CHUCNANGID = @"ChucNangID";
        private const string PARM_TENCHUCNANG = @"TenChucNang";
        private const string PARM_CHUCNANGCHAID = @"ChucNangChaID";

        private const string PARM_START = @"Start";
        private const string PARM_END = "End";
        private const string PARM_KEYWORD = "@Keyword";

        private const string PARM_NHOMNGUOIDUNG_ID = "@NhomNguoiDungID";

        #endregion


        public IList<NguoiDungInfo> GetByTest(String keyword)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 250)
            };
            parm[0].Value = keyword;

            IList<NguoiDungInfo> cnList = new List<NguoiDungInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_TEST, parm))
                {
                    while (dr.Read())
                    {
                        NguoiDungInfo cnInfo = new NguoiDungInfo();
                        cnInfo.NguoiDungID = Utils.GetInt32(dr["NguoiDungID"], 0);
                        cnInfo.TenNguoiDung = Utils.GetString(dr["TenNguoiDung"], string.Empty);
                        cnList.Add(cnInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return cnList;
        }

        private ChucNangInfo GetData(SqlDataReader rdr)
        {
            ChucNangInfo chucNangInfo = new ChucNangInfo();
            chucNangInfo.ChucNangID = Utils.GetInt32(rdr["ChucNangID"], 0);
            chucNangInfo.TenChucNang = Utils.GetString(rdr["TenChucNang"], String.Empty);
            chucNangInfo.ChucNangChaID = Utils.GetInt32(rdr["ChucNangChaID"], 0);
            
            return chucNangInfo;
        }        

        private ChucNangInfo GetJoinData(SqlDataReader rdr)
        {
            ChucNangInfo chucNangInfo = new ChucNangInfo();
            chucNangInfo.ChucNangID = Utils.GetInt32(rdr["ChucNangID"], 0);
            chucNangInfo.TenChucNang = Utils.GetString(rdr["TenChucNang"], String.Empty);
            chucNangInfo.ChucNangChaID = Utils.GetInt32(rdr["ChucNangChaID"], 0);
            chucNangInfo.Quyen = Utils.GetInt32(rdr["Quyen"], 0);
            
            return chucNangInfo;
        }

        private ChucNangInfo GetJoinData2(SqlDataReader rdr)
        {
            ChucNangInfo chucNangInfo = new ChucNangInfo();
            chucNangInfo.ChucNangID = Utils.GetInt32(rdr["ChucNangID"], 0);
            chucNangInfo.TenChucNang = Utils.GetString(rdr["TenChucNang"], String.Empty);
            chucNangInfo.ChucNangChaID = Utils.GetInt32(rdr["ChucNangChaID"], 0);            
            chucNangInfo.TenChucNangCha = Utils.GetString(rdr["TenChucNangCha"], String.Empty);

            return chucNangInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_TENCHUCNANG, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_CHUCNANGCHAID, SqlDbType.Int),
            }; return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, ChucNangInfo chucNangInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in chucNangInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && proInfo.Name != "ChucNangID")
                {
                    parms[index].Value = proInfo.GetValue(chucNangInfo, null);
                    index++;
                }
            }
        }

        private SqlParameter[] GetUpdateParms()
        {
            List<SqlParameter> parms = GetInsertParms().ToList();
            parms.Insert(0, new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int));
            return parms.ToArray();
        }

        private void SetUpdateParms(SqlParameter[] parms, ChucNangInfo chucNangInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in chucNangInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead)
                {
                    parms[index].Value = proInfo.GetValue(chucNangInfo, null);
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
            SqlParameter parm = new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 200);
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

        public IList<ChucNangInfo> GetBySearch(String keyword, int start, int end)
        {
            
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;

            IList<ChucNangInfo> cnList = new List<ChucNangInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parm))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo cnInfo = GetJoinData2(dr);
                        cnList.Add(cnInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return cnList;
        }

        public IList<ChucNangInfo> GetByPage(int start, int end)
        {
            IList<ChucNangInfo> cnList = new List<ChucNangInfo>();
            
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
                        ChucNangInfo cnInfo = GetJoinData2(dr);
                        cnList.Add(cnInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return cnList;
        }

        public IList<ChucNangInfo> GetAll()
        {
            IList<ChucNangInfo> chucnangs = new List<ChucNangInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo chucNangInfo = GetData(dr);
                        chucnangs.Add(chucNangInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucnangs;
        }

        public IList<ChucNangInfo> GetByGroup(int groupID)
        {
            IList<ChucNangInfo> chucnangs = new List<ChucNangInfo>();
            SqlParameter parm = new SqlParameter(PARM_NHOMNGUOIDUNG_ID , SqlDbType.Int);
            parm.Value = groupID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_GROUP, parm))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo chucNangInfo = GetJoinData(dr);
                        chucnangs.Add(chucNangInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucnangs;
        }

        public IList<ChucNangInfo> GetChilds(int parentID)
        {
            IList<ChucNangInfo> chucnangs = new List<ChucNangInfo>();
            SqlParameter parm = new SqlParameter(PARM_CHUCNANGCHAID, SqlDbType.Int);
            parm.Value = parentID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_CHILDS, parm))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo chucNangInfo = GetData(dr);
                        chucnangs.Add(chucNangInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucnangs;
        }

        public IList<ChucNangInfo> GetChildsForAdminChild(int parentID)
        {
            IList<ChucNangInfo> chucnangs = new List<ChucNangInfo>();
            SqlParameter parm = new SqlParameter(PARM_CHUCNANGCHAID, SqlDbType.Int);
            parm.Value = parentID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_CHILDS_FOR_ADMINCHILD, parm))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo chucNangInfo = GetData(dr);
                        chucnangs.Add(chucNangInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucnangs;
        }

        public IList<ChucNangInfo> GetParents()
        {
            IList<ChucNangInfo> chucnangs = new List<ChucNangInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTS, null))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo chucNangInfo = GetData(dr);
                        chucnangs.Add(chucNangInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucnangs;
        }

        public IList<ChucNangInfo> GetParentsForAdminChidl()
        {
            IList<ChucNangInfo> chucnangs = new List<ChucNangInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTS_FOR_ADMINCHILD, null))
                {
                    while (dr.Read())
                    {
                        ChucNangInfo chucNangInfo = GetData(dr);
                        chucnangs.Add(chucNangInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucnangs;
        }

        public ChucNangInfo GetChucNangByID(int chucnangID)
        {
            ChucNangInfo chucNangInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int) };
            parameters[0].Value = chucnangID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        chucNangInfo = GetData(dr);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return chucNangInfo;
        }

        public int Delete(int chucnangID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_CHUCNANGID, SqlDbType.Int) };
            parameters[0].Value = chucnangID;
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

        public int Update(ChucNangInfo chucNangInfo)
        {
            int val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, chucNangInfo);
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


        public int Insert(ChucNangInfo chucNangInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, chucNangInfo);
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
