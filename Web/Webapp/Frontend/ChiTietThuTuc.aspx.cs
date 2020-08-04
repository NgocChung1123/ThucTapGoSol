using Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class ChiTietThuTuc : System.Web.UI.Page
    {
        private int stt = 1;
        private int thuTucId = 0;
        private string fileName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            thuTucId = Utility.Utils.ConvertToInt32(Request.QueryString["id"].ToString(), 0);
            DMLoaiThuTucInfo info = new DMLoaiThuTuc().GetByID(thuTucId);
            lblTenThuTuc1.Text = info.TenLoaiThuTuc;
            lblCoSoPhapLy.Text = info.CoSoPhapLy;
            fileName = info.FileUrl;
            if (info.FileUrl != "")
            {
                tdDownload.Controls.Add(new LiteralControl("<a href = '../../../Handler/DownloadFileQuyetDinh.ashx?filename=" + fileName + "&zz=FileWF'><img src='" + "../../../images/download.png"
                                        + "' style='" + "width: 20px; height: 20px" + "'/>"));
            }
            else
            {
                tdDownload.Controls.Add(new LiteralControl("<a onclick='lol();'; return false;';><img src='" + "../../../images/download.png"
                                        + "' style='" + "width: 20px; height: 20px" + "'/>"));
            }
            List<DMThuTucInfo> list = new DMThuTuc().GetStepsByThuTuc(thuTucId);
            rptThuTuc.DataSource = list;
            rptThuTuc.DataBind();
        }

        protected void rptThuTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            lblSTT.Text = stt.ToString();
            stt++;
        }

        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("/Webapp/Frontend/TrinhTuThuTuc.aspx");
        //    string path = Server.HtmlEncode(Request.PhysicalApplicationPath);
        //    if (fileName != "")
        //    {
        //        string url = path + "UploadFiles\\FileWF\\" + fileName;
        //        if (File.Exists(url))
        //        {
        //            Response.ContentType = "text/plain";
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //            Response.TransmitFile(url);
        //            Response.End();
        //        }
        //        else
        //        {
        //            //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess1", "showthongBaoSuccess1()", true);
        //            Response.Redirect("/Webapp/Frontend/TrinhTuThuTuc.aspx",false);
        //            //ClientScript.RegisterStartupScript(this.GetType(), "showthongBaoSuccess", "showthongBaoSuccess()", true);
        //            //ClientScript.RegisterStartupScript(typeof(Page), "showthongBaoSuccess", "window.location='/Webapp/Frontend/TrinhTuThuTuc.aspx'; showthongBaoSuccess();", true);
        //            // ("/Webapp/Frontend/TrinhTuThuTuc.aspx");
        //        }
        //    }
        //    else
        //    {
        //        //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess1", "showthongBaoSuccess1()", true);
        //        //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess1", "showthongBaoSuccess1('" + thuTucId.ToString() + "')", true);
        //        Response.Redirect("/Webapp/Frontend/TrinhTuThuTuc.aspx");
        //    }
        //}
    }
}