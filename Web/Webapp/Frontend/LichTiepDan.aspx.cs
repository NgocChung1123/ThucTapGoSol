using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.LichTiepDan;
using Com.Gosol.CMS.Utility;
using HSCB.Helper;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class LichTiepDan : System.Web.UI.Page
    {
        private int stt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string tenCanBo = IdentityHelper.GetTenCanBo();
                //HttpCookie c_utencanbo = new HttpCookie("utencanbo", tenCanBo);
                //Response.Cookies.Add(c_utencanbo);

                //CustomCalendar calendar = new CustomCalendar();
                //calendar.BindCell += calendar_BindCell;
                //calendar.CssClass = "table table-bordered";
                //calendar.CalendarNotes = new List<CalendarNote>();
                //calendar.Title = "Lịch tháng " + DateTime.Now.Month;

                //SystemConfigInfo sysInfo1 = new SystemConfig().GetByKey("LANH_DAO_TIEP_HANG_THANG");
                //SystemConfigInfo sysInfo2 = new SystemConfig().GetByKey("BACKUP_DINH_KY");

                //if (sysInfo1 != null)
                //{
                //}

                //if (sysInfo2 != null)
                //{
                //    int ngayBackupDinhKy = Utils.ConvertToInt32(sysInfo2.ConfigValue, 0);
                //    CalendarNote note = new CalendarNote
                //    {
                //        Day = ngayBackupDinhKy
                //    };
                //    //note.Text = "Ngày sao lưu cơ sở dữ liệu định kỳ";

                //    calendar.CalendarNotes.Add(note);
                //}

                //calendar.CreateCalendar(DateTime.Now.Month, DateTime.Now.Year);
                //ltrCalendar.Text = calendar.GeneratedText;

                try
                {
                    ThemeConfigInfo info = new ThemeConfig().GetTheme();
                    //lblHomePhone.Text = info.HomePhone;
                    //lblHomePhone.CssClass = "bg-primary";
                    //lblPhone.Text = info.Phone;
                }
                catch { }

                //hdfNgayTiep.Value = Format.FormatDate(DateTime.Now);

                LoadCoQuan();
                //SetSession();
                //BindRepeater();
            }
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    createPaging();
        //}

        //private void createPaging()
        //{
        //    int totalRow = 0;
        //    string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
        //    string ngayTiep = Utils.ConvertToString(Session["NgayTiep" + Request.Url.AbsolutePath], string.Empty);
        //    try
        //    {
        //        int coQuanID = Utils.ConvertToInt32(keyword, 0);
        //        DateTime NgayTiep = Utils.ConvertToDateTime(ngayTiep, DateTime.MinValue);
        //        totalRow = new Com.Gosol.CMS.DAL.LichTiepDan.LichTiepDan().CountSearchCoQuanNgayTiep(coQuanID, NgayTiep);
        //    }
        //    catch
        //    {
        //    }
        //    int PageSize = IdentityHelper.GetPageSize();
        //    int pageCount = (totalRow / PageSize);
        //    if (totalRow % PageSize != 0)
        //    {
        //        pageCount++;
        //    }

        //    if (pageCount > 1)
        //    {
        //        int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
        //        PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
        //    }
        //}

        //private void calendar_BindCell(object sender, CalendarEventArgs e)
        //{
        //    CustomCalendar cal = sender as CustomCalendar;
        //}

        public void LoadCoQuan()
        {
            ddlCoQuanTiep.DataSource = new DAL.CoQuan().GetAllCoQuan();
            ddlCoQuanTiep.DataBind();
            ddlCoQuanTiep.Items.Insert(0, "Chọn cơ quan");
        }

        //protected void SetSession()
        //{
        //    int page = Utils.ConvertToInt32(Request.Params["page"], 1);
        //    if (Session["CurrentPage"] == null)
        //    {
        //        Session.Add("CurrentPage", page);
        //    }
        //    else
        //    {
        //        Session["CurrentPage"] = page;
        //    }

        //    int pageCheckTab = Utils.ConvertToInt32(Request.Params["page"], 0);

        //    if (pageCheckTab < 1)
        //    {
        //        Session["Keyword" + Request.Url.AbsolutePath] = null;
        //        Session["NgayTiep" + Request.Url.AbsolutePath] = null;
        //    }

        //    if (Session["Keyword" + Request.Url.AbsolutePath] != null)
        //    {
        //        ddlCoQuanTiep.SelectedValue = Session["Keyword" + Request.Url.AbsolutePath].ToString();
        //    }

        //    if (Session["NgayTiep" + Request.Url.AbsolutePath] != null)
        //    {
        //        ddlThangTiep.InnerText = Session["NgayTiep" + Request.Url.AbsolutePath].ToString();
        //    }
        //}

        //protected void BindRepeater()
        //{
        //    int currentPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
        //    int keyword = Utils.ConvertToInt32(ddlCoQuanTiep.SelectedValue,0);
        //    //int keyword = Utils.ConvertToInt32(hdfCoQuanID.Value, 0);
        //    if (Session["Keyword" + Request.Url.AbsolutePath] == null)
        //    {
        //        Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
        //    }
        //    else
        //    {
        //        Session["Keyword" + Request.Url.AbsolutePath] = keyword;
        //    }
        //    //string ngayTiep = ddlThangTiep.DataValueField;
        //    //string ngayTiep = hdfNgayTiep.Value;
        //    if (Session["NgayTiep" + Request.Url.AbsolutePath] == null)
        //    {
        //        //Session.Add("NgayTiep" + Request.Url.AbsolutePath, ngayTiep);
        //    }
        //    else
        //    {
        //        //Session["NgayTiep" + Request.Url.AbsolutePath] = ngayTiep;
        //    }

        //    try
        //    {
        //        int start = (currentPage - 1) * IdentityHelper.GetPageSize();
        //        int end = currentPage * IdentityHelper.GetPageSize();
        //        int coQuanID = Utils.ConvertToInt32(keyword, 0);
        //        //DateTime NgayTiep = Utils.ConvertToDateTime(ngayTiep, DateTime.MinValue);
        //        //rptLichTiepDan.DataSource = new DAL.LichTiepDan.LichTiepDan().GetLichTiepBySearchCoQuanNgayTiep(coQuanID, NgayTiep, start, end);
        //        rptLichTiepDan.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    if (rptLichTiepDan.Items.Count == 0)
        //    {
        //        if (currentPage > 1)
        //        {
        //            currentPage = 1;
        //            Uri pageUri = new Uri(Request.Url.AbsoluteUri);
        //            Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
        //        }
        //    }
        //}

        //protected void rptLichTiepDan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Label lblNoiDungTiep = (Label)e.Item.FindControl("lblNoiDungTiep");
        //    Label lblNgayTiep = (Label)e.Item.FindControl("lblNgayTiep");
        //    HiddenField hdfID = (HiddenField)e.Item.FindControl("hdfIDLichTiepDan");
        //    LichTiepDanInfo info = e.Item.DataItem as LichTiepDanInfo;
        //    string current_ngaytiep = hdfNgayTiep.Value;
        //    //string search_ngaytiep = ddlThangTiep.InnerText;
        //    Label lblSTT = (Label)e.Item.FindControl("lblSTT");

        //    int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
        //    lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
        //    stt++;

        //    System.Web.UI.HtmlControls.HtmlControl ngayTiep = e.Item.FindControl("ngayTiep") as System.Web.UI.HtmlControls.HtmlControl;

        //    //if (Format.FormatDate(info.NgayTiep) == current_ngaytiep || Format.FormatDate(info.NgayTiep) == search_ngaytiep)
        //    //{
        //    //    ngayTiep.Attributes["class"] = "myCssClass";
        //    //}
        //    if (info.NgayTiep != DateTime.MinValue)
        //    {
        //        lblNgayTiep.Text = Format.FormatDate(info.NgayTiep);
        //    }
        //    else
        //    {
        //        lblNgayTiep.Text = "";
        //    }
        //    //lblNoiDungTiep.Text = "Cán bộ <b>" + info.CanBoTiep + "</b> tiếp dân về vấn đề <b>" + info.NDTiep + "</b>";
        //    hdfID.Value = Utils.ConvertToString(info.IDLichTiep, string.Empty);
        //    //}
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    UpdatePanel1.Update();
        //    BindRepeater();
        //}

        [WebMethod]
        public static string GetDataLichTiepDan(int coquanid, int thang, int nam)
        {
            List<LichTiepDanInfo> result = new List<LichTiepDanInfo>();
            result = new DAL.LichTiepDan.LichTiepDan().GetLichTiepByCoQuanAndThangNam(coquanid, thang, nam);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(result);
        }
    }
}