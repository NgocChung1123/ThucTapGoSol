using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Com.Gosol.CMS.Utility;
using System.Data;
using System.Reflection;
using Com.Gosol.CMS.Model;
using System;
using Com.Gosol.CMS.Model.WorkFlow_Model;
using System.Collections;

namespace Com.Gosol.CMS.DAL
{
    public class CoQuan
    {
        #region Database query string

        private const string SELECT_ALL = @"DM_CoQuan_GetAll";
        private const string SELECT_ALL_FOR_AJAX = @"DM_CoQuan_GetAllForAjax";
        private const string GETCOQUAN_BY_COQUANID_FOR_AJAX = @"DM_CoQuan_GetCoQuanForAjax";
        private const string SELECT_BY_ID = @"DM_CoQuan_GetByID";
        private const string SELECT_BY_PARENTID = @"DM_CoQuan_GetByParentID";
        private const string SELECT_TREE_VIEW_BY_ID = @"DM_CoQuan_GetTreeViewByID";
        private const string GET_ALL_COQUAN = @"DM_CoQuan_GetAll";
        private const string DELETE = @"DM_CoQuan_Delete";
        private const string UPDATE = @"DM_CoQuan_Update";
        private const string INSERT = @"DM_CoQuan_Insert";
        private const string GET_BY_CAP = @"DM_CoQuan_GetByCap";
        private const string GET_LIST_CQGQ = @"DM_CoQuan_GetDSCoQuanGQ";

        private const string SELECT_ALL_HAVE_NULL = @"DM_CoQuan_GetAllHaveNull";

        private const string GET_PARENTS = @"DM_CoQuan_GetParents";
        private const string GET_CHILRENTS = @"DM_CoQuan_GetChilrents";
        private const string GET_PARENTS_BY_TINH = @"DM_CoQuan_GetParentsByTinh";

        private const string GET_BY_HUYEN = @"DM_CoQuan_GetByHuyen";

        private const string GET_COQUAN_DA_GIAIQUYET = @"DM_CoQuan_GetDaGiaiQuyet";

        private const string GET_COQUAN_BYCANBOID = @"DM_CoQuan_GetByCanBoID";

        // workflow
        private const string GETALL_WORKFLOW = @"WF_GetAll_WorkFlow";
        #endregion

        #region paramaters constant

        private const string PARM_COQUANID = @"CoQuanID";
        private const string PARM_TENCOQUAN = @"TenCoQuan";
        private const string PARM_COQUANCHA = @"CoQuanChaID";
        private const string PARM_THAMQUYENID = @"ThamQuyenID";
        private const string PARM_TINHID = @"TinhID";
        private const string PARM_HUYENID = @"HuyenID";
        private const string PARM_XAID = @"XaID";

        private const string PARM_CAP_ID = @"CapID";

        private const string PARM_CAPUBND = "@CapUBND";
        private const string PARM_CAPTHANHTRA = "@CapThanhTra";
        private const string PARM_SUDUNGPM = "@SuDungPM";
        private const string PARM_MACQ = @"MaCQ";
        private const string PARM_SUDUNGQUYTRINH = @"SuDungQuyTrinh";
        private const string PARM_SUDUNGQUYTRINHGQ = @"SuDungQuyTrinhGQ";
        private const string PARM_QUYTRINHVANTHUTIEPNHAN = @"QuyTrinhVanThuTiepNhan";
        private const string PARM_QUYTRINHVANTHUTIEPDAN = @"@QuyTrinhVanThuTiepDan";
        private const string PARM_CQCO_HIEULUC = @"CQCoHieuLuc";
        //private const string PARM_VALID = @"Valid";
        private const string PARM_CANBOID = @"CanBoID";
        private const string PARM_WORKFLOWID = @"WorkFlowID";
        private const string PARM_WFTIENHANHTTID = @"WFTienHanhTTID";
        #endregion

