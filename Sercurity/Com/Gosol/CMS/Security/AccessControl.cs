namespace Com.Gosol.CMS.Security
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Security;

    public sealed class AccessControl
    {
        private AccessControl()
        {
            throw new AccessControlExceptions("CanNotCreateClass");
        }

        public static void SignIn(string username, string password)
        {
            if (!IsLoggedIn)
            {
                int num;
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5.ToString());
                if (AccessControlData.VerifyUser(username, password, out num))
                {
                    Hashtable hashtable;
                    Hashtable hashtable2;
                    Hashtable hashtable3;
                    Hashtable hashtable4;
                    AccessControlData.RequestAccessRight(num, out hashtable, out hashtable2);
                    AccessControlData.RequestUserInfo(num, out hashtable4, out hashtable3);
                    AccessControlPrincipal principal = AccessControlPrincipal.CreateInstance(hashtable3, hashtable, hashtable2, hashtable4);
                    HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4", principal);

                }
            }
        }

        public static void SignOut()
        {
            HttpContext.Current.Session.Remove("USER$DA31A175C7679319BFFEDF3EF282D1F4");
        }

        public static void LoockScreen()
        {
            //HttpContext.Current.Session.Remove("USER$DA31A175C7679319BFFEDF3EF282D1F4");
        }

        public static bool IsLoggedIn
        {
            get
            {
                return (User != null);
            }
        }

        public static AccessControlPrincipal User
        {
            get
            {
                return (HttpContext.Current.Session["USER$DA31A175C7679319BFFEDF3EF282D1F4"] as AccessControlPrincipal);
            }
        }

        // Login Frontend

        public static void MemberSignIn(string username, string password)
        {
            if (!MemberIsLoggedIn)
            {
                int num;
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5.ToString());
                if (AccessControlData.VerifyUser(username, password, out num))
                {
                    Hashtable hashtable;
                    Hashtable hashtable2;
                    Hashtable hashtable3;
                    Hashtable hashtable4;
                    AccessControlData.RequestAccessRight(num, out hashtable, out hashtable2);
                    AccessControlData.RequestUserInfo(num, out hashtable4, out hashtable3);
                    AccessControlPrincipal principal = AccessControlPrincipal.CreateInstance(hashtable3, hashtable, hashtable2, hashtable4);
                    HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4CUONGLB", principal);

                }
            }
        }
        public static void MemberSignInNow(string username, string password)
        {
            if (!MemberIsLoggedIn)
            {
                int num;
                //password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5.ToString());
                if (AccessControlData.VerifyUser(username, password, out num))
                {
                    Hashtable hashtable;
                    Hashtable hashtable2;
                    Hashtable hashtable3;
                    Hashtable hashtable4;
                    AccessControlData.RequestAccessRight(num, out hashtable, out hashtable2);
                    AccessControlData.RequestUserInfo(num, out hashtable4, out hashtable3);
                    AccessControlPrincipal principal = AccessControlPrincipal.CreateInstance(hashtable3, hashtable, hashtable2, hashtable4);
                    HttpContext.Current.Session.Add("USER$DA31A175C7679319BFFEDF3EF282D1F4CUONGLB", principal);

                }
            }
        }
        public static void MemberSignOut()
        {
            HttpContext.Current.Session.Remove("USER$DA31A175C7679319BFFEDF3EF282D1F4CUONGLB");
            HttpContext.Current.Session.Remove("USER$DA31A175C7679319BFFEDF3EF282D1F4TENCANBO");
        }

        public static void MemberLoockScreen()
        {
            //HttpContext.Current.Session.Remove("USER$DA31A175C7679319BFFEDF3EF282D1F4");
        }

        public static bool MemberIsLoggedIn
        {
            get
            {
                return (MemberUser != null);
            }
        }

        public static AccessControlPrincipal MemberUser
        {
            get
            {
                return (HttpContext.Current.Session["USER$DA31A175C7679319BFFEDF3EF282D1F4CUONGLB"] as AccessControlPrincipal);
            }
        }
    }
}
