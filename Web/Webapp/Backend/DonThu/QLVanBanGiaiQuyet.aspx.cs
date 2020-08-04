using Com.Gosol.CMS.DAL.DonThu;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.DonThu;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.DonThu
{
    public partial class QLVanBanGiaiQuyet : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //#region -- check permission
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyQuyetDinhGiaiQuyet, AccessLevel.Read))
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
                totalRow = new DAL.DonThu.DonThu().CountSearchQuyetDinhBackEnd(keyword, coQuanID);
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

            parentList = (List<CoQuanInfo>)new DAL.CoQuan().GetAllCoQuan();

            foreach (CoQuanInfo parentInfo in parentList)
            {
                TreeSort(ref cqList, parentInfo, 0);
            }

            ddlCoQuanSearch.DataSource = cqList;
            ddlCoQuanSearch.DataBind();
            ddlCoQuanSearch.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanSearch.SelectedIndex = 0;
            ddlCoQuan.DataSource = cqList;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("Chọn cơ quan", ""));
            ddlCoQuan.SelectedIndex = 0;
            ddlCoQuanBanHanh.DataSource = cqList;
            ddlCoQuanBanHanh.DataBind();
            ddlCoQuanBanHanh.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanBanHanh.SelectedIndex = 0;
            ddlCoQuanXuLy.DataSource = cqList;
            ddlCoQuanXuLy.DataBind();
            ddlCoQuanXuLy.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanXuLy.SelectedIndex = 0;
            ddlCoQuanGiaiQuyet.DataSource = cqList;
            ddlCoQuanGiaiQuyet.DataBind();
            ddlCoQuanGiaiQuyet.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanGiaiQuyet.SelectedIndex = 0;
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
                rptQuyetDinh.DataSource = new DAL.DonThu.DonThu().GetBySearchQuyetDinhBackEnd(keyword, start, end, coQuanID);
            }
            catch (Exception)
            {

            }
            rptQuyetDinh.DataBind();

            if (rptQuyetDinh.Items.Count == 0 && currentPage != 1)
            {
                currentPage = 1;
                Session.Add("CurrentPage", currentPage);
                Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
            }
        }
        protected void rptQuyetDinh_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int DonThuID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (e.CommandName == "ShowFile")
            {
                if (DonThuID != 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowFileVBGQForm", "ShowFileVBGQForm(" + DonThuID + ")", true);
                }
            }
        }

        protected void rptQuyetDinh_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            //ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            DonThuInfo info = (DonThuInfo)e.Item.DataItem;

            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;

            CheckBox cbCongKhai = (CheckBox)e.Item.FindControl("cbCongKhai");
            if (info.CongKhai == "1")
            {
                cbCongKhai.Checked = true;
            }
            else
            {
                cbCongKhai.Checked = false;
            }

            //if (!AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Edit))cbCongKhai
            //{
            //    btnEdit.Enabled = false;
            //    btnEdit.ToolTip = Constant.ToolTip;
            //    btnEdit.CssClass += " disable";
            //}

            //if (!AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Delete))
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

        protected void ddlCoQuanSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }

        private void InitAddForm()
        {
            //ddlHuongXuLy.DataSource = new HuongXuLy().GetAll();
            //ddlHuongXuLy.DataBind();
            //ddlHuongXuLy.Items.Insert(0, new ListItem("Chọn hướng xử lý", "0"));
            //ddlHuongXuLy.SelectedIndex = 0;
        }

        [WebMethod]
        public static string GetFile(string id)
        {
            int DonThuID = Utils.ConvertToInt32(id, 0);
            List<DonThuInfo> listFileInfo = new List<DonThuInfo>();

            try
            {
                listFileInfo = new DAL.DonThu.DonThu().GetListFileKetQuaByID(DonThuID);
            }
            catch
            {
            }

            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(listFileInfo);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string fileQuyetDinh = string.Empty;
            if (fileUpload.HasFile)
            {
                fileQuyetDinh = fileUpload.FileName;
                //fileUpload.PostedFile.SaveAs(Server.MapPath("~/UploadFiles/") + fileQuyetDinh);
            }

            DAL.DonThu.DonThu dal = new DAL.DonThu.DonThu();
            //DonThuInfo info;
            //try
            //{
            DonThuInfo info = new DonThuInfo()
            {
                ID = Utils.ConvertToInt32(hdfDonThuID.Value, 0),
                SoDonThu = Utils.ConvertToString(txtSoDon.Text, string.Empty),
                NgayTiepNhan = Utils.ConvertToString(txtNgayTiepNhan.Text, string.Empty),
                CoQuanID = Utils.ConvertToInt32(ddlCoQuan.SelectedValue, 0),
                CoQuanTiepNhan = Utils.ConvertToString(ddlCoQuan.SelectedItem.Text, string.Empty),
                NoiDungDon = Utils.ConvertToString(txtNoiDungDon.Text, string.Empty),
                NgayBanHanh = Utils.ConvertToString(txtNgayBanHanh.Text, string.Empty),
                CoQuanBanHanhID = Utils.ConvertToInt32(ddlCoQuanBanHanh.SelectedValue, 0),
                CoQuanBanHanh = Utils.ConvertToString(ddlCoQuanBanHanh.SelectedItem.Text, string.Empty),
                NguoiDaiDien = Utils.ConvertToString(txtHoTen.Text, string.Empty),
                FileQuyetDinh = fileQuyetDinh,
                TrangThaiDonID = 2,
                CoQuanXuLyID = Utils.ConvertToInt32(ddlCoQuanXuLy.SelectedValue, 0),
                CoQuanXuLy = Utils.ConvertToString(ddlCoQuanXuLy.SelectedItem.Text, string.Empty),
                CoQuanGiaiQuyetID = Utils.ConvertToInt32(ddlCoQuanGiaiQuyet.SelectedValue, 0),
                CoQuanGiaiQuyet = Utils.ConvertToString(ddlCoQuanGiaiQuyet.SelectedItem.Text, string.Empty),
                SoTienPhaiThu = Utils.ConvertToInt32(txtTienPhaiThu.Text, 0),
                SoDatPhaiThu = Utils.ConvertToInt32(txtDatPhaiThu.Text, 0),
                SoDoiTuongBiXuLy = Utils.ConvertToInt32(txtDoiTuongBiXuLy.Text, 0),
                DiaChi = Utils.ConvertToString(txtDiaChi.Text, string.Empty),
                TenQuyetDinh = Utils.ConvertToString(txtTenQuyetDinh.Text, string.Empty),
            };
            if (cbCongKhai1.Checked) info.CongKhai = "1";
            else info.CongKhai = "0";
            //}
            //catch(Exception ex)
            //{

            //}

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
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "HieAddHscbForm", "HideAddHscbForm();", true);
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
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "HieAddHscbForm", "HideAddHscbForm();", true);
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
        [WebMethod]
        public static string SaveDB(string dataDonThu)
        {
            string[] dtInfo = dataDonThu.Split(new string[] { "//@//" }, StringSplitOptions.None);
            string result = "";
            for (int i = 0; i < dtInfo.Length - 1; i++)
            {
                try
                {
                    //'64654'
                    string[] dtCells = dtInfo[i].Split(new string[] { "/;/" }, StringSplitOptions.None);
                    if (dtCells.Length > 13)
                    {
                        DonThuInfo donThu = new DonThuInfo();
                        donThu.SoDonThu = Utils.GetString(dtCells[0], String.Empty);
                        donThu.NgayTiepNhan = Utils.GetString(dtCells[5], String.Empty);
                        donThu.CoQuanTiepNhan = Utils.GetString(dtCells[6], String.Empty);
                        donThu.CanBoTiepNhan = Utils.GetString(dtCells[7], String.Empty);
                        donThu.PhanLoaiDon = Utils.GetString(dtCells[8], String.Empty);
                        donThu.NoiDungDon = Utils.GetString(dtCells[9], String.Empty);
                        donThu.DoiTuongKhieuNai = Utils.GetString(dtCells[2], String.Empty);
                        donThu.NguoiDaiDien = Utils.GetString(dtCells[10], String.Empty);
                        donThu.CoQuanXuLy = Utils.GetString(dtCells[11], String.Empty);
                        donThu.PhongBanXuLy = Utils.GetString(dtCells[3], String.Empty);
                        //donThu.CanBoXuLy = Utils.GetString(dtCells[11], String.Empty);
                        donThu.TrangThaiDonThu = Utils.GetString(dtCells[12], String.Empty);
                        donThu.CongKhai = "0";
                        donThu.XuLyDonID = Utils.ConvertToInt32(dtCells[1], 0);
                        donThu.CoQuanID = Utils.ConvertToInt32(dtCells[4], 0);
                        donThu.CongKhai = Utils.ConvertToString(dtCells[13], "0");
                        //string fileBase64 = Utils.GetString(dtCells[14], String.Empty);
                        string fileName = Utils.GetString(dtCells[14], String.Empty);
                        string fileBase64 = Utils.GetString(dtCells[16], String.Empty);
                        string[] fileParts = fileBase64.Split('*');

                        if (!String.IsNullOrEmpty(fileName))
                            donThu.FileQuyetDinh = string.Empty;

                        donThu.TrangThaiDonID = 2;
                        int val = 0;

                        if (donThu.XuLyDonID != 0)
                        {
                            val = new DAL.DonThu.DonThu().Insert(donThu);
                            if (val > 0)
                            {
                                for (int j = 0; j < fileParts.Length; j++)
                                {
                                    if (!String.IsNullOrEmpty(fileParts[j]))
                                    {
                                        string fileStr = fileParts[j];
                                        string[] dataParts = fileStr.Split(',');
                                        FileHoSoInfo infoFileHoSo = new FileHoSoInfo();
                                        infoFileHoSo.DonThuID = val;
                                        infoFileHoSo.FileURL = dataParts[2];
                                        infoFileHoSo.TenFile = dataParts[1];
                                        string path = HttpContext.Current.Server.MapPath("~") + dataParts[2];
                                        bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~") + "/UploadFiles/FileBanHanhQD/");
                                        if (!exists)
                                            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + "/UploadFiles/FileBanHanhQD/");
                                        if (!String.IsNullOrEmpty(dataParts[1]) && !String.IsNullOrEmpty(dataParts[0]))
                                        {
                                            Byte[] bytes = Convert.FromBase64String(dataParts[0]);
                                            File.WriteAllBytes(path, bytes);
                                        }
                                        new DAL.DonThu.DonThu().InsertFileKetQua(infoFileHoSo);
                                    }

                                }
                            }
                            
                            if (val != 0)
                            {
                                if (i == 0)
                                {
                                    result += donThu.XuLyDonID;
                                }
                                else
                                {
                                    result += ";" + donThu.XuLyDonID;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        [WebMethod]
        public static string SaveFileBase64(string fileName, string fileBase64)
        {

            string result = "";



            if (!String.IsNullOrEmpty(fileBase64))
            {
                var aa = fileName;
                //HttpContext.Current.Server.MapPath("~/UploadFiles/Templace/") + "ImportKHTTMau.xlsx";
                //fileUpload.SaveAs(Server.MapPath("~/UploadFiles/") + fileQuyetDinh);
                string path = HttpContext.Current.Server.MapPath("~/UploadFiles/") + fileName;
                Byte[] bytes = Convert.FromBase64String(fileBase64);
                File.WriteAllBytes(path, bytes);
            }

            return result;
        }
        [WebMethod]
        public static bool ChangedIsShow(bool IsChecked, int ID)
        {
            var kq = new DAL.DonThu.DonThu().UpdateIsShow(IsChecked, ID);
            return true;
        }
    }
}