        private CoQuanInfo GetData(SqlDataReader rdr)
        {
            CoQuanInfo coQuanInfo = new CoQuanInfo();
            coQuanInfo.CoQuanID = Utils.GetInt32(rdr["CoQuanID"], 0);
            coQuanInfo.TenCoQuan = Utils.GetString(rdr["TenCoQuan"], String.Empty);

            coQuanInfo.CoQuanChaID = Utils.GetInt32(rdr["CoQuanChaID"], 0);
            //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
            coQuanInfo.CapID = Utils.ConvertToInt32(rdr["CapID"], 0);
            coQuanInfo.ThamQuyenID = Utils.GetInt32(rdr["ThamQuyenID"], 0);
            coQuanInfo.TinhID = Utils.GetInt32(rdr["TinhID"], 0);
            coQuanInfo.HuyenID = Utils.GetInt32(rdr["HuyenID"], 0);
            coQuanInfo.XaID = Utils.GetInt32(rdr["XaID"], 0);

            //coQuanInfo.CapUBND = Utils.ConvertToBoolean(rdr["CapUBND"], false);
            //coQuanInfo.CapThanhTra = Utils.ConvertToBoolean(rdr["CapThanhTra"], false);
            coQuanInfo.SuDungPM = Utils.ConvertToBoolean(rdr["SuDungPM"], false);

            coQuanInfo.MaCQ = Utils.ConvertToString(rdr["MaCQ"], String.Empty);
            //coQuanInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return coQuanInfo;
        }
        private CoQuanInfo GetDataSearch(SqlDataReader rdr)
        {
            CoQuanInfo coQuanInfo = new CoQuanInfo();
            coQuanInfo.CoQuanID = Utils.GetInt32(rdr["CoQuanID"], 0);
            coQuanInfo.TenCoQuan = Utils.GetString(rdr["TenCoQuan"], String.Empty);

            coQuanInfo.CoQuanChaID = Utils.GetInt32(rdr["CoQuanChaID"], 0);
            //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
            coQuanInfo.CapID = Utils.GetInt32(rdr["CapID"], 0);
            coQuanInfo.ThamQuyenID = Utils.GetInt32(rdr["ThamQuyenID"], 0);
            coQuanInfo.TinhID = Utils.GetInt32(rdr["TinhID"], 0);
            coQuanInfo.HuyenID = Utils.GetInt32(rdr["HuyenID"], 0);
            coQuanInfo.XaID = Utils.GetInt32(rdr["XaID"], 0);
            return coQuanInfo;
        }

        private CoQuanInfo GetDataForAjax(SqlDataReader rdr)
        {
            CoQuanInfo coQuanInfo = new CoQuanInfo();
            coQuanInfo.CoQuanID = Utils.GetInt32(rdr["CoQuanID"], 0);
            coQuanInfo.TenCoQuan = Utils.GetString(rdr["TenCoQuan"], String.Empty);

            coQuanInfo.CoQuanChaID = Utils.GetInt32(rdr["CoQuanChaID"], 0);
            //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
            coQuanInfo.CapID = Utils.GetInt32(rdr["CapID"], 0);
            coQuanInfo.ThamQuyenID = Utils.GetInt32(rdr["ThamQuyenID"], 0);
            coQuanInfo.TinhID = Utils.GetInt32(rdr["TinhID"], 0);
            coQuanInfo.HuyenID = Utils.GetInt32(rdr["HuyenID"], 0);
            coQuanInfo.XaID = Utils.GetInt32(rdr["XaID"], 0);

            //coQuanInfo.CapUBND = Utils.ConvertToBoolean(rdr["CapUBND"], false);
            //coQuanInfo.CapThanhTra = Utils.ConvertToBoolean(rdr["CapThanhTra"], false);
            coQuanInfo.SuDungPM = Utils.ConvertToBoolean(rdr["SuDungPM"], false);

            //coQuanInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);

            coQuanInfo.hasChild = Utils.GetInt32(rdr["hasChild"], 0);

            //coQuanInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return coQuanInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_TENCOQUAN, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_COQUANCHA, SqlDbType.Int),
                new SqlParameter(PARM_CAP_ID, SqlDbType.Int),
                new SqlParameter(PARM_THAMQUYENID, SqlDbType.Int),
                new SqlParameter(PARM_TINHID, SqlDbType.Int),
                new SqlParameter(PARM_HUYENID, SqlDbType.Int),
                new SqlParameter(PARM_XAID, SqlDbType.Int),

                //new SqlParameter(PARM_CAPUBND, SqlDbType.Int),
                //new SqlParameter(PARM_CAPTHANHTRA, SqlDbType.Int),
                new SqlParameter(PARM_SUDUNGPM, SqlDbType.Int),
                new SqlParameter(PARM_MACQ, SqlDbType.NVarChar,100),
                //new SqlParameter(PARM_SUDUNGQUYTRINH, SqlDbType.Bit),
                //new SqlParameter(PARM_SUDUNGQUYTRINHGQ, SqlDbType.Bit),
                //new SqlParameter(PARM_QUYTRINHVANTHUTIEPNHAN, SqlDbType.Bit),
                //new SqlParameter(PARM_QUYTRINHVANTHUTIEPDAN, SqlDbType.Bit),
                new SqlParameter(PARM_CQCO_HIEULUC, SqlDbType.Bit),
                new SqlParameter(PARM_WORKFLOWID, SqlDbType.Int),
                new SqlParameter(PARM_WFTIENHANHTTID, SqlDbType.Int)

            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, CoQuanInfo coQuanInfo)
        {
            parms[0].Value = coQuanInfo.TenCoQuan;
            if (coQuanInfo.CoQuanChaID != 0) parms[1].Value = coQuanInfo.CoQuanChaID;
            else parms[1].Value = DBNull.Value;
            if (coQuanInfo.CapID == 0)
                parms[2].Value = DBNull.Value;
            else
                parms[2].Value = coQuanInfo.CapID;
            if (coQuanInfo.ThamQuyenID == 0)
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = coQuanInfo.ThamQuyenID;
            if (coQuanInfo.TinhID == 0)
                parms[4].Value = DBNull.Value;
            else
                parms[4].Value = coQuanInfo.TinhID;
            if (coQuanInfo.HuyenID == 0)
                parms[5].Value = DBNull.Value;
            else parms[5].Value = coQuanInfo.HuyenID;

            if (coQuanInfo.XaID == 0)
                parms[6].Value = DBNull.Value;
            else parms[6].Value = coQuanInfo.XaID;


            //parms[7].Value = coQuanInfo.CapUBND;
            //parms[8].Value = coQuanInfo.CapThanhTra;
            parms[7].Value = coQuanInfo.SuDungPM;
            parms[8].Value = coQuanInfo.MaCQ;
            //parms[11].Value = coQuanInfo.SuDungQuyTrinh;
            //parms[12].Value = coQuanInfo.SuDungQuyTrinhGQ;
            //parms[13].Value = coQuanInfo.QuyTrinhVanThuTiepNhan;
            //parms[14].Value = coQuanInfo.QTVanThuTiepDan;
            parms[9].Value = coQuanInfo.CQCoHieuLuc;
            if (coQuanInfo.WorkFlowID == 0)
                parms[10].Value = DBNull.Value;
            else parms[10].Value = coQuanInfo.WorkFlowID;
            if (coQuanInfo.WFTienHanhTTID == 0)
                parms[11].Value = DBNull.Value;
            else parms[11].Value = coQuanInfo.WFTienHanhTTID;
        }

