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
    public class CoQuanAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CoQuanInfo info = new CoQuanInfo();

            //"operation" : "create_node", 
            //        "parent_id" : data.rslt.parent === -1 ? "" : data.rslt.parent.attr("id").replace("node_",""), 
            //        "name" : data.rslt.name

            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);
            int parentId = Utils.ConvertToInt32(context.Request.Form["parent_id"], 0); //id neu co
            string name = Utils.ConvertToString(context.Request.Form["name"], String.Empty);
            int capID = Utils.ConvertToInt32(context.Request.Form["capID"], 0);
            int thamquyenID = Utils.ConvertToInt32(context.Request.Form["thamquyenID"], 0);
            int tinhID = Utils.ConvertToInt32(context.Request.Form["tinhID"], 0);
            int huyenID = Utils.ConvertToInt32(context.Request.Form["huyenID"], 0);
            int xaID = Utils.ConvertToInt32(context.Request.Form["xaID"], 0);

            String json = "{\"message\":\"Không thể thêm dữ liệu !!!\",\"status\":false}";

            if (op == "create_node")
            {
                //check ten da ton tai
                bool isExists = new DAL.CoQuan().checkExistsCoQuan(name, parentId, 0);

                CoQuanInfo infoParent = new CoQuanInfo();
                if (parentId > 0)
                    infoParent = new DAL.CoQuan().GetCoQuanByID(parentId);

                if (!isExists)
                {
                    //insert to database
                    info.CoQuanChaID = parentId;
                    info.TenCoQuan = name;
                    info.CapID = capID;
                    info.ThamQuyenID = thamquyenID;
                    info.TinhID = tinhID;
                    info.HuyenID = huyenID;
                    info.XaID = xaID;
                    //info.Cap = infoParent != null ? infoParent.Cap + 1 : 1;
                    int id = new DAL.CoQuan().Insert(info);
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