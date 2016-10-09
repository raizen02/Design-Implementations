//**************************************************************
//Programmer Name		: John Alexander M.Baltazar
//Date Created			: 2014.03.05
//Finished Date         : 2014.03.05
//Program Name          : forgotpassword
//Program Description   : Interface for SSO Account Recovery
//**************************************************************
//Programmer Name		: John Alexander M.Baltazar
//Date Updated          : 2014.05.26
//Changes               : Changed mask of TIN from Numeric to AlphaNumeric
//Remarks               : DEV - 2014.05.26 | PROD - 2014.05.26
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# J1501697
//Date Updated           : 2015.06.26
//Changes                : added email as validation on Account Recovery
//Remarks                : DEV - 2015.06.26 | PROD - n/a
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# JYYXXXXX
//Date Updated           : 2015.08.19
//Changes                : added check if username or email is active
//Remarks                : DEV - 2015.08.19 | PROD - n/a
//**************************************************************

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

public partial class forgotpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            UserManager _priclsUser = new UserManager();

            try
            {
                _priclsUser.OpenDbConnection();

                if (txbUsername.Text.Trim() != "" && _priclsUser.UsernameExists(txbUsername.Text.Trim()) == false)
                {
                    cvUserName.IsValid = false;
                    cvUserName.ErrorMessage = "Username doesn't exists.";
                }

                if (txbUsername.Text.Trim() != "" && cvUserName.IsValid && _priclsUser.IsUserActive(txbUsername.Text.Trim()) == false)
                {
                    cvUserName.IsValid = false;
                    cvUserName.ErrorMessage = "User is inactive";
                }

                if (txbEmail.Text.Trim() != "" && _priclsUser.EmailExists(txbEmail.Text.Trim()) == false)
                {
                    cvEmail.IsValid = false;
                    cvEmail.ErrorMessage = "Email Address doesn't exists.";
                }

                if (txbEmail.Text.Trim() != "" && cvEmail.IsValid && _priclsUser.IsEmailActive(txbEmail.Text.Trim()) == false)
                {
                    cvEmail.IsValid = false;
                    cvEmail.ErrorMessage = "Email Address is inactive.";
                }

                if (_priclsUser.ValidSeller(txbSellerCode.Text.Trim(), txbTIN.Text.Trim(), Convert.ToDateTime(txbBirthday.Text.Trim())) == false)
                {
                    if (_priclsUser.LastError.ErrorNumber == 501)
                    {
                        cvSellers.IsValid = false;
                        cvSellers.ErrorMessage = _priclsUser.LastError.ErrorMessage;
                    }                
                }

                if (!cvSellers.IsValid || !cvUserName.IsValid || !cvEmail.IsValid)
                {
                    //ClientScriptManager cs = Page.ClientScript;
                    //cs.RegisterStartupScript(this.GetType(), "startupScript", "ValidateUserInputs_KjseoD();", true);

                    return;
                }

                bool _dimblnSuccess;

                _priclsUser.BeginTransaction();

                _dimblnSuccess = _priclsUser.RecoverAccount(txbUsername.Text.Trim(), txbEmail.Text.Trim(), Util.GetGuidHash(), txbSellerCode.Text.Trim());

                if (!_dimblnSuccess)
                {
                    _priclsUser.RollbackTran();
                    cvServerError.ErrorMessage = _priclsUser.LastError.ErrorMessage;
                    cvServerError.IsValid = false;
                    return;
                }

                _priclsUser.CommitTran();
                ClearEntries();

                cvSuccess.ErrorMessage = "Your new password was sent to your email address.";
                cvSuccess.IsValid = false;
            }
            catch (Exception ex)
            {
                _priclsUser.RollbackTran();
                cvServerError.ErrorMessage = ex.Message;
                cvServerError.IsValid = false;
            }
            finally
            {
                _priclsUser.CloseConnection();
                _priclsUser = null;

            }
        }
        else
        {
            cvServerError.ErrorMessage = "Please try again.";
            cvServerError.IsValid = false;
        }
    }

    private void ClearEntries()
    { 
        txbUsername.Text = "";
        txbSellerCode.Text = "";
        txbBirthday.Text = "";
        txbTIN.Text = "";
        txbEmail.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearEntries();
    }

    protected void rccReCaptcha_Init(object sender, EventArgs e)
    {
        rccReCaptcha.Theme = "white";
        rccReCaptcha.PublicKey = Config.MyCaptchaKey.PublicKey;
        rccReCaptcha.PrivateKey = Config.MyCaptchaKey.PrivateKey;
    }
}
