using Com.Gosol.CMS.Model.CauHoi;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.HoiDap
{
    public partial class TraLoiCauHoi : System.Web.UI.Page
    {
        private int stt = 1;
        private int sttTraLoi = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.HoiDap, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.HoiDap, AccessLevel.Create))
            {
                btnAdd.OnClientClick = "return false;";
                btnAdd.Visible = false;
                btnAdd.ToolTip = Constant.ToolTip;
                btnAdd.CssClass += " disable";
            }


            if (!IsPostBack)
            {
                LoadLinhVuc();
                SetSession();
                BindRepeater();
                createPaging();

                //khởi tạo page hdfChangDiv=""Session["CauHoiOrTraLoi"]
                //if (hdfChangDiv.Value == "2")
                if (Utils.ConvertToString(Session["CauHoiOrTraLoi"],string.Empty) == "2")
                {
                    pnCauHoi.Visible = false;
                    pnTraLoi.Visible = true;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(2);", true);
                }
                else
                {
                    pnCauHoi.Visible = true;
                    pnTraLoi.Visible = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(1);", true);
                }
            }
            //if (hdfChangDiv.Value == "2")
            //{
            //    pnCauHoi.Visible = false;
            //    pnTraLoi.Visible = true;
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(2);", true);
            //}
            //else
            //{
            //    pnCauHoi.Visible = true;
            //    pnTraLoi.Visible = false;
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(1);", true);
            //}
        }

        public void LoadLinhVuc()
        {
            ddlLinhVucSearch.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetAllDMLinhVuc();
            ddlLinhVucSearch.DataBind();
            ddlLinhVucSearch.Items.Insert(0, "Tất cả");
            ddlLinhVucSearch.SelectedIndex = 0;
            ddlLinhVuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetAllDMLinhVuc();
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, "Tất cả");
            ddlLinhVuc.SelectedIndex = 0;
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

            int pageTraLoi = Utils.ConvertToInt32(Request.Params["pageTraLoi"], 1);
            if (Session["CurrentPageTraLoi"] == null)
            {
                Session.Add("CurrentPageTraLoi", pageTraLoi);
            }
            else
            {
                Session["CurrentPageTraLoi"] = pageTraLoi;
            }

            int pageCheckTabTraLoi = Utils.ConvertToInt32(Request.Params["pageTraLoi"], 0);

            if (pageCheckTab < 1)
            {
                Session["Keyword" + Request.Url.AbsolutePath] = null;
            }

            if (pageCheckTabTraLoi < 1)
            {
                Session["Keyword" + Request.Url.AbsolutePath] = null;
            }

            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }

            if (Session["LinhVuc"] == null)
            {
                Session.Add("LinhVuc", 0);
            }
            else
            {
                ddlLinhVucSearch.SelectedValue = Utils.ConvertToString(Session["LinhVuc"], string.Empty);
            }

            if (Session["CauHoiOrTraLoi"] == null)
            {
                Session.Add("CauHoiOrTraLoi", 1);
            }
            //else
            //{
            //    Session["CauHoiOrTraLoi"] = Session["CauHoiOrTraLoi"];
            //}
        }

        protected void BindRepeater()
        {
            int currentPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            int currentPageTraLoi = string.IsNullOrEmpty(Request.QueryString["pageTraLoi"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["pageTraLoi"], 1);
            string keyword = txtSearch.Text;
            int linhVucID = Utils.ConvertToInt32(Session["LinhVuc"], 0);
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
            //int linhVucID = Utils.ConvertToInt32(ddlLinhVucSearch.SelectedValue, 0);
            keyword = "%" + keyword + "%";

            try
            {
                string parmKeyword = keyword;
                rptCauHoi.DataSource = new DAL.TraLoiCauHoi.CauHoi().GetBySearchNotAnswer(keyword, linhVucID, start, end);
                rptCauHoi.DataBind();
            }
            catch (Exception)
            {

            }

            start = (currentPageTraLoi - 1) * IdentityHelper.GetPageSize();
            end = currentPageTraLoi * IdentityHelper.GetPageSize();
            try
            {
                string parmKeyword = keyword;
                rptTraLoi.DataSource = new DAL.TraLoiCauHoi.CauHoi().GetCauHoiAnswered(keyword, linhVucID, start, end);
                rptTraLoi.DataBind();
            }
            catch (Exception )
            {

            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptCauHoi.Items.Count == 0 && Utils.ConvertToString(Session["CauHoiOrTraLoi"], "1") == "1")
            {
                if (currentPage > 1)
                {
                    currentPage = 1;

                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);

                    //if (Utils.ConvertToString(Session["CauHoiOrTraLoi"], "1") == "1")
                    //{
                        Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString() + "&pageTraLoi=" + currentPageTraLoi.ToString());
                    //}
                }
            }

            if (rptTraLoi.Items.Count == 0 && Utils.ConvertToString(Session["CauHoiOrTraLoi"], "1") == "2")
            {
                if (currentPageTraLoi > 1)
                {
                    currentPageTraLoi = 1;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);

                    //if (Utils.ConvertToString(Session["CauHoiOrTraLoi"], "1") == "2")
                    //{
                        Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString() + "&pageTraLoi=" + currentPageTraLoi.ToString());
                    //}
                }
            }
        }

        private void createPaging()
        {
            int totalRow = 0;
            int totalRowTraLoi = 0;
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            txtSearch.Text = keyword;
            int linhVucID = Utils.ConvertToInt32(Session["LinhVuc"], 0);//Utils.ConvertToInt32(ddlLinhVucSearch.SelectedValue, 0);
            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new DAL.TraLoiCauHoi.CauHoi().CountSearchNotAnswer(keyword, linhVucID);
                totalRowTraLoi = new DAL.TraLoiCauHoi.CauHoi().CountSearchAnswered(keyword, linhVucID);
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
                CreatePaging(totalRow, currentPage, ref plhPaging, 1);
            }

            int pageCountTraLoi = (totalRowTraLoi / PageSize);
            if (totalRowTraLoi % PageSize != 0)
            {
                pageCountTraLoi++;
            }

            if (pageCountTraLoi > 1)
            {
                int currentPageTraLoi = Utils.ConvertToInt32(Session["CurrentPageTraLoi"], 1);
                CreatePaging(totalRowTraLoi, currentPageTraLoi, ref plhPaging2, 2);
            }
        }

        protected void rptCauHoi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;
            Button btnTraLoi = (Button)e.Item.FindControl("btnTraLoi");
            Label lblNgayHoi = (Label)e.Item.FindControl("lblNgayHoi");

            CauHoiInfo info = e.Item.DataItem as CauHoiInfo;

            //if (info.IDTraLoi != 0)
            //{
            //    btnEdit.Visible = true;
            //    btnDelete.Visible = true;
            //    btnTraLoiCauHoi.Visible = false;
            //}
            //else
            //{
            //    btnEdit.Visible = false;
            //    btnDelete.Visible = false;
            //    btnTraLoiCauHoi.Visible = true;
            //}

            if (info.CreateDate != DateTime.MinValue)
            {
                lblNgayHoi.Text = Format.FormatDate(info.CreateDate);
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.TraLoiCauHoi, AccessLevel.Create))
            {
                btnTraLoi.OnClientClick = "return false;";
                btnTraLoi.Visible = false;
                btnTraLoi.ToolTip = Constant.ToolTip;
                btnTraLoi.CssClass += " disable";
            }
        }

        protected void rptTraLoi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
          
            Label lblSTT = (Label)e.Item.FindControl("lblSTT1");
            int currentPage = Utils.ConvertToInt32(Session["CurrentPageTraLoi"], 1);
            lblSTT.Text = (sttTraLoi + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            sttTraLoi++;


            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            //LinkButton btnTraLoiCauHoi = (LinkButton)e.Item.FindControl("btnTraLoiCauHoi");
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            //Label lblNgayHoi = (Label)e.Item.FindControl("lblNgayHoi");
            //Label lblNgayTraLoi = (Label)e.Item.FindControl("lblNgayTraLoi");
   
            TraLoiInfo info = e.Item.DataItem as TraLoiInfo;

            //if (info.IDTraLoi != 0)
            //{
            //    btnEdit.Visible = true;
            //    btnDelete.Visible = true;
            //    btnTraLoiCauHoi.Visible = false;
            //}
            //else
            //{
            //    btnEdit.Visible = false;
            //    btnDelete.Visible = false;
            //    btnTraLoiCauHoi.Visible = true;
            //}
            //if (info.CreateDate != DateTime.MinValue)
            //    lblNgayHoi.Text = Format.FormatDate(info.CreateDate);
            //if (info.NgayTraLoi != DateTime.MinValue)
            //    lblNgayTraLoi.Text = Format.FormatDate(info.NgayTraLoi);

            CheckBox cbHienThi = (CheckBox)e.Item.FindControl("cbHienThi2");
            if (info.Public == true)
            {
                cbHienThi.Checked = true;
            }
            else
            {
                cbHienThi.Checked = false;
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.TraLoiCauHoi, AccessLevel.Edit))
            {
                btnEdit.OnClientClick = "return false;";
                btnEdit.Visible = false;
                btnEdit.ToolTip = Constant.ToolTip;
                btnEdit.CssClass += " disable";
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.TraLoiCauHoi, AccessLevel.Delete))
            {
                btnDelete.OnClientClick = "return false;";
                btnDelete.Visible = false;
                btnDelete.ToolTip = Constant.ToolTip;
                btnDelete.CssClass += " disable";
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int IDTraLoi = Utils.ConvertToInt32(hdDeleteID.Value, 0);
            if (IDTraLoi != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.TraLoiCauHoi.TraLoiCauHoi().Delete(IDTraLoi);
                    lblContentSuccess.Text = Constant.CONTENT_DELETE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                catch
                {
                    lblthongBaoDeleteError.Text = Constant.CONTENT_DELETE_ERR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoDeleteError", "showthongBaoDeleteError();", true);
                }
            }

            hdDeleteID.Value = "0";
            BindRepeater();
            createPaging();
        }

        public TraLoiInfo GetData()
        {
            TraLoiInfo info = new TraLoiInfo
            {
                IDTraLoi = Utils.ConvertToInt32(hdfIDTraLoiEdit.Value, 0),
                IDCauHoi = Utils.ConvertToInt32(hdfCauHoiID.Value, 0),
                NDTraLoi = Utils.ConvertToString(CKEditorNoiDungTraLoi.Text, string.Empty),
                Public = Utils.ConvertToBoolean(checkPublic.Checked, false),
                CreaterId = IdentityHelper.GetCanBoID(),
                CreateDate = DateTime.Now,
                Editer = IdentityHelper.GetCanBoID(),
                EditDate = DateTime.Now
            };

            return info;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            TraLoiInfo info = GetData();
            //int cauhoiID=Utils.ConvertToInt32(hdfCauHoiID.Value, 0);
            //CauHoiInfo info = new DAL.TraLoiCauHoi.CauHoi().GetCauHoiByID_BackEnd(cauhoiID);
            int status = 0;
            if (info.IDTraLoi != 0)
            {
                try
                {
                    new Com.Gosol.CMS.DAL.TraLoiCauHoi.TraLoiCauHoi().Update(info);
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                catch
                {
                    lblThongBaoError.Text = Constant.MESSAGE_INSERT_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
                }
            }
            else
            {
                try
                {
                    status = new Com.Gosol.CMS.DAL.TraLoiCauHoi.TraLoiCauHoi().Insert(info);
                }
                catch
                {
                    throw;
                }
                if (status > 0)
                {
                    lblContentSuccess.Text = Constant.MESSAGE_INSERT_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                else
                {
                    lblThongBaoError.Text = Constant.MESSAGE_INSERT_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError();", true);
                }
            }
            BindRepeater();
            createPaging();
            hdfIDTraLoiEdit.Value = "0";
            hdfCauHoiID.Value = "0";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindRepeater();
            createPaging();
            if (Utils.ConvertToString(Session["CauHoiOrTraLoi"], string.Empty) == "2")
            {
                pnCauHoi.Visible = false;
                pnTraLoi.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(2);", true);
            }
            else
            {
                pnCauHoi.Visible = true;
                pnTraLoi.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(1);", true);
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetByID(string idTraLoiCauHoi)
        {
            TraLoiInfo tinTucInfo = new TraLoiInfo();
            int IDTraLoi = Utils.ConvertToInt32(idTraLoiCauHoi, 0);
            try
            {
                tinTucInfo = new Com.Gosol.CMS.DAL.TraLoiCauHoi.TraLoiCauHoi().GetTraLoiCauHoiByID(IDTraLoi);
            }
            catch (Exception)
            {
            }
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

        [System.Web.Services.WebMethod]
        public static string GetCauHoiByID(string idCauHoi)
        {
            CauHoiInfo tinTucInfo = new CauHoiInfo();
            int IDCauHoi = Utils.ConvertToInt32(idCauHoi, 0);
            try
            {
                tinTucInfo = new Com.Gosol.CMS.DAL.TraLoiCauHoi.CauHoi().GetCauHoiByID_BackEnd(IDCauHoi);
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

        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    btnSearch_Click(sender, e);
        //}

        protected void ddlLinhVucSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.ConvertToString(Session["CauHoiOrTraLoi"], string.Empty) == "2")
            {
                pnCauHoi.Visible = false;
                pnTraLoi.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(2);", true);
            }
            else
            {
                pnCauHoi.Visible = true;
                pnTraLoi.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(1);", true);
            }
            Session["LinhVuc"] = ddlLinhVucSearch.SelectedValue;
            BindRepeater();
            createPaging();
        }

        protected void btnCauHoi_Click(object sender, EventArgs e)
        {
            pnCauHoi.Visible = true;
            pnTraLoi.Visible = false;
            Session["CauHoiOrTraLoi"] = "1";
            hdfChangDiv.Value = "1";
            BindRepeater();
            createPaging();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(1);", true);
        }

        protected void btnDaTraLoi_Click(object sender, EventArgs e)
        {
            pnCauHoi.Visible = false;
            pnTraLoi.Visible = true;
            hdfChangDiv.Value = "2";
            Session["CauHoiOrTraLoi"] = "2";
            BindRepeater();
            createPaging();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "changeDiv", "changeDiv(2);", true);
        }

        public void CreatePaging(int totalRow, int currentPage, ref PlaceHolder pageControl, int zz)
        {
            int PageSize = IdentityHelper.GetPageSize();

            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0)
            {
                pageCount++;
            }

            if (pageCount > 1 && pageCount < 10)
            {
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)
                    {
                        Label lblPage = new Label
                        {
                            Text = (i + 1).ToString(),
                            CssClass = "current"
                        };
                        pageControl.Controls.Add(lblPage);
                    }
                    else
                    {
                        HyperLink hplPage = new HyperLink();
                        Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                        if (zz == 1)
                        {
                            string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                            hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString() + "&pageTraLoi=" + currentPageTraLoi;
                        }
                        else
                        {
                            string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                            hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=" + (i + 1).ToString() + "&page=" + currentPage1;
                        }
                        hplPage.Text = (i + 1).ToString();
                        pageControl.Controls.Add(hplPage);
                    }
                }
            }
            else if (pageCount >= 10)
            {
                if (currentPage - 5 > 0 && currentPage + 4 >= pageCount)
                {
                    HyperLink firstPage = new HyperLink();
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    if (zz == 1)
                    {
                        string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                        firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=1" + "&pageTraLoi=" + currentPageTraLoi;
                    }
                    else
                    {
                        string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                        firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=1" + "&page=" + currentPage1;
                    }
                    firstPage.Text = "Trang đầu";
                    firstPage.CssClass = "firstPage";
                    pageControl.Controls.Add(firstPage);

                    for (int i = currentPage - 5; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)
                        {
                            Label lblPage = new Label
                            {
                                Text = (i + 1).ToString(),
                                CssClass = "current"
                            };
                            pageControl.Controls.Add(lblPage);
                        }
                        else
                        {
                            HyperLink hplPage = new HyperLink();
                            if (zz == 1)
                            {
                                string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                                hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString() + "&pageTraLoi=" + currentPageTraLoi;
                            }
                            else
                            {
                                string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                                hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=" + (i + 1).ToString() + "&page=" + currentPage1;
                            }
                            hplPage.Text = (i + 1).ToString();
                            pageControl.Controls.Add(hplPage);
                        }
                    }
                }
                else if (currentPage - 5 > 0 && currentPage + 4 < pageCount)
                {
                    HyperLink firstPage = new HyperLink();
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    if (zz == 1)
                    {
                        string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                        firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=1" + "&pageTraLoi=" + currentPageTraLoi;
                    }
                    else
                    {
                        string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                        firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=1" + "&page=" + currentPage1;
                    }
                    firstPage.Text = "Trang đầu";
                    firstPage.CssClass = "firstPage";
                    pageControl.Controls.Add(firstPage);

                    for (int i = currentPage - 5; i < currentPage + 4; i++)
                    {
                        if (i == currentPage - 1)
                        {
                            Label lblPage = new Label
                            {
                                Text = (i + 1).ToString(),
                                CssClass = "current"
                            };
                            pageControl.Controls.Add(lblPage);
                        }
                        else
                        {
                            HyperLink hplPage = new HyperLink();
                            if (zz == 1)
                            {
                                string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                                hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString() + "&pageTraLoi=" + currentPageTraLoi;
                            }
                            else
                            {
                                string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                                hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=" + (i + 1).ToString() + "&page=" + currentPage1;
                            }
                            hplPage.Text = (i + 1).ToString();
                            pageControl.Controls.Add(hplPage);
                        }
                    }

                    HyperLink lastPage = new HyperLink();
                    if (zz == 1)
                    {
                        string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                        lastPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + pageCount.ToString() + "&pageTraLoi=" + currentPageTraLoi;
                    }
                    else
                    {
                        string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                        lastPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=" + pageCount.ToString() + "&page=" + currentPage1;
                    }
                    lastPage.Text = "Trang cuối";
                    lastPage.CssClass = "lastPage";
                    pageControl.Controls.Add(lastPage);
                }
                else if (currentPage - 5 <= 0 && currentPage + 4 < pageCount)
                {
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);

                    for (int i = 0; i < currentPage + 4; i++)
                    {
                        if (i == currentPage - 1)
                        {
                            Label lblPage = new Label
                            {
                                Text = (i + 1).ToString(),
                                CssClass = "current"
                            };
                            pageControl.Controls.Add(lblPage);
                        }
                        else
                        {
                            HyperLink hplPage = new HyperLink();
                            if (zz == 1)
                            {
                                string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                                hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString() + "&pageTraLoi=" + currentPageTraLoi;
                            }
                            else
                            {
                                string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                                hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=" + (i + 1).ToString() + "&page=" + currentPage1;
                            }
                            hplPage.Text = (i + 1).ToString();
                            pageControl.Controls.Add(hplPage);
                        }
                    }

                    HyperLink lastPage = new HyperLink();
                    if (zz == 1)
                    {
                        string currentPageTraLoi = Utils.ConvertToString(Session["CurrentPageTraLoi"], "1");
                        lastPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + pageCount.ToString() + "&pageTraLoi=" + currentPageTraLoi;
                    }
                    else
                    {
                        string currentPage1 = Utils.ConvertToString(Session["CurrentPage"], "1");
                        lastPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?pageTraLoi=" + pageCount.ToString() + "&page=" + currentPage1;
                    }
                    lastPage.Text = "Trang cuối";
                    lastPage.CssClass = "lastPage";
                    pageControl.Controls.Add(lastPage);
                }
            }
        }
    }
}