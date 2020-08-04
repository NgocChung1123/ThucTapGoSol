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
    public partial class DanTocManage : System.Web.UI.Page
    {
        public int stt = 1;
        public int pageSize = IdentityHelper.GetPageSize();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc,AccessLevel.Read)){
                //Redirect
                Response.Redirect("~");
            }
            if(!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc,AccessLevel.Create)){
                //btnThem.Enabled=false;
                //btnThem.ToolTip=Constant.NoCreate;
                //btnThem.CssClass += " disable";
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
                //light.Visible = false;
                //popXoa.Visible = false;
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
                    total = new DanToc().CountAll();
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
                    total = new DanToc().CountSearch(keyword);
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
                    rptDanToc.DataSource = new DanToc().GetByPage(start, end);
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
                    rptDanToc.DataSource = new DanToc().GetBySearch(keyword, start, end);
                }
                catch
                {
                }
            }
            rptDanToc.DataBind();
        }

        //Su kien button Edit
        protected void btnSua_Click(object sender, EventArgs e)
        {
            //light.Visible = true;
            //popXoa.Visible = false;
            //fade.Attributes.CssStyle["display"] = "block";
            DanToc c = new DanToc();

            //Chuyen doi du lieu tu button edit click
            LinkButton btnSua = (LinkButton)sender;
            int DanToc_id = Utils.ConvertToInt32(btnSua.CommandArgument, 0);

            try
            {
                DanTocInfo DTInfo = c.GetByID(DanToc_id);
                DanTocID.Text = DTInfo.DanTocID.ToString();
                txtTenDanToc.Text = DTInfo.TenDanToc;
                hdfDanTocIDXoa.Value = DTInfo.DanTocID.ToString();
                
            }
            catch
            {
                
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "showThemDanToc", "showThemDanToc();", true);
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {   
            DanToc DT = new DanToc();

            DanTocInfo DTInfo = new DanTocInfo();
            if (hdfDanTocIDXoa.Value == string.Empty)
                { //Truong hop them moi du lieu
                    DTInfo.TenDanToc = txtTenDanToc.Text;
                    int status = 0;
                     if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Create)){

                        try
                        {
                            status = DT.Insert(DTInfo);
                        }
                        catch
                        {
                            throw;
                        }
                        if (status > 0)
                        {
                            //lblHeaderSuccess.InnerHtml = "Thông báo";
                            lblContentSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                        }
                        if (status == -1)
                        {
                            //lblHeaderSuccess.InnerHtml = "Lỗi";

                            lblContentSuccess.Text = Constant.MESSAGE_INSERT_ERROR;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                        }
                    }   
                }
                else
                { //Truong hop update du lieu

                    DTInfo.DanTocID = Utils.ConvertToInt32(hdfDanTocIDXoa.Value, 0);
                    DTInfo.TenDanToc = txtTenDanToc.Text;
                    int status = 0;
                    if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Edit)) {
                        try
                        {
                            status = DT.Update(DTInfo);
                            if (status > 0) {
                                //lblHeaderSuccess.InnerHtml = "Thông báo";
                            lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                            }
                            else
                            {
                                //lblHeaderSuccess.InnerHtml = "Lỗi";

                                lblContentErr.Text = Constant.MESSAGE_UPDATE_ERROR;
                                lblContentSuccess.Text = "";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);

                            }
                            hdfDanTocIDXoa.Value = string.Empty;

                        }
                        catch
                        {
                            //lblContentSuccess.Text = "";
                            //lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                            //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);

                        }
                    }
                }


                //light.Visible = false;
                //popXoa.Visible = false;
                fade.Attributes.CssStyle["display"] = "none";
            BindRepeater();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            DanTocID.Text = "";
            txtTenDanToc.Text = "";
            
            //fade.Attributes.CssStyle["display"] = "block";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showThemDanToc", "showThemDanToc();", true);
            //light.Visible = true;
            //popXoa.Visible = false;
            
        }
 
        //protected void btnXoa_Click(object sender, EventArgs e)
        //{

        //    //Show fade
        //    //fade.Attributes.CssStyle["display"] = "block";
        //    //popXoa.Visible = true;
        //    //light.Visible = false;
            

        //    LinkButton btnXoa = (LinkButton)sender;
        //    int DanToc_id = Utils.ConvertToInt32(btnXoa.CommandArgument, 0);
        //    //id_xoa.Text = DanToc_id.ToString();
        //    DanToc DT = new DanToc();
        //    try
        //    {
        //        DanTocInfo cInfo = DT.GetByID(DanToc_id);
        //        //ten_xoa.Text = cInfo.TenDanToc;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            DanToc DT = new DanToc();
            int status = 0;
            int DT_id = Utils.ConvertToInt32(hdfDanTocIDXoa.Value, 0);
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Delete))
            {
                try
                {
                    status = DT.Delete(DT_id);
                    //-------------------------------
                    int total = 0;
                    int curentTotal = 0;
                    int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
                    total = new DanToc().CountAll();
                    curentTotal = (page - 1) * pageSize;

                    if (total <= curentTotal)
                    {
                        int newpage = 0;
                        newpage = page - 1;
                        Response.Redirect("DanTocManage.aspx?page=" + newpage);
                    }
                    if (status > 0)
                    {
                        //lblHeaderSuccess.InnerHtml = "Thông báo";
                        lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                    }
                    else
                    {
                        //lblHeaderSuccess.InnerHtml = "Lỗi";

                        lblContentErr.Text = Constant.CONTENT_DELETE_ERROR;
                        lblContentSuccess.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);

                    }
                }
                catch
                {
                    //lblContentSuccess.Text = "";
                    //lblContentErr.Text = Constant.CONTENT_DELETE_ERROR;
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);

                }
            }
            BindRepeater();

            ////popXoa.Visible = false;
            ////light.Visible = false;
            //fade.Attributes.CssStyle["display"] = "none";
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
            BindRepeater();
        }

        protected void rptDanToc_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Edit)) {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.NoEdit;
                btnEdit.CssClass += " disable"; 
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.NoDelete;
                btnDelete.CssClass += " disable";
            }

            int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
            if (page == null)
                page = 1;
            stt = (page - 1) * pageSize + 1;
        }
    }
}