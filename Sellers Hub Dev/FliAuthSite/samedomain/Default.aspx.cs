using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FliAuthLib.FliAuthService;
using FliAuthLib;

public partial class Default : AuthenticatedPageBase
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        Url.Text = Request.Url.Host;
        
        if (CurrentUser != null)
        {
            Username.Text = FliAuthLib.Crypto.Decrypt(CurrentUser.Username, Config.Crypto.Key);
            ExpiresOn.Text = CurrentUser.ExpiresOn.ToString();
        }
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Logout.aspx?Action=Logout");
    }

    protected void Logout2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/logout.aspx");
    }
}