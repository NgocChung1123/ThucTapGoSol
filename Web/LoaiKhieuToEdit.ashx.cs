using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Edit Loai khieu to
    /// </summary>
    public class LoaiKhieuToEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            LoaiKhieuToInfo info = new LoaiKhieuToInfo();

            string op = Utils.ConvertToString(context.Request.Form["operation"], String.Empty);
            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co
            string name = Utils.ConvertToString(context.Request.Form["name"], String.Empty);

            String json = "{\"message\":\"Không thể edit dữ liệu !!!\",\"status\":false}";

            if (op == "edit_node" && id > 0)
            {

                info = new DAL.LoaiKhieuTo().GetLoaiKhieuToByID(id);
                bool isExists = false;
                if (info.TenLoaiKhieuTo == name)
                    json = "{\"id\":" + id + ",\"status\":true}";
                else
                {
                    //check ten da ton tai
                    isExists = new DAL.LoaiKhieuTo().checkExistsLoaiKhieuTo(name, info.LoaiKhieuToCha, id);

                    if (!isExists)
                    {
                        //insert to database
                        info.LoaiKhieuToCha = info.LoaiKhieuToCha;
                        info.TenLoaiKhieuTo = name;
                        info.Cap = info.Cap;
                        new DAL.LoaiKhieuTo().Update(info);
                        //LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.CAPNHAT + "Loai Khieu To" + " " + id);
                        json = "{\"id\":" + id + ",\"status\":true}";
                    }
                    else
                    {
                        json = "{\"message\":\"Tên này đã tồn tại !!!\",\"status\":false}";
                    }
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