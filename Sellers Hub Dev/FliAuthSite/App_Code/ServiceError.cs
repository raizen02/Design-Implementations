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
/// Summary description for ServiceError
/// </summary>
public class ServiceError
{
    public ServiceError()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _priintErrorNumber;
    private string _pristrErrorMessage;

    public static int SuccessErrorNumber
    {
        get
        {
            return 8888;
        }
    }

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
            return _pristrErrorMessage;
        }
        set
        {
            _pristrErrorMessage = value;
        }
    }
}
