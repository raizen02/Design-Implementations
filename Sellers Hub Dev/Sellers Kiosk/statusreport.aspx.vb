'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2010-05-10
'Finished Date          : 2010-05-10
'Program Name           : Viewing of Commission Status Report
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Nestor Garais Jr | 5/17/2012 - JO# JYYXXXXX
'						  DEV - 5/17/2012 | PROD mm/dd/yyyy
'							- change controls textbox date to EO datetimepicker
'						* Nestor Garais Jr | 6/18/2013 - JO# JYYXXXXX
'						  DEV - 6/18/2013  | PROD mm/dd/yyyy
'							- Update Commission STatus and sales without commision report, source data using frebas table
'                           - use datatable for report datasource
'						* Nestor Garais Jr | 2013-9-18 - JO# JYYXXXXX
'						  DEV   2013-9-18  | PROD mm/dd/yyyy
'							- Update selrptDefinitionOfDocuments report source
'                           - Transfer SP to connecting commission kiosk server
'						* Nestor Garais Jr | 2013-9-27  - JO# JYYXXXXX
'						  DEV   2013-9-27  | PROD mm/dd/yyyy
'							- set _pdteDateFrom/_pdteDateTo to now(date) if no selected date

'						* Malvin V. Reyes | 2013-12-10 | JO# n/a
'						  DEV 2013-12-10 | PROD 2014-02-19
'							- Fixed the flexible layout.

'						* Malvin Reyes | 2014-03-03 | JO# JYYXXXXX
'						  DEV - 2014-03-03 | PROD 2014-05-13
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
'                           CA2000 : Microsoft.Reliability : In method 'statusreport.fnblnQueryData(statusreport.TransMode, Date, Date, String, String, ByRef DataTable, ByRef DataSet)', call System.IDisposable.Dispose on object '_dimSqlDataAdapter' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'statusreport.fnblnQueryData(statusreport.TransMode, Date, Date, String, String, ByRef DataTable, ByRef DataSet)', call System.IDisposable.Dispose on object '_dimclsClsConnection' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'statusreport.LoadCommStatus()', call System.IDisposable.Dispose on object '_pridttOutput' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'statusreport.Page_Load(Object, EventArgs)', object 'dt' is not disposed along all exception paths. Call System.IDisposable.Dispose on object 'dt' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************

Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports FliAuthLib

Partial Class statusreport
    Inherits AuthenticatedPageBase

    Private MyReport As ReportDocument
    Private _priclsClsConnection As clsConnection
    Private _prisqlDataAdapter As SqlDataAdapter
    Private _pridttOutput As DataTable

