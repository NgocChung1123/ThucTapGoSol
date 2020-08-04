using Com.Gosol.CMS.DAL.HeThong;
using Com.Gosol.CMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class DownloadFileTaiLieu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int fileScanId = String.IsNullOrEmpty(Request.QueryString["ma_file"]) ? 0 : Convert.ToInt32(Request.QueryString["ma_file"]);

            FileTaiLieuInfo info = new FileTaiLieu().GetByID(fileScanId);
            if (info != null)
            {
                //HiddenField hdf_fileurl = (HiddenField)e.Item.FindControl("hdf_fileurl");
                string fileName = info.FileUrl;//hdf_fileurl.Value.ToString();
                string input = Server.MapPath("~/UploadFiles/encrypt/") + fileName;
                string output = Server.MapPath("~/UploadFiles/decrypt/") + fileName;//link.Replace("encrypt", "decrypt");

                //Giai ma file
                DecryptFile(input, output);

                //set for download
                FileInfo fi = new FileInfo(output);
                long sz = fi.Length;

                //download file
                System.IO.FileInfo FileName = new System.IO.FileInfo(output);
                FileStream myFile = new FileStream(output, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader _BinaryReader = new BinaryReader(myFile);

                long startBytes = 0;
                string filePath = Server.MapPath("~/UploadFiles/decrypt/");
                string lastUpdateTiemStamp = File.GetLastWriteTimeUtc(filePath).ToString("r");
                string _EncodedData = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStamp;

                //Clear the content of the response
                Response.Clear();
                Response.Buffer = false;
                Response.AddHeader("Accept-Ranges", "bytes");
                Response.AppendHeader("ETag", "\"" + _EncodedData + "\"");
                Response.AppendHeader("Last-Modified", lastUpdateTiemStamp);

                //Set the ContentType
                Response.ContentType = "application/octet-stream";

                //Add the file name and attachment, 
                //which will force the open/cancel/save dialog to show, to the header
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName.Name);

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

                if (i >= maxCount)
                {
                    //xoa file
                    _BinaryReader.Close();
                    myFile.Close();
                    if (File.Exists(output))
                        File.Delete(output);
                }
            }
            else
            {

            }
        }
        private void DecryptFile(string inputFile, string outputFile)
        {
            string password = @"myKey123"; // Your Key Here

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateDecryptor(key, key),
                CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();
        }
    }
}