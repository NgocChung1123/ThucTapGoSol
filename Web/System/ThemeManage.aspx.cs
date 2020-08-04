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

namespace Com.Gosol.CMS.Web
{
    public partial class ThemeManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.ThayDoiGiaoDien, AccessLevel.Edit)) { 
                //Redirect
                Response.Redirect("~");
            }

            if (!IsPostBack)
            {
                GetInfo();
            }            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }

        protected void GetInfo() {
            try
            {
                ThemeConfigInfo info = new ThemeConfig().GetTheme();
                hdfThemeConfigID.Value = info.ThemeConfigID.ToString();
                txtUnitName.Text = info.UnitName;
                txtUnitThemeName.Text = info.UnitThemeName;
                txtHomePhone.Text = info.HomePhone;
                txtPhone.Text = info.Phone;

                hdfFileUpload.Value = info.UnitLogo;
                if (info.UnitLogo != null)
                {
                    imgUnitLogo.ImageUrl = info.UnitLogo;
                }
                else {                    
                    imgUnitLogo.ImageUrl = "UploadFiles/quochuy.png";
                }
            }
            catch
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            ThemeConfigInfo info = new ThemeConfigInfo();
            info.ThemeConfigID = Utils.ConvertToInt32(hdfThemeConfigID.Value, 0);
            info.UnitName = txtUnitName.Text;
            info.UnitThemeName = txtUnitThemeName.Text;
            info.HomePhone = txtHomePhone.Text;
            info.Phone = txtPhone.Text;

            #region upload file
            String fileName = String.Empty;
            try
            {
                fileName = FileHelper.Upload(fileUpload);
            }
            catch
            {
            }

            if (fileName != String.Empty)
            {
                info.UnitLogo = "UploadFiles/" + fileName;
            }
            else {
                info.UnitLogo = hdfFileUpload.Value;
            }

            #endregion

            try
            {
                int kq = 0;
                if (info.ThemeConfigID > 0) { kq = new ThemeConfig().Update(info); }
                else { kq = new ThemeConfig().Insert(info); }
                if (kq != 0)
                {
                    lblContentSuccess.Text = Constant.CONTENT_MESSAGE_SUCCESS;
                    lblContentErr.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
                }
                else
                {
                    lblContentSuccess.Text = "";
                    lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
                }
            }
            catch{
            }

            GetInfo();
        }
    }
}