#Region "Declaring Variables"
    ' Your constant variables here
    Private Enum TransMode
        SalesWithoutCommission = 1
        CommissionStatus = 2
        Documents = 3
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
        Try
            MyReport = New ReportDocument
            _pridttOutput = New DataTable

            Dim _dimclsGlobalFunctions As New clsGlobalFunctions
            Dim strReportPath As String = String.Empty
            Dim _dimintMode As Integer

            If ddlTypesReport.SelectedValue = "Status" Then
                strReportPath = Server.MapPath("Reports/selrptCommissionStatus.rpt")
                _dimintMode = TransMode.CommissionStatus
            ElseIf ddlTypesReport.SelectedValue = "WithoutComm" Then
                strReportPath = Server.MapPath("Reports/selrptSalesWithoutCommission.rpt")
                _dimintMode = TransMode.SalesWithoutCommission
            ElseIf ddlTypesReport.SelectedValue = "DefDocuments" Then
                _dimintMode = TransMode.Documents
                strReportPath = Server.MapPath("Reports/selrptDefinitionOfDocuments.rpt")
            End If

            ' 2013-6-18 : use datable for report source
            Dim _dimstrErrorMessage As String = ""
            'Dim _pridttOutput As New DataTable

            If fnblnQueryData(_dimintMode, _
                              dtpDateFrom.SelectedDate, _
                              dtpDateTo.SelectedDate, _
                              _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelAgentCode")), _
                              _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelAgentCode")), _
                              _pridttOutput) = False Then
                Exit Sub
            End If

            MyReport.Load(strReportPath)
            MyReport.SetDataSource(_pridttOutput.Copy)

            Session.Add("StatusReport", MyReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))

            divPDFLoader.InnerHtml = "<iframe src='selPDFLoader.aspx?SessionID=" & "StatusReport" & _
                                     "#zoom=100' name=""resReport"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no""></iframe>"

        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            subErrorMessagePrompt(True, MyExceptionNotice)
        Finally
            If MyReport IsNot Nothing Then MyReport.Dispose()
            If _pridttOutput IsNot Nothing Then _pridttOutput.Dispose()
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
        _priclsClsConnection = New clsConnection
        fnblnQueryData = True

        Try
            '2013-9-18 from MyMSSQLServer2000ConnectionString to  MyMSSQLServer2000ComKioskConnectionString
            If _priclsClsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, _
                                                   MyMSSQLServer2000ComKioskConnectionString) = False Then
                subErrorMessagePrompt(True, "Error while connecting to server")
                fnblnQueryData = False

                Exit Function
            End If

            _prisqlDataAdapter = New SqlClient.SqlDataAdapter("SP_selrptOnlineCommissionStatusReports", _priclsClsConnection.MSSQLServer2000Connection)

            With _prisqlDataAdapter.SelectCommand
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
                    .AddWithValue("@pstrUserName", _parstrUserName)

                    .Add("@pintErrorNumber", SqlDbType.Int, 4).Value = 4444
                    .Item("@pintErrorNumber").Direction = ParameterDirection.Output
                    .Add("@pstrErrorMessage", SqlDbType.VarChar, 1000).Value = " "
                    .Item("@pstrErrorMessage").Direction = ParameterDirection.Output
                End With
            End With

            If _parDttOutput IsNot Nothing Then
                _prisqlDataAdapter.Fill(_parDttOutput)
            ElseIf _parDtsOutput IsNot Nothing Then
                _prisqlDataAdapter.Fill(_parDtsOutput)
            End If

            If _prisqlDataAdapter.SelectCommand.Parameters("@pintErrorNumber").Value <> MyMSSQLServer2000SuccessErrorNumber Then
                fnblnQueryData = False
                subErrorMessagePrompt(True, _prisqlDataAdapter.SelectCommand.Parameters("@pstrErrorMessage").Value)

                Exit Function
            End If

            subErrorMessagePrompt(False)
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)

            subErrorMessagePrompt(True, MyExceptionNotice)
            fnblnQueryData = False
        Finally
            If _priclsClsConnection IsNot Nothing Then _priclsClsConnection.Dispose()
            If _prisqlDataAdapter IsNot Nothing Then _prisqlDataAdapter.Dispose()
        End Try
    End Function

    Protected Function fnErrorCheck() As Boolean
        If ddlTypesReport.SelectedValue = "Status" Then
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

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|statusreport.aspx|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Dim dt As New DataTable()

                Try
                    dt.Columns.AddRange(New DataColumn(1) {New DataColumn("value"), New DataColumn("valueDesc")})
                    dt.Rows.Add("Status", "Commission Status")
                    dt.Rows.Add("WithoutComm", "Sales without Commission Set-up")
                    dt.Rows.Add("DefDocuments", "Definition of Documents")

                    With ddlTypesReport
                        .DataTextField = "valueDesc"
                        .DataValueField = "value"
                        .DataSource = dt
                        .DataBind()
                    End With
                Catch ex As Exception
                    subErrorMessagePrompt(True, MyExceptionNotice)

                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                Finally
                    If dt IsNot Nothing Then dt.Dispose()
                End Try
            End If
        End If
        'End If

        _dimclsGlobalFunctions = Nothing
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If MyReport IsNot Nothing Then MyReport.Dispose()
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
    Protected Sub ddlTypesReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTypesReport.SelectedIndexChanged
        divPDFLoader.InnerHtml = ""

        If ddlTypesReport.SelectedValue <> "Status" Then
            pnlTransactionDate.Style.Add("display", "none")
        Else
            pnlTransactionDate.Style.Clear()
        End If
    End Sub
#End Region
    'etc...
#End Region
End Class
