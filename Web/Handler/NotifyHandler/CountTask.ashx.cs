using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web.Handler.NotifyHandler
{
    /// <summary>
    /// Summary description for CountTask
    /// </summary>
    public class CountTask : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int coQuanID = Utils.ConvertToInt32(context.Request.Params["coQuanID"], 0);
            int roleID = Utils.ConvertToInt32(context.Request.Params["roleID"], 0);
            int phongBanID = Utils.ConvertToInt32(context.Request.Params["phongBanID"], 0);

            if (coQuanID != 0)
            {
                DAL.Notify.Notify notify = new DAL.Notify.Notify();
                //int cbThucHienID = IdentityHelper.GetCanBoID();
                string countNotity = "";
                try
                {

                    countNotity = notify.Count_NhiemVu(coQuanID, roleID, phongBanID).ToString();
                }
                catch (Exception ex)
                {
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                var resultStr = js.Serialize(countNotity);
                context.Response.Write(resultStr);
            }
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