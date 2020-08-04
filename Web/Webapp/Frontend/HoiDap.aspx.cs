using Com.Gosol.CMS.DAL.TraLoiCauHoi;
using Com.Gosol.CMS.Model.CauHoi;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class HoiDap : System.Web.UI.Page
    {
        private int stt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDMLinhVuc();
                SetSession();
                BindRepeater();
                //BindDMLinhVuc();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            int totalRow = 0;
            string linhVuc = Utils.ConvertToString(Session["LinhVucID" + Request.Url.AbsolutePath], string.Empty);
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            try
            {
                int linhVucID = Utils.ConvertToInt32(linhVuc, 0);
                keyword = "%" + keyword + "%";
                totalRow = new CauHoi().CountSearch(linhVucID, keyword);
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
                CreatePaging(totalRow, currentPage, ref plhPaging);
            }
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

            if (Session["LinhVucID" + Request.Url.AbsolutePath] != null)
            {
                int linhVucID = Utils.ConvertToInt32(Session["LinhVucID" + Request.Url.AbsolutePath], 0);
                ddlLinhVuc.SelectedValue = linhVucID.ToString();
            }

            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
                txtNoiDungCauHoi.Text = keyword;
            }

            //int pageCheckTab = Utils.ConvertToInt32(Request.Params["page"], 0);

            //if (pageCheckTab < 1)
            //{
            //    Session["Keyword" + Request.Url.AbsolutePath] = null;
            //    Session["LinhVucID" + Request.Url.AbsolutePath] = null;
            //}
        }

        protected void BindDMLinhVuc()
        {
            ddlLinhVuc.DataSource = new DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetAllDMLinhVuc();
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem() { Value = "0", Text = "Chọn lĩnh vực" });

            ddlLinhVuc_GuiCauHoi.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetAllDMLinhVuc();
            ddlLinhVuc_GuiCauHoi.DataBind();
            ddlLinhVuc_GuiCauHoi.Items.Insert(0, "Chọn lĩnh vực");
            ddlLinhVuc_GuiCauHoi.SelectedIndex = 0;
        }

        protected void BindRepeater()
        {
            int currentPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            int linhVucID = Utils.ConvertToInt32(Session["LinhVucID" + Request.Url.AbsolutePath], 0);
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            //if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            //{
            //    Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            //}
            //else
            //{
            //    Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            //}

            //string ngayTiep = "";
            //if (Session["NgayTiep" + Request.Url.AbsolutePath] == null)
            //{
            //    Session.Add("NgayTiep" + Request.Url.AbsolutePath, ngayTiep);
            //}
            //else
            //{
            //    Session["NgayTiep" + Request.Url.AbsolutePath] = ngayTiep;
            //}

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            try
            {
                keyword = "%" + keyword + "%";
                rptCauHoi.DataSource = new CauHoi().GetBySearch(linhVucID, keyword, start, end);
                rptCauHoi.DataBind();
            }
            catch
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptCauHoi.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage = 1;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        //protected void BindRepeaterDMLinhVuc()
        //{
        //    rptLinhVuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetAllDMLinhVuc();
        //    rptLinhVuc.DataBind();
        //}


        //protected void rptLichTiepDan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Label lblNoiDungTiep = (Label)e.Item.FindControl("lblNoiDungTiep");
        //    Label lblNgayTiep = (Label)e.Item.FindControl("lblNgayTiep");
        //    LichTiepDanInfo info = e.Item.DataItem as LichTiepDanInfo;

        //    System.Web.UI.HtmlControls.HtmlControl ngayTiep = e.Item.FindControl("ngayTiep") as System.Web.UI.HtmlControls.HtmlControl;

        //    if (e.Item.ItemIndex == 0)
        //    {
        //        ngayTiep.Attributes["class"] = "myCssClass";
        //    }
        //    if (info.NgayTiep != DateTime.MinValue)
        //        lblNgayTiep.Text = Format.FormatDate(info.NgayTiep);
        //    else
        //        lblNgayTiep.Text = "";
        //    lblNoiDungTiep.Text = "Nguyên cán bộ " + info.CanBoTiep + " tiếp dân về vấn đề " + info.NDTiep;
        //}

        protected void rptLinhVuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblNoiDungLinhVuc = (Label)e.Item.FindControl("lblNoiDungLinhVuc");
            DMLinhVucInfo info = e.Item.DataItem as DMLinhVucInfo;

            System.Web.UI.HtmlControls.HtmlControl ngayTiep = e.Item.FindControl("ngayTiep") as System.Web.UI.HtmlControls.HtmlControl;

            lblNoiDungLinhVuc.Text = info.TenLinhVuc;
        }

        [WebMethod]
        public static string BindCauHoi(string linhVucID, string noiDungCauHoi)
        {
            int LinhVucID = Utils.ConvertToInt32(linhVucID, 0);
            string NoiDungCauHoi = "%" + noiDungCauHoi + "%";
            List<TraLoiInfo> lsCauHoi = new List<TraLoiInfo>();

            try
            {
                lsCauHoi = new Com.Gosol.CMS.DAL.TraLoiCauHoi.TraLoiCauHoi().GetTraLoiCauHoiBySearchLinhVucAndNoiDungCauHoi(NoiDungCauHoi, LinhVucID);
            }
            catch
            {
            }
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(lsCauHoi);
                return data;
            }
            catch
            {
                return data;
            }
        }

        protected CauHoiInfo GetData()
        {
            CauHoiInfo info = new CauHoiInfo
            {
                //info.IDLinhVuc = Utils.ConvertToInt32(ddlLinhVuc_GuiCauHoi.SelectedValue,0);
                IDLinhVuc = Utils.ConvertToInt32(hdfLinhVucCauHoi.Value, 0),
                NDCauHoi = Utils.ConvertToString(txtGuiCauHoi.Text, string.Empty),
                IsCauHoiHopLe = true,
                GhiChu = "Người dùng hỏi",
                //var userID = HttpContext.Current.Session["USER$DA31A175C7679319BFFEDF3EF282D1F4CANBOID"];
                CreaterId = 0, // nguoi gui
                CreateDate = DateTime.Now,
                HoTen = Utils.ConvertToString(txtHoTen.Text, string.Empty),
                Email = Utils.ConvertToString(txtEmail.Text, string.Empty),
                SDT = Utils.ConvertToString(txtSDT.Text, string.Empty)
            };
            return info;
        }

        protected void ResetForm()
        {
            ddlLinhVuc_GuiCauHoi.SelectedValue = "Chọn lĩnh vực";
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtCaptcha.Text = "";

        }

        protected void btnGuiCauHoi_Click(object sender, EventArgs e)
        {
            bool isHuman = Captcha.Validate(txtCaptcha.Text);
            txtCaptcha.Text = null;
            hdfTab.Value = "1";
            if (isHuman)
            {
                int result = 0;
                CauHoiInfo info = GetData();

                try
                {
                    if (!string.IsNullOrEmpty(info.NDCauHoi))
                    {
                        result = new Com.Gosol.CMS.DAL.TraLoiCauHoi.CauHoi().Insert(info);
                    }
                }
                catch (Exception)
                {

                }

                if (result > 0)
                {
                    RequiredFieldValidator4.Attributes.CssStyle["display"] = "none";
                    lblContentSuccess.Text = Constant.CONTENT_QUESTION_SUCCESS;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                else
                {
                    lblContentSuccess.Text = "Gửi câu hỏi thất bại!";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess();", true);
                }
                ResetForm();
            }
            else
            {
                lblBaoLoi.Text = "Captcha chưa đúng!";
                lblBaoLoi.Attributes.Add("style", "display:block");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "captchaIncorrectly", "$('#divGuiCauHoi').show();", true);
            }

        }

        protected void rptCauHoi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtNoiDungCauHoi.Text.Trim();
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }

            Session.Add("CurrentPage" + Request.Url.AbsolutePath, "1");

            if (Session["LinhVucID" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("LinhVucID" + Request.Url.AbsolutePath, Utils.ConvertToInt32(ddlLinhVuc.SelectedValue, 0));
            }
            else
            {
                Session["LinhVucID" + Request.Url.AbsolutePath] = Utils.ConvertToInt32(ddlLinhVuc.SelectedValue, 0);
            }

            BindRepeater();
        }

        public void CreatePaging(int totalRow, int currentPage, ref PlaceHolder pageControl)
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
                        hplPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString();
                        hplPage.Text = (i + 1).ToString();
                        pageControl.Controls.Add(hplPage);
                        //hplPage.ID = "hplPage" + i.ToString();
                        //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                        //trigger.ControlID = hplPage.ID;
                        //trigger.EventName = "Click";
                        //UpdatePanel1.Triggers.Add(trigger);
                    }
                }
            }
            else if (pageCount >= 10)
            {
                if (currentPage - 5 > 0 && currentPage + 4 >= pageCount)
                {
                    HyperLink firstPage = new HyperLink();
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=1";
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
                            HyperLink hplPage = new HyperLink
                            {
                                NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString(),
                                Text = (i + 1).ToString()
                            };
                            pageControl.Controls.Add(hplPage);
                        }
                    }
                }
                else if (currentPage - 5 > 0 && currentPage + 4 < pageCount)
                {
                    HyperLink firstPage = new HyperLink();
                    Uri pageUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    firstPage.NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=1";
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
                            HyperLink hplPage = new HyperLink
                            {
                                NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString(),
                                Text = (i + 1).ToString()
                            };
                            pageControl.Controls.Add(hplPage);
                        }
                    }

                    HyperLink lastPage = new HyperLink
                    {
                        NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + pageCount.ToString(),
                        Text = "Trang cuối",
                        CssClass = "lastPage"
                    };
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
                            HyperLink hplPage = new HyperLink
                            {
                                NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + (i + 1).ToString(),
                                Text = (i + 1).ToString()
                            };
                            pageControl.Controls.Add(hplPage);
                        }
                    }

                    HyperLink lastPage = new HyperLink
                    {
                        NavigateUrl = pageUri.GetLeftPart(UriPartial.Path) + "?page=" + pageCount.ToString(),
                        Text = "Trang cuối",
                        CssClass = "lastPage"
                    };
                    pageControl.Controls.Add(lastPage);
                }
            }
        }
    }
}