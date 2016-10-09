//**************************************************************
//Programmer Name		: John Alexander M.Baltazar
//Date Created			: 2014.02.27
//Program Name           : AuthenticationService
//Program Description    : Web service for authentication for individual sites
//Remarks                : DEV - 2013.02.05 | PROD - 2013.07.08
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# JYYXXXXX
//Date Updated           : 2014.05.14
//Changes                : Change storage of web user object from HttpRuntime.Cache to HttpContext.Current.Application
//Remarks                : DEV - 2014.05.14 | PROD - 2014.05.14
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# JYYXXXXX
//Date Updated           : 2015.04.22
//Changes                : Added Web User Properties
//Remarks                : DEV - 2015.04.22 | PROD - n/a
//**************************************************************

using System;
using System.Web;

using System.Web.Services;
using System.Web.Script.Services;
/// <summary>
/// Summary description for AuthenticationService
/// </summary>
[WebService(Namespace = "https://kiosk.filinvest.com.ph/SSO-DEV/services")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//[System.Web.Script.Services.ScriptService]
public class AuthenticationService : System.Web.Services.WebService
{
    public AuthenticationService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    
    [WebMethod]
    public WebUser Authenticate(string _parstrUserName, string _parstrPassword, string _parstrComputerName, string _parstrIpAddr, out ServiceError _parseError)
    {       
        return AuthenticateUser(_parstrUserName, _parstrPassword, _parstrComputerName, _parstrIpAddr, false, out _parseError);
    }

    private WebUser AuthenticateUser(string _parstrUserName, string _parstrPassword, string _parstrComputerName, string _parstrIpAddr, bool _parblnRememberSession, out ServiceError _parseError)
    {
        UserManager _dimumUser = new UserManager();
        
        _parseError = new ServiceError();

        WebUser _dimwuUser = _dimumUser.AuthenticateUser(_parstrUserName, _parstrPassword, AppConstants.AppCodes.A0002,
            _parstrComputerName, _parstrIpAddr);

        if (_dimwuUser == null)
        {
            _parseError.ErrorNumber = _dimumUser.LastError.ErrorNumber;
            _parseError.ErrorMessage = _dimumUser.LastError.ErrorMessage;
        }
        else
        {
            //Store the user object in the Application scope, to mark the user as logged onto the SSO site
            //Along with the cookie, this is a supportive way to trak user's logged in status
            //In order to track a user as logged onto the SSO site user token has to be presented in the cookie as well as 
            //he/she has to be presented in teh Application scope
            //HttpContext.Current.Application[user.Token] = user;
            _dimwuUser.ExpiresOn = Util.GetExpirationBasedOnConfig();
            _dimwuUser.Username = Crypto.Encrypt(_dimwuUser.Username, Config.Crypto.Key);
            _dimwuUser.SellerCode = Crypto.Encrypt(_dimwuUser.SellerCode, Config.Crypto.Key);
            _dimwuUser.UserLevel = Crypto.Encrypt(_dimwuUser.UserLevel, Config.Crypto.Key);
            //_dimwuUser.SellerAllocation = Crypto.Encrypt(_dimwuUser.SellerAllocation, Config.Crypto.Key);
            //_dimwuUser.SellerSalesChannel = Crypto.Encrypt(_dimwuUser.SellerSalesChannel, Config.Crypto.Key);
            //_dimwuUser.SellerClassification = Crypto.Encrypt(_dimwuUser.SellerClassification, Config.Crypto.Key);
            //_dimwuUser.SellerLevel = Crypto.Encrypt(_dimwuUser.SellerLevel, Config.Crypto.Key);
            //_dimwuUser.SellerPosition = Crypto.Encrypt(_dimwuUser.SellerPosition, Config.Crypto.Key);
            _dimwuUser.LoggedFromApp = false;

            //Remember Session
            if (_parblnRememberSession)
            {
                string _dimstrSessionId = Util.GetGuidHash();

                _dimumUser.OpenDbConnection();                
                while (_dimumUser.AddSessionId(_parstrUserName, _dimstrSessionId) == false)
                {
                    _dimstrSessionId = Util.GetGuidHash();
                }
                _dimumUser.CloseConnection();

                //Adds a salt on session id then encrypt
                //This will be saved on cookies
                _dimwuUser.SessionId = Util.EncodeSession(_dimstrSessionId); //Crypto.Encrypt(Config.PreKey.AccountSessionId + _dimstrSessionId, Config.Crypto.Key);
            }

            HttpContext.Current.Application[_dimwuUser.Token] = _dimwuUser;
            //HttpRuntime.Cache.Insert(_dimwuUser.Token, _dimwuUser, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            //    (Config.CookieSettings.SlidingExpiration ? new TimeSpan(0, Config.CookieSettings.CookieTimeoutInMinutes, 0) : Cache.NoSlidingExpiration),
            //    CacheItemPriority.Normal, null);
        }

        return _dimwuUser;
    }

    /// <summary>
    /// Retrieves user by Token
    /// </summary>
    /// <param name="_parstrToken"></param>
    /// <returns></returns>
    [WebMethod]
    public WebUser GetUserByToken(string _parstrToken)
    {

        if (HttpContext.Current.Application[_parstrToken] == null)
        {
            return null;
        }

        return HttpContext.Current.Application[_parstrToken] as WebUser;

        // if (HttpRuntime.Cache[_parstrToken] == null)
        //{
        //    return null;
        //}

        //return HttpRuntime.Cache[_parstrToken] as WebUser;
    }

    /// <summary>
    /// Determines whether user is still logged onto the site
    /// </summary>
    /// <param name="_parstrToken"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsUserLoggedIn(string _parstrToken)
    {
        if (!string.IsNullOrEmpty(_parstrToken))
        {
            WebUser user = (WebUser)HttpContext.Current.Application[_parstrToken];
                        
            if (user != null)
            {
                if (user.ExpiresOn.CompareTo(DateTime.Now) < 0) //User session Expired
                {
                    HttpContext.Current.Application.Remove(_parstrToken);
                    return false;
                }

                if (Config.CookieSettings.SlidingExpiration)
                {
                    user.ExpiresOn = Util.GetExpirationBasedOnConfig();
                }

                return true;
            }            
        }

        return false;
    }

    /// <summary>
    /// Determines whether the current request is valid or not
    /// </summary>
    /// <param name="_parstrRedirectId"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsValidRequest(string _parstrRedirectId)
    {
        if ((string)Application[_parstrRedirectId] == _parstrRedirectId)
        {
            Application[_parstrRedirectId] = null;
            return true;
        }
        return false;
    }

    [WebMethod]
    public UserStatus ValidateTokenAndRequest(string _parstrToken, string _parstrRequestId)
    {
        UserStatus _dimusUserStatus = new UserStatus();

        if (!string.IsNullOrEmpty(_parstrRequestId))
        {
            if ((string)Application[_parstrRequestId] == _parstrRequestId)
            {
                Application[_parstrRequestId] = null;
                _dimusUserStatus.RequestIdValid = true;
            }
        }

        _dimusUserStatus.UserLoggedIn = IsUserLoggedIn(_parstrToken);

        return _dimusUserStatus;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public WebUserLogInfo AuthenticateApp(string _parstrUserName, string _parstrPassword, bool _parblnIsRemember)
    {
        ServiceError _parseError = new ServiceError();
        WebUser _dimwuUser = AuthenticateUser(_parstrUserName, _parstrPassword, HttpContext.Current.Request.ServerVariables["REMOTE_HOST"], HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], _parblnIsRemember, out _parseError);

        WebUserLogInfo _dimwuiLogInfo = new WebUserLogInfo();

        if (_dimwuUser != null)
        {
            _dimwuUser.LoggedFromApp = true;
            _dimwuiLogInfo.Username = _dimwuUser.Username;
            _dimwuiLogInfo.UserLevel = _dimwuUser.UserLevel;
            _dimwuiLogInfo.ExpiresOn = _dimwuUser.ExpiresOn;
            _dimwuiLogInfo.SellerCode = _dimwuUser.SellerCode;
            _dimwuiLogInfo.Token = _dimwuUser.Token;
            _dimwuiLogInfo.LoggedFromApp = true;
            _dimwuiLogInfo.SessionId = _dimwuUser.SessionId;
            HttpContext.Current.Application[_dimwuUser.Token] = _dimwuUser;
        }

        _dimwuiLogInfo.ErrorNumber = _parseError.ErrorNumber;
        _dimwuiLogInfo.ErrorMessage = _parseError.ErrorMessage;

        return _dimwuiLogInfo;
    }

    [WebMethod]
    public WebUserLogInfo AuthenticateAppSession(string _parstrSessionId, bool _parstrIsEncoded)
    {
        WebUserLogInfo _dimwuiLogInfo = new WebUserLogInfo();

        string _dimstrSessionId = "";

        if (_parstrIsEncoded)
        {
            _dimstrSessionId = Util.DecodeSession(_parstrSessionId);
        }
        else
        {
            _dimstrSessionId = _parstrSessionId;
        }

        if (_dimstrSessionId == "")
        {
            _dimwuiLogInfo.ErrorNumber = 5000;
            _dimwuiLogInfo.ErrorMessage = "No Session.";
            return _dimwuiLogInfo;
        }
                
        UserManager _dimumUser = new UserManager();
        WebUser _dimwuUser = _dimumUser.AuthenticateSession(_dimstrSessionId, AppConstants.AppCodes.A0002, HttpContext.Current.Request.ServerVariables["REMOTE_HOST"], HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

        if (_dimwuUser == null)
        {
            _dimwuiLogInfo.ErrorNumber = _dimumUser.LastError.ErrorNumber;
            _dimwuiLogInfo.ErrorMessage = _dimumUser.LastError.ErrorMessage;
        }
        else
        {
            _dimwuUser.ExpiresOn = Util.GetExpirationBasedOnConfig();
            _dimwuUser.Username = Crypto.Encrypt(_dimwuUser.Username, Config.Crypto.Key);
            _dimwuUser.SellerCode = Crypto.Encrypt(_dimwuUser.SellerCode, Config.Crypto.Key);
            _dimwuUser.UserLevel = Crypto.Encrypt(_dimwuUser.UserLevel, Config.Crypto.Key);
            _dimwuUser.SessionId = Util.EncodeSession(_dimstrSessionId);
            _dimwuUser.LoggedFromApp = true;
            HttpContext.Current.Application[_dimwuUser.Token] = _dimwuUser;

            _dimwuiLogInfo.Username = _dimwuUser.Username;
            _dimwuiLogInfo.UserLevel = _dimwuUser.UserLevel;
            _dimwuiLogInfo.ExpiresOn = _dimwuUser.ExpiresOn;
            _dimwuiLogInfo.SellerCode = _dimwuUser.SellerCode;
            _dimwuiLogInfo.Token = _dimwuUser.Token;
            _dimwuiLogInfo.LoggedFromApp = _dimwuUser.LoggedFromApp;
            _dimwuiLogInfo.SessionId = _dimwuUser.SessionId;
        }

        return _dimwuiLogInfo;
    }


}