using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.DAL;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web
{
    public partial class GroupManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Read))
            {
                Response.Redirect("~");
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Create))
            {
                //btnThemNhom.Enabled = false;
                //btnThemNhom.ToolTip = Constant.ToolTip;
                //btnThemNhom.CssClass += " disable";
                //btnThemNhom.OnClientClick = "";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                btnThemChucNang.Enabled = false;
                btnThemChucNang.ToolTip = Constant.ToolTip;
                btnThemChucNang.CssClass += " disable";
                btnThemChucNang.OnClientClick = "";

                btnThemNguoiDung.Enabled = false;
                btnThemNguoiDung.ToolTip = Constant.ToolTip;
                btnThemNguoiDung.CssClass += " disable";
                btnThemNguoiDung.OnClientClick = "";
            }

            if (!IsPostBack)
            {
                //if (IdentityHelper.GetCapID() != (int)CapQuanLy.CapUBNDTinh)
                //{
                //    Response.Redirect("~");
                //}

                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                    Session.Add("CurrentPage", page);
                else Session["CurrentPage"] = page;
                BindRepeater();
                BindNhomDDL();
                BindNguoiDungDDL();
                BindChucNangDDL();
                BindCoQuanDDL();
            }
            else
            {

            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Hệ thống");
        }

        #region ddl chuc nang
        private void BindChucNangDDL()
        {
            int capID = IdentityHelper.GetCapID();
            int coQuanUserID = IdentityHelper.GetCoQuanID();

            List<ChucNangInfo> parentList = new List<ChucNangInfo>();
            //Bind chuc nang cha ddl
            try
            {
                if (capID == (int)CapQuanLy.CapUBNDTinh)
                {
                    parentList = new DAL.ChucNang().GetParents().ToList();
                }
                else {
                    parentList = new DAL.ChucNang().GetParentsForAdminChidl().ToList();
                }
            }
            catch
            {
            }

            //Bind chuc nang con ddl
            List<ChucNangInfo> resultList = new List<ChucNangInfo>();
            foreach (ChucNangInfo parentInfo in parentList)
            {   
                GetChucNangTree(ref resultList, parentInfo, 0);
            }            

            ddlChucNang.DataSource = resultList;            
            ddlChucNang.DataBind();

            ddlChucNang.Items.Insert(0, new ListItem("Chọn một chức năng trong danh sách", "0"));
        }

        private void GetChucNangTree(ref List<ChucNangInfo> resultList, ChucNangInfo parentInfo, int level) {
            String space = "-- ";
            for (int i = 0; i < level; i++)
            {
                space = space + space;
            }
            //Root has bold name
            if (level == 0)
            {
                parentInfo.TenChucNang = parentInfo.TenChucNang;
            }
            else
            {
                parentInfo.TenChucNang = space + parentInfo.TenChucNang;
            }
            level++;
            resultList.Add(parentInfo);
            List<ChucNangInfo> childList = new List<ChucNangInfo>(); ;
            try
            {
                int capID = IdentityHelper.GetCapID();
                int coQuanUserID = IdentityHelper.GetCoQuanID();

                if (capID == (int)CapQuanLy.CapUBNDTinh)
                {
                    childList = new DAL.ChucNang().GetChilds(parentInfo.ChucNangID).ToList();
                }
                else
                {
                    childList = new DAL.ChucNang().GetChildsForAdminChild(parentInfo.ChucNangID).ToList();
                }
            }
            catch
            {
            }
            if (childList.Count > 0)
            {
                foreach (ChucNangInfo childInfo in childList)
                {
                    GetChucNangTree(ref resultList, childInfo, level);
                }
            }
        }

        private void BindCoQuanDDL()
        {
            List<CoQuanInfo> cqList = new List<CoQuanInfo>();
            List<CoQuanInfo> parentList = new List<CoQuanInfo>();

            int capID = IdentityHelper.GetCapID();
            int coQuanUserID = IdentityHelper.GetCoQuanID();

            if (capID == (int)CapQuanLy.CapUBNDTinh)
            {
                parentList = new DAL.CoQuan().GetParents().ToList();

                foreach (CoQuanInfo parentInfo in parentList)
                {
                    TreeSort(ref cqList, parentInfo, 0);
                }

                ddlCoQuan.DataSource = cqList;
                ddlCoQuan.DataBind();
                ddlCoQuan.Items.Insert(0, new ListItem("Chọn một đơn vị bên dưới", "0"));
            }
            else
            {
                parentList = new DAL.CoQuan().GetCoQuanByCoQuanID(coQuanUserID).ToList();
                ddlCoQuan.DataSource = parentList;
                ddlCoQuan.DataBind();
            }
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

        #region paging
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
                    totalRow = new DAL.NhomNguoiDung().CountAll();
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
                    totalRow = new DAL.NhomNguoiDung().CountSearch(keyword);
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
            //Response.Redirect(Request.Url.AbsolutePath);
            //createPaging();
            BindRepeater();
        }

        #region insert group
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            NhomNguoiDungInfo nhomInfo = GetNhomNguoiDungInfo();

            if (nhomInfo.NhomNguoiDungID != 0)
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
                {
                    try
                    {
                        new DAL.NhomNguoiDung().Update(nhomInfo);
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
            }
            else
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Create))
                {
                    try
                    {
                        new DAL.NhomNguoiDung().Insert(nhomInfo);
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
            }
            ClearForm();
            BindRepeater();
            BindNhomDDL();
        }

        private NhomNguoiDungInfo GetNhomNguoiDungInfo()
        {
            NhomNguoiDungInfo nhomInfo = new NhomNguoiDungInfo();
            nhomInfo.NhomNguoiDungID = Utils.ConvertToInt32(hdNhomNguoiDungID.Value, 0);
            nhomInfo.TenNhom = txtTenNhom.Text;
            nhomInfo.GhiChu = txtGhiChu.Text;
            nhomInfo.CoQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0);

            return nhomInfo;
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hdNhomNguoiDungID.Value = String.Empty;
            txtTenNhom.Text = String.Empty;
            txtGhiChu.Text = String.Empty;            

            //light.Attributes.CssStyle["display"] = "none";
            //fade.Attributes.CssStyle["display"] = "none";
        }


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
            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();
            try
            {
                keyword = "%" + keyword + "%";
                if (capID == (int)CapQuanLy.CapUBNDTinh)
                    rptNhom.DataSource = new DAL.NhomNguoiDung().GetBySearch(keyword, start, end);
                else
                    rptNhom.DataSource = new DAL.NhomNguoiDung().GetByCoQuanID(keyword, start, end, coQuanUserID);
            }
            catch
            {

            }
            //if (String.IsNullOrEmpty(keyword))
            //{
            //    try
            //    {
            //        rptNhom.DataSource = new DAL.NhomNguoiDung().GetByPage(start, end);
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
                
            //}
            rptNhom.DataBind();
            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptNhom.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        private void FillFormData(NhomNguoiDungInfo nhomInfo)
        {
            hdNhomNguoiDungID.Value = nhomInfo.NhomNguoiDungID.ToString();
            txtTenNhom.Text = nhomInfo.TenNhom;
            txtGhiChu.Text = nhomInfo.GhiChu;
        }

        protected void rptNhom_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (e.CommandName == "Edit")
            {
                if (nhomID != 0)
                {
                    NhomNguoiDungInfo nhomInfo = new DAL.NhomNguoiDung().GetByID(nhomID);
                    FillFormData(nhomInfo);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showAddForm", "showAddForm()", true);
                }

                //show popup form
                //light.Attributes.CssStyle["display"] = "block";
                //fade.Attributes.CssStyle["display"] = "block";

            }
            else if (e.CommandName == "Delete")
            {                
            }
            //else if (e.CommandName == "abc")
            //{
            //    if (nhomID != 0)
            //    {
            //        BindNguoiDungRepeater(nhomID);
            //        BindChucNangRepeater(nhomID);
            //    }
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "showAddForm", "showAddForm()", true);
            //}
            BindRepeater();
            BindNhomDDL();
        }

        [WebMethod]
        public static string GetByNhomNguoiDungID(string nhomNguoiDungID)
        {
            int nhomnguoidungID = Utils.ConvertToInt32(nhomNguoiDungID, 0);
            NhomNguoiDungInfo Info = new NhomNguoiDungInfo();
            try
            {
                Info = new DAL.NhomNguoiDung().GetByID(nhomnguoidungID);
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

        private void BindNguoiDungDDL()
        {
            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();

            IList<NguoiDungInfo> nDungInfo = new List<NguoiDungInfo>();
            try
            {
                int nhomID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);
                if (nhomID > 0)
                {
                    //nDungInfo = new DAL.NguoiDung().GetNguoidungchuaThuocNhom(nhomID);
                    if (capID == (int)CapQuanLy.CapUBNDTinh)
                    {
                        nDungInfo = new DAL.NguoiDung().GetNguoidungchuaThuocNhom(nhomID);
                        ddlNguoiDung.DataSource = nDungInfo;
                    }
                    else
                    {
                        ddlNguoiDung.DataSource = new DAL.NguoiDung().GetUserchuaThuocNhomByCoQuan(nhomID, coQuanUserID);
                    }
                }
                else
                {
                    ddlNguoiDung.DataSource = new DAL.NguoiDung().GetNguoidungchuaThuocNhom(nhomID);
                }
            }
            catch
            {
            }
            
            ddlNguoiDung.DataBind();           
        }

        private void BindNhomDDL()
        {
            try
            {
                ddlNhom.DataSource = new DAL.NhomNguoiDung().GetAll();
            }
            catch
            {
            }
            ddlNhom.DataBind();
            ddlNhom.Items.Insert(0, new ListItem("Chọn", "0"));
            ddlNhom.SelectedIndex = 0;
        }

        protected void ddlNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);
            if (nhomID != 0)
            {
                BindNguoiDungRepeater(nhomID);
                BindChucNangRepeater(nhomID);
                
            }
        }

        private void BindChucNangRepeater(int nhomID)
        {
            List<ChucNangInfo> cnList = new List<ChucNangInfo>();
            try
            {
                cnList = new DAL.ChucNang().GetByGroup(nhomID).ToList();
            }
            catch
            {
            }

            List<ChucNangInfo> resultList = new List<ChucNangInfo>();
            List<ChucNangInfo> parentList = GetParentList(cnList);
            
            foreach (ChucNangInfo cnInfo in parentList)
            {
                TreeSort(ref resultList, cnList, cnInfo.ChucNangID, 0);
            }            
            

            rptChucNang.DataSource = resultList;
            rptChucNang.DataBind();            
        }

        private List<ChucNangInfo> GetParentList(List<ChucNangInfo> sourceList) {
            List<ChucNangInfo> parentList = new List<ChucNangInfo>();

            foreach (ChucNangInfo cnInfo in sourceList)
            {
                bool hasParent = false;
                foreach (ChucNangInfo cncInfo in sourceList)
                {
                    if (cnInfo.ChucNangChaID == cncInfo.ChucNangID)
                    {
                        hasParent = true;
                        break;
                    }
                }
                if (!hasParent) parentList.Add(cnInfo);
            }

            return parentList;
        }

        private void TreeSort(ref List<ChucNangInfo> resultList, List<ChucNangInfo> sourceList, int parentID, int level)
        {            
            foreach (ChucNangInfo cnInfo in sourceList)
            {
                if (cnInfo.ChucNangID == parentID)
                {
                    cnInfo.Level = level;
                    resultList.Add(cnInfo);                    
                }
            }
            level++;
            foreach (ChucNangInfo cnInfo in sourceList)
            {                
                if (cnInfo.ChucNangChaID == parentID)
                {
                    foreach (ChucNangInfo cnChaInfo in resultList)
                    {
                        if (cnChaInfo.ChucNangID == parentID) cnChaInfo.HasChild = true;
                    }
                    //tiep tuc goi de quy
                    TreeSort(ref resultList, sourceList, cnInfo.ChucNangID, level);                        
                }                
            }
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);
            int ndID = Utils.ConvertToInt32(ddlNguoiDung.SelectedValue, 0);

            if (nhomID != 0 && ndID != 0)
            {
                if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
                {
                    try
                    {
                        int kq = new DAL.NguoiDung().AddGroup(ndID, nhomID);
                        if (kq > 0)
                        {
                            lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                        else
                        {
                            lblContentSuccess.Text = "";
                            lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                        }
                    }
                    catch
                    {
                        lblContentSuccess.Text = "";
                        lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            BindNguoiDungRepeater(nhomID);
            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "hideAddUserForm", "hideAddUserForm()", true);
        }

        private void BindNguoiDungRepeater(int nhomID)
        {
            rptNguoiDung.DataSource = new DAL.NguoiDung().GetByGroup(nhomID).ToList();
            rptNguoiDung.DataBind();
            
        }

        protected void rptNguoiDung_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {                
            }
        }

        protected void btnSaveChucNang_Click(object sender, EventArgs e)
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                List<int> resultList = new List<int>();
                int chucNangID = Utils.ConvertToInt32(ddlChucNang.SelectedValue, 0);
                int groupID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);

                ChucNangInfo cnInfo = new ChucNangInfo();
                try
                {
                    cnInfo = new DAL.ChucNang().GetChucNangByID(chucNangID);
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

                int quyen = CalculateAccessRight(cblAccessRight);

                if (groupID != 0 && quyen != 0)
                {
                    GetChucNangCon(ref resultList, chucNangID);

                    //add quyen cho cac chuc nang con
                    foreach (int cnID in resultList)
                    {
                        try
                        {
                            new DAL.NhomNguoiDung().AddChucNang(groupID, cnID, quyen);
                            lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                        }
                        catch
                        {
                            new DAL.NhomNguoiDung().UpdateChucNang(groupID, cnID, quyen);
                        }
                    }
                    //add quyen cho chuc nang cha
                    try
                    {
                        new DAL.NhomNguoiDung().AddChucNang(groupID, cnInfo.ChucNangChaID, quyen);
                        lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    catch
                    {
                        new DAL.NhomNguoiDung().UpdateChucNang(groupID, cnInfo.ChucNangChaID, quyen);
                        lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }

                    BindChucNangRepeater(groupID);
                    BindChucNangDDL();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "  hideAddChucNangForm", "  hideAddChucNangForm()", true);
                }
            }
            
        }

        private int CalculateAccessRight(CheckBoxList cbl)
        {
            int result = 0;
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    result += Utils.ConvertToInt32(item.Value, 0);
                }
            }
            return result;
        }

        private void GetChucNangCon(ref List<int> resultList, int cnChaID) {
            resultList.Add(cnChaID);
            List<ChucNangInfo> childList = new DAL.ChucNang().GetChilds(cnChaID).ToList();
            foreach (ChucNangInfo childInfo in childList)
            {
                GetChucNangCon(ref resultList, childInfo.ChucNangID);
            }
        }


        protected void rptChucNang_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int chucNangID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                if (e.CommandName == "DeleteChucNang")
                { 
                }
                else if (e.CommandName == "SaveQuyen")
                {
                    int nhomID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);

                    List<int> childList = new List<int>();
                    GetChucNangCon(ref childList, chucNangID);

                    CheckBoxList cblQuyen = e.Item.FindControl("cblQuyen") as CheckBoxList;
                    int quyen = CalculateAccessRight(cblQuyen);

                    foreach (int childID in childList)
                    {
                        if (nhomID != 0 && childID != 0)
                        {
                            //Neu bo het quyen, xoa chuc nang
                            if (quyen == 0)
                            {
                                try
                                {
                                    new DAL.NhomNguoiDung().RemoveChucNang(nhomID, childID);
                                   
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                try
                                {
                                    new DAL.NhomNguoiDung().UpdateChucNang(nhomID, childID, quyen);
                                   
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    lblContentErr.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    //success.Attributes.CssStyle["display"] = "block";
                    //ajax_fade.Attributes.CssStyle["display"] = "block";
                    BindChucNangRepeater(nhomID);
                }
            }
        }

        protected void rptChucNang_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ChucNangInfo cnInfo = e.Item.DataItem as ChucNangInfo;
            CheckBoxList cblQuyen = e.Item.FindControl("cblQuyen") as CheckBoxList;

            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            ImageButton btnLuu = (ImageButton)e.Item.FindControl("btnLuu");

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";
                
                btnLuu.Enabled = false;
                btnLuu.ToolTip = Constant.ToolTip;
                btnLuu.CssClass += " disable";
            }

            foreach (ListItem item in cblQuyen.Items)
            {
                int itemQuyen = Utils.ConvertToInt32(item.Value, 0);
                if(itemQuyen != 0) {                    
                    if ((itemQuyen & cnInfo.Quyen) != 0)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void rptNguoiDung_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");            

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";
            }
        }

        protected void rptNhom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";

            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";

            }

            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Delete))
            {
                if (nhomID != 0)
                {
                    try
                    {
                        new DAL.NhomNguoiDung().Delete(nhomID);
                        lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    catch(Exception ex)
                    {
                        lblContentSuccess.Text = "";
                        lblContentErr.Text = Constant.CONTENT_DELETE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }

                }
            }
            BindRepeater();
            hdDeleteID.Value = String.Empty;
        }

        protected void btnDeleteRole_Click(object sender, EventArgs e)
        {
            int chucNangID = Utils.ConvertToInt32(hdDeleteRoleID.Value, 0);            
            int nhomID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);

            if (chucNangID != 0)
            {
                List<int> childList = new List<int>();
                GetChucNangCon(ref childList, chucNangID);

                foreach (int childID in childList)
                {
                    if (nhomID != 0 && childID != 0)
                    {
                        try
                        {
                            new DAL.NhomNguoiDung().RemoveChucNang(nhomID, childID);
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
                    }
                }
                BindChucNangRepeater(nhomID);
            }
            hdDeleteRoleID.Value = String.Empty;
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyNhomNguoiDung, AccessLevel.Edit))
            {
                int userID = Utils.ConvertToInt32(hdDeleteUserID.Value, 0);
                if (userID != 0)
                {
                    int groupID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);
                    if (groupID != 0)
                    {
                        try
                        {
                            new DAL.NguoiDung().RemoveGroup(userID, groupID);
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
                    }
                    BindNguoiDungRepeater(groupID);
                    hdDeleteUserID.Value = String.Empty;
                }                
            }
        }

        protected String SetSelected(int nhomID)
        {
            int groupID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);
            if (groupID == nhomID)
            {
                return "selected_hl";
            }
            else return String.Empty;
        }

        protected void showAddUserForm(object sender, EventArgs e)
        {
            BindNguoiDungDDL();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showAddUserForm", "showAddUserForm()", true);
        }

    }
}