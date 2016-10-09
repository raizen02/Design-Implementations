'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2015-04-24
'Finished Date          : 
'Program Name           : 2015 Incentives & Contest Deck
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 20xx-xx-xx | JO# JYYXXXXX
'						  DEV - 201x-xx-xx | PROD 201x-xx-xx
'							- 
'**************************************************************
Imports FliAuthLib

Partial Class Incentives_Contest_Deck_Default
    Inherits AuthenticatedPageBase

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        SessionAPI.RefreshSessions(CurrentUser)

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        If Session("sesSelIsInternational") = "True" Then
            Response.Redirect("../../errorPage.aspx")
        Else
            ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
End Class
