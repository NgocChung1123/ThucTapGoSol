using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for NguoiDung_CheckExists
    /// </summary>
    public class NguoiDung_CheckExists : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //String fieldID = context.Request.Params["fieldId"].ToString();
            String fieldValue = context.Request.Params["fieldValue"].ToString();

            int result = 0;

            result = new DAL.NguoiDung().CheckExists(fieldValue.Trim());
            bool json = true;

            if (result == 1)
            {
                json = false;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
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