using Com.Gosol.CMS.DAL.DonThu;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.DonThu;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Com.Gosol.CMS.Web.Webapp.Backend.DonThu
{
    public partial class QLVanBanTraLoi : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyVanBanTraLoi, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyVanBanTraLoi, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyVanBanTraLoi, AccessLevel.Edit))
            {
                btnEdit.OnClientClick = "return false;";
                btnEdit.Visible = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.QuanLyVanBanTraLoi, AccessLevel.Delete))
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
                BindCoQuanDDL();
                InitAddForm();
                SetSession();

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
            List<CoQuanInfo> cqList = new List<CoQuanInfo>();
            List<CoQuanInfo> parentList = new List<CoQuanInfo>();

            int capID = IdentityHelper.GetCapID();
            int coQuanUserID = IdentityHelper.GetCoQuanID();

            cqList = (List<CoQuanInfo>)new DAL.CoQuan().GetAllCoQuan();

            //foreach (CoQuanInfo parentInfo in parentList)
            //{
            //    TreeSort(ref cqList, parentInfo, 0);
            //}

            ddlCoQuanSearch.DataSource = cqList;
            ddlCoQuanSearch.DataBind();
            ddlCoQuanSearch.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanSearch.SelectedIndex = 0;
            ddlCoQuan.DataSource = cqList;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("Chọn cơ quan", ""));
            ddlCoQuan.SelectedIndex = 0;
            ddlCoQuanXuLy.DataSource = cqList;
            ddlCoQuanXuLy.DataBind();
            ddlCoQuanXuLy.Items.Insert(0, new ListItem("Chọn cơ quan", "0"));
            ddlCoQuanXuLy.SelectedIndex = 0;
            ddlCoQuanGiaiQuyet.DataSource = cqList;
            ddlCoQuanGiaiQuyet.DataBind();
            ddlCoQuanGiaiQuyet.Items.Insert(0, new ListItem("Chọn cơ quan", ""));
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

            int coQuanUserID = IdentityHelper.GetCoQuanID();
            int capID = IdentityHelper.GetCapID();

            try
            {
                keyword = "%" + keyword + "%";
                int coQuanID = Utils.ConvertToInt32(ddlCoQuanSearch.SelectedItem.Value, 0);
                rptVanBan.DataSource = new DAL.DonThu.DonThu().GetBySearchVanBanBackEnd(keyword, start, end, coQuanID);
                rptVanBan.DataBind();
            }
            catch (Exception)
            {

            }


        }

        protected void rptVanBan_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            //if (!AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Edit))
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
            BindRepeater();
            //createPaging();

        }
        protected void rptVanBan_ItemCommand(object sender, RepeaterCommandEventArgs e)
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

        [WebMethod]
        public static string GetFile(string id)
        {
            int DonThuID = Utils.ConvertToInt32(id, 0);
            List<DonThuInfo> listFileInfo = new List<DonThuInfo>();

            try
            {
                listFileInfo = new DAL.DonThu.DonThu().GetListFileYKienXuLyByID(DonThuID);
            }
            catch(Exception ex)
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
                TrangThaiDonID = 1,
                NgayBanHanh = Utils.ConvertToString(txtNgayBanHanh.Text, string.Empty),
                DiaChi = Utils.ConvertToString(txtDiaChi.Text, string.Empty),
                CoQuanGiaiQuyetID = Utils.ConvertToInt32(ddlCoQuanGiaiQuyet.SelectedValue, 0),
                CoQuanGiaiQuyet = Utils.ConvertToString(ddlCoQuanGiaiQuyet.SelectedItem.Text, string.Empty),
                TenQuyetDinh = Utils.ConvertToString(txtTenQuyetDinh.Text, string.Empty),
            };
            if (cbCongKhai1.Checked) info.CongKhai = "1";
            else info.CongKhai = "0";
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
            fileName = "VanBanTraLoi/" + file.FileName;
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

        //[WebMethod]
        //public static string Download(string filePath)
        //{
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = "application/octect-stream";
        //    HttpContext.Current.Response.AppendHeader("content-disposition", "filename=TenFile");
        //    //HttpContext.Current.Response.Charset = "";
        //    //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    //HttpContext.Current.Response.Write(list[0]);
        //    HttpContext.Current.Response.TransmitFile(Server.MapPath(filePath));
        //    HttpContext.Current.Response.End();
        //    return "";
        //}

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            //cần net framework 4.5
            //GetData().Wait();
        }

        protected void btnDongBo_Click(object sender, EventArgs e)
        {

        }

        //static async Task GetData()
        //{
        //    using (var client=new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://192.168.100.42:10008/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = await client.GetAsync("") 
        //        if (client.IsSuccessStatusCode)
        //        {
        //            Departmentdepartment = awaitresponse.Content.ReadAsAsync<Department>();
        //            Console.WriteLine("Id:{0}\tName:{1}", department.DepartmentId, department.DepartmentName);
        //            Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Internal server Error");
        //        }
        //    }
        //}

        [WebMethod]
        public static bool ChangedIsShow(bool IsChecked, int ID)
        {
            var kq = new DAL.DonThu.DonThu().UpdateIsShow(IsChecked, ID);
            return true;
        }
        [WebMethod]
        public static string SaveDB(string dataDonThu)
        {
            string result = "";
            try
            {
                string[] dtInfo = dataDonThu.Split(new string[] { "//@//" }, StringSplitOptions.None);
                

                for (int i = 0; i < dtInfo.Length - 1; i++)
                {
                    try
                    {

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
                            donThu.TrangThaiDonID = 1;
                            string fileName = Utils.GetString(dtCells[14], String.Empty);
                            string fileBase64 = Utils.GetString(dtCells[16], String.Empty);
                            string[] fileParts = fileBase64.Split('*');

                            if (!String.IsNullOrEmpty(fileName))
                                donThu.FileQuyetDinh = string.Empty;


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
                                            infoFileHoSo.XuLyDonID = donThu.XuLyDonID;
                                            infoFileHoSo.FileURL = dataParts[2];
                                            infoFileHoSo.TenFile = dataParts[1];
                                            string path = HttpContext.Current.Server.MapPath("~") + dataParts[2];
                                            bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~") + "/UploadFiles/FileKetQuaXuLy/");
                                            if (!exists)
                                                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + "/UploadFiles/FileKetQuaXuLy/");
                                            if (!String.IsNullOrEmpty(dataParts[1]) && !String.IsNullOrEmpty(dataParts[0]))
                                            {
                                                Byte[] bytes = Convert.FromBase64String(dataParts[0]);
                                                File.WriteAllBytes(path, bytes);
                                            }
                                            new DAL.DonThu.DonThu().InsertFileYKienXuLy(infoFileHoSo);
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
                        result = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        [WebMethod]
        public static string SaveFileBase64(string fileName, string fileBase64)
        {

            string result = "";
            try
            {
                if (!String.IsNullOrEmpty(fileBase64))
                {
                    string path = HttpContext.Current.Server.MapPath("~/UploadFiles/VanBanTraLoi/") + fileName;
                    Byte[] bytes = Convert.FromBase64String(fileBase64);
                    File.WriteAllBytes(path, bytes);
                }
            }
            catch
            {

            }
            return result;
        }
    }
}