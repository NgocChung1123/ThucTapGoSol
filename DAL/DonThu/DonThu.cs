using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.DonThu;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Com.Gosol.CMS.DAL.DonThu
{
    public class DonThu
    {
        #region query string
        private const string INSERT = @"DonThu_Insert";
        #endregion

        #region param
        #endregion

        private DonThuInfo GetData(SqlDataReader rdr)
        {
            DonThuInfo donThuInfo = new DonThuInfo
            {
                ID = Utils.GetInt32(rdr["ID"], 0),
                XuLyDonID = Utils.GetInt32(rdr["XuLyDonID"], 0),
                SoDonThu = Utils.GetString(rdr["SoDonThu"], string.Empty),
                NgayBanHanh = Utils.GetString(rdr["NgayBanHanh"], string.Empty),
                NgayTiepNhan = Utils.GetString(rdr["NgayTiepNhan"], string.Empty),
                CoQuanTiepNhan = Utils.GetString(rdr["CoQuanTiepNhan"], string.Empty),
                CanBoTiepNhan = Utils.GetString(rdr["CanBoTiepNhan"], string.Empty),
                PhanLoaiDon = Utils.GetString(rdr["PhanLoaiDon"], string.Empty),
                NoiDungDon = Utils.GetString(rdr["NoiDungDon"], string.Empty),
                DoiTuongKhieuNai = Utils.GetString(rdr["DoiTuongKhieuNai"], string.Empty),
                CoQuanXuLy = Utils.ConvertToString(rdr["CoQuanXuLy"], string.Empty),
                PhongBanXuLy = Utils.ConvertToString(rdr["PhongBanXuLy"], string.Empty),
                CanBoXuLy = Utils.GetString(rdr["CanBoXuLy"], string.Empty),
                TrangThaiDonThu = Utils.GetString(rdr["TrangThaiDonThu"], string.Empty),
                NguoiDaiDien = Utils.GetString(rdr["NguoiDaiDien"], string.Empty),
                CongKhai = Utils.GetString(rdr["CongKhai"], "0"),
                DiaChi = Utils.GetString(rdr["DiaChi"], string.Empty),
                FileQuyetDinh = Utils.GetString(rdr["FileQuyetDinh"], string.Empty),
                CoQuanID = Utils.GetInt32(rdr["CoQuanID"], 0),
                HuongXuLy = Utils.GetString(rdr["HuongXuLy"], string.Empty),
                CoQuanBanHanhID = Utils.GetInt32(rdr["CoQuanBanHanhID"], 0),
                CoQuanGiaiQuyetID = Utils.GetInt32(rdr["CoQuanGiaiQuyetID"], 0),
                CoQuanXuLyID = Utils.GetInt32(rdr["CoQuanXuLyID"], 0),
                SoTienPhaiThu = Utils.GetInt32(rdr["SoTienPhaiThu"], 0),
                SoDatPhaiThu = Utils.GetInt32(rdr["SoDatPhaiThu"], 0),
                SoDoiTuongBiXuLy = Utils.GetInt32(rdr["SoDoiTuongBiXuLy"], 0),
                NgayXuLyStr = Utils.GetString(rdr["NgayXuLy"], string.Empty),
                HuongXuLyID = Utils.GetInt32(rdr["HuongXuLyID"], 0),
                TenQuyetDinh = Utils.GetString(rdr["TenQuyetDinh"], ""),
                CoQuanBanHanh = Utils.GetString(rdr["CoQuanBanHanh"], ""),
                TrangThaiDonID = Utils.GetInt32(rdr["TrangThaiDonID"], 0),
            };
            return donThuInfo;
        }

        private DonThuInfo GetFileData(SqlDataReader rdr)
        {
            DonThuInfo donThuInfo = new DonThuInfo
            {
                ID = Utils.GetInt32(rdr["DonThuID"], 0),
                NgayUp = Utils.GetDateTime(rdr["NgayUp"], DateTime.MinValue),
                NgayUp_Str = Format.FormatDate(Utils.GetDateTime(rdr["NgayUp"], DateTime.MinValue)),
                TenFile = Utils.GetString(rdr["TenFile"], string.Empty),
                FileURL = Utils.GetString(rdr["FileURL"], string.Empty),
        };
            return donThuInfo;
        }

        private DonThuInfo GetFileYKienXuLyData(SqlDataReader rdr)
        {
            DonThuInfo donThuInfo = new DonThuInfo
            {
                ID = Utils.GetInt32(rdr["ID"], 0),
                NgayUp = Utils.GetDateTime(rdr["NgayUp"], DateTime.MinValue),
                NgayUp_Str = Format.FormatDate(Utils.GetDateTime(rdr["NgayUp"], DateTime.MinValue)),
                TenFile = Utils.GetString(rdr["TenFile"], string.Empty),
                FileURL = Utils.GetString(rdr["FileURL"], string.Empty),
        };
            return donThuInfo;
        }
        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@DonThuID", SqlDbType.Int),
                new SqlParameter("@XuLyDonID", SqlDbType.Int),
                new SqlParameter("@SoDonThu", SqlDbType.NVarChar),
                new SqlParameter("@NgayTiepNhan", SqlDbType.NVarChar),
                new SqlParameter("@CanBoTiepNhan", SqlDbType.NVarChar),
                new SqlParameter("@PhanLoaiDon", SqlDbType.NVarChar),
                new SqlParameter("@NoiDungDon", SqlDbType.NVarChar),
                new SqlParameter("@DoiTuongKhieuNai", SqlDbType.NVarChar),
                new SqlParameter("@CoQuanXuLy", SqlDbType.NVarChar),
                new SqlParameter("@PhongBanXuLy",SqlDbType.NVarChar),
                new SqlParameter("@CanBoXuLy", SqlDbType.NVarChar),
                new SqlParameter("@TrangThaiDonThu", SqlDbType.NVarChar),
                new SqlParameter("@CongKhai", SqlDbType.NVarChar),
                new SqlParameter("@CoQuanTiepNhan", SqlDbType.NVarChar),
                new SqlParameter("@NguoiDaiDien", SqlDbType.NVarChar),
                new SqlParameter("@CoQuanID", SqlDbType.Int),

                new SqlParameter("@CoQuanXuLyID", SqlDbType.Int),
                new SqlParameter("@HuongXuLyID", SqlDbType.Int),
                new SqlParameter("@HuongXuLy", SqlDbType.NVarChar),
                new SqlParameter("@TrangThaiDonID", SqlDbType.Int),
                new SqlParameter("@FileQuyetDinh", SqlDbType.NVarChar),
                new SqlParameter("@CoQuanGiaiQuyetID", SqlDbType.Int),
                new SqlParameter("@CoQuanGiaiQuyet", SqlDbType.NVarChar),
                new SqlParameter("@CoQuanBanHanhID", SqlDbType.Int),
                new SqlParameter("@CoQuanBanHanh", SqlDbType.NVarChar),
                new SqlParameter("@SoTienPhaiThu", SqlDbType.Int),
                new SqlParameter("@SoDatPhaiThu", SqlDbType.Int),
                new SqlParameter("@SoDoiTuongBiXuLy", SqlDbType.Int),

                new SqlParameter("@DiaChi", SqlDbType.NVarChar),
                new SqlParameter("@NgayBanHanh", SqlDbType.NVarChar),
                new SqlParameter("@NgayXuLyStr", SqlDbType.NVarChar),
                new SqlParameter("@TenQuyetDinh", SqlDbType.NVarChar)
            };
            return parms;
        }
        private void SetInsertParms(SqlParameter[] parms, DonThuInfo DonThuInfo)
        {
            parms[0].Value = Utils.GetInt32(DonThuInfo.ID, 0);
            parms[1].Value = Utils.GetInt32(DonThuInfo.XuLyDonID, 0);
            parms[2].Value = Utils.GetString(DonThuInfo.SoDonThu, string.Empty);
            parms[3].Value = Utils.GetString(DonThuInfo.NgayTiepNhan, string.Empty);
            parms[4].Value = Utils.GetString(DonThuInfo.CanBoTiepNhan, string.Empty);
            parms[5].Value = Utils.GetString(DonThuInfo.PhanLoaiDon, string.Empty);
            parms[6].Value = Utils.GetString(DonThuInfo.NoiDungDon, string.Empty);
            parms[7].Value = Utils.GetString(DonThuInfo.DoiTuongKhieuNai, string.Empty);
            if (DonThuInfo.CoQuanXuLyID == 0)
            {
                parms[8].Value = "";
            }
            else
                parms[8].Value = Utils.GetString(DonThuInfo.CoQuanXuLy, string.Empty);
            parms[9].Value = Utils.GetString(DonThuInfo.PhongBanXuLy, string.Empty);
            parms[10].Value = Utils.GetString(DonThuInfo.CanBoXuLy, string.Empty);
            parms[11].Value = Utils.GetString(DonThuInfo.TrangThaiDonThu, string.Empty);
            parms[12].Value = Utils.GetString(DonThuInfo.CongKhai, "0");
            parms[13].Value = Utils.GetString(DonThuInfo.CoQuanTiepNhan, string.Empty);
            parms[14].Value = Utils.GetString(DonThuInfo.NguoiDaiDien, string.Empty);
            parms[15].Value = Utils.GetInt32(DonThuInfo.CoQuanID, 0);
            parms[16].Value = Utils.GetInt32(DonThuInfo.CoQuanXuLyID, 0);
            parms[17].Value = Utils.GetInt32(DonThuInfo.HuongXuLyID, 0);
            parms[18].Value = Utils.GetString(DonThuInfo.HuongXuLy, string.Empty);
            parms[19].Value = Utils.GetInt32(DonThuInfo.TrangThaiDonID, 0);
            parms[20].Value = Utils.GetString(DonThuInfo.FileQuyetDinh, string.Empty);
            parms[21].Value = Utils.GetInt32(DonThuInfo.CoQuanGiaiQuyetID, 0);
            if (DonThuInfo.CoQuanXuLyID == 0)
            {
                parms[22].Value = "";
            }
            else
                parms[22].Value = Utils.GetString(DonThuInfo.CoQuanGiaiQuyet, string.Empty);

            parms[23].Value = Utils.GetInt32(DonThuInfo.CoQuanBanHanhID, 0);
            if (DonThuInfo.CoQuanXuLyID == 0)
            {
                parms[24].Value = "";
            }
            else
                parms[24].Value = Utils.GetString(DonThuInfo.CoQuanBanHanh, string.Empty);
            parms[25].Value = Utils.GetInt32(DonThuInfo.SoTienPhaiThu, 0);
            parms[26].Value = Utils.GetInt32(DonThuInfo.SoDatPhaiThu, 0);
            parms[27].Value = Utils.GetInt32(DonThuInfo.SoDoiTuongBiXuLy, 0);

            parms[28].Value = Utils.GetString(DonThuInfo.DiaChi, string.Empty);
            parms[29].Value = Utils.GetString(DonThuInfo.NgayBanHanh, string.Empty);
            parms[30].Value = Utils.GetString(DonThuInfo.NgayXuLyStr, string.Empty);
            parms[31].Value = Utils.GetString(DonThuInfo.TenQuyetDinh, string.Empty);
        }

        public int Insert(DonThuInfo DonThuInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, DonThuInfo);
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
            return Utils.ConvertToInt32(val, 0);
        }

        public int Update(DonThuInfo DonThuInfo)
        {
            int val = 0;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, DonThuInfo);
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "DonThu_Update", parameters);
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
            return val;
        }

        public int Delete(int donThuID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter(@"DonThuID", SqlDbType.Int) };
            parameters[0].Value = donThuID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, @"DonThu_Delete", parameters);
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
            return val;
        }

        public DonThuInfo GetByID(int donThuID)
        {
            DonThuInfo info = new DonThuInfo();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetByID", new SqlParameter[] { new SqlParameter("@DonThuID", SqlDbType.Int) { Value = donThuID } }))
            {
                if (dr.Read())
                {
                    info = GetData(dr);
                }
                dr.Close();
            }
            return info;
        }

        public int CountSearch(string keyword)
        {
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),

            };
            parms[0].Value = keyword;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_CountSearch", parms))
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

        public IList<DonThuInfo> GetBySearch(string keyword, int start, int end)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50),
                new SqlParameter("@start", SqlDbType.Int),
                new SqlParameter("@end", SqlDbType.Int),
                //new SqlParameter("@LoaiDonThu", SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            //parm[3].Value = loaiDonThu;
            IList<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySearch", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }
        public List<DonThuInfo> GetListFileKetQuaByID(int id)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@DonThuID", SqlDbType.Int),
            };
            parm[0].Value = id;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetFileByID", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetFileData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        public List<DonThuInfo> GetListFileYKienXuLyByID(int id)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@ID", SqlDbType.Int),
            };
            parm[0].Value = id;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetFileYKienXuLyByID", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetFileYKienXuLyData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        public int UpdatePublic(int xldID)
        {
            int val = 0;
            SqlParameter[] parameters = {
                new SqlParameter("@XuLyDonID", SqlDbType.Int),
                //new SqlParameter("@CongKhai", SqlDbType.Bit),
                
            };
            parameters[0].Value = xldID;
            //parameters[1].Value = isPublic;

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "DonThu_UpdateCongKhai", parameters);
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

        public DonThuInfo GetBySoDonThu(string soDonThu, int coQuanID)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@SoDonThu", SqlDbType.NVarChar, 50),
                new SqlParameter("@CoQuanID", SqlDbType.Int),

            };
            parm[0].Value = soDonThu;
            parm[1].Value = coQuanID;

            DonThuInfo donThu = new DonThuInfo();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySoDonThu", parm))
                {
                    if (dr.Read())
                    {
                        donThu = GetData(dr);

                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return donThu;
        }

        public IList<DonThuInfo> GetTop10()
        {

            SqlParameter[] parm = null;
            IList<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetTop10", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        private SqlParameter[] GetInsertFileKQParms()
        {
            SqlParameter[] parms = new SqlParameter[]{
    
               
                new SqlParameter("@TenFile", SqlDbType.NVarChar),
                new SqlParameter("@FileUrl", SqlDbType.NVarChar),
                new SqlParameter("@DonThuID", SqlDbType.Int)
                };
            return parms;
        }

        private void SetInsertFileKQParms(SqlParameter[] parms, FileHoSoInfo info)
        {
            parms[0].Value = info.TenFile;
            parms[1].Value = info.FileURL;
            parms[2].Value = info.DonThuID;
        }
        public int InsertFileKetQua(FileHoSoInfo info)
        {

            object val;

            SqlParameter[] parameters = GetInsertFileKQParms();
            SetInsertFileKQParms(parameters, info);

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {

                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure,"FileKetQua_Insert", parameters);
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

        private SqlParameter[] GetInsertFileYKienXLParms()
        {
            SqlParameter[] parms = new SqlParameter[]{


                new SqlParameter("@TenFile", SqlDbType.NVarChar),
                new SqlParameter("@FileUrl", SqlDbType.NVarChar),
                new SqlParameter("@XuLyDonID", SqlDbType.Int)
                };
            return parms;
        }

        private void SetInsertFileYKienXLParms(SqlParameter[] parms, FileHoSoInfo info)
        {
            parms[0].Value = info.TenFile;
            parms[1].Value = info.FileURL;
            parms[2].Value = info.XuLyDonID;
        }

        public int InsertFileYKienXuLy(FileHoSoInfo info)
        {

            object val;

            SqlParameter[] parameters = GetInsertFileYKienXLParms();
            SetInsertFileYKienXLParms(parameters, info);

            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {

                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "FileYKienXuLy_Insert", parameters);
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


        #region FrontEnd

        public IList<DonThuInfo> GetBySearchFrontEnd(string keyword, int start, int end)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50),
                new SqlParameter("@start", SqlDbType.Int),
                new SqlParameter("@end", SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            IList<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySearch_Front", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        public int CountSearchQuyetDinh(string keyword, int coQuanID, string from, string to)
        {
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),
                new SqlParameter("@CoQuanID", SqlDbType.Int),
                new SqlParameter("@TuNgay", SqlDbType.NVarChar),
                new SqlParameter("@DenNgay", SqlDbType.NVarChar)
            };
            parms[0].Value = keyword;
            parms[1].Value = coQuanID;
            parms[2].Value = from;
            parms[3].Value = (to == "" ? "31-12-9999" : to);

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_CountSearchQuyetDinh_1", parms))
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

        public int CountSearchQuyetDinh(string keyword, int coQuanID)
        {
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parms[0].Value = keyword;
            parms[1].Value = coQuanID;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_CountSearchQuyetDinh", parms))
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

        public List<DonThuInfo> GetBySearchQuyetDinh(string keyword, int start, int end, int coQuanID, string tuNgay, string denNgay)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50),
                new SqlParameter("@start", SqlDbType.Int),
                new SqlParameter("@end", SqlDbType.Int),
                new SqlParameter("@CoQuanID", SqlDbType.Int),
                new SqlParameter("@TuNgay", SqlDbType.NVarChar),
                new SqlParameter("@DenNgay", SqlDbType.NVarChar)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            parm[3].Value = coQuanID;
            parm[4].Value = tuNgay;
            parm[5].Value = (denNgay == "" ? "31-12-9999" : denNgay);
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySearchQuyetDinh", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        public List<DonThuInfo> GetBySearchQuyetDinh(string keyword, int start, int end, int coQuanID)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50),
                new SqlParameter("@start", SqlDbType.Int),
                new SqlParameter("@end", SqlDbType.Int),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            parm[3].Value = coQuanID;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySearchQuyetDinh_1", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }
        #endregion

        #region BackEnd
        public int CountSearchQuyetDinhBackEnd(string keyword, int coQuanID)
        {
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parms[0].Value = keyword;
            parms[1].Value = coQuanID;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_CountSearchQuyetDinh_BackEnd", parms))
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

        public List<DonThuInfo> GetBySearchQuyetDinhBackEnd(string keyword, int start, int end, int coQuanID)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50),
                new SqlParameter("@start", SqlDbType.Int),
                new SqlParameter("@end", SqlDbType.Int),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            parm[3].Value = coQuanID;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {
                int stt = 0;
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySearchQuyetDinh_BackEnd", parm))
                {
                    while (dr.Read())
                    {
                        stt++;
                        DonThuInfo donThuInfo = GetData(dr);
                        donThuInfo.STT = stt;
                        DonThus.Add(donThuInfo);

                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        public int CountSearchVanBanBackEnd(string keyword, int coQuanID)
        {
            int result = 0;
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 200),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parms[0].Value = keyword;
            parms[1].Value = coQuanID;

            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_CountSearchVanBan_BackEnd", parms))
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

        public List<DonThuInfo> GetBySearchVanBanBackEnd(string keyword, int start, int end, int coQuanID)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@keyword", SqlDbType.NVarChar, 50),
                new SqlParameter("@start", SqlDbType.Int),
                new SqlParameter("@end", SqlDbType.Int),
                new SqlParameter("@CoQuanID", SqlDbType.Int)
            };
            parm[0].Value = keyword;
            parm[1].Value = start;
            parm[2].Value = end;
            parm[3].Value = coQuanID;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_GetBySearchVanBan_BackEnd", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DonThus;
        }

        public int UpdateIsShow(bool IsChecked, int ID)
        {
            int val = 0;
            SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter(@"DonThuID", SqlDbType.Int),
            new SqlParameter(@"IsChecked", SqlDbType.NVarChar)
            };

            parameters[0].Value = ID;
            parameters[1].Value = IsChecked ? "1" : "0";
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        val = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, @"DonThu_UpdateIsPublic", parameters);
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
            return val;
        }
        public List<DonThuInfo> SearchVBTraLoi(int coQuanID, int coQuanID2, string SoDonThu, string TenQuyetDinh, DateTime? From, DateTime? To, int Start, int End)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@CoQuanID", SqlDbType.Int),
                new SqlParameter("@CoQuanID2", SqlDbType.Int),
                new SqlParameter("@SoDonThu", SqlDbType.NVarChar),
                new SqlParameter("@TenQuyetDinh", SqlDbType.NVarChar),
                new SqlParameter("@From", SqlDbType.DateTime),
                new SqlParameter("@To", SqlDbType.DateTime),
                new SqlParameter("@Start", SqlDbType.Int),
                new SqlParameter("@End", SqlDbType.Int)

            };
            parm[0].Value = coQuanID;
            parm[1].Value = coQuanID2;
            parm[2].Value = SoDonThu;
            parm[3].Value = TenQuyetDinh;
            parm[4].Value = From == DateTime.MinValue ? null : From;
            parm[5].Value = To == DateTime.MinValue ? null : To;
            parm[6].Value = Start;
            parm[7].Value = End;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_FrontEnd_SearchVBTraLoi", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo DonThuInfo = GetData(dr);
                        DonThus.Add(DonThuInfo);

                    }
                    dr.Close();
                }
                //TotalRows = (int)parm[7].Value;
            }
            catch (Exception ex)
            {
                throw;
            }

            return DonThus;
        }
        public int CountSearchVBTraLoi(int coQuanID, int coQuanID2, string SoDonThu, string TenQuyetDinh, DateTime? From, DateTime? To)
        {

            int result = 0;
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@CoQuanID", SqlDbType.Int),
                 new SqlParameter("@CoQuanID2", SqlDbType.Int),
                new SqlParameter("@SoDonThu", SqlDbType.NVarChar),
                new SqlParameter("@TenQuyetDinh", SqlDbType.NVarChar),
                new SqlParameter("@From", SqlDbType.DateTime),
                new SqlParameter("@To", SqlDbType.DateTime)

            };
            parm[0].Value = coQuanID;
            parm[1].Value = coQuanID2;
            parm[2].Value = SoDonThu;
            parm[3].Value = TenQuyetDinh;
            parm[4].Value = From == DateTime.MinValue ? null : From;
            parm[5].Value = To == DateTime.MinValue ? null : To;

            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_FrontEnd_CountSearchVBTraLoi", parm))
                {
                    while (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);

                    }
                    dr.Close();
                }
                //TotalRows = (int)parm[7].Value;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        public List<DonThuInfo> SearchDonThuByType(int coQuanID, int coQuanID2, string SoDonThu, string TenQuyetDinh, DateTime? From, DateTime? To, int Start, int End, int Type)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@CoQuanID", SqlDbType.Int),
                new SqlParameter("@CoQuanID2", SqlDbType.Int),
                new SqlParameter("@SoDonThu", SqlDbType.NVarChar),
                new SqlParameter("@TenQuyetDinh", SqlDbType.NVarChar),
                new SqlParameter("@From", SqlDbType.DateTime),
                new SqlParameter("@To", SqlDbType.DateTime),
                new SqlParameter("@Start", SqlDbType.Int),
                new SqlParameter("@End", SqlDbType.Int),
                new SqlParameter("@Type", SqlDbType.Int)

            };
            parm[0].Value = coQuanID;
            parm[1].Value = coQuanID2;
            parm[2].Value = SoDonThu;
            parm[3].Value = TenQuyetDinh;
            parm[4].Value = From == DateTime.MinValue ? null : From;
            parm[5].Value = To == DateTime.MinValue ? null : To;
            parm[6].Value = Start;
            parm[7].Value = End;
            parm[8].Value = Type;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_FrontEnd_SearchDonThuByType", parm))
                {
                    while (dr.Read())
                    {
                        DonThuInfo donThuInfo = GetData(dr);

                        donThuInfo.CoQuanGiaiQuyet = Utils.GetString(dr["CoQuanGiaiQuyet"], string.Empty);
                        string pathf = donThuInfo.FileQuyetDinh.ToUpper();
                        if (pathf.Contains(".DOC") || pathf.Contains(".PDF"))
                            donThuInfo.XemTruocFile = 1;
                        else
                            donThuInfo.XemTruocFile = 0;
                        if (donThuInfo.TrangThaiDonID != 2)
                            donThuInfo.NgayBanHanh = "";

                        DonThus.Add(donThuInfo);
                        

                    }
                    dr.Close();
                }
                //TotalRows = (int)parm[7].Value;
            }
            catch (Exception ex)
            {
                throw;
            }

            return DonThus;
        }
        public int CountSearchDonThuByType(int coQuanID, int coQuanID2, string SoDonThu, string TenQuyetDinh, DateTime? From, DateTime? To, int Type)
        {

            int result = 0;
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@CoQuanID", SqlDbType.Int),
                 new SqlParameter("@CoQuanID2", SqlDbType.Int),
                new SqlParameter("@SoDonThu", SqlDbType.NVarChar),
                new SqlParameter("@TenQuyetDinh", SqlDbType.NVarChar),
                new SqlParameter("@From", SqlDbType.DateTime),
                new SqlParameter("@To", SqlDbType.DateTime),
                new SqlParameter("@Type", SqlDbType.Int)

            };
            parm[0].Value = coQuanID;
            parm[1].Value = coQuanID2;
            parm[2].Value = SoDonThu;
            parm[3].Value = TenQuyetDinh;
            parm[4].Value = From == DateTime.MinValue ? null : From;
            parm[5].Value = To == DateTime.MinValue ? null : To;
            parm[6].Value = Type;
            List<DonThuInfo> DonThus = new List<DonThuInfo>();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "DonThu_FrontEnd_CountSearchDonThuByType", parm))
                {
                    while (dr.Read())
                    {
                        result = Utils.ConvertToInt32(dr["CountNum"], 0);

                    }
                    dr.Close();
                }
                //TotalRows = (int)parm[7].Value;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        #endregion
    }
}
