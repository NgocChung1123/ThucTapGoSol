using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc
{
    public partial class DMLinhVuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLinhVuc, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLinhVuc, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
            if (!IsPostBack)
            {
                SetSession();
                BindRepeater();
                createPaging();
                LoadTrangThai();
            }

        }

        protected void SetSession()
        {
            int page = Utils.ConvertToInt32(Request.Params["page"], 1);
            if (Session["CurrentPage"] == null)
                Session.Add("CurrentPage", page);
            else Session["CurrentPage"] = page;

            int pageCheckTab = Utils.ConvertToInt32(Request.Params["page"], 0);

            if (pageCheckTab < 1)
            {
                Session["Keyword" + Request.Url.AbsolutePath] = null;
                Session["TrangThai" + Request.Url.AbsolutePath] = null;
            }
            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }
            if (Session["TrangThai" + Request.Url.AbsolutePath] != null)
            {
                ddlTrangThai.SelectedValue = Session["TrangThai" + Request.Url.AbsolutePath].ToString();
            }
        }

        protected void BindRepeater()
        {
            int currentPage = String.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            string keyword = txtSearch.Text;
            int TrangThai = Utils.ConvertToInt32(ddlTrangThai.SelectedValue, 0);


            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }

            if (Session["TrangThai" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("TrangThai" + Request.Url.AbsolutePath, TrangThai);
            }
            else
            {
                Session["TrangThai" + Request.Url.AbsolutePath] = TrangThai;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            try
            {
                keyword = "%" + keyword + "%";
                string parmKeyword = keyword;
                rptLinhVuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetDMLinhVucBySearch(keyword, TrangThai, start, end);
                rptLinhVuc.DataBind();
            }
            catch
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptLinhVuc.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage = 1;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        private void createPaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            int TrangThai = Utils.ConvertToInt32(Session["TrangThai" + Request.Url.AbsolutePath], 0);
            txtSearch.Text = keyword;
            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().CountSearch(keyword, TrangThai);
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


        protected void rptLinhVuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai");
            DMLinhVucInfo info = e.Item.DataItem as DMLinhVucInfo;
            if (info.Public == true)
                lblTrangThai.Text = "Hiển thị";
            else
                lblTrangThai.Text = "Không hiển thị";
            Button btnEdit = (Button)e.Item.FindControl("btnEdit");
            Button btnDelete = (Button)e.Item.FindControl("btnDelete");
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLinhVuc, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLinhVuc, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += "disable";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRepeater();
            createPaging();
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int IDLoaiTin = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            if (IDLoaiTin != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().Delete(IDLoaiTin);
                    lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch
                {
                    lblContentSuccess.Text = Constant.CONTENT_DELETE_ERR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
            }
            hdDeleteID.Value = "0";
            BindRepeater();
            createPaging();
        }

        public DMLinhVucInfo GetDataDMLinhVuc()
        {
            DMLinhVucInfo info = new DMLinhVucInfo();
            info.IDLinhVuc = Utils.ConvertToInt32(hdfIDDMLinhVucEdit.Value, 0);
            info.TenLinhVuc = Utils.ConvertToString(txtLoaiTin.Text, string.Empty);
            info.GhiChu = Utils.ConvertToString(txtGhiChu.Text, string.Empty);
            info.Public = Utils.ConvertToBoolean(checkPublic.Checked, false);
            info.Creater = IdentityHelper.GetCanBoID();
            info.CreateDate = DateTime.Now;
            info.Editer = IdentityHelper.GetCanBoID();
            info.EditDate = DateTime.Now;
            return info;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DMLinhVucInfo info = GetDataDMLinhVuc();
            int status = 0;
            if (info.IDLinhVuc != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().Update(info);
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch
                {
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
            }
            else
            {
                try
                {
                    status = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().Insert(info);
                }
                catch
                {
                    throw;
                }
                if (status > 0)
                {
                    lblContentSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                else
                {
                    lblContentSuccess.Text = Constant.MESSAGE_INSERT_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
            }

            BindRepeater();
            createPaging();
        }

        [System.Web.Services.WebMethod]
        public static string GetByID(string idLoaiTin)
        {
            DMLinhVucInfo loaiTinInfo = new DMLinhVucInfo();
            int loaiTinID = Utils.ConvertToInt32(idLoaiTin, 0);
            try
            {
                loaiTinInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetDMLinhVucByID(loaiTinID);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(loaiTinInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }


        public void LoadTrangThai()
        {
            var listTrangThai = new List<object>()
            {
                new { TrangThaiID = 0, TenTrangThai = "Trạng thái" },
                new { TrangThaiID = 1, TenTrangThai = "Hiển thị" },
            new { TrangThaiID = 2, TenTrangThai = "Không hiển thị" }
            };

            ddlTrangThai.DataSource = listTrangThai;
            ddlTrangThai.DataBind();
        }

        protected void ddlTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
            createPaging();
        }
    }
}