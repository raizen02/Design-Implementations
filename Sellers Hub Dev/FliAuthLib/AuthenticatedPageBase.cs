using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using FliAuthLib.FliAuthService;
using FliAuthLib;

namespace FliAuthLib
{
    public class AuthenticatedPageBase : System.Web.UI.Page
    {
        protected WebUser CurrentUser;

        //protected string Token
        //{
        //    get
        //    {
        //        return Request.QueryString[AppConstants.UrlParams.TOKEN];
        //    }
        //}

        //protected string RequestId
        //{
        //    get
        //    {
        //        return Request.QueryString[AppConstants.UrlParams.REQUEST_ID];
        //    }
        //}

        //protected string Action
        //{
        //    get
        //    {
        //        return Request.QueryString[AppConstants.UrlParams.ACTION];
        //    }
        //}

        //protected string _pristrAuthSiteUrl;

        //protected string _pristrLoginUrl;

        //private void LoadParams()
        //{
        //    _pristrAuthSiteUrl = ConfigurationManager.AppSettings[AppConstants.Urls.AUTH_SITE];
        //    _pristrLoginUrl = ConfigurationManager.AppSettings[AppConstants.Urls.LOGIN];
        //}

        //protected override void OnLoad(EventArgs e)
        //{
        //    LoadParams();

        //    if (IsPostBack)
        //    {
        //        //Checks if token is still logged in
        //        UserStatus _dimusStatus = new AuthService().ValidateTokenAndRequest(Token, RequestId);

        //        if (_dimusStatus.UserLoggedIn == false)
        //        {
        //            //Redirect To Login page
        //            RedirectToLogin();
        //            return;
        //        }

        //        CurrentUser = new AuthService().GetUserByToken(Token);

        //        base.OnLoad(e);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(RequestId))
        //    {
        //        //Redirect to Auth site
        //        //Get Request Id
        //        RedirectToAuthSite();
        //        return;
        //    }
        //    else
        //    {
        //        //Validate Token and Request
        //        //Verify Token and Request Id if from AuthSite
        //        //Redirect if neccessary
        //        VerifyTokenAndRequest();
        //    }

        //    CurrentUser = new AuthService().GetUserByToken(Token);

        //    base.OnLoad(e);
        //}

        //private void VerifyTokenAndRequest()
        //{
        //    UserStatus _dimusStatus = new AuthService().ValidateTokenAndRequest(Token, RequestId);

        //    if (_dimusStatus.UserLoggedIn == false)
        //    {
        //        //Redirect To Login page
        //        RedirectToLogin();
        //        return;
        //    }

        //    if (_dimusStatus.RequestIdValid == false)
        //    {
        //        RedirectToAuthSite();
        //        return;
        //    }
        //}

        protected virtual void RedirectToLogin()
        {
            //string _dimstrReturnUrl = Request.Url.AbsoluteUri;

            ////Remove token and request id
            ////token is expired
            //_dimstrReturnUrl = Util.RemoveQueryString(_dimstrReturnUrl, AppConstants.UrlParams.REQUEST_ID);
            //_dimstrReturnUrl = Util.RemoveQueryString(_dimstrReturnUrl, AppConstants.UrlParams.ACTION);
            //_dimstrReturnUrl = Util.RemoveQueryString(_dimstrReturnUrl, AppConstants.UrlParams.TOKEN);

            ////Redirect to login + return to original after login
            //string _strRedirect = Util.AppendedQueryString(Util.RelativeToAbsolutePath(_pristrLoginUrl), AppConstants.UrlParams.RETURN_URL, HttpUtility.UrlEncode(_dimstrReturnUrl));

            //Response.Redirect(_strRedirect);
        }

        private void RedirectToAuthSite()
        {
            //string _dimstrUrl = Request.Url.AbsoluteUri.ToLower();

            ////Clean up all current QueryString parameters before redirecting to SSO-DEV site
            //_dimstrUrl = Util.RemoveQueryString(_dimstrUrl, AppConstants.UrlParams.REQUEST_ID);
            ////_strUrl = Util.RemoveQueryString(_strUrl, AppConstants.UrlParams.ACTION);
            //_dimstrUrl = Util.RemoveQueryString(_dimstrUrl, AppConstants.UrlParams.TOKEN);

            //string _dimstrAuthSiteUrl = string.Format(_pristrAuthSiteUrl, HttpUtility.UrlEncode(_dimstrUrl));

            ////Redirect to SSO-DEV site
            //Response.Redirect(_dimstrAuthSiteUrl);
        }
    }
}