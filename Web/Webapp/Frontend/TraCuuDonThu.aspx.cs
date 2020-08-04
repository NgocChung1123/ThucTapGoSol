using Com.Gosol.CMS.DAL.DonThu;
using Com.Gosol.CMS.Model.DonThu;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TraCuuDonThu : System.Web.UI.Page
    {

        private int stt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                string keyword = Utils.ConvertToString(Request.Params["keyword"], string.Empty);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", page);
                }
                else
                {
                    Session["CurrentPage"] = page;
                }
                //BindCoQuanDDL();
                SetSession(keyword);
                BindRepeater();

            }
        }
        [WebMethod]
        public static DonThuInfo GetDonThuBySoDonThu(string soDonThu, int coQuanID)
        {
            DonThuInfo dt = new DonThuInfo();
            try
            {
                dt = new DonThu().GetBySoDonThu(soDonThu, coQuanID);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dt;
        }
        #region preLoad
        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            int totalRow = 0;

            int coQuanID = Utils.ConvertToInt32(Session["CoQuanID" + Request.Url.AbsolutePath], 0);
            int coQuanID2 = Utils.ConvertToInt32(Session["CoQuanID2" + Request.Url.AbsolutePath], 0);
            string SoDonThu = Utils.ConvertToString(Session["SoDonThu" + Request.Url.AbsolutePath], string.Empty);
            string TenQuyetDinh = Utils.ConvertToString(Session["TenQuyetDinh" + Request.Url.AbsolutePath], string.Empty);

            DateTime FromDate = Utils.ConvertToDateTime(Session["From" + Request.Url.AbsolutePath], DateTime.MinValue);
            DateTime ToDate = Utils.ConvertToDateTime(Session["To" + Request.Url.AbsolutePath], DateTime.MinValue);


            try
            {

                totalRow = new DAL.DonThu.DonThu().CountSearchDonThuByType(coQuanID, coQuanID2, SoDonThu, TenQuyetDinh, FromDate, ToDate, 3);
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

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            int coQuanID = Utils.ConvertToInt32(Session["CoQuanID" + Request.Url.AbsolutePath], 0);
            int coQuanID2 = Utils.ConvertToInt32(Session["CoQuanID2" + Request.Url.AbsolutePath], 0);
            string SoDonThu = Utils.ConvertToString(Session["SoDonThu" + Request.Url.AbsolutePath], string.Empty);
            string TenQuyetDinh = Utils.ConvertToString(Session["TenQuyetDinh" + Request.Url.AbsolutePath], string.Empty);

            DateTime From = Utils.ConvertToDateTime(Session["From" + Request.Url.AbsolutePath], DateTime.MinValue);
            DateTime To = Utils.ConvertToDateTime(Session["To" + Request.Url.AbsolutePath], DateTime.MinValue);

            txtTgBanHanhFrom.Text = Format.FormatDate(From);
            txtTgBanHanhTo.Text = Format.FormatDate(To);

            hdfCoQuanID.Value = coQuanID.ToString();
            hdfCoQuanID2.Value = coQuanID2.ToString();
            txtTenQuyetDinh.Text = TenQuyetDinh;
            txtSoDonThu.Text = SoDonThu;

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            List<DonThuInfo> list = new List<DonThuInfo>();
            try
            {



                list = new DAL.DonThu.DonThu().SearchDonThuByType(coQuanID, coQuanID2, SoDonThu, TenQuyetDinh, From, To, start, end, 3);
                rptDonThu.DataSource = list;
                rptDonThu.DataBind();
            }
            catch (Exception ex)
            {

            }

            if (list.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage = 1;
                    if (Session["CurrentPage"] == null)
                        Session.Add("CurrentPage", currentPage);
                    else Session["CurrentPage"] = currentPage;
                    //Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    //Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }

        }

        protected void rptDonThu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //Label lblNgayTiepNhan = (Label)e.Item.FindControl("lblNgayTiepNhan");
            //Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            //DonThuInfo info = e.Item.DataItem as DonThuInfo;
            //lblNgayTiepNhan.Text = info.NgayTiepNhan;
            //int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            //lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            //stt++;

            Label lblNgayTiepNhan = (Label)e.Item.FindControl("lblNgayTiepNhan");
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            DonThuInfo info = e.Item.DataItem as DonThuInfo;

            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            lblNgayTiepNhan.Text = info.NgayTiepNhan;
            stt++;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetSession("");
            BindRepeater();
        }

        protected void SetSession(string keyword)
        {
            int page = Utils.ConvertToInt32(Request.Params["page"], 1);
            if (Session["CurrentPage"] == null)
                Session.Add("CurrentPage", page);
            else Session["CurrentPage"] = page;

            int pageCheckTab = Utils.ConvertToInt32(Request.Params["page"], 0);

            if (pageCheckTab < 1)
            {
                Session["SoDonThu" + Request.Url.AbsolutePath] = null;
            }

            if (Session["CoQuanID" + Request.Url.AbsolutePath] != null)
            {
                Session.Add("CoQuanID" + Request.Url.AbsolutePath, Utils.ConvertToInt32(HttpContext.Current.Request.Form["ctl00$MainContent$ddlCoQuan_C1"], 0));
            }
            else
            {
                Session["CoQuanID" + Request.Url.AbsolutePath] = Utils.ConvertToInt32(HttpContext.Current.Request.Form["ctl00$MainContent$ddlCoQuan_C1"], 0);
            }

            if (Session["CoQuanID2" + Request.Url.AbsolutePath] != null)
            {
                Session.Add("CoQuanID2" + Request.Url.AbsolutePath, Utils.ConvertToInt32(HttpContext.Current.Request.Form["ctl00$MainContent$ddlCoQuan_C2"], 0));
            }
            else
            {
                Session["CoQuanID2" + Request.Url.AbsolutePath] = Utils.ConvertToInt32(HttpContext.Current.Request.Form["ctl00$MainContent$ddlCoQuan_C2"], 0);
            }


            if (Session["SoDonThu" + Request.Url.AbsolutePath] != null)
            {
                if (String.IsNullOrEmpty(keyword))
                    Session["SoDonThu" + Request.Url.AbsolutePath] = txtSoDonThu.Text;
                else
                    Session["SoDonThu" + Request.Url.AbsolutePath] = keyword;
            }
            else
            {
                if (String.IsNullOrEmpty(keyword))
                    Session.Add("SoDonThu" + Request.Url.AbsolutePath, txtSoDonThu.Text);
                else
                    Session.Add("SoDonThu" + Request.Url.AbsolutePath, keyword);
            }

            if (Session["TenQuyetDinh" + Request.Url.AbsolutePath] != null)
            {
                Session["TenQuyetDinh" + Request.Url.AbsolutePath] = txtTenQuyetDinh.Text;
            }
            else
            {
                Session.Add("TenQuyetDinh" + Request.Url.AbsolutePath, txtTenQuyetDinh.Text);
            }


            if (Session["From" + Request.Url.AbsolutePath] != null)
            {
                Session["From" + Request.Url.AbsolutePath] = txtTgBanHanhFrom.Text;
            }
            else
            {
                Session.Add("From" + Request.Url.AbsolutePath, txtTgBanHanhFrom.Text);
            }


            if (Session["To" + Request.Url.AbsolutePath] != null)
            {
                Session["To" + Request.Url.AbsolutePath] = txtTgBanHanhTo.Text;
            }
            else
            {
                Session.Add("To" + Request.Url.AbsolutePath, txtTgBanHanhTo.Text);
            }


        }

       

        [WebMethod]
        public static string GetFile(string id,int XuLyDonID,int TrangThaiDonID)
        {
            int DonThuID = Utils.ConvertToInt32(id, 0);
            List<DonThuInfo> listFileInfo = new List<DonThuInfo>();

            try
            {
                if(TrangThaiDonID== (int)EnumLoaiVanBan.QuyetDinhGiaiQuyet)
                {
                    listFileInfo = new DAL.DonThu.DonThu().GetListFileKetQuaByID(DonThuID);

                }
                else if(TrangThaiDonID == (int)EnumLoaiVanBan.VanBanTraLoi)
                {
                    listFileInfo = new DAL.DonThu.DonThu().GetListFileYKienXuLyByID(DonThuID);

                }
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
    }
}