using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Gosol.CMS.Model;
using System.Data.SqlClient;
using System.Data;
using Com.Gosol.CMS.Utility;
using System.Reflection;

namespace Com.Gosol.CMS.DAL
{
    public class Menu
    {
        #region Database query string

        private const string GET_ALL = @"Menu_GetAll";
        private const string GET_BY_ID = @"Menu_GetByID";
        private const string DELETE = @"Menu_Delete";
        private const string UPDATE = @"Menu_Update";
        private const string INSERT = @"Menu_Insert";

        private const string GET_PARENTS = "Menu_GetParents";
        private const string GET_CHILDS = "Menu_GetChilds";
        private const string GET_BY_NAME = "Menu_GetByName";
       
        #endregion

        #region Parameters

        private const string PARM_MENU_ID = "@MenuID";
        private const string PARM_TEN_MENU = "@TenMenu";
        private const string PARM_MENU_URL = "@MenuUrl";
        private const string PARM_MENU_CHA_ID = "@MenuChaID";
        private const string PARM_CHUC_NANG_ID = "@ChucNangID";
        
        #endregion

        private MenuInfo GetData(SqlDataReader rdr)
        {
            MenuInfo menuInfo = new MenuInfo();

            menuInfo.ChucNangID = Utils.ConvertToInt32(rdr["ChucNangID"], 0);
            menuInfo.ImageUrl = Utils.GetString(rdr["ImageUrl"], String.Empty);
            menuInfo.MenuChaID = Utils.ConvertToInt32(rdr["MenuChaID"], 0);
            menuInfo.MenuID = Utils.ConvertToInt32(rdr["MenuID"], 0);
            menuInfo.MenuUrl = Utils.GetString(rdr["MenuUrl"], String.Empty);
            menuInfo.TenMenu = Utils.GetString(rdr["TenMenu"], String.Empty);            

            return menuInfo;
        }

        
        public IList<MenuInfo> GetParents()
        {
            IList<MenuInfo> menus = new List<MenuInfo>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_PARENTS, null))
            {
                while (dr.Read())
                {
                    MenuInfo menuInfo = GetData(dr);
                    menus.Add(menuInfo);
                }
                dr.Close();
            }
            return menus;
        }

        public IList<MenuInfo> GetChilds(int menuChaID)
        {
            IList<MenuInfo> menus = new List<MenuInfo>();
            SqlParameter parm = new SqlParameter(PARM_MENU_CHA_ID, SqlDbType.Int);
            parm.Value = menuChaID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_CHILDS, parm))
            {
                while (dr.Read())
                {
                    MenuInfo menuInfo = GetData(dr);
                    menus.Add(menuInfo);
                }
                dr.Close();
            }
            return menus;
        }

        public MenuInfo GetMenuByID(int menuID)
        {
            MenuInfo menuInfo = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(PARM_MENU_ID, SqlDbType.Int)
            };
            parameters[0].Value = menuID;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_ID, parameters))
            {
                if (dr.Read())
                {
                    menuInfo = GetData(dr);
                }
                dr.Close();
            }
            return menuInfo;
        }

        public MenuInfo GetByName(String menuName)
        {
            MenuInfo menuInfo = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(PARM_TEN_MENU, SqlDbType.NVarChar, 50)
            };
            parameters[0].Value = menuName;
            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.CONN_BACKEND, CommandType.StoredProcedure, GET_BY_NAME, parameters))
            {
                if (dr.Read())
                {
                    menuInfo = GetData(dr);
                }
                dr.Close();
            }
            return menuInfo;
        }
    }
}
