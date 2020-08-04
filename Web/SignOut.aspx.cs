using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccessControl.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }
    }
}