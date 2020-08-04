using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Summary description for DownloadFile
    /// </summary>
    public class DownloadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string TenFile = context.Request.Form["tenfile"];
            try
            {
                string FullFilePath = context.Server.MapPath("~/UploadFiles/" + TenFile);
                FileInfo file = new FileInfo(FullFilePath);
                if (File.Exists(FullFilePath) == true)
                {
                   
                    string[] files = System.IO.Directory.GetFiles(context.Server.MapPath("~/UploadFiles"), TenFile + "*");

                    if (files.Length == 1) // We got one and only one file
                    {
                    var fullpath = Path.Combine(context.Server.MapPath("~/UploadFiles"), TenFile);
                        using (BinaryReader reader = new BinaryReader(new FileStream(fullpath,FileMode.Open,FileAccess.Read)))
                        {
                            byte[] buffer = new byte[(int)reader.BaseStream.Length];
                             buffer = reader.ReadBytes((int)reader.BaseStream.Length);
                            if (Path.GetExtension(FullFilePath) == ".jpg")
                                context.Response.ContentType = "image/jpeg";
                            else
                                context.Response.ContentType = "application/octet-stream";
                            context.Response.AddHeader("content-disposition", "attachment; filename=\"" + TenFile + "\"");
                            context.Response.BinaryWrite(buffer);
                            context.Response.End();
                            // use the stream
                        }
                    }
                    //FileStream files = new FileStream(FullFilePath, FileMode.Create);
                   
                    //files.Read(buffer, 0, (int)file.Length);
                    //files.Close();
                    
                }
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