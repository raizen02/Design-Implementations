using System;
using System.Collections.Generic;
using System.Web;


public class AppConstants
{
    public class UrlParams
    {
        public const string TOKEN = "Token";
        public const string ACTION = "Action";
        public const string REQUEST_ID = "RequestId";
        public const string RETURN_URL = "ReturnUrl";
        public const string CONFIRMATION_ID = "ConfirmId";
    }

    public class ParamValues
    {
        public const string LOGOUT = "Logout";
        public const string CONFIRM = "Confirm";
    }

    public class Cookies
    {
        public const string AUTH_COOKIE = "AUTH_COOKIE";
    }

    public class UserLevels
    {
        public const string ADMIN = "00000";
        public const string USER = "00001";
        public const string MANAGER = "00002";
    }

    public class AppCodes
    {
        public const string A0002 = "A0002";
    }

    public class Urls
    {
        public const string CONFIRM_LOGIN_LINK = "CONFIRM_LOGIN_LINK";
    }
}