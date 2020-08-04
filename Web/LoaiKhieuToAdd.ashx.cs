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
    public class LoaiKhieuToAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            LoaiKhieuToInfo info = new LoaiKhieuToInfo();

            //"operation" : "create_node", 
            //        "parent_id" : data.rslt.parent === -1 ? "" : data.rslt.parent.attr("id").replace("node_",""), 
            //        "name" : data.rslt.name

            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);
            int parentId = Utils.ConvertToInt32(context.Request.Form["parent_id"], 0); //id neu co
            string name = Utils.ConvertToString(context.Request.Form["name"], String.Empty);

            String json = "{\"message\":\"Không thể thêm dữ liệu !!!\",\"status\":false}";

            if (op == "create_node")
            {
                //check ten da ton tai
                bool isExists = new DAL.LoaiKhieuTo().checkExistsLoaiKhieuTo(name, parentId, 0);

                LoaiKhieuToInfo infoParent = new LoaiKhieuToInfo();
                if (parentId > 0)
                    infoParent = new DAL.LoaiKhieuTo().GetLoaiKhieuToByID(parentId);

                if (!isExists)
                {
                    //insert to database
                    info.LoaiKhieuToCha = parentId;
                    info.TenLoaiKhieuTo = name;
                    info.Cap = infoParent != null ? infoParent.Cap + 1 : 1;
                    int id = new DAL.LoaiKhieuTo().Insert(info);

                    //LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.THEMMOI + "Loai Khieu To" + " " + id);
                    if (id > 0)
                        json = "{\"id\":" + id + ",\"status\":true}";
                }
                else
                {
                    json = "{\"message\":\"Tên này đã tồn tại !!!\",\"status\":false}";
                }
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