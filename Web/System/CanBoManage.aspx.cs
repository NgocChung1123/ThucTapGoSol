using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class CanBoManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Read))
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Create))
            {
                //btnThem.OnClientClick = "return false;";
                //btnThem.ToolTip = Constant.ToolTip;
                //btnThem.CssClass += " disable";
            }

            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                    Session.Add("CurrentPage", page);
                else Session["CurrentPage"] = page;
                //Session["Keyword" + Request.Url.AbsolutePath] = null;
                BindRepeater();
                BindCoQuanDDL();
                BindChucVuDDL();
              
            }
            else
            {

            }            
        }

        #region paging
        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            int loaiCanBo = Utils.ConvertToInt32(ddlLoai.SelectedValue, 1);
            txtSearch.Text = keyword;
            if (Session["LoaiCanBo_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiCanBo_" + Request.Url.AbsolutePath, loaiCanBo);
            }
            else
            {
                Session["LoaiCanBo_" + Request.Url.AbsolutePath] = loaiCanBo;
            }

            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();

            try
            {
                keyword = "%" + keyword + "%";
                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    totalRow = new DAL.CanBo().CountSearch(keyword, loaiCanBo);
                else
                    totalRow = new DAL.CanBo().CountByCoQuanChaID(keyword, coQuanUserID, loaiCanBo);
            }
            catch
            {
            }
            //if (String.IsNullOrEmpty(keyword))
            //{
            //    try
            //    {
            //        totalRow = new DAL.CanBo().CountAll();
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
                
            //}

            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0) pageCount++;
            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
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
            }




        }
        #endregion

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

            BindRepeater();
            //createPaging();
        }

        #region bild ddl
        private void BindCoQuanDDL()
        {
            List<CoQuanInfo> cqList = new List<CoQuanInfo>();
            List<CoQuanInfo> parentList = new List<CoQuanInfo>();

            int capID = IdentityHelper.GetCapID();
            int coQuanUserID = IdentityHelper.GetCoQuanID();

            if(capID == (int)CapQuanLy.CapUBNDTinh){
                parentList = new DAL.CoQuan().GetParents().ToList();
            }
            else
            {
                parentList = new DAL.CoQuan().GetCoQuanByCoQuanID(coQuanUserID).ToList();
            }

            foreach (CoQuanInfo parentInfo in parentList)
            {
                TreeSort(ref cqList, parentInfo, 0);
            }

            ddlCoQuan.DataSource = cqList;

            ddlCoQuan.Items.Add(new ListItem("Chọn một đơn vị bên dưới", "0"));
            ddlCoQuan.DataBind();

            //FIX cho ban TTCP: CoQuan set mac dinh la TTCP
            //ddlCoQuan.SelectedValue = "1";
        }

        private void BindChucVuDDL()
        {
            List<ChucVuInfo> cvList = new DAL.ChucVu().GetAll().ToList();
            ddlChucVu.DataSource = cvList;
            ddlChucVu.DataBind();
            ddlChucVu.Items.Insert(0, new ListItem("Chọn một chức vụ bên dưới", ""));
        }

        private void TreeSort(ref List<CoQuanInfo> cqList, CoQuanInfo parentInfo, int level)
        {
            String prefix = String.Empty;
            String delta = "";
            for (int i = 0; i < level; i++)
            {
                prefix += delta;
            }
            level++;
            parentInfo.TenCoQuan = prefix + parentInfo.TenCoQuan;

            cqList.Add(parentInfo);

            List<CoQuanInfo> childList = new DAL.CoQuan().GetCoQuanByParentID(parentInfo.CoQuanID).ToList();
            foreach (CoQuanInfo childInfo in childList)
            {
                TreeSort(ref cqList, childInfo, level);
            }
        }
        #endregion

        #region submit form
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CanBoInfo canBoInfo = GetCanBoInfo();

            if (canBoInfo.CanBoID != 0)
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Edit))
                {
                    int kq = 0;
                    try
                    {
                        kq = new DAL.CanBo().Update(canBoInfo);
                        if (kq != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            else
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Create))
                {
                    try
                    {
                        int cbID = new DAL.CanBo().Insert(canBoInfo);
                        if (cbID != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            ClearForm();
            BindRepeater();
        }
        #endregion

        
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hdCanBoID.Value = String.Empty;
            txtTenCanBo.Text = String.Empty;
            txtNgaySinh.Text = String.Empty;
            txtDiaChi.Text = String.Empty;
            ddlGioiTinh.SelectedIndex = 0;
            ddlCoQuan.SelectedIndex = 0;
            ddlChucVu.SelectedIndex = 0;
        }

        #region info
        private CanBoInfo GetCanBoInfo()
        {
            CanBoInfo canBoInfo = new CanBoInfo();
            canBoInfo.CanBoID = Utils.ConvertToInt32(hdCanBoID.Value, 0);
            canBoInfo.TenCanBo = txtTenCanBo.Text;
            canBoInfo.NgaySinh = Utils.ConvertToDateTime(txtNgaySinh.Text, Constant.DEFAULT_DATE);
            canBoInfo.DiaChi = txtDiaChi.Text;
            canBoInfo.GioiTinh = Utils.ConvertToInt32(ddlGioiTinh.SelectedValue, 0);
            canBoInfo.CoQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0);
            if (cbxQuyenKy.Checked) canBoInfo.QuyenKy = 1;
            else canBoInfo.QuyenKy = 0;
            canBoInfo.Email = txtEmail.Text.Trim();
            canBoInfo.DienThoai = txtDienThoai.Text.Trim();
            canBoInfo.ChucVuID = Utils.ConvertToInt32(ddlChucVu.SelectedValue, 0);
            canBoInfo.RoleID = Utils.ConvertToInt32(ddlVaiTro.SelectedValue, 0);
            return canBoInfo;
        }

        private void FillFormData(CanBoInfo canBoInfo)
        {
            hdCanBoID.Value = canBoInfo.CanBoID.ToString();
            txtTenCanBo.Text = canBoInfo.TenCanBo;

            if (Format.FormatDate(canBoInfo.NgaySinh) != "01/01/1753")
                txtNgaySinh.Text = Format.FormatDate(canBoInfo.NgaySinh);
            else
                txtNgaySinh.Text = "";
            txtDiaChi.Text = canBoInfo.DiaChi;
            ddlGioiTinh.SelectedValue = Utils.ConvertToString(canBoInfo.GioiTinh, "0");
            try
            {
                ddlCoQuan.SelectedValue = canBoInfo.CoQuanID.ToString();
                ddlChucVu.SelectedValue = canBoInfo.ChucVuID.ToString();
                ddlVaiTro.SelectedValue = canBoInfo.RoleID.ToString();
            }
            catch
            {
            }

            if (canBoInfo.QuyenKy > 0) cbxQuyenKy.Checked = true;
            else cbxQuyenKy.Checked = false;
            txtEmail.Text = canBoInfo.Email;
            txtDienThoai.Text = canBoInfo.DienThoai;
        }
        #endregion

        #region repeater 
        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            int loaiCanBo = Utils.ConvertToInt32(ddlLoai.SelectedValue, 1);
            txtSearch.Text = keyword;
            if (Session["LoaiCanBo_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiCanBo_" + Request.Url.AbsolutePath, loaiCanBo);
            }
            else
            {
                Session["LoaiCanBo_" + Request.Url.AbsolutePath] = loaiCanBo;
            }
            if (currentPage == 0)
            {
                currentPage = 1;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();

            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();

            try
            {
                keyword = "%" + keyword + "%";
                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    rptCanBo.DataSource = new DAL.CanBo().GetBySearch(keyword, start, end, loaiCanBo);
                else
                    rptCanBo.DataSource = new DAL.CanBo().GetByCoQuanChaID(keyword, start, end, coQuanUserID, loaiCanBo);
            }
            catch
            {
            }
            //if (String.IsNullOrEmpty(keyword))
            //{
            //    try
            //    {
            //        rptCanBo.DataSource = new DAL.CanBo().GetByPage(start, end);
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
                
            //}
            rptCanBo.DataBind();
            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptCanBo.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }


        protected void rptCanBo_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int canBoID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (e.CommandName == "Edit")
            {
                if (canBoID != 0)
                {
                    CanBoInfo canBoInfo = new DAL.CanBo().GetCanBoByID(canBoID);
                    FillFormData(canBoInfo);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowAddCanBoForm", "ShowAddCanBoForm()", true);
                }
                
                
            }
            else if (e.CommandName == "Delete")
            {
            }

           // createPaging();
        }

        protected void rptCanBo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CanBoJoinInfo canBoInfo = (CanBoJoinInfo)e.Item.DataItem;

            Label lblGioiTinh = (Label)e.Item.FindControl("lblGioiTinh");
            Label lblNgaySinh = (Label)e.Item.FindControl("lblNgaySinh");

            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");

            if (canBoInfo.GioiTinh == 1)
            {
                lblGioiTinh.Text = "Nam";
            }
            else if(canBoInfo.GioiTinh == 2)
            {
                lblGioiTinh.Text = "Nữ";
            }
            else
            {
                lblGioiTinh.Text = "";
            }

            if (Format.FormatDate(canBoInfo.NgaySinh) != "01/01/1753")
                lblNgaySinh.Text = Format.FormatDate(canBoInfo.NgaySinh);
            else
                lblNgaySinh.Text = "";

            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";
            }
        }
        #endregion

        #region delete
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int canBoID = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            if (canBoID != 0)
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoCanBo, AccessLevel.Delete))
                {
                    int kq = 0;
                    try
                    {
                        kq = new DAL.CanBo().Delete(canBoID);
                        if (kq != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessageBox", "showMessageBox('Xóa dữ liệu thành công')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessageBox", "showMessageBox('Xảy ra lỗi trong quá trình cập nhật dữ liệu')", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessageBox", "showMessageBox('Xảy ra lỗi trong quá trình cập nhật dữ liệu')", true);
                    }
                }
            }
            BindRepeater();
           // createPaging();
            hdDeleteID.Value = "0";
        }
        #endregion

        protected void ddlLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            String keyword = txtSearch.Text.Trim();
            int loaiCanBo = Utils.ConvertToInt32(ddlLoai.SelectedValue, 0);
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            if (Session["LoaiCanBo_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiCanBo_" + Request.Url.AbsolutePath, loaiCanBo);
            }
            else
            {
                Session["LoaiCanBo_" + Request.Url.AbsolutePath] = loaiCanBo;
            }
            BindRepeater();
            //createPaging();
        }
    }
}