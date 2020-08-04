using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Ajax Search du lieu
    /// </summary>
    public class DiaChiSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //string searchKey = Utils.ConvertToString(context.Request.Form["str"], String.Empty); //
            string searchKey = Utils.ConvertToString(context.Request.Form["search_str"], String.Empty);

            List<DiaChiInfo> info = new DAL.DiaChi().GetDiaChiSearch(searchKey).ToList();


            string json = "[";

            int count = info.Count();
            int i = 0;
            foreach (var diachi in info)
            {
                json += "\"#node_" + diachi.DiaChiID + "\"";
                i++;
                if (i < count)
                {
                    json += ",";
                }
            }
            json += "]";

            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }
        public string GetTheJson(List<DiaChiInfo> ls_diachi)
        {
           
            String str = String.Empty;
          
            //lay dia chi theo id
            String hasChildren = "";
            String state = "";
            String type = "tỉnh";

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


                    str += "{" + state + "\"data\":\"" + diachi.TenDiaChi;

                    string[] typeValue;
                    if (diachi.TenDayDu != null)
                    {
                        typeValue = diachi.TenDayDu.Split(new Char[] { ' ' });

                        if (diachi.TenDiaChi != null)
                        {
                            string[] typeValue1 = diachi.TenDiaChi.Split(new Char[] { ' ' });
                            if (typeValue.Count() > 1 && typeValue1[0] == typeValue[1])
                                type = typeValue[0];
                            else
                            {
                                if (diachi.TenDiaChi == diachi.TenDayDu)
                                    type = "khác";
                                else if (typeValue.Count() > 1)
                                    type = typeValue[0] + " " + typeValue[1];
                            }
                        }
                    }


                    str += " (" + type + ")\"";

                    //switch (diachi.Cap)
                    //{
                    //    case 1:
                    //        //str += " (tỉnh)\"";
                    //        //type = "tỉnh";
                    //        //hasChild = "true";
                    //        break;
                    //    case 2:
                    //        //str += " (huyện)\"";
                    //        //type = "huyện";
                    //        //hasChild = "true";
                    //        break;
                    //    case 3:
                    //        //str += " (xã)\"";
                    //        //type = "xã";
                    //        //hasChild = "false";
                    //        break;
                    //}
                    //tinh
                    if (diachi.Cap == 1)
                    {
                        str += ",\"attr\":{\"id\":\"node_tinh_" + diachi.DiaChiID + "\"" + hasChildren + ",\"title\":\"" + type + " " + diachi.TenDiaChi + "\",\"level\":" + diachi.Cap + ",\"rel\":\"" + type + "\"}}";
                    }
                    else if (diachi.Cap == 2)
                    {
                        str += ",\"attr\":{\"id\":\"node_huyen_" + diachi.DiaChiID + "\"" + hasChildren + ",\"title\":\"" + type + " " + diachi.TenDiaChi + "\",\"level\":" + diachi.Cap + ",\"rel\":\"" + type + "\"}}";
                    }
                    else
                    {
                        str += ",\"attr\":{\"id\":\"node_xa_" + diachi.DiaChiID + "\"" + hasChildren + ",\"title\":\"" + type + " " + diachi.TenDiaChi + "\",\"level\":" + diachi.Cap + ",\"rel\":\"" + type + "\"}}";
                    }
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