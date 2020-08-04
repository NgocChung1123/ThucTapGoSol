using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    public static class MenuHelper
    {
        public static void CreateSideMenu(Literal menuControl, String rootMenuName)
        {
            MenuInfo rootMenu = new DAL.Menu().GetByName(rootMenuName);
            if (menuControl.Text == String.Empty)
            {
                menuControl.Text += "<h2 class='ico_sys'>" + rootMenu.TenMenu + "</h2>";
                if (rootMenu != null)
                {
                    CreateChildSideMenu(ref menuControl, rootMenu, 0);
                }
            }
        }

        private static void CreateChildSideMenu(ref Literal menuControl, MenuInfo rootMenu, int level)
        {
            bool hasChild = false;
            List<MenuInfo> childMenus = new DAL.Menu().GetChilds(rootMenu.MenuID).ToList();
            if (childMenus.Count > 0)
            {
                hasChild = true;
            }
            if (hasChild)
            {
                if (level == 0)
                {
                    menuControl.Text += "<ul id='menu'>";
                }
                else
                {
                    if (level < 2)
                    {
                        menuControl.Text += "<li><a href='" + rootMenu.MenuUrl + "' class='icon_posts'>" + rootMenu.TenMenu + "</a>";
                        menuControl.Text += "</li>";
                    }
                    else
                    {
                        menuControl.Text += "<li><a href='" + rootMenu.MenuUrl + "'>" + rootMenu.TenMenu + "</a>";
                    }
                    menuControl.Text += "<ul>";
                }
            }
            else
            {
                if (level < 2)
                {
                    menuControl.Text += "<li><a href='" + rootMenu.MenuUrl + "' class='icon_page'>" + rootMenu.TenMenu + "</a>";
                    menuControl.Text += "</li>";
                }
                else
                {
                    menuControl.Text += "<li><a href='" + rootMenu.MenuUrl +  "'>" + rootMenu.TenMenu + "</a>";
                }
            }
            level++;
            foreach (MenuInfo childMenu in childMenus)
            {
                CreateChildSideMenu(ref menuControl, childMenu, level);
            }
            if (hasChild)
            {
                menuControl.Text += "</ul>";
            }
        }
    }
}