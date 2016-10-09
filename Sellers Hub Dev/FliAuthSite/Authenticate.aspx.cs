using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Authenticate : System.Web.UI.Page
{
    private string _pristrToken = "";
    private string _pristrAction = "";
    private string _pristrReturnUrl = "";

    private void LoadRequestParams()
    {
        _pristrToken = Request.Params[AppConstants.UrlParams.TOKEN];
        _pristrAction = Request.Params[AppConstants.UrlParams.ACTION];
        _pristrReturnUrl = Request.Params[AppConstants.UrlParams.RETURN_URL];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadRequestParams();

        if (string.Equals(_pristrAction, AppConstants.ParamValues.LOGOUT, StringComparison.OrdinalIgnoreCase))
        {
            //Logout
            LogoutUser();
        }
        else
        {
            if (_pristrToken != null)
            {
                //Set auth cookie here
                SetAuthCookie(AppConstants.Cookies.AUTH_COOKIE, _pristrToken);

                //Redirect to the original site request
                _pristrReturnUrl = Util.AppendedQueryString(_pristrReturnUrl, AppConstants.UrlParams.TOKEN, _pristrToken);
                Redirect(_pristrReturnUrl);
            }
            else
            {
                //User Token is not available in URL. 
                //Check whether the authentication Cookie is available in the Request
                HttpCookie _cooAuthCookie = Request.Cookies[AppConstants.Cookies.AUTH_COOKIE];

                if (_cooAuthCookie != null)                
                {
                    //Cookie available
                    //Check Cookie status
                    CheckCookieStatus(_cooAuthCookie);
                }
                else
                {
                    //no cookie available, user is logged out of system
                    //mark user as logged out
                    if (!string.IsNullOrEmpty(_pristrToken))
                    {
                        HttpContext.Current.Application.Remove(_pristrToken);
                    }

                    //Return
                    Redirect(_pristrReturnUrl);
                }
            }
        }
    }

    private void LogoutUser()
    {
        //This is a logout request. So, remove the authentication Cookie from the response
        if (_pristrToken != null)
        {
            HttpCookie Cookie = Request.Cookies[AppConstants.Cookies.AUTH_COOKIE];

            if (Cookie.Value.Contains(_pristrToken))
            {
                RemoveCookie(Cookie);
            }
           
            //Also, mark the user at the application scope as null
            HttpContext.Current.Application.Remove(_pristrToken);
        }

        //Redirect user to the desired location
        Redirect(_pristrReturnUrl);
    }

    private void SetAuthCookie(string CookieName, string Token)
    {
        HttpCookie _cooAuthCookie = new HttpCookie(CookieName);
        _cooAuthCookie.Value = Token;
        Response.Cookies.Add(_cooAuthCookie);
    }

    private void Redirect(string Url)
    {
        //Generate and Appent RequestId to Return Url to validate if RequestId is from SSO-DEV site or not
        string _strReqId = Util.GetGuidHash();
        string _strRedirectUrl = Util.AppendedQueryString(Url, AppConstants.UrlParams.REQUEST_ID, _strReqId);

        //Save the RequestId in the Application for later verification
        Application[_strReqId] = _strReqId;

        //Redirect
        Response.Redirect(_strRedirectUrl);
    }

    private void CheckCookieStatus(HttpCookie Cookie)
    {
        //Read Cookie value  
        string _strCookieToken = Cookie.Value;
        WebUser user = (WebUser)HttpContext.Current.Application[_strCookieToken];

        if (user == null)
        {
            //ClearCookie
            RemoveCookie(Cookie);

            //Clear token
            if (!string.IsNullOrEmpty(_pristrToken))
            {
                HttpContext.Current.Application.Remove(_pristrToken);
            }

            //Redirect to the site URL
            Redirect(_pristrReturnUrl);
        }

        if (user.ExpiresOn.CompareTo(DateTime.Now) < 0) //Cookie Expired
        {
            //ClearCookie
            RemoveCookie(Cookie);

            //Clear token
            if (!string.IsNullOrEmpty(_pristrToken))
            {
                HttpContext.Current.Application.Remove(_pristrToken);
            }

            //Redirect to the site URL
            Redirect(_pristrReturnUrl);
        }

        if (!string.IsNullOrEmpty(_strCookieToken) && !_pristrReturnUrl.Contains(_strCookieToken))
        {
            _pristrReturnUrl = Util.AppendedQueryString(_pristrReturnUrl, AppConstants.UrlParams.TOKEN, _strCookieToken);
            Redirect(_pristrReturnUrl);
        }
        else
        {
            //Cookie is expired or Token is not available. So, redirect user to the ReturnUrl.
            Redirect(_pristrReturnUrl);
        }
    }

    private void RemoveCookie(HttpCookie Cookie)
    {
        Response.Cookies.Remove(Cookie.Name);

        HttpCookie myCookie = new HttpCookie(Cookie.Name);
        myCookie.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(myCookie);
    }
}