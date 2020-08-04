using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web.Handler.NotifyHandler
{
    /// <summary>
    /// Summary description for CountDuyetGQ
    /// </summary>
    public class CountDuyetGQ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int coQuanID = Utils.ConvertToInt32(context.Request.Params["coQuanID"], 0);
            int roleID = Utils.ConvertToInt32(context.Request.Params["roleID"], 0);
            int canBoID = Utils.ConvertToInt32(context.Request.Params["userID"], 0);

            if (coQuanID != 0)
            {
                DAL.Notify.Notify notify = new DAL.Notify.Notify();
                //int cbThucHienID = IdentityHelper.GetCanBoID();
                string countNotity = "";
                try
                {
                    if (roleID == 1)
                        countNotity = notify.Count_DTCanDuyetGQ(coQuanID).ToString();
                    if (roleID == 2)
                        countNotity = notify.Count_DTCanDuyetGQ_TP(coQuanID, canBoID).ToString();
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