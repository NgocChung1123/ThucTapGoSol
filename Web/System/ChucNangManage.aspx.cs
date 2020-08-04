using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class ChucNangManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyChucNang, AccessLevel.Read))
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyChucNang, AccessLevel.Edit))
            {
                btnThemNhom.Enabled = false;
                btnThemNhom.ToolTip = Constant.ToolTip;
                btnThemNhom.CssClass += " disable";
                btnThemNhom.OnClientClick = String.Empty;
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
                BindChucNangDDL();
                createPaging();
            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Hệ thống");
        }

        private void BindRepeater()
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
                    rptChucNang.DataSource = new DAL.ChucNang().GetByPage(start, end);
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
                    rptChucNang.DataSource = new DAL.ChucNang().GetBySearch(keyword, start, end);
                }
                catch
                {
                }
            }

            rptChucNang.DataBind();

            //string nguoiDungs = "92,94,95";
            //List<NguoiDungInfo> ls = new DAL.ChucNang().GetByTest(nguoiDungs).ToList();

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptChucNang.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    createPaging();
        //}

        private void createPaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
           
            txtSearch.Text = keyword;
            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    totalRow = new DAL.ChucNang().CountAll();
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
                    totalRow = new DAL.ChucNang().CountSearch(keyword);
                    
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String keyword = txtSearch.Text.Trim();
            Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            createPaging();
            BindRepeater();
        }

        protected void BindChucNangDDL()
        {
            ddlChucNang.DataSource = new DAL.ChucNang().GetAll();
            ddlChucNang.Items.Insert(0, new ListItem("0", "0"));
            ddlChucNang.DataBind();
            ddlChucNang.SelectedIndex = 0;
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
        }

        protected void ddlChucNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cnID = Utils.ConvertToInt32(ddlChucNang.SelectedValue, 0);
            if (cnID != 0)
            {                
                BindNhomRepeater(cnID);
                //Fake sleep
                System.Threading.Thread.Sleep(500);
            }
        }

        private void BindNhomRepeater(int cnID)
        {
            List<NhomNguoiDungInfo> groupList = new List<NhomNguoiDungInfo>();
            try
            {
                groupList = new DAL.NhomNguoiDung().GetByChucNang(cnID).ToList();
            }
            catch
            {
            }            

            rptNhom.DataSource = groupList;
            rptNhom.DataBind();
        }

        private List<ChucNangInfo> GetParentList(List<ChucNangInfo> sourceList)
        {
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
        

        protected void btnSaveNhom_Click(object sender, EventArgs e)
        {
            List<int> resultList = new List<int>();
            int chucNangID = Utils.ConvertToInt32(ddlChucNang.SelectedValue, 0);
            int groupID = Utils.ConvertToInt32(ddlNhom.SelectedValue, 0);

            int quyen = CalculateAccessRight(cblAccessRight);

            if (groupID != 0 && quyen != 0)
            {
                GetChucNangCon(ref resultList, chucNangID);

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
                BindNhomRepeater(chucNangID);                
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

        private void GetChucNangCon(ref List<int> resultList, int cnChaID)
        {
            resultList.Add(cnChaID);
            List<ChucNangInfo> childList = new DAL.ChucNang().GetChilds(cnChaID).ToList();
            foreach (ChucNangInfo childInfo in childList)
            {
                GetChucNangCon(ref resultList, childInfo.ChucNangID);
            }
        }


        protected void rptNhom_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (AccessControl.User.HasPermission(ChucNangEnum.QuanLyChucNang, AccessLevel.Edit))
            {
                if (e.CommandName == "DeleteGroup")
                {
                }
                else if (e.CommandName == "SaveQuyen")
                {
                    int cnID = Utils.ConvertToInt32(ddlChucNang.SelectedValue, 0);

                    List<int> childList = new List<int>();
                    GetChucNangCon(ref childList, cnID);

                    CheckBoxList cblQuyen = e.Item.FindControl("cblQuyen") as CheckBoxList;
                    int quyen = CalculateAccessRight(cblQuyen);

                    foreach (int childID in childList)
                    {
                        if (cnID != 0 && childID != 0)
                        {
                            //Neu quyen bang 0, xoa nhom
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
                    //success.Attributes.CssStyle["display"] = "block";
                    //ajax_fade.Attributes.CssStyle["display"] = "block";

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showThongBaoSucces", "showThongBaoSucces();", true);
                    BindNhomRepeater(cnID);
                }
            }
        }

        protected void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            int nhomID = Utils.ConvertToInt32(hdDeleteGroupID.Value, 0);
            int cnID = Utils.ConvertToInt32(ddlChucNang.SelectedValue, 0);

            if (nhomID != 0)
            {
                List<int> childList = new List<int>();
                GetChucNangCon(ref childList, cnID);

                foreach (int childID in childList)
                {
                    if (cnID != 0 && childID != 0)
                    {
                        try
                        {
                            new DAL.NhomNguoiDung().RemoveChucNang(nhomID, childID);
                            lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                            lblContentErr.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showThongBaoThanhCong();", true);
                        }
                        catch
                        {
                            lblContentSuccess.Text = "";
                            lblContentErr.Text = Constant.CONTENT_DELETE_ERROR;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
                        }
                    }
                }
            }
            BindNhomRepeater(cnID);
            hdDeleteGroupID.Value = String.Empty;            
        }

        protected void rptNhom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            NhomNguoiDungInfo nhomInfo = e.Item.DataItem as NhomNguoiDungInfo;
            int cnID = Utils.ConvertToInt32(ddlChucNang.SelectedValue, 0);

            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            ImageButton btnSave = (ImageButton)e.Item.FindControl("btnSave");

            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyChucNang, AccessLevel.Edit))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";

                btnSave.Enabled = false;
                btnSave.ToolTip = Constant.ToolTip;
                btnSave.CssClass += " disable";
            }
            
            List<ChucNangInfo> cnList = new DAL.ChucNang().GetByGroup(nhomInfo.NhomNguoiDungID).ToList();
            ChucNangInfo cnInfo = new ChucNangInfo();
            foreach (ChucNangInfo chucnangInfo in cnList)
            {
                if (chucnangInfo.ChucNangID == cnID)
                {
                    cnInfo = chucnangInfo;
                    break;
                }
            }

            CheckBoxList cblQuyen = e.Item.FindControl("cblQuyen") as CheckBoxList;
            foreach (ListItem item in cblQuyen.Items)
            {
                int itemQuyen = Utils.ConvertToInt32(item.Value, 0);
                if (itemQuyen != 0)
                {
                    if ((itemQuyen & cnInfo.Quyen) != 0)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
    }
}