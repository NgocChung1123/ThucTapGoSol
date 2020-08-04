using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Move Loai khieu to
    /// </summary>
    public class CoQuanMove : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CoQuanInfo info = new CoQuanInfo();

            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);
            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co
            int parentId = Utils.ConvertToInt32(context.Request.Form["parent_id"], 0);

            String json = "{\"message\":\"Không thể di chuyển dữ liệu !!!\",\"status\":false}";

            if (op == "move_node" && id > 0 && parentId > 0)
            {

                info = new DAL.CoQuan().GetCoQuanByID(id);

                if (info.CoQuanChaID > 0)
                {
                    bool isExists = false;

                    //check ten da ton tai
                    isExists = new DAL.CoQuan().checkExistsCoQuan(info.TenCoQuan, parentId, id);

                    CoQuanInfo infoParent = new CoQuanInfo();
                    if (parentId > 0)
                        infoParent = new DAL.CoQuan().GetCoQuanByID(parentId);

                    if (!isExists)
                    {
                        //update to database
                        info.CoQuanChaID = parentId;
                        //info.Cap = infoParent != null ? infoParent.Cap + 1 : 1;
                        new DAL.CoQuan().Update(info);
                        //LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.CAPNHAT + "Co Quan" + " " + info.CoQuanID);
                        json = "{\"id\":" + id + ",\"status\":true}";
                    }
                    else
                    {
                        json = "{\"message\":\"Tên này đã tồn tại !!!\",\"status\":false}";
                    }
                }
                else
                    json = "{\"message\":\"Không thể di chuyển Danh mục cấp 1 !!!\",\"status\":false}";
            }

            if (parentId == 0)
                json = "{\"message\":\"Không thể di chuyển Danh mục cấp 1 !!!\",\"status\":false}";

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