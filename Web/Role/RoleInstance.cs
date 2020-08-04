using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Gosol.CMS.Web.Role
{
    public enum RoleEnum
    {
        ChuyenVien = 3,
        LanhDaoPhong = 2,
        LanhDaoDonVi = 1
    }

    public class RoleInstance
    {
        private static readonly RoleInstance _instance = new RoleInstance();
        public static RoleInstance Instance
        {
            get { return _instance; }
        }

        public bool IsLanhDao(int userID)
        {
            if (IdentityHelper.GetRoleID() == (int)RoleEnum.LanhDaoDonVi) return true;
            else return false;
        }

        public bool IsTruongPhong(int userID)
        {
            if (IdentityHelper.GetRoleID() == (int)RoleEnum.LanhDaoPhong) return true;
            else return false;
        }

        public bool IsChuyenVien(int userID)
        {
            if (IdentityHelper.GetRoleID() == (int)RoleEnum.ChuyenVien) return true;
            else return false;
        }

        public string GetRoleName(int userID)
        {
            return IdentityHelper.GetRoleName();
        }
    }
}