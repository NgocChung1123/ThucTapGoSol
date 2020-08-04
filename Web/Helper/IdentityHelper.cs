using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    public static class IdentityHelper
    {
        public static String GetTenTinhTrienKhai()
        {
            String tenTinh = String.Empty;
            if (HttpContext.Current.Cache["TenTinhTrienKhai"] != null)
            {
                tenTinh = HttpContext.Current.Cache["TenTinhTrienKhai"].ToString();
            }
            else
            {
                try
                {
                    tenTinh = new DAL.Tinh().GetByID(IdentityHelper.GetTinhID()).TenTinh;
                }
                catch
                {
                }
                HttpContext.Current.Cache.Add("TenTinhTrienKhai", tenTinh, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
            }
            return tenTinh;
        }

        public static int GetPageSize()
        {
            int pageSize = 10;
            
                try
                {
                    SystemConfigInfo sysInfo = new SystemConfigInfo();
                    string psize = "PAGE_SIZE";
                    sysInfo = new SystemConfig().GetByKey(psize);
                    pageSize = Utils.ConvertToInt32(sysInfo.ConfigValue, 0);
                }
                catch
                {
                }
                
            
            return pageSize;
        }
        
        public static int GetCanBoID()
        {
            int result = 0;
            result = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("CanBoID"), 0);
            return result;
        }

        public static String GetTenCanBo()
        {
            String tenCB = String.Empty;

            tenCB = ((AccessControlIdentity)AccessControl.User.Identity).GetProperty("TenCanBo").ToString();

            return tenCB;
        }

        public static String GetMaCoQuan()
        {
            String maCQ = String.Empty;

            maCQ = ((AccessControlIdentity)AccessControl.User.Identity).GetProperty("MaCQ").ToString();

            return maCQ;
        }

        public static String GetTenCoQuan()
        {
            String tenCQ = String.Empty;

            tenCQ = ((AccessControlIdentity)AccessControl.User.Identity).GetProperty("TenCoQuan").ToString();

            return tenCQ;
        }

        public static int GetCoQuanID()
        {
            int cqID = 0;

            cqID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("CoQuanID"), 0);

            return cqID;
        }

        public static int GetCoQuanChaID()
        {
            int cqID = 0;

            cqID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("CoQuanChaID"), 0);

            return cqID;
        }

        public static int GetRoleID()
        {
            int roleID = 0;

            roleID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("RoleID"), 0);

            return roleID;
        }

        public static string GetRoleName()
        {
            string roleName = string.Empty;

            roleName = Utils.GetString(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("RoleName"), string.Empty);

            return roleName;
        }

        public static int GetPhongID()
        {
            int phongID = 1;

            phongID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("PhongBanID"), 0);

            return phongID;
        }


        public static bool GetSuDungPM()
        {
            bool sudungPM = false;

            sudungPM = Utils.ConvertToBoolean(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("SuDungPM"), false);

            return sudungPM;
        }

        public static int GetUserID()
        {
            int userID = 0;

            userID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("NguoiDungID"), 0);

            return userID;
        }

        public static int GetCapID()
        {
            int capID = 0;

            capID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("CapID"), 0);

            return capID;
        }

        public static int GetTinhID()
        {
            int tinhID = 0;

            tinhID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("TinhID"), 0);

            return tinhID;
        }

        public static int GetHuyenID()
        {
            int huyenID = 0;

            huyenID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("HuyenID"), 0);

            return huyenID;
        }

        public static int GetXaID()
        {
            int xaID = 0;

            xaID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("XaID"), 0);

            return xaID;
        }

        public static int GetWorkFlowID()
        {
            int workFlowID = 0;

            workFlowID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("WorkFlowID"), 0);

            return workFlowID;
        }

        public static String GetWorkFlowCode()
        {
            String workFlowCode = String.Empty;

            workFlowCode = ((AccessControlIdentity)AccessControl.User.Identity).GetProperty("WorkFlowCode").ToString();

            return workFlowCode;
        }

        public static int GetWFTienHanhTTID()
        {
            int workFlowID = 0;

            workFlowID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("WFTienHanhTTID"), 0);

            return workFlowID;
        }

        public static String GetWFCodeTienHanhTT()
        {
            String workFlowCode = String.Empty;

            workFlowCode = ((AccessControlIdentity)AccessControl.User.Identity).GetProperty("WFCodeTienHanhTT").ToString();

            return workFlowCode;
        }
    }
}