'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2014-04-21
'Finished Date          : 
'Program Name           : BentaTrenta
'Program Description    : 
'Stored Procedure       : 
'Updates Information

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Malvin Reyes | 2015-02-02 | JO# J1500291
'						  DEV - 2015-02-02 | PROD 2015-02-03
'							- Redirect the page in "errorPage.aspx"
'**************************************************************
Imports FliAuthLib

Partial Class BentaTrenta
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

        'If Session("sesSelIsInternational") = "True" Then
        Response.Redirect("../../errorPage.aspx")
        'Else
        '    ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))
        'End If

        '_dimclsGlobalFunctions = Nothing
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
