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
    public class DMTinTuc
    {
        #region Database query string

        private const string GET_ALL = @"DM_TinTuc_GetAll";
        private const string GET_BY_ID = @"DM_TinTuc_GetByID";
        private const string GET_BY_SEARCH = @"DM_TinTuc_GetBySearch";
        private const string COUNT_BY_SEARCH = @"DM_TinTuc_CountBySearch";

        private const string DELETE = @"DM_TinTuc_Delete";
        private const string UPDATE = @"DM_TinTuc_Update";
        private const string INSERT = @"DM_TinTuc_Insert";
        private const string GETALL_TINHOT = "DM_TinTuc_GetAll_TinHot";

        /*frontend*/
        private const string GETTOP3_TINNOIBAT_TINHOT = "DM_TinTuc_GetTop3_TinNoiBat_TinHot";
        private const string GETBY_LOAITINID = "DM_TinTuc_GetBy_LoaiTinID";
        private const string DM_TinTucPublic__GetBy_LoaiTinID = "DM_TinTucPublic__GetBy_LoaiTinID";
        private const string DM_TinTuc_GetAll_Public = "DM_TinTuc_GetAll_Public";
        private const string DM_TinTuc_CTLoaiTin_GetBy_LoaiTinID = "DM_TinTuc_CTLoaiTin_GetBy_LoaiTinID";
        private const string DM_TinTuc_CTLoaiTin_GetBy_LoaiTinID_Count = "DM_TinTuc_CTLoaiTin_GetBy_LoaiTinID_Count";

        #endregion

        #region paramaters constant

        private const string PARM_IDTINTUC = @"IDTinTuc";
        private const string PARM_IDLOAITIN = @"IDLoaiTin";
        private const string PARM_TIEUDE = @"TieuDe";
        private const string PARM_TOMTAT = @"TomTat";
        private const string PARM_NOIDUNG = @"NoiDung";
        private const string PARM_CREATER = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";
        private const string PARM_LATINHOT = @"laTinHot";
        private const string PARM_PUBLIC = @"Public";
        private const string PARM_IMAGEURL = @"ImageUrl";

        private const string PARM_START = @"pStart";
        private const string PARM_END = @"pEnd";
        private const string PARM_KEYWORD = @"pKeyWord";

        #endregion


        #region -- insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
                new SqlParameter(PARM_TIEUDE, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_TOMTAT, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_NOIDUNG, SqlDbType.NVarChar),
                new SqlParameter(PARM_CREATER, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_LATINHOT, SqlDbType.Bit),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),
                new SqlParameter(PARM_IMAGEURL, SqlDbType.NVarChar,500),
                
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, DMTinTucInfo tinTucInfo)
        {
            parms[0].Value = tinTucInfo.IDLoaiTin;
            parms[1].Value = tinTucInfo.TieuDe;
            parms[2].Value = tinTucInfo.TomTat;
            parms[3].Value = tinTucInfo.NoiDung;
            parms[4].Value = tinTucInfo.Creater;
            parms[5].Value = tinTucInfo.CreateDate;
            parms[6].Value = tinTucInfo.laTinHot;
            parms[7].Value = tinTucInfo.Public;
            parms[8].Value = tinTucInfo.ImageUrl;

        }

        public int Insert(DMTinTucInfo tinTucInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, tinTucInfo);
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

        public int Delete(int tinTucID)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDTINTUC, SqlDbType.Int), 
            };
            parameters[0].Value = tinTucID;
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
                new SqlParameter(PARM_IDTINTUC, SqlDbType.Int),
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
                new SqlParameter(PARM_TIEUDE, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_TOMTAT, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_NOIDUNG, SqlDbType.NVarChar),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_LATINHOT, SqlDbType.Bit),
                new SqlParameter(PARM_PUBLIC, SqlDbType.Bit),
                new SqlParameter(PARM_IMAGEURL, SqlDbType.NVarChar,500),
                
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, DMTinTucInfo tinTucInfo)
        {
            parms[0].Value = tinTucInfo.IDTinTuc;
            parms[1].Value = tinTucInfo.IDLoaiTin;
            parms[2].Value = tinTucInfo.TieuDe;
            parms[3].Value = tinTucInfo.TomTat;
            parms[4].Value = tinTucInfo.NoiDung;
            parms[5].Value = tinTucInfo.Editer;
            parms[6].Value = tinTucInfo.EditDate;
            parms[7].Value = tinTucInfo.laTinHot;
            parms[8].Value = tinTucInfo.Public;
            parms[9].Value = tinTucInfo.ImageUrl;
        }

        public int Update(DMTinTucInfo loaiTinInfo)
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
        public DMTinTucInfo GetData(SqlDataReader rdr)
        {
            DMTinTucInfo info = new DMTinTucInfo();
            info.IDTinTuc = Utils.ConvertToInt32(rdr["IDTinTuc"], 0);
            info.IDLoaiTin = Utils.ConvertToInt32(rdr["IDLoaiTin"], 0);
            info.TieuDe = Utils.ConvertToString(rdr["TieuDe"], string.Empty);
            info.TomTat = Utils.ConvertToString(rdr["TomTat"], string.Empty);
            info.NoiDung = Utils.ConvertToString(rdr["NoiDung"], string.Empty);
            info.Creater = Utils.ConvertToInt32(rdr["Creater"], 0);
            info.CreateDate = Utils.ConvertToDateTime(rdr["CreateDate"], DateTime.MinValue);
            info.Editer = Utils.ConvertToInt32(rdr["Editer"], 0);
            info.EditDate = Utils.ConvertToDateTime(rdr["EditDate"], DateTime.MinValue);
            info.laTinHot = Utils.ConvertToBoolean(rdr["laTinHot"], false);
            info.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            info.ImageUrl = Utils.ConvertToString(rdr["ImageUrl"], string.Empty);
            return info;

        }
        #endregion

        #region -- get by id
        public DMTinTucInfo GetTinTucByID(int tinTucID)
        {
            DMTinTucInfo diachiInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDTINTUC, SqlDbType.Int),
            };
            parameters[0].Value = tinTucID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    diachiInfo = GetData(dr);
                    diachiInfo.NguoiTao = Utils.ConvertToString(dr["NguoiTao"], string.Empty);
                    diachiInfo.NguoiSua = Utils.ConvertToString(dr["NguoiSua"], string.Empty);
                    diachiInfo.TenLoaiTin = Utils.ConvertToString(dr["TenLoaiTin"],string.Empty);
                }
                dr.Close();
            }
            return diachiInfo;
        }
        #endregion

        #region -- get by search

        public List<DMTinTucInfo> GetTinTucBySearch(string keySearch,int loaiTinID, int start, int end)
        {
            List<DMTinTucInfo> loaiTins = new List<DMTinTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,100) ,
                  new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int) ,
                 new SqlParameter(PARM_START, SqlDbType.Int) ,
                  new SqlParameter(PARM_END, SqlDbType.Int) 
            };
            parameters[0].Value = keySearch;
            parameters[1].Value = loaiTinID;
            parameters[2].Value = start;
            parameters[3].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parameters))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        loaiTinInfo.NguoiTao = Utils.ConvertToString(dr["NguoiTao"], string.Empty);
                        loaiTinInfo.NguoiSua = Utils.ConvertToString(dr["NguoiSua"], string.Empty);
                        loaiTinInfo.TenLoaiTin = Utils.ConvertToString(dr["TenLoaiTin"], string.Empty);
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

        public int CountSearch(string keyword, int loaiTinID)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,100),
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int) ,
            };
            parameters[0].Value = keyword;
            parameters[1].Value = loaiTinID;
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

        public List<DMTinTucInfo> GetAllTinTuc()
        {
            List<DMTinTucInfo> loaiTins = new List<DMTinTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        loaiTinInfo.TenLoaiTin = Utils.ConvertToString(dr["TenLoaiTin"], string.Empty);
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

        #region get data frontend
     
        public List<DMTinTucInfo> GetAllTinTuc_Public()
        {
            List<DMTinTucInfo> loaiTins = new List<DMTinTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, DM_TinTuc_GetAll_Public, null))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        loaiTinInfo.TenLoaiTin = Utils.ConvertToString(dr["TenLoaiTin"], string.Empty);
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
        public List<DMTinTucInfo> GetTop3_TinNoiBat_TinHot()
        {
            List<DMTinTucInfo> lsTinHot = new List<DMTinTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETTOP3_TINNOIBAT_TINHOT, null))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        lsTinHot.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return lsTinHot;
        }

        public List<DMTinTucInfo> Get_ALL_Tin_Hot()
        {
            List<DMTinTucInfo> lsTinHot = new List<DMTinTucInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETALL_TINHOT, null))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        lsTinHot.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return lsTinHot;
        }

        public List<DMTinTucInfo> GetByLoaiTinID(int loaiTinID)
        {
            List<DMTinTucInfo> lsTinHot = new List<DMTinTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETBY_LOAITINID, parameters))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        lsTinHot.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return lsTinHot;
        }

        public List<DMTinTucInfo> TinPublic_GetByLoaiTinID(int loaiTinID)
        {
            List<DMTinTucInfo> lsTinHot = new List<DMTinTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, DM_TinTucPublic__GetBy_LoaiTinID, parameters))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        lsTinHot.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return lsTinHot;
        }

        public List<DMTinTucInfo>ChiTietLoaiTin_GetByLoaiTinID(int loaiTinID,int start, int end)
        {

            List<DMTinTucInfo> lsTinHot = new List<DMTinTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
                  new SqlParameter(PARM_START, SqlDbType.Int) ,
                  new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = loaiTinID;
            parameters[1].Value = start;
            parameters[2].Value = end;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, DM_TinTuc_CTLoaiTin_GetBy_LoaiTinID, parameters))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo loaiTinInfo = GetData(dr);
                        lsTinHot.Add(loaiTinInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return lsTinHot;
        }

        public int GetByLoaiTin_Count(int loaiTinID)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                 new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, DM_TinTuc_CTLoaiTin_GetBy_LoaiTinID_Count, parameters))
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


        #region
        private const string GET_TINMOI_BY_LOAITIN = "DM_TinTuc_GetTinMoiByLoaiTin";

        public List<DMTinTucInfo> GeTinMoi_ByLoaiTin(int loaiTinID)
        {
            List<DMTinTucInfo> list = new List<DMTinTucInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLOAITIN, SqlDbType.Int)
            };
            parameters[0].Value = loaiTinID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_TINMOI_BY_LOAITIN, parameters))
                {
                    while (dr.Read())
                    {
                        DMTinTucInfo info = GetData(dr);
                        list.Add(info);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return list;
        }
        #endregion
    }
}
