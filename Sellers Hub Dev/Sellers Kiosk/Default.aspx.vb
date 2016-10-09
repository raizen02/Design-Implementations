'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2011-01-09
'Finished Date          : 
'Program Name           : Sellers Kiosk Login
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-01-07 - JO# J1304070
'						  DEV - 2013-01-07 | PROD 2014-02-19
'							- Change the design layout based on recommendation of Mr. Reuel.
'							- Change the themes

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "LoginUser"
'                           - Remove the Session("sesSelVerified") checking
'                           - Remove the login process

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Malvin Reyes | 2016-07-13 | JO# JYYXXXXX
'						  DEV - 2016-07-13 | PROD 2017-0x-xx
'							- Remove the Imports of System.Data, System.Data.SqlClient, clsConnection, clsUserLogin
'                           - Code cleanup
'**************************************************************

Imports FliAuthLib.FliAuthService
Imports FliAuthLib
Imports System.Web.UI

Partial Class DefaultLogin
    Inherits AuthenticatedPageBase


#Region "Declaring Variables"
    ' Your constant variables here

#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here

#End Region

#Region "Functions"
    ' Place your Functions here

#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '   txbUsername.Focus()
    End Sub

    'Protected Sub UserAuthenticated(ByVal sender As Object, ByVal User As WebUser) Handles Me.UserAuthenticateCompleted
    '    SessionAPI.ClearSessions()
    'End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnSignin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSignin.Click
        'If txbUsername.Text.Trim = String.Empty Or _
        '   txbPassword.Text.Trim = String.Empty Or _
        '   (txbUsername.Text.Trim = "Username" And _
        '   txbPassword.Text.Trim = "Password") Then
        '    rfvUsername.IsValid = False
        '    rfvPassword.IsValid = False
        'Else
        '    If LoginUser(txbUsername.Text.Trim, txbPassword.Text.Trim) = False Then
        '        cmvErrorMsg.IsValid = False
        '        cmvErrorMsg.Text = LoginError.ErrorMessage

        '        Session("sesSelVerified") = False
        '    End If
        'End If
    End Sub
#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

    'etc...
#End Region
End Class
