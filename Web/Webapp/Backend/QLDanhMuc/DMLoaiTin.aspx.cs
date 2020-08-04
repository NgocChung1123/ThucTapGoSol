using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc
{
    public partial class DMLoaiTin : System.Web.UI.Page
    {
        private int stt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiTin, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiTin, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
            if (!IsPostBack)
            {
                LoadDanhMucCha();
                SetSession();
                BindRepeater();
                createPaging();
            }
        }

        public void LoadDanhMucCha()
        {
            ddlLoaiTin.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().GetAllLoaiTin();
            ddlLoaiTin.DataBind();
            ddlLoaiTin.Items.Insert(0, "Tất cả");
            ddlLoaiTin.SelectedIndex = 0;
        }

        protected void SetSession()
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

            int pageCheckTab = Utils.ConvertToInt32(Request.Params["page"], 0);

            if (pageCheckTab < 1)
            {
                Session["Keyword" + Request.Url.AbsolutePath] = null;
            }


            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }
        }

        protected void BindRepeater()
        {
            int currentPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            string keyword = txtSearch.Text;

            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            try
            {
                keyword = "%" + keyword + "%";
                string parmKeyword = keyword;
                rptLoaiTin.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().GetLoaiTinBySearch(keyword, start, end);
                rptLoaiTin.DataBind();
            }
            catch
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptLoaiTin.Items.Count == 0)
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
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            txtSearch.Text = keyword;
            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().CountSearch(keyword);
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


        protected void rptLoaiTin_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;
            Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai");
            DMLoaiTinInfo info = e.Item.DataItem as DMLoaiTinInfo;
            if (info.Public == true)
            {
                lblTrangThai.Text = "Hiển thị";
            }
            else
            {
                lblTrangThai.Text = "Không hiển thị";
            }

            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (!AccessControl.User.HasPermission(ChucNangEnum.LichTiepDan, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.LichTiepDan, AccessLevel.Delete))
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
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().Delete(IDLoaiTin);
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

        public DMLoaiTinInfo GetDataDMLoaiTin()
        {
            DMLoaiTinInfo info = new DMLoaiTinInfo
            {
                IDLoaiTin = Utils.ConvertToInt32(hdfIDLoaiTinEdit.Value, 0),
                ParentID = Utils.ConvertToInt32(ddlLoaiTin.SelectedValue, 0),
                TenLoaiTin = Utils.ConvertToString(txtLoaiTin.Text, string.Empty),
                GhiChu = Utils.ConvertToString(txtGhiChu.Text, string.Empty),
                Public = Utils.ConvertToBoolean(checkPublic.Checked, false),
                Order = Utils.ConvertToInt32(txtOrder.Text, 0),
                Creater = IdentityHelper.GetCanBoID(),
                CreateDate = DateTime.Now,
                Editer = IdentityHelper.GetCanBoID(),
                EditDate = DateTime.Now
            };
            return info;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DMLoaiTinInfo info = GetDataDMLoaiTin();
            int status = 0;
            if (info.IDLoaiTin != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().Update(info);
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
                    status = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().Insert(info);
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
            ClearForm();
        }

        protected void ClearForm()
        {
            txtLoaiTin.Text = "";
            txtGhiChu.Text = "";
            txtOrder.Text = "";
            ddlLoaiTin.SelectedIndex = 0;
            checkPublic.Checked = false;
        }

        [System.Web.Services.WebMethod]
        public static string GetByID(string idLoaiTin)
        {
            DMLoaiTinInfo loaiTinInfo = new DMLoaiTinInfo();
            int loaiTinID = Utils.ConvertToInt32(idLoaiTin, 0);
            try
            {
                loaiTinInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().GetLoaiTinByID(loaiTinID);
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
    }
}