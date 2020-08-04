using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {  
            string uploadPath = context.Server.MapPath("~/UploadFiles");
            string fileUploadedUrl = string.Empty;

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = null;

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        file = context.Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            fileName = DateTime.Now.ToString("mmddyyyy_HHmmss") + "_" + fileName;                           

                            var path = Path.Combine(uploadPath, fileName);
                            file.SaveAs(path);


                            fileUploadedUrl = "UploadFiles/" + fileName;                            

                        }
                    }

                }

                if (fileUploadedUrl == string.Empty) fileUploadedUrl = "";

                context.Response.ContentType = "text/plain";
                context.Response.Write(fileUploadedUrl);
            }
            catch { }            
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}