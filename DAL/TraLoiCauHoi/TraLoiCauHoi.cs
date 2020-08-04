using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.CauHoi;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.TraLoiCauHoi
{
    public class TraLoiCauHoi
    {
        #region Database query string

        private const string GET_ALL = @"TraLoiCauHoi_GetAll";
        private const string GET_BY_ID = @"TraLoiCauHoi_GetByID";
        private const string GET_BY_SEARCH = @"TraLoiCauHoi_GetBySearch";
        private const string GET_BY_SEARCH_LINHVUC_AND_NOIDUNGCAUHOI = @"TraLoiCauHoi_GetBySearchLinhVucAndNDCauHoi";
        private const string COUNT_BY_SEARCH = @"TraLoiCauHoi_CountBySearch";

        private const string DELETE = @"TraLoiCauHoi_Delete";
        private const string UPDATE = @"TraLoiCauHoi_Update";
        private const string INSERT = @"TraLoiCauHoi_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_IDTRALOI = @"IDTraLoi";
        private const string PARM_IDCAUHOI = @"IDCauHoi";
        private const string PARM_NDTRALOI = @"NDTraLoi";
        private const string PARM_CREATERID = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";
        private const string PARM_PULLIC = @"Public";
        private const string PARM_IDLINHVUC = @"IDLinhVuc";

        private const string PARM_START = @"pStart";
        private const string PARM_END = @"pEnd";
        private const string PARM_KEYWORD = @"pKeyWord";

        #endregion


        #region -- insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_IDCAUHOI, SqlDbType.Int),
                new SqlParameter(PARM_NDTRALOI, SqlDbType.NVarChar),
                new SqlParameter(PARM_CREATERID, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_PULLIC, SqlDbType.Bit),
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, TraLoiInfo info)
        {
            parms[0].Value = Utils.ConvertToInt32(info.IDCauHoi,0);
            parms[1].Value = info.NDTraLoi;
            parms[2].Value = Utils.ConvertToInt32(info.CreaterId,0);
            parms[3].Value = info.CreateDate;
            parms[4].Value = Utils.ConvertToBoolean(info.Public,false);
        }

        public int Insert(TraLoiInfo loaiTinInfo)
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

        public int Delete(int idTraLoi)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDTRALOI, SqlDbType.Int), 
            };
            parameters[0].Value = idTraLoi;
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
                new SqlParameter(PARM_IDTRALOI, SqlDbType.Int),
                new SqlParameter(PARM_IDCAUHOI, SqlDbType.Int),
                new SqlParameter(PARM_NDTRALOI, SqlDbType.NVarChar),
                new SqlParameter(PARM_EDITER, SqlDbType.Int),
                new SqlParameter(PARM_EDITDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_PULLIC, SqlDbType.Bit),
                
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, TraLoiInfo info)
        {
            parms[0].Value = info.IDTraLoi;
            parms[1].Value = info.IDCauHoi;
            parms[2].Value = info.NDTraLoi;
            parms[3].Value = info.Editer;
            parms[4].Value = info.EditDate;
            parms[5].Value = info.Public;
        }

        public int Update(TraLoiInfo traLoiInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, traLoiInfo);
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
        public TraLoiInfo GetData(SqlDataReader rdr)
        {
            TraLoiInfo info = new TraLoiInfo();
            info.IDTraLoi = Utils.ConvertToInt32(rdr["IDTraLoi"], 0);
            info.IDCauHoi = Utils.ConvertToInt32(rdr["IDCauHoi"], 0);
            info.NDTraLoi = Utils.ConvertToString(rdr["NDTraLoi"], string.Empty);
            info.CreaterId = Utils.ConvertToInt32(rdr["Creater"], 0);
            //info.CreaterName = Utils.ConvertToString(rdr["TenCanBo"], string.Empty);
            info.nguoi_hoi = Utils.ConvertToString(rdr["nguoi_hoi"], string.Empty);
            info.nguoi_traloi = Utils.ConvertToString(rdr["nguoi_traloi"], string.Empty);
            info.CreateDate = Utils.ConvertToDateTime(rdr["CreateDate"], DateTime.MinValue);
            info.Editer = Utils.ConvertToInt32(rdr["Editer"], 0);
            info.EditDate = Utils.ConvertToDateTime(rdr["EditDate"], DateTime.MinValue);
            info.Public = Utils.ConvertToBoolean(rdr["Public"], false);
            return info;

        }
        #endregion

        #region -- get by id
        public TraLoiInfo GetTraLoiCauHoiByID(int loaiTinID)
        {
            TraLoiInfo diachiInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDTRALOI, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    diachiInfo = GetData(dr);
                    diachiInfo.NDCauHoi = Utils.ConvertToString(dr["NDCauHoi"],string.Empty);
                    diachiInfo.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"],0);
                    
                }
                dr.Close();
            }
            return diachiInfo;
        }
        #endregion

        #region -- get by search

        public List<TraLoiInfo> GetTraLoiCauHoiBySearch(string keySearch,int linhVucID, int start, int end)
        {
            List<TraLoiInfo> loaiTins = new List<TraLoiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int),
                 new SqlParameter(PARM_START, SqlDbType.Int),
                  new SqlParameter(PARM_END, SqlDbType.Int) 
            };
            parameters[0].Value = keySearch;
            parameters[1].Value = linhVucID;
            parameters[2].Value = start;
            parameters[3].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_GetBySearch_Answered", parameters))
                {
                    while (dr.Read())
                    {
                        TraLoiInfo info = new TraLoiInfo();
                        info.IDCauHoi = Utils.ConvertToInt32(dr["IDCauHoi"], 0);
                        info.IDTraLoi = Utils.ConvertToInt32(dr["IDTraLoi"], 0);
                        info.NDCauHoi = Utils.ConvertToString(dr["NDCauHoi"], string.Empty);
                        info.NDTraLoi = Utils.ConvertToString(dr["NDTraLoi"], string.Empty);
                        info.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"],0);
                        info.CreateDate = Utils.ConvertToDateTime(dr["CreateDate"],DateTime.MinValue);
                        info.nguoi_hoi = Utils.ConvertToString(dr["nguoi_hoi"], string.Empty);
                        info.TenLinhVuc = Utils.ConvertToString(dr["linhvuc_name"], string.Empty);
                        info.nguoi_traloi = Utils.ConvertToString(dr["nguoi_traloi"], string.Empty);
                        info.EditDate = Utils.ConvertToDateTime(dr["EditDate"], DateTime.MinValue);
                        info.NgayTraLoi = Utils.ConvertToDateTime(dr["NgayTraLoi"], DateTime.MinValue);
                        info.NgaySuaTraLoi = Utils.ConvertToDateTime(dr["NgaySuaTraLoi"], DateTime.MinValue);
                        info.Email = Utils.ConvertToString(dr["Email"], string.Empty);
                        info.SDT = Utils.ConvertToString(dr["SDT"], string.Empty);
                        //info.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"],string.Empty);
                        loaiTins.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return loaiTins;
        }

        public int CountSearch(string keyword, int linhVucID)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,500),
                new SqlParameter(PARM_IDLINHVUC,SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = linhVucID;
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

        #region -- get by search linh vuc and noi dung

        public List<TraLoiInfo> GetTraLoiCauHoiBySearchLinhVucAndNoiDungCauHoi(string keySearch, int linhVucID)
        {
            List<TraLoiInfo> loaiTins = new List<TraLoiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,500) ,
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int) ,
            };
            parameters[0].Value = keySearch;
            parameters[1].Value = linhVucID;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH_LINHVUC_AND_NOIDUNGCAUHOI, parameters))
                {
                    while (dr.Read())
                    {
                        TraLoiInfo info = new TraLoiInfo();
                        info.IDCauHoi = Utils.ConvertToInt32(dr["IDCauHoi"], 0);
                        info.IDTraLoi = Utils.ConvertToInt32(dr["IDTraLoi"], 0);
                        info.NDCauHoi = Utils.ConvertToString(dr["NDCauHoi"], string.Empty);
                        info.NDTraLoi = Utils.ConvertToString(dr["NDTraLoi"], string.Empty);
                        info.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"], 0);
                        info.CreaterId = Utils.ConvertToInt32(dr["Creater"], 0);
                        info.CreaterName = Utils.ConvertToString(dr["creater_name"], string.Empty);
                        info.EditerName = Utils.ConvertToString(dr["editer_name"], string.Empty);
                        info.CreateDate = Utils.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);

                        info.EditDate = Utils.ConvertToDateTime(dr["EditDate"], DateTime.MinValue);
                        info.NgayTraLoi = Utils.ConvertToDateTime(dr["NgayTraLoi"], DateTime.MinValue);
                        info.NgaySuaTraLoi = Utils.ConvertToDateTime(dr["NgaySuaTraLoi"], DateTime.MinValue);
                        info.TenLinhVuc = Utils.ConvertToString(new DMLinhVuc().GetDMLinhVucByID(info.IDLinhVuc).TenLinhVuc,string.Empty);
                        info.HoTen = Utils.ConvertToString(dr["HoTen"], string.Empty);
                        info.Email = Utils.ConvertToString(dr["Email"], string.Empty);
                        info.SDT = Utils.ConvertToString(dr["SDT"], string.Empty);
                        
                        if (info.CreateDate != DateTime.MinValue)
                            info.NgayHoi_Str = Format.FormatDate(info.CreateDate);
                        else
                            info.NgayHoi_Str = "";
                        if (info.NgayTraLoi != DateTime.MinValue)
                            info.NgayTraLoi_Str = Format.FormatDate(info.NgayTraLoi);
                        else
                            info.NgayTraLoi_Str = "";
                        loaiTins.Add(info);
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

        #region -- get all

        public List<TraLoiInfo> GetAllTraLoiCauHoi()
        {
            List<TraLoiInfo> lichTieps = new List<TraLoiInfo>();

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL, null))
                {
                    while (dr.Read())
                    {
                        TraLoiInfo loaiTinInfo = GetData(dr);

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
