using Com.Gosol.CMS.DAL.LichSu;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.DonThu
{
    public partial class LichSuTraCuuDonThu : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //#region -- check permission
            if (!AccessControl.User.HasPermission(ChucNangEnum.LichSuTraCuu, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            //if (!AccessControl.User.HasPermission(ChucNangEnum.LichSuTraCuu, AccessLevel.Create)) // màn hình này ko có thêm mới dữ liệu. Nếu có thì ẩn button thêm
            //{
            //    btnAdd.OnClientClick = "return false;";
            //    btnAdd.Visible = false;
            //    btnAdd.ToolTip = Constant.ToolTip;
            //    btnAdd.CssClass += " disable";
            //}
            //#endregion



            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", page);
                }
                else
                {
                    Session["CurrentPage"] = page;
                }
                BindCoQuanDDL();
                BindRepeater();
            }
        }

        #region preLoad
        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            int totalRow = 0;
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            txtSearch.Text = keyword;

            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();

            try
            {
                keyword = "%" + keyword + "%";
                //if (capID == (int)CapQuanLy.CapUBNDTinh)
                //{
                if (ddlCoQuan.SelectedItem.Value == "0")
                {
                    //totalRow = new LichSuTraCuu().CountSearch(keyword);
                    totalRow = new LichSuTraCuu().CountSearchByTreeView(keyword, coQuanUserID);
                }
                else
                {
                    int coQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedItem.Value, 0);
                    totalRow = new LichSuTraCuu().CountSearch_CoQuanID(keyword, coQuanID);
                }
                //}
                //else
                //{
                //    totalRow = new LichSuTraCuu().CountByCoQuanChaID(keyword, coQuanUserID);
                //}
            }
            catch
            {

            }

            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0)
            {
                pageCount++;
            }

            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
            }
        }

        #endregion
        private void BindCoQuanDDL()
        {
            List<CoQuanInfo> cqList = new List<CoQuanInfo>();
            List<CoQuanInfo> parentList = new List<CoQuanInfo>();

            int capID = IdentityHelper.GetCapID();
            int coQuanUserID = IdentityHelper.GetCoQuanID();
            
            

            if(capID == (int)CapQuanLy.CapTinh || capID == (int)CapQuanLy.CapUBNDTinh) // la cap tinh lay het co quan
            {
                cqList = (List<CoQuanInfo>)new DAL.CoQuan().GetAllCoQuan();
            }
            else
            {
                cqList = (List<CoQuanInfo>)new DAL.CoQuan().GetCoQuanTreeView(coQuanUserID);
            }

            ddlCoQuan.DataSource = cqList;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuan.SelectedIndex = 0;
        }

        private void TreeSort(ref List<CoQuanInfo> cqList, CoQuanInfo parentInfo, int level)
        {
            string prefix = string.Empty;
            string delta = "";
            for (int i = 0; i < level; i++)
            {
                prefix += delta;
            }
            level++;
            parentInfo.TenCoQuan = prefix + parentInfo.TenCoQuan;

            cqList.Add(parentInfo);

            List<CoQuanInfo> childList = (List<CoQuanInfo>)new DAL.CoQuan().GetCoQuanByParentID(parentInfo.CoQuanID);
            foreach (CoQuanInfo childInfo in childList)
            {
                TreeSort(ref cqList, childInfo, level);
            }
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
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
                //if (capID == (int)CapQuanLy.CapUBNDTinh)
                //{
                if (ddlCoQuan.SelectedItem.Value == "0")
                {
                    if(capID == (int)CapQuanLy.CapTinh || capID == (int)CapQuanLy.CapUBNDTinh)
                    {
                        rptLichSu.DataSource = new LichSuTraCuu().GetBySearch(keyword, start, end);
                    }
                    else
                    {
                        rptLichSu.DataSource = new LichSuTraCuu().GetByTreeView(keyword, start, end, coQuanUserID);
                    }
                    
                }
                else
                {
                    int coQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedItem.Value, 0);
                    rptLichSu.DataSource = new LichSuTraCuu().GetBySearch_CoQuanID(keyword, start, end, coQuanID);
                }
                //}
                //else
                //{
                //    rptLichSu.DataSource = new LichSuTraCuu().GetBySearch_CoQuanChaID(keyword, start, end, coQuanUserID);
                //}
            }
            catch (Exception ex)
            {

            }
            rptLichSu.DataBind();

            if (rptLichSu.Items.Count == 0 && currentPage != 1)
            {
                currentPage = 1;
                Session.Add("CurrentPage", currentPage);
                Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
            }
        }

        protected void rptLichSu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            //ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;

            //if (!AccessControl.User.HasPermission(ChucNangEnum.LichSu, AccessLevel.Edit)) // nếu ko có quền sửa thì disable chức năng 
            //{
            //    btnEdit.Enabled = false;
            //    btnEdit.ToolTip = Constant.ToolTip;
            //    btnEdit.CssClass += " disable";
            //}

            //if (!AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Delete))  // nếu ko có quền xoá thì disable chức năng 
            //{
            //    btnDelete.Enabled = false;
            //    btnDelete.ToolTip = Constant.ToolTip;
            //    btnDelete.CssClass += "disable";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            Session.Add("CurrentPage" + Request.Url.AbsolutePath, "1");

            BindRepeater();
        }

        protected void ddlCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }
    }
}