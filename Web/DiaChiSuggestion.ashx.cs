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
    public class DiaDanhSuggestion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tenDiaChi = Utils.ConvertToString(context.Request.Form["query"], String.Empty); //

            List<DiaChiInfo> info = new DAL.DiaChi().GetDiaChiSuggestion(tenDiaChi).ToList();

            
            //{"query":"gia b","data":["Gia Bình","Gia Bắc"],"suggestions":["Gia Bình","Gia Bắc"]}
            //string json = "{\"id\":"+id+",\"level\":"+info.Cap+",\"cap\":\""+cap+"\",\"name\":\""+ info.TenDiaChi+"\",\"fullName\":\""+info.TenDayDu+"\"}";
            string json = "{\"query\":\""+tenDiaChi+"\", \"data\":[";

            string suggestion = "\"suggestions\":[";
            int count = info.Count();
            int i = 0;
            foreach (var diachi in info)
            {
                json += "\"" + diachi.TenDiaChi+"\"";
                suggestion += "\""+diachi.TenDiaChi+"\"";
                i++;
                if (i < count)
                {
                    json += ",";
                    suggestion += ",";
                }
                else
                {
                    json += "],";
                    suggestion += "]";
                }
            }
            json += suggestion + "}";

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