using Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class ThuTuc : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", page);
                }
                else
                {
                    Session["CurrentPage"] = page;
                }
                BindRepeater();
            }
        }

        #region preLoad
        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            int totalRow = 0;
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            txtSearch.Text = keyword;

            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new DMLoaiThuTuc().CountSearch(keyword);
            }
            catch (Exception ex)
            {

            }

            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0)
            {
                pageCount++;
            }

            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
            }
        }

        #endregion

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            if (currentPage == 0)
            {
                currentPage = 1;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();

            try
            {
                keyword = "%" + keyword + "%";
                rptThuTuc.DataSource = new DMLoaiThuTuc().GetLoaiThuTucBySearch(keyword,start,end);
            }
            catch (Exception ex)
            {
            }
            rptThuTuc.DataBind();

            if (rptThuTuc.Items.Count == 0 && currentPage != 1)
            {
                currentPage = 1;
                Session.Add("CurrentPage", currentPage);
                Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
            }
        }

        protected void rptThuTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;
        }

        [WebMethod]
        public static string LoadThuTuc(string id)
        {
            List<DMThuTucInfo> thuTucInfo = new List<DMThuTucInfo>();

            int loaithutuc_id = Utils.ConvertToInt32(id, 0);
            try
            {
                //thuTucInfo = new DMThuTuc().GetByLoaiThuTucID(loaithutuc_id);
                thuTucInfo = new DMThuTuc().GetStepsByThuTuc(loaithutuc_id);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(thuTucInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected void Download_Click(Object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn.CommandName == "Download_File")
            {
                string path = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string file_name = btn.CommandArgument.ToString();
                if (file_name != "")
                {
                    string url = path + "UploadFiles\\FileWF\\" + file_name;
                    if (File.Exists(url))
                    {
                        Response.ContentType = "text/plain";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + file_name);
                        Response.TransmitFile(url);
                        Response.End();
                    }
                    else
                    {
                        lblContentSuccess.Text = "File bạn muốn tải không tồn tại !";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                }
                else
                {
                    lblContentSuccess.Text = "File bạn muốn tải không tồn tại !";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            Session.Add("CurrentPage" + Request.Url.AbsolutePath, "1");

            BindRepeater();
        }
    }
}