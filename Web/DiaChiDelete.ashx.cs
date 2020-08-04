using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class DiaChiDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DiaChiInfo info = new DiaChiInfo();

            String tempID = context.Request.Form["id"].Replace("tinh_", "").Replace("huyen_", "").Replace("xa_", "");
            int id = Utils.ConvertToInt32(tempID, 0); //id neu co
            int level = Utils.ConvertToInt32(context.Request.Form["level"], 1); //id neu co
            string operation = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);

            //delete dia chi
            string json = "{\"status\":false}";
            if (id > 0 && operation == "remove_node")
            {
                int status = new DAL.DiaChi().Delete(id, level);
                if (status == 1)
                    json = "{\"status\":true}";
                else
                    json = "{\"message\":\"Không thể xóa danh mục cha khi còn danh mục con !!!\",\"status\":false}";
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