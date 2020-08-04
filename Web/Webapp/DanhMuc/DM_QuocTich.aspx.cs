using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.SoftWare.DanhMuc
{
    public partial class DM_QuocTich : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucQuocTich, AccessLevel.Read))
            {
                //Redirect
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucQuocTich, AccessLevel.Create)) 
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
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

                if (Session["keyword" + Request.Url.AbsolutePath] != null)
                {
                    txtSearch.Text = Session["keyword" + Request.Url.AbsolutePath].ToString();
                }
                BindRepeater();
                CreatePaging();
            }
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            string keyword = txtSearch.Text.Trim();
            if (Session["keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["keyword" + Request.Url.AbsolutePath] = keyword;
            }
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            try
            {
                keyword = "%" + keyword + "%";
                rptQuocTich.DataSource = new QuocTich().GetBySearch(keyword, start, end);
            }
            catch
            {
            }
            rptQuocTich.DataBind();

            //neu xoa ban ghi cuoi cung cua trang hien tai, chuyen ve trang truoc
            if (rptQuocTich.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        private void CreatePaging()
        {
            int total = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            try
            {
                keyword = "%" + keyword + "%";
                total = new QuocTich().CountSearch(keyword);
            }
            catch
            {
            }

            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = total / PageSize;
            if (total % PageSize != 0) pageCount++;
            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                PagingHelper.CreatePaging(total, currentPage, ref plhPaging);
            }

        }

        [WebMethod]
        public static string GetByID(string quocTichID)
        {
            int quoctichid = Utils.ConvertToInt32(quocTichID, 0);
            QuocTichInfo Info = new QuocTichInfo();
            try
            {
                Info = new DAL.QuocTich().GetByID(quoctichid);
            }
            catch
            {
            }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(Info);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRepeater();
            CreatePaging();
        }

        protected void rptQuocTich_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected QuocTichInfo GetQuocTichInfo()
        {
            QuocTichInfo qtInfo = new QuocTichInfo();
            qtInfo.QuocTichID = Utils.ConvertToInt32(hdfQuocTichID.Value, 0);
            qtInfo.TenQuocTich = txtTenQuocTich.Text;
            return qtInfo;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            QuocTichInfo quocTichInfo = GetQuocTichInfo();
            if (quocTichInfo.QuocTichID == 0)
            { //Truong hop them moi du lieu
                if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucQuocTich, AccessLevel.Create))
                {
                    try
                    {
                        new QuocTich().Insert(quocTichInfo);
                        lblMesSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    catch
                    {
                        lblMesError.Text = Constant.MESSAGE_INSERT_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            else
            { //Truong hop update du lieu
                if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucQuocTich, AccessLevel.Edit))
                {
                    try
                    {
                        new QuocTich().Update(quocTichInfo);
                        lblMesSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    catch
                    {
                        lblMesError.Text = Constant.MESSAGE_UPDATE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "clearForm", "clearForm()", true);
            BindRepeater();
            CreatePaging();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int quocTichID = Utils.ConvertToInt32(hdfDelete.Value, 0);
            try
            {
                new QuocTich().Delete(quocTichID);
                lblMesSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
            }
            catch
            {
                lblMesError.Text = Constant.CONTENT_DELETE_ERROR;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
            }
            BindRepeater();
            CreatePaging();
            hdfDelete.Value = "0";
        }
    }
}