using Com.Gosol.CMS.Model.LichSuTraCuu;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Com.Gosol.CMS.DAL.LichSu
{
    public class LichSuTraCuu
    {
        #region Database query string

        private const string GET_ALL = @"LichSuTraCuu_GetALL";
        private const string GET_BY_ID = @"LichSuTraCuu_GetByID";
        private const string GET_BY_SEARCH = @"LichSuTraCuu_GetBySearch";
        private const string GET_BY_TREE_VIEW = @"LichSuTraCuu_GetByTreeView";
        private const string GET_BY_SEARCH_COQUANID = @"LichSuTraCuu_GetBySearch_CoQuanID";
        private const string COUNT_SEARCH = @"LichSuTraCuu_CountSearch";
        private const string COUNT_SEARCH_COQUANID = @"LichSuTraCuu_CountSearch_CoQuanID";

        private const string DELETE = @"LichSuTraCuu_Delete";
        private const string UPDATE = @"LichSuTraCuu_Update";
        private const string INSERT = @"LichSuTraCuu_Insert";

        private const string PARM_START = @"Start";
        private const string PARM_END = @"End";
        private const string PARM_KEYWORD = @"Keyword";
        #endregion

        #region paramaters constant

        private const string PARM_LICHSUTRACUUID = @"LichSuTraCuuID";
        private const string PARM_XULYDONID = @"XuLyDonID";
        private const string PARM_SODONTHU = @"SoDonThu";
        private const string PARM_NGAYTIEPNHAN = @"NgayTiepNhan";
        private const string PARM_PHANLOAIDON = @"PhanLoaiDon";
        private const string PARM_NOIDUNGDON = @"NoiDungDon";
        private const string PARM_DOITUONGKHIEUNAI = @"DoiTuongKhieuNai";
        private const string PARM_HUONGXULY = @"HuongXuLy";
        private const string PARM_COQUANXULY = @"CoQuanXuLy";
        private const string PARM_CANBOXULY = @"CanBoXuLy";
        private const string PARM_COQUANTIEPNHAN = @"CoQuanTiepNhan";
        private const string PARM_CANBOTIEPNHAN = @"CanBoTiepNhan";
        private const string PARM_CMND = @"CMND";
        private const string PARM_NGUOIDAIDIEN = @"NguoiDaiDien";
        private const string PARM_DIACHI = @"DiaChi";
        private const string PARM_COQUANID = @"CoQuanID";
        #endregion


        #region -- insert

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
new SqlParameter(PARM_XULYDONID, SqlDbType.Int),
new SqlParameter(PARM_SODONTHU, SqlDbType.NVarChar),
new SqlParameter(PARM_NGAYTIEPNHAN, SqlDbType.DateTime),
new SqlParameter(PARM_PHANLOAIDON, SqlDbType.NVarChar),
new SqlParameter(PARM_NOIDUNGDON, SqlDbType.NVarChar),
new SqlParameter(PARM_DOITUONGKHIEUNAI, SqlDbType.NVarChar),
new SqlParameter(PARM_HUONGXULY, SqlDbType.NVarChar),
new SqlParameter(PARM_COQUANXULY, SqlDbType.NVarChar),
new SqlParameter(PARM_CANBOXULY, SqlDbType.NVarChar),
new SqlParameter(PARM_COQUANTIEPNHAN, SqlDbType.NVarChar),
new SqlParameter(PARM_CANBOTIEPNHAN, SqlDbType.NVarChar),
new SqlParameter(PARM_CMND, SqlDbType.NVarChar),
new SqlParameter(PARM_NGUOIDAIDIEN, SqlDbType.NVarChar),
new SqlParameter(PARM_DIACHI, SqlDbType.NVarChar),
new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, LichSuTraCuuInfo info)
        {
            parms[0].Value = info.XuLyDonID;
            parms[1].Value = info.SoDonThu;
            parms[2].Value = info.NgayTiepNhan;
            parms[3].Value = info.PhanLoaiDon;
            parms[4].Value = info.NoiDungDon;
            parms[5].Value = info.DoiTuongKhieuNai;
            parms[6].Value = info.HuongXuLy;
            parms[7].Value = info.CoQuanXuLy;
            parms[8].Value = info.CanBoXuLy;
            parms[9].Value = info.CoQuanTiepNhan;
            parms[10].Value = info.CanBoTiepNhan;
            parms[11].Value = info.CMND;
            parms[12].Value = info.NguoiDaiDien;
            parms[13].Value = info.DiaChi;
            parms[14].Value = info.CoQuanID;
        }

        public int Insert(LichSuTraCuuInfo info)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, info);
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
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                conn.Close();
            }
            return Convert.ToInt32(val);
        }

        private LichSuTraCuuInfo GetData(SqlDataReader rdr)
        {
            LichSuTraCuuInfo info = new LichSuTraCuuInfo
            {
                LichSuTraCuuID = Utils.GetInt32(rdr["LichSuTraCuuID"], 0),
                XuLyDonID = Utils.GetInt32(rdr["XuLyDonID"], 0),
                SoDonThu = Utils.GetString(rdr["SoDonThu"], string.Empty),
                NgayTiepNhan = Utils.GetDateTime(rdr["NgayTiepNhan"], DateTime.MinValue),
                PhanLoaiDon = Utils.GetString(rdr["PhanLoaiDon"], string.Empty),
                NoiDungDon = Utils.GetString(rdr["NoiDungDon"], string.Empty),
                DoiTuongKhieuNai = Utils.GetString(rdr["DoiTuongKhieuNai"], string.Empty),
                HuongXuLy = Utils.GetString(rdr["HuongXuLy"], string.Empty),
                CoQuanXuLy = Utils.GetString(rdr["CoQuanXuLy"], string.Empty),
                CanBoXuLy = Utils.GetString(rdr["CanBoXuLy"], string.Empty),
                CoQuanTiepNhan = Utils.GetString(rdr["CoQuanTiepNhan"], string.Empty),
                CanBoTiepNhan = Utils.GetString(rdr["CanBoTiepNhan"], string.Empty),
                CMND = Utils.GetString(rdr["CMND"], string.Empty),
                NguoiDaiDien = Utils.GetString(rdr["NguoiDaiDien"], string.Empty),
                DiaChi = Utils.GetString(rdr["DiaChi"], string.Empty),
                CoQuanID = Utils.GetInt32(rdr["CoQuanID"], 0),
                NgayTraCuu = Utils.GetDateTime(rdr["NgayTraCuu"], DateTime.MinValue),
                TrangThaiDonThu = Utils.GetString(rdr["TrangThaiDonThu"], string.Empty),
            };
            return info;
        }



        public int CountSearch(string keyword)
        {
            int result = 0;
            SqlParameter parm = new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 200)
            {
                Value = keyword
            };
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
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public int CountSearch_CoQuanID(string keyword, int coQuanID)
        {
            int result = 0;
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = coQuanID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COUNT_SEARCH_COQUANID, parm))
                {
                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();

                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public List<LichSuTraCuuInfo> GetBySearch(string keyword, int start, int end)
        {
            List<LichSuTraCuuInfo> list = new List<LichSuTraCuuInfo>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = start;
            parameters[2].Value = end;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH, parameters))//
            {
                while (dr.Read())
                {
                    LichSuTraCuuInfo info = GetData(dr);
                    list.Add(info);
                }
                dr.Close();
            }
            return list;
        }

        public List<LichSuTraCuuInfo> GetBySearch_CoQuanID(string keyword, int start, int end, int coQuanID)
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

            List<LichSuTraCuuInfo> list = new List<LichSuTraCuuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_SEARCH_COQUANID, parm))
                {
                    while (dr.Read())
                    {
                        LichSuTraCuuInfo canBoInfo = GetData(dr);
                        list.Add(canBoInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<LichSuTraCuuInfo> GetByTreeView(string keyword, int start, int end, int CoQuanID)
        {
            List<LichSuTraCuuInfo> list = new List<LichSuTraCuuInfo>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter(PARM_START, SqlDbType.Int),
                new SqlParameter(PARM_END, SqlDbType.Int),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parameters[0].Value = keyword;
            parameters[1].Value = start;
            parameters[2].Value = end;
            parameters[3].Value = CoQuanID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_TREE_VIEW, parameters))//
            {
                while (dr.Read())
                {
                    LichSuTraCuuInfo info = GetData(dr);
                    list.Add(info);
                }
                dr.Close();
            }
            return list;
        }
        public int CountSearchByTreeView(string keyword, int CoQuanID)
        {
            int result = 0;
            SqlParameter[] parameters = new SqlParameter[]
          {
                new SqlParameter(PARM_KEYWORD, SqlDbType.NVarChar, 50),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
          };
            parameters[0].Value = keyword;
            parameters[1].Value = CoQuanID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "LichSuTraCuu_GetByTreeView_Count", parameters))
                {
                    if (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);
                    }
                    dr.Close();

                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }

    #endregion
}
