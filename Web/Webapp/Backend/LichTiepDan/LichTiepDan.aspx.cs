using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.LichTiepDan;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.LichTiepDan
{
    public partial class LichTiepDan : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.LichTiepDan, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.LichTiepDan, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
            if (!IsPostBack)
            {
                LoadCoQuan();
                SetSession();
                BindRepeater();
                createPaging();
            }
        }

        public void LoadCoQuan()
        {
            ddlCoQuan.DataSource = new Com.Gosol.CMS.DAL.CoQuan().GetAllCoQuan();
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("Tất cả","0"));
            ddlCoQuan.SelectedIndex = 0;
            ddlCoQuanTiep.DataSource = new Com.Gosol.CMS.DAL.CoQuan().GetAllCoQuan();
            ddlCoQuanTiep.DataBind();
            //ddlCoQuanTiep.Items.Insert(0, "Tất cả");
            //ddlCoQuanTiep.SelectedIndex = 0;
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
            }

            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }

            if (Session["CoQuan"] == null)
            {
                Session.Add("CoQuan", ddlCoQuan.SelectedValue);
            }
            else
            {
                Session["CoQuan"] = ddlCoQuan.SelectedValue;
            }
        }

        protected void BindRepeater()
        {
            int currentPage = String.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
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

            int coQuanID = Utils.ConvertToInt32(Session["CoQuan"], 0);
            //int coQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0);

            try
            {
                keyword = "%" + keyword + "%";
                string parmKeyword = keyword;
                rptLichTiepDan.DataSource = new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().GetLichTiepBySearch(keyword, coQuanID, start, end);
                rptLichTiepDan.DataBind();
            }
            catch
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptLichTiepDan.Items.Count == 0)
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
            txtSearch.Text = keyword;

            try
            {
                keyword = "%" + keyword + "%";
                //int coQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0);
                int coQuanID = Utils.ConvertToInt32(Session["CoQuan"], 0);
                totalRow = new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().CountSearch(keyword, coQuanID);
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

        protected void rptLichTiepDan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;

            //Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai");
            Label lblNgayTiep = (Label)e.Item.FindControl("lblNgayTiep");
            LichTiepDanInfo info = e.Item.DataItem as LichTiepDanInfo;

            CheckBox cbHienThi = (CheckBox)e.Item.FindControl("cbHienThi");
            if (info.Public == true)
                //lblTrangThai.Text = "Hiển thị";
                cbHienThi.Checked = true;
            else
                cbHienThi.Checked = false;
            //lblTrangThai.Text = "Không hiển thị";
            //cbHienThi.Enabled = false;
            if (info.NgayTiep != DateTime.MinValue)
                lblNgayTiep.Text = Format.FormatDate(info.NgayTiep);
            else
                lblNgayTiep.Text = "";

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
            Session["Keyword" + Request.Url.AbsolutePath] = txtSearch.Text.Trim();

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
                    new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().Delete(IDLoaiTin);
                    lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch
                {
                    lblthongBaoDeleteError.Text = Constant.CONTENT_DELETE_ERR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoDeleteError", "showthongBaoDeleteError()", true);
                }
            }
            hdDeleteID.Value = "0";
            BindRepeater();
            createPaging();
        }

        public LichTiepDanInfo GetDataLichTiepDan()
        {
            LichTiepDanInfo info = new LichTiepDanInfo();
            info.IDLichTiep = Utils.ConvertToInt32(hdfIDLichTiepEdit.Value, 0);
            info.IDCoQuanTiep = Utils.ConvertToInt32(ddlCoQuanTiep.SelectedValue, 0);
            //info.IDCanBoTiep = Utils.ConvertToInt32(ddlCanBoTiep.SelectedValue, 0);
            info.IDCanBoTiep = Utils.ConvertToInt32(hdfCanBoTiep.Value, 0);
            info.NDTiep = Utils.ConvertToString(txtLoaiTin.Text, string.Empty);
            info.NgayTiep = Utils.ConvertToDateTime(txtTuNgayFilter.Text, DateTime.MinValue);
            info.Public = Utils.ConvertToBoolean(checkPublic.Checked, false);
            info.Creater = IdentityHelper.GetCanBoID();
            info.CreateDate = DateTime.Now;
            info.Editer = IdentityHelper.GetCanBoID();
            info.EditDate = DateTime.Now;
            return info;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LichTiepDanInfo info = GetDataLichTiepDan();
            int status = 0;
            if (info.IDLichTiep != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().Update(info);
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch
                {
                    lblThongBaoError.Text = Constant.CONTENT_MESSAGE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                }
            }
            else
            {
                try
                {
                    status = new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().Insert(info);
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
                    lblThongBaoError.Text = Constant.MESSAGE_INSERT_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                }
            }

            BindRepeater();
            createPaging();
            hdfCheckEdit.Value = "0";
        }

        [System.Web.Services.WebMethod]
        public static string GetByID(string idLoaiTin)
        {
            LichTiepDanInfo loaiTinInfo = new LichTiepDanInfo();
            int loaiTinID = Utils.ConvertToInt32(idLoaiTin, 0);
            try
            {
                loaiTinInfo = new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().GetLichTiepByID(loaiTinID);
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

        [System.Web.Services.WebMethod]
        public static string GetCanBoByCoQuanID(string idCoQuan)
        {
            int CoQUanID = Utils.ConvertToInt32(idCoQuan, 0);
            IList<CanBoInfo> lsCanBo = new List<CanBoInfo>();
            string a = "";

            try
            {
                lsCanBo = new Com.Gosol.CMS.DAL.CanBo().GetByCoQuanID(CoQUanID);

                if (lsCanBo.Count > 0)
                {
                    a += "<option value='0'>Tất cả</option>";
                    foreach (CanBoInfo info in lsCanBo)
                    {
                        a += "<option value='" + info.CanBoID + "'>" + info.TenCanBo + "</option>";
                    }
                }
                else
                {
                    a = String.Empty;
                }

            }
            catch
            {
            }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(a);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindRepeater();
            createPaging();
        }

        protected void ddlCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CoQuan"] = ddlCoQuan.SelectedValue;
            BindRepeater();
            createPaging();
        }
    }
}