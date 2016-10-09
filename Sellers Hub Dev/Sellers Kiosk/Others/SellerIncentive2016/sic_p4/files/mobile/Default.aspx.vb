'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2016-03-31
'Finished Date          : 
'Program Name           : 2016 Incentives & Contest Deck (Page 4 - mobile)
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2016-03-21 | JO# SR-IT-2016-00822
'						  DEV - 2016-04-04 | PROD 2016-04-07
'							- 2016 I Own My Dream
'**************************************************************
Imports FliAuthLib

Partial Class Others_SellerIncentive2016_sic_p4_mobile_Default
    Inherits AuthenticatedPageBase

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        SessionAPI.RefreshSessions(CurrentUser)

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        If Session("sesSelIsInternational") = "True" Then
            Response.Redirect("../../../../../errorPage.aspx")
        Else
            ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
End Class
