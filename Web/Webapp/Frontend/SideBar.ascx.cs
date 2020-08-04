using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class SideBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTinNoiBat();
            }
        }

        private void BindTinNoiBat()
        {
            List<DMTinTucInfo> list = new DMTinTuc().Get_ALL_Tin_Hot().Take(4).OrderByDescending(x => x.IDTinTuc).ToList();
            rptTinNoiBat.DataSource = list;
            rptTinNoiBat.DataBind();
        }

        protected void rptTinNoiBat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Image imgTinNoiBat_Top4 = (Image)e.Item.FindControl("imgTinNoiBat_Top4");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;

            imgTinNoiBat_Top4.ImageUrl = "~/" + info.ImageUrl;

            if (info.TieuDe.Length > 50)
            {
                lblTitle.Text = info.TieuDe.Substring(0, 50) + "...";
            }
            else
            {
                lblTitle.Text = info.TieuDe;
            }

            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/xem-chi-tiet-tin-tuc/" + info.IDTinTuc;
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");
            hplImage.NavigateUrl = "/xem-chi-tiet-tin-tuc/" + info.IDTinTuc;
        }
    }
}