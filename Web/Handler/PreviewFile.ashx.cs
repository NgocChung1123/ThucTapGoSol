using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Com.Gosol.CMS.Web.Handler
{
    /// <summary>
    /// Summary description for PreviewFile
    /// </summary>
    public class PreviewFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["filename"];
            //string fileName = new DAL.DonThu.DonThu().GetByID(int.Parse(ID)).FileQuyetDinh;
            Document document = new Document();
            string zz = context.Request["zz"];
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
               
                string fullPath = filePath + fileName;
                if (File.Exists(fullPath))
                {
                    if (fileName.ToUpper().Contains(".PDF"))
                    {
                        var stream = new MemoryStream();

                        using (FileStream source = File.Open(fullPath, FileMode.Open))
                        {
                            source.CopyTo(stream);
                        }
                        var content = stream.ToArray();
                        context.Response.Clear();
                        context.Response.ContentType = "application/pdf";

                        context.Response.BinaryWrite(content);

                        context.Response.Flush();
                        context.Response.End();
                    }
                    else
                    {
                        document.LoadFromFile(filePath + fileName);
                        var stream = new MemoryStream();
                        document.SaveToStream(stream, FileFormat.PDF);
                        var content = stream.ToArray();
                        context.Response.Clear();
                        context.Response.ContentType = "application/pdf";

                        context.Response.BinaryWrite(content);

                        context.Response.Flush();
                        context.Response.End();
                    }

                }


               
            }
            catch (Exception ex)
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