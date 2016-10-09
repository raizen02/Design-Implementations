'**************************************************************
'Programmer Name		: Malvin V. Reyes
'Date Created			: 2010-08-23
'Finished Date          : 
'Program Name           : Printing of Documents
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'						* Malvin  | 2010-08-23
'							- Add the Grid (ItemCommand Server Event) for the Contract List
'                   
'						* Malvin | 2010-09-06
'							- Upload to PRODUCTION SERVER for testing.

'						* Nestor S. Garais Jr | 2012-05-07 | JO# JYYXXXXX
'						  DEV 2012-05-07 | PROD N/A
'							- apply css on markup

'						* Malvin V. Reyes | 2013-11-29 | JO# JYYXXXXX
'						  DEV 2012-11-29 | PROD xxxx-xx-xx
'							- Fixed the flexible layout.

'						* Malvin Reyes | 2014-01-07 - JO# J1304070
'						  DEV - 2013-01-07 | PROD 2014-02-19
'							- Change the design layout based on recommendation of Mr. Reuel.
'							- Change the themes

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "LoginUser"
'                           - Remove the Session("sesSelVerified") checking
'                           - Remove the login process

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'						* Nestor Garais Jr | 2016-06-28 | JO# JYYXXXXX
'						  DEV - 2016-06-28| PROD  
'                           CA2000 : Microsoft.Reliability : In method 'printingdocuments.LoadGridContractList(String, String, String)', call System.IDisposable.Dispose on object '_dimdtsDataSet' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'printingdocuments.PopulateContractInfo(String, String)', call System.IDisposable.Dispose on object '_dimdtsDataSet' before all references to it are out of scope.
'                           CA2000 : Microsoft.Reliability : In method 'printingdocuments.LoadGridContractList(String, String, String)', call System.IDisposable.Dispose on object '_priclsConnection' before all references to it are out of scope.
'                                   Solution : Add try/catch and dispose all disposable objects in try/finally
'**************************************************************

Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports FliAuthLib

Partial Class printingdocuments
    Inherits AuthenticatedPageBase

#Region "Declaring Variables"
    ' Your constant variables here
    Private _prirepMyReport As ReportDocument
    Private _priclsConnection As clsConnection
    Private _priclsPrintingDocuments As clsPrintingDocuments
    Private _pridtsDataSet As DataSet
#End Region

