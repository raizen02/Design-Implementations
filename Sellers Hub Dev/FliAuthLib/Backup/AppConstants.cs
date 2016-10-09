using System;
using System.Collections.Generic;
using System.Web;


namespace FliAuthLib
{ 
   
public class AppConstants
{
    public class UrlParams
    {
        public const string TOKEN = "Token";
        public const string ACTION = "Action";
        public const string REQUEST_ID = "RequestId";
        public const string RETURN_URL = "ReturnUrl";
    }

    public class ParamValues
    {
        public const string LOGOUT = "Logout";
    }

    public class Cookies
    {
        public const string AUTH_COOKIE = "AUTH_COOKIE";
    }

    public class Urls
    {
        public const string AUTH_SITE = "AUTH_SITE_URL";
        public const string LOGIN = "LOGIN_URL";
        public const string DEFAULT_URL = "DEFAULT_URL";
    }

    public class AppCodes
    {
        public const string A0002 = "A0002";
    }
}
}