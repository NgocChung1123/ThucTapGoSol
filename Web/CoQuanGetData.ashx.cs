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
    public class CoQuanGetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"];
            string coQuanID = context.Request.QueryString["coQuanID"];
            string capID = context.Request.QueryString["capID"];

            int coQuanIDs = 0;
            int capIDs = 0;
            if (id != String.Empty)
            {
                //---
            }
            else
            {
                coQuanIDs = Utils.ConvertToInt32(coQuanID, 0);
                capIDs = Utils.ConvertToInt32(capID, 0);
            }
            
            string json = GetTheJson(coQuanIDs, capIDs,id);

            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public string GetTheJson(int coQuanID, int capID, string id)
        {
            List<CoQuanInfo> ls_coquan = new List<CoQuanInfo>();
            String str = String.Empty;

            if (id != String.Empty)
            {
                ls_coquan = new DAL.CoQuan().GetCoQuanByParentID(Com.Gosol.CMS.Utility.Utils.ConvertToInt32(id, 0)).ToList();
            }
            else {
                if (capID == (int)CapQuanLy.CapUBNDTinh)
                {
                    ls_coquan = new DAL.CoQuan().GetCoQuanForAjax().ToList();
                }
                else
                {
                    //ls_coquan = new DAL.CoQuan().GetCoQuanByParentID(coQuanID).ToList();
                    ls_coquan = new DAL.CoQuan().GetCoQuanByCoQuanID(coQuanID).ToList();
                }
            }


            //if (id != String.Empty)
            //    ls_coquan = new DAL.CoQuan().GetCoQuanByParentID(Com.Gosol.CMS.Utility.Utils.ConvertToInt32(id, 0)).ToList();
            //else
            //    ls_coquan = new DAL.CoQuan().GetCoQuanForAjax().ToList();

            //lay dia chi theo id
            String hasChildren = "";
            String state = "";

            str = "[";
            if (ls_coquan != null)
            {
                int count = ls_coquan.Count();
                int i = 0;
                foreach (var diachi in ls_coquan)
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
                    str += "{"+state + "\"data\":\"" + diachi.TenCoQuan + "\"";

                    str += ",\"attr\":{\"id\":\"node_" + diachi.CoQuanID + "\"" + hasChildren + "}}";
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