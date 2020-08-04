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
    public class DiaChi
    {
        #region Database query string

        private const string SELECT_ALL = @"DM_DiaChi_GetAll";
        private const string SELECT_ALL_FOR_AJAX = @"DM_DiaChi_GetAllForAjax";
        private const string SELECT_BY_ID = @"DM_DiaChi_GetByID";
        private const string SELECT_BY_PARENTID = @"DM_DiaChi_GetByParentID";
        private const string DELETE = @"DM_DiaChi_Delete";
        private const string UPDATE = @"DM_DiaChi_Update";
        private const string INSERT = @"DM_DiaChi_Insert";

        #endregion

        #region paramaters constant

        private const string PARM_DIACHIID = @"DiaChiID";
        private const string PARM_TENDIACHI = @"TenDiaChi";
        private const string PARM_DIACHICHA = @"DiaChiChaID";
        private const string PARM_CAP = @"Cap";
        private const string PARM_TENDAYDU = @"TenDayDu";
        //private const string PARM_VALID = @"Valid";

        #endregion

        private DiaChiInfo GetData(SqlDataReader rdr)
        {
            DiaChiInfo diachiInfo = new DiaChiInfo();
            diachiInfo.DiaChiID = Utils.GetInt32(rdr["DiaChiID"], 0);
            diachiInfo.TenDiaChi = Utils.GetString(rdr["TenDiaChi"], String.Empty);

            diachiInfo.DiaChiCha = Utils.GetInt32(rdr["DiaChiChaID"], 0);
            diachiInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);

            diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
            //diachiInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return diachiInfo;
        }

        //get data for search
        private DiaChiInfo GetDataSearch(SqlDataReader rdr)
        {
            DiaChiInfo diachiInfo = new DiaChiInfo();
            diachiInfo.DiaChiID = Utils.GetInt32(rdr["DiaChiID"], 0);
            return diachiInfo;
        }

        private DiaChiInfo GetDataByLevel(SqlDataReader rdr, int level)
        {
            DiaChiInfo diachiInfo = new DiaChiInfo();
            switch (level)
            {
                case 1:
                    diachiInfo.DiaChiID = Utils.GetInt32(rdr["TinhID"], 0);
                    diachiInfo.TenDiaChi = Utils.GetString(rdr["TenTinh"], String.Empty);

                    diachiInfo.DiaChiCha = 0;//Utils.GetInt32(rdr["DiaChiChaID"], 0);
                    diachiInfo.Cap = 1;//Utils.GetInt32(rdr["Cap"], 0);

                    diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
                    break;
                case 2:
                    diachiInfo.DiaChiID = Utils.GetInt32(rdr["HuyenID"], 0);
                    diachiInfo.TenDiaChi = Utils.GetString(rdr["TenHuyen"], String.Empty);

                    diachiInfo.DiaChiCha = Utils.GetInt32(rdr["TinhID"], 0);
                    diachiInfo.Cap = 2;//Utils.GetInt32(rdr["Cap"], 0);

                    diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
                    break;
                case 3:
                    diachiInfo.DiaChiID = Utils.GetInt32(rdr["XaID"], 0);
                    diachiInfo.TenDiaChi = Utils.GetString(rdr["TenXa"], String.Empty);

                    diachiInfo.DiaChiCha = Utils.GetInt32(rdr["HuyenID"], 0);
                    diachiInfo.Cap = 3;//Utils.GetInt32(rdr["Cap"], 0);

                    diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
                    break;
            }
            //diachiInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return diachiInfo;
        }
        //get data cap 1
        private DiaChiInfo GetDataTinh(SqlDataReader rdr)
        {
            //DiaChiInfo diachiInfo = new DiaChiInfo();
            //diachiInfo.TinhID = Utils.GetInt32(rdr["TinhID"], 0);
            //diachiInfo.TenTinh = Utils.GetString(rdr["TenTinh"], String.Empty);

            //diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);


            DiaChiInfo diachiInfo = new DiaChiInfo();
            diachiInfo.DiaChiID = Utils.GetInt32(rdr["TinhID"], 0);
            diachiInfo.TenDiaChi = Utils.GetString(rdr["TenTinh"], String.Empty);

            diachiInfo.DiaChiCha = 0;// Utils.GetInt32(rdr["DiaChiChaID"], 0);
            diachiInfo.Cap = 1;// Utils.GetInt32(rdr["Cap"], 0);

            diachiInfo.hasChild = Utils.GetInt32(rdr["hasChild"], 0);

            diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
            //diachiInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return diachiInfo;
        }

        //get data cap 3
        private DiaChiInfo GetDataHuyen(SqlDataReader rdr)
        {

            DiaChiInfo diachiInfo = new DiaChiInfo();
            diachiInfo.DiaChiID = Utils.GetInt32(rdr["HuyenID"], 0);
            diachiInfo.TenDiaChi = Utils.GetString(rdr["TenHuyen"], String.Empty);

            diachiInfo.DiaChiCha = Utils.GetInt32(rdr["TinhID"], 0);
            diachiInfo.Cap = 2;// Utils.GetInt32(rdr["Cap"], 0);

            diachiInfo.hasChild = Utils.GetInt32(rdr["hasChild"], 0);

            diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
            //diachiInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return diachiInfo;
        }

        //get data cap 3
        private DiaChiInfo GetDataXa(SqlDataReader rdr)
        {

            DiaChiInfo diachiInfo = new DiaChiInfo();
            diachiInfo.DiaChiID = Utils.GetInt32(rdr["XaID"], 0);
            diachiInfo.TenDiaChi = Utils.GetString(rdr["TenXa"], String.Empty);

            diachiInfo.DiaChiCha = Utils.GetInt32(rdr["HuyenID"], 0);
            diachiInfo.Cap = 3;// Utils.GetInt32(rdr["Cap"], 0);

            diachiInfo.hasChild = 0;//Utils.GetInt32(rdr["hasChild"], 0);

            diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);
            //diachiInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return diachiInfo;
        }

        private DiaChiInfo GetDataForAjax(SqlDataReader rdr)
        {
            DiaChiInfo diachiInfo = new DiaChiInfo();
            diachiInfo.DiaChiID = Utils.GetInt32(rdr["DiaChiID"], 0);
            diachiInfo.TenDiaChi = Utils.GetString(rdr["TenDiaChi"], String.Empty);

            diachiInfo.DiaChiCha = Utils.GetInt32(rdr["DiaChiChaID"], 0);
            diachiInfo.Cap = Utils.GetInt32(rdr["Cap"], 0);

            diachiInfo.TenDayDu = Utils.GetString(rdr["TenDayDu"], String.Empty);

            diachiInfo.hasChild = Utils.GetInt32(rdr["hasChild"], 0);

            //diachiInfo.Valid = Utils.GetInt32(rdr["Valid"], 0);
            return diachiInfo;
        }

        private SqlParameter[] GetInsertParms()
        {
            SqlParameter[] parms = new SqlParameter[] {
                //new SqlParameter(PARM_DIACHIID, SqlDbType.Int),
                new SqlParameter(PARM_TENDIACHI, SqlDbType.NVarChar, 200),
                new SqlParameter(PARM_DIACHICHA, SqlDbType.Int),
                new SqlParameter(PARM_CAP, SqlDbType.Int),
                new SqlParameter(PARM_TENDAYDU, SqlDbType.NVarChar, 150)
                
            };
            return parms;
        }

        private void SetInsertParms(SqlParameter[] parms, DiaChiInfo diachiInfo)
        {
            int index = 0;

            foreach (PropertyInfo proInfo in diachiInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && (proInfo.Name != "DiaChiID" && proInfo.Name != "hasChild"))
                {
                    parms[index].Value = proInfo.GetValue(diachiInfo, null);
                    index++;
                }
            }
        }

        private SqlParameter[] GetUpdateParms()
        {
            List<SqlParameter> parms = GetInsertParms().ToList();
            parms.Insert(0, new SqlParameter(PARM_DIACHIID, SqlDbType.Int));
            return parms.ToArray();
        }

        private void SetUpdateParms(SqlParameter[] parms, DiaChiInfo diachiInfo)
        {
            int index = 0;
            foreach (PropertyInfo proInfo in diachiInfo.GetType().GetProperties())
            {
                if (proInfo.CanRead && proInfo.Name != "hasChild")
                {
                    parms[index].Value = proInfo.GetValue(diachiInfo, null);
                    index++;
                }
            }
        }

        //check dia chi da ton tai
        private const string CHECK_EXISTS_DIACHI = @"DM_DiaChi_CheckExistsName";
        public Boolean checkExistsDiaChi(int diachiId, string tenDiaChi, int diaChiCha, int level,string tendaydu)
        {
            bool valid = false;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENDIACHI, SqlDbType.NVarChar),
                new SqlParameter(PARM_DIACHICHA, SqlDbType.Int),
                new SqlParameter(PARM_CAP, SqlDbType.Int),
                new SqlParameter(PARM_DIACHIID, SqlDbType.Int),
                new SqlParameter(PARM_TENDAYDU,SqlDbType.NVarChar,200)
            };
            parameters[0].Value = tenDiaChi;
            parameters[1].Value = diaChiCha;
            parameters[2].Value = level;
            parameters[3].Value = diachiId;
            parameters[4].Value = tendaydu;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, CHECK_EXISTS_DIACHI, parameters))
            {
                while (dr.Read())
                {
                    valid = Utils.GetInt32(dr["isExists"], 0) > 0 ? true : false;
                }
                dr.Close();
            }
            return valid;
        }

        public IList<DiaChiInfo> GetDiaChis()
        {
            IList<DiaChiInfo> diachis = new List<DiaChiInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL, null))
            {
                while (dr.Read())
                {
                    DiaChiInfo diachiInfo = GetData(dr);
                    diachis.Add(diachiInfo);
                }
                dr.Close();
            }
            return diachis;
        }

        //get data tinh
        public IList<DiaChiInfo> GetDiaChiForAjax()
        {
            IList<DiaChiInfo> diachis = new List<DiaChiInfo>();
            //SqlParameter[] parameters = new SqlParameter[] {
            //    new SqlParameter(PARM_TENDIACHI, SqlDbType.NVarChar,50),
            //};
            //parameters[0].Value = keyword;
            //if (keyword == null)
            //{
            //    parameters[0].Value = DBNull.Value;
            //}
           
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_ALL_FOR_AJAX, null))
            {
                while (dr.Read())
                {
                    DiaChiInfo diachiInfo = GetDataTinh(dr);
                    diachis.Add(diachiInfo);
                }
                dr.Close();
            }
            return diachis;
        }

        //Dia Chi Suggestion
        private const string DIACHI_SUGGESTION = @"DM_DiaChi_GetSuggestion";
        public IList<DiaChiInfo> GetDiaChiSuggestion(string tenDiaChi)
        {
            IList<DiaChiInfo> diachis = new List<DiaChiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENDIACHI, SqlDbType.NVarChar) 
            };
            parameters[0].Value = tenDiaChi;

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, DIACHI_SUGGESTION, parameters))
            {
                while (dr.Read())
                {
                    DiaChiInfo diachiInfo = GetData(dr);
                    diachis.Add(diachiInfo);
                }
                dr.Close();
            }
            return diachis;
        }

        //Dia Chi Search
        private const string DIACHI_SEARCH = @"DM_DiaChi_GetDiaChi_Search";
        public IList<DiaChiInfo> GetDiaChiSearch(string keySearch)
        {
            IList<DiaChiInfo> diachis = new List<DiaChiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_TENDIACHI, SqlDbType.NVarChar,50) 
            };
            parameters[0].Value = keySearch;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, DIACHI_SEARCH, parameters))
                {
                    while (dr.Read())
                    {
                        DiaChiInfo diachiInfo = GetDataSearch(dr);
                        diachis.Add(diachiInfo);
                    }
                    dr.Close();
                }
            }
            catch 
            {
           
            }
           
            return diachis;
        }

        public IList<DiaChiInfo> GetDiaChiByParentID(int diachiID, int level)
        {
            IList<DiaChiInfo> diachis = new List<DiaChiInfo>();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_DIACHIID, SqlDbType.Int),
                new SqlParameter(PARM_CAP, SqlDbType.Int),
            };
            parameters[0].Value = diachiID;
            parameters[1].Value = level;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_PARENTID, parameters))
            {
                while (dr.Read())
                {
                    DiaChiInfo diachiInfo = new DiaChiInfo();
                    if (level == 1)
                        diachiInfo = GetDataHuyen(dr);
                    else
                        diachiInfo = GetDataXa(dr);

                    diachis.Add(diachiInfo);
                }
                dr.Close();
            }
            return diachis;
        }

        public DiaChiInfo GetDiaChiByID(int diachiID, int level)
        {
            DiaChiInfo diachiInfo = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_DIACHIID, SqlDbType.Int),
                new SqlParameter(PARM_CAP, SqlDbType.Int)
            };
            parameters[0].Value = diachiID;
            parameters[1].Value = level;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, SELECT_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    diachiInfo = GetDataByLevel(dr, level);
                }
                dr.Close();
            }
            return diachiInfo;
        }

        public int Delete(int diachiID, int level)
        {
            object val;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_DIACHIID, SqlDbType.Int),
                new SqlParameter(PARM_CAP, SqlDbType.Int) 
            };
            parameters[0].Value = diachiID;
            parameters[1].Value = level;
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

        public int Update(DiaChiInfo diachiInfo)
        {
            object val = 0;
            SqlParameter[] parameters = GetUpdateParms();
            SetUpdateParms(parameters, diachiInfo);
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


        public int Insert(DiaChiInfo diachiInfo)
        {
            object val = null;
            SqlParameter[] parameters = GetInsertParms();
            SetInsertParms(parameters, diachiInfo);
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
