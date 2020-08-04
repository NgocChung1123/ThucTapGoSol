namespace Com.Gosol.CMS.Security
{
    using System;
    using System.Collections;
    using System.Security.Principal;

    public class AccessControlPrincipal : IPrincipal
    {
        private Hashtable _accessGroups;
        private Hashtable _acl;
        private Hashtable _aclType;
        private AccessControlIdentity _userIdentity;

        private AccessControlPrincipal()
        {
            throw new AccessControlExceptions("CanNotCreateClass");
        }

        private AccessControlPrincipal(Hashtable accessGroups, Hashtable acl, Hashtable aclType, Hashtable userInfo)
        {
            this._accessGroups = accessGroups;
            this._acl = acl;
            this._aclType = aclType;
            this._userIdentity = AccessControlIdentity.CreateInstance(userInfo);
        }

        public static AccessControlPrincipal CreateInstance(Hashtable accessGroups, Hashtable acl, Hashtable aclType, Hashtable userInfo)
        {
            return new AccessControlPrincipal(accessGroups, acl, aclType, userInfo);
        }

        public bool HasPermission(EntityType entityType, AccessLevel accessLevel)
        {
            string key = Convert.ToInt32(entityType).ToString();
            if (this._aclType.ContainsKey(key))
            {
                int num = Convert.ToInt32(accessLevel);
                return (num == (num & ((int) this._aclType[key])));
            }
            return (accessLevel == AccessLevel.NoAccess);
        }

        public bool HasPermission(object entityType, AccessLevel accessLevel)
        {
            if (entityType.ToString().Equals("?"))
            {
                return false;
            }
            if (entityType.ToString().Equals("*"))
            {
                return true;
            }
            string key = Convert.ToInt32(entityType).ToString();
            if (this._aclType.ContainsKey(key))
            {
                int num = Convert.ToInt32(accessLevel);
                return (num == (num & ((int) this._aclType[key])));
            }
            return (accessLevel == AccessLevel.NoAccess);
        }

        public bool HasPermission(object entityType, string accessLevel)
        {
            if (entityType.ToString().Equals("?") || accessLevel.Equals("?"))
            {
                return false;
            }
            if (entityType.ToString().Equals("*") || accessLevel.Equals("*"))
            {
                return true;
            }
            bool flag = false;
            if (Enum.IsDefined(typeof(AccessLevel), accessLevel))
            {
                AccessLevel level = (AccessLevel) Enum.Parse(typeof(AccessLevel), accessLevel);
                flag = AccessControl.User.HasPermission(entityType, level);
            }
            return flag;
        }

        public bool HasPermission(object entityType, int entityID, AccessLevel accessLevel)
        {
            if (entityType.ToString().Equals("?"))
            {
                return false;
            }
            if (entityType.ToString().Equals("*"))
            {
                return true;
            }
            string key = Convert.ToInt32(entityType).ToString() + "$" + entityID.ToString();
            if (this._acl.ContainsKey(key))
            {
                int num = Convert.ToInt32(accessLevel);
                return (num == (num & ((int) this._acl[key])));
            }
            return (accessLevel == AccessLevel.NoAccess);
        }

        public bool IsInRole(int roleID)
        {
            return this._accessGroups.ContainsKey(roleID);
        }

        public bool IsInRole(string roleName)
        {
            return this._accessGroups.ContainsValue(roleName);
        }

        public IIdentity Identity
        {
            get
            {
                return this._userIdentity;
            }
        }
    }
}
