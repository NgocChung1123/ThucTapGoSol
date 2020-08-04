using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    public partial class CapManage : System.Web.UI.Page
    {
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

                BindRepeater();
                light.Visible = false;
                popXoa.Visible = false;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e) {
            CreatePaging();
        }
        private void CreatePaging() {
            int total = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    total = new Cap().CountAll();
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
                    total = new Cap().CountSearch(keyword);
                }
                catch
                {
                }
            }
            int pageCount = total / Constant.PageSize;
            if (total % Constant.PageSize != 0) pageCount++;
            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)
                    {
                        Label lblPage = new Label();
                        lblPage.Text = (i + 1).ToString();
                        lblPage.CssClass = "current";
                        plhPaging.Controls.Add(lblPage);
                    }
                    else
                    {
                        HyperLink hplPage = new HyperLink();
                        Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                        hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                        hplPage.Text = (i + 1).ToString();
                        plhPaging.Controls.Add(hplPage);
                    }
                }
            }

        }
        //Bind du lieu
        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    rptCap.DataSource = new Cap().GetByPage(currentPage);
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
                    rptCap.DataSource = new Cap().GetBySearch(keyword, currentPage);
                }
                catch
                {
                }
            }
            rptCap.DataBind();

            //neu xoa ban ghi cuoi cung cua trang hien tai, chuyen ve trang truoc
            if (rptCap.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        //Su kien button Edit
        protected void btnSua_Click(object sender, EventArgs e)
        {
            light.Visible = true;
            fade.Attributes.CssStyle["display"] = "block";
            Cap c = new Cap();

            //Chuyen doi du lieu tu button edit click
            LinkButton btnSua = (LinkButton)sender;
            int cap_id = Utils.ConvertToInt32(btnSua.CommandArgument, 0);

            try
            {
                CapInfo cInfo = c.GetByID(cap_id);
                CapID.Text = cInfo.CapID.ToString();
                txtTenCap.Text = cInfo.TenCap;
                capQLy.Text = cInfo.CapQuanLy;
            }
            catch 
            {
                lblContentSuccess.Text = "";
                lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
            }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateServer()) return;

            Cap c = new Cap();

            CapInfo cInfo = new CapInfo();

            if (CapID.Text == string.Empty)
            { //Truong hop them moi du lieu

                cInfo.TenCap = txtTenCap.Text;
                //cInfo.CapQuanLy = capQLy.Text;
                int tt = Utils.ConvertToInt32(capQLy.Text.ToString(), 0);
                if (tt != 0)
                    cInfo.ThuTu = tt;
                
                try
                {
                   int id = c.Insert(cInfo);
                   lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                   lblContentErr.Text = "";
                   ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch
                {
                    lblContentSuccess.Text = "";
                    lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                }
            }
            else
            { //Truong hop update du lieu

                cInfo.CapID = Utils.ConvertToInt32(CapID.Text, 0);
                cInfo.TenCap = txtTenCap.Text;
                //cInfo.CapQuanLy = capQLy.Text;
                int tt = Utils.ConvertToInt32(capQLy.Text.ToString(), 0);
                if (tt != 0)
                    cInfo.ThuTu = tt;
                
                try
                {
                    c.Update(cInfo);
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    lblContentErr.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch 
                {

                    lblContentSuccess.Text = "";
                    lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                }
                
            }
            light.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
            BindRepeater();

        }

        private bool ValidateServer()
        {
            if (String.IsNullOrEmpty(txtTenCap.Text))
            {
                Validate_TenCap.Validate();
                return false;
            }

            if (String.IsNullOrEmpty(capQLy.Text))
            {
                Validate_ThuTu.Validate();
                return false;
            }

            int integer;
            if(!Int32.TryParse(capQLy.Text, out integer))
            {
                cv.Validate();
                return false;
            }

            return true;
        }
        protected void btnThem_Click(object sender, EventArgs e)
        {
            CapID.Text = "";
            txtTenCap.Text = "";
            capQLy.Text = "";
            fade.Attributes.CssStyle["display"] = "block";
            light.Visible = true;
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {

            light.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {

            //Show fade
            fade.Attributes.CssStyle["display"] = "block";
            popXoa.Visible = true;
            LinkButton btnXoa = (LinkButton)sender;
            int cap_id = Utils.ConvertToInt32(btnXoa.CommandArgument, 0);
            id_xoa.Text = cap_id.ToString();
            Cap c = new Cap();
            try
            {
                CapInfo cInfo = c.GetByID(cap_id);
                ten_xoa.Text = cInfo.TenCap;
            }
            catch 
            {
                throw;
            }
        }

        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            Cap c = new Cap();
            int cap_id = Utils.ConvertToInt32(id_xoa.Text,0);
            try
            {
                c.Delete(cap_id);
                lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                lblContentErr.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
            }
            catch
            {

                lblContentSuccess.Text = "";
                lblContentErr.Text = Constant.CONTENT_DELETE_ERROR;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
            }           
            BindRepeater();
            popXoa.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
        }

        protected void btnHuyXoa_Click(object sender, EventArgs e)
        {

            popXoa.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
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
            Response.Redirect(Request.Url.AbsolutePath);
        }
    }
}