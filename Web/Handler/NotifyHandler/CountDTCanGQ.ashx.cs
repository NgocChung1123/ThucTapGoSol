using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web.Handler.NotifyHandler
{
    /// <summary>
    /// Summary description for CountDTCanGQ
    /// </summary>
    public class CountDTCanGQ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int canBoID = Utils.ConvertToInt32(context.Request.Params["userID"], 0);
            int coQuanID = Utils.ConvertToInt32(context.Request.Params["coQuanID"], 0);

            if (coQuanID != 0)
            {
                DAL.Notify.Notify notify = new DAL.Notify.Notify();
                //int cbThucHienID = IdentityHelper.GetCanBoID();
                string countNotity = "";
                try
                {

                    countNotity = notify.Count_DTCanGQ(canBoID, coQuanID).ToString();
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