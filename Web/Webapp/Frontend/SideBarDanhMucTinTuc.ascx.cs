using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class SideBar2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
            }
        }

        protected void BindRepeater()
        {
            rptDanhMucTinTuc.DataSource = new DMLoaiTin().GetAllLoaiTin().Take(4);
            rptDanhMucTinTuc.DataBind();
        }

        protected void rptDanhMucTinTuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hplLoaiTin = (HyperLink)e.Item.FindControl("hplLoaiTin");
                Repeater rptDanhMucTinTuc = (Repeater)e.Item.FindControl("rptDanhMucTinTuc");

                DMLoaiTinInfo info = (DMLoaiTinInfo)e.Item.DataItem;
                //if (info != null)
                //{
                    hplLoaiTin.NavigateUrl = "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + info.IDLoaiTin;
                //}
            }
        }
    }
}