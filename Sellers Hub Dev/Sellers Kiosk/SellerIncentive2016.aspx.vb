'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2016-03-22
'Finished Date          : 
'Program Name           : 2016 Incentives & Contest Deck
'Program Description    : 
'Stored Procedure       : 
'Updates Information

'						* Malvin Reyes | 2016-03-21 | JO# SR-IT-2016-00822
'						  DEV - 2016-04-04 | PROD 2016-04-07
'							- 2016 I Own My Dream
'**************************************************************
Imports FliAuthLib

Partial Class Others_SellerIncentive2016
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
        SessionAPI.RefreshSessions(CurrentUser)

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        If Session("sesSelIsInternational") = "True" Then
            Response.Redirect("errorPage.aspx")
        Else
            ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons

#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

    'etc...
#End Region
End Class
