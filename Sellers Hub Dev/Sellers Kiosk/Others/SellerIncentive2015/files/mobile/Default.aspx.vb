'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2013-09-20
'Finished Date          : 
'Program Name           : Project Catalog (mobile)
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2013-09-17 - JO# J1303585
'						  DEV 2013-09-20 | PROD 2013-09-20
'							- Include the User validation for mobile

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-29 | JO# JYYXXXXX
'						  DEV - 2014-04-29 | PROD 2014-05-13
'							- Insert the Google Analytics Code
'                           - Set the 'dimension1' to SellerName
'                           - Set the 'dimension2' to SellerType
'                           - Set the 'dimension3' to SellerName + ' / ' + SellerType
'**************************************************************
Imports FliAuthLib

Partial Class ProjectCatalog_files_mobile_Default
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions
        Dim _dimcsmClientScriptManager As ClientScriptManager = Page.ClientScript

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|ProjectCatalog/|") Then
            _dimcsmClientScriptManager.RegisterStartupScript(Me.GetType(), "meClose", "window.location='../../../errorPage.aspx';", True)
        Else
            ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
End Class
