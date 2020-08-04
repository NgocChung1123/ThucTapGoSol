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

namespace Com.Gosol.CMS.Web.Webapp.Backend.QLTinTuc
{
    public partial class QuanLyTinTuc : System.Web.UI.Page
    {
        private int stt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyTinTuc, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyTinTuc, AccessLevel.Create)) // màn hình này ko có thêm mới dữ liệu. Nếu có thì ẩn button thêm
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

            ddlLoaiTin_Filter.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLoaiTin().GetAllLoaiTin();
            ddlLoaiTin_Filter.DataBind();
            ddlLoaiTin_Filter.Items.Insert(0, "Tìm kiếm theo loại tin");
            ddlLoaiTin_Filter.SelectedIndex = 0;
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
                Session["LoaiTinID" + Request.Url.AbsolutePath] = null;
            }

            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }
            if (Session["LoaiTinID" + Request.Url.AbsolutePath] != null)
            {
                ddlLoaiTin_Filter.SelectedValue = Session["LoaiTinID" + Request.Url.AbsolutePath].ToString();
            }
        }

        protected void BindRepeater()
        {
            int currentPage = String.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            string keyword = txtSearch.Text;
            string loaiTinID_Str = ddlLoaiTin_Filter.SelectedValue;

            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }

            if (Session["LoaiTinID" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LoaiTinID" + Request.Url.AbsolutePath, loaiTinID_Str);
            }
            else
            {
                Session["LoaiTinID" + Request.Url.AbsolutePath] = loaiTinID_Str;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            try
            {
                keyword = "%" + keyword + "%";
                string parmKeyword = keyword;
                int loaiTinID = Utils.ConvertToInt32(loaiTinID_Str, 0);
                rptTinTuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMTinTuc().GetTinTucBySearch(keyword, loaiTinID, start, end);
                rptTinTuc.DataBind();
            }
            catch
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptTinTuc.Items.Count == 0)
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
          int  loaiTinID = Utils.ConvertToInt32(ddlLoaiTin_Filter.SelectedValue, 0);
            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMTinTuc().CountSearch(keyword, loaiTinID);
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
        
        protected void rptTinTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;
            Label lblNgayTao = (Label)e.Item.FindControl("lblNgayTao");
            Label lblNgaySua = (Label)e.Item.FindControl("lblNgaySua");
            Label lblAnh = (Label)e.Item.FindControl("lblAnh");
            Label lblNoiDung = (Label)e.Item.FindControl("lblNoiDung");
           
            ImageButton ImageAnh = (ImageButton)e.Item.FindControl("ImageAnh");

            DMTinTucInfo info = e.Item.DataItem as DMTinTucInfo;

            System.Web.UI.HtmlControls.HtmlTableCell tieuDe = e.Item.FindControl("tieuDe") as System.Web.UI.HtmlControls.HtmlTableCell;
            tieuDe.Attributes["class"] = "myCssClass";

            if (info.CreateDate != DateTime.MinValue)
            {
                lblNgayTao.Text = Format.FormatDate(info.CreateDate);
            }
            if (info.EditDate != DateTime.MinValue)
            {
                lblNgaySua.Text = Format.FormatDate(info.EditDate);
            }
            //if (info.NoiDung != string.Empty)
            //{
            //    if (info.NoiDung.Length > 300)
            //        lblNoiDung.Text = info.NoiDung.Substring(0, 300);
            //    else
            //        lblNoiDung.Text = info.NoiDung;
            //}

            lblNoiDung.Text = info.TomTat;
            string[] result;
            if (info.ImageUrl != null && info.ImageUrl != "")
            {
                result = info.ImageUrl.Split('/');

                //lblAnh.Text = "http://localhost:10003/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg";
                ImageAnh.ImageUrl = "~/" + info.ImageUrl;
            }
            else
            {
                ImageAnh.ImageUrl = "";
            }
            CheckBox cbHienThi = (CheckBox)e.Item.FindControl("cbHienThi");
            if (info.Public == true)
            {
                cbHienThi.Checked = true;
            }
            else
            {
                cbHienThi.Checked = false;
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

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int IDTinTuc = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            if (IDTinTuc != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMTinTuc().Delete(IDTinTuc);
                    lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                catch
                {
                    lblContentSuccess.Text = Constant.CONTENT_DELETE_ERR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
            }
            hdDeleteID.Value = "0";
            BindRepeater();
            createPaging();
        }
        public DMTinTucInfo GetData()
        {
            DMTinTucInfo info = new DMTinTucInfo();
            info.IDTinTuc = Utils.ConvertToInt32(hdfIDTinTucEdit.Value, 0);
            info.TieuDe = Utils.ConvertToString(txtTieuDe.Text, string.Empty);
            info.TomTat = Utils.ConvertToString(txtTomTat.Text, string.Empty);
            info.NoiDung = Utils.ConvertToString(CKEditorNoiDung.Text, string.Empty);
            info.IDLoaiTin = Utils.ConvertToInt32(ddlLoaiTin.SelectedValue, 0);
            info.laTinHot = Utils.ConvertToBoolean(checkLaTinHot.Checked, false);
            info.Public = Utils.ConvertToBoolean(checkPublic.Checked, false);
            info.Creater = IdentityHelper.GetCanBoID();
            info.CreateDate = DateTime.Now;
            info.Editer = IdentityHelper.GetCanBoID();
            info.EditDate = DateTime.Now;

            string file_upload = hdfFileUpload.Value;
            if (file_upload != String.Empty)
            {

                info.ImageUrl = hdfFileUpload.Value;
            }
            else
            {
                info.ImageUrl = "";
            }

            return info;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DMTinTucInfo info = GetData();
            int status = 0;
            if (info.IDTinTuc != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMTinTuc().Update(info);
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                catch
                {
                    lblContentSuccess.Text = Constant.MESSAGE_UPDATE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
            }
            else
            {
                try
                {
                    status = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMTinTuc().Insert(info);
                    lblContentSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                catch
                {
                    lblContentSuccess.Text = Constant.MESSAGE_INSERT_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                if (status > 0)
                {
                  
                }
                else
                {

                }
            }
            BindRepeater();
            createPaging();
            ClearForm();
        }

        protected void ClearForm()
        {
            hdfIDTinTucEdit.Value = "";
            txtTieuDe.Text = "";
            txtTomTat.Text = "";
            CKEditorNoiDung.Text = "";
            ddlLoaiTin.SelectedIndex = 0;
            checkLaTinHot.Checked = false;
            checkPublic.Checked = false;
            imageTieuDe.ImageUrl = "";
            hdfFileUpload.Value = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRepeater();
            createPaging();
        }

        [System.Web.Services.WebMethod]
        public static string GetByID(string idTinTuc)
        {
            DMTinTucInfo tinTucInfo = new DMTinTucInfo();
            int tinTucID = Utils.ConvertToInt32(idTinTuc, 0);
            try
            {
                tinTucInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMTinTuc().GetTinTucByID(tinTucID);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(tinTucInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected void ddlLoaiTin_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
            createPaging();
        }
    }
}