using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class DowloadFileDonThuSync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.QueryString["url"];
            if (!string.IsNullOrEmpty(url))
            {
                string fileURL = url.Replace("*", " ");

                string pathFile = Server.MapPath("~") + fileURL;

                long startBytes = 0;
                string fileinfo = "";
                if (File.Exists(pathFile))
                {
                    FileInfo fi = new FileInfo(pathFile);
                    long sz = fi.Length;

                    //download file
                    fileinfo = pathFile;
                    //string filePath = Server.MapPath("~" + Constant.PATH_FILE_HOSO);
                    //string lastUpdateTiemStamp = File.GetLastWriteTimeUtc(filePath).ToString("r");
                    string _EncodedData = HttpUtility.UrlEncode(fileURL, Encoding.UTF8);

                    System.IO.FileInfo FileName = new System.IO.FileInfo(fileinfo);
                    FileStream myFile = new FileStream(fileinfo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    BinaryReader _BinaryReader = new BinaryReader(myFile);
                    //Clear the content of the response
                    Response.Clear();
                    Response.Buffer = false;

                    //Set the ContentType
                    if (FileName.Extension == ".pdf" || FileName.Extension == ".PDF")
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "inline;filename=\"" + FileName.Name + "\"");
                    }
                    else
                    {
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName.Name + "\"");
                    }


                    //Add the file size into the response header
                    Response.AddHeader("Content-Length", (FileName.Length - startBytes).ToString());
                    Response.AddHeader("Connection", "Keep-Alive");

                    //Set the Content Encoding type
                    Response.ContentEncoding = Encoding.UTF8;

                    //Send data
                    _BinaryReader.BaseStream.Seek(startBytes, SeekOrigin.Begin);

                    //Dividing the data in 1024 bytes package
                    int maxCount = (int)Math.Ceiling((FileName.Length - startBytes + 0.0) / 1024);
                    //Download in block of 1024 bytes
                    int i;
                    for (i = 0; i < maxCount && Response.IsClientConnected; i++)
                    {
                        //compare packets transferred with total number of packets
                        Response.BinaryWrite(_BinaryReader.ReadBytes(1024));
                        Response.Flush();
                    }


                }
                else
                {

                }
            }
        }
    }
}