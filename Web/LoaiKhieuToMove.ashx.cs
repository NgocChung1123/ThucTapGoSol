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
    public class LoaiKhieuToMove : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            LoaiKhieuToInfo info = new LoaiKhieuToInfo();

            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);
            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co
            int parentId = Utils.ConvertToInt32(context.Request.Form["parent_id"], 0);

            String json = "{\"message\":\"Không thể di chuyển dữ liệu !!!\",\"status\":false}";

            if (op == "move_node" && id > 0)
            {

                info = new DAL.LoaiKhieuTo().GetLoaiKhieuToByID(id);
                bool isExists = false;

                //check ten da ton tai
                isExists = new DAL.LoaiKhieuTo().checkExistsLoaiKhieuTo(info.TenLoaiKhieuTo, parentId, id);

                LoaiKhieuToInfo infoParent = new LoaiKhieuToInfo();
                if (parentId > 0)
                    infoParent = new DAL.LoaiKhieuTo().GetLoaiKhieuToByID(parentId);

                if (infoParent.Cap > 2)
                    json = "{\"message\":\"Loại khiếu tố chỉ có thể có 3 cấp !!!\",\"status\":false}";
                else
                    if (!isExists)
                    {
                        //update to database
                        info.LoaiKhieuToCha = parentId;
                        info.Cap = infoParent != null ? infoParent.Cap + 1 : 1;
                        new DAL.LoaiKhieuTo().Update(info);
                        //LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.CAPNHAT + "Loai Khieu To" + " " + id);
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