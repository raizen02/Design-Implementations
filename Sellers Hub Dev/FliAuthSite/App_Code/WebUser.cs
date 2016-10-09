//**************************************************************
//Programmer Name		: John Alexander M.Baltazar
//Date Created			: 2014.02.27
//Program Name           : WebUser Entity
//Program Description    : WebUser Info
//Remarks                : DEV - 2013.02.05 | PROD - 2013.07.08
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# JYYXXXXX
//Date Updated           : 2015.04.22
//Changes                : Added Web User Properties
//Remarks                : DEV - 2015.04.22 | PROD - n/a
//**************************************************************

using System;
using System.Collections.Generic;
using System.Web;

public class WebUser
{

    private string _pristrSessionId;
    private string _pristrToken;
    private string _pristrUsername;
    private string _pristrSellerCode;
    private string _pristrUserLevel;
    private bool _pristrLoggedFromApp;
    private DateTime _pridteExpiresOn;
    private string _pristrSellerAllocation;
    private string _pristrSellerSalesChannel;
    private string _pristrSellerClassification;
    private string _pristrSellerLevel;
    private string _pristrSellerPosition;

    public WebUser()
    { 

    }

    public string SessionId
    {
        get
        {
            return _pristrSessionId;
        }
        set
        {
            _pristrSessionId = value;
        }
    }

    public string Token
    {
        get
        {
            return _pristrToken;
        }
        set
        {
            _pristrToken = value;
        }
    }
    
    public string Username
    {
        get
        {
            return _pristrUsername;
        }
        set
        {
            _pristrUsername = value;
        }
    }

    public string SellerCode
    {
        get
        {
            return _pristrSellerCode;
        }
        set
        {
            _pristrSellerCode = value;
        }
    }

    public string UserLevel
    {
        get
        {
            return _pristrUserLevel;
        }
        set
        {
            _pristrUserLevel = value;
        }
    }

    public bool LoggedFromApp
    {
        get
        {
            return _pristrLoggedFromApp;
        }
        set
        {
            _pristrLoggedFromApp = value;
        }
    }

    public DateTime ExpiresOn
    {
        get
        {
            return _pridteExpiresOn;
        }
        set
        {
            _pridteExpiresOn = value;
        }
    }
    
    public string SellerAllocation
    {
        get { return _pristrSellerAllocation; }
        set { _pristrSellerAllocation = value; }
    }
    
    public string SellerSalesChannel
    {
        get { return _pristrSellerSalesChannel; }
        set { _pristrSellerSalesChannel = value; }
    }
    
    public string SellerClassification
    {
        get { return _pristrSellerClassification; }
        set { _pristrSellerClassification = value; }
    }

    public string SellerLevel
    {
        get { return _pristrSellerLevel; }
        set { _pristrSellerLevel = value; }
    }
    
    public string SellerPosition
    {
        get { return _pristrSellerPosition; }
        set { _pristrSellerPosition = value; }
    }


}