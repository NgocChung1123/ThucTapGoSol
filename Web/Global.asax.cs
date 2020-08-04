using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Com.Gosol.CMS.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            Application["DangTruyCap"] = 0;
            SystemConfigInfo dangTruyCapInfo = new SystemConfig().GetByKey("DANG_TRUY_CAP");
            if (dangTruyCapInfo != null)
            {
                dangTruyCapInfo.ConfigValue = "0";
                new SystemConfig().Update(dangTruyCapInfo);
            }
        }

        void RegisterRoutes(RouteCollection routes)
        {
            // tin tuc
            //routes.MapPageRoute("", "xem-chi-tiet-tin-tuc/{url_title}", "~/Webapp/Frontend/TinTucDetail.aspx");
            //routes.MapPageRoute("", "xem-mang-tin/{mangtinid}", "~/Webapp/Frontend/LoaiTinTucDetail.aspx");
            routes.MapPageRoute("", "", "~/Webapp/Frontend/Home.aspx");
            //routes.MapPageRoute("", "quan-tri", "~/Default.aspx");

            ////menu
            //routes.MapPageRoute("", "tin-tuc", "~/Webapp/Frontend/TinTuc.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            int count_visit = 0;
            SystemConfigInfo tongLuotTruyCapInfo = new SystemConfig().GetByKey("TONG_LUOT_TRUY_CAP");
            SystemConfigInfo dangTruyCapInfo = new SystemConfig().GetByKey("DANG_TRUY_CAP");
            if (tongLuotTruyCapInfo != null)
            {
                count_visit = Utils.ConvertToInt32(tongLuotTruyCapInfo.ConfigValue, 0);
                count_visit++;
            }
            if (dangTruyCapInfo != null)
            {
                int val = Utils.ConvertToInt32(dangTruyCapInfo.ConfigValue, 0) + 1;
                dangTruyCapInfo.ConfigValue = val.ToString();
            }
            // khóa website
            Application.Lock();
            // gán biến Application count_visit
            Application["count_visit"] = count_visit;
            tongLuotTruyCapInfo.ConfigValue = count_visit.ToString();
            new SystemConfig().Update(tongLuotTruyCapInfo);
            new SystemConfig().Update(dangTruyCapInfo);
            // Mở khóa website
            Application.UnLock();

            if (Session["DangTruyCap"] == null)
            {
                Session["DangTruyCap"] = 1;
            }
            else
            {
                Session["DangTruyCap"] = int.Parse(Session["DangTruyCap"].ToString()) + 1;
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}