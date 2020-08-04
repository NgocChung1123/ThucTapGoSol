using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for GetXaByHuyen
    /// </summary>
    public class GetXaByHuyen : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int huyenID = Utils.ConvertToInt32(context.Request.QueryString["huyenID"], 0);

            List<XaInfo> xaList = new DAL.Xa().GetByHuyen(huyenID).ToList();

            String data = String.Empty;
            data += "<option value='0'>Chọn xã</option>";
            foreach (XaInfo xaInfo in xaList)
            {
                data += "<option value='" + xaInfo.XaID + "'>" + xaInfo.TenXa + "</option>";
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