using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class ChucVuManage : System.Web.UI.Page
    {
        public int stt = 1;
        public int pageSize = IdentityHelper.GetPageSize();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Read)) { 
                //Redirect
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Create)) {
                btnThem.Enabled = false;
                btnThem.ToolTip = Constant.NoCreate;
                btnThem.CssClass += " disable";
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
                Session["Keyword" + Request.Url.AbsolutePath] = null;
                BindRepeater();
                light.Visible = false;
                popXoa.Visible = false;
                fade.Attributes.CssStyle["display"] = "none";
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
                    total = new ChucVu().CountAll();
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
                    total = new ChucVu().CountSearch(keyword);
                }
                catch
                {
                }
            }
            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = total / PageSize;
            if (total % PageSize != 0) pageCount++;
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
                    rptChucVu.DataSource = new ChucVu().GetByPage(start, end);
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
                    rptChucVu.DataSource = new ChucVu().GetBySearch(keyword, start, end);
                }
                catch
                {
                }
            }
            rptChucVu.DataBind();

            //neu xoa ban ghi cuoi cung cua trang hien tai, chuyen ve trang truoc
            if (rptChucVu.Items.Count == 0)
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
            fade.Attributes.CssStyle["display"] = "block";
            light.Visible = true;
            fade.Visible = true;
            ma.Visible = false;
            popXoa.Visible = false;

            ChucVu c = new ChucVu();

            //Chuyen doi du lieu tu button edit click
            LinkButton btnSua = (LinkButton)sender;
            int cap_id = Utils.ConvertToInt32(btnSua.CommandArgument, 0);

            try
            {
                ChucVuInfo cInfo = c.GetByID(cap_id);
                ChucVuID.Text = cInfo.ChucVuID.ToString();
                txtTenChucVu.Text = cInfo.TenChucVu;
                //capQLy.Text = cInfo.CapQuanLy;
            }
            catch
            {

            }
            // asdfdasfasdf  sd asd fsd sdf sd
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            ChucVu c = new ChucVu();

            ChucVuInfo cInfo = new ChucVuInfo();

            if (ChucVuID.Text == string.Empty)
            { //Truong hop them moi du lieu

                //if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Create)) {
                //    cInfo.TenChucVu = txtTenChucVu.Text;
                //    //cInfo.CapQuanLy = capQLy.Text;
                //    try
                //    {
                //        c.Insert(cInfo);
                //        lblmessageSucsses.Text = "<img src='images/iconInformation.gif' alt='Warning' class='icon img_warning'>Thêm mới thành công !";
                //        lblmessageError.Text = "";
                //    }
                //    catch
                //    {
                //        lblmessageError.Text = "<img src='images/iconWarning.gif' alt='Warning' class='icon img_warning'>Thêm mới không thành công !<br>";
                //        lblmessageSucsses.Text = "";
                //    }
                //} 

                int status = 0;
                cInfo.TenChucVu = txtTenChucVu.Text;
                if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucQuocTich, AccessLevel.Create))
                {
                    try
                    {
                        status = c.Insert(cInfo);
                    }
                    catch
                    {

                        throw;
                    }
                    if (status > 0)
                    {
                        lblHeaderSuccess.InnerHtml = "Thông báo";
                        lblContentSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                    }
                    if (status == -1)
                    {
                        lblHeaderSuccess.InnerHtml = "Lỗi";

                        lblContentSuccess.Text = Constant.MESSAGE_INSERT_ERROR;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                    }
                }  
            }
            else
            { //Truong hop update du lieu

                cInfo.ChucVuID = Utils.ConvertToInt32(ChucVuID.Text, 0);
                cInfo.TenChucVu = txtTenChucVu.Text;
                //cInfo.CapQuanLy = capQLy.Text;
                int status = 0;
                if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Edit)) {
                    try
                    {
                        status=c.Update(cInfo);
                        
                    }
                    catch
                    {
                        lblContentSuccess.Text = "";
                        lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }

                    if (status > 0)
                    {
                        lblHeaderSuccess.InnerHtml = "Thông báo";
                        lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    if (status == -1)
                    {
                        lblHeaderSuccess.InnerHtml = "Lỗi";

                        lblContentSuccess.Text = Constant.MESSAGE_UPDATE_ERROR;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                    }
                }
            }
            light.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
            popXoa.Visible = false;
            ma.Visible = false;
            BindRepeater();
        }
 
        protected void btnThem_Click(object sender, EventArgs e)
        {
            ChucVuID.Text = "";
            txtTenChucVu.Text = "";
            //capQLy.Text = "";
            fade.Attributes.CssStyle["display"] = "block";
            light.Visible = true;
            fade.Visible = true;
            ma.Visible = false ;
            popXoa.Visible = false;
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {

            //Show fade
            fade.Attributes.CssStyle["display"] = "block";
            popXoa.Visible = true;
            light.Visible = false;
            LinkButton btnXoa = (LinkButton)sender;
            int cap_id = Utils.ConvertToInt32(btnXoa.CommandArgument, 0);
            id_xoa.Text = cap_id.ToString();
            ChucVu c = new ChucVu();
            try
            {
                ChucVuInfo cInfo = c.GetByID(cap_id);
                ten_xoa.Text = cInfo.TenChucVu;
            }
            catch
            {
                throw;
            }
        }

        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            ChucVu c = new ChucVu();
            int cap_id = Utils.ConvertToInt32(id_xoa.Text, 0);
            int status = 0;
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Delete)) {
                try
                {
                    status = c.Delete(cap_id);
                    //-------------------------------
                    int total = 0;
                    int curentTotal = 0;
                    int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
                    total = new ChucVu().CountAll();
                    curentTotal = (page - 1) * pageSize;

                    if (total <= curentTotal)
                    {
                        int newpage = 0;
                        newpage = page - 1;
                        Response.Redirect("ChucVuManage.aspx?page=" + newpage);
                    }
                    if (status > 0)
                    {
                        lblHeaderSuccess.InnerHtml = "Thông báo";
                        lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    else
                    {
                        lblHeaderSuccess.InnerHtml = "Lỗi";

                        lblContentSuccess.Text = Constant.CONTENT_DELETE_ERROR;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                    }
                }
                catch
                {
                    lblContentSuccess.Text = "";
                    lblContentErr.Text = Constant.CONTENT_DELETE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                }
            }
            BindRepeater();
            popXoa.Visible = false;
            light.Visible = false;
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
            //Response.Redirect(Request.Url.AbsolutePath);
            //light.Visible = false;

            CreatePaging();
            BindRepeater();
        }

        protected void CloseAddFormClick(object sender, EventArgs e)
        {
            light.Visible = false;
            fade.Visible = false;
        }

        protected void btnKhongXoa_Click(object sender ,EventArgs e)
        {
            light.Visible = false;
            fade.Visible = false;
            popXoa.Visible = false;
        }
        protected void rptChucVu_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Edit)) {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.NoEdit;
                btnEdit.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucChucVu, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.NoDelete;
                btnDelete.CssClass += " disable";
            }

            //------------------Danh STT----------------------------
            int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
            if (page == null)
                page = 1;
            stt = (page - 1) * pageSize + 1;
        }
    }
}