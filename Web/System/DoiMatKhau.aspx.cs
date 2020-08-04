using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Security;
using System.Web.Security;
using System.Web.Configuration;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    public partial class DoiMatKhau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //MenuHelper.CreateSideMenu(ltrSideMenu, "Hệ thống");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String oldPassword = txtOldPassword.Text.Trim();
            oldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, FormsAuthPasswordFormat.MD5.ToString());
            String userPassword = ((AccessControlIdentity)AccessControl.User.Identity).GetProperty("MatKhau").ToString().ToUpper();

            String newPass = txtNewPassword.Text.Trim();
            int kq = 0;
            if (oldPassword != userPassword)
            {
                lblError.Text = "Mật khẩu cũ không chính xác";
                return;
            }
            else
            if (newPass == txtOldPassword.Text.Trim())
            {
                lblError.Text = "Mật khẩu mới giống mật khẩu cũ";
                return;
            }
            else
            if (newPass != txtNewPasswordRepeat.Text.Trim())
            {
                lblError.Text = "Mật khẩu nhập lại không chính xác";
                return;
            }
            else
            {
                lblError.Text = "";
            }
            int ndID = Utils.ConvertToInt32(((AccessControlIdentity)AccessControl.User.Identity).GetProperty("NguoiDungID"), 0);
            NguoiDungInfo ndInfo = new NguoiDungInfo();
            try
            {
                ndInfo = new DAL.NguoiDung().GetNguoiDungByID(ndID);
            }
            catch
            {
            }
            ndInfo.MatKhau = FormsAuthentication.HashPasswordForStoringInConfigFile(newPass, FormsAuthPasswordFormat.MD5.ToString());
            try
            {
                kq=new DAL.NguoiDung().Update(ndInfo);
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
            catch
            {
                lblContentSuccess.Text = "";
                lblContentErr.Text = Constant.CONTENT_MESSAGE_ERROR;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoError", "showthongBaoError()", true);
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = String.Empty;
            txtNewPassword.Text = String.Empty;
            txtNewPasswordRepeat.Text = String.Empty;
        }

    }
}