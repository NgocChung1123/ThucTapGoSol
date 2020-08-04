using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.DAL.HeThong;
using System.Web.Script.Serialization;

namespace Com.Gosol.CMS.Web
{
    public partial class CauHinhModuleHT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoThamSoHeThong, AccessLevel.Read))
            {
                Response.Redirect("~");
            }

            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                    Session.Add("CurrentPage", page);
                else Session["CurrentPage"] = page;
                Session["Keyword" + Request.Url.AbsolutePath] = null;
                //BindRepeater();
                BindModuleDDL();
                hdfCurrentPage.Value = 1.ToString();
                hdfPageSize.Value = IdentityHelper.GetPageSize().ToString();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "LoadData", "LoadData(1);", true);
            }
        }
        private void BindModuleDDL()
        {
            
            List<CauHinhModuleInfo> chList = new List<CauHinhModuleInfo>();
            CauHinhModuleInfo module = new CauHinhModuleInfo();
            module.ModuleStr = "Module";
            module.Module = 0;
            chList.Add(module);
            CauHinhModuleInfo menu = new CauHinhModuleInfo();
            menu.ModuleStr = "Menu";
            menu.Module = 1;
            chList.Add(menu);
            CauHinhModuleInfo sidebar = new CauHinhModuleInfo();
            sidebar.ModuleStr = "Sidebar";
            sidebar.Module = 2;
            chList.Add(sidebar);

            ddlModule.DataSource = chList;
            ddlModule.DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            createPaging();
        }

        private void createPaging()
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
        }

        protected void rptSystemConfig_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");

            if (!AccessControl.User.HasPermission(ChucNangEnum.KhaiBaoThamSoHeThong, AccessLevel.Edit))
            {
                btnEdit.Enabled = false;
                btnEdit.CssClass += " disable";
                btnEdit.ToolTip = Constant.ToolTip;
            }
        }

        protected void rptSystemConfig_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int configID = Utils.ConvertToInt32(e.CommandArgument, 0);
            if (e.CommandName == "Edit")
            {
                SystemConfigInfo configInfo = new SystemConfigInfo();
                try
                {
                    configInfo = new DAL.SystemConfig().GetByID(configID);
                }
                catch
                {
                }
                txtSystemConfigID.Text = configID.ToString();
                txtConfigKey.Text = configInfo.ConfigKey;
                txtConfigValue.Text = configInfo.ConfigValue;
                txtDescription.Text = configInfo.Description;

                //light.Attributes.CssStyle["display"] = "block";
                //fade.Attributes.CssStyle["display"] = "block";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showEditFormThamSoHeThong", "showEditFormThamSoHeThong();", true);
            }
        }

        protected void cbTrangThaiHienThi_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "updateTrangThaiHienThi", "updateTrangThaiHienThi();", true);
        }

        protected void txtThuTuHienThi_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "showEditFormThamSoHeThong", "showEditFormThamSoHeThong();", true);
        }

        [System.Web.Services.WebMethod]
        public static string LoadData(string currentPage, string module)
        {
            int _currentPage = Convert.ToInt32(currentPage);
            if (_currentPage == 0)
            {
                _currentPage = 1;
            }
            int start = (_currentPage - 1) * IdentityHelper.GetPageSize();
            int end = _currentPage * IdentityHelper.GetPageSize();
            int Module = Utils.ConvertToInt32(module, 0);

            List<CauHinhModuleInfo> listModule = new List<CauHinhModuleInfo>();
            try
            {
                listModule = new CauHinhModule().GetBySearch("", start, end, Module);
            }
            catch
            {
            }

            String data = "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            data = serializer.Serialize(listModule);
            return data;
        }

        [System.Web.Services.WebMethod]
        public static string UpdateTrangThaiHienThi(string trangthai, string moduleID)
        {
            int ModuleID = Utils.ConvertToInt32(moduleID, 0);
            Boolean TrangThaiHienThi = Utils.ConvertToBoolean(trangthai,false);
            CauHinhModuleInfo info = new CauHinhModuleInfo();
            info.ModuleID = ModuleID;
            info.TrangThaiHienThi = TrangThaiHienThi;

            var val = new CauHinhModule().UpdateTrangThaiHienThi(info);

            String data = "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            data = serializer.Serialize(val);
            return data;
        }

        [System.Web.Services.WebMethod]
        public static string UpdateThuTuHienThi(string thutu, string moduleID)
        {
            int ModuleID = Utils.ConvertToInt32(moduleID, 0);
            int ThuTuHienThi = Utils.ConvertToInt32(thutu, 0);
            
            CauHinhModuleInfo info = new CauHinhModuleInfo();
            info.ModuleID = ModuleID;
            info.ThuTuHienThi = ThuTuHienThi;

            var val = new CauHinhModule().UpdateThuTuHienThi(info);

            String data = "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            data = serializer.Serialize(val);
            return data;
        }
    }
}