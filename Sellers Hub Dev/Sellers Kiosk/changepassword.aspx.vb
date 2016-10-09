'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2013-03-27
'Finished Date          : 
'Program Name           : Sellers Kiosk Change Password
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | 2013-04-01 | JO# J1300765
'						  DEV - 2013-04-01 | PROD 2013-04-01
'							- Include the captcha validation.

'						* Malvin Reyes | 2013-11-27 | JO# J1304070
'						  DEV - 2013-11-27 | PROD 2014-02-19
'							- Change the themes

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Nestor Garais Jr | 2016-06-22 | JO# JYYXXXXX
'						  DEV - 2016-06-22 | PROD 
'							- Fix warning : CA2000 : Microsoft.Reliability : In method 'changepassword.btnSave_Click(Object, EventArgs)', 
'                                            call System.IDisposable.Dispose on object '_dimdtsDataSet'
'                                            before all references to it are out of scope.
'                             Solution : Declare variable _dimdtsDataSet outside the scope then dispose on try/finally
'                           - Fix warning :  CA0060 : The indirectly-referenced assembly 'System.Web.Mvc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' could not be found. 
'                                            This assembly is not required for analysis, however, analysis results could be incomplete.
'                                            This assembly was referenced by: C:\Users\nestorsg\AppData\Local\Temp\tmpA195.tmp.cadir\bin\Recaptcha.dll.
'                             Solution : Update Recaptcha.dll with new GoogleReCaptcha.dll version 2
'                           - Replace clsConnection.CloseConnection() to clsConnection.Dispose() for connection close and cleanup
'**************************************************************

Imports System.Data
Imports System.Data.SqlClient
Imports clsConnection
Imports clsUserLogin
Imports FliAuthLib

Partial Class changepassword
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        _dimclsGlobalFunctions = Nothing
    End Sub

    '' Events of Main form
    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    System.Data.SqlClient.SqlConnection.ClearAllPools()
    'End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        divErrorMsgBox.Attributes.Remove("class")
        divErrorMsgBox.Attributes.Add("class", "alert alert-error")

        If Page.IsValid Then
            Dim _dimclsConnection As clsConnection = Nothing
            Dim _dimclsUserLogin As clsUserLogin = Nothing
            Dim _dimclsGlobalFunctions As New clsGlobalFunctions

            Dim _dimintMode As Integer = 2
             
            Dim _dimdtsDataSet As DataSet = Nothing

            Try
                _dimclsConnection = New clsConnection
                _dimclsUserLogin = New clsUserLogin

                If _dimclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                    divErrorMsgBox.Visible = True
                    divErrorMsg.InnerHtml = MyExceptionNotice

                    Exit Sub
                End If

                With _dimclsUserLogin
                    .Connection = _dimclsConnection.MSSQLServer2000Connection

                    If .fnUserLogin(ExecuteCommand.ExecuteDataAdapter, _dimintMode, _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser").ToString), txbNewPassword.Text, , , , txbOldPassword.Text) = False Then
                        divErrorMsgBox.Visible = True
                        divErrorMsg.InnerHtml = .ErrorMessage

                        Exit Sub
                    Else
                        _dimdtsDataSet = New DataSet
                        .DataAdapter.Fill(_dimdtsDataSet)

                        If _dimdtsDataSet.Tables(0).Rows.Count > 0 Then
                            divErrorMsgBox.Visible = True

                            If _dimdtsDataSet.Tables(0).Rows(0).Item("usrErrorNumber").ToString() = "8888" Then
                                divErrorMsgBox.Attributes.Remove("class")
                                divErrorMsgBox.Attributes.Add("class", "alert alert-success")
                            End If

                            divErrorMsg.InnerHtml = _dimdtsDataSet.Tables(0).Rows(0).Item("usrErrorMessage").ToString()
                        Else
                            divErrorMsgBox.Visible = True
                            divErrorMsg.InnerHtml = "Invalid Password."
                        End If
                    End If
                End With
            Catch ex As Exception
                divErrorMsgBox.Visible = True
                divErrorMsg.InnerHtml = MyExceptionNotice

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Finally
                If _dimclsConnection IsNot Nothing Then _dimclsConnection.Dispose()
                If _dimclsUserLogin IsNot Nothing Then _dimclsUserLogin.Dispose()
         
                If _dimdtsDataSet IsNot Nothing Then _dimdtsDataSet.Dispose()

                _dimclsConnection = Nothing
                _dimclsUserLogin = Nothing
                _dimclsGlobalFunctions = Nothing
            End Try
        Else
            divErrorMsgBox.Visible = True
            divErrorMsg.InnerHtml = "Please try again."
        End If
    End Sub
#End Region

#Region "ReCaptcha"
    ' Events in all ReCaptcha
    Protected Sub rccReCaptcha_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles rccReCaptcha.Init
        'rccReCaptcha.Theme = "white"
        rccReCaptcha.PublicKey = MyPublicKey
        rccReCaptcha.PrivateKey = MyPrivateKey
    End Sub
#End Region

    'etc...
#End Region
End Class
