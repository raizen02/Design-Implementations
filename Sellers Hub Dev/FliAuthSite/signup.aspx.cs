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

public partial class signup : System.Web.UI.Page
{
    private UserManager _priclsUser;
    private string _pristrConfirmId;
    private string _pristrAction;

    private void LoadParam()
    {
        _pristrConfirmId = Request.QueryString[AppConstants.UrlParams.CONFIRMATION_ID];
        _pristrAction = Request.QueryString[AppConstants.UrlParams.ACTION];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadParam();

        if (Page.IsPostBack == true)
        {
            return;
        }

        if (_pristrConfirmId != null && _pristrAction != null && _pristrAction.ToLower() == AppConstants.ParamValues.CONFIRM.ToLower())
        {
            _priclsUser = new UserManager();

            try
            {
                pnlFields.Visible = false;

                _priclsUser.OpenDbConnection();

                if (_priclsUser.ValidConfirmationId(_pristrConfirmId) == true)
                {
                    _priclsUser.BeginTransaction();

                    if (_priclsUser.ConfirmUser(_pristrConfirmId, null) == false)
                    {
                        _priclsUser.RollbackTran();
                        cvServerError.ErrorMessage = _priclsUser.LastError.ErrorMessage;
                        cvServerError.IsValid = false;
                        return;                        
                    }

                    _priclsUser.CommitTran();

                    hlLogin.NavigateUrl = ConfigurationManager.AppSettings[AppConstants.Urls.CONFIRM_LOGIN_LINK];
                    hlLogin.Visible = true;

                    cvSuccess.ErrorMessage = "Confirmation successful. You can now use your account.";
                    cvSuccess.IsValid = false;
                }
                else
                {
                    cvServerError.ErrorMessage = "Your confimation link is invalid or expired. Please sign up for an account.";
                    cvServerError.IsValid = false;
                    hlSignup.Visible = true;
                }
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
    }

    private void ClearEntries()
    {
        txbUsername.Text = "";
        txbPassword.Text = "";
        txbConfirmPassword.Text = "";
        txbEmailAddr.Text = "";
        txbSellerCode.Text = "";
        txbTIN.Text = "";
        txbBirthday.Text = "";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        _priclsUser = new UserManager();

        try
        {
            _priclsUser.OpenDbConnection();

            if (_priclsUser.UsernameExists(txbUsername.Text.Trim()) == true)
            {
                cvUserName.IsValid = false;
                cvUserName.ErrorMessage = _priclsUser.LastError.ErrorMessage;
            }

            if (_priclsUser.EmailExists(txbEmailAddr.Text.Trim()) == true)
            {
                cvEmail.IsValid = false;
                cvEmail.ErrorMessage = _priclsUser.LastError.ErrorMessage;
            }

            if (_priclsUser.ValidSeller(txbSellerCode.Text.Trim(), txbTIN.Text.Trim(), Convert.ToDateTime(txbBirthday.Text.Trim())) == false)
            {
                cvSellers.IsValid = false;
                cvSellers.ErrorMessage = _priclsUser.LastError.ErrorMessage;
            }

            if (!cvUserName.IsValid || !cvEmail.IsValid || !cvSellers.IsValid)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "startupScript", "ValidateUserInputs_KjseoD();", true);

                return;
            }

            bool _dimblnSuccess;

            _priclsUser.BeginTransaction();

            _dimblnSuccess = _priclsUser.AddUserForConfirmation(txbUsername.Text.Trim(), txbPassword.Text.Trim(), txbEmailAddr.Text.Trim(),
                txbSellerCode.Text.Trim(), txbTIN.Text.Trim(), Convert.ToDateTime(txbBirthday.Text.Trim()), Request.ServerVariables["REMOTE_ADDR"]);

            if (!_dimblnSuccess)
            {
                _priclsUser.RollbackTran();
                cvServerError.ErrorMessage = _priclsUser.LastError.ErrorMessage;
                cvServerError.IsValid = false;
                return;
            }
             
            _priclsUser.CommitTran();
            ClearEntries();

            cvSuccess.ErrorMessage = "Confirmation link was sent to your email address.";
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearEntries();
    }
}
