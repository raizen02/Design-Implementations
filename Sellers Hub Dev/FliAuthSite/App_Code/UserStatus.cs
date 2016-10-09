using System;
using System.Collections.Generic;
using System.Web;

public class UserStatus
{
    public UserStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private bool _priblnUserLoggedIn;
    private bool _priblnRequestIdValid;

    public bool UserLoggedIn
    {
        get
        {
            return _priblnUserLoggedIn;
        }
        set
        {
            _priblnUserLoggedIn = value;
        }
    }

    public bool RequestIdValid
    {
        get
        {
            return _priblnRequestIdValid;
        }
        set
        {
            _priblnRequestIdValid = value;
        }
    }


}
