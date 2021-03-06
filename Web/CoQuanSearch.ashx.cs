﻿using System;
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
    public class CoQuanSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string searchKey = Utils.ConvertToString(context.Request.Form["str"], String.Empty); //

            List<CoQuanInfo> info = new DAL.CoQuan().GetCoQuanSearch(searchKey).ToList();

            
            string json = "[";

            int count = info.Count();
            int i = 0;
            foreach (var diachi in info)
            {
                json += "\"#node_" + diachi.CoQuanID+"\"";
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

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}