        private SqlParameter[] GetUpdateParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int),
                new SqlParameter(PARM_TENCOQUAN, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_COQUANCHA, SqlDbType.Int),
                new SqlParameter(PARM_CAP_ID, SqlDbType.Int),
                new SqlParameter(PARM_THAMQUYENID, SqlDbType.Int),
                new SqlParameter(PARM_TINHID, SqlDbType.Int),
                new SqlParameter(PARM_HUYENID, SqlDbType.Int),
                new SqlParameter(PARM_XAID, SqlDbType.Int),
                //new SqlParameter(PARM_CAPUBND, SqlDbType.Int),
                //new SqlParameter(PARM_CAPTHANHTRA, SqlDbType.Int),
                new SqlParameter(PARM_SUDUNGPM, SqlDbType.Int),
                new SqlParameter(PARM_MACQ, SqlDbType.NVarChar,100),
                //new SqlParameter(PARM_SUDUNGQUYTRINH, SqlDbType.Bit),
                //new SqlParameter(PARM_SUDUNGQUYTRINHGQ, SqlDbType.Bit),
                //new SqlParameter(PARM_QUYTRINHVANTHUTIEPNHAN, SqlDbType.Bit),
                //new SqlParameter(PARM_QUYTRINHVANTHUTIEPDAN, SqlDbType.Bit),
                new SqlParameter(PARM_CQCO_HIEULUC, SqlDbType.Bit),
                new SqlParameter(PARM_WORKFLOWID, SqlDbType.Int),
                new SqlParameter(PARM_WFTIENHANHTTID, SqlDbType.Int)
            };
            return parms;
        }

        private void SetUpdateParms(SqlParameter[] parms, CoQuanInfo coQuanInfo)
        {
            parms[0].Value = coQuanInfo.CoQuanID;
            parms[1].Value = coQuanInfo.TenCoQuan;
            if (coQuanInfo.CoQuanChaID != 0)
            {
                parms[2].Value = coQuanInfo.CoQuanChaID;
            }
            else parms[2].Value = DBNull.Value;
            if (coQuanInfo.CapID == 0)
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = coQuanInfo.CapID;
            if (coQuanInfo.ThamQuyenID == 0)
                parms[4].Value = DBNull.Value;
            else
                parms[4].Value = coQuanInfo.ThamQuyenID;
            if (coQuanInfo.TinhID == 0)
                parms[5].Value = DBNull.Value;
            else
                parms[5].Value = coQuanInfo.TinhID;
            if (coQuanInfo.HuyenID == 0)
                parms[6].Value = DBNull.Value;
            else parms[6].Value = coQuanInfo.HuyenID;

            if (coQuanInfo.XaID == 0)
                parms[7].Value = DBNull.Value;
            else parms[7].Value = coQuanInfo.XaID;

            //parms[8].Value = coQuanInfo.CapUBND;
            //parms[9].Value = coQuanInfo.CapThanhTra;
            parms[8].Value = coQuanInfo.SuDungPM;
            parms[9].Value = coQuanInfo.MaCQ;
            //parms[12].Value = coQuanInfo.SuDungQuyTrinh;
            //parms[13].Value = coQuanInfo.SuDungQuyTrinhGQ;
            //parms[14].Value = coQuanInfo.QuyTrinhVanThuTiepNhan;
            //parms[15].Value = coQuanInfo.QTVanThuTiepDan;
            parms[10].Value = coQuanInfo.CQCoHieuLuc;
            parms[11].Value = coQuanInfo.WorkFlowID;
            parms[12].Value = coQuanInfo.WFTienHanhTTID;
        }

        public IList<CoQuanInfo> GetParents()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTS, null))
                {
                    while (dr.Read())
                    {
                        CoQuanInfo coQuanInfo = GetData(dr);
                        coQuans.Add(coQuanInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetChilrents()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_CHILRENTS, null))
                {
                    while (dr.Read())
                    {
                        CoQuanInfo coQuanInfo = GetData(dr);
                        coQuans.Add(coQuanInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetParents(int tinhID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter parm = new SqlParameter(PARM_TINHID, SqlDbType.Int);
            parm.Value = tinhID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTS_BY_TINH, parm))
                {
                    while (dr.Read())
                    {
                        CoQuanInfo coQuanInfo = GetData(dr);
                        coQuans.Add(coQuanInfo);
                    }
                    dr.Close();
                }
            }
            catch
            {
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetCoQuanDaGiaiQuyet()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_COQUAN_DA_GIAIQUYET, null))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        //check dia chi da ton tai
        private const string CHECK_EXISTS_COQUAN = @"DM_CoQuan_CheckExistsName";
        public Boolean checkExistsCoQuan(string tenCoQuan, int coQuanCha, int coQuanId)
        {
            bool valid = false;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENCOQUAN, SqlDbType.NVarChar),
                new SqlParameter(PARM_COQUANCHA, SqlDbType.Int),
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parameters[0].Value = tenCoQuan;
            parameters[1].Value = coQuanCha;
            parameters[2].Value = coQuanId;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, CHECK_EXISTS_COQUAN, parameters))
            {
                while (dr.Read())
                {
                    valid = Utils.GetInt32(dr["isExists"], 0) > 0 ? true : false;
                }
                dr.Close();
            }
            return valid;
        }

        public IList<CoQuanInfo> GetCoQuans()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL, null))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetCoQuanForAjax()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL_FOR_AJAX, null))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetDataForAjax(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetCoQuanByCoQuanID(int CoQuanID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parameters[0].Value = CoQuanID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETCOQUAN_BY_COQUANID_FOR_AJAX, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetDataForAjax(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        //Dia Chi Suggestion
        private const string COQUAN_SUGGESTION = @"DM_CoQuan_GetSuggestion";
        public IList<CoQuanInfo> GetCoQuanSuggestion(string tenCoQuan)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENCOQUAN, SqlDbType.NVarChar)
            };
            parameters[0].Value = tenCoQuan;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COQUAN_SUGGESTION, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        //Dia Chi Search
        private const string COQUAN_SEARCH = @"DM_CoQuan_GetCoQuan_Search";
        public IList<CoQuanInfo> GetCoQuanSearch(string keySearch)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENCOQUAN, SqlDbType.NVarChar)
            };
            parameters[0].Value = keySearch;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, COQUAN_SEARCH, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetDataSearch(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetCoQuanByParentID(int coQuanID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parameters[0].Value = coQuanID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_PARENTID, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetDataForAjax(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        //public IList<CoQuanInfo> GetCoQuanByCanBoID(int coQuanID)
        //{
        //    IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
        //    SqlParameter[] parameters = new SqlParameter[] {
        //        new SqlParameter(PARM_COQUANID, SqlDbType.Int) 
        //    };
        //    parameters[0].Value = coQuanID;
        //    using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_PARENTID, parameters))
        //    {
        //        while (dr.Read())
        //        {
        //            CoQuanInfo coQuanInfo = GetDataForAjax(dr);
        //            coQuans.Add(coQuanInfo);
        //        }
        //        dr.Close();
        //    }
        //    return coQuans;
        //}
        public IList<CoQuanInfo> GetListCoQuanGQbyID(int coQuanID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parameters[0].Value = coQuanID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_LIST_CQGQ, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetAllCoQuan()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_ALL_COQUAN, null))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public CoQuanInfo GetCoQuanByID(int coQuanID)
        {
            CoQuanInfo coQuanInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int)
            };
            parameters[0].Value = coQuanID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    coQuanInfo = GetData(dr);
                    //coQuanInfo.QTVanThuTiepDan = Utils.ConvertToBoolean(dr["QTVanThuTiepDan"], false);
                    //coQuanInfo.QuyTrinhVanThuTiepNhan = Utils.ConvertToBoolean(dr["QTVanThuTiepNhanDon"], false);
                    //coQuanInfo.SuDungQuyTrinh = Utils.ConvertToBoolean(dr["SuDungQuyTrinh"], false);
                    //coQuanInfo.SuDungQuyTrinhGQ = Utils.ConvertToBoolean(dr["SuDungQuyTrinhGQ"], false);
                    coQuanInfo.CQCoHieuLuc = Utils.ConvertToBoolean(dr["CQCoHieuLuc"], false);
                    coQuanInfo.WorkFlowID = Utils.ConvertToInt32(dr["WorkFlowID"], 0);
                    coQuanInfo.WFTienHanhTTID = Utils.ConvertToInt32(dr["WFTienHanhTTID"], 0);
                }
                dr.Close();
            }
            return coQuanInfo;
        }


        public CoQuanInfo GetCoQuanByCanBoID(int canBoID)
        {
            CoQuanInfo coQuanInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_CANBOID, SqlDbType.Int)
            };
            parameters[0].Value = canBoID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_COQUAN_BYCANBOID, parameters))
            {
                if (dr.Read())
                {
                    coQuanInfo = GetData(dr);
                    //coQuanInfo.QTVanThuTiepDan = Utils.ConvertToBoolean(dr["QTVanThuTiepDan"], false);
                    //coQuanInfo.QuyTrinhVanThuTiepNhan = Utils.ConvertToBoolean(dr["QTVanThuTiepNhanDon"], false);
                    //coQuanInfo.SuDungQuyTrinh = Utils.ConvertToBoolean(dr["SuDungQuyTrinh"], false);
                    //coQuanInfo.SuDungQuyTrinhGQ = Utils.ConvertToBoolean(dr["SuDungQuyTrinhGQ"], false);
                    coQuanInfo.CQCoHieuLuc = Utils.ConvertToBoolean(dr["CQCoHieuLuc"], false);
                }
                dr.Close();
            }
            return coQuanInfo;
        }

        public int Delete(int coQuanID)
        {
            //object val = 0;
            int result = 0;
            SqlParameter[] parameters = new SqlParameter[] {
new SqlParameter(PARM_COQUANID, SqlDbType.Int) };
            parameters[0].Value = coQuanID;
            using (SqlConnection conn = new SqlConnection(SQLHelper.CONN_BACKEND))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //val = SQLHelper.ExecuteScalar(trans, CommandType.StoredProcedure, DELETE, parameters);
                        result = SQLHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, DELETE, parameters);
                        if (result > 0)
                            trans.Commit();
                        else
                            trans.Rollback();
                    }
                    catch
                    {
                        trans.Rollback();
                        //throw;
                        result = -2;
                    }
                }
                conn.Close();
            }
            //return Utils.ConvertToInt32(val, -1);
            return result;
        }

        public int Update(CoQuanInfo coQuanInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, coQuanInfo);
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


        public int Insert(CoQuanInfo coQuanInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, coQuanInfo);
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

        public IList<CoQuanInfo> GetCoQuanByCap(int capID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_CAP_ID, SqlDbType.Int)
            };
            parameters[0].Value = capID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_CAP, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetAllHaveNull()
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL_HAVE_NULL, null))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<WorkFlowInfo> GetAllWorkFlow()
        {
            IList<WorkFlowInfo> workFlowInfos = new List<WorkFlowInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GETALL_WORKFLOW, null))
            {
                while (dr.Read())
                {
                    WorkFlowInfo workFlowInfo = new WorkFlowInfo();
                    workFlowInfo.WorkFlowID = Utils.ConvertToInt32(dr["WorkFlowID"], 0);
                    workFlowInfo.WorkFlowName = Utils.ConvertToString(dr["WorkFlowName"], String.Empty);
                    workFlowInfo.WorkFlowCode = Utils.ConvertToString(dr["WorkFlowCode"], String.Empty);
                    workFlowInfos.Add(workFlowInfo);
                }
                dr.Close();
            }
            return workFlowInfos;
        }


        private const string SELECT_BY_TINHID = @"DM_CoQuan_GetByTinhID";
        public IList<CoQuanInfo> GetCoQuanByTinhID(int tinhID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TINHID, SqlDbType.Int)
            };
            parameters[0].Value = tinhID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_TINHID, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetByHuyen(int huyenID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_HUYENID, SqlDbType.Int)
            };
            parameters[0].Value = huyenID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_HUYEN, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = GetData(dr);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }


        // anhnt
        private const string PARM_SU_DUNG_PHAM_MEM = @"pSuDungPhamMem";
        private const string SELECT_BY_PARENTID_AND_SUDUNGPHAMMEM = @"DM_CoQuan_GetByParentID_And_SuDungPhanMem";
        public IList<CoQuanInfo> GetCoQuanByParentIDAnSuDungPhanMem(int coQuanID, int suDungPhanMem)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_COQUANID, SqlDbType.Int) ,
                new SqlParameter(PARM_SU_DUNG_PHAM_MEM,SqlDbType.Int)
            };
            parameters[0].Value = coQuanID;
            parameters[1].Value = suDungPhanMem;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_PARENTID_AND_SUDUNGPHAMMEM, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = new CoQuanInfo();
                    coQuanInfo.CoQuanID = Utils.GetInt32(dr["CoQuanID"], 0);
                    coQuanInfo.TenCoQuan = Utils.GetString(dr["TenCoQuan"], String.Empty);

                    coQuanInfo.CoQuanChaID = Utils.GetInt32(dr["CoQuanChaID"], 0);
                    //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
                    coQuanInfo.CapID = Utils.GetInt32(dr["CapID"], 0);
                    coQuanInfo.ThamQuyenID = Utils.GetInt32(dr["ThamQuyenID"], 0);
                    coQuanInfo.TinhID = Utils.GetInt32(dr["TinhID"], 0);
                    coQuanInfo.HuyenID = Utils.GetInt32(dr["HuyenID"], 0);
                    coQuanInfo.XaID = Utils.GetInt32(dr["XaID"], 0);

                    //coQuanInfo.CapUBND = Utils.ConvertToBoolean(rdr["CapUBND"], false);
                    //coQuanInfo.CapThanhTra = Utils.ConvertToBoolean(rdr["CapThanhTra"], false);
                    coQuanInfo.SuDungPM = Utils.ConvertToBoolean(dr["SuDungPM"], false);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        //private const string SELECT_BY_PARENTID_AND_HUYENID = @"DM_CoQuan_GetByParentID_And_HuyenID";
        //public IList<CoQuanInfo> GetCoQuanByParentIDAndHuyenID(int coQuanChaID, int huyenID,int namTimKiem)
        //{
        //    IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
        //    SqlParameter[] parameters = new SqlParameter[] {
        //        new SqlParameter(PARM_COQUANCHAID, SqlDbType.Int) ,
        //        new SqlParameter(PARM_HUYENID,SqlDbType.Int),
        //    };
        //    parameters[0].Value = coQuanChaID;
        //    parameters[1].Value = huyenID;
        //    using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_PARENTID_AND_HUYENID, parameters))
        //    {
        //        while (dr.Read())
        //        {
        //            CoQuanInfo coQuanInfo = new CoQuanInfo();
        //            coQuanInfo.CoQuanID = Utils.GetInt32(dr["CoQuanID"], 0);
        //            coQuanInfo.TenCoQuan = Utils.GetString(dr["TenCoQuan"], String.Empty);

        //            coQuanInfo.CoQuanChaID = Utils.GetInt32(dr["CoQuanChaID"], 0);
        //            //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
        //            coQuanInfo.CapID = Utils.GetInt32(dr["CapID"], 0);
        //            coQuanInfo.ThamQuyenID = Utils.GetInt32(dr["ThamQuyenID"], 0);
        //            coQuanInfo.TinhID = Utils.GetInt32(dr["TinhID"], 0);
        //            coQuanInfo.HuyenID = Utils.GetInt32(dr["HuyenID"], 0);
        //            coQuanInfo.XaID = Utils.GetInt32(dr["XaID"], 0);

        //            coQuanInfo.SuDungPM = Utils.ConvertToBoolean(dr["SuDungPM"], false);

        //            // ke hoach thanh tra
        //            string KeHoachThanhTraID = string.Empty;
        //            string KeHoachThanhTraHanhChinhID = string.Empty;
        //            string KeHoachThanhTraChuyenNganhID = string.Empty;
        //            #region
        //            if (coQuanInfo.CoQuanID != 0)
        //            {
        //                coQuanInfo.PhanLoaiThanhTraID = 0;
        //                IList<TongHopKeHoachThanhTraInfo> info = new DAL.NVCongTacThanhTra.TongHopKeHoachThanhTra().GetCuocThanhTraByCoQuanID(coQuanInfo.CoQuanID, namTimKiem);
        //                if (info != null)
        //                {
        //                    coQuanInfo.TongSoVuViec = Utils.ConvertToInt32(info.Count, 0);
        //                    IList infoTTHanhChinh = new List<TongHopKeHoachThanhTraInfo>();
        //                    IList infoTTChuyenNganh = new List<TongHopKeHoachThanhTraInfo>();
        //                    for (int i = 0; i < info.Count; i++)
        //                    {
        //                        KeHoachThanhTraID += info[i].KeHoachThanhTraID + ",";
        //                        if (info[i].PhanLoaiThanhTraID1 == 1)
        //                        {
        //                            // 1 la thanh tra hanh chinh
        //                            infoTTHanhChinh.Add(info[i]);
        //                            KeHoachThanhTraHanhChinhID += info[i].KeHoachThanhTraID + ",";
        //                            coQuanInfo.PhanLoaiThanhTraHanhChinhID = info[i].PhanLoaiThanhTraID1;
        //                        }
        //                        else
        //                        {
        //                            // 26 thanh tra chuyen nganh
        //                            if (info[i].PhanLoaiThanhTraID1 == 26)
        //                            {
        //                                infoTTChuyenNganh.Add(info[i]);
        //                                KeHoachThanhTraChuyenNganhID += info[i].KeHoachThanhTraID + ",";
        //                                coQuanInfo.PhanLoaiThanhTraChuyenNganhID = info[i].PhanLoaiThanhTraID1;
        //                            }

        //                        }
        //                    }

        //                    if (infoTTChuyenNganh != null)
        //                    {
        //                        coQuanInfo.TongSoVuViecChuyenNghanh = Utils.ConvertToInt32(infoTTChuyenNganh.Count, 0);
        //                    }
        //                    if (infoTTHanhChinh != null)
        //                    {
        //                        coQuanInfo.TongSoVuViecHanhChinh = Utils.ConvertToInt32(infoTTHanhChinh.Count, 0);
        //                    }
        //                    coQuanInfo.TongSoVuViecChongCheo = Utils.ConvertToInt32(infoTTHanhChinh.Count, 0);
        //                    if (KeHoachThanhTraID != string.Empty)
        //                    {
        //                        KeHoachThanhTraID = KeHoachThanhTraID.Substring(0, KeHoachThanhTraID.Length - 1);

        //                        coQuanInfo.KeHoachThanhTraIDString = Utils.ConvertToString(KeHoachThanhTraID, string.Empty);
        //                    }

        //                    if (KeHoachThanhTraChuyenNganhID != string.Empty)
        //                    {
        //                        KeHoachThanhTraChuyenNganhID = KeHoachThanhTraChuyenNganhID.Substring(0, KeHoachThanhTraChuyenNganhID.Length - 1);

        //                        coQuanInfo.KeHoachThanhTraChuyenNganhIDString = Utils.ConvertToString(KeHoachThanhTraChuyenNganhID, string.Empty);
        //                    }

        //                    if (KeHoachThanhTraHanhChinhID != string.Empty)
        //                    {
        //                        KeHoachThanhTraHanhChinhID = KeHoachThanhTraHanhChinhID.Substring(0, KeHoachThanhTraHanhChinhID.Length - 1);

        //                        coQuanInfo.KeHoachThanhTraHanhChinhIDString = Utils.ConvertToString(KeHoachThanhTraHanhChinhID, string.Empty);
        //                    }
        //                }
        //            }
        //            #endregion

        //            coQuans.Add(coQuanInfo);
        //        }
        //        dr.Close();
        //    }
        //    return coQuans;
        //}

        //private const string PARM_COQUANCHAID = @"pCoQuanChaID";
        //private const string SELECT_SEARCH_PARENTID_AND_HUYENID_COQUANID = @"DM_CoQuan_Search_GetByParentID_And_HuyenID_CoQuanID";
        //public IList<CoQuanInfo> GetCoQuan_Search_ParentIDAndHuyenID_CoQuanID(int coQuanChaID, int huyenID, int coQuanID, int namTimKiem)
        //{
        //    IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
        //    SqlParameter[] parameters = new SqlParameter[] {
        //        new SqlParameter(PARM_COQUANCHAID, SqlDbType.Int) ,
        //        new SqlParameter(PARM_HUYENID,SqlDbType.Int),
        //        new SqlParameter(PARM_COQUANID,SqlDbType.Int),
        //    };
        //    parameters[0].Value = coQuanChaID;
        //    parameters[1].Value = huyenID;
        //    parameters[2].Value = coQuanID;
        //    using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_SEARCH_PARENTID_AND_HUYENID_COQUANID, parameters))
        //    {
        //        while (dr.Read())
        //        {
        //            CoQuanInfo coQuanInfo = new CoQuanInfo();
        //            coQuanInfo.CoQuanID = Utils.GetInt32(dr["CoQuanID"], 0);
        //            coQuanInfo.TenCoQuan = Utils.GetString(dr["TenCoQuan"], String.Empty);

        //            coQuanInfo.CoQuanChaID = Utils.GetInt32(dr["CoQuanChaID"], 0);
        //            //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
        //            coQuanInfo.CapID = Utils.GetInt32(dr["CapID"], 0);
        //            coQuanInfo.ThamQuyenID = Utils.GetInt32(dr["ThamQuyenID"], 0);
        //            coQuanInfo.TinhID = Utils.GetInt32(dr["TinhID"], 0);
        //            coQuanInfo.HuyenID = Utils.GetInt32(dr["HuyenID"], 0);
        //            coQuanInfo.XaID = Utils.GetInt32(dr["XaID"], 0);
        //            coQuanInfo.SuDungPM = Utils.ConvertToBoolean(dr["SuDungPM"], false);

        //            // ke hoach thanh tra 
        //            string KeHoachThanhTraID = string.Empty;
        //            #region
        //            if (coQuanInfo.CoQuanID != 0)
        //            {
        //                IList<TongHopKeHoachThanhTraInfo> info = new DAL.NVCongTacThanhTra.TongHopKeHoachThanhTra().GetCuocThanhTraByCoQuanID(coQuanInfo.CoQuanID, namTimKiem);
        //                if (info != null)
        //                {
        //                    coQuanInfo.TongSoVuViec = Utils.ConvertToInt32(info.Count, 0);
        //                    IList infoTTHanhChinh = new List<TongHopKeHoachThanhTraInfo>();
        //                    IList infoTTChuyenNganh = new List<TongHopKeHoachThanhTraInfo>();
        //                    for (int i = 0; i < info.Count; i++)
        //                    {
        //                        KeHoachThanhTraID += info[i].KeHoachThanhTraID + ",";

        //                        if (info[i].PhanLoaiThanhTraID1 == 1)
        //                        {
        //                            // 1 la thanh tra hanh chinh
        //                            infoTTHanhChinh.Add(info[i]);
        //                        }
        //                        else
        //                        {
        //                            // 26 thanh tra chuyen nganh
        //                            if (info[i].PhanLoaiThanhTraID1 == 26)
        //                                infoTTChuyenNganh.Add(info[i]);
        //                        }
        //                    }

        //                    if (infoTTChuyenNganh != null)
        //                    {
        //                        coQuanInfo.TongSoVuViecChuyenNghanh = Utils.ConvertToInt32(infoTTChuyenNganh.Count, 0);
        //                    }
        //                    if (infoTTHanhChinh != null)
        //                    {
        //                        coQuanInfo.TongSoVuViecHanhChinh = Utils.ConvertToInt32(infoTTHanhChinh.Count, 0);
        //                    }
        //                    coQuanInfo.TongSoVuViecChongCheo = Utils.ConvertToInt32(infoTTHanhChinh.Count, 0);

        //                    if(KeHoachThanhTraID != string.Empty)
        //                    {
        //                        KeHoachThanhTraID = KeHoachThanhTraID.Substring(0, KeHoachThanhTraID.Length - 1);

        //                        coQuanInfo.KeHoachThanhTraIDString = Utils.ConvertToString(KeHoachThanhTraID,string.Empty);
        //                    }
        //                }
        //            }
        //            #endregion


        //            coQuans.Add(coQuanInfo);
        //        }
        //        dr.Close();
        //    }
        //    return coQuans;
        //}


        private const string SELECT_BY_CAPID_ANDSUDUNGPHANMEM = @"DM_CoQuan_GetBy_CapID_AndSuDungPhanMem";
        public IList<CoQuanInfo> GetCoQuan_Search_ParentIDAndHuyenID_CoQuanID(int suDungPhanMem)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_SUDUNGPM,SqlDbType.Int),
            };
            parameters[0].Value = suDungPhanMem;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_CAPID_ANDSUDUNGPHANMEM, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = new CoQuanInfo();
                    coQuanInfo.CoQuanID = Utils.GetInt32(dr["CoQuanID"], 0);
                    coQuanInfo.TenCoQuan = Utils.GetString(dr["TenCoQuan"], String.Empty);

                    coQuanInfo.CoQuanChaID = Utils.GetInt32(dr["CoQuanChaID"], 0);
                    //coQuanInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);
                    coQuanInfo.CapID = Utils.GetInt32(dr["CapID"], 0);
                    coQuanInfo.ThamQuyenID = Utils.GetInt32(dr["ThamQuyenID"], 0);
                    coQuanInfo.TinhID = Utils.GetInt32(dr["TinhID"], 0);
                    coQuanInfo.HuyenID = Utils.GetInt32(dr["HuyenID"], 0);
                    coQuanInfo.XaID = Utils.GetInt32(dr["XaID"], 0);

                    //coQuanInfo.CapUBND = Utils.ConvertToBoolean(rdr["CapUBND"], false);
                    //coQuanInfo.CapThanhTra = Utils.ConvertToBoolean(rdr["CapThanhTra"], false);
                    coQuanInfo.SuDungPM = Utils.ConvertToBoolean(dr["SuDungPM"], false);
                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }

        public IList<CoQuanInfo> GetCoQuanTreeView(int CoQuanID)
        {
            IList<CoQuanInfo> coQuans = new List<CoQuanInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@CoQuanID",SqlDbType.Int),
            };
            parameters[0].Value = CoQuanID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_TREE_VIEW_BY_ID, parameters))
            {
                while (dr.Read())
                {
                    CoQuanInfo coQuanInfo = new CoQuanInfo();
                    coQuanInfo.CoQuanID = Utils.GetInt32(dr["CoQuanID"], 0);
                    coQuanInfo.TenCoQuan = Utils.GetString(dr["TenCoQuan"], String.Empty);

                    coQuanInfo.CoQuanChaID = Utils.GetInt32(dr["CoQuanChaID"], 0);
                    coQuanInfo.CapID = Utils.GetInt32(dr["CapID"], 0);

                    coQuans.Add(coQuanInfo);
                }
                dr.Close();
            }
            return coQuans;
        }
    }
}
