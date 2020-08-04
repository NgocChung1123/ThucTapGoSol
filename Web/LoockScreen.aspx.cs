using Com.Gosol.CMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class LoockScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //if (AccessControl.IsLoggedIn)
                //{
                //    Response.Redirect("~");
                //}
                //else
                //{
                //    txtPassWord.Focus();
                //}

                string User_Name = string.Empty;
                User_Name = Request.Cookies["uname"].Value;
                string User_TenCanBo = string.Empty;
                User_TenCanBo = Request.Cookies["utencanbo"].Value;
                if (User_TenCanBo != string.Empty)
                {
                    UserName.Text = User_TenCanBo;
                }
                else
                {
                    UserName.Text = User_Name;
                }

                
                txtPassWord.Focus();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string User_Name = string.Empty;
            User_Name = Request.Cookies["uname"].Value;

            String tenNguoiDung = User_Name;
            String matKhau = txtPassWord.Text;

            if (User_Name == string.Empty)
            {
                Response.Redirect("Login.aspx");
            }
            //Validate
            if (String.IsNullOrEmpty(matKhau))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Mật khẩu không được bỏ trống";
                txtPassWord.Focus();
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
                HttpCookie c_password = new HttpCookie("upass", matKhau);
                Response.Cookies.Add(c_username);
                Response.Cookies.Add(c_password);
                //}

                Response.Redirect("~");
            }
            else
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Mật khẩu không đúng";
            }
        }
    }
}