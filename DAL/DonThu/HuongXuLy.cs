using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Com.Gosol.CMS.DAL.DonThu
{
    public class HuongXuLy
    {
        private HuongXuLyInfo GetData(SqlDataReader rdr)
        {
            HuongXuLyInfo info = new HuongXuLyInfo();
            info.HuongXuLyID = Utils.GetInt32(rdr["HuongXuLyID"], 0);
            info.TenHuongXuLy = Utils.GetString(rdr["TenHuongXuLy"], String.Empty);
            return info;
        }

        public List<HuongXuLyInfo> GetAll()
        {
            List<HuongXuLyInfo> list = new List<HuongXuLyInfo>();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, "HuongXuLy_GetAll", null))
                {
                    while (dr.Read())
                    {
                        HuongXuLyInfo info = GetData(dr);
                        list.Add(info);
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return list;
        }
    }
}
