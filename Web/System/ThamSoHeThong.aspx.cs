using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    public partial class ThamSoHeThong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoThamSoHeThong, AccessLevel.Read))
            {
                Response.Redirect("~");
            }

            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                    Session.Add("CurrentPage", page);
                else Session["CurrentPage"] = page;
                Session["Keyword" + Request.Url.AbsolutePath] = null;
                BindRepeater();
            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Hệ thống");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    totalRow = new DAL.SystemConfig().CountAll();
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
                    totalRow = new DAL.SystemConfig().CountSearch(keyword);
                }
                catch
                {
                }
            }
            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0) pageCount++;
            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
            }
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();

            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    rptSystemConfig.DataSource = new DAL.SystemConfig().GetByPage(start, end);
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
                    rptSystemConfig.DataSource = new DAL.SystemConfig().GetBySearch(keyword, start, end);
                }
                catch
                {
                }
            }
            rptSystemConfig.DataBind();
            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptSystemConfig.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
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
            createPaging();
            BindRepeater();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SystemConfigInfo configInfo = new SystemConfigInfo();
            configInfo.SystemConfigID = Utils.ConvertToInt32(txtSystemConfigID.Text, 0);
            configInfo.ConfigKey = txtConfigKey.Text;
            configInfo.ConfigValue = txtConfigValue.Text;
            configInfo.Description = txtDescription.Text;
            int kq = 0;
            try
            {
                kq = new DAL.SystemConfig().Update(configInfo);
                if (kq != 0)
                {
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;                
                    lblContentErr.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showThongBaoSuccess", "showThongBaoSuccess();", true);
                }
                else
                {
                    lblContentSuccess.Text = "";
                    lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
                }
            }
            catch
            {
                lblContentSuccess.Text = "";
                lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
            }
            //light.Attributes.CssStyle["display"] = "none";
            //fade.Attributes.CssStyle["display"] = "none";
            BindRepeater();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
        }

        protected void rptSystemConfig_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");

            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoThamSoHeThong, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.CssClass += " disable";
                btnEdit.ToolTip = Constant.ToolTip;
            }
        }

        protected void rptSystemConfig_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int configID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (e.CommandName == "Edit")
            {
                SystemConfigInfo configInfo = new SystemConfigInfo();
                try
                {
                    configInfo = new DAL.SystemConfig().GetByID(configID);
                }
                catch
                {
                }
                txtSystemConfigID.Text = configID.ToString();
                txtConfigKey.Text = configInfo.ConfigKey;
                txtConfigValue.Text = configInfo.ConfigValue;
                txtDescription.Text = configInfo.Description;

                //light.Attributes.CssStyle["display"] = "block";
                //fade.Attributes.CssStyle["display"] = "block";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showEditFormThamSoHeThong", "showEditFormThamSoHeThong();", true);
            }
        }
    }
}