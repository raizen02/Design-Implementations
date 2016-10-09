'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2015-03-16
'Finished Date          : 2015-03-16
'Program Name           : Downlodable Forms
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* ProgrammersName | DateModified - JO# JYYXXXXX
'						  DEV - mm/dd/yyyy | PROD mm/dd/yyyy
'							- ChangeDescription
'**************************************************************
Imports FliAuthLib

Partial Class downloads
    'Inherits System.Web.UI.Page
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
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|downloads.aspx|") Then
            Response.Redirect("errorPage.aspx")

            Exit Sub
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    System.Data.SqlClient.SqlConnection.ClearAllPools()
    'End Sub
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
