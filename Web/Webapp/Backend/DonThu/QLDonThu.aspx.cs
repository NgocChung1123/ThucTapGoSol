using Com.Gosol.CMS.DAL.DonThu;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.CauHoi;
using Com.Gosol.CMS.Model.DonThu;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Backend.DonThu
{
    public partial class QLDonThu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DonThu, AccessLevel.Read)) // ko có quen xem thì out
            {
                Response.Redirect("~");
            }
            if (!IsPostBack)
            {
                int currentPage = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                    Session.Add("CurrentPage", currentPage);
                else
                    Session["CurrentPage"] = currentPage;
                SetSession();
                BindRepeater();
                createPaging();
            }

        }

        //public void LoadLinhVuc()
        //{
        //    ddlLinhVuc.DataSource = new Com.Gosol.CMS.DAL.DanhMuc.QLTinTuc.DMLinhVuc().GetAllDMLinhVuc();
        //    ddlLinhVuc.DataBind();
        //    ddlLinhVuc.Items.Insert(0, "Tất cả");
        //    ddlLinhVuc.SelectedIndex = 0;
        //}

        protected void SetSession()
        {
            int page = Utils.ConvertToInt32(Request.Params["page"], 1);
            if (Session["CurrentPage"] == null)
                Session.Add("CurrentPage", page);
            else Session["CurrentPage"] = page;

            int pageCheckTab = Utils.ConvertToInt32(Request.Params["page"], 0);

            if (pageCheckTab < 1)
            {
                Session["Keyword" + Request.Url.AbsolutePath] = null;
            }


            if (Session["Keyword" + Request.Url.AbsolutePath] != null)
            {
                txtSearch.Text = Session["Keyword" + Request.Url.AbsolutePath].ToString();
            }
        }

        protected void BindRepeater()
        {
            int currentPage = String.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Utils.ConvertToInt32(Request.QueryString["page"], 1);
            string keyword = txtSearch.Text;

            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();
            try
            {
                keyword = "%" + keyword + "%";
                string parmKeyword = keyword;
                rptDonThu.DataSource = new Com.Gosol.CMS.DAL.DonThu.DonThu().GetBySearch(keyword, start, end);
                rptDonThu.DataBind();
            }
            catch(Exception ex)
            {
            }

            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            //if (rptDonThu.Items.Count == 0)
            //{
            //    if (currentPage > 1)
            //    {
            //        currentPage = 1;
            //        Uri pageUri = new Uri(Request.Url.AbsoluteUri);
            //        Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
            //    }
            //}
        }

        private void createPaging()
        {
            int totalRow = 0;
            String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            txtSearch.Text = keyword;
            try
            {
                keyword = "%" + keyword + "%";
                totalRow = new Com.Gosol.CMS.DAL.DonThu.DonThu().CountSearch(keyword);
            }
            catch
            {
            }
            int PageSize = IdentityHelper.GetPageSize();
            int pageCount = (totalRow / PageSize);
            if (totalRow % PageSize != 0) pageCount++;
            if (pageCount > 1)
            {
                int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
                PagingHelper.CreatePaging(totalRow, currentPage, ref plhPaging);
            }
        }
        int stt = 1;
        protected void rptDonThu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            
            Label lblNgayTiepNhan = (Label)e.Item.FindControl("lblNgayTiepNhan");
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");
            LinkButton btnPublic = (LinkButton)e.Item.FindControl("btnPublic");
            DonThuInfo info = e.Item.DataItem as DonThuInfo;
            lblNgayTiepNhan.Text = info.NgayTiepNhan;
            
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            int pageSize = IdentityHelper.GetPageSize();
            lblSTT.Text = (stt + (currentPage - 1) * pageSize).ToString();
            stt++;
            if (info.CongKhai == "1")
            {
                btnPublic.Text = "Ẩn";
            }
            else
            {
                btnPublic.Text = "Công khai";
            }
           
        }

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRepeater();
            createPaging();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string SaveDB(string dataDonThu)
        {
            string[] dtInfo = dataDonThu.Split('@');
            string result = "";
            for(int i = 0; i < dtInfo.Length - 1; i++)
            {
                string[] dtCells = dtInfo[i].Split(';');
                DonThuInfo donThu = new DonThuInfo();
                donThu.SoDonThu = Utils.GetString(dtCells[0], String.Empty);
                donThu.NgayTiepNhan = Utils.GetString(dtCells[5], String.Empty);
                donThu.CoQuanTiepNhan = Utils.GetString(dtCells[6], String.Empty);
                donThu.CanBoTiepNhan = Utils.GetString(dtCells[7], String.Empty);
                donThu.PhanLoaiDon = Utils.GetString(dtCells[8], String.Empty);               
                donThu.NoiDungDon = Utils.GetString(dtCells[9], String.Empty);
                donThu.DoiTuongKhieuNai = Utils.GetString(dtCells[2], String.Empty);
                donThu.NguoiDaiDien = Utils.GetString(dtCells[10], String.Empty);
                donThu.CoQuanXuLy = Utils.GetString(dtCells[11], String.Empty);
                donThu.PhongBanXuLy = Utils.GetString(dtCells[3], String.Empty);
                //donThu.CanBoXuLy = Utils.GetString(dtCells[11], String.Empty);
                donThu.TrangThaiDonThu = Utils.GetString(dtCells[12], String.Empty);
                donThu.CongKhai = "0";
                donThu.XuLyDonID = Utils.ConvertToInt32(dtCells[1], 0);
                donThu.CoQuanID = Utils.ConvertToInt32(dtCells[4], 0);
                int val = 0;
                try
                {
                    if (donThu.XuLyDonID != 0)
                    {
                        val = new DAL.DonThu.DonThu().Insert(donThu);
                        if(val != 0)
                        {
                            if(i == 0)
                            {
                                result += donThu.XuLyDonID;
                            }
                            else
                            {
                                result += ";" + donThu.XuLyDonID;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
        [WebMethod]
        public static string InsertDonThu(List<DonThuJson> listDonThu)
        {
            int dem = 0;
            foreach(DonThuJson json in listDonThu)
            {
                DonThuInfo donThu = new DonThuInfo();
                donThu.SoDonThu = Utils.GetString(json.SoDonThu, String.Empty);
                donThu.NgayTiepNhan = Utils.GetString(json.NgayNhapDonStr, String.Empty);
                donThu.CoQuanTiepNhan = Utils.GetString(json.TenCoQuanTiepNhan, String.Empty);
                donThu.CanBoTiepNhan = Utils.GetString(json.TenCanBoTiepNhan, String.Empty);
                donThu.PhanLoaiDon = Utils.GetString(json.TenLoaiKhieuTo1, String.Empty);
                if(!String.IsNullOrEmpty(Utils.GetString(json.TenLoaiKhieuTo2, String.Empty)))
                {
                    donThu.PhanLoaiDon = Utils.GetString(json.TenLoaiKhieuTo2, String.Empty);
                }
                if (!String.IsNullOrEmpty(Utils.GetString(json.TenLoaiKhieuTo3, String.Empty)))
                {
                    donThu.PhanLoaiDon = Utils.GetString(json.TenLoaiKhieuTo3, String.Empty);
                }
                donThu.NoiDungDon = Utils.GetString(json.NoiDungDon, String.Empty);
                
                if (json.NhomKNInfo != null)
                {
                    if (json.NhomKNInfo.StringLoaiDoiTuongKN == "CaNhan")
                    {
                        donThu.DoiTuongKhieuNai = "Cá nhân";
                    }
                    if (json.NhomKNInfo.StringLoaiDoiTuongKN == "CoQuan")
                    {
                        donThu.DoiTuongKhieuNai = "Cơ quan, tổ chức";
                    }
                    if (json.NhomKNInfo.StringLoaiDoiTuongKN == "TapThe")
                    {
                        donThu.DoiTuongKhieuNai = "Tập thể";
                    }
                }
                else
                {
                    donThu.DoiTuongKhieuNai = "";
                }
                donThu.CoQuanXuLy = Utils.GetString(json.TenCoQuanXL, String.Empty);
                donThu.PhongBanXuLy = Utils.GetString(json.TenPhongBanXuLy, String.Empty);
                donThu.CanBoXuLy = Utils.GetString(json.TenCanBoXuLy, String.Empty);
                donThu.TrangThaiDonThu = Utils.GetString(json.TrangThaiDonThu, String.Empty);
                donThu.CongKhai = "0";
                donThu.XuLyDonID = Utils.ConvertToInt32(json.XuLyDonID, 0);
                int val = 0;
                try
                {
                    if (donThu.XuLyDonID != 0)
                    {
                        val = new DAL.DonThu.DonThu().Insert(donThu);
                        if (val != 0)
                        {
                            dem++;
                            if (json.lsDoiTuongKN != null)
                            {
                                foreach (DoiTuongKNInfo dt in json.lsDoiTuongKN)
                                {
                                    NguoiDaiDienInfo nguoiDaiDien = new NguoiDaiDienInfo();
                                    nguoiDaiDien.HoTen = Utils.GetString(dt.HoTen, String.Empty);
                                    nguoiDaiDien.DanToc = Utils.GetString(dt.TenDanToc, String.Empty);
                                    nguoiDaiDien.DiaChi = Utils.GetString(dt.DiaChiCT, String.Empty);
                                    nguoiDaiDien.DonThuID = val;
                                    new NguoiDaiDien().Insert(nguoiDaiDien);
                                }
                            }

                        }
                    }
                    
                }
                catch (Exception ex)
                {
                }
            }
            return "Đã đồng bộ " + dem.ToString() + " đơn thư";
        }

        protected void rptDonThu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int xldID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (e.CommandName == "Public")
            {
                if (xldID != 0)
                {
                    new DAL.DonThu.DonThu().UpdatePublic(xldID);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowAddCanBoForm", "ShowAddCanBoForm()", true);
                }


            }
            BindRepeater();
            createPaging();
        }
        [WebMethod]
        public static DonThuInfo GetDonThuBySoDonThu(string soDonThu, int coQuanID)
        {
            DonThuInfo dt = new DonThuInfo();
            try
            {
                dt = new DAL.DonThu.DonThu().GetBySoDonThu(soDonThu, coQuanID);
            }
            catch (Exception ex)
            {
                throw;
            }

            return dt;
        }
    }
}