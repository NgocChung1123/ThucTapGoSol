using Com.Gosol.CMS.DAL.TraLoiCauHoi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class ChiTietCauHoi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int cauHoiID = Utility.Utils.ConvertToInt32(Request.QueryString["id"].ToString(), 0);
            var info = new CauHoi().GetCauHoiByID(cauHoiID);
            lblTieuDe.Text =  info.NDCauHoi;
            lblNguoiHoi.Text = info.HoTen ==null ? info.CreaterName : info.HoTen;
            lblNgayHoi.Text = info.CreateDate.ToShortDateString();
            //lblNgayHoi.Text = DateTime.ParseExact(info.CreateDate.ToShortDateString(), "dd/MM/yyyy",
            //                            CultureInfo.InvariantCulture).ToString();
            lblNDCauHoi.Text = info.NDCauHoi;
            lblNguoiTraLoi.Text = info.NguoiTraLoi;
            lblNDTraLoi.Text = info.NDTraLoi;
            if (info.NDTraLoi == "" || !info.Public)
            {
                pnTraLoi.Visible = false;
                pnChuaTraLoi.Visible = true;
            }
            else
            {
                pnTraLoi.Visible = true;
                pnChuaTraLoi.Visible = false;
            }
        }
    }
}