using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using System.IO;

namespace Com.Gosol.CMS.Web
{
    public static class FileHelper
    {
        public static String Upload(FileUpload fileUpload)
        {
            String fileName = String.Empty;
            if (fileUpload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fileUpload.FileName);

                if (fileExt == ".txt" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".gif" || fileExt == ".tif")
                {
                    try
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + DateTime.Now.ToString("ddmmyyyy") + fileExt; ;
                        String path = HttpContext.Current.Server.MapPath("~/UploadFiles");
                        fileUpload.SaveAs(path + "/" + fileName);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }                
            }
            return fileName; 
        }
        public static String UploadFileHoSo(FileUpload fileUpload)
        {
            String fileName = String.Empty;
            if (fileUpload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fileUpload.FileName);

                if (fileExt == ".txt" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg")
                {
                    try
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + DateTime.Now.ToString("ddmmyyyy") + fileExt; ;
                        String path = HttpContext.Current.Server.MapPath("~/UploadFiles/filehoso");
                        fileUpload.SaveAs(path + "/" + fileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return fileName;
        }
        public static String UploadKetQua(FileUpload fileUpload)
        {
            String fileName = String.Empty;
            if (fileUpload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fileUpload.FileName);

                if (fileExt == ".txt" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
                {
                    try
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + DateTime.Now.ToString("ddmmyyyy") + fileExt; ;
                        String path = HttpContext.Current.Server.MapPath("~/UploadFiles/ketqua");
                        fileUpload.SaveAs(path + "/" + fileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return fileName;
        }

        public static String UploadKetQuaXuLy(FileUpload fileUpload)
        {
            String fileName = String.Empty;
            if (fileUpload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fileUpload.FileName);

                if (fileExt == ".txt" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".PDF" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".zip" || fileExt == ".rar")
                {
                    try
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + DateTime.Now.ToString("ddmmyyyy") + fileExt; ;
                        String path = HttpContext.Current.Server.MapPath("~/UploadFiles/FileKetQuaXuLy");
                        fileUpload.SaveAs(path + "/" + fileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return fileName;
        }

        public static String UploadKetQua(string fileUpload)
        {
            String fileName = String.Empty;
            string sPath = "";
            HttpPostedFile PostedFiles = null;
            if (fileUpload != "")
            {
                string[] fileExt = fileUpload.Split('.');

                //if (fileExt[1] == ".txt" || fileExt[1] == ".doc" || fileExt[1] == ".docx" || fileExt[1] == ".xls" || fileExt[1] == ".xlsx" || fileExt[1] == ".pdf" || fileExt[1] == ".jpg" || fileExt[1] == ".jpeg" || fileExt[1] == ".png")
                //{
                // try
                {
                    fileName = fileExt[0] + DateTime.Now.ToString("ddmmyyyy") + fileUpload;
                    string pathToSave = HttpContext.Current.Server.MapPath("~/UploadFiles/ketqua");
                    sPath = Path.Combine(pathToSave, fileName).ToString();
                    PostedFiles.SaveAs(sPath);
                }
                // catch
                {
                    //  throw;
                }
                //}
            }
            return fileName;
        }

        public static String UploadThiHanh(FileUpload fileUpload)
        {
            String fileName = String.Empty;
            if (fileUpload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fileUpload.FileName);

                if (fileExt == ".txt" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
                {
                    try
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + DateTime.Now.ToString("ddmmyyyy") + fileExt; ;
                        String path = HttpContext.Current.Server.MapPath("~/UploadFiles/thihanh");
                        fileUpload.SaveAs(path + "/" + fileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return fileName;
        }

        public static String UploadFileChuyen(FileUpload fileUpload)
        {
            String fileName = String.Empty;
            if (fileUpload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fileUpload.FileName);

                if (fileExt == ".txt" || fileExt == ".doc" || fileExt == ".docx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
                {
                    try
                    {
                        fileName = System.IO.Path.GetFileNameWithoutExtension(fileUpload.FileName) + "_" + DateTime.Now.ToString("ddmmyyyy") + fileExt; ;
                        String path = HttpContext.Current.Server.MapPath("~/UploadFiles/filechuyengq");
                        fileUpload.SaveAs(path + "/" + fileName);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return fileName;
        }
    }
}