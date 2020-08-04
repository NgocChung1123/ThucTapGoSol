using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Handler
{
    /// <summary>
    /// Summary description for CkeditorUploader
    /// </summary>
    public class CkeditorUploader : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile uploads = context.Request.Files["upload"];
            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];

            string fileName = DateTime.Now.ToString("yyyyMMdd") + "_" + System.IO.Path.GetFileName(uploads.FileName);

            string paths = context.Server.MapPath("~/UploadFiles/Ckeditor");
            if (!Directory.Exists(paths))
            {
                Directory.CreateDirectory(paths);
            }

            var path = Path.Combine(paths, fileName);

            uploads.SaveAs(path);

            string url = "/UploadFiles/Ckeditor/" + fileName;

            context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");

            context.Response.End();

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