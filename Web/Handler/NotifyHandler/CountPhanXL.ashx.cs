using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web.Handler.Notify
{
    /// <summary>
    /// Summary description for CountPhanXL
    /// </summary>
    public class CountPhanXL : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int userID = Utils.ConvertToInt32(context.Request.Params["userID"], 0);
            int coQuanID = Utils.ConvertToInt32(context.Request.Params["coQuanID"], 0);
            int roleID = Utils.ConvertToInt32(context.Request.Params["roleID"], 0);
            int phongBanID = Utils.ConvertToInt32(context.Request.Params["phongBanID"], 0);

            if (userID != 0 && coQuanID != 0)
            {
                DAL.Notify.Notify notify = new DAL.Notify.Notify();
                //int cbThucHienID = IdentityHelper.GetCanBoID();
                string countNotity = "";
                try
                {

                    countNotity = notify.Count_DTCanPhanXL(userID, coQuanID, roleID, phongBanID).ToString();
                }
                catch
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