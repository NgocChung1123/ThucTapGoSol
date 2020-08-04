using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class LoaiKhieuToGetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];

            string json = GetTheJson(id);

            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public string GetTheJson(String id)
        {
            List<LoaiKhieuToInfo> ls_diachi = new List<LoaiKhieuToInfo>();
            String str = String.Empty;
            if (id != String.Empty)
                ls_diachi = new DAL.LoaiKhieuTo().GetLoaiKhieuToByParentID(Com.Gosol.CMS.Utility.Utils.ConvertToInt32(id, 0)).ToList();
            else
                ls_diachi = new DAL.LoaiKhieuTo().GetLoaiKhieuToForAjax().ToList();
            //lay dia chi theo id
            String hasChildren = "";
            String state = "";

            str = "[";
            if (ls_diachi != null)
            {
                int count = ls_diachi.Count();
                int i = 0;
                foreach (var diachi in ls_diachi)
                {
                    if (diachi.hasChild > 0)
                    {
                        state = "\"state\":\"closed\",";
                        hasChildren = ",\"hasChildren\": true";
                    }
                    else
                    {
                        state = "";
                        hasChildren = "";
                    }

                    //[{"state":"closed","data":"Khiếu nại","attr":{"id":"node_1","hasChildren":true}},
                    str += "{"+state + "\"data\":\"" + diachi.TenLoaiKhieuTo + "\"";

                    str += ",\"attr\":{\"id\":\"node_" + diachi.LoaiKhieuToID + "\"" + hasChildren + "}}";
                    i++;
                    if (i < count)
                        str += ",";
                }
                str += "]";
            }
            else
                return "[]";


            return str;
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