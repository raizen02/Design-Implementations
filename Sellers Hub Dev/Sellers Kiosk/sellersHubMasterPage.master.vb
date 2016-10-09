'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2013-11-29
'Finished Date          : 2013-11-29
'Program Name           : 
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin Reyes | DateModified - JO# JYYXXXXX
'						  DEV - 2014-01-14 | PROD 2014-02-19
'							- Change the footer

'						* Nestor Garais Jr | DateModified - JO# JYYXXXXX
'						  DEV - 2014-02-07 | PROD 2014-02-19
'							- Populate Navigation Menu in divMenu.InnerHtml

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Pass the parameter (Fullname and Sales Channel) for Google Analytics

'						* Malvin Reyes | 2014-05-19 | JO# JYYXXXXX
'						  DEV - 2014-05-19 | PROD 2014-xx-xx
'							- Change the "https://kiosk.filinvest.com.ph/SSO/" to <%=MySSOSite%> in html

'						* Malvin Reyes | 2015-02-16 | JO# J1500465
'						  DEV - 2015-02-18 | PROD 2015-xx-xx
'							- Change the logo if the Session("LoggedFromApp") is TRUE
'                           - Change the mode from 2 to 4 if the Session("LoggedFromApp") is TRUE

'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD  
'                         Fix warnings :
'                               CA2000 : Microsoft.Reliability : In method 'sellersHubMasterPage.fnblnQueryData(Integer, ByRef String, ByRef DataTable, ByRef DataSet, String)', call System.IDisposable.Dispose on object '_priclsClsConnection' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sellersHubMasterPage.fnblnQueryData(Integer, ByRef String, ByRef DataTable, ByRef DataSet, String)', call System.IDisposable.Dispose on object '_priclsMenu' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'sellersHubMasterPage.Page_Load(Object, EventArgs)', call System.IDisposable.Dispose on object '_pridttTable' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************
Imports System.Data

Partial Class sellersHubMasterPage
    Inherits System.Web.UI.MasterPage

#Region "Declaring Variables"
    ' Your constant variables here
    Private _priclsMenu As clsMenu
    Private _priclsClsConnection As clsConnection
    Private _pridttTable As DataTable
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here

#End Region

#Region "Functions"
    ' Place your Functions here
    Private Function fnblnQueryData(ByVal _parintMode As Integer, _
                                    Optional ByRef _parstrErrorMessage As String = "", _
                                    Optional ByRef _parDttOutput As DataTable = Nothing, _
                                    Optional ByRef _parDtsOutput As DataSet = Nothing, _
                                    Optional ByVal _parstrUsername As String = "") As Boolean
        Try
            _priclsMenu = New clsMenu
            _priclsClsConnection = New clsConnection

            fnblnQueryData = True

NotYetOpen:
            If _priclsClsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, _
                                                   MyMSSQLServer2000ConnectionString) = False Then
                _parstrErrorMessage = "Error while connecting to server"
                fnblnQueryData = False

                Exit Function
            End If

            If _priclsClsConnection.MSSQLServer2000Connection.State <> ConnectionState.Open Then
                GoTo NotYetOpen
            End If

            Dim _dimclsGlobalFunctions As New clsGlobalFunctions

            _priclsMenu.SQLConnection = _priclsClsConnection.MSSQLServer2000Connection

            If _priclsMenu.ExeCuteAko(clsMenu.ExecuteCommand.ExecuteDataAdapter, _
                                      _parstrUsername, _
                                      _parintMode) = False Then
                _parstrErrorMessage = _priclsMenu.SQLMessage
                fnblnQueryData = False

                Exit Function
            End If

            If _parDttOutput IsNot Nothing Then
                _priclsMenu.SQLDataAdapter.Fill(_parDttOutput)
            ElseIf _parDtsOutput IsNot Nothing Then
                _priclsMenu.SQLDataAdapter.Fill(_parDtsOutput)
            End If

            If _priclsMenu.SQLDataAdapter.SelectCommand.Parameters("@pintErrorNumber").Value <> 8888 Then
                fnblnQueryData = False

                _parstrErrorMessage = _priclsMenu.SQLDataAdapter.SelectCommand.Parameters("@pstrErrorMessage").Value
            End If
        Catch ex As Exception
            _parstrErrorMessage = MyExceptionNotice

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            fnblnQueryData = False
        Finally
            If _priclsClsConnection IsNot Nothing Then _priclsClsConnection.Dispose()
            If _priclsMenu IsNot Nothing Then _priclsMenu.Dispose()
        End Try
    End Function
#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions
        Dim _dimintMode As Integer = 2

        ViewState("Fullname") = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))

        spnWelcomeName.InnerHtml = _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname"))

        If Session("LoggedFromApp") = True Then
            imgLogo.Src = "img/logom.png"
            _dimintMode = 4
        Else
            imgLogo.Src = "img/logo.png"
            _dimintMode = 2
        End If
        'spnLastLog.InnerHtml = FormatDateTime(Session("sesSelLastLogIn"), DateFormat.LongDate) & " " & FormatDateTime(Session("sesSelLastLogIn"), DateFormat.LongTime)

        'Session("sesSelCurrentAddress") = _dimclsGlobalFunctions.fnGetPath(False, Request.ServerVariables("SERVER_NAME"), Request.ServerVariables("URL"), Request.ServerVariables("HTTPS"))
        'End If

        If Not Me.IsPostBack Then
            'Populate Seller Menu
            Dim _dimstrErrorMessage As String = ""

            ' Mode 2 : Seller Menu
            ' Mode 4 : Seller Menu ORS Only for Mobile Apps
            Try
                _pridttTable = New DataTable

                If fnblnQueryData(_dimintMode, _
                                  _dimstrErrorMessage, _
                                  _pridttTable, , _
                                  _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser"))) = False Then
                    divNavMenu.InnerHtml = ""
                    'Response.Write(_dimstrErrorMessage)
                Else
                    If _pridttTable.Rows.Count > 0 Then
                        divNavMenu.InnerHtml = _pridttTable.Rows(0).Item(0)
                    Else
                        divNavMenu.InnerHtml = ""
                    End If
                End If
            Catch ex As Exception
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Finally
                If _pridttTable IsNot Nothing Then _pridttTable.Dispose()
            End Try
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

