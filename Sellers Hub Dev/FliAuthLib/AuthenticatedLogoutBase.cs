using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using FliAuthLib;

namespace FliAuthLib
{
    public class AuthenticatedLogoutBase : AuthenticatedPageBase
    {
        //public delegate void UserLoggedOutHandler(object sender, System.EventArgs e);
        //public event UserLoggedOutHandler UserLoggedOut;

        //protected override void RedirectToLogin()
        //{
        //    Response.Redirect(_pristrLoginUrl);
        //}

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    if (string.Equals(this.Action, AppConstants.ParamValues.LOGOUT, StringComparison.OrdinalIgnoreCase))
        //    {
        //        //Logout user
        //        LogoutUser();
        //    }
        //}

        //protected void LogoutUser()
        //{
        //    if (UserLoggedOut != null)
        //    {
        //        UserLoggedOut(this, new EventArgs());
        //    }

        //    string _dimstrUrl = Util.RelativeToAbsolutePath(_pristrLoginUrl);

        //    string _dimstrAuthSiteUrl = string.Format(Util.RelativeToAbsolutePath(_pristrAuthSiteUrl), HttpUtility.UrlEncode(_dimstrUrl));
        //    _dimstrAuthSiteUrl = Util.AppendedQueryString(_dimstrAuthSiteUrl, AppConstants.UrlParams.ACTION, AppConstants.ParamValues.LOGOUT);
        //    _dimstrAuthSiteUrl = Util.AppendedQueryString(_dimstrAuthSiteUrl, AppConstants.UrlParams.TOKEN, Token);

        //    //Redirect to SSO-DEV site
        //    Response.Redirect(_dimstrAuthSiteUrl);
        //}
    }
}