using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!AccessControl.IsLoggedIn)
            {                
                Response.Redirect("~/Login.aspx");      
            }

            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckLoockScreen();
            if (ltrMenuTop.Text == String.Empty)
            {
                List<MenuInfo> parentMenus = Cache["Menu"] as List<MenuInfo>;
                /*if (parentMenus == null)
                {
                    parentMenus = new DAL.Menu().GetParents().ToList();
                    Cache.Add("Menu", parentMenus, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                }*/
                parentMenus = new DAL.Menu().GetParents().ToList();
                createMenu(parentMenus, 0);

            }
            
            try
            {
                //lblUserName.Text = IdentityHelper.GetTenCanBo();
                lblTenCoQuan.Text = "";

                Init();
            }
            catch { }

            if(!IsPostBack){
                
                CheckLoockScreen();

                txtPassWord.Focus();
            }
            
        }

        protected void Init()
        {
            lblFullName.Text = IdentityHelper.GetTenCanBo();
            //lblChucVu.Text = IdentityHelper.getC
            lblCoQuanUser.Text = IdentityHelper.GetTenCoQuan();

            hdfRoleUser.Value = IdentityHelper.GetRoleID().ToString();
            hdfCoQuanID.Value = IdentityHelper.GetCoQuanID().ToString();
        }

        protected void CheckLoockScreen()
        {
            
            string User_Pass = string.Empty;
            User_Pass = Request.Cookies["upass"].Value;
            if (User_Pass == string.Empty)
            {
                hdfLoockStatus.Value = "1";
            }
            else
            {
                hdfLoockStatus.Value = "0";
            }

            string User_TenCanBo = string.Empty;
            User_TenCanBo = Request.Cookies["utencanbo"].Value;
            string User_Name = string.Empty;
            User_Name = IdentityHelper.GetTenCanBo();//Request.Cookies["uname"].Value;
            UserName.Text = User_Name;
            
        }

        protected void lbtnLoockScreen_Click(object sender, EventArgs e)
        {
            HttpCookie c_password = new HttpCookie("upass", string.Empty);
            Response.Cookies.Add(c_password);
            hdfLoockStatus.Value = "1";

            //ScriptManager.RegisterStartupScript(this, typeof(Page), "LoockScreen", "LoockScreen()", true);
        }

        protected void lbtnUnLoockScreen_Click(object sender, EventArgs e)
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

            //AccessControl.SignOut();
            AccessControl.SignIn(tenNguoiDung, matKhau);
            if (AccessControl.IsLoggedIn)
            {
                HttpCookie c_username = new HttpCookie("uname", tenNguoiDung);
                HttpCookie c_password = new HttpCookie("upass", matKhau);
                Response.Cookies.Add(c_username);
                Response.Cookies.Add(c_password);
                hdfLoockStatus.Value = "0";
            }
            else
            {
                error_div.Attributes.CssStyle["display"] = "block";
                lblError.Text = "Mật khẩu không đúng";
            }
        }

        protected void lbtLogout_Click(object sender, EventArgs e)
        {
            AccessControl.SignOut();

            if (Request.Cookies["uname"] != null)
            {
                Response.Cookies["uname"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["upass"] != null)
            {
                Response.Cookies["upass"].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Redirect("~/Login.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = "";
            Session["Keyword_Global"] = keyword;

            Response.Redirect("DonThuManage.aspx");
        }

        protected void createMenu(List<MenuInfo> menuList, int level)
        {
            if (level == 0)
                ltrMenuTop.Text += "";
            else
                ltrMenuTop.Text += "<ul class='treeview-menu'>";

            foreach (MenuInfo menuInfo in menuList)
            {
                //check quyen
                if (AccessControl.User.HasPermission(menuInfo.ChucNangID, AccessLevel.Read) || menuInfo.ChucNangID == 0)
                {
                    List<MenuInfo> childMenus = new DAL.Menu().GetChilds(menuInfo.MenuID).ToList();

                    if (menuInfo.MenuChaID == 0)
                    {

                        if (childMenus.Count > 0)
                        {
                            if (menuInfo.MenuUrl != string.Empty)
                            {
                                ltrMenuTop.Text += "<li class='treeview'><a href='http://" + Request.Url.Authority + "/" + menuInfo.MenuUrl + "'><i class='" + menuInfo.ImageUrl + "'></i><span>" + menuInfo.TenMenu + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                            }
                            else
                            {
                                ltrMenuTop.Text += "<li class='treeview'><a href='#'><i class='" + menuInfo.ImageUrl + "'></i><span>" + menuInfo.TenMenu + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                            }
                        }
                        else
                        {
                            if (menuInfo.MenuUrl != string.Empty)
                            {
                                ltrMenuTop.Text += "<li class='treeview'><a href='http://" + Request.Url.Authority + "/" + menuInfo.MenuUrl + "'><i class='" + menuInfo.ImageUrl + "'></i><span>" + menuInfo.TenMenu + "</span><span class='pull-right-container'></span></a>";
                            }
                            else
                            {
                                ltrMenuTop.Text += "<li class='treeview'><a href='#'><i class='" + menuInfo.ImageUrl + "'></i><span>" + menuInfo.TenMenu + "</span><span class='pull-right-container'></span></a>";
                            }
                        }

                    }
                    else
                    {
                        #region
                        if (menuInfo.MenuUrl != String.Empty)
                        {
                            ltrMenuTop.Text += "<li><a href='http://" + Request.Url.Authority + "/" + menuInfo.MenuUrl + "'><img src='/" + menuInfo.ImageUrl + "' alt=''/>" + menuInfo.TenMenu + "</a>";
                        }
                        else
                        {
                            ltrMenuTop.Text += "<li><a href='#' onclick='return false;'><img src='/" + menuInfo.ImageUrl + "' alt=''/>" + menuInfo.TenMenu + "</a>";
                        }
                        if (childMenus.Count != 0)
                        {
                            ltrMenuTop.Text += "";
                        }
                        #endregion
                    }
                    level++;
                    if (childMenus.Count != 0)
                    {
                        createMenu(childMenus, level);
                    }
                    ltrMenuTop.Text += "</li>";
                }
            }
            ltrMenuTop.Text += "</ul>";
        }
    }
}