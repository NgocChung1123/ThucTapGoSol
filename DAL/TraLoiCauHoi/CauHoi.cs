using Com.Gosol.CMS.Model.CauHoi;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.TraLoiCauHoi
{
    public class CauHoi
    {
        private const string GET_BY_ID = @"CauHoi_GetByID";
        private const string INSERT = "CauHoi_Insert";
        private const string GETTOP2NGAYTAOGANNHAT = "CauHoi_GetNgayTao_GanNhat";

        private const string PARM_IDCAUHOI = @"IDCauHoi";
        private const string PARM_IDLINHVUC = @"IDLinhVuc";

        private const string PARM_LINHVUCID = @"LinhVucID";

        private const string PARM_NDCAUHOI = @"NDCauHoi";
        private const string PARM_ISCAUHOIHOPLE = @"IsCauHoiHopLe";
        private const string PARM_GHICHU = @"GhiChu";
        private const string PARM_CREATERID = @"Creater";
        private const string PARM_CREATEDATE = @"CreateDate";
        private const string PARM_EDITER = @"Editer";
        private const string PARM_EDITDATE = @"EditDate";
        private const string PARM_HOTEN = @"HoTen";
        private const string PARM_EMAIL = @"Email";
        private const string PARM_SDT = @"SDT";

        private const string PARM_KEYWORD = @"Keyword";
        private const string PARM_START = @"Start";
        private const string PARM_END = @"End";

        private const string GETBY_CREATEDATE = "CauHoi_GetByNgayTao";
        private const string COUNT_SEARCH = "CauHoi_CountSearch";

        #region -- get data
        public CauHoiInfo GetData(SqlDataReader rdr)
        {
            CauHoiInfo info = new CauHoiInfo();
            info.IDCauHoi = Utils.ConvertToInt32(rdr["IDCauHoi"], 0);
            info.NDCauHoi = Utils.ConvertToString(rdr["NDCauHoi"], string.Empty);
            info.CreaterId = Utils.ConvertToInt32(rdr["Creater"], 0);
            info.CreateDate = Utils.ConvertToDateTime(rdr["CreateDate"], DateTime.MinValue);
            info.Editer = Utils.ConvertToInt32(rdr["Editer"], 0);
            info.EditDate = Utils.ConvertToDateTime(rdr["EditDate"], DateTime.MinValue);
            info.IsCauHoiHopLe = Utils.ConvertToBoolean(rdr["IsCauHoiHopLe"], false);
            return info;

        }
        #endregion

        #region -- insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int),
                new SqlParameter(PARM_NDCAUHOI, SqlDbType.NVarChar),
                new SqlParameter(PARM_ISCAUHOIHOPLE, SqlDbType.Bit),
                 new SqlParameter(PARM_GHICHU, SqlDbType.NVarChar),
                new SqlParameter(PARM_CREATERID, SqlDbType.Int),
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
                new SqlParameter(PARM_HOTEN, SqlDbType.NVarChar),
                new SqlParameter(PARM_EMAIL, SqlDbType.NVarChar),
                new SqlParameter(PARM_SDT, SqlDbType.NVarChar),
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, CauHoiInfo info)
        {
            parms[0].Value = info.IDLinhVuc;
            parms[1].Value = info.NDCauHoi;
            parms[2].Value = info.IsCauHoiHopLe;
            parms[3].Value = info.GhiChu;
            parms[4].Value = info.CreaterId;
            parms[5].Value = info.CreateDate;
            parms[6].Value = info.HoTen;
            parms[7].Value = info.Email;
            parms[8].Value = info.SDT;
        }

        public int Insert(CauHoiInfo cauHoiInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, cauHoiInfo);
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
                    catch(Exception ex)
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

        #region -- get by id
        public CauHoiInfo GetCauHoiByID(int loaiTinID)
        {
            CauHoiInfo diachiInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDCAUHOI, SqlDbType.Int),
            };
            parameters[0].Value = loaiTinID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    diachiInfo = GetData(dr);
                    diachiInfo.HoTen = Utils.ConvertToString(dr["HoTen"], string.Empty);
                    diachiInfo.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"],0);
                    diachiInfo.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"], string.Empty);
                    diachiInfo.NguoiTraLoi=Utils.ConvertToString(dr["NguoiTraLoi"],string.Empty);
                    diachiInfo.NDTraLoi= Utils.ConvertToString(dr["NDTraLoi"], string.Empty);
                    diachiInfo.CreaterName = Utils.ConvertToString(dr["NguoiHoi"], string.Empty);
                    diachiInfo.Public = Utils.ConvertToBoolean(dr["Public"], false);
                }
                dr.Close();
            }
            return diachiInfo;
        }

        public CauHoiInfo GetCauHoiByID_BackEnd(int id)
        {
            CauHoiInfo info = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDCAUHOI, SqlDbType.Int),
            };
            parameters[0].Value = id;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_GetByID_BackEnd", parameters))
            {
                if (dr.Read())
                {
                    info = GetData(dr);
                    info.HoTen = Utils.ConvertToString(dr["HoTen"], string.Empty);
                    info.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"], 0);
                    info.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"], string.Empty);
                    info.NguoiTraLoi = Utils.ConvertToString(dr["NguoiTraLoi"], string.Empty);
                    info.NDTraLoi = Utils.ConvertToString(dr["NDTraLoi"], string.Empty);
                    info.CreaterName = Utils.ConvertToString(dr["NguoiHoi"], string.Empty);
                }
                dr.Close();
            }
            return info;
        }

        public List<CauHoiInfo> GetTop2NgayTaoGanNhat()
        {
          List<CauHoiInfo>   lsData = new List<CauHoiInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETTOP2NGAYTAOGANNHAT, null))
            {
                while (dr.Read())
                {
                    CauHoiInfo cauHoiInfo = new CauHoiInfo();
                    cauHoiInfo.CreateDate = Utils.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
                    lsData.Add(cauHoiInfo);

                }
                dr.Close();
            }
            return lsData;
        }
        public List<CauHoiInfo> GetByNgayTao(DateTime ngayTao)
        {
            List<CauHoiInfo> lsData = new List<CauHoiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_CREATEDATE, SqlDbType.DateTime),
            };
            parameters[0].Value = ngayTao;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETBY_CREATEDATE, parameters))
                {
                    while (dr.Read())
                    {
                        CauHoiInfo cauHoiInfo = GetData(dr);
                        lsData.Add(cauHoiInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }

            return lsData;
        }
        #endregion


        #region Tìm kiếm câu hỏi
        public int CountSearch(int linhVucID, string keyword)
        {
            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_LINHVUCID,SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar)
            };
            parameters[0].Value = linhVucID;
            parameters[1].Value = keyword;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_CountSearch", parameters))
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

        public int CountSearchNotAnswer(string keyword, int linhVucID)
        {
            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar),
                new SqlParameter(PARM_LINHVUCID,SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = linhVucID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_CountSearch_NotAnswer", parameters))
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

        public List<CauHoiInfo> GetBySearch(int linhVucID, string keyword,int start, int end)
        {
            List<CauHoiInfo> list = new List<CauHoiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_LINHVUCID, SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = linhVucID;
            parameters[1].Value = keyword;
            parameters[2].Value = start;
            parameters[3].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_GetBySearch", parameters))
                {
                    while (dr.Read())
                    {
                        CauHoiInfo info = new CauHoiInfo();
                        info.IDCauHoi = Utils.ConvertToInt32(dr["IDCauHoi"], 0);
                        info.IDTraLoi = Utils.ConvertToInt32(dr["IDTraLoi"], 0);
                        info.NDCauHoi = Utils.ConvertToString(dr["NDCauHoi"], string.Empty);
                        info.NDTraLoi = Utils.ConvertToString(dr["NDTraLoi"], string.Empty);
                        info.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"], 0);
                        info.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"], string.Empty);
                        info.CreaterId = Utils.ConvertToInt32(dr["Creater"], 0);
                        info.CreateDate = Utils.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
                        info.Editer = Utils.ConvertToInt32(dr["Editer"], 0);
                        info.EditDate = Utils.ConvertToDateTime(dr["EditDate"], DateTime.MinValue);
                        //info.NgayTraLoi = Utils.ConvertToDateTime(dr["NgayTraLoi"], DateTime.MinValue);
                        //info.NgaySuaTraLoi = Utils.ConvertToDateTime(dr["NgaySuaTraLoi"], DateTime.MinValue);
                        info.HoTen = Utils.ConvertToString(dr["HoTen"], string.Empty);
                        info.Email = Utils.ConvertToString(dr["Email"], string.Empty);
                        info.SDT = Utils.ConvertToString(dr["SDT"], string.Empty);

                        //if (info.CreateDate != DateTime.MinValue)
                        //    info.NgayHoi_Str = Format.FormatDate(info.CreateDate);
                        //else
                        //    info.NgayHoi_Str = "";
                        //if (info.NgayTraLoi != DateTime.MinValue)
                        //    info.NgayTraLoi_Str = Format.FormatDate(info.NgayTraLoi);
                        //else
                        //    info.NgayTraLoi_Str = "";
                        list.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }

        public List<CauHoiInfo> GetBySearchNotAnswer(string keyword, int linhVucID, int start, int end)
        {
            List<CauHoiInfo> list = new List<CauHoiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,500),
                new SqlParameter(PARM_LINHVUCID, SqlDbType.Int),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = linhVucID;
            parameters[2].Value = start;
            parameters[3].Value = end;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_GetBySearch_NotAnswer", parameters))
                {
                    while (dr.Read())
                    {
                        CauHoiInfo info = new CauHoiInfo();
                        info.IDCauHoi = Utils.ConvertToInt32(dr["IDCauHoi"], 0);
                        info.IDTraLoi = Utils.ConvertToInt32(dr["IDTraLoi"], 0);
                        info.NDCauHoi = Utils.ConvertToString(dr["NDCauHoi"], string.Empty);
                        info.NDTraLoi = Utils.ConvertToString(dr["NDTraLoi"], string.Empty);
                        info.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"], 0);
                        info.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"], string.Empty);
                        info.CreaterId = Utils.ConvertToInt32(dr["Creater"], 0);
                        info.CreateDate = Utils.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
                        info.Editer = Utils.ConvertToInt32(dr["Editer"], 0);
                        info.EditDate = Utils.ConvertToDateTime(dr["EditDate"], DateTime.MinValue);
                        //info.NgayTraLoi = Utils.ConvertToDateTime(dr["NgayTraLoi"], DateTime.MinValue);
                        //info.NgaySuaTraLoi = Utils.ConvertToDateTime(dr["NgaySuaTraLoi"], DateTime.MinValue);
                        info.HoTen = Utils.ConvertToString(dr["HoTen"], string.Empty);
                        info.Email = Utils.ConvertToString(dr["Email"], string.Empty);
                        info.SDT = Utils.ConvertToString(dr["SDT"], string.Empty);
                        info.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], string.Empty);

                        //if (info.CreateDate != DateTime.MinValue)
                        //    info.NgayHoi_Str = Format.FormatDate(info.CreateDate);
                        //else
                        //    info.NgayHoi_Str = "";
                        //if (info.NgayTraLoi != DateTime.MinValue)
                        //    info.NgayTraLoi_Str = Format.FormatDate(info.NgayTraLoi);
                        //else
                        //    info.NgayTraLoi_Str = "";
                        list.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }



        public List<TraLoiInfo> GetCauHoiAnswered(string keySearch, int linhVucID, int start, int end)
        {
            List<TraLoiInfo> loaiTins = new List<TraLoiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_IDLINHVUC, SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar,500),
                 new SqlParameter(PARM_START, SqlDbType.Int),
                  new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = linhVucID;
            parameters[1].Value = keySearch;
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
                        info.IDLinhVuc = Utils.ConvertToInt32(dr["IDLinhVuc"], 0);
                        info.CreateDate = Utils.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
                        info.nguoi_hoi = Utils.ConvertToString(dr["TenCanBo"], string.Empty);
                        info.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"], string.Empty);
                        info.nguoi_traloi = Utils.ConvertToString(dr["nguoi_traloi"], string.Empty);
                        info.EditDate = Utils.ConvertToDateTime(dr["EditDate"], DateTime.MinValue);
                        info.NgayTraLoi = Utils.ConvertToDateTime(dr["NgayTraLoi"], DateTime.MinValue);
                        info.NgaySuaTraLoi = Utils.ConvertToDateTime(dr["NgaySuaTraLoi"], DateTime.MinValue);
                        info.Email = Utils.ConvertToString(dr["Email"], string.Empty);
                        info.SDT = Utils.ConvertToString(dr["SDT"], string.Empty);
                        //info.TenLinhVuc = Utils.ConvertToString(dr["TenLinhVuc"],string.Empty);
                        info.Public = Utils.ConvertToBoolean(dr["Public"],false);
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

        public int CountSearchAnswered(string keyword, int linhVucID)
        {

            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(PARM_IDLINHVUC,SqlDbType.Int),
                new SqlParameter(PARM_KEYWORD,SqlDbType.NVarChar,500)
            };
            parameters[0].Value = linhVucID;
            parameters[1].Value = keyword;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "CauHoi_CountSearch_Answered", parameters))
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
    }
}
