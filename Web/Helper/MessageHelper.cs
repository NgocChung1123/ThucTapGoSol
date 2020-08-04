using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Com.Gosol.CMS.Web.Helper
{
    public class MessageHelper
    {
        public static void Message(ref HtmlGenericControl messageDiv, MessageType type, String message)
        {
            if (type == MessageType.Error)
            {
                messageDiv.Attributes["class"] = "alert alert-error";
                messageDiv.Attributes.CssStyle["display"] = "block";
                messageDiv.InnerHtml = message;
            }
            else if (type == MessageType.Success)
            {
                messageDiv.Attributes["class"] = "alert alert-success";
                messageDiv.Attributes.CssStyle["display"] = "block";
                messageDiv.InnerHtml = message;
            }
        }
    }
}