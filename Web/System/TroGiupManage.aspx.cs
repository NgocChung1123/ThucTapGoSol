using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.DAL.HeThong;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class TroGiupManage : System.Web.UI.Page
    {
        public int stt = 1;
        public int pageSize = IdentityHelper.GetPageSize();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Read))
            {
                //Redirect
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Create))
            {
                //btnThem.Enabled = false;
                //btnThem.ToolTip = Constant.NoCreate;
                //btnThem.CssClass += " disable";
            }

            if (!IsPostBack)
            {
                int currentPage = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", currentPage);
                }
                else
                {
                    Session["CurrentPage"] = currentPage;
                }

                BindRepeater();
                light.Visible = false;
                popXoa.Visible = false;
                fade.Attributes.CssStyle["display"] = "none";
            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Danh mục");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CreatePaging();
        }
        private void CreatePaging()
        {
            int total = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            if (String.IsNullOrEmpty(keyword))
            {
                try
                {
                    total = new FileTaiLieu().CountAll();
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
                    total = new FileTaiLieu().CountSearch(keyword);
                }
                catch
                {
                }
            }
            int pSize = IdentityHelper.GetPageSize();
            int pageCount = total / pSize;
            if (total % pSize != 0) pageCount++;
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
        //Bind du lieu
        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
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

                    rptFileTaiLieu.DataSource = new FileTaiLieu().GetByPage(currentPage, start, end);
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
                    rptFileTaiLieu.DataSource = new FileTaiLieu().GetBySearch(keyword, currentPage, start, end);
                }
                catch
                {
                }
            }
            rptFileTaiLieu.DataBind();
        }

        //Su kien button Edit
        protected void btnSua_Click(object sender, EventArgs e)
        {
            light.Visible = true;
            popXoa.Visible = false;
            fade.Attributes.CssStyle["display"] = "block";
            DanToc c = new DanToc();

            //Chuyen doi du lieu tu button edit click
            LinkButton btnSua = (LinkButton)sender;
            int DanToc_id = Utils.ConvertToInt32(btnSua.CommandArgument, 0);

            try
            {
                DanTocInfo DTInfo = c.GetByID(DanToc_id);
                DanTocID.Text = DTInfo.DanTocID.ToString();
                txtTenDanToc.Text = DTInfo.TenDanToc;

            }
            catch
            {

            }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            DanToc DT = new DanToc();

            DanTocInfo DTInfo = new DanTocInfo();
            if (DanTocID.Text == string.Empty)
            { //Truong hop them moi du lieu
                DTInfo.TenDanToc = txtTenDanToc.Text;
                int status = 0;
                if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Create))
                {

                    try
                    {
                        status = DT.Insert(DTInfo);
                    }
                    catch
                    {
                        throw;
                    }
                    if (status > 0)
                    {
                        lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);

                    }
                    if (status == -1)
                    {
                        lblContentSuccess.Text = "";
                        lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
            }
            else
            { //Truong hop update du lieu

                DTInfo.DanTocID = Utils.ConvertToInt32(DanTocID.Text, 0);
                DTInfo.TenDanToc = txtTenDanToc.Text;
                if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Edit))
                {
                    try
                    {
                        DT.Update(DTInfo);
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


            light.Visible = false;
            popXoa.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
            BindRepeater();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            DanTocID.Text = "";
            txtTenDanToc.Text = "";

            fade.Attributes.CssStyle["display"] = "block";
            light.Visible = true;
            popXoa.Visible = false;

        }

        //protected void btnXoa_Click(object sender, EventArgs e)
        //{

        //    //Show fade
        //    fade.Attributes.CssStyle["display"] = "block";
        //    popXoa.Visible = true;
        //    light.Visible = false;


        //    LinkButton btnXoa = (LinkButton)sender;
        //    int file_id = Utils.ConvertToInt32(btnXoa.CommandArgument, 0);
        //    id_xoa.Text = file_id.ToString();
        //    //DanToc DT = new DanToc();
        //    try
        //    {
        //        new FileTaiLieu().Delete(file_id);
        //        //DanTocInfo cInfo = DT.GetByID(DanToc_id);
        //        //ten_xoa.Text = cInfo.TenDanToc;
        //    }
        //    catch
        //    {  
        //    }
        //}

        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            int DT_id = Utils.ConvertToInt32(id_xoa.Text, 0);

            try
            {
                new FileTaiLieu().Delete(DT_id);
                //-------------------------------
                //int total = 0;
                //int curentTotal = 0;
                //int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
                //total = new DanToc().CountAll();
                //curentTotal = (page - 1) * pageSize;

                //if (total <= curentTotal)
                //{
                //    int newpage = 0;
                //    newpage = page - 1;
                //    Response.Redirect("DanTocManage.aspx?page=" + newpage);
                //}
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
            CreatePaging();
            BindRepeater();

            popXoa.Visible = false;
            light.Visible = false;
            fade.Attributes.CssStyle["display"] = "none";
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
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
            Response.Redirect(Request.Url.AbsolutePath);
        }

        protected void rptFileTaiLieu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.NoEdit;
                btnEdit.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucDanToc, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.NoDelete;
                btnDelete.CssClass += " disable";
            }

            int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
            if (page == null)
                page = 1;
            stt = (page - 1) * pageSize + 1;

            Label lblNgayUp = (Label)e.Item.FindControl("lblNgayUp");

            FileTaiLieuInfo info = (FileTaiLieuInfo)e.Item.DataItem;
            lblNgayUp.Text = Format.FormatDate(info.NgayUp);
        }

        //upload filescan

        

        protected void btnSaveFileOnclick(object sender, EventArgs e)
        {
            //int xulydonID = 0;//Utils.ConvertToInt32(hdf_xulydonId.Value, 0);
            //int fileId = Utils.ConvertToInt32(txt_fileid.Text, 0);
            if(FileUploadControl.HasFile)
            {
                string filename = Path.GetFileName(FileUploadControl.FileName);
                FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
            }
           

            FileTaiLieuInfo info = new FileTaiLieuInfo();
            info.FileTaiLieuID = Utils.ConvertToInt32(txt_fileid.Text, 0);
            info.NgayUp = Utils.ConvertToDateTime(txt_ngayscan.Text, Constant.DEFAULT_DATE);
            info.NguoiUp = IdentityHelper.GetUserID();
            info.TenFile = txt_tenfile.Text;
            info.TomTat = txt_tomtat.Text;
            //info.FileUrl = txt_fileurl.Text;
            int kq = 0;
            if (!String.IsNullOrEmpty(info.FileUrl))
            {
                if (!String.IsNullOrEmpty(txt_foruploadfile.Text))
                    info.FileUrl = txt_foruploadfile.Text;

                if (info.FileTaiLieuID < 1)
                {
                    //insert to HoSoFile
                    try
                    {
                        kq = new FileTaiLieu().Insert(info);
                    }
                    catch
                    {

                    }

                    if (kq != 0)
                    {

                        lblContentErr.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                    else
                    {
                        lblContentSuccess.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                    }
                }
                else
                {
                    if (txt_foruploadfile.Text != string.Empty)
                    {
                        //delete older file
                        deleteFileScan(info.FileTaiLieuID);
                        //string url = Server.MapPath("~/UploadFiles/encrypt/") + info.FileURL;//Utils.ConvertToString(txt_fileurl.Text, String.Empty);
                        //if (File.Exists(url))
                        //    File.Delete(url);
                    }
                    try
                    {
                        kq = new FileTaiLieu().Update(info);
                    }
                    catch
                    {
                    }
                    if (kq != 0)
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
                    //xoa session
                    Session["filePath"] = null;
                }

                BindRepeater();

            }
            else
            {
                if (info.FileTaiLieuID < 1)
                {
                    show_err_msg.Text = "true";
                    plh_err_file.Visible = true;
                    file_err_msg.Text = "Vui lòng chọn file upload!";
                }
                else
                {
                    plh_err_file.Visible = false;
                    try
                    {
                        new FileTaiLieu().Update(info);
                    }
                    catch
                    {

                    }
                    //bindFileHoSo();
                }
            }

            hideFileScanDiv.Text = "hidden";
        }

        protected void deleteFileScan(int fileId)
        {
            FileTaiLieuInfo fileDelete = new FileTaiLieu().GetByID(fileId);
            if (fileDelete != null)
            {
                //xoa file scan
                string url_fileDelete = Server.MapPath("~/UploadFiles/encrypt/") + fileDelete.FileUrl;
                if (File.Exists(url_fileDelete))
                    File.Delete(url_fileDelete);
            }
        }
        protected void btnDeleteFileHoSo_Clicked(object sender, EventArgs e)
        {
            txt_hideFileDeleteConfirm.Text = "hidden";

            int hosoId = Utils.ConvertToInt32(deleteFileHoSoID.Value, 0);
            int nguoiUp = Utils.ConvertToInt32(nguoiUpFileID.Value, 0);
            if (hosoId > 0 && nguoiUp == IdentityHelper.GetUserID())
            {
                //xoa file
                try
                {
                    deleteFileScan(hosoId);

                    //xoa ho so trong db
                    new FileTaiLieu().Delete(hosoId);
                    lblContentSuccess.Text = "Xóa dữ liệu thành công!";
                    lblContentErr.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                catch
                {


                }
                //bind lai file ho so
                //bindFileHoSo();
                CreatePaging();
                BindRepeater();
                plh_fileErr.Visible = false;
                lbl_file_err_msg.Text = "";
            }
            else
            {
                plh_fileErr.Visible = true;
                lbl_file_err_msg.Text = "Không thể xóa File hồ sơ";
            }
        }
    }
}