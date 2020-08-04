using Spire.Doc;
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
    public class DownloadFileQuyetDinh : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["filename"];
            string zz = context.Request["zz"];
            string fileDown = fileName.Replace("//", "\\").Replace("/", "\\").Split('\\').Last();
            string filePath;

            try
            {
                if (zz == "FileWF")
                {
                    filePath = context.Server.MapPath("~/UploadFiles/FileWF/");
                }
                else
                {
                    filePath = context.Server.MapPath("~/UploadFiles/");
                }
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileDown);
                // context.Response.TransmitFile(filePath + "asd.png");
                context.Response.WriteFile(filePath + fileName);
                context.Response.End();
            }
            catch
            {

            }

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