#Region "     User Define Subs and Functions     "
#Region "Procedures"
    ' Place your Sub here
    Private Sub subErrorMessagePrompt(ByVal _parbolIsVisible As Boolean, _
                                      Optional ByVal _parstrErrorMsg As String = MyExceptionNotice, _
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

    Private Sub LoadGridContractList(ByVal _parstrUserName As String, _
                                     Optional ByVal _parstrContractNumber As String = "", _
                                     Optional ByVal _parstrCustomerName As String = "")
        _priclsConnection = New clsConnection
        _priclsPrintingDocuments = New clsPrintingDocuments
        _pridtsDataSet = New DataSet

        pnlDocumentLoader.Visible = False
        divPrintResult.Visible = False

        Try
            If _priclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                subErrorMessagePrompt(True)

                Exit Sub
            End If

            With _priclsPrintingDocuments
                Dim _dimintMode As Integer = 1 'List of Selers Contract

                .Connection = _priclsConnection.MSSQLServer2000Connection

                If .PrintingDocuments(clsPrintingDocuments.ExecuteCommand.ExecuteDataAdapter, _
                                      _dimintMode, _
                                      _parstrUserName, , _
                                      _parstrContractNumber, _
                                      _parstrCustomerName, ) = False Then
                    subErrorMessagePrompt(True, .ErrorMessage)

                    Exit Sub
                Else
                    .DataAdapter.Fill(_pridtsDataSet)   'Selers Contract
                    grdContractList.DataSource = _pridtsDataSet.Tables(0)
                    grdContractList.DataBind()
                End If
            End With

            subErrorMessagePrompt(False)
        Catch ex As Exception
            subErrorMessagePrompt(True)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsConnection IsNot Nothing Then _priclsConnection.Dispose()
            If _priclsPrintingDocuments IsNot Nothing Then _priclsPrintingDocuments.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()
        End Try
    End Sub 'LoadGridContractList

    Private Sub PopulateContractInfo(ByVal _parstrCompanyNumber As String, _
                                     ByVal _parstrContractnumber As String)
        _priclsConnection = New clsConnection
        _priclsPrintingDocuments = New clsPrintingDocuments
        _pridtsDataSet = New DataSet

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions
        Dim _dimintMode As Integer

        Try
            If _priclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                subErrorMessagePrompt(True)

                Exit Sub
            End If

            With _priclsPrintingDocuments
                _dimintMode = 2
                .Connection = _priclsConnection.MSSQLServer2000Connection

                If .PrintingDocuments(clsPrintingDocuments.ExecuteCommand.ExecuteDataAdapter, _
                                      _dimintMode, _
                                      _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")), _
                                      _parstrCompanyNumber, _
                                      _parstrContractnumber) = False Then
                    subErrorMessagePrompt(True, .ErrorMessage)

                    Exit Sub
                Else
                    .DataAdapter.Fill(_pridtsDataSet)   'Document List for Printing

                    If _pridtsDataSet.Tables(0).Rows.Count > 0 Then
                        'Contract Information
                        pnlContracts.Visible = False
                        pnlDocumentLoader.Visible = True

                        ViewState("vwsCompanyCode") = _pridtsDataSet.Tables(0).Rows(0).Item("conCompanyCode").ToString
                        ViewState("vwsContractNumber") = _pridtsDataSet.Tables(0).Rows(0).Item("conContractNumber").ToString

                        spnContractNumber.InnerHtml = _pridtsDataSet.Tables(0).Rows(0).Item("conContractNumber").ToString
                        spnCustomerName.InnerHtml = _pridtsDataSet.Tables(0).Rows(0).Item("conCustomerName").ToString
                        spnProject.InnerHtml = _pridtsDataSet.Tables(0).Rows(0).Item("conProjectName").ToString
                        spnPhase.InnerHtml = _pridtsDataSet.Tables(0).Rows(0).Item("conPhaseName").ToString
                        spnBlock.InnerHtml = _pridtsDataSet.Tables(0).Rows(0).Item("conBlockName").ToString
                        spnLot.InnerHtml = _pridtsDataSet.Tables(0).Rows(0).Item("conLotName").ToString

                        If _pridtsDataSet.Tables(1).Rows.Count = 0 Then
                            pnlOtherDocuments.Visible = False
                        Else
                            pnlOtherDocuments.Visible = True

                            grdOtherDocuments.DataSource = _pridtsDataSet.Tables(1)
                            grdOtherDocuments.DataBind()
                        End If

                        If _pridtsDataSet.Tables(2).Rows.Count = 0 Then
                            pnlDocuments.Visible = False
                        Else
                            pnlDocuments.Visible = True

                            grdDocuments.DataSource = _pridtsDataSet.Tables(2)
                            grdDocuments.DataBind()
                        End If
                    Else
                        pnlContracts.Visible = True
                        pnlDocumentLoader.Visible = False
                    End If
                End If
            End With

            subErrorMessagePrompt(False)
        Catch ex As Exception
            subErrorMessagePrompt(True)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsConnection IsNot Nothing Then _priclsConnection.Dispose()
            If _priclsPrintingDocuments IsNot Nothing Then _priclsPrintingDocuments.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()

            _dimclsGlobalFunctions = Nothing
        End Try
    End Sub 'PopulateContractInfo

    Private Sub PopulateReportInfo(ByVal _parstrReportCode As String, _
                                   ByVal _parstrPrintStatus As Integer)
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions
        Dim _dimstrSessionID As String

        _prirepMyReport = New ReportDocument

        ViewState("vwsReportCode") = _parstrReportCode
        '_prirepMyReport.Load("D:\System Development\Filinvest International\Reports\00000000004000001559D0004.rpt")
        _prirepMyReport.Load(MyDefaultReportPath & _parstrReportCode & ".rpt")

        Try
            If _parstrPrintStatus = 1 Then
                btnPrint.Visible = False
                pnlPDFLoader.Visible = True

                _dimstrSessionID = "sesrptReport" & _parstrReportCode
                Session.Add(_dimstrSessionID, _
                            _prirepMyReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))

                divPDFLoader.InnerHtml = "<iframe src='selPDFLoader.aspx?SessionID=" & _dimstrSessionID & _
                                         "#zoom=100' name=""resReport"" width=""100%"" height=""500"" align=""center"" border=""0"" frameborder=""1"" scrolling=""no""></iframe>"
            Else
                btnPrint.Visible = True
                pnlPDFLoader.Visible = False

                'rptLoader.ReportSource = _prirepMyReport
            End If

            divPrintResult.Visible = True
        Catch ex As Exception
            subErrorMessagePrompt(True)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _prirepMyReport IsNot Nothing Then _prirepMyReport.Dispose()

            _dimclsGlobalFunctions = Nothing
        End Try
    End Sub 'PopulateReportInfo

    Private Sub SavePrintStatus(ByVal _parstrUserName As String, _
                                ByVal _parstrUserFullName As String, _
                                ByVal _parstrCompanyCode As String, _
                                ByVal _parstrContractnumber As String, _
                                ByVal _parstrReportFileName As String)
        _priclsConnection = New clsConnection
        _priclsPrintingDocuments = New clsPrintingDocuments

        Dim _dimintMode As Integer
        Dim _dmclsConnectionFREBAS As New clsConnection

        Try
            If _priclsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ConnectionString) = False Then
                subErrorMessagePrompt(True)

                Exit Sub
            End If

            With _priclsPrintingDocuments
                _dimintMode = 3
                .Connection = _priclsConnection.MSSQLServer2000Connection
                .Transaction = _priclsConnection.MSSQLServer2000Connection.BeginTransaction

                If .PrintingDocuments(clsPrintingDocuments.ExecuteCommand.ExecuteNonQuery, _
                                      _dimintMode, _
                                      _parstrUserName, _
                                      _parstrCompanyCode, _
                                      _parstrContractnumber, , _
                                      _parstrReportFileName) = False Then
                    .Transaction.Rollback()

                    subErrorMessagePrompt(True, .ErrorMessage)

                    Exit Sub
                Else
                    If .ErrorNumber <> MyMSSQLServer2000SuccessErrorNumber Then
                        .Transaction.Rollback()

                        subErrorMessagePrompt(True, .ErrorMessage)

                        Exit Sub
                    End If

                    If _dmclsConnectionFREBAS.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000FREBASConnectionString) = False Then
                        subErrorMessagePrompt(True)

                        Exit Sub
                    End If

                    .ConnectionFREBAS = _dmclsConnectionFREBAS.MSSQLServer2000Connection
                    .TransactionFREBAS = _dmclsConnectionFREBAS.MSSQLServer2000Connection.BeginTransaction

                    If Left(Right(_parstrReportFileName, 5), 1) = "D" Then
                        _dimintMode = 2
                    Else
                        _dimintMode = 1
                    End If

                    If .PrintingDocumentsSaveDispatch(clsPrintingDocuments.ExecuteCommand.ExecuteNonQuery, _
                                                      _dimintMode, _
                                                      _parstrUserFullName, _
                                                      _parstrCompanyCode, _
                                                      _parstrContractnumber, _
                                                      .ReportDocuCode) = False Then
                        .Transaction.Rollback()
                        .TransactionFREBAS.Rollback()

                        subErrorMessagePrompt(True, .ErrorMessage)

                        Exit Sub
                    Else
                        If .ErrorNumber <> MyMSSQLServer2000SuccessErrorNumber Then
                            .Transaction.Rollback()
                            .TransactionFREBAS.Rollback()

                            subErrorMessagePrompt(True, .ErrorMessage)

                            Exit Sub
                        End If

                        .Transaction.Commit()
                        .TransactionFREBAS.Commit()

                        PopulateReportInfo(_parstrReportFileName, 1)
                        subErrorMessagePrompt(False)
                    End If
                End If
            End With
        Catch ex As Exception
            subErrorMessagePrompt(True)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsConnection IsNot Nothing Then _priclsConnection.Dispose()
            If _priclsPrintingDocuments IsNot Nothing Then _priclsPrintingDocuments.Dispose()
            If _pridtsDataSet IsNot Nothing Then _pridtsDataSet.Dispose()

            If _dmclsConnectionFREBAS IsNot Nothing Then _dmclsConnectionFREBAS.Dispose()
        End Try
    End Sub 'SavePrintStatus
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
        EO.Web.Runtime.DebugLevel = 0

        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        SessionAPI.RefreshSessions(CurrentUser)

        If Not _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|printingdocuments.aspx|") Then
            Response.Redirect("errorPage.aspx")

            Exit Sub
        Else
            If Page.IsPostBack = False Then
                LoadGridContractList(_dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")))
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
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        LoadGridContractList(_dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")), , txbSearch.Text)

        pnlContracts.Visible = True
        pnlDocumentLoader.Visible = False

        _dimclsGlobalFunctions = Nothing
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        'pnlPrintButton.Visible = False
        Call SavePrintStatus(_dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")), _
                             _dimclsGlobalFunctions.fnDeCrypt(Session("sesSelFullname")), _
                             ViewState("vwsCompanyCode"), _
                             ViewState("vwsContractNumber"), _
                             ViewState("vwsReportCode"))

        Call PopulateContractInfo(ViewState("vwsCompanyCode"), _
                                  ViewState("vwsContractNumber"))

        _dimclsGlobalFunctions = Nothing
    End Sub 'btnPrint_Click
#End Region

#Region "Grid"
    ' Events in all Grid
    Protected Overrides Sub Render(ByVal textWriter As System.Web.UI.HtmlTextWriter)
        For Each gvRow As GridViewRow In grdContractList.Rows
            If gvRow.RowType = DataControlRowType.DataRow Then
                gvRow.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(grdContractList, "Select$" & gvRow.RowIndex, True)
                gvRow.ToolTip = "Click to select this row."
            End If
        Next

        For Each gvRow As GridViewRow In grdOtherDocuments.Rows
            If gvRow.RowType = DataControlRowType.DataRow Then
                gvRow.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(grdOtherDocuments, "Select$" & gvRow.RowIndex, True)
                gvRow.ToolTip = "Click to select this row."
            End If
        Next

        For Each gvRow As GridViewRow In grdDocuments.Rows
            If gvRow.RowType = DataControlRowType.DataRow Then
                gvRow.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(grdDocuments, "Select$" & gvRow.RowIndex, True)
                gvRow.ToolTip = "Click to select this row."
            End If
        Next
        'End If

        MyBase.Render(textWriter)
    End Sub

    Protected Sub grdContractList_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdContractList.SelectedIndexChanged
        For Each row As GridViewRow In grdContractList.Rows
            If row.RowIndex = grdContractList.SelectedIndex Then
                PopulateContractInfo(row.Cells(0).Text, row.Cells(1).Text)
                divPrintResult.Visible = False
            End If
        Next
    End Sub

    Protected Sub grdOtherDocuments_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdOtherDocuments.SelectedIndexChanged
        For Each row As GridViewRow In grdOtherDocuments.Rows
            If row.RowIndex = grdOtherDocuments.SelectedIndex Then
                PopulateContractInfo(ViewState("vwsCompanyCode"), ViewState("vwsContractNumber"))
                PopulateReportInfo(row.Cells(0).Text, row.Cells(1).Text)
            End If
        Next
    End Sub

    Protected Sub grdDocuments_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdDocuments.SelectedIndexChanged
        For Each row As GridViewRow In grdDocuments.Rows
            If row.RowIndex = grdDocuments.SelectedIndex Then
                PopulateContractInfo(ViewState("vwsCompanyCode"), ViewState("vwsContractNumber"))
                PopulateReportInfo(row.Cells(0).Text, row.Cells(1).Text)
            End If
        Next
    End Sub

    Protected Sub grdContractList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdContractList.PageIndexChanging
        Dim _dimclsGlobalFunctions As New clsGlobalFunctions

        LoadGridContractList(_dimclsGlobalFunctions.fnDeCrypt(Session("sesSelUser")), , txbSearch.Text)
        _dimclsGlobalFunctions = Nothing

        grdContractList.PageIndex = e.NewPageIndex
        grdContractList.DataBind()
    End Sub
#End Region

    'etc...
#End Region
End Class