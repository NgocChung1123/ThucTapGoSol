using Com.Gosol.CMS.DAL.DonThu;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.DonThu;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Com.Gosol.CMS.Web.Webapp.Backend.DonThu
{
    public partial class QlTrangThaiHoSo : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //#region -- check permission
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyQuyetDinhGiaiQuyet, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyQuyetDinhGiaiQuyet, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyQuyetDinhGiaiQuyet, AccessLevel.Edit))
            {
                btnEdit.OnClientClick = "return false;";
                btnEdit.Visible = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyQuyetDinhGiaiQuyet, AccessLevel.Delete))
            {
                btnDelete1.OnClientClick = "return false;";
                btnDelete1.Visible = false;
                btnDelete1.ToolTip = Constant.ToolTip;
                btnDelete1.CssClass += " disable";
            }
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
                InitAddForm();
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
                int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedItem.Value, 0);
                totalRow = new DAL.DonThu.DonThu().CountSearchVanBanBackEnd(keyword, coQuanID);
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
            IList<CoQuanInfo> cqList = new List<CoQuanInfo>();
            List<CoQuanInfo> parentList = new List<CoQuanInfo>();

            int capID = IdentityHelper.GetCapID();
            int coQuanUserID = IdentityHelper.GetCoQuanID();

            //parentList = (List<CoQuanInfo>)new DAL.CoQuan().GetAllCoQuan();

            //foreach (CoQuanInfo parentInfo in parentList)
            //{
            //    TreeSort(ref cqList, parentInfo, 0);
            //}
            cqList = new DAL.CoQuan().GetCoQuanTreeView(coQuanUserID);
   
            
            ddlCoQuanSearch.DataSource = cqList;
            ddlCoQuanSearch.DataBind();
            ddlCoQuanSearch.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanSearch.SelectedIndex = 0;
            ddlCoQuan.DataSource = cqList;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuan.SelectedIndex = 0;
            ddlCoQuanXuLy.DataSource = cqList;
            ddlCoQuanXuLy.DataBind();
            ddlCoQuanXuLy.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanXuLy.SelectedIndex = 0;
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
                int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedItem.Value, 0);
                rptVanBan.DataSource = new DAL.DonThu.DonThu().GetBySearchVanBanBackEnd(keyword, start, end, coQuanID);
            }
            catch (Exception)
            {

            }
            rptVanBan.DataBind();

            if (rptVanBan.Items.Count == 0 && currentPage != 1)
            {
                currentPage = 1;
                Session.Add("CurrentPage", currentPage);
                Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
            }
        }

        protected void rptVanBan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            if (!AccessControl.User.HasPermission(ChucNangEnum.CauHoi, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.CauHoi, AccessLevel.Delete))
            {
                btnDelete.Enabled = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += "disable";
            }
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

        protected void ddlCoQuanSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }

        private void InitAddForm()
        {
            ddlHuongXuLy.DataSource = new HuongXuLy().GetAll();
            ddlHuongXuLy.DataBind();
            ddlHuongXuLy.Items.Insert(0, new ListItem("Chọn hướng xử lý", "0"));
            ddlHuongXuLy.SelectedIndex = 0;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string fileQuyetDinh = string.Empty;
            if (fileUpload.HasFile)
            {
                SetFile(fileUpload, ref fileQuyetDinh);
            }

            DAL.DonThu.DonThu dal = new DAL.DonThu.DonThu();
            DonThuInfo info = new DonThuInfo()
            {
                ID = Utils.ConvertToInt32(hdfDonThuID.Value, 0),
                SoDonThu = Utils.ConvertToString(txtSoDon.Text, string.Empty),
                NgayTiepNhan = Utils.ConvertToString(txtNgayTiepNhan.Text, string.Empty),
                CoQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0),
                CoQuanTiepNhan = Utils.ConvertToString(ddlCoQuan.SelectedItem.Text, string.Empty),
                NoiDungDon = Utils.ConvertToString(txtNoiDungDon.Text, string.Empty),
                NgayXuLyStr = Utils.ConvertToString(txtNgayXuLy.Text, string.Empty),
                CoQuanXuLyID = Utils.ConvertToInt32(ddlCoQuanXuLy.SelectedValue, 0),
                CoQuanXuLy = Utils.ConvertToString(ddlCoQuanXuLy.SelectedItem.Text, string.Empty),
                HuongXuLyID = Utils.ConvertToInt32(ddlHuongXuLy.SelectedValue, 0),
                HuongXuLy = Utils.ConvertToString(ddlHuongXuLy.SelectedItem.Text, string.Empty),
                NguoiDaiDien = Utils.ConvertToString(txtHoTen.Text, string.Empty),
                FileQuyetDinh = fileQuyetDinh,
                TrangThaiDonID = 1
            };
            try
            {
                if (info.ID == 0)
                {
                    int val = dal.Insert(info);
                    if (val != 0)
                    {
                        if (fileQuyetDinh != "")
                        {
                            fileUpload.SaveAs(Server.MapPath("~/UploadFiles/") + fileQuyetDinh);
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowSuccessNotify", "ShowSuccessNotify();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowErrorNotify", "ShowErrorNotify();", true);
                    }
                }
                else
                {
                    int val = dal.Update(info);
                    if (val != 0)
                    {
                        if (fileQuyetDinh != "")
                        {
                            fileUpload.SaveAs(Server.MapPath("~/UploadFiles/") + fileQuyetDinh);
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowSuccessNotify", "ShowSuccessNotify();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowErrorNotify", "ShowErrorNotify();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowErrorNotify", "ShowErrorNotify();", true);
            }
            hdfDonThuID.Value = "0";
            BindRepeater();
        }

        public void SetFile(FileUpload file, ref string fileName)
        {
            fileName = file.FileName;
            file.PostedFile.SaveAs(Server.MapPath("~/UploadFiles/") + fileName);
        }

        [WebMethod]
        public static string GetByID(string donThuId)
        {
            int id = Utils.ConvertToInt32(donThuId, 0);
            string data = "";
            if (id != 0)
            {
                DonThuInfo info = new DAL.DonThu.DonThu().GetByID(id);
                try
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    data = serializer.Serialize(info);
                }
                catch
                {

                }
            }
            return data;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int donThuID = Utils.ConvertToInt32(hdfDonThuID.Value, 0);
            //if (AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Delete))
            //{
            if (donThuID != 0)
            {
                try
                {
                    int val = new DAL.DonThu.DonThu().Delete(donThuID);
                    if (val != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
                }
            }
            //}
            hdfDonThuID.Value = "0";
            BindRepeater();
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            //cần net framework 4.5
            //GetData().Wait();
        }

        protected void btnDongBo_Click(object sender, EventArgs e)
        {

        }
    }
}