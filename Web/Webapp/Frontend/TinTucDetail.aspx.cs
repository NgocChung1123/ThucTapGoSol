using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TinTucDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int tinTucID = Utils.ConvertToInt32(Page.RouteData.Values["url_title"], 0);
            int tinTucID = Utils.ConvertToInt32(Request.Params["tintuc"], 0);

            if (!IsPostBack)
            {
                if (tinTucID > 0)
                {
                    BindTinTucDetail(tinTucID);
                }
                else
                {
                    lblLoaiTin.Text = "Dữ liệu chưa được cập nhật!";
                }
            }
        }

        protected void BindTinTucDetail(int tinTucID)
        {
            DMTinTucInfo tinInfo = new DMTinTuc().GetTinTucByID(tinTucID);
            lblTitle.Text = tinInfo.TieuDe;
            lblNoiDung.Text = tinInfo.NoiDung;
            lblLoaiTin.Text = tinInfo.TenLoaiTin;
            hplLoaiTin.NavigateUrl = "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + tinInfo.IDLoaiTin;
            hplXemThem.NavigateUrl = "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + tinInfo.IDLoaiTin;
            lblNgayDang.Text = "Ngày " + Format.FormatDate(tinInfo.CreateDate);
            //lblNguoiDang.Text = tinInfo.NguoiTao;
            lblTomTat.Text = tinInfo.TomTat;

            imgTinTuc.ImageUrl = "/" + tinInfo.ImageUrl;

            //Tin khác: các tin mới cùng loại
            List<DMTinTucInfo> lsTinMoi = new DMTinTuc().GeTinMoi_ByLoaiTin(tinInfo.IDLoaiTin).Where(x => x.IDTinTuc != tinInfo.IDTinTuc).Take(5).ToList();
            rptTinKhac.DataSource = lsTinMoi;
            rptTinKhac.DataBind();
        }

        protected void rptTinKhac_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblNgayDang = (Label)e.Item.FindControl("lblNgayDang");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;
            lblNgayDang.Text = " (" + Format.FormatDate(info.CreateDate) + ")";
            if (info.TieuDe.Length > 120)
            {
                lblTitle.Text = info.TieuDe.Substring(0, 120) + "...";
            }
            else
            {
                lblTitle.Text = info.TieuDe;
            }
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }
    }
}