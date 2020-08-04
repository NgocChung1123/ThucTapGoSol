using System;
using System.Web;
using System.Web.SessionState;

using System.IO;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Ajax Search du lieu
    /// </summary>
    public class DeleteFileUploaded : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string pathToFile = HttpContext.Current.Server.MapPath("~/UploadFiles/encrypt/") + context.Session["filePath"];

            if (File.Exists(pathToFile))
                File.Delete(pathToFile);
            //context.Response.ContentType = "application/text";
            //context.Response.Write(json);
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