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
    public class LoaiKhieuToDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            LoaiKhieuToInfo info = new LoaiKhieuToInfo();

            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co
            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);

            //delete dia chi
            string json = "{\"status\":false}";
            if (id > 0 && op == "remove_node")
            {
                new DAL.LoaiKhieuTo().Delete(id);
                //LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.XOA + "Loai Khieu To" + " " + id);
                json = "{\"status\":true}";
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