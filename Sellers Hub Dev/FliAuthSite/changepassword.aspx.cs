using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class changepasswordcs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void ClearInputs()
    {
        txbUsername.Text = "";
        txbOldPassword.Text = "";
        txbNewPassword.Text = "";
        txbConfirmPassword.Text = "";
    }        

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearInputs();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            UserManager _priclsUserMgr = new UserManager();

            try
            {
                _priclsUserMgr.OpenDbConnection();
                _priclsUserMgr.BeginTransaction();

                if (_priclsUserMgr.ChangePassword(txbUsername.Text.Trim(), txbNewPassword.Text.Trim(), txbOldPassword.Text.Trim()) == false)
                {
                    _priclsUserMgr.RollbackTran();
                    cvServerError.ErrorMessage = _priclsUserMgr.LastError.ErrorMessage;
                    cvServerError.IsValid = false;
                    return;
                }

                _priclsUserMgr.CommitTran();

                cvSuccess.ErrorMessage = "Password successfully changed.";
                cvSuccess.IsValid = false;
            }
            catch (Exception ex)
            {
                _priclsUserMgr.RollbackTran();
                cvServerError.ErrorMessage = ex.Message;
                cvServerError.IsValid = false;
            }
            finally
            {
                _priclsUserMgr.CloseConnection();
                _priclsUserMgr = null;
            }
        }
        else
        {
            cvServerError.ErrorMessage = "Please try again.";
            cvServerError.IsValid = false;
        }
    }

    protected void rccReCaptcha_Init(object sender, EventArgs e)
    {
        rccReCaptcha.Theme = "white";
        rccReCaptcha.PublicKey = Config.MyCaptchaKey.PublicKey;
        rccReCaptcha.PrivateKey = Config.MyCaptchaKey.PrivateKey;
    }
}
