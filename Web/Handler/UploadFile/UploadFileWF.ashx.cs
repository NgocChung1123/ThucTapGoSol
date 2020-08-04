using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Gosol.CMS.Web.Handler.UploadFile
{
    /// <summary>
    /// Summary description for UploadFileWF
    /// </summary>
    public class UploadFileWF : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile uploadFiles;
            string file_Name = "";
            DateTime time = DateTime.Now;              // Use current time
            string format = "yyyyMMddhhmmss";    // Use this format
            string fileUploadedUrl = "";
            try
            {
                uploadFiles = context.Request.Files["Filedata"];

                file_Name = time.ToString(format) + "_" + uploadFiles.FileName;
                fileUploadedUrl = "UploadFiles/FileWF/" + file_Name;
                string pathToSave = HttpContext.Current.Server.MapPath("~/UploadFiles/FileWF/") + file_Name;// uploadFiles.FileName;
                uploadFiles.SaveAs(pathToSave);

            }
            catch
            {
                context.Session["filePath"] = null;
            }
            context.Response.Write(fileUploadedUrl);
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