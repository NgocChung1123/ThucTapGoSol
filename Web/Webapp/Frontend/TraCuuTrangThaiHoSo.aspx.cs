using Com.Gosol.CMS.DAL.LichSu;
using Com.Gosol.CMS.Model.LichSuTraCuu;
using Com.Gosol.CMS.Utility;
using System;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TraCuuTrangThaiHoSo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string keyword = Utils.ConvertToString(Request.Params["keyword"], string.Empty);
            }
        }

        [WebMethod]
        public static string SaveHistory(
                    string XuLyDonID,
                    string SoDonThu,
                    string NgayTiepNhan,
                    string PhanLoaiDon,
                    string NoiDungDon,
                    string DoiTuongKhieuNai,
                    string HuongXuLy,
                    string CoQuanXuLy,
                    string CanBoXuLy,
                    string CoQuanTiepNhan,
                    string CanBoTiepNhan,
                    string CMND,
                    string NguoiDaiDien,
                    string DiaChi,
                    string CoQuanID,
                    string TrangThaiDonThu
            )
        {
            int result = 0;

            LichSuTraCuuInfo info = new LichSuTraCuuInfo()
            {
                XuLyDonID= Utility.Utils.ConvertToInt32(XuLyDonID,0),
                SoDonThu= SoDonThu,
                NgayTiepNhan= Utility.Utils.ConvertToDateTime(NgayTiepNhan,DateTime.MinValue),
                PhanLoaiDon= PhanLoaiDon,
                NoiDungDon= NoiDungDon,
                DoiTuongKhieuNai = DoiTuongKhieuNai,
                HuongXuLy = HuongXuLy,
                CoQuanXuLy = CoQuanXuLy,
                CanBoXuLy = CanBoXuLy,
                CoQuanTiepNhan = CoQuanTiepNhan,
                CanBoTiepNhan = CanBoTiepNhan,
                CMND = CMND,
                NguoiDaiDien = NguoiDaiDien,
                DiaChi = DiaChi,
                CoQuanID= Utility.Utils.ConvertToInt32(CoQuanID, 0),
                TrangThaiDonThu= TrangThaiDonThu
            };
            try
            {
                int val = new LichSuTraCuu().Insert(info);
                if (val > 0)
                {
                    int a = new LichSuTraCuuChiTiet().Insert(Convert.ToInt32(val), info.TrangThaiDonThu);
                    result = 1;
                }
            }
            catch
            {

            }

            return new JavaScriptSerializer().Serialize(result);
        }
    }
}