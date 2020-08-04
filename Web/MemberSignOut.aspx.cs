﻿using Com.Gosol.CMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web
{
    public partial class MemberSignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccessControl.MemberSignOut();
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "isLogin", "isLogin()", true);
            Response.Redirect("~//Webapp/Frontend/Home.aspx");
        }
    }
}