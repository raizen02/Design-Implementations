'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2014-04-21
'Finished Date          : 
'Program Name           : GroupTour
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-04-29 | JO# JYYXXXXX
'						  DEV - 2014-04-29 | PROD 2014-05-13
'							- Insert the Google Analytics Code
'                           - Set the 'dimension1' to SellerName
'                           - Set the 'dimension2' to SellerType
'                           - Set the 'dimension3' to SellerName + ' / ' + SellerType
'**************************************************************
Imports FliAuthLib

Partial Class BentaTrenta_GroupTour_Default
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
