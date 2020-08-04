//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Com.Gosol.CMS.Model;
//using Com.Gosol.CMS.Utility;

//namespace Com.Gosol.CMS.Web
//{
//    /// <summary>
//    /// Summary description for Handler1
//    /// </summary>
//    public class DiaChiCheckAvailable : IHttpHandler
//    {

//        public void ProcessRequest(HttpContext context)
//        {
//            //string fieldId = context.Request.QueryString["fieldId"];
//            //string fieldValue = context.Request.QueryString["fieldValue"]; //ten dia danh
//            //string extraData = context.Request.QueryString["extraData"];
//            //int id_frm = Utils.ConvertToInt32(context.Request.QueryString["id"], 0);
//            //string parentId = context.Request.QueryString["parentId"];//dia chi cha
//            //string _node = context.Request.QueryString["_"];
//            //string level = context.Request.QueryString["level"]; //cap

//            String tempID = context.Request.Form["id"].Replace("tinh_", "").Replace("huyen_", "").Replace("xa_", "");
//            int id = Utils.ConvertToInt32(tempID, 0); //id neu co
//            string name = context.Request.Form["name"]; //ten dia danh
//            int parentId = Utils.ConvertToInt32(context.Request.Form["parentId"], 0);//dia chi cha
//            int level = Utils.ConvertToInt32(context.Request.Form["level"], 1); //cap bac
//            string _node = context.Request.QueryString["_"];

//            //check dia chi da ton tai?
//            //bool checkExists = new DAL.DiaChi().checkExistsDiaChi(id, name, parentId, level,);

//            //string isExists = "true";
//            //if (checkExists == false)
//            //    isExists = "false";

//            //string json = "{\"flag\":" + isExists + "}";

//            context.Response.ContentType = "application/json";
//            context.Response.Write(json);
//        }

//        public bool IsReusable
//        {
//            get
//            {
//                return true;
//            }
//        }
//    }
//}