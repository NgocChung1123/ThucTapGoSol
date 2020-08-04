using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TinTucDemo : System.Web.UI.Page
    {
        static IList<DMTinTucInfo> lst3TinHot;
        static IList<DMTinTucInfo> lstAllTinHot;
        static IList<DMLoaiTinInfo> lstAllLoaiTin;
        static IList<DMTinTucInfo> lstTinTucByIDLoaiTin;
        private List<DMTinTucInfo> lsTinMoi;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindLoaiTinTuc();
        }
        [WebMethod]
        public static string getTop3TinHot()
        {
            lst3TinHot = new DMTinTuc().GetTop3_TinNoiBat_TinHot();
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(lst3TinHot);
                return data;
            }
            catch
            {
                return data;
            }
        }

        [WebMethod]
        public static string getAllTinHot()
        {
            lstAllTinHot = new DMTinTuc().Get_ALL_Tin_Hot();
            List<DMTinTucInfo> lstTemp = lstAllTinHot.Where(x => x.IDTinTuc != lst3TinHot[0].IDTinTuc && x.IDTinTuc != lst3TinHot[1].IDTinTuc && x.IDTinTuc != lst3TinHot[2].IDTinTuc).Take(10).ToList();
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(lstTemp);
                return data;
            }
            catch
            {
                return data;
            }
        }

        [WebMethod]
        public static string getAllLoaiTinTuc()
        {
            lstAllLoaiTin = new DMLoaiTin().GetAllLoaiTin_Public();
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(lstAllLoaiTin);
                return data;
            }
            catch
            {
                return data;
            }
        }

        [WebMethod]
        public static string getAllTinTuc_By_IDLoaiTin(int IDLoaiTin)
        {
            lstTinTucByIDLoaiTin = new DMTinTuc().TinPublic_GetByLoaiTinID(IDLoaiTin);
            string data = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                data = serializer.Serialize(lstTinTucByIDLoaiTin);
                return data;
            }
            catch
            {
                return data;
            }
        }
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
        protected void BindTinChildren(ref Repeater rptTinTucChildren, ref Repeater rptTinTucLienQuan, int loaiTinID, ref HtmlGenericControl divNoiDungTin)
        {
            List<DMTinTucInfo> lsTinChildren = new DMTinTuc().TinPublic_GetByLoaiTinID(loaiTinID).ToList();
            List<DMTinTucInfo> lsTinResult = new List<DMTinTucInfo>();
            List<DMTinTucInfo> lsTinChildrenTop1Result = new List<DMTinTucInfo>();
            lsTinMoi = new DMTinTuc().Get_ALL_Tin_Hot().Take(8).ToList();
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

            imgTinKinhTeNoiBat.ImageUrl = "~/" + info.ImageUrl;

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
            imgTinTucLienQuan.ImageUrl = "~/" + info.ImageUrl;

            string ngayTao = "Ngày " + Format.FormatDate(info.CreateDate) + " | ";
            lblNgayTao.Text = ngayTao;

            lblTitle.Text = info.TieuDe;
            HyperLink hylTin = (HyperLink)e.Item.FindControl("hylTin");
            hylTin.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
            hplXemChiTiet.NavigateUrl = "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + info.IDTinTuc;
        }

    }
}