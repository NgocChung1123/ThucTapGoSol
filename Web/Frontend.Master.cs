using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.DAL.HeThong;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class Frontend : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string currentTime = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                string currentDate = DateTime.Now.Day.ToString();
                string currentMonth = DateTime.Now.Month.ToString();
                string currentYear =DateTime.Now.Year.ToString();
                lblCurrentDate.Text = currentTime + " Ngày " + currentDate + " Tháng " + currentMonth +" năm " + currentYear;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            String tenNguoiDung = txtUserName.Text;
            String matKhau = txtPassword.Text;

            //Validate
            if (String.IsNullOrEmpty(tenNguoiDung))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Tên đăng nhập không được bỏ trống";
                txtUserName.Focus();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showLoginForm", "showLoginForm()", true);
                return;
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Mật khẩu không được bỏ trống";
                txtPassword.Focus();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showLoginForm", "showLoginForm()", true);
                return;
            }

            AccessControl.MemberSignOut();
            AccessControl.MemberSignIn(tenNguoiDung, matKhau);
            if (AccessControl.MemberIsLoggedIn)
            {
                //Remember user
                //if (remember)
                //{
                HttpCookie c_username = new HttpCookie("u_name", tenNguoiDung);
                //c_username.Expires = DateTime.Now.AddDays(30);
                HttpCookie c_password = new HttpCookie("u_pass", matKhau);
                //c_password.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(c_username);
                Response.Cookies.Add(c_password);
                //}
                string tenCanBo = Utils.GetString(new NguoiDung().GetNguoiDungByUserName(tenNguoiDung).TenCanBo, "");
                int canBoID = Utils.GetInt32(new NguoiDung().GetNguoiDungByUserName(tenNguoiDung).CanBoID, 0);
                //lblUserName.Text = tenCanBo;
                HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4TENCANBO", tenCanBo);
                HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4CANBOID", canBoID);
                //Response.Redirect("~");
            }
            else
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showLoginForm", "showLoginForm()", true);
            }
        }
        private CanBoInfo GetCanBoInfo()
        {
            CanBoInfo canBoInfo = new CanBoInfo();
            canBoInfo.CanBoID = Utils.ConvertToInt32(hdCanBoID.Value, 0);
            canBoInfo.TenCanBo = Utils.ConvertToString(txtHoTen.Text,"");
            canBoInfo.NgaySinh = Utils.ConvertToDateTime(txtNgaySinh.Text, Constant.DEFAULT_DATE);
            canBoInfo.DiaChi = txtDiaChi.Text;
            canBoInfo.GioiTinh = Utils.ConvertToInt32(ddlGioiTinh.SelectedValue, 0);            
            canBoInfo.Email = txtEmail.Text.Trim();
            canBoInfo.DienThoai = txtDienThoai.Text.Trim();          
            return canBoInfo;
        }
        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            CanBoInfo canBoInfo = GetCanBoInfo();
            try
            {
                int cbID = new DAL.CanBo().Insert(canBoInfo);
                if (cbID != 0)
                {
                    NguoiDungInfo ndInfo = new NguoiDungInfo();
                    ndInfo.CanBoID = cbID;
                    ndInfo.TenNguoiDung = txtTaiKhoan.Text.Trim();
                    ndInfo.MatKhau = Utils.HashFile(Encoding.ASCII.GetBytes(txtMatKhau.Text.Trim())).ToUpper();
                    int ndID = new NguoiDung().InsertNguoiDan(ndInfo);
                    if (ndID != 0)
                    {
                        hdfUser.Value = ndInfo.TenNguoiDung;
                        hdfPass.Value = ndInfo.MatKhau;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showConfirmLoginForm", "showConfirmLoginForm()", true);
                    }
                    else
                    {
                        new CanBo().Delete(cbID);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessage", "showMessage('Đăng ký tài khoản thất bại')", true);
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessage", "showMessage('Đăng ký tài khoản thất bại')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessage", "showMessage('Đăng ký tài khoản thất bại')", true);
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void btnLoginNow_Click(object sender, EventArgs e)
        {
            String tenNguoiDung = hdfUser.Value;
            String matKhau = hdfPass.Value;

            //Validate
            if (String.IsNullOrEmpty(tenNguoiDung))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Tên đăng nhập không được bỏ trống";
                txtUserName.Focus();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showLoginForm", "showLoginForm()", true);
                return;
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Mật khẩu không được bỏ trống";
                txtPassword.Focus();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showLoginForm", "showLoginForm()", true);
                return;
            }

            AccessControl.MemberSignOut();
            AccessControl.MemberSignInNow(tenNguoiDung, matKhau);
            if (AccessControl.MemberIsLoggedIn)
            {
                //Remember user
                //if (remember)
                //{
                HttpCookie c_username = new HttpCookie("u_name", tenNguoiDung);
                //c_username.Expires = DateTime.Now.AddDays(30);
                HttpCookie c_password = new HttpCookie("u_pass", matKhau);
                //c_password.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(c_username);
                Response.Cookies.Add(c_password);
                //}
                string tenCanBo = Utils.GetString(new NguoiDung().GetNguoiDungByUserName(tenNguoiDung).TenCanBo, "");
                int canBoID = Utils.GetInt32(new NguoiDung().GetNguoiDungByUserName(tenNguoiDung).CanBoID, 0);
                //lblUserName.Text = tenCanBo;
                HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4TENCANBO", tenCanBo);
                HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4CANBOID", canBoID);
                //Response.Redirect("~");
            }
            else
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showLoginForm", "showLoginForm()", true);
            }
        }

        [WebMethod]
        public static string GetDataMenu()
        {
            List<CauHinhModuleInfo> listCauHinhModule = new CauHinhModule().GetAll();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(listCauHinhModule);
        }
    }
}