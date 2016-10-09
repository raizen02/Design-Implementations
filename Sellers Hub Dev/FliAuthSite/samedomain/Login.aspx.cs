using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FliAuthLib;
using FliAuthLib.FliAuthService;

public partial class Login : AuthenticatedLoginBase
{  
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        base.UserAuthenticateCompleted += UserAuthenticated1;

        if (LoginUser(Username.Text.Trim(), Password.Text.Trim()) == false)
        {
            Msg.Text = LoginError.ErrorMessage;
        }
    }

    private void UserAuthenticated1(object sender, FliAuthLib.FliAuthService.WebUser user)
    {
        
    }
   
}