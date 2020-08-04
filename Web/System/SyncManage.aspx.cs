using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.DAL.Sync;
using Com.Gosol.CMS.DAL.TiepCongDan;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Web.Importer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class SyncManage : System.Web.UI.Page
    {
        public int stt = 1;
        public int pageSize = IdentityHelper.GetPageSize();

        private static List<BaoCao2aInfo> sync2aList;
        private static List<BaoCao2bInfo> sync2bList;
        private static List<BaoCao2cInfo> sync2cList;
        private static List<BaoCao2dInfo> sync2dList;
        private static List<DonThuInfo> syncDonThuList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSyncInfo();
                BindDDLs();
                BindRepeater();

                txtTuNgay.Text = Format.FormatDate(DateTime.Now);
                txtDenNgay.Text = Format.FormatDate(DateTime.Now);
            }
        }

        protected void btnExportXML_Click(object sender, EventArgs e)
        {
            var startDate = Utils.ConvertToDateTime(txtTuNgay.Text, DateTime.Now);
            var endDate = Utils.ConvertToDateTime(txtDenNgay.Text, DateTime.Now);

            //Lay danh sach don thu da chon
            List<int> selectedDonThuIDList = new List<int>();
            foreach (RepeaterItem item in rptDonThuDongBo.Items)
            {
                CheckBox cbxCheck = item.FindControl("cbxCheck") as CheckBox;
                if (cbxCheck.Checked)
                {
                    HiddenField hdfXuLyDonID = item.FindControl("hdfXuLyDonID") as HiddenField;
                    int xuLyDonID = Utils.ConvertToInt32(hdfXuLyDonID.Value, 0);
                    selectedDonThuIDList.Add(xuLyDonID);
                }
            }
            var selectedDonThuList = syncDonThuList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            //Lay danh sach Buoc giai quyet theo don thu da chon
            List<TheoDoiXuLyInfo> bgqList = new List<TheoDoiXuLyInfo>();

            //Lay danh sach Ket qua theo don thu da chon
            List<KetQuaInfo> kqList = new List<KetQuaInfo>();
            kqList = new KetQua().GetByTime(startDate, endDate).ToList();
            kqList = kqList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            //Lay danh sach Thi hanh theo don thu da chon
            List<ThiHanhInfo> thiHanhList = new List<ThiHanhInfo>();
            thiHanhList = new ThiHanh().GetByTime(startDate, endDate).ToList();
            thiHanhList = thiHanhList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            //Lay ds tiep dan theo don thu da chon
            List<TiepDanKhongDonInfo> tiepDanList = new List<TiepDanKhongDonInfo>();
            tiepDanList = new TiepDanKhongDon().GetInTime(startDate, endDate, 0).ToList();
            tiepDanList = tiepDanList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            var xmlString = DonThuImporter.Instance.ExportXML(selectedDonThuList, tiepDanList, bgqList, kqList, thiHanhList, Server.MapPath("~/App_Data/ImportTemplate/DonThu.xml"));

            string fileName = "import_" + startDate.ToString("ddMMyyyy") + "_" + endDate.ToString("ddMMyyyy");

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendHeader("Content-Type", "application/xml");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName + ".xml");
            Response.Write(xmlString);
            Response.Flush();
            Response.End();

        }

        #region sync click

        protected void btnSyncDonThu_Click(object sender, EventArgs e)
        {
            var startDate = Utils.ConvertToDateTime(txtTuNgay.Text, DateTime.Now);
            var endDate = Utils.ConvertToDateTime(txtDenNgay.Text, DateTime.Now);

            //Lay danh sach don thu da chon
            List<int> selectedDonThuIDList = new List<int>();
            foreach (RepeaterItem item in rptDonThuDongBo.Items)
            {
                CheckBox cbxCheck = item.FindControl("cbxCheck") as CheckBox;
                if (cbxCheck.Checked)
                {
                    HiddenField hdfXuLyDonID = item.FindControl("hdfXuLyDonID") as HiddenField;
                    int xuLyDonID = Utils.ConvertToInt32(hdfXuLyDonID.Value, 0);
                    selectedDonThuIDList.Add(xuLyDonID);
                }
            }
            var selectedDonThuList = syncDonThuList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            //Lay danh sach Buoc giai quyet theo don thu da chon
            List<TheoDoiXuLyInfo> bgqList = new List<TheoDoiXuLyInfo>();            

            //Lay danh sach Ket qua theo don thu da chon
            List<KetQuaInfo> kqList = new List<KetQuaInfo>();
            kqList = new KetQua().GetByTime(startDate, endDate).ToList();
            kqList = kqList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();            

            //Lay danh sach Thi hanh theo don thu da chon
            List<ThiHanhInfo> thiHanhList = new List<ThiHanhInfo>();
            thiHanhList = new ThiHanh().GetByTime(startDate, endDate).ToList();
            thiHanhList = thiHanhList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            //Lay ds tiep dan theo don thu da chon
            List<TiepDanKhongDonInfo> tiepDanList = new List<TiepDanKhongDonInfo>();
            tiepDanList = new TiepDanKhongDon().GetInTime(startDate, endDate, 0).ToList();
            tiepDanList = tiepDanList.Where(x => selectedDonThuIDList.Contains(x.XuLyDonID)).ToList();

            string importUrl = new SystemConfig().GetByKey("SYNC_SERVER_IP_2").ConfigValue + new SystemConfig().GetByKey("SYNC_DON_THU_URL").ConfigValue;

            try
            {
                DonThuImporter.Instance.Import(importUrl, selectedDonThuList, tiepDanList, bgqList, kqList, thiHanhList, Server.MapPath("~/App_Data/ImportTemplate/DonThu.xml"));
                lblMessage.Text = "Đồng bộ thành công";
            }
            catch
            {
                lblMessage.Text = "Đồng bộ thất bại";
            }
        }

        protected void btnSync2a_Click(object sender, EventArgs e)
        {
            SyncInfo syncInfo = new SyncInfo();
            syncInfo.Description = "Đồng bộ dữ liệu báo cáo 2a";

            int thang = Convert.ToInt32(ddlThang.SelectedValue);
            int nam = Convert.ToInt32(ddlNam.SelectedValue);            

            string maDonVi = new SystemConfig().GetByKey("MA_DON_VI").ConfigValue;
            string importUrl = new SystemConfig().GetByKey("SYNC_SERVER_IP").ConfigValue + new SystemConfig().GetByKey("SYNC_2a_URL").ConfigValue;

            try
            {
                BaoCao2aImporter.Instance.Import(importUrl, sync2aList, thang, nam, maDonVi, IdentityHelper.GetTenTinhTrienKhai(), Server.MapPath("~/App_Data/ImportTemplate/BaoCao2a.xml"));

                syncInfo.IsSuccess = true;
            }
            catch {
                syncInfo.IsSuccess = false;
            }

            InsertSyncHistory(syncInfo);
        }

        protected void btnSync2b_Click(object sender, EventArgs e)
        {
            SyncInfo syncInfo = new SyncInfo();
            syncInfo.Description = "Đồng bộ dữ liệu báo cáo 2b";

            int thang = Convert.ToInt32(ddlThang.SelectedValue);
            int nam = Convert.ToInt32(ddlNam.SelectedValue);

            string maDonVi = new SystemConfig().GetByKey("MA_DON_VI").ConfigValue;
            string importUrl = new SystemConfig().GetByKey("SYNC_SERVER_IP").ConfigValue + new SystemConfig().GetByKey("SYNC_2b_URL").ConfigValue;

            try
            {
                BaoCao2bImporter.Instance.Import(importUrl, sync2bList, thang, nam, maDonVi, IdentityHelper.GetTenTinhTrienKhai(), Server.MapPath("~/App_Data/ImportTemplate/BaoCao2b.xml"));

                syncInfo.IsSuccess = true;
                
            }
            catch
            {
                syncInfo.IsSuccess = false;
            }

            InsertSyncHistory(syncInfo);
        }

        protected void btnSync2c_Click(object sender, EventArgs e)
        {
            SyncInfo syncInfo = new SyncInfo();
            syncInfo.Description = "Đồng bộ dữ liệu báo cáo 2c";

            int thang = Convert.ToInt32(ddlThang.SelectedValue);
            int nam = Convert.ToInt32(ddlNam.SelectedValue);

            string maDonVi = new SystemConfig().GetByKey("MA_DON_VI").ConfigValue;

            string importUrl = new SystemConfig().GetByKey("SYNC_SERVER_IP").ConfigValue + new SystemConfig().GetByKey("SYNC_2c_URL").ConfigValue;

            try
            {
                BaoCao2cImporter.Instance.Import(importUrl, sync2cList, thang, nam, maDonVi, IdentityHelper.GetTenTinhTrienKhai(), Server.MapPath("~/App_Data/ImportTemplate/BaoCao2c.xml"));

                syncInfo.IsSuccess = true;
            }
            catch
            {
                syncInfo.IsSuccess = false;
            }

            InsertSyncHistory(syncInfo);
        }

        protected void btnSync2d_Click(object sender, EventArgs e)
        {
            SyncInfo syncInfo = new SyncInfo();
            syncInfo.Description = "Đồng bộ dữ liệu báo cáo 2d";

            int thang = Convert.ToInt32(ddlThang.SelectedValue);
            int nam = Convert.ToInt32(ddlNam.SelectedValue);

            string maDonVi = new SystemConfig().GetByKey("MA_DON_VI").ConfigValue;

            string importUrl = new SystemConfig().GetByKey("SYNC_SERVER_IP").ConfigValue + new SystemConfig().GetByKey("SYNC_2d_URL").ConfigValue;

            try
            {
                BaoCao2dImporter.Instance.Import(importUrl, sync2dList, thang, nam, maDonVi, IdentityHelper.GetTenTinhTrienKhai(), Server.MapPath("~/App_Data/ImportTemplate/BaoCao2d.xml"));
            }
            catch
            {
                syncInfo.IsSuccess = false;
            }

            InsertSyncHistory(syncInfo);
        }
        #endregion

        protected void btnDongBo_Click(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(ddlThang.SelectedValue);
            int nam = Convert.ToInt32(ddlNam.SelectedValue);

            var startDate = GetThoiGianBaoCao(thang, nam)[0];
            var endDate = GetThoiGianBaoCao(thang, nam)[1];

            if (ddlReportType.SelectedValue == "2a")
            {
                CreateBaoCao2a(startDate, endDate);                
            }
            else if (ddlReportType.SelectedValue == "2b")
            {
                CreateBaoCao2b(startDate, endDate);
            }
            else if (ddlReportType.SelectedValue == "2c")
            {
                CreateBaoCao2c(startDate, endDate);
            }
            else if (ddlReportType.SelectedValue == "2d")
            {
                CreateBaoCao2d(startDate, endDate);
            }
            else if (ddlReportType.SelectedValue == "don_thu")
            {
                startDate = Utils.ConvertToDateTime(txtTuNgay.Text, DateTime.Now);
                endDate = Utils.ConvertToDateTime(txtDenNgay.Text, DateTime.Now);

                CreateDonThuList(startDate, endDate);
            }
        }

        protected void rptDonThuDongBo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblNgayNhapDon = (Label)e.Item.FindControl("lblNgayNhapDon");
            DonThuInfo donThuInfo = (DonThuInfo)e.Item.DataItem;

            lblNgayNhapDon.Text = Format.FormatDate(donThuInfo.NgayNhapDon);
        }

        #region create bao cao

        private void CreateDonThuList(DateTime startDate, DateTime endDate)
        {
            syncDonThuList = new DonThu().GetBySearch("", 0, startDate, endDate, -1, 0, Int16.MaxValue, 0).ToList();
            rptDonThuDongBo.DataSource = syncDonThuList;
            rptDonThuDongBo.DataBind();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showDonThuDongBoDiv", "showDonThuDongBoDiv()", true);
        }

        private void CreateBaoCao2a(DateTime startDate, DateTime endDate)
        {
            List<BaoCao2aInfo> bcList = new List<BaoCao2aInfo>();
            var capList = new DAL.Cap().GetAll().Where(x => x.CapID != (int)CapQuanLy.CapTrungUong && x.CapID != (int)CapQuanLy.CapPhong).ToList();
            BaoCao2aCalc.CalculateBaoCaoCapTinh(ref bcList, startDate, endDate, capList);

            List<BaoCao2aInfo> dongBoList = new List<BaoCao2aInfo>();
            var toanTinhInfo = bcList.FirstOrDefault();
            toanTinhInfo.DonVi = "Tỉnh " + IdentityHelper.GetTenTinhTrienKhai();
            dongBoList.Add(bcList.FirstOrDefault());

            rptReport2a.DataSource = dongBoList;
            rptReport2a.DataBind();

            sync2aList = dongBoList;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showBaoCao2aDiv", "showBaoCao2aDiv()", true);
        }

        private void CreateBaoCao2b(DateTime startDate, DateTime endDate)
        {
            List<BaoCao2bInfo> bcList = new List<BaoCao2bInfo>();
            var capList = new DAL.Cap().GetAll().Where(x => x.CapID != (int)CapQuanLy.CapTrungUong && x.CapID != (int)CapQuanLy.CapPhong).ToList();
            BaoCao2bCalc.CalculateBaoCaoCapTinh(ref bcList, startDate, endDate, capList);

            List<BaoCao2bInfo> dongBoList = new List<BaoCao2bInfo>();
            var toanTinhInfo = bcList.FirstOrDefault();
            toanTinhInfo.DonVi = "Tỉnh " + IdentityHelper.GetTenTinhTrienKhai();
            dongBoList.Add(bcList.FirstOrDefault());

            rptReport2b.DataSource = dongBoList;
            rptReport2b.DataBind();

            sync2bList = dongBoList;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showBaoCao2bDiv", "showBaoCao2bDiv()", true);
        }

        private void CreateBaoCao2c(DateTime startDate, DateTime endDate)
        {
            List<BaoCao2cInfo> bcList = new List<BaoCao2cInfo>();
            var capList = new DAL.Cap().GetAll().Where(x => x.CapID != (int)CapQuanLy.CapTrungUong && x.CapID != (int)CapQuanLy.CapPhong).ToList();
            BaoCao2cCalc.CalculateBaoCaoCapTinh(ref bcList, startDate, endDate, capList);

            List<BaoCao2cInfo> dongBoList = new List<BaoCao2cInfo>();
            var toanTinhInfo = bcList.FirstOrDefault();
            toanTinhInfo.DonVi = "Tỉnh " + IdentityHelper.GetTenTinhTrienKhai();
            dongBoList.Add(bcList.FirstOrDefault());

            rptReport2c.DataSource = dongBoList;
            rptReport2c.DataBind();

            sync2cList = dongBoList;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showBaoCao2cDiv", "showBaoCao2cDiv()", true);
        }

        private void CreateBaoCao2d(DateTime startDate, DateTime endDate)
        {
            List<BaoCao2dInfo> bcList = new List<BaoCao2dInfo>();
            var capList = new DAL.Cap().GetAll().Where(x => x.CapID != (int)CapQuanLy.CapTrungUong && x.CapID != (int)CapQuanLy.CapPhong).ToList();
            BaoCao2dCalc.CalculateBaoCaoCapTinh(ref bcList, startDate, endDate, capList);

            List<BaoCao2dInfo> dongBoList = new List<BaoCao2dInfo>();
            var toanTinhInfo = bcList.FirstOrDefault();
            toanTinhInfo.DonVi = "Tỉnh " + IdentityHelper.GetTenTinhTrienKhai();
            dongBoList.Add(bcList.FirstOrDefault());

            rptReport2d.DataSource = dongBoList;
            rptReport2d.DataBind();

            sync2dList = dongBoList;

            ScriptManager.RegisterStartupScript(this, typeof(Page), "showBaoCao2dDiv", "showBaoCao2dDiv()", true);
        }
        #endregion

        private List<DateTime> GetThoiGianBaoCao(int thang, int nam)
        {
            List<DateTime> resultList = new List<DateTime>();

            DateTime startDate;
            DateTime endDate;

            if (thang == 1)
            {
                startDate = new DateTime(nam - 1, 12, 16);
            }
            else
            {
                startDate = new DateTime(nam, thang - 1, 16);
            }

            endDate = new DateTime(nam, thang, 15);

            resultList.Add(startDate);
            resultList.Add(endDate);

            return resultList;
        }
        
        private void BindSyncInfo()
        {
            string serverIP = new SystemConfig().GetByKey("SYNC_SERVER_IP").ConfigValue;
            string syncAcc = new SystemConfig().GetByKey("SYNC_ACCOUNT").ConfigValue;
            string syncPass = new SystemConfig().GetByKey("SYNC_PASSWORD").ConfigValue;

            lblServerIP.Text = serverIP;
            lblAccountInfo.Text = syncAcc + "/" + syncPass.Substring(0, syncPass.Length - 2) + "**";
        }

        private void BindDDLs()
        {
            for (int i = 1; i <= 12; i++)
            {
                ddlThang.Items.Add(new ListItem("Tháng " + i, i.ToString()));
            }
            ddlThang.SelectedValue = DateTime.Now.Month.ToString();

            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                ddlNam.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlNam.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void InsertSyncHistory(SyncInfo syncInfo)
        {
            syncInfo.SyncDate = DateTime.Now;
            syncInfo.SyncDuration = 0;
            syncInfo.SyncRows = 0;
            
            try
            {
                new Sync().Insert(syncInfo);
            }
            catch
            {
                throw;
            }
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            //String keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], String.Empty);
            //txtSearch.Text = keyword;
            if (currentPage == 0)
            {
                currentPage = 1;
            }


            // cho cac bien vao query info
            QueryFilterInfo info = new QueryFilterInfo();
            info.KeyWord = "";//Utils.ConvertToString(txtSearch.Text, string.Empty);
            info.Start = (currentPage - 1) * IdentityHelper.GetPageSize();
            info.End = currentPage * IdentityHelper.GetPageSize();


            try
            {
                rptSyncHistory.DataSource = new Sync().GetSyncHistory(info);
            }
            catch (Exception e)
            {
            }


            rptSyncHistory.DataBind();
            //truong hop xoa ban ghi cuoi cung cua trang, chuyen ve trang truoc
            if (rptSyncHistory.Items.Count == 0)
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                    Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
                }
            }
        }

        protected void rptSyncHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SyncInfo info = (SyncInfo)e.Item.DataItem;

            Label lblSuccess = (Label)e.Item.FindControl("lblSuccess");
            Label lblSyncDate = (Label)e.Item.FindControl("lblSyncDate");

            if (info.IsSuccess)
                lblSuccess.Text = "Thành công";
            else
            {
                lblSuccess.Text = "Thất bại";
                lblSuccess.CssClass += "redClass";
            }

            lblSyncDate.Text = info.SyncDate.ToString();


            int page = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : Com.Gosol.CMS.Utility.Utils.ConvertToInt32(Request.QueryString["page"], 1);
            if (page == null)
                page = 1;
            stt = (page - 1) * pageSize + 1;
        }
    }
}