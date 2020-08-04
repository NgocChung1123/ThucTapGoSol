using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;
using System.Text;
using System.Configuration;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Web.Role;

namespace Com.Gosol.CMS.Web
{
    public partial class PhongBanManage : System.Web.UI.Page
    {
        public int stt = 1;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            

            //Check quyen
            
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Read))
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Create))
            {
                btnThem.Enabled = false;
                btnThem.ToolTip = Constant.ToolTip;
                btnThem.CssClass += " disable";
                btnThem.OnClientClick = "return false;";                
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                btnThem.Enabled = false;
                btnThem.ToolTip = Constant.ToolTip;
                btnThem.CssClass += " disable";
                btnThem.OnClientClick = "";
                udpCanBo.Update();
            }
                        
            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                    Session.Add("CurrentPage", page);
                else Session["CurrentPage"] = page;
                //BindRepeater();
                BindPhongBanRepeater();
                BindCanBoDDL();         
                BindCoQuanDDL();
                BindPhongBanDDL();                
            }
            else
            {
                light.Attributes.CssStyle["display"] = "none";
                fade.Attributes.CssStyle["display"] = "none";
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
            String keyword = Utils.ConvertToString(Session["Keyword"+Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            
                try
                {
                    keyword = "%" + keyword + "%";
                    totalRow = new DAL.PhongBan().Count(keyword);
                }
                catch
                {
                }
            
            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0) pageCount++;
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
            BindRepeater();
            createPaging();
        }
        
        

        private void TreeSort(ref List<CoQuanInfo> cqList, CoQuanInfo parentInfo, int level)
        {
            String prefix = String.Empty;
            String delta = "---- ";
            for (int i = 0; i < level-1; i++)
            {
                prefix += delta;
            }
            level++;
            parentInfo.TenCoQuan = prefix + parentInfo.TenCoQuan;
            if (parentInfo.CoQuanChaID != 0)
            {
                cqList.Add(parentInfo);
            }
            List<CoQuanInfo> childList = new DAL.CoQuan().GetCoQuanByParentID(parentInfo.CoQuanID).ToList();
            foreach (CoQuanInfo childInfo in childList)
            {
                TreeSort(ref cqList, childInfo, level);
            }
        }

        private void BindCanBoDDL()
        {
            //if (ddlPhongBan.SelectedValue != "")
            //{

            //    int pbid = Utils.GetInt32(ddlPhongBan.SelectedValue, 0);
            //    if (pbid != 0)
            //    {
            //        PhongBanInfo pb = new DAL.PhongBan().GetByID(pbid);
            //        List<CanBoInfo> canboList = new DAL.CanBo().GetByCoQuanID(pb.CoQuanID).ToList();
            //        ddlCanBo.DataSource = canboList;
            //        ddlCanBo.DataBind();
            //    }
            //}
            
            IList<CanBoInfo> ListInfo = new List<CanBoInfo>();
            try
            {
                ListInfo = new DAL.CanBo().GetAllCanBo();
            }
            catch
            {

            }
            if (ListInfo != null)
            {
                for (int i = ListInfo.Count - 1; i >= 0; i--)
                {
                    ListInfo[i].TenCanBo = ListInfo[i].TenCanBo + " (" + ListInfo[i].TenCoQuan + ")";
                    if (ListInfo[i].PhongBanID != 0 || ListInfo[i].RoleName == "Lãnh đạo đơn vị")
                    {
                        ListInfo.RemoveAt(i);
                        
                    }
                    
                }
            }
            //ddlCanBo.DataSource = null;
            //ddlCanBo.Items.Clear();
            ddlCanBo.DataSource = ListInfo;
            ddlCanBo.DataBind();
            ddlCanBo.Items.Insert(0, new ListItem("Chọn cán bộ", "0"));
            try
            {
                ddlCanBo.SelectedIndex = 0;
            }
            catch
            {
            }
            
        }        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //NguoiDungInfo nguoiDungInfo = GetNguoiDungInfo();
            PhongBanInfo pbinfo = new PhongBanInfo();
            pbinfo.PhongBanID = Utils.ConvertToInt32(txtPhongBanID.Text, 0);
            pbinfo.TenPhongBan = txtTenPhongBan.Text;
            pbinfo.GhiChu = txtGhiChu.Text;
            pbinfo.CoQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0);
            pbinfo.SoDienThoai = Utils.ConvertToString(txtDienthoai.Text, string.Empty);

            if (pbinfo.PhongBanID != 0)
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
                {
                    //PhongBanInfo pb = new DAL.PhongBan().GetByID(pbinfo.PhongBanID);
                    
                    try
                    {
                       new DAL.PhongBan().Update(pbinfo);
                       lblHeaderSuccess.InnerHtml = "Thông báo";
                       lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                       lblContentErr.Text = "";
                       ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                       //int kq=  new DAL.PhongBan().Update(pbinfo);
                       //if (kq > 0)
                       //{
                       //    lblHeaderSuccess.InnerHtml = "Thông báo";
                       //    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                       //    lblContentErr.Text = "";
                       //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                       //}
                       //if (kq == -1)
                       //{
                       //    lblHeaderSuccess.InnerHtml = "Lỗi";

                       //    lblContentSuccess.Text = Constant.MESSAGE_UPDATE_ERROR;
                       //    lblContentErr.Text = "";
                       //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                       //}
                    }
                    catch
                    {
                        lblContentSuccess.Text = "";
                        lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            else
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Create))
                {
                    int result = 0;
                    
                    try
                    {
                        result = new DAL.PhongBan().Insert(pbinfo);
                        if (result > 0)
                        {
                            lblHeaderSuccess.InnerHtml = "Thông báo";
                            lblContentSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                        //if (result == -1)
                        //{
                        //    lblHeaderSuccess.InnerHtml = "Lỗi";

                        //    lblContentSuccess.Text = Constant.MESSAGE_INSERT_ERROR;
                        //    lblContentErr.Text = "";
                        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                        //}
                    }
                    catch
                    {
                        lblHeaderSuccess.InnerHtml = "Lỗi";

                        lblContentSuccess.Text = Constant.MESSAGE_INSERT_ERROR;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                    //if (result == 0)
                    //{
                    //    lblContentErr.Text = "Lỗi kết nối đến CSDL";
                    //    lblContentSuccess.Text = "";
                    //    //lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    //}
                    //else if (result == -1)
                    //{
                    //    lblContentErr.Text = "Tên đăng nhập đã tồn tại";
                    //    lblContentSuccess.Text = "";
                    //    //lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    //}
                    //else if (result == -2)
                    //{
                    //    lblContentErr.Text = "Cán bộ đã được gán cho người dùng khác";
                    //    lblContentSuccess.Text = "";
                    //    //lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    //}
                }
            }
            ClearForm();
            BindRepeater();
            BindPhongBanRepeater();
            //BindCanBoDDL();

            //light.Attributes.CssStyle["display"] = "none";
            //fade.Attributes.CssStyle["display"] = "none";
        }

        protected void ClearForm()
        {
            txtPhongBanID.Text = "";
            txtTenPhongBan.Text = string.Empty;
            txtDienthoai.Text = "";
            txtGhiChu.Text = "";
            ddlCoQuan.SelectedValue = "0";
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            String keyword = Utils.ConvertToString(Session["Keyword"+Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            
                try
                {
                    keyword = "%" + keyword + "%";
                    rptPhongBan.DataSource = new DAL.PhongBan().GetBySearch(start, end, keyword);
                }
                catch
                {
                }
            
            rptPhongBan.DataBind();
            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptPhongBan.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        

        protected void rptPhongBan_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int pbid = Utils.ConvertToInt32(e.CommandArgument, 0);            
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                if (e.CommandName == "Edit")
                {
                    if (pbid != 0)
                    {
                        PhongBanInfo pbinfo = new DAL.PhongBan().GetByID(pbid);
                        txtPhongBanID.Text = pbinfo.PhongBanID.ToString();
                        txtTenPhongBan.Text = pbinfo.TenPhongBan.ToString();
                        txtGhiChu.Text = pbinfo.GhiChu.ToString();
                        txtDienthoai.Text = pbinfo.SoDienThoai.ToString();

                        try
                        {
                            ddlCoQuan.SelectedValue = Utils.ConvertToString(pbinfo.CoQuanID, "0");
                        }
                        catch
                        {
                        }
                        //FillFormData(ndInfo);
                    }

                    //show popup form                    
                    //txtTenNguoiDung.Enabled = false;
                    light.Attributes.CssStyle["display"] = "block";
                    fade.Attributes.CssStyle["display"] = "block";
                    //success.Attributes.CssStyle["display"] = "none";                    
                }
                else if (e.CommandName == "Refresh")
                {                    
                }
                else if (e.CommandName == "Delete")
                {                    
                }
                //BindRepeater();
                BindPhongBanRepeater();
            }
        }

        
        private void BindCoQuanDDL()
        {
            ddlCoQuan.DataSource = new DAL.CoQuan().GetAllCoQuan();
            ddlCoQuan.DataBind();            
            ddlCoQuan.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            try {
                ddlCoQuan.SelectedIndex = 0;
            }
            catch {
            }
        }

        private void BindPhongBanDDL()
        {
            ddlPhongBan.DataSource = new DAL.PhongBan().GetAll();
            ddlPhongBan.DataBind();
            ddlPhongBan.Items.Insert(0, new ListItem("Chọn phòng ban", "0"));
            try
            {
                ddlPhongBan.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        protected void ddlPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pbid = Utils.ConvertToInt32(ddlPhongBan.SelectedValue, 0);
            if (pbid != 0)
            {
                //Fake sleep
                System.Threading.Thread.Sleep(500);

                BindCanBoRepeater(pbid);
                BindCanBoDDL();
            }
        }

        

        private void BindPhongBanRepeater()
        {
            int tinhid = IdentityHelper.GetTinhID();
            List<CoQuanInfo> cqList = new List<CoQuanInfo>();
            try
            {
                
                //cqtList = new DAL.CoQuan().GetParents(tinhid).ToList();
                //        // get co quan by cap id
                cqList = new DAL.CoQuan().GetCoQuanByTinhID(tinhid).ToList();
            }
            catch
            {
            }

            List<PhongBanInfo> resultList = new List<PhongBanInfo>();
            foreach (CoQuanInfo cqInfo in cqList)
            {
                // chuyen co quan sang kieu phong ban info, co quan la phong ban dau tien
                PhongBanInfo coquan = new PhongBanInfo();
                //coquan.PhongBanID = cqInfo.CoQuanID;
                coquan.TenCoQuan = cqInfo.TenCoQuan;
                coquan.CQCheck = true;
                resultList.Add(coquan);

                IList<PhongBanInfo> pb = new List<PhongBanInfo>();
                pb = new DAL.PhongBan().GetByCoQuanID(cqInfo.CoQuanID);

                foreach (PhongBanInfo Info in pb)
                {
                    resultList.Add(Info);
                }

            }

            
            rptPhongBan.DataSource = resultList;
            rptPhongBan.DataBind();
        }

        protected void rptPhongBan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            Label lblTenCoQuan = (Label)e.Item.FindControl("lblTenCoQuan");
            Label lblTenPhongBan = (Label)e.Item.FindControl("lblTenPhongBan");
            

            PhongBanInfo info = (PhongBanInfo)e.Item.DataItem;

            if (info.CQCheck == true)
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                lblTenCoQuan.Text = info.TenCoQuan;
                lblTenPhongBan.Text = "";
            }
            else
            {
                lblTenCoQuan.Text = "";
                lblTenPhongBan.Text =  info.TenPhongBan;
            }

            
        }

        

        protected void btnSaveCB_Click(object sender, EventArgs e)
        {
            int cbid = Utils.ConvertToInt32(ddlCanBo.SelectedValue, 0);
            int pbid = Utils.ConvertToInt32(ddlPhongBan.SelectedValue, 0);
            int kq = 0;

            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                if (cbid != 0 && pbid != 0)
                {
                    try
                    {
                        kq = new DAL.CanBo().SetPhongBan(cbid, pbid);
                        if (kq > 0)
                        {
                            lblHeaderSuccess.InnerHtml = "Thông báo";
                            lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                            //lblContentErr.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                            //ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                        }
                        else
                        {
                            lblHeaderSuccess.InnerHtml = "Lỗi";
                            lblContentSuccess.Text = Constant.CONTENT_MESSAGE_ERROR;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                        

                    }
                    catch
                    {
                    }
                }
                BindCanBoRepeater(pbid);
            }
            //addCanBoForm.Attributes.CssStyle["display"] = "none";
            //fade.Attributes.CssStyle["display"] = "none";
            BindCanBoDDL();
        }

        private void BindCanBoRepeater(int pbid)
        {
            rptCanBo.DataSource = new DAL.CanBo().GetByPhongBanID(pbid).ToList();
            rptCanBo.DataBind();
        }

        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int pbid = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            int status = 0;
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Delete))
            {
                if (pbid != 0)
                {
                    try
                    {
                        status = new DAL.PhongBan().Delete(pbid);
                        if (status != 0)
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
            }
            //deleteConfirm.Attributes.CssStyle["display"] = "none";
            //fade.Attributes.CssStyle["display"] = "none";
            hdDeleteID.Value = String.Empty;
            //BindRepeater();
            BindPhongBanRepeater();
        }

        

        protected void btnDeleteCB_Click(object sender, EventArgs e)
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                int cbid = Utils.ConvertToInt32(hdDeleteCanBoID.Value, 0);
                int kq = 0;
                if (cbid != 0)
                {
                    int pbid = Utils.ConvertToInt32(ddlPhongBan.SelectedValue, 0);
                    if (pbid != 0)
                    {
                        try
                        {
                            kq = new DAL.CanBo().SetPhongBan(cbid, 0);
                            if (kq > 0)
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
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                    }
                    BindCanBoRepeater(pbid);
                }
            }
            //canboDeleteConfirm.Attributes.CssStyle["display"] = "none";
            //fade.Attributes.CssStyle["display"] = "none";
            hdDeleteCanBoID.Value = String.Empty;
            //BindRepeater();
            BindPhongBanRepeater();
            BindCanBoDDL();
        }

        protected String SetSelected(int phID)
        {
            int pbid = Utils.ConvertToInt32(ddlPhongBan.SelectedValue, 0);
            if (phID == pbid)
            {
                return "selected_hl";
            }
            else return String.Empty;
        }

        
    }
}