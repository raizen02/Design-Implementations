using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using FliAuthLib.FliAuthService;
using FliAuthLib;

namespace FliAuthLib
{
    public class AuthenticatedLoginBase : System.Web.UI.Page
    {
        private string _pristrAuthSiteUrl;
        private string _pristrDefaultUrl;
        private ServiceError _priseError;

        public delegate void UserAuthenticatedHandler(object sender, WebUser user);
        public event UserAuthenticatedHandler UserAuthenticateCompleted;

        private void LoadParams()
        {
            _pristrAuthSiteUrl = ConfigurationManager.AppSettings[AppConstants.Urls.AUTH_SITE];
            _pristrDefaultUrl = ConfigurationManager.AppSettings[AppConstants.Urls.DEFAULT_URL];
        }

        private string ReturnUrl
        {
            get { return Request.QueryString[AppConstants.UrlParams.RETURN_URL]; }
        }

        protected ServiceError LoginError
        {
            get
            {
                return _priseError;
            }
        }

        protected bool LoginUser(string _parstrUsername, string _parstrPassword)
        {
            //Autheticate User
            _priseError = new ServiceError();
            WebUser _dimwuUser = new AuthService().Authenticate(_parstrUsername, _parstrPassword, Request.ServerVariables["REMOTE_HOST"], Request.ServerVariables["REMOTE_ADDR"], out _priseError);

            //Get Return Url from Query
            string _dimstrReturnUrl = ReturnUrl;

            if (_dimwuUser != null)
            {
                if (UserAuthenticateCompleted != null)
                {
                    UserAuthenticateCompleted(this, _dimwuUser);
                }

                LoadParams();

                if (string.IsNullOrEmpty(_dimstrReturnUrl))
                {
                    _dimstrReturnUrl = Util.RelativeToAbsolutePath(_pristrDefaultUrl);
                }

                //Redirect to AuthSite  with Token for ServerLogin
                //ReturnUrl to return to origianl url where login was triggered
                string _strRedirectTo = string.Format(_pristrAuthSiteUrl, HttpUtility.UrlEncode(_dimstrReturnUrl));
                _strRedirectTo = Util.AppendedQueryString(Util.RelativeToAbsolutePath(_strRedirectTo), AppConstants.UrlParams.TOKEN, _dimwuUser.Token);

                Response.Redirect(_strRedirectTo);

                return true;
            }

            return false;
        }

    }
}