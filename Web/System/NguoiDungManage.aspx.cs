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
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web
{
    public partial class NguoiDungManage : System.Web.UI.Page
    {
        private int stt = 1;
        protected String defaultPassword = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //lay mat khau mac dinh tu du lieu cache
            try
            {
                defaultPassword = new DAL.SystemConfig().GetByKey("MAT_KHAU_MAC_DINH").ConfigValue;
            }
            catch
            {
            }    
            //if (Cache["DefaultPassword"] != null)
            //{
            //    defaultPassword = Cache["DefaultPassword"].ToString();
            //}
            //else
            //{
                            
            //    Page.Cache.Add("DefaultPassword", defaultPassword, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0), System.Web.Caching.CacheItemPriority.Normal, null);
            //}

            //Check quyen
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Read))
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Create))
            {
                //btnThem.Enabled = false;
                //btnThem.ToolTip = Constant.ToolTip;
                //btnThem.CssClass += " disable";
                //btnThem.OnClientClick = "return false;";                
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                btnThemNhom.Enabled = false;
                btnThemNhom.ToolTip = Constant.ToolTip;
                btnThemNhom.CssClass += " disable";
                btnThemNhom.OnClientClick = "";
                udpChucNang.Update();
            }
                        
            if (!IsPostBack)
            {
                int canboID = Utils.ConvertToInt32(Request.Params["canboID"], 0);
                int currentPage = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", currentPage);
                }
                else
                {
                    Session["CurrentPage"] = currentPage;
                }

                 if (Session["Keyword" + Request.Url.AbsolutePath] != null)
                {
                    txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
                }

                if (Session["CoQuanID_" + Request.Url.AbsolutePath] != null)
                {
                  //  ddlCoQuanSearch.SelectedValue = Session["CoQuanID_" + Request.Url.AbsolutePath].ToString();
                }
                //BindCanBoDDL();         
                BindNguoiDungDDL();
                BindNhomNguoiDungDDL();
                BindCoQuan();
                if (canboID > 0)
                {
                    try
                    {
                        ddlCanBo.SelectedValue = canboID.ToString();
                    }
                    catch
                    {
                    }
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "showAddForm", "showAddForm()", true);
                }
                BindRepeater();
                createPaging();
            }
            else
            {
                //light.Attributes.CssStyle["display"] = "none";
            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Hệ thống");
        }

        protected void BindCoQuan()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Create) || AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit) || AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Delete))
            {
                int coQuanUserID = IdentityHelper.GetCoQuanID();
                int capID = IdentityHelper.GetCapID();

                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    ddlCoQuanSearch.DataSource = new DAL.CoQuan().GetAllCoQuan();
                else
                    ddlCoQuanSearch.DataSource = new DAL.CoQuan().GetCoQuanByParentID(coQuanUserID);
                ddlCoQuanSearch.DataBind();
                ddlCoQuanSearch.Items.Insert(0, "Chọn cơ quan");
                ddlCoQuanSearch.SelectedIndex = 0;

                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    ddlCanBo.DataSource = new DAL.CanBo().GetAllCanBo();
                else
                    ddlCanBo.DataSource = new DAL.CanBo().GetCanBoByCoQuanChaID(coQuanUserID);
                ddlCanBo.DataBind();
                ddlCanBo.Items.Insert(0, new ListItem("Chọn cán bộ", ""));
                ddlCanBo.SelectedIndex = 0;

                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    ddlCoQuan.DataSource = new DAL.CoQuan().GetAllCoQuan();
                else
                    ddlCoQuan.DataSource = new DAL.CoQuan().GetCoQuanByParentID(coQuanUserID);
                ddlCoQuan.DataBind();
                ddlCoQuan.Items.Insert(0, "Chọn cơ quan");
                ddlCoQuan.SelectedIndex = 0;
            }
            else
            {
                ddlCoQuan.DataSource = new DAL.CoQuan().GetCoQuanByID(IdentityHelper.GetCoQuanID());
                ddlCoQuanSearch.Visible = false;
                ddlCoQuan.Enabled = false;

                ddlCanBo.DataSource = new DAL.CanBo().GetByCoQuanID(IdentityHelper.GetCoQuanID());
                ddlCanBo.DataBind();
                ddlCanBo.Items.Insert(0, new ListItem("Chọn cán bộ", "0"));
                ddlCanBo.SelectedIndex = 0;
            }
          
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedValue, 0);
            int loaiNguoiDung = Utils.ConvertToInt32(ddlLoai.SelectedValue, 1);
            txtSearch.Text = keyword;
            if (Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiNguoiDung_" + Request.Url.AbsolutePath, loaiNguoiDung);
            }
            else
            {
                Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] = loaiNguoiDung;
            }

            if (Session["CoQuanID_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("CoQuanID_" + Request.Url.AbsolutePath, coQuanID);
            }
            else
            {
                Session["CoQuanID_" + Request.Url.AbsolutePath] = coQuanID;
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
                rptNguoiDung.DataSource = new DAL.NguoiDung().GetBySearch(coQuanID, keyword, start, end, loaiNguoiDung);
                //if (capID == (int)CapQuanLy.CapUBNDTinh)
                //    rptNguoiDung.DataSource = new DAL.NguoiDung().GetBySearch(coQuanID, keyword, start, end, loaiNguoiDung);
                //else
                //    rptNguoiDung.DataSource = new DAL.NguoiDung().GetByCoQuanChaID(coQuanID, keyword, start, end, coQuanUserID, loaiNguoiDung);
            }
            catch
            {
            }
            rptNguoiDung.DataBind();
            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptNguoiDung.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        private void createPaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedValue, 0);
            int loaiNguoiDung = Utils.ConvertToInt32(ddlLoai.SelectedValue, 1);
            txtSearch.Text = keyword;
            if (Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiNguoiDung_" + Request.Url.AbsolutePath, loaiNguoiDung);
            }
            else
            {
                Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] = loaiNguoiDung;
            }

            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();

            try
            {
                keyword = "%" + keyword + "%";
                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    totalRow = new DAL.NguoiDung().CountSearch(coQuanID, keyword, loaiNguoiDung);
                else
                    totalRow = new DAL.NguoiDung().CountByCoQuanChaID(coQuanID, keyword, coQuanUserID, loaiNguoiDung);
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
                PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String keyword = txtSearch.Text.Trim();
            int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedValue, 0);
            int loaiNguoiDung = Utils.ConvertToInt32(ddlLoai.SelectedValue, 1);
          
            if (Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiNguoiDung_" + Request.Url.AbsolutePath, loaiNguoiDung);
            }
            else
            {
                Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] = loaiNguoiDung;
            }
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }

            if (Session["CoQuanID_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("CoQuanID_" + Request.Url.AbsolutePath, coQuanID);
            }
            else
            {
                Session["CoQuanID_" + Request.Url.AbsolutePath] = coQuanID;
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
            List<CanBoJoinInfo> canboList = new DAL.CanBo().GetCanBos().ToList();
            ddlCanBo.DataSource = canboList;
            ddlCanBo.DataBind();
            ddlCanBo.Items.Insert(0, new ListItem("Chọn cán bộ", "0"));
            ddlCanBo.SelectedIndex = 0;
        }

        protected void ddlCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "showAddForm", "showAddForm()", true);
            int coQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0);
            if (coQuanID > 0)
                ddlCanBo.DataSource = new DAL.CanBo().GetByCoQuanID(coQuanID);
            else
            {
                ddlCanBo.DataSource = new DAL.CanBo().GetCanBos().ToList();
            }
            ddlCanBo.DataBind();
        }

        [WebMethod]
        public static string getCanBoByCoQuanID(string coQuanID)
        {
            int coquanid = Utils.ConvertToInt32(coQuanID, 0);
            IList<CanBoInfo> canBoLs = new List<CanBoInfo>();
            try
            {
                if (coquanid > 0)
                    canBoLs = new DAL.CanBo().GetByCoQuanID(coquanid);
            }
            catch
            {
            }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(canBoLs);
                return data;
            }
            catch
            {
                return data;
            }
        }

        [WebMethod]
        public static string getALLCanBo()
        {
            IList<CanBoJoinInfo> canBoLs = new List<CanBoJoinInfo>();
            try
            {
                canBoLs = new DAL.CanBo().GetCanBos().ToList();
            }
            catch
            {
            }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(canBoLs);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            NguoiDungInfo nguoiDungInfo = GetNguoiDungInfo();

            if (nguoiDungInfo.NguoiDungID != 0)
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
                {
                    NguoiDungInfo oldNdInfo = new DAL.NguoiDung().GetNguoiDungByID(nguoiDungInfo.NguoiDungID);
                    nguoiDungInfo.MatKhau = oldNdInfo.MatKhau;
                    nguoiDungInfo.TenNguoiDung = oldNdInfo.TenNguoiDung;
                    try
                    {
                        int kq = new DAL.NguoiDung().Update(nguoiDungInfo);
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "hideAddEditForm", "hideAddEditForm()", true);

                        lblthongBaoAddSussess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoEditSuccess", "hideAddEditForm(); showthongBaoEditSuccess()", true);
                    }
                    catch
                    {
                        lblThongBaoError.Text = Constant.CONTENT_MESSAGE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            else
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Create))
                {
                    int result = 0;
                    //defaultPassword = "";
                    nguoiDungInfo.MatKhau = Utils.HashFile(Encoding.ASCII.GetBytes(defaultPassword)).ToUpper();
                    try
                    {
                        result = new DAL.NguoiDung().Insert(nguoiDungInfo);
                        //result = 1;
                        if ((result != 0) && (result != -1) && (result != -2))
                        {
                           //ScriptManager.RegisterStartupScript(this, typeof(Page), "hideAddEditForm", "hideAddEditForm()", true);
                            lblthongBaoAddSussess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showThongBaoAddSuccess", "hideAddEditForm(); showThongBaoAddSuccess();", true);
                        }
                    }
                    catch
                    {
                    }

                    if (result == 0)
                    {
                        lblThongBaoError.Text = "Lỗi kết nối đến CSDL";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                    else if (result == -1)
                    {
                        lblThongBaoError.Text = "Tên đăng nhập đã tồn tại";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                    else if (result == -2)
                    {
                        lblThongBaoError.Text = "Cán bộ đã được gán cho người dùng khác";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
          //  UpnAdd.Update();
            ClearForm();
            BindCoQuan();
            BindRepeater();
            BindNguoiDungDDL();
            createPaging();
        }

        private NguoiDungInfo GetNguoiDungInfo()
        {
            NguoiDungInfo ndInfo = new NguoiDungInfo();
            ndInfo.NguoiDungID = Utils.ConvertToInt32(hdfNguoiDungID.Value, 0);
            ndInfo.TenNguoiDung = txtTenNguoiDung.Text;
            ndInfo.GhiChu = txtGhiChu.Text;
            ndInfo.CanBoID = Utils.ConvertToInt32(ddlCanBo.SelectedValue, 0);
            ndInfo.TrangThai = Utils.ConvertToInt32(ddlTrangThai.SelectedValue, 0);

            return ndInfo;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            BindRepeater();
            createPaging();
        }

        private void ClearForm()
        {
            hdfNguoiDungID.Value = "0";
            txtTenNguoiDung.Text = String.Empty;
            txtGhiChu.Text = String.Empty;
            try
            {                
                ddlCanBo.SelectedIndex = 0;
                ddlTrangThai.SelectedIndex = 0;
                ddlCoQuan.SelectedIndex = 0;
            }
            catch
            {
            }            
        }


        [WebMethod]
        public static string GetByNguoiDungID(string nguoiDungID)
        {
            int nguoidungID = Utils.ConvertToInt32(nguoiDungID, 0);
            NguoiDungInfo Info = new NguoiDungInfo();
            try
            {
                Info = new DAL.NguoiDung().GetNguoiDungByID(nguoidungID);
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

        protected void rptNguoiDung_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;

            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            ImageButton btnRefresh = (ImageButton)e.Item.FindControl("btnRefresh");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            Label lblTrangThai = e.Item.FindControl("lblTrangThai") as Label;

            NguoiDungJoinInfo nguoidungInfo = e.Item.DataItem as NguoiDungJoinInfo;
            if (nguoidungInfo.TrangThai == 1) lblTrangThai.Text = "Đang hoạt động";
            else lblTrangThai.Text = "Bị khóa";

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";

                btnRefresh.Enabled = false;
                btnRefresh.ToolTip = Constant.ToolTip;
                btnRefresh.CssClass += " disable";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += "disable";                
            }
        }

        private void BindNguoiDungDDL()
        {
            ddlNguoiDung.DataSource = new DAL.NguoiDung().GetAll();
            ddlNguoiDung.DataBind();            
            ddlNguoiDung.Items.Insert(0,new ListItem("Chọn", "0"));
            try {
                ddlNguoiDung.SelectedIndex = 0;
            }
            catch {
            }
        }

        private void BindNhomNguoiDungDDL()
        {
            ddlNhom.DataSource = new DAL.NhomNguoiDung().GetAll();
            ddlNhom.DataBind();
        }

        protected void ddlNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            int ndID = Utils.ConvertToInt32(ddlNguoiDung.SelectedValue, 0);
            if (ndID != 0)
            {
                //Fake sleep
                System.Threading.Thread.Sleep(500);

                BindNhomRepeater(ndID);
            }
        }

        protected void btnSaveGroup_Click(object sender, EventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);
            int ndID = Utils.ConvertToInt32(ddlNguoiDung.SelectedValue, 0);

            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                lblMsg.Visible = true;
                if (nhomID != 0 && ndID != 0)
                {
                    int result = 0;
                    try
                    {
                        result = new DAL.NguoiDung().AddGroup(ndID, nhomID);
                        lblMesGroup.Text = Constant.MESSAGE_INSERT_SUCCESS;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showMesGroup", "showMesGroup()", true);
                        udpChucNang.Update();
                    }
                    catch
                    {
                        lblMesGroup.Text = Constant.CONTENT_MESSAGE_ERROR;
                        lblMesGroup.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showMesGroup", "showMesGroup()", true);
                    }
                }
                BindNhomRepeater(ndID);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "hideAddGroupForm", "hideAddGroupForm()", true);
            }
        }

        private void BindNhomRepeater(int ndID)
        {
            rptNhom.DataSource = new DAL.NhomNguoiDung().GetByUser(ndID).ToList();
            rptNhom.DataBind();
        }

        protected void rptNhom_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteGroup")
            {                
            }
        }

        protected void rptNhom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";
            }
        }

        //protected void btnThem_Click(object sender, EventArgs e)
        //{
        //    txtTenNguoiDung.Enabled = true;
        //    light.Attributes.CssStyle["display"] = "block";
        //    fade.Attributes.CssStyle["display"] = "block";
        //    ClearForm();
        //}

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
          
        //}

        protected void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = true;
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                int groupID = Utils.ConvertToInt32(hdDeleteGroupID.Value, 0);
                if (groupID != 0)
                {
                    int userID = Utils.ConvertToInt32(ddlNguoiDung.SelectedValue, 0);
                    if (userID != 0)
                    {
                        try
                        {
                            new DAL.NguoiDung().RemoveGroup(userID, groupID);
                            lblMesGroup.Text = Constant.CONTENT_DELETE_SUCCESS;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showMesGroup", "showMesGroup()", true);
                        }
                        catch
                        {
                            lblMesGroup.Text = Constant.CONTENT_DELETE_ERROR;
                            lblMesGroup.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showMesGroup", "showMesGroup()", true);
                        }
                    }
                    BindNhomRepeater(userID);
                    udpChucNang.Update();
                }
            }
        }

        protected String SetSelected(int userID)
        {
            int ndID = Utils.ConvertToInt32(ddlNguoiDung.SelectedValue, 0);
            if (userID == ndID)
            {
                return "selected_hl";
            }
            else return String.Empty;
        }


        protected void btnResetMatKhau_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            int ndID = Utils.ConvertToInt32(hdRefreshID.Value, 0);
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Edit))
            {
                if (ndID != 0)
                {
                    NguoiDungInfo ndInfo = new DAL.NguoiDung().GetNguoiDungByID(ndID);

                    ndInfo.MatKhau = Utils.HashFile(Encoding.ASCII.GetBytes(defaultPassword)).ToUpper();
                    try
                    {
                        new DAL.NguoiDung().Update(ndInfo);
                        //fade.Attributes.CssStyle["display"] = "block";
                        //success.Attributes.CssStyle["display"] = "block";
                        lblContentSuccess.Text = "Mật khẩu của người dùng đã được reset về " + defaultPassword;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    catch
                    {
                        //txtError.Text = "Lỗi kết nối đến CSDL";
                        //submitError.Attributes.CssStyle["display"] = "block";
                        lblContentSuccess.Text = "Lỗi kết nối đến CSDL";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    hdRefreshID.Value = String.Empty;
                }
            }
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            int ndID = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNguoiDung, AccessLevel.Delete))
            {
                if (ndID != 0)
                {
                    try
                    {
                        new DAL.NguoiDung().Delete(ndID);
                        lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    catch
                    {
                        lblthongBaoDeleteError.Text = Constant.CONTENT_DELETE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoDeleteError", "showthongBaoDeleteError()", true);
                    }
                }
            }
            hdDeleteID.Value = String.Empty;
            BindRepeater();
            createPaging();
            // UpnAdd.Update();
        }

        protected void ddlCoQuanSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            String keyword = txtSearch.Text.Trim();
            int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedValue, 0);
            int loaiNguoiDung = Utils.ConvertToInt32(ddlLoai.SelectedValue, 1);
            
            if (Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiNguoiDung_" + Request.Url.AbsolutePath, loaiNguoiDung);
            }
            else
            {
                Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] = loaiNguoiDung;
            }
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            if (Session["CoQuanID_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("CoQuanID_" + Request.Url.AbsolutePath, coQuanID);
            }
            else
            {
                Session["CoQuanID_" + Request.Url.AbsolutePath] = coQuanID;
            }
            
            BindRepeater();
            createPaging();
        }

        protected void ddlLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            String keyword = txtSearch.Text.Trim();
            int loaiNguoiDung = Utils.ConvertToInt32(ddlLoai.SelectedValue, 0);
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            if (Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiNguoiDung_" + Request.Url.AbsolutePath, loaiNguoiDung);
            }
            else
            {
                Session["LoaiNguoiDung_" + Request.Url.AbsolutePath] = loaiNguoiDung;
            }
            
            BindRepeater();
            createPaging();
            if (loaiNguoiDung == 1)
                ddlCoQuanSearch.Enabled = true;
            else
                ddlCoQuanSearch.Enabled = false;
        }

    }
}