using Com.Gosol.CMS.Model.LichTiepDan;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.LichTiepDan
{
    public class LichTiepDan
    {
        #region Database query string

        private const string GET_ALL = @"LichTiepDan_GetAll";
        private const string GET_BY_ID = @"LichTiepDan_GetByID";
        private const string GET_BY_SEARCH = @"LichTiepDan_GetBySearch";
        private const string COUNT_BY_SEARCH = @"LichTiepDan_CountBySearch";
        private const string GET_BY_COQUAN_THANGNAM = @"LichTiepDan_GetByCoQuanThangNam";

        private const string GET_BY_SEARCH_COQUAN_NGAYTIEP = @"LichTiepDan_GetBySearchCoQuanNgayTiep";
        private const string COUNT_BY_SEARCH_COQUAN_NGAYTIEP = @"LichTiepDan_CountBySearchCoQuanNgayTiep";

        private const string DELETE = @"LichTiepDan_Delete";
        private const string UPDATE = @"LichTiepDan_Update";
        private const string INSERT = @"LichTiepDan_Insert";
        private const string GETTOP2NGAYTIEP = @"LichTiepDan_GetNgayTiep_GanNhat";
        private const string GETBY_NGAYTIEP = "LichTiepDan_GetByNgayTiep";

        #endregion

        #region paramaters constant

        private const string PARM_IDLICHTIEP = @"IDLichTiep";
        private const string PARM_IDCOQUANTIEP = @"IDCoQuanTiep";
        private const string PARM_IDCANBOTIEP = @"IDCanBoTiep";
        private const string PARM_NDTIEP = @"NDTiep";
        private const string PARM_NGAYTIEP = @"NgayTiep";
        private const string PARM_CREATER = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";
        private const string PARM_PULIC = @"Public";

        private const string PARM_START = @"pStart";
        private const string PARM_END = @"pEnd";
        private const string PARM_KEYWORD = @"pKeyWord";

        #endregion


        #region -- insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_IDCOQUANTIEP, SqlDbType.Int),
                new SqlParameter(PARM_IDCANBOTIEP, SqlDbType.Int),
                new SqlParameter(PARM_NDTIEP, SqlDbType.NVarChar),
                new SqlParameter(PARM_NGAYTIEP, SqlDbType.DateTime),
                new SqlParameter(PARM_CREATER, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_PULIC, SqlDbType.Bit),
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, LichTiepDanInfo info)
        {
            parms[0].Value = info.IDCoQuanTiep;
            parms[1].Value = info.IDCanBoTiep;
            parms[2].Value = info.NDTiep;
            parms[3].Value = info.NgayTiep;
            parms[4].Value = info.Creater;
            parms[5].Value = info.CreateDate;
            parms[6].Value = info.Public;
        }

        public int Insert(LichTiepDanInfo loaiTinInfo)
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

        public int Delete(int lichTiepID)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLICHTIEP, SqlDbType.Int), 
            };
            parameters[0].Value = lichTiepID;
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
                new SqlParameter(PARM_IDLICHTIEP, SqlDbType.Int),
                new SqlParameter(PARM_IDCOQUANTIEP, SqlDbType.Int),
                new SqlParameter(PARM_IDCANBOTIEP, SqlDbType.Int),
                new SqlParameter(PARM_NDTIEP, SqlDbType.NVarChar),
                new SqlParameter(PARM_NGAYTIEP, SqlDbType.DateTime),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_PULIC, SqlDbType.Bit),
                
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, LichTiepDanInfo info)
        {
            parms[0].Value = info.IDLichTiep;
            parms[1].Value = info.IDCoQuanTiep;
            parms[2].Value = info.IDCanBoTiep;
            parms[3].Value = info.NDTiep;
            parms[4].Value = info.NgayTiep;
            parms[5].Value = info.Editer;
            parms[6].Value = info.EditDate;
            if (info.CreateDate == DateTime.MinValue)
                parms[6].Value = DBNull.Value;
            parms[7].Value = info.Public;
        }

        public int Update(LichTiepDanInfo loaiTinInfo)
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
        public LichTiepDanInfo GetData(SqlDataReader rdr)
        {
            LichTiepDanInfo info = new LichTiepDanInfo();
            info.IDLichTiep = Utils.ConvertToInt32(rdr["IDLichTiep"], 0);
            info.IDCoQuanTiep = Utils.ConvertToInt32(rdr["IDCoQuanTiep"], 0);
            info.IDCanBoTiep = Utils.ConvertToInt32(rdr["IDCanBoTiep"], 0);
            info.CanBoTiep = Utils.ConvertToString(rdr["TenCanBo"], string.Empty);
            info.CoQuanTiep = Utils.ConvertToString(rdr["TenCoQuan"], string.Empty);
            info.NDTiep = Utils.ConvertToString(rdr["NDTiep"], string.Empty);
            info.NgayTiep = Utils.ConvertToDateTime(rdr["NgayTiep"], DateTime.MinValue);
            info.Creater = Utils.ConvertToInt32(rdr["Creater"], 0);
            info.CreateDate = Utils.ConvertToDateTime(rdr["CreateDate"], DateTime.MinValue);
            info.Editer = Utils.ConvertToInt32(rdr["Editer"], 0);
            info.EditDate = Utils.ConvertToDateTime(rdr["EditDate"], DateTime.MinValue);
            info.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            return info;

        }
        #endregion

        #region -- get by id
        public LichTiepDanInfo GetLichTiepByID(int loaiTinID)
        {
            LichTiepDanInfo diachiInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLICHTIEP, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    diachiInfo = GetData(dr);
                    diachiInfo.CanBoTiep = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                    diachiInfo.CoQuanTiep = Utils.ConvertToString(dr["TenCoQuan"], string.Empty);
                    if (diachiInfo.NgayTiep != DateTime.MinValue)
                        diachiInfo.NgayTiep_Str = Format.FormatDate(diachiInfo.NgayTiep);
                }
                dr.Close();
            }
            return diachiInfo;
        }
        #endregion

        #region -- get by search

        public List<LichTiepDanInfo> GetLichTiepBySearch(string keySearch,int CoQuanID, int start, int end)
        {
            List<LichTiepDanInfo> loaiTins = new List<LichTiepDanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,500) ,
                new SqlParameter(@"CoQuanID", SqlDbType.Int) ,
                 new SqlParameter(PARM_START, SqlDbType.Int) ,
                  new SqlParameter(PARM_END, SqlDbType.Int) 
            };
            parameters[0].Value = keySearch;
            parameters[1].Value = CoQuanID;
            parameters[2].Value = start;
            parameters[3].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parameters))
                {
                    while (dr.Read())
                    {
                        LichTiepDanInfo loaiTinInfo = GetData(dr);
                        loaiTinInfo.CanBoTiep = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                        loaiTinInfo.CoQuanTiep = Utils.ConvertToString(dr["TenCoQuan"], string.Empty);
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

        public int CountSearch(string keyword,int coQuanID)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,500),
                new SqlParameter(@"CoQuanID",SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = coQuanID;
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


        #region -- get by search co quan , ngay tiep

        public List<LichTiepDanInfo> GetLichTiepBySearchCoQuanNgayTiep(int coQuanID, DateTime ngayTiep, int start, int end)
        {
            List<LichTiepDanInfo> lichtiepdan = new List<LichTiepDanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDCOQUANTIEP, SqlDbType.Int),
                new SqlParameter(PARM_NGAYTIEP, SqlDbType.DateTime),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = coQuanID;
            parameters[1].Value = ngayTiep;
            parameters[2].Value = start;
            parameters[3].Value = end;
            if (ngayTiep == DateTime.MinValue)
                parameters[1].Value = DBNull.Value;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH_COQUAN_NGAYTIEP, parameters))
                {
                    while (dr.Read())
                    {
                        LichTiepDanInfo loaiTinInfo = GetData(dr);
                        loaiTinInfo.CanBoTiep = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                        loaiTinInfo.CoQuanTiep = Utils.ConvertToString(dr["TenCoQuan"], string.Empty);
                        lichtiepdan.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return lichtiepdan;
        }

        public List<LichTiepDanInfo> GetLichTiepByCoQuanAndThangNam(int coQuanID, int Thang, int Nam)
        {
            List<LichTiepDanInfo> lichtiepdan = new List<LichTiepDanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("IDCoQuanTiep", SqlDbType.Int),
                new SqlParameter("NamTiep", SqlDbType.Int),
                new SqlParameter("ThangTiep", SqlDbType.Int),
            };
            parameters[0].Value = coQuanID;
            parameters[1].Value = Nam;
            parameters[2].Value = Thang;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "LichTiepDan_GetByCoQuanThangNam", parameters))
                {
                    while (dr.Read())
                    {
                        var info = new LichTiepDanInfo();
                        info.IDCoQuanTiep = Utils.ConvertToInt32(dr["IDCoQuanTiep"], 0);
                        info.IDCanBoTiep = Utils.ConvertToInt32(dr["IDCanBoTiep"], 0);
                        info.CanBoTiep = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                        info.CoQuanTiep = Utils.ConvertToString(dr["TenCoQuan"], string.Empty);
                        info.NDTiep = Utils.ConvertToString(dr["NDTiep"], string.Empty);
                        info.NgayTiep = Utils.ConvertToDateTime(dr["NgayTiep"], DateTime.MinValue);
                        info.NgayTiep_Str = info.NgayTiep.ToString("dd/MM/yyyy");
                        lichtiepdan.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return lichtiepdan;
        }

        public int CountSearchCoQuanNgayTiep(int coQuanID, DateTime ngayTiep)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_IDCOQUANTIEP, SqlDbType.Int) ,
                new SqlParameter(PARM_NGAYTIEP, SqlDbType.DateTime) ,       
            };
            parameters[0].Value = coQuanID;
            parameters[1].Value = ngayTiep;
            if (ngayTiep == DateTime.MinValue)
                parameters[1].Value = DBNull.Value;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_BY_SEARCH_COQUAN_NGAYTIEP, parameters))
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

        public List<LichTiepDanInfo> GetAllLichTiep()
        {
            List<LichTiepDanInfo> lichTieps = new List<LichTiepDanInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        LichTiepDanInfo loaiTinInfo = GetData(dr);

                        lichTieps.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return lichTieps;
        }

        public List<LichTiepDanInfo> GetTop2NgayTiepMoiNhat()
        {
            List<LichTiepDanInfo> lichTieps = new List<LichTiepDanInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETTOP2NGAYTIEP, null))
                {
                    while (dr.Read())
                    {
                        LichTiepDanInfo loaiTinInfo = new LichTiepDanInfo();
                        loaiTinInfo.NgayTiep = Utils.ConvertToDateTime(dr["NgayTiep"], DateTime.MinValue);
                        lichTieps.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {

            }

            return lichTieps;
        }

        public List<LichTiepDanInfo> GetByNgayTiep(DateTime ngayTiep)
        {
            List<LichTiepDanInfo> lichTieps = new List<LichTiepDanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_NGAYTIEP, SqlDbType.DateTime),
            };
            parameters[0].Value = ngayTiep;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETBY_NGAYTIEP, parameters))
                {
                    while (dr.Read())
                    {
                        LichTiepDanInfo loaiTinInfo = GetData(dr);
                        lichTieps.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }

            return lichTieps;
        }


        #endregion
    }
}
