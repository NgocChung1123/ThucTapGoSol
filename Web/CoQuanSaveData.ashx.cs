using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// luu thong tin co quan ajax
    /// </summary>
    public class CoQuanSaveData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CoQuanInfo info = new CoQuanInfo();

            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co

            info.TenCoQuan = context.Request.Form["name"]; //ten co quan
            info.CoQuanChaID = Utils.ConvertToInt32(context.Request.Form["coquanchaID"], 0);//co quan cha id
            info.CapID = Utils.ConvertToInt32(context.Request.Form["capID"], 0); //cap id
            info.ThamQuyenID = Utils.ConvertToInt32(context.Request.Form["thamquyenID"], 0); //tham quyen id
            info.TinhID = Utils.ConvertToInt32(context.Request.Form["tinhID"], 0); //tinh id
            info.HuyenID = Utils.ConvertToInt32(context.Request.Form["huyenID"], 0); //huyenID id
            info.XaID = Utils.ConvertToInt32(context.Request.Form["xaID"], 0); //xa id


            string json = "{\"message\":\"Dữ liệu truyền lên không hợp lệ !!!\",\"status\":false}";

            if (info != null)
            {
                if (id <= 0)
                {
                    //insert new
                    if (info.CoQuanChaID == 0) {
                        info.TinhID = 0;
                        info.HuyenID = 0;
                        info.XaID = 0;
                    }
                    id = new DAL.CoQuan().Insert(info);
                }
                else
                {
                    info.CoQuanID = id;
                    new DAL.CoQuan().Update(info);
                }

                json = "{\"id\":" + id + ",\"coquanchaID\":" + info.CoQuanChaID + ",\"capID\":" + info.CapID + ",\"thamquyenID\":" + info.ThamQuyenID + ",\"tinhID\":" + info.TinhID + ",\"huyenID\":" + info.HuyenID + ",\"xaID\":" + info.XaID + ",\"status\":true}";
            }

            //{"id":6678804,"parentId":6678800,"level":2,"status":true,"cap":"huyện"}

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