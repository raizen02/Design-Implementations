using System;
using System.Collections.Generic;
using System.Web;

public class Util
{
    public static string AppendedQueryString(string _parstrUrl, string _parstrKey, string _parstrValue)
    {
        _parstrUrl = RemoveQueryString(_parstrUrl, _parstrKey);

        if (_parstrUrl.Contains("?"))
        {
            _parstrUrl = string.Format("{0}&{1}={2}", _parstrUrl, _parstrKey, _parstrValue);
        }
        else
        {
            _parstrUrl = string.Format("{0}?{1}={2}", _parstrUrl, _parstrKey, _parstrValue);
        }

        return _parstrUrl;
    }

    public static string RemoveQueryString(string _parstrUrl, string _parstrKey)
    {
        Uri _uriUrl = new Uri(_parstrUrl);
        string _strQuery = _uriUrl.Query.ToLower();
        _parstrKey = _parstrKey.ToLower();
        int _intKeyIndex = _strQuery.IndexOf("&" + _parstrKey);
        int _intNextEscapeChar;
        int _intLength;

                      
        if (_intKeyIndex < 0)
        {
             _intKeyIndex = _strQuery.IndexOf("?" + _parstrKey);
        }

        if (_intKeyIndex < 0)
        {
            return _parstrUrl;
        }

        _intNextEscapeChar = _strQuery.IndexOfAny(new char[] { '?', '&' }, _intKeyIndex + 1);

        if (_intNextEscapeChar > -1)
        {
            _intLength = _intNextEscapeChar - _intKeyIndex;
        }
        else
        {
            _intLength = _strQuery.Length - _intKeyIndex;
        }

        _parstrUrl = _uriUrl.AbsoluteUri.Split('?')[0];
        _strQuery = _strQuery.Remove(_intKeyIndex, _intLength).TrimStart('?').TrimStart('&');
        _parstrUrl = _parstrUrl + (_strQuery != "" ? "?" + _strQuery : "");
        
        return _parstrUrl;
    }

    public static string GetGuidHash()
    {
        return Guid.NewGuid().GetHashCode().ToString("x");
    }

    public static bool IsDate(string val)
    {
        try
        {
            DateTime.Parse(val);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string RelativeToAbsolutePath(string _parstrRelativePath)
    {
        HttpRequest Request = HttpContext.Current.Request;
        string returnUrl = string.Format("{0}{1}", Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, string.Empty), VirtualPathUtility.ToAbsolute(_parstrRelativePath));
        return returnUrl;
    }

    public static DateTime GetExpirationBasedOnConfig()
    {
        return DateTime.Now.AddMinutes(Config.CookieSettings.CookieTimeoutInMinutes);
    }

    public static string EncodeSession(string _parstrSessionId)
    {
        return Crypto.Encrypt(Config.PreKey.AccountSessionId + _parstrSessionId, Config.Crypto.Key);
    }

    public static string DecodeSession(string _parstrEncodedSessionId)
    {
        string _dimstrSessionId;
        _dimstrSessionId = Crypto.Decrypt(_parstrEncodedSessionId, Config.Crypto.Key);

        if (_dimstrSessionId.StartsWith("#ERROR"))
        {
            _dimstrSessionId = "";
        }

        if (_dimstrSessionId.StartsWith(Config.PreKey.AccountSessionId))
        {
            _dimstrSessionId = _dimstrSessionId.Replace(Config.PreKey.AccountSessionId, "");
        }
        else
        {
            _dimstrSessionId = "";
        }

        return _dimstrSessionId;
    }
}