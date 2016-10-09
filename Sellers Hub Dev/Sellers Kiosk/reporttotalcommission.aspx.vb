'**************************************************************
'Programmer Name		: Nestor S. Garais Jr
'Date Created			: 2010-05-04
'Finished Date          : 2010-05-04
'Program Name           : Viewing of Commission Status Report
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Nestor Garais Jr | 2012-05-17 | JO# JYYXXXXX
'						  DEV - 2012-05-17 | PROD 2012-XX-XX
'							- change controls textbox date to EO datetimepicker

'						* Nestor Garais Jr | 2013-06-18 | JO# JYYXXXXX
'						  DEV - 2013-06-18  | PROD 2013-XX-XX
'							- Update Commission STatus and sales without commision report, source data using frebas table
'                           - use datatable for report datasource

'						* Nestor Garais Jr | 2013-9-18 | JO# JYYXXXXX
'						  DEV   2013-9-18  | PROD 2013-xx-xx
'							- Update selrptDefinitionOfDocuments report source
'                           - Transfer SP to connecting commission kiosk server

'						* Nestor Garais Jr | 2013-9-27 | JO# JYYXXXXX
'						  DEV   2013-9-27  | PROD 2013-xx-xx
'							- set _pdteDateFrom/_pdteDateTo to now(date) if no selected date

'						* Malvin V. Reyes | 2013-12-10 | JO# n/a
'						  DEV 2013-12-10 | PROD 2014-02-19
'							- Fixed the flexible layout.

'						* Nestor Garais Jr | 2014-02-17 | JO# n/a
'						  DEV 2014-02-17 | PROD 2014-02-19
'							- remove dropdown report list
'							- load only one report. Total Commission Report (rptTotalCommissionReport.rpt) 

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV -  2016-06-28 | PROD  
'                         Fix warnings :
'                              CA2000 : Microsoft.Reliability : In method 'reporttotalcommission.fnblnQueryData(reporttotalcommission.TransMode, Date, Date, String, String, ByRef DataTable, ByRef DataSet)', call System.IDisposable.Dispose on object '_dimSqlDataAdapter' before all references to it are out of scope.
'                              CA2000 : Microsoft.Reliability : In method 'reporttotalcommission.fnblnQueryData(reporttotalcommission.TransMode, Date, Date, String, String, ByRef DataTable, ByRef DataSet)', call System.IDisposable.Dispose on object '_dimclsClsConnection' before all references to it are out of scope.
'                              CA2000 : Microsoft.Reliability : In method 'reporttotalcommission.LoadCommStatus()', call System.IDisposable.Dispose on object '_priDttOutput' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************

Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports FliAuthLib

Partial Class reporttotalcommission
    'Inherits System.Web.UI.Page
    Inherits AuthenticatedPageBase
    Private _prirpdReport As ReportDocument
    Private _priDttOutput As DataTable

#Region "Declaring Variables"
    ' Your constant variables here
    Private Enum TransMode
        UnearnedCommissions = 1
        TotalCommissionStatus = 2
    End Enum
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subErrorMessagePrompt(ByVal _parbolIsVisible As Boolean, _
                                      Optional ByVal _parstrErrorMsg As String = "Internal Error Occured.", _
                                      Optional ByVal _parstrAlert As String = "error")
        If _parbolIsVisible Then
            divErrorMsgBox.Visible = True

            divErrorMsgBox.Attributes.Remove("class")
            divErrorMsgBox.Attributes.Add("class", "alert alert-" & _parstrAlert)

            divErrorMsg.InnerHtml = _parstrErrorMsg
        Else
            divErrorMsgBox.Visible = False
        End If
    End Sub

    Public Sub LoadCommStatus()
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions
        Dim strReportPath As String = String.Empty

        _prirpdReport = New ReportDocument
        strReportPath = Server.MapPath("Reports/rptTotalCommissionReport.rpt")
        _priDttOutput = New DataTable

        Try
            ' 2013-6-18 : use datable for report source
            Dim _dimstrErrorMessage As String = ""

            If fnblnQueryData(TransMode.TotalCommissionStatus, _
                              dtpDateFrom.SelectedDate, _
                              dtpDateTo.SelectedDate, _
                              _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelAgentCode")), _
                              _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelAgentCode")), _
                              _priDttOutput) = False Then
                Exit Sub
            End If

            _prirpdReport.Load(strReportPath)
            _prirpdReport.SetDataSource(_priDttOutput)
            _prirpdReport.PrintOptions.PaperSize = PaperSize.PaperLegal

            divPDFLoader.Visible = True

            Dim _dimstrReportKey As String

            _dimstrReportKey = "StatusReport"
            Session.Add(_dimstrReportKey, _prirpdReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))

            divPDFLoader.InnerHtml = "<iframe src='selPDFLoader.aspx?SessionID=" & _dimstrReportKey & _
                                     "#zoom=100' name=""resReport"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no""></iframe>"
        Catch ex As Exception
            divPDFLoader.Visible = False
            subErrorMessagePrompt(True, MyExceptionNotice)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _prirpdReport IsNot Nothing Then _prirpdReport.Dispose()
            If _priDttOutput IsNot Nothing Then _priDttOutput.Dispose()
        End Try
    End Sub
