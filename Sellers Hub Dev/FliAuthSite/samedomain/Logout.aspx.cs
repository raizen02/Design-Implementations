using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FliAuthLib;
using FliAuthLib.FliAuthService;

public partial class LogoutPage : AuthenticatedLogoutBase
{    

    protected void Page_Load(object sender, EventArgs e)
    {
        Url.Text = Request.Url.Host;

        if (CurrentUser != null)
        {
            Username.Text = CurrentUser.Username;
        }
    }   

    protected void Logout_Click(object sender, EventArgs e)
    {
        this.LogoutUser();
    }
}