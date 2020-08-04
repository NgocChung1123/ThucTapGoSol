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
    public class NguoiDung
    {
        #region Database query string

        private const string GET_ALL = @"NguoiDung_GetAll";
        private const string GET_BY_ID = @"NguoiDung_GetByID";
        private const string DELETE = @"NguoiDung_Delete";
        private const string UPDATE = @"NguoiDung_Update";
        private const string INSERT = @"NguoiDung_Insert";

        private const string GET_BY_PAGE = @"NguoiDung_GetByPage";
        private const string GET_ALL_JOIN = @"NguoiDung_GetAllJoin";
        private const string GET_BY_SEARCH = @"NguoiDung_GetBySearch";
        private const string GET_BY_COQUANCHA_ID = @"NguoiDung_GetByCoQuanChaID";

        private const string COUNT_ALL = @"NguoiDung_CountAll";
        private const string COUNT_SEARCH = @"NguoiDung_CountSearch";
        private const string COUNT_BY_COQUANCHA_ID = @"NguoiDung_CountByCoQuanCha_ID";

        private const string GET_BY_GROUP = @"NguoiDung_GetByGroup";
        private const string ADD_GROUP = "NguoiDung_AddGroup";
        private const string REMOVE_GROUP = "NguoiDung_RemoveGroup";

        private const string CHECK_EXISTS = "NguoiDung_CheckExists";

        private const string GET_NGUOIDUNG_CHUA_THUOC_NHOM = @"NguoiDung_GetNguoiDungChuathuocNhom";
        private const string GET_NGUOIDUNG_CHUA_THUOC_NHOM_BYCOQUANID = @"NguoiDung_GetNguoiDungChuaThuocNhomBy_CQID";

        #endregion

        #region paramaters constant

        private const string PARM_NGUOIDUNGID = @"NguoiDungID";
        private const string PARM_TENNGUOIDUNG = @"TenNguoiDung";
        private const string PARM_MATKHAU = @"MatKhau";
        private const string PARM_GHICHU = @"GhiChu";
        private const string PARM_TRANGTHAI = @"TrangThai";
        private const string PARM_CANBOID = @"CanBoID";
        private const string PARM_COQUANID = @"CoQuanID";
        private const string PARM_COQUANCHAID = @"CoQuanChaID";

        private const string PARM_START = @"Start";
        private const string PARM_END = @"End";
        private const string PARM_KEYWORD = @"Keyword";

        private const string PARM_NHOMNGUOIDUNGID = "NhomNguoiDungID";

        #endregion        

        private NguoiDungJoinInfo GetJoinData(SqlDataReader rdr)
        {
            NguoiDungJoinInfo nguoiDungInfo = new NguoiDungJoinInfo();
            nguoiDungInfo.NguoiDungID = Utils.GetInt32(rdr["NguoiDungID"], 0);
            nguoiDungInfo.TenNguoiDung = Utils.GetString(rdr["TenNguoiDung"], String.Empty);
            nguoiDungInfo.MatKhau = Utils.GetString(rdr["MatKhau"], String.Empty);
            nguoiDungInfo.GhiChu = Utils.GetString(rdr["GhiChu"], String.Empty);
            nguoiDungInfo.TrangThai = Utils.GetInt32(rdr["TrangThai"], 0);
            nguoiDungInfo.CanBoID = Utils.GetInt32(rdr["CanBoID"], 0);
            nguoiDungInfo.TenCanBo = Utils.GetString(rdr["TenCanBo"], String.Empty);
            nguoiDungInfo.TenCoQuan = Utils.GetString(rdr["TenCoQuan"], String.Empty);
            return nguoiDungInfo;
        }

        private NguoiDungInfo GetData(SqlDataReader rdr)
        {
            NguoiDungInfo nguoiDungInfo = new NguoiDungInfo();
            nguoiDungInfo.NguoiDungID = Utils.GetInt32(rdr["NguoiDungID"], 0);
            nguoiDungInfo.TenNguoiDung = Utils.GetString(rdr["TenNguoiDung"], String.Empty);
            nguoiDungInfo.MatKhau = Utils.GetString(rdr["MatKhau"], String.Empty);
            nguoiDungInfo.GhiChu = Utils.GetString(rdr["GhiChu"], String.Empty);
            nguoiDungInfo.TrangThai = Utils.GetInt32(rdr["TrangThai"], 0);
            nguoiDungInfo.CanBoID = Utils.GetInt32(rdr["CanBoID"], 0);            
            return nguoiDungInfo;
        }


        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_TENNGUOIDUNG, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_MATKHAU, SqlDbType.NVarChar, 100),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_TRANGTHAI, SqlDbType.Int),
                new SqlParameter(PARM_CANBOID, SqlDbType.Int)
            }; 
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, NguoiDungInfo NguoiDungInfo)
        {

            parms[0].Value = NguoiDungInfo.TenNguoiDung;
            parms[1].Value = NguoiDungInfo.MatKhau;
            parms[2].Value = NguoiDungInfo.GhiChu;
            parms[3].Value = NguoiDungInfo.TrangThai;
            parms[4].Value = NguoiDungInfo.CanBoID;
            //int index = 0;
            //foreach (PropertyInfo proInfo in NguoiDungInfo.GetType().GetProperties())
            //{
            //    if (proInfo.CanRead && proInfo.Name != "NguoiDungID")
            //    {
            //        parms[index].Value = proInfo.GetValue(NguoiDungInfo, null);                    
            //        index++;
            //    }
            //}
            ////check don vi id = null
            //if ((int)parms[4].Value == 0) parms[4].Value = DBNull.Value;
        }

        private SqlParameter[] GetUpdateParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_TENNGUOIDUNG, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_MATKHAU, SqlDbType.NVarChar, 100),
                new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_TRANGTHAI, SqlDbType.Int),
                new SqlParameter(PARM_CANBOID, SqlDbType.Int)
            };
            return parms;
            //List<SqlParameter> parms = GetInsertParms().ToList();
            //parms.Insert(0, new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int));
            //return parms.ToArray();
        }

        private void SetUpdateParms(SqlParameter[] parms, NguoiDungInfo NguoiDungInfo)
        {
            parms[0].Value = NguoiDungInfo.NguoiDungID;
            parms[1].Value = NguoiDungInfo.TenNguoiDung;
            parms[2].Value = NguoiDungInfo.MatKhau;
            parms[3].Value = NguoiDungInfo.GhiChu;
            parms[4].Value = NguoiDungInfo.TrangThai;
            parms[5].Value = NguoiDungInfo.CanBoID;
            //int index = 0;
            //foreach (PropertyInfo proInfo in NguoiDungInfo.GetType().GetProperties())
            //{
            //    if (proInfo.CanRead)
            //    {
            //        parms[index].Value = proInfo.GetValue(NguoiDungInfo, null);
            //        index++;
            //    }
            //}
            ////check don vi id = null
            //if ((int)parms[5].Value == 0) parms[5].Value = DBNull.Value;
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

        public int CountSearch(int coQuanID, String keyword, int loaiNguoiDung)
        {
            int result = 0;

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter("@LoaiNguoiDung", SqlDbType.Int),
            };
            parm[0].Value = coQuanID;
            parm[1].Value = keyword;
            parm[2].Value = loaiNguoiDung;
            if (coQuanID == 0)
            {
                parm[0].Value = DBNull.Value;
            }

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

        public int CountByCoQuanChaID(int coQuanID, String keyword, int coQuanChaID, int loaiNguoiDung)
        {
            int result = 0;

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),    
                new SqlParameter(PARM_COQUANCHAID, SqlDbType.Int),
                new SqlParameter("@LoaiNguoiDung", SqlDbType.Int)
            };
            parm[0].Value = coQuanID;
            parm[1].Value = keyword;
            parm[2].Value = coQuanChaID;
            parm[3].Value = loaiNguoiDung;
            if (coQuanID == 0)
            {
                parm[0].Value = DBNull.Value;
            }

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_BY_COQUANCHA_ID, parm))
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
        
        public IList<NguoiDungJoinInfo> GetBySearch(int coQuanID,String keyword, int start, int end, int loaiNguoiDung)
        {
            
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int) ,
                new SqlParameter("@LoaiNguoiDung", SqlDbType.Int)
            };
            parm[0].Value = coQuanID;
            parm[1].Value = keyword;
            parm[2].Value = start;
            parm[3].Value = end;
            parm[4].Value = loaiNguoiDung;

            if (coQuanID == 0)
            {
                parm[0].Value = DBNull.Value;
            }

            IList<NguoiDungJoinInfo> nguoiDungs = new List<NguoiDungJoinInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH , parm))
                {
                    while (dr.Read())
                    {
                        NguoiDungJoinInfo nguoiDungInfo = GetJoinData(dr);
                        nguoiDungs.Add(nguoiDungInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nguoiDungs;
        }

        public IList<NguoiDungJoinInfo> GetByCoQuanChaID(int coQuanID, String keyword, int start, int end, int coQuanChaID, int loaiNguoiDung)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int),
                new SqlParameter(PARM_COQUANCHAID, SqlDbType.Int),
                new SqlParameter("@LoaiNguoiDung", SqlDbType.Int)
            };
            parm[0].Value = coQuanID;
            parm[1].Value = keyword;
            parm[2].Value = start;
            parm[3].Value = end;
            parm[4].Value = coQuanChaID;
            parm[5].Value = loaiNguoiDung;

            if (coQuanID == 0)
            {
                parm[0].Value = DBNull.Value;
            }

            IList<NguoiDungJoinInfo> nguoiDungs = new List<NguoiDungJoinInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_COQUANCHA_ID, parm))
                {
                    while (dr.Read())
                    {
                        NguoiDungJoinInfo nguoiDungInfo = GetJoinData(dr);
                        nguoiDungs.Add(nguoiDungInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nguoiDungs;
        }

        public IList<NguoiDungInfo> GetAll()
        {
            IList<NguoiDungInfo> ndList = new List<NguoiDungInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        NguoiDungInfo ndInfo = GetData(dr);
                        ndList.Add(ndInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return ndList;
        }

        public IList<NguoiDungInfo> GetNguoidungchuaThuocNhom(int nhomID)
        {
            IList<NguoiDungInfo> nguoiDungJoinList = new List<NguoiDungInfo>();

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int)
            };
            parm[0].Value = nhomID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_NGUOIDUNG_CHUA_THUOC_NHOM, parm))
                {
                    while (dr.Read())
                    {
                        NguoiDungInfo ndInfo = GetData(dr);
                        ndInfo.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                        nguoiDungJoinList.Add(ndInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nguoiDungJoinList;
        }

        public IList<NguoiDungInfo> GetUserchuaThuocNhomByCoQuan(int nhomID, int coQuanID)
        {
            IList<NguoiDungInfo> nguoiDungJoinList = new List<NguoiDungInfo>();

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parm[0].Value = nhomID;
            parm[1].Value = coQuanID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_NGUOIDUNG_CHUA_THUOC_NHOM_BYCOQUANID, parm))
                {
                    while (dr.Read())
                    {
                        NguoiDungInfo ndInfo = GetData(dr);
                        ndInfo.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                        nguoiDungJoinList.Add(ndInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nguoiDungJoinList;
        }

        public IList<NguoiDungJoinInfo> GetNguoiDungsJoin()
        {
            IList<NguoiDungJoinInfo> nguoiDungJoinList = new List<NguoiDungJoinInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL_JOIN, null))
                {
                    while (dr.Read())
                    {
                        NguoiDungJoinInfo ndInfo = GetJoinData(dr);
                        nguoiDungJoinList.Add(ndInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nguoiDungJoinList;
        }

        public IList<NguoiDungJoinInfo> GetByPage(int start, int end)
        {
            IList<NguoiDungJoinInfo> nguoiDungJoinList = new List<NguoiDungJoinInfo>();
            
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
                        NguoiDungJoinInfo ndInfo = GetJoinData(dr);
                        nguoiDungJoinList.Add(ndInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return nguoiDungJoinList;
        }

        public IList<NguoiDungInfo> GetByGroup(int groupID)
        {
            IList<NguoiDungInfo> ndList = new List<NguoiDungInfo>();            
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int)                
            };
            parm[0].Value = groupID;            
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_GROUP, parm))
                {
                    while (dr.Read())
                    {
                        NguoiDungInfo ndInfo = GetData(dr);
                        ndList.Add(ndInfo);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return ndList;
        }


        public NguoiDungInfo GetNguoiDungByID(int nguoiDungID)
        {
            NguoiDungInfo ndInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int) };
            parameters[0].Value = nguoiDungID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
                {
                    if (dr.Read())
                    {
                        ndInfo = GetData(dr);
                        ndInfo.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                    }
                    dr.Close();
                }
            }
            catch
            {
                throw;
            }
            return ndInfo;
        }
        public NguoiDungInfo GetNguoiDungByUserName(string userName)
        {
            NguoiDungInfo ndInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@userName", SqlDbType.NVarChar) };
            parameters[0].Value = userName;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "NguoiDung_GetByName", parameters))
                {
                    if (dr.Read())
                    {
                        ndInfo = GetData(dr);
                        ndInfo.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        ndInfo.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], "");
                    }
                    dr.Close();
                }
            }
            catch
            {
                throw;
            }
            return ndInfo;
        }

        public int Delete(int nguoiDungID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int) };
            parameters[0].Value = nguoiDungID;
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

        public int Update(NguoiDungInfo nguoiDungInfo)
        {
            int val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, nguoiDungInfo);
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


        public int Insert(NguoiDungInfo nguoiDungInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, nguoiDungInfo);
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

        public int AddGroup(int nguoiDungID, int nhomNguoiDungID)
        {
            //object val = null;
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int)
            };
            parameters[0].Value = nguoiDungID;
            parameters[1].Value = nhomNguoiDungID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, ADD_GROUP, parameters);
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
            //return Convert.ToInt32(val);
            return val;
        }

        public int RemoveGroup(int nguoiDungID, int nhomNguoiDungID)
        {
            object val = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NGUOIDUNGID, SqlDbType.Int),
                new SqlParameter(PARM_NHOMNGUOIDUNGID, SqlDbType.Int)
            };
            parameters[0].Value = nguoiDungID;
            parameters[1].Value = nhomNguoiDungID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, REMOVE_GROUP, parameters);
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

        public int CheckExists(String tenNguoiDung)
        {
            object val = null;
            SqlParameter parm = new SqlParameter(PARM_TENNGUOIDUNG, SqlDbType.NVarChar, 50);
            parm.Value = tenNguoiDung;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, CHECK_EXISTS, parm);
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

        public int InsertNguoiDan(NguoiDungInfo nguoiDungInfo)
        {
            object val = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENNGUOIDUNG, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_MATKHAU, SqlDbType.NVarChar, 100),
                new SqlParameter(PARM_CANBOID, SqlDbType.Int)
            };
            parameters[0].Value = nguoiDungInfo.TenNguoiDung;
            parameters[1].Value = nguoiDungInfo.MatKhau;
            parameters[2].Value = nguoiDungInfo.CanBoID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "NguoiDung_InsertNguoiDan", parameters);
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
