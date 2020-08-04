using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TinTuc : System.Web.UI.Page
    {
        private List<DMTinTucInfo> tinHotTop1;
        private List<DMTinTucInfo> tinHotTop2;
        private List<DMTinTucInfo> tinHotTop3;
        private List<DMTinTucInfo> lsTinMoi;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTop3TinNoiBat();
                BindLoaiTinTuc();
            }
        }

        #region tin hot noi bat
        private void BindTop3TinNoiBat()
        {

            List<DMTinTucInfo> lsData = new DMTinTuc().GetTop3_TinNoiBat_TinHot();

            tinHotTop1 = lsData.Take(1).OrderByDescending(x => x.IDTinTuc).ToList();
            rptTinHot_Top1.DataSource = tinHotTop1;
            rptTinHot_Top1.DataBind();

            List<DMTinTucInfo> lsDataTop2 = lsData.Take(2).ToList();
            tinHotTop2 = lsDataTop2.Skip(1).Take(1).OrderByDescending(x => x.IDTinTuc).ToList();
            rptTinHot_Top2.DataSource = tinHotTop2;
            rptTinHot_Top2.DataBind();

            tinHotTop3 = lsData.Skip(2).OrderBy(x => x.IDTinTuc).ToList();
            rptTinHot_Top3.DataSource = tinHotTop3;
            rptTinHot_Top3.DataBind();

            lsTinMoi = new DMTinTuc().Get_ALL_Tin_Hot().Where(x => x.IDTinTuc != tinHotTop2[0].IDTinTuc && x.IDTinTuc != tinHotTop3[0].IDTinTuc && x.IDTinTuc != tinHotTop1[0].IDTinTuc).Take(8).OrderByDescending(x => x.IDTinTuc).ToList();
            rptTinMoi.DataSource = lsTinMoi;
            rptTinMoi.DataBind();
        }

        protected void rptTinHot_Top1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblTomTat = (Label)e.Item.FindControl("lblTomTat");
            Image imgTinHot_Top1 = (Image)e.Item.FindControl("imgTinHot_Top1");
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;

            imgTinHot_Top1.ImageUrl = "/" + info.ImageUrl;

                lblTitle.Text = info.TieuDe;

            if (info.TomTat.Length > 130)
            {
                lblTomTat.Text = info.TomTat.Substring(0, 130) + "...";
            }
            else
            {
                lblTomTat.Text = info.TomTat;
            }
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            hplImage.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;

        }
        protected void rptTinHot_Top2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblTomTat = (Label)e.Item.FindControl("lblTomTat");
            Image imgTinHot_Top2 = (Image)e.Item.FindControl("imgTinHot_Top2");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;

            imgTinHot_Top2.ImageUrl = "/" + info.ImageUrl;

                lblTitle.Text = info.TieuDe;


            if (info.TomTat.Length > 100)
            {
                lblTomTat.Text = info.TomTat.Substring(0, 100) + "...";
            }
            else
            {
                lblTomTat.Text = info.TomTat;
            }
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");
            hplImage.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }
        protected void rptTinHot_Top3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblTomTat = (Label)e.Item.FindControl("lblTomTat");
            Image imgTinHot_Top3 = (Image)e.Item.FindControl("imgTinHot_Top3");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;

            imgTinHot_Top3.ImageUrl = "/" + info.ImageUrl;

                lblTitle.Text = info.TieuDe;
   

            if (info.TomTat.Length > 100)
            {
                lblTomTat.Text = info.TomTat.Substring(0, 100) + "...";
            }
            else
            {
                lblTomTat.Text = info.TomTat;
            }
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");
            hplImage.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;

        }
        protected void rptTinMoi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Image imgTinNoiBat_Top4 = (Image)e.Item.FindControl("imgTinNoiBat_Top4");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;

            imgTinNoiBat_Top4.ImageUrl = "/" + info.ImageUrl;

            lblTitle.Text = info.TieuDe;

            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");
            hplImage.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }

        #endregion
        protected void BindLoaiTinTuc()
        {
            var list = new DMLoaiTin().GetAllLoaiTin_Public();
            rptLoaiTinTuc.DataSource = list;
            rptLoaiTinTuc.DataBind();
        }

        protected void rptLoaiTinTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTenLoaiTin = (Label)e.Item.FindControl("lblTenLoaiTin");
            HyperLink hplLoaiTin = (HyperLink)e.Item.FindControl("hplLoaiTin");
            HyperLink hplXemThem = (HyperLink)e.Item.FindControl("hplXemThem");
            Repeater rptTinTucChildren = (Repeater)e.Item.FindControl("rptTinTucChildren");
            Repeater rptTinTucLienQuan = (Repeater)e.Item.FindControl("rptTinTucLienQuan");
            HtmlGenericControl divNoiDungTin = (HtmlGenericControl)e.Item.FindControl("divNoiDungTin");

            DMLoaiTinInfo info = (DMLoaiTinInfo)e.Item.DataItem;
            lblTenLoaiTin.Text = info.TenLoaiTin;
            hplLoaiTin.NavigateUrl = "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + info.IDLoaiTin;
            hplXemThem.NavigateUrl = "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + info.IDLoaiTin;

            BindTinChildren(ref rptTinTucChildren, ref rptTinTucLienQuan, info.IDLoaiTin, ref divNoiDungTin);
        }

        #region tin kinh te
        protected void BindTinChildren(ref Repeater rptTinTucChildren, ref Repeater rptTinTucLienQuan, int loaiTinID, ref HtmlGenericControl divNoiDungTin)
        {
            List<DMTinTucInfo> lsTinChildren = new DMTinTuc().TinPublic_GetByLoaiTinID(loaiTinID).Where(x => x.IDTinTuc != tinHotTop2[0].IDTinTuc && x.IDTinTuc != tinHotTop3[0].IDTinTuc && x.IDTinTuc != tinHotTop1[0].IDTinTuc).ToList();
            List<DMTinTucInfo> lsTinResult = new List<DMTinTucInfo>();
            List<DMTinTucInfo> lsTinChildrenTop1Result = new List<DMTinTucInfo>();
            // kiem tra tin tuc da hien thi o tintop4
            foreach (DMTinTucInfo info1 in lsTinChildren)
            {
                bool ischek = true;
                foreach (DMTinTucInfo info2 in lsTinMoi)
                {
                    if (info1.IDTinTuc == info2.IDTinTuc)
                    {
                        ischek = false;
                        break;
                    }
                }
                if (ischek == true)
                {
                    lsTinResult.Add(info1);
                }
            }

            lsTinChildrenTop1Result = lsTinResult.Take(1).OrderByDescending(x => x.IDTinTuc).ToList();
            rptTinTucChildren.DataSource = lsTinChildrenTop1Result;
            rptTinTucChildren.DataBind();

            rptTinTucLienQuan.DataSource = lsTinResult.Where(x => x.IDTinTuc != lsTinChildrenTop1Result[0].IDTinTuc).Take(5).OrderByDescending(x => x.IDTinTuc);
            rptTinTucLienQuan.DataBind();

            if (lsTinChildrenTop1Result.Count == 0)
            {
                divNoiDungTin.Visible = false;
            }
        }

        protected void rptTinTucChildren_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblTomTat = (Label)e.Item.FindControl("lblTomTat");
            Label lblNgayTao = (Label)e.Item.FindControl("lblNgayTao");
            Image imgTinKinhTeNoiBat = (Image)e.Item.FindControl("imgTinKinhTeNoiBat");
            HyperLink hplXemChiTiet = (HyperLink)e.Item.FindControl("hplXemChiTiet");
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;

            string ngayTao = "Ngày " + Format.FormatDate(info.CreateDate) + " | ";
            lblNgayTao.Text = ngayTao;

            imgTinKinhTeNoiBat.ImageUrl = "/" + info.ImageUrl;

            if (info.TieuDe.Length > 90)
            {
                lblTitle.Text = info.TieuDe.Substring(0, 90) + "...";
            }
            else
            {
                lblTitle.Text = info.TieuDe;
            }

            if (info.TomTat.Length > 200)
            {
                lblTomTat.Text = info.TomTat.Substring(0, 200) + "...";
            }
            else
            {
                lblTomTat.Text = info.TomTat;
            }
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            HyperLink hplImage = (HyperLink)e.Item.FindControl("hplImage");
            hplImage.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            hplXemChiTiet.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }

        protected void rptTinTucLienQuan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblTitle = (Label)e.Item.FindControl("lblTitle");
            Label lblNgayTao = (Label)e.Item.FindControl("lblNgayTao");
            Image imgTinTucLienQuan = (Image)e.Item.FindControl("imgTinTucLienQuan");
            HyperLink hplXemChiTiet = (HyperLink)e.Item.FindControl("hplXemChiTiet");

            DMTinTucInfo info = (DMTinTucInfo)e.Item.DataItem;
            imgTinTucLienQuan.ImageUrl = "/" + info.ImageUrl;

            string ngayTao = "Ngày " + Format.FormatDate(info.CreateDate) + " | ";
            lblNgayTao.Text = ngayTao;

            lblTitle.Text = info.TieuDe;
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            hplXemChiTiet.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }


        #endregion
    }
}