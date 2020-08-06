using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
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
    public partial class LoaiTinTucDemoAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllLoaiTinTucAndTinLienQuan()
        {
            List<ChiTietTinTucInfo> ListTinTuc = new List<ChiTietTinTucInfo>();
            List<DMLoaiTinInfo> lstAllLoaiTin = new DMLoaiTin().GetAllLoaiTin_Public();
            foreach (DMLoaiTinInfo LoaiTinTuc in lstAllLoaiTin)
            {
                ChiTietTinTucInfo info = new ChiTietTinTucInfo();
                info.LoaiTinTuc = LoaiTinTuc;
                info.ChiTietTinTuc = new DMTinTuc().TinPublic_GetByLoaiTinID(LoaiTinTuc.IDLoaiTin);
                ListTinTuc.Add(info);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(ListTinTuc);
        }
    }
}