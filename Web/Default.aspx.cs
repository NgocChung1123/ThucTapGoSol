using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Model;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Web;
using HSCB.Helper;
using Com.Gosol.CMS.Model;
using Workflow;
using Com.Gosol.CMS.Web.Role;
using Com.Gosol.CMS.DAL.HeThong;

namespace Com.Gosol.CMS.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        private int ngayLDTiep = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            

            if (!IsPostBack)
            {
                string tenCanBo = IdentityHelper.GetTenCanBo();
                HttpCookie c_utencanbo = new HttpCookie("utencanbo", tenCanBo);
                Response.Cookies.Add(c_utencanbo);

                CustomCalendar calendar = new CustomCalendar();
                calendar.BindCell += calendar_BindCell;
                calendar.CssClass = "table table-bordered";
                calendar.CalendarNotes = new List<CalendarNote>();
                calendar.Title = "Lịch tháng " + DateTime.Now.Month;

                SystemConfigInfo sysInfo1 = new SystemConfig().GetByKey("LANH_DAO_TIEP_HANG_THANG");
                SystemConfigInfo sysInfo2 = new SystemConfig().GetByKey("BACKUP_DINH_KY");

                if (sysInfo1 != null)
                {
                    ngayLDTiep = Utils.ConvertToInt32(sysInfo1.ConfigValue, 0);
                    var note = new CalendarNote();
                    note.Day = ngayLDTiep;
                    //note.Text = "Ngày lãnh đạo tiếp dân";

                    calendar.CalendarNotes.Add(note);
                }

                if (sysInfo2 != null)
                {
                    int ngayBackupDinhKy = Utils.ConvertToInt32(sysInfo2.ConfigValue, 0);
                    var note = new CalendarNote();
                    note.Day = ngayBackupDinhKy;
                    //note.Text = "Ngày sao lưu cơ sở dữ liệu định kỳ";

                    calendar.CalendarNotes.Add(note);
                }

                calendar.CreateCalendar(DateTime.Now.Month, DateTime.Now.Year);
                ltrCalendar.Text = calendar.GeneratedText;

                try
                {
                    ThemeConfigInfo info = new ThemeConfig().GetTheme();
                    lblHomePhone.Text = info.HomePhone;
                    lblHomePhone.CssClass = "bg-primary";
                    //lblPhone.Text = info.Phone;
                }
                catch { }

                BindTaiLieuHD();

                lblTitle.Text = "Danh sách kế hoạch thanh tra năm " + DateTime.Now.Year;

                //if (RoleInstance.Instance.IsLanhDao(IdentityHelper.GetCanBoID()))
                //{
                //    hplDonThuhetHan.Visible = false;
                //    hplXlDonThuHetHan.Visible = false;
                //}
            }
            int nguoiDungID = IdentityHelper.GetUserID();
            if (nguoiDungID == (int)EnumAdministrator.NguoiDungID)
            {
                Response.Redirect("~/Webapp/Backend/HoiDap/TraLoiCauHoi.aspx");
            }
            else
            {
                Response.Redirect("~/Webapp/Backend/LichTiepDan/LichTiepDan.aspx");
            }
           
        }

        protected void BindTaiLieuHD()
        {
            try
            {
                rptTaiLieu.DataSource = new FileTaiLieu().GetAll();
            }
            catch
            {

            }
            rptTaiLieu.DataBind();
        }

        private void calendar_BindCell(object sender, CalendarEventArgs e)
        {
            CustomCalendar cal = sender as CustomCalendar;

        }      

        
    }
}
