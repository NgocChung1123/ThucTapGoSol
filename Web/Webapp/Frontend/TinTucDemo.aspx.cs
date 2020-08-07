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
            List<ChiTietTinTucInfo> ListTinTuc = new List<ChiTietTinTucInfo>();
            lstAllLoaiTin = new DMLoaiTin().GetAllLoaiTin_Public();
            foreach(DMLoaiTinInfo LoaiTinTuc in lstAllLoaiTin)
            {
                ChiTietTinTucInfo info = new ChiTietTinTucInfo();
                info.LoaiTinTuc = LoaiTinTuc;
                info.ChiTietTinTuc = new DMTinTuc().TinPublic_GetByLoaiTinID(LoaiTinTuc.IDLoaiTin);
                ListTinTuc.Add(info);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(ListTinTuc);
        }

        [WebMethod]
        public static string getAllTinTuc_By_IDLoaiTin(string IDLoaiTin)
        {
            int ID = Utils.ConvertToInt32(IDLoaiTin, 0);
            lstTinTucByIDLoaiTin = new DMTinTuc().TinPublic_GetByLoaiTinID(ID);
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

    }
}