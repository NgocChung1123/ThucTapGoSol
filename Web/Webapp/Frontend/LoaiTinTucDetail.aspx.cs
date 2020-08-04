using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class LoaiTinTucDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int loaiTinTucID = Utils.ConvertToInt32(Page.RouteData.Values["mangtinid"], 0);
            int loaiTinTucID = Utils.ConvertToInt32(Request.Params["mangtinid"], 0);
            if (!IsPostBack)
            {
                if (loaiTinTucID > 0)
                {
                    int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                    if (Session["CurrentPage"] == null)
                        Session.Add("CurrentPage", page);
                    else Session["CurrentPage"] = page;

                    BindMangLoaiTin(loaiTinTucID);
                    CountTinByLoaiTin(loaiTinTucID);
                    DMLoaiTinInfo loaiTinInfo = new DMLoaiTin().GetLoaiTinByID(loaiTinTucID);
                    lblTenLoaiTin.Text = loaiTinInfo.TenLoaiTin;
                }
            }
        }

        protected void BindMangLoaiTin(int loaiTinID)
        {
            int currentPage = String.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            rptLoaiTinTuc.DataSource = new DMTinTuc().ChiTietLoaiTin_GetByLoaiTinID(loaiTinID,start,end);
            rptLoaiTinTuc.DataBind();
        }

        protected void CountTinByLoaiTin(int loaiTinID)
        {
            int totalRow = 0;
            try
            {
                totalRow = new DMTinTuc().GetByLoaiTin_Count(loaiTinID);
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
        protected void rptLoaiTinTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblTomTat = (Label)e.Item.FindControl("lblTomTat");
            Label lblNgayTao = (Label)e.Item.FindControl("lblNgayTao");
            Image imgTinTuc = (Image)e.Item.FindControl("imgTinTuc");
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            HyperLink hplXemChiTiet = (HyperLink)e.Item.FindControl("hplXemChiTiet");
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");

            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;
            string ngayTao = "Ngày " + Format.FormatDate(info.CreateDate) + " | ";
            lblNgayTao.Text = ngayTao;
            imgTinTuc.ImageUrl = "~/" + info.ImageUrl;

            if (info.TieuDe.Length > 100)
            {
                lblTitle.Text = info.TieuDe.Substring(0, 100) + "...";
            }
            else
            {
                lblTitle.Text = info.TieuDe;
            }


            if (info.TomTat.Length > 300)
            {
                lblTomTat.Text = info.TomTat.Substring(0, 300) + "...";
            }
            else
            {
                lblTomTat.Text = info.TomTat;
            }

            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            hplImage.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            hplXemChiTiet.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }
    }
}