//**************************************************************
//Programmer Name		: John Alexander M.Baltazar
//Date Created			: 2015.02.16
//Program Name           : WebUserLogInfo
//Program Description    : WebUserLogInfo for Mobile Apps
//Remarks                : DEV - 2015.02.24 | PROD - 201x.xx.xx
//**************************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for WebUserLogInfo
/// </summary>
public class WebUserLogInfo: WebUser
{
    public WebUserLogInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _priintErrorNumber;
    private string _priintErrorMessage;

    public int ErrorNumber
    {
        get
        {
            return _priintErrorNumber;
        }
        set
        {
            _priintErrorNumber = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            return _priintErrorMessage;
        }
        set
        {
            _priintErrorMessage = value;
        }
    }


}
