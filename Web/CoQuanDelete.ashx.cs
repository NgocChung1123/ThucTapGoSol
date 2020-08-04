using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Xoa loai khieu to
    /// </summary>
    public class CoQuanDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CoQuanInfo info = new CoQuanInfo();

            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co
            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);

            //delete dia chi
            string json = "{\"status\":0}";
            if (id > 0 && op == "remove_node")
            {
                try
                {
                    json = new DAL.CoQuan().Delete(id).ToString();
                    //json = id.ToString();
                }
                catch 
                {
                    json = "0";
                }
                //LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.XOA + "Co Quan" + " " + id);
            }

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