using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TinTucDetailDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int IDTinTuc = Utils.ConvertToInt32(Request.Params["tintuc"], 1);
            if (!IsPostBack)
            {
                BindData(IDTinTuc);
            }
        }

        public void BindData(int IDTinTuc)
        {
            DMTinTucInfo TinTuc = new DMTinTuc().GetTinTucByID(IDTinTuc);
            lblLoaiTin.Text = TinTuc.TenLoaiTin;
            lblTitle.Text = TinTuc.TieuDe;
            lblNgayDang.Text = "Ngày: " + TinTuc.CreateDate.Date.ToString("dd/MM/yyyy");
            lblTomTat.Text = TinTuc.TomTat;
            imgTinTuc.ImageUrl = TinTuc.ImageUrl;
            lblNoiDung.Text = TinTuc.NoiDung;
            List<DMTinTucInfo> lstTinLienQuan = new List<DMTinTucInfo>();
            lstTinLienQuan = new DMTinTuc().GetByLoaiTinID(TinTuc.IDLoaiTin).Where(x => x.IDTinTuc != TinTuc.IDTinTuc).Take(5).ToList();
            rptTinKhac.DataSource = lstTinLienQuan;
            rptTinKhac.DataBind();
            hplXemThem.NavigateUrl = "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + TinTuc.IDLoaiTin;
        }

        protected void rptTinKhac_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label Title = (Label)e.Item.FindControl("lblTitle");
            Label ngay = (Label)e.Item.FindControl("lblNgayDang");
            HyperLink link = (HyperLink)e.Item.FindControl("hylTin");
            DMTinTucInfo item = (DMTinTucInfo)e.Item.DataItem;
            Title.Text = item.TieuDe;
            ngay.Text = "(" + item.CreateDate.Date.ToString("dd/MM/yyyy") + ")";
            link.NavigateUrl = "/Webapp/Frontend/TinTucDetailDemo.aspx?tintuc=" + item.IDTinTuc;
        }

    }
}