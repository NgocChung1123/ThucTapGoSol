using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for GetHuyenByTinh
    /// </summary>
    public class GetHuyenByTinh : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int tinhID = Utils.ConvertToInt32(context.Request.QueryString["tinhID"], 0);

            List<HuyenInfo> huyenList = new DAL.Huyen().GetByTinh(tinhID).ToList();
            String data = string.Empty;

            data += "<option value='0'>Chọn huyện</option>";
            foreach (HuyenInfo huyenInfo in huyenList)
            {
                data += "<option value='" + huyenInfo.HuyenID + "'>" + huyenInfo.TenHuyen + "</option>";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(data);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}