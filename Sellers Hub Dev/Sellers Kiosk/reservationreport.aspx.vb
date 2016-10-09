'**************************************************************
'Programmer Name		: Malvin Reyes
'Date Created			: 2014-02-10
'Finished Date          : 2014-02-10
'Program Name           : Online Reservation Reports
'Program Description    : 
'Stored Procedure       : 
'						  DEV - 2014-02-10 | PROD 2014-02-19
'Updates Information
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
'                               CA2000 : Microsoft.Reliability : In method 'reservationreport.PopulateReport(Integer, String)', call System.IDisposable.Dispose on object '_dimclsConnection' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'reservationreport.PopulateReport(Integer, String)', call System.IDisposable.Dispose on object '_dimclsReservationReport' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'reservationreport.PopulateReport(Integer, String)', call System.IDisposable.Dispose on object '_dimdtsDataSet' before all references to it are out of scope.
'                               CA2000 : Microsoft.Reliability : In method 'reservationreport.PopulateReport(Integer, String)', call System.IDisposable.Dispose on object '_dimdtsDataSet' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************
 
Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports FliAuthLib

Public Class reservationreport
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
    Private _prirepMyReport As ReportDocument
    Private _priSqlDataAdapter As SqlDataAdapter
    Private _priclsClsConnection As clsConnection
    Private _priclsReservationReport As clsReservationReport
    Private _pridtsDataSet As DataSet
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

    Private Sub PopulateReport(ByVal _parintMode As Integer, _
                               ByVal _parstrUserName As String)
        Dim _dimstrSessionID As String

        subErrorMessagePrompt(False)

        Try
            _prirepMyReport = New ReportDocument
            _priclsReservationReport = New clsReservationReport
            _priclsClsConnection = New clsConnection

            If _priclsClsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                subErrorMessagePrompt(True)

                Exit Sub
            End If

            With _priclsReservationReport
                Dim _dimintMode As Integer = CInt(Request.QueryString("ReportCode")) 'Select Report Filename

                .Connection = _priclsClsConnection.MSSQLServer2000Connection

                If .ReportsInfo(clsReservationReport.ExecuteCommand.ExecuteDataAdapter, 0, _parstrUserName) = False Then
                    divPDFLoader.Visible = False
                    subErrorMessagePrompt(True, .ErrorMessage.ToString)

                    Exit Sub
                Else
                    _pridtsDataSet = New DataSet
                    .DataAdapter.Fill(_pridtsDataSet)

                    Dim _dimstrReportFile As String
                    Dim _dimstrReportNameHeader As String

                    If _pridtsDataSet.Tables(0).Select("[*ReportModeID] =" & _dimintMode).Length > 0 Then
                        Me.Title = _pridtsDataSet.Tables(0).Select("[*ReportModeID] =" & _dimintMode)(0).Item("Report Mode").ToString & " | Reservation Report"
                        spnReportTitle.InnerHtml = _pridtsDataSet.Tables(0).Select("[*ReportModeID] =" & _dimintMode)(0).Item("Report Mode").ToString
                        _dimstrReportFile = _pridtsDataSet.Tables(0).Select("[*ReportModeID] =" & _dimintMode)(0).Item("*ReportFile").ToString.Trim
                        _dimstrReportNameHeader = _pridtsDataSet.Tables(0).Select("[*ReportModeID] =" & _dimintMode)(0).Item("Report Mode").ToString.Trim

                        If .ReportsInfo(clsReservationReport.ExecuteCommand.ExecuteDataAdapter, _dimintMode, _parstrUserName) = False Then
                            divPDFLoader.Visible = False
                            subErrorMessagePrompt(True, .ErrorMessage.ToString)

                            Exit Sub
                        Else
                            _pridtsDataSet = New DataSet
                            .DataAdapter.Fill(_pridtsDataSet)

                            If _pridtsDataSet.Tables(0).Rows.Count > 0 Then
                                '_prirepMyReport = New ReportDocument
                                With _prirepMyReport
                                    .Load(Server.MapPath("Reports/" & _dimstrReportFile))

                                    .SetDataSource(_pridtsDataSet.Tables(0))

                                    If _dimintMode <> 10 Then
                                        .SetParameterValue("ReportNameHeader", _dimstrReportNameHeader)
                                    End If
                                End With

                                With _prirepMyReport.FormatEngine.PrintOptions
                                    .PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
                                    .PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
                                End With

                                _dimstrSessionID = "sesrptReport" & Request.QueryString("ReportCode")
                                Session.Add(_dimstrSessionID, _prirepMyReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))

                                divPDFLoader.InnerHtml = "<iframe src='selPDFLoader.aspx?SessionID=" & _dimstrSessionID & _
                                                         "#zoom=100' name=""resReport"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no""></iframe>"
                            Else
                                Session.Add("sesErrorMessage", "No record found.")
                                divPDFLoader.InnerHtml = "<iframe src='selPDFLoader.aspx' " & _
                                                         "name=""resReport"" width=""100%"" height=""50"" align=""center""  border=""0"" frameborder=""0""  scrolling=""no""></iframe>"
                            End If
                        End If
                    Else
                        divPDFLoader.Visible = False
                        subErrorMessagePrompt(True, "Sorry, you are not authorized!")

                        Exit Sub
                    End If
                End If
            End With
        Catch ex As Exception
            subErrorMessagePrompt(True, MyExceptionNotice)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priSqlDataAdapter IsNot Nothing Then _priSqlDataAdapter.Dispose()
            If _priclsClsConnection IsNot Nothing Then _priclsClsConnection.Dispose()
            If _priclsReservationReport IsNot Nothing Then _priclsReservationReport.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()

            If _prirepMyReport IsNot Nothing Then _prirepMyReport.Dispose()
        End Try
    End Sub
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

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|ReservationReport.aspx?ReportCode=" & Request.QueryString("ReportCode") & "|") Then
            _dimclsGlobalFunctions = Nothing
            Response.Redirect("errorPage.aspx")

            Exit Sub
        Else
            If IsNumeric(Request.QueryString("ReportCode")) = 0 Then
                divPDFLoader.Visible = False
                subErrorMessagePrompt(True, "Sorry, you are not authorized!")
            Else
                divPDFLoader.Visible = True

                PopulateReport(Request.QueryString("ReportCode"), _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")))
            End If
        End If

        _dimclsGlobalFunctions = Nothing
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If _prirepMyReport IsNot Nothing Then _prirepMyReport.Dispose()
    End Sub 'Page_Unload
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
