using Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc;
using Com.Gosol.CMS.Model.DanhMuc;
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
    public partial class LoaiTinTucDemoAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int loaiTinTucID = Utils.ConvertToInt32(Request.Params["mangtinid"], 0);
            loaiTinID.Value = loaiTinTucID.ToString();
        }

        [WebMethod]
        public static string GetAlltinTucByIDLoaiTin(int loaiTinID)
        {
            //int IDLoaiTin = Utils.ConvertToInt32(loaiTinID, 1);
            List<DMTinTucInfo> lstTinTucByIDLoaiTin = new List<DMTinTucInfo>();
            string data = "";
            lstTinTucByIDLoaiTin = new DMTinTuc().TinPublic_GetByLoaiTinID(loaiTinID);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            data = serializer.Serialize(lstTinTucByIDLoaiTin);
            return data;
        }
        [WebMethod]
        public static string GetAllLoaiTinTucAndTinTucLienQuan()
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