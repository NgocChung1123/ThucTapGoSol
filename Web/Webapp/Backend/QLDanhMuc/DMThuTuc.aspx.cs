using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc
{
    public partial class DMThuTuc : System.Web.UI.Page
    {
        private int stt = 1;
        private int b = 0;
        private int d = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucThuTuc , AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucThuTuc, AccessLevel.Create))
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
                //CreatePaging();
                LoadLoaiThuTuc();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            CreatePaging();
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
            try
            {
                keyword = "%" + keyword + "%";
                string parmKeyword = keyword;
                //rptLoaiThuTuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().GetBySearch(keyword, start, end);
                rptLoaiThuTuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().GetBySearch1(keyword, start, end);
                //rptLoaiThuTuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().GetParentIDBySearch(keyword, start, end);
                rptLoaiThuTuc.DataBind();
            }
            catch (Exception ex)
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptLoaiThuTuc.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage = 1;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }
        [WebMethod]
        public static string LoadThuTuc(string id)
        {
            List<DMThuTucInfo> thuTucInfo = new List<DMThuTucInfo>();

            int loaithutuc_id = Utils.ConvertToInt32(id, 0);
            try
            {
                thuTucInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().GetByLoaiThuTucID(loaithutuc_id);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(thuTucInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }
        public void LoadLoaiThuTuc()
        {
            ddlLoaiThuTuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().GetAll();
            ddlLoaiThuTuc.DataBind();
            //ddlLoaiThuTuc.Items.Insert(0, new ListItem("Tất cả", ""));
            ddlLoaiThuTuc.SelectedIndex = 0;
        }
        private void CreatePaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            try
            {
                keyword = "%" + keyword + "%";
                //totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().CountSearch(keyword);
                //totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().CountSearch(keyword);
                //totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().CountSearch(keyword);
                totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().CountSearch1(keyword);
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

        protected void rptLoaiThuTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var info = (DMThuTucInfo)e.Item.DataItem;

            int a = new DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().GetNumSteps(info.LoaiThuTucID);
            int c = info.LoaiThuTucID;

            var td = (HtmlTableCell)e.Item.FindControl("tdLoaiThuTuc");
            var td2 = (HtmlTableCell)e.Item.FindControl("tdSTT");
            var td3 = (HtmlTableCell)e.Item.FindControl("td3");
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            if (a == b && c==d)
            {
                //td.Attributes.Add("style", "display:none");
                td.Style.Add("display", "none");
                td2.Style.Add("display", "none");
                td3.Style.Add("display", "none");
            }
            else
            {
                td.Attributes.Add("rowspan", a.ToString());

                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
                stt++;
                td2.Attributes.Add("rowspan", a.ToString());
                td3.Attributes.Add("rowspan", a.ToString());

            }
            b = a;
            d = c;


            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucThuTuc, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucThuTuc, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += "disable";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSession();
            BindRepeater();
            //CreatePaging();
        }

        [WebMethod]
        public static string GetByID(string id)
        {
            DMThuTucInfo thuTucInfo = new DMThuTucInfo();
            int thutucID = Utils.ConvertToInt32(id, 0);
            try
            {
                thuTucInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().GetByID(thutucID);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(thuTucInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }

        [WebMethod]
        public static string GetByOrder(string id)
        {
            DMThuTucInfo thuTucInfo = new DMThuTucInfo();
            int thutucID = Utils.ConvertToInt32(id, 0);
            try
            {
                thuTucInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().GetByOrder(thutucID);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(thuTucInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }

        public DMThuTucInfo GetData()
        {
            DMThuTucInfo info = new DMThuTucInfo();
            info.ThuTucID = Utils.ConvertToInt32(hdfIDEdit.Value, 0);
            info.LoaiThuTucID = Utils.ConvertToInt32(hdfLoaiThuTucID.Value, 0);
            info.NDThuTuc = Utils.ConvertToString(txtNDThuTuc.Text, string.Empty);
            info.Order = Utils.ConvertToInt32(txtOrder.Text, 0);
            info.Creater = IdentityHelper.GetCanBoID();
            info.CreateDate = DateTime.Now;
            info.Editer = IdentityHelper.GetCanBoID();
            info.EditDate = DateTime.Now;
            return info;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DMThuTucInfo info = GetData();
            string fileQuyetDinh = string.Empty;
            if (fileDinhKem.HasFile)
            {
                fileQuyetDinh = fileDinhKem.FileName;
                fileDinhKem.PostedFile.SaveAs(Server.MapPath("~/UploadFiles/") + fileQuyetDinh);
            }
            info.FileDinhKem = fileQuyetDinh;
            int status = 0;
            if (info.ThuTucID != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().Update(info);
                    if (fileQuyetDinh != "")
                    {
                        fileDinhKem.SaveAs(Server.MapPath("~/UploadFiles/") + fileDinhKem.FileName);
                    }
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
                    status = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().Insert(info);
                    if (fileQuyetDinh != "")
                    {
                        fileDinhKem.SaveAs(Server.MapPath("~/UploadFiles/") + fileDinhKem.FileName);
                    }
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
            //CreatePaging();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Utils.ConvertToInt32(hdDeleteID.Value, 0);

            if (id != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMThuTuc().Delete(id);
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
            //CreatePaging();
        }
    }
}