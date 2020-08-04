using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                if (AccessControl.IsLoggedIn)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    txtUserName.Focus();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            String tenNguoiDung =txtUserName.Text;
            String matKhau = txtPassword.Text;

            //Validate
            if (String.IsNullOrEmpty(tenNguoiDung))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Tên đăng nhập không được bỏ trống";
                txtUserName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Mật khẩu không được bỏ trống";
                txtPassword.Focus();
                return;
            }

            AccessControl.SignOut();
            AccessControl.SignIn(tenNguoiDung, matKhau);
            if (AccessControl.IsLoggedIn)
            {
                //Remember user
                //if (remember)
                //{
                    HttpCookie c_username = new HttpCookie("uname", tenNguoiDung);
                    //c_username.Expires = DateTime.Now.AddDays(30);
                    HttpCookie c_password = new HttpCookie("upass", matKhau);
                    //c_password.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(c_username);
                    Response.Cookies.Add(c_password);
                //}

                Response.Redirect("~/Default.aspx");
            }
            else
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
        }
    }
}