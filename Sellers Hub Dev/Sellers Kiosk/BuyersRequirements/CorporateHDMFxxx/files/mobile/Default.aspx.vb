'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2014-11-14
'Finished Date          : 
'Program Name           : BuyersRequirements_CorporateHDMF (mobile)
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-XX-XX | JO# JYYXXXXX
'						  DEV - 2014-XX-XX | PROD 2014-XX-XX
'							- 
'**************************************************************
Imports FliAuthLib

Partial Class BuyersRequirements_CorporateHDMF_files_mobile_Default
    Inherits AuthenticatedPageBase

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        _dimclsGlobalFunctions = Nothing
    End Sub
End Class
