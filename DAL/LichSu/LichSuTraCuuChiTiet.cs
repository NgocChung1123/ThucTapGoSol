using Com.Gosol.CMS.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Com.Gosol.CMS.DAL.LichSu
{
    public class LichSuTraCuuChiTiet
    {
        private const string INSERT = @"LichSuTraCuu_ChiTiet_Insert";

        private const string PARM_LICHSUTRACUUID = @"LichSuTraCuuID";
        private const string PARM_NGAYTRACUU = @"NgayTraCuu";
        private const string PARM_TRANGTHAIDONTHU = @"TrangThaiDonThu";

        public int Insert(int lichSuTraCuuID, string trangThaiDonThu)
        {
            object val = null;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(PARM_LICHSUTRACUUID, SqlDbType.Int),
                new SqlParameter(PARM_NGAYTRACUU, SqlDbType.DateTime),
                new SqlParameter(PARM_TRANGTHAIDONTHU, SqlDbType.NVarChar),
            };
            parameters[0].Value = lichSuTraCuuID;
            parameters[1].Value = DateTime.Now.Date;
            parameters[2].Value = trangThaiDonThu;
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
    }
}
