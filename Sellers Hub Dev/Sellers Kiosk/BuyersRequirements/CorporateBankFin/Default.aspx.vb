'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2014-11-14
'Finished Date          : 
'Program Name           : BuyersRequirements_CorporateBankFin
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-11-17 | JO# JYYXXXXX
'						  DEV - 2014-11-17 | PROD 2014-XX-XX
'							- Validate if the user have an access in reqindicator.aspx
'**************************************************************
Imports FliAuthLib

Partial Class BuyersRequirements_CorporateBankFin_Default
    Inherits AuthenticatedPageBase

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        SessionAPI.RefreshSessions(CurrentUser)

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|reqindicator.aspx|") Then
            Response.Redirect("../../errorPage.aspx")
        Else
            ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
End Class
