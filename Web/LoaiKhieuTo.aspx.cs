using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class LoaiKhieuTo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiKhieuTo, AccessLevel.Read))
            {
                Response.Redirect("~");
            }

            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiKhieuTo, AccessLevel.Create))
            {
                themLoaiKT.Attributes["class"] += " disable";
                themLoaiKT.Attributes["onclick"] = "return false;";
            }
            //MenuHelper.CreateSideMenu(ltrSideMenu, "Danh mục");
        }

        protected string GetDeleteDeny()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiKhieuTo, AccessLevel.Delete)) return "false";
            else return "true";
        }

        protected string GetCreateDeny()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiKhieuTo, AccessLevel.Create)) return "false";
            else return "true";
        }

        protected string GetEditDeny()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucLoaiKhieuTo, AccessLevel.Edit)) return "false";
            else return "true";
        }
    }
}