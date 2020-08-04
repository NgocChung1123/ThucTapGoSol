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
    public class DiaChiEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DiaChiInfo info = new DiaChiInfo();

            String tempID = context.Request.Form["id"].Replace("tinh_", "").Replace("huyen_", "").Replace("xa_", "");
            int id = Utils.ConvertToInt32(tempID, 0); //id neu co
            int level = Utils.ConvertToInt32(context.Request.Form["level"], 1);

            info = new DAL.DiaChi().GetDiaChiByID(id, level);
            int parentId;

            string str_TenDayDu = info.TenDayDu;

            string parentName;
            string str_parentId = "";
            string str_parentName = "";
            string cap = "khác";
            if (level > 1)
            {
                //co cha
                parentId = info.DiaChiCha;
                DiaChiInfo parentInfo = new DAL.DiaChi().GetDiaChiByID(parentId, level - 1);
                parentName = parentInfo.TenDayDu;

                str_parentId = "\"parentId\":" + parentId + ",";
                str_parentName = ",\"parentName\":\"" + parentName + "\"";
                str_TenDayDu = str_TenDayDu + " - " + parentName;
            }
            string[] str = info.TenDayDu.Split(new Char[] { ' ' });
            string[] str1 = info.TenDiaChi.Split(new Char[] { ' ' });

            if (info.TenDayDu != info.TenDiaChi)
                if (str.Count() > 1 && str[1] != str1[0])
                    cap = str[0] + " " + str[1];
                else
                    cap = str[0];


            //{"id":1886,"parentId":189,"level":2,"cap":"huyện","name":"An Phú","fullName":"huyện An Phú - tỉnh An Giang","parentName":"tỉnh An Giang"}
            string json = "{\"id\":" + id + "," + str_parentId + "\"level\":" + info.Cap + ",\"cap\":\"" + cap + "\",\"name\":\"" + info.TenDiaChi + "\",\"fullName\":\"" + str_TenDayDu + "\"" + str_parentName + "}";

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