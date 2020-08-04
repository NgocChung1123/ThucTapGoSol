using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc
{
    public partial class DMLoaiThuTuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the current app path:
            //var current_Path = Server.HtmlEncode(Request.PhysicalPath);

            //// Get the destination path
            //var copyToPath = "E:\\Project\\Tmp\\txt1.txt";

            //// Copy the file
            //File.Copy(current_Path, copyToPath);
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiThuTuc, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiThuTuc, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
            if (!IsPostBack)
            {
                LoadParent();
                SetSession();
                BindRepeater();
                CreatePaging();
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
            }


            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearchCha.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }
        }

        //Get parent
        protected void BindRepeater()
        {
            int currentPage = String.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            string keyword = txtSearchCha.Text;

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
                rptLoaiThuTucCha.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().GetParentIDBySearch(keyword, start, end);
                rptLoaiThuTucCha.DataBind();
            }
            catch
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptLoaiThuTucCha.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage = 1;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        public void LoadParent()
        {
            ddlLoaiThuTuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().GetAll();
            ddlLoaiThuTuc.DataBind();
            ddlLoaiThuTuc.Items.Insert(0, "Tất cả");
            ddlLoaiThuTuc.SelectedIndex = 0;
        }

        private void CreatePaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearchCha.Text = keyword;
            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().CountSearch(keyword);
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

        protected void rptLoaiThuTucCha_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTrangThaiCha = (Label)e.Item.FindControl("lblTrangThaiCha");
            DMLoaiThuTucInfo info = e.Item.DataItem as DMLoaiThuTucInfo;
            if (info.Public == true)
                lblTrangThaiCha.Text = "Hiển thị";
            else
                lblTrangThaiCha.Text = "Không hiển thị";

            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEditParent");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDeleteParent");
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiThuTuc, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiThuTuc, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += "disable";
            }
        }

        protected void btnSearchCha_Click(object sender, EventArgs e)
        {
            BindRepeater();
            CreatePaging();
        }

        [WebMethod]
        public static string GetByID(string id)
        {
            DMLoaiThuTucInfo loaiThuTucInfo = new DMLoaiThuTucInfo();
            int loaithutucID = Utils.ConvertToInt32(id, 0);
            try
            {
                loaiThuTucInfo = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().GetByID(loaithutucID);
            }
            catch { }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(loaiThuTucInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }

        public DMLoaiThuTucInfo GetDataDMLoaiThuTuc()
        {
            DMLoaiThuTucInfo info = new DMLoaiThuTucInfo();
            info.LoaiThuTucID = Utils.ConvertToInt32(hdfIDEdit.Value, 0);
            info.ParentID = Utils.ConvertToInt32(ddlLoaiThuTuc.SelectedValue, 0);
            info.TenLoaiThuTuc = Utils.ConvertToString(txtLoaiThuTuc.Text, string.Empty);
            info.GhiChu = Utils.ConvertToString(txtGhiChu.Text, string.Empty);
            info.Public = Utils.ConvertToBoolean(checkPublic.Checked, false);
            info.FileUrl = Utils.ConvertToString(file_name.Text, string.Empty);
            info.FileName = Utils.ConvertToString(txtNameFile.Text, string.Empty);
            info.Creater = IdentityHelper.GetCanBoID();
            info.CreateDate = DateTime.Now;
            info.Editer = IdentityHelper.GetCanBoID();
            info.EditDate = DateTime.Now;
            return info;
        }

        public void SaveFile(HttpPostedFile file)
        {
            // Specify the path to save the uploaded file to.
            string save_path2 = Server.HtmlEncode(Request.PhysicalApplicationPath);
            string save_path = HttpContext.Current.Server.MapPath("~/");
            string full_path = save_path + "UploadFiles\\FileWF\\";

            // Get the name of the file to upload.
            string fileName = file.FileName;

            // Create the path and file name to check for duplicates.
            string pathToCheck = full_path + fileName;

            // Create a temporary file name to use for checking duplicates.
            string tempfileName = "";

            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = full_path + tempfileName;
                    counter++;
                }

                fileName = tempfileName;

                // Notify the user that the file name was changed.
                file_name.Text = fileName;
            }
            else
            {

            }

            // Append the name of the file to upload to the path.
            full_path += fileName;

            // Call the SaveAs method to save the uploaded
            // file to the specified directory.
            file.SaveAs(full_path);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (file_upload.HasFile)
                SaveFile(file_upload.PostedFile);
            else
            {
                Console.WriteLine("Có lỗi sảy ra");
            }

            DMLoaiThuTucInfo info = GetDataDMLoaiThuTuc();
            int status = 0;
            if (info.LoaiThuTucID != 0)
            {
                try
                {
                    if (info.ParentID <= 0)
                    {
                        info.ParentID = -1;
                    }
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().Update(info);
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

                    status = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().Insert(info);
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
            CreatePaging();
            LoadParent();
            //LoadChildrent(info.LoaiThuTucID.ToString());
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            int total_odl = 0, total_new = 0;
            total_odl = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().Count();

            if (id != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().Delete(id);
                    total_new = new Com.Gosol.CMS.DAL.DanhMuc.QLTrinhTuThuTuc.DMLoaiThuTuc().Count();
                    if (total_odl == total_new)
                    {
                        lblthongBaoDeleteError.Text = Constant.CONTENT_DELETE_INFO;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoDeleteError", "showthongBaoDeleteError()", true);
                    }

                    else
                    {
                        lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                    }
                }
                catch
                {
                    lblthongBaoDeleteError.Text = Constant.CONTENT_DELETE_ERR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoDeleteError", "showthongBaoDeleteError()", true);
                }
            }
            hdDeleteID.Value = "0";
            BindRepeater();
            CreatePaging();
        }

        protected void txtSearchCha_TextChanged(object sender, EventArgs e)
        {
            btnSearchCha_Click(sender, e);
        }
    }
}