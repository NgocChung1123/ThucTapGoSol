using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class SystemLog : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                int currentPage = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", currentPage);
                }
                else
                {
                    Session["CurrentPage"] = currentPage;
                }
                var keyWord = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath],string.Empty);
                if(keyWord == string.Empty)
                {
                    Session["Keyword" + Request.Url.AbsolutePath] = null;
                }
                
                BindRepeater();
            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Danh mục");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            CreatePaging();
        }

        private void CreatePaging()
        {
            int total = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    total = new DAL.SystemLog().CountAll();
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    keyword = "%" + keyword + "%";
                    total = new DAL.SystemLog().CountSearch(keyword);
                    
                }
                catch
                {
                }
            }
            int pageCount = total / IdentityHelper.GetPageSize();
            if (total % IdentityHelper.GetPageSize() != 0) pageCount++;
            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                //for (int i = 0; i < pageCount; i++)
                //{
                //    if (i == currentPage - 1)
                //    {
                //        Label lblPage = new Label();
                //        lblPage.Text = (i + 1).ToString();
                //        lblPage.CssClass = "current";
                //        plhPaging.Controls.Add(lblPage);
                //    }
                //    else
                //    {
                //        HyperLink hplPage = new HyperLink();
                //        Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                //        hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                //        hplPage.Text = (i + 1).ToString();
                //        plhPaging.Controls.Add(hplPage);
                //    }
                //}
                PagingHelper.CreatePaging(total, currentPage, ref plhPaging);
            }

        }
        //Bind du lieu
        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();

            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    rptSystemLog.DataSource = new DAL.SystemLog().GetByPage(start, end);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    keyword = "%" + keyword + "%";
                    rptSystemLog.DataSource = new DAL.SystemLog().GetBySearch(keyword, start, end);
                }
                catch
                {
                }
            }
            rptSystemLog.DataBind();
            //neu xoa ban ghi cuoi cung cua trang hien tai, chuyen ve trang truoc
            if (rptSystemLog.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            String keyword = txtSearch.Text.Trim();
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            //Response.Redirect(Request.Url.AbsolutePath);
            //CreatePaging();
            BindRepeater();
        }

        protected void rptSystemLog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            Label lblLogTime = (Label)e.Item.FindControl("lblLogTime");

            SystemLogInfo info = (SystemLogInfo)e.Item.DataItem;
            if(info.LogTime != null){
                lblLogTime.Text = Convert.ToDateTime(info.LogTime).ToString("dd/MM/yyyy HH:mm:ss"
                , System.Globalization.CultureInfo.InvariantCulture); ;
            }
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * Constant.PageSize).ToString();
            stt++;
        }
    }
}