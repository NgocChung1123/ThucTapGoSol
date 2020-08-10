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
    public partial class TinTucDetailDemoAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int IDTinTuc = Utils.ConvertToInt32(Request.Params["tintuc"], 1);
            //int IDLoaiTin = Utils.ConvertToInt32(idLoaiTin.Value, 1);
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
            idLoaiTin.Value = TinTuc.IDLoaiTin.ToString();
        }

        [WebMethod]
        public static string GetTinLienQuanByIDLoaiTin(int LoaiTin)
        {
            string data = "";
            List<DMTinTucInfo> lstTinLienQuan = new List<DMTinTucInfo>();
            lstTinLienQuan = new DMTinTuc().GetByLoaiTinID(LoaiTin).Take(5).ToList();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            data = serializer.Serialize(lstTinLienQuan);
            return data;
        }
    }
}