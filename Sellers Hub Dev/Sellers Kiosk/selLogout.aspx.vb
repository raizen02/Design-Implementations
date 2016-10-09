'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 20xx-xx-xx
'Finished Date          : 20xx-xx-xx
'Program Name           : Logout
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-XX-XX
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking
'						* Nestor Garais Jr | 2016-02-09 | JO# JYYXXXXX
'						  DEV - 2016-02-09 | PROD 
'							  - add function deleting 1 day old uploaded images from tempfolder
'**************************************************************
Imports FliAuthLib

Partial Class selLogout
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedLogoutBase
    Private _pristrFilePath As String = "./TempUploadFiles/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        Session("sesSelVerified") = False
        Session.Abandon()

        SessionAPI.ClearSessions()

        ' Delete old images 
        Call CleanUpTempPostedFiles()
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "meClose", "window.location='" & _dimclsGlobalFunctions.fnGetPath(False, Request.ServerVariables("SERVER_NAME"), MyDefaultFile, Request.ServerVariables("HTTPS")) & "';", True)
    End Sub

    Private Sub CleanUpTempPostedFiles()
        Dim _dimStrFileNames() As String = IO.Directory.GetFiles(Server.MapPath(_pristrFilePath))

        For Each _forFile As String In _dimStrFileNames
            Dim _dimfiFileTobdeleted As New IO.FileInfo(_forFile)

            'delete all temp file 1 days after last used
            If _dimfiFileTobdeleted.LastWriteTime < Date.Now - New TimeSpan(1, 0, 0, 0) Then
                _dimfiFileTobdeleted.Delete()
            End If
        Next
    End Sub
End Class