#End Region

#Region "Functions"
    ' Place your Functions here
    Private Function fnblnQueryData(ByVal _parintMode As TransMode, _
                                    ByVal _pdteDateFrom As Date, _
                                    ByVal _pdteDateTo As Date, _
                                    Optional ByVal _pstrSellerCode As String = "", _
                                    Optional ByVal _parstrUserName As String = "", _
                                    Optional ByRef _parDttOutput As DataTable = Nothing, _
                                    Optional ByRef _parDtsOutput As DataSet = Nothing) As Boolean
        Dim _dimclsClsConnection As clsConnection = Nothing
        Dim _dimSqlDataAdapter As SqlDataAdapter = Nothing

        fnblnQueryData = True

        Try
            _dimclsClsConnection = New clsConnection

            '2013-9-18 from MyMSSQLServer2000ConnectionString to  MyMSSQLServer2000ComKioskConnectionString
            If _dimclsClsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ComKioskConnectionString) = False Then
                subErrorMessagePrompt(True, "Error while connecting to server")
                fnblnQueryData = False

                Exit Function
            End If

            _dimSqlDataAdapter = New SqlClient.SqlDataAdapter("SP_portalTotalAndUnearnedCommissionReport", _dimclsClsConnection.MSSQLServer2000Connection)

            With _dimSqlDataAdapter.SelectCommand
                .CommandType = CommandType.StoredProcedure
                .CommandTimeout = 0

                With .Parameters
                    '2013-9-27 : set _pdteDateFrom/_pdteDateTo to now(date) if no selected date
                    If _pdteDateFrom.ToString = "1/1/0001 12:00:00 AM" Then
                        _pdteDateFrom = "1900-01-01"
                    End If

                    If _pdteDateTo.ToString = "1/1/0001 12:00:00 AM" Then
                        _pdteDateTo = Now
                    End If

                    .AddWithValue("@pintMode", _parintMode)
                    .AddWithValue("@pdteDateFrom", _pdteDateFrom)
                    .AddWithValue("@pdteDateTo", _pdteDateTo)
                    .AddWithValue("@pstrSellerCode", _pstrSellerCode)
                    '.AddWithValue("@pstrUserName", _parstrUserName)

                    .Add("@pintErrorNumber", SqlDbType.Int, 4).Value = 4444
                    .Item("@pintErrorNumber").Direction = ParameterDirection.Output
                    .Add("@pstrErrorMessage", SqlDbType.VarChar, 1000).Value = " "
                    .Item("@pstrErrorMessage").Direction = ParameterDirection.Output
                End With
            End With

            If _parDttOutput IsNot Nothing Then
                _dimSqlDataAdapter.Fill(_parDttOutput)
            ElseIf _parDtsOutput IsNot Nothing Then
                _dimSqlDataAdapter.Fill(_parDtsOutput)
            End If

            If _dimSqlDataAdapter.SelectCommand.Parameters("@pintErrorNumber").Value <> MyMSSQLServer2000SuccessErrorNumber Then
                fnblnQueryData = False
                subErrorMessagePrompt(True, _dimSqlDataAdapter.SelectCommand.Parameters("@pstrErrorMessage").Value)

                Exit Function
            End If

            subErrorMessagePrompt(False)
        Catch ex As Exception
            subErrorMessagePrompt(True, MyExceptionNotice)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            fnblnQueryData = False
        Finally
            If _dimclsClsConnection IsNot Nothing Then _dimclsClsConnection.Dispose()
            If _dimSqlDataAdapter IsNot Nothing Then _dimSqlDataAdapter.Dispose()
        End Try
    End Function

    Protected Function fnErrorCheck() As Boolean
        If dtpDateFrom.SelectedDateString = "" Or dtpDateTo.SelectedDateString = "" Then
            subErrorMessagePrompt(True, "Please make entry in all fields")
            Return False

            Exit Function
        End If

        If IsDate(dtpDateFrom.SelectedDate) = False Or IsDate(dtpDateTo.SelectedDate) = False Then
            subErrorMessagePrompt(True, "Date is not a valid date")
            Return False

            Exit Function
        End If

        If CDate(dtpDateFrom.SelectedDate) > CDate(dtpDateTo.SelectedDate) Then
            subErrorMessagePrompt(True, "Starting Date is Greater Than Ending Date")
            Return False

            Exit Function
        End If

        subErrorMessagePrompt(False)
        Return True
    End Function
#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|reporttotalcommission.aspx|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If _prirpdReport IsNot Nothing Then _prirpdReport.Dispose()
    End Sub
#End Region

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        If fnErrorCheck() = True Then
            LoadCommStatus()
        End If
    End Sub
#End Region

#Region "Grid"
    ' Events in all Grid

#End Region

#Region "DropDownList"
    ' Events in all DropDownList
#End Region
    'etc...
#End Region
End Class
