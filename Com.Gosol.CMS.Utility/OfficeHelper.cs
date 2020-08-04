using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Linq;
//using NotesFor.HtmlToOpenXml;
using Microsoft.Office.Interop.Word;

namespace Com.Gosol.CMS.Utility
{
    public class OfficeHelper
    {
        private static String path = HttpContext.Current.Server.MapPath("~/Temp");
        private static object missing = Type.Missing;


        public static void ExportWordFile(System.Web.UI.Control ctrl, string filename, bool isLandscape = false, float margin = 50f, float pageWidth = 0, float pageHeight = 0)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            HttpContext.Current.Response.ContentType = "Content-type: application/msword";

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("EN-US", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            StringWriter tempStringWriter = new StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            HtmlTextWriter tempHtmlWriter = new HtmlTextWriter(tempStringWriter);

            System.Web.UI.Page pg = new System.Web.UI.Page();

            pg.EnableEventValidation = false;
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            pg.DesignerInitialize();
            pg.RenderControl(tempHtmlWriter);

            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath("~/Styles/word.css"));
            StringBuilder strBuilder = new StringBuilder();
            StreamReader strReader = file.OpenText();
            while (strReader.Peek() >= 0)
            {
                strBuilder.Append(strReader.ReadLine());
            }
            strReader.Close();

            oHtmlTextWriter.Write("<html>");
            oHtmlTextWriter.Write("<head>");
            oHtmlTextWriter.Write("<style type='text/css'>");

            oHtmlTextWriter.Write(strBuilder.ToString());

            oHtmlTextWriter.Write("</style>");
            oHtmlTextWriter.Write("</head>");
            oHtmlTextWriter.Write("<body>");
            oHtmlTextWriter.Write(tempStringWriter.ToString());
            oHtmlTextWriter.Write("</body>");
            oHtmlTextWriter.Write("</html>");

            byte[] bytes = null;
            Com.Gosol.CMS.Utility.OfficeHelper.GetWordFileStream(oStringWriter.ToString(), ref bytes, isLandscape, margin, pageWidth, pageHeight);

            HttpContext.Current.Response.BinaryWrite(bytes);

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void ExportExcelFile(String dataTable, string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            HttpContext.Current.Response.ContentType = "Content-type: application/ms-excel";

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("EN-US", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            StringWriter tempStringWriter = new StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            HtmlTextWriter tempHtmlWriter = new HtmlTextWriter(tempStringWriter);

            System.Web.UI.Page pg = new System.Web.UI.Page();

            
            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath("~/Styles/excel.css"));
            StringBuilder strBuilder = new StringBuilder();
            StreamReader strReader = file.OpenText();
            while (strReader.Peek() >= 0)
            {
                strBuilder.Append(strReader.ReadLine());
            }
            strReader.Close();

            oHtmlTextWriter.Write("<html>");
            oHtmlTextWriter.Write("<head>");
            oHtmlTextWriter.Write("<style type='text/css'>");

            oHtmlTextWriter.Write(strBuilder.ToString());

            oHtmlTextWriter.Write("</style>");
            oHtmlTextWriter.Write("</head>");
            oHtmlTextWriter.Write("<body>");
            oHtmlTextWriter.Write(dataTable);
            oHtmlTextWriter.Write("</body>");
            oHtmlTextWriter.Write("<html>");

            byte[] bytes = null;
            Com.Gosol.CMS.Utility.OfficeHelper.GetExcelFileStream(oStringWriter.ToString(), ref bytes);

            HttpContext.Current.Response.BinaryWrite(bytes);

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void GetWordFileStream(string input, ref byte[] bytes, bool isLandscape, float margin, float pageWidth, float pageHeight)
        {
            //WdOrientation.wdOrientLandscape
            //WdOrientation.wdOrientPortrait                       
            Guid guid = Guid.NewGuid();
            string fileInput = path + "/I" + guid.ToString() + ".doc";
            string fileOutput = path + "/O" + guid.ToString() + ".doc";            
            
            try
            {
                File.WriteAllText(fileInput, input);

                Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document document = new Microsoft.Office.Interop.Word.Document();

                try
                {
                    document = application.Documents.OpenNoRepairDialog(fileInput);

                    document.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;
                    if (pageWidth == 0 || pageHeight == 0)
                    {
                        document.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                    }
                    else
                    {
                        document.PageSetup.PaperSize = WdPaperSize.wdPaperCustom;
                        document.PageSetup.PageWidth = pageWidth;
                        document.PageSetup.PageHeight = pageHeight;
                    }
                    document.PageSetup.TopMargin = margin;
                    document.PageSetup.BottomMargin = margin;
                    document.PageSetup.LeftMargin = margin;
                    document.PageSetup.RightMargin = margin;
                    if (isLandscape)
                    {
                        document.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                    }
                    else
                    {
                        document.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
                    }                    

                    document.SaveAs(fileOutput, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument, ref missing, ref missing, ref missing, ref missing, false, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);


                    document.Close(ref missing, ref missing, ref missing);
                    application.Quit(ref missing, ref missing, ref missing);
                }
                finally
                {
                    Marshal.ReleaseComObject(document);
                    Marshal.ReleaseComObject(application);
                    GC.Collect();
                }
                
                bytes = File.ReadAllBytes(fileOutput);
            }
            finally
            {
                if (File.Exists(fileInput))
                {
                    File.Delete(fileInput);
                }

                if (File.Exists(fileOutput))
                {
                    File.Delete(fileOutput);
                }

            }

        }

        public static void GetExcelFileStream(string input, ref byte[] bytes)
        {
            Guid guid = Guid.NewGuid();
            string fileInput = path + "/I" + guid.ToString() + ".xls";
            string fileOutput = path + "/O" + guid.ToString() + ".xls";
            try
            {
                File.WriteAllText(fileInput, input);

                Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Add(missing);
                application.DisplayAlerts = false;
                
                try
                {   
                    workbook = application.Workbooks.Open(fileInput);
                    application.Windows.Application.ActiveWindow.DisplayGridlines = true;
                    workbook.SaveAs(fileOutput, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, missing,
                        missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
                    workbook.Close(missing, missing, missing);                    
                    application.Quit();
                }
                finally
                {                    
                    Marshal.FinalReleaseComObject(workbook);
                    Marshal.FinalReleaseComObject(application);
                    GC.Collect();
                }
                bytes = File.ReadAllBytes(fileOutput);
            }
            finally
            {
                try
                {
                    if (File.Exists(fileInput))
                    {
                        File.Delete(fileInput);
                    }
                }
                catch
                {
                }
                try
                {
                    if (File.Exists(fileOutput))
                    {
                        File.Delete(fileOutput);
                    }
                }
                catch
                {
                }
            }
        }
    }
}
