using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Lay so don thu tu dong
    /// </summary>
    public class HoSoDonCurrentStatus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //get so Ho so hien tai
            int coQuanId = 1;
            int number = 1;//new DAL.DonThu().getSoDonThu(coQuanId);
            string json = "{\"soDk\":"+number+"}";
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}