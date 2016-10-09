'**************************************************************
'Programmer Name		: Nestor S. Garais Jr
'Date Created			: 2014-XX-XX
'Finished Date          : 2014-02-04
'Program Name           : commissionreport
'Program Description    : 
'Stored Procedure       : 
'Remarks			    : DEV - 2014-02-04 | PROD - 2014-02-19

'Updates Information
'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking
'						* Nestor Garais Jr | 2014-04-28 | JO# J1400906
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change layout of report
'                           - Add lacking requirement list subreport(LackingRequirements) in the selrptCommissionReport report

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add try/catch on every function and procedure then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************

Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports FliAuthLib

Partial Class commissionreport
    Inherits AuthenticatedPageBase

    Private _pridtsTable As DataSet
    Private _pridtsTableMain As DataSet
    Private _priclsCommissReport As clsCommissReport
    Private _priclsClsConnection As clsConnection
    Private _priclsGlobalFunctions As clsGlobalFunctions

#Region "     Declaring Variables     "
    ' Your constant variables here
    Private Class MsgCss
        Public Const Validation As String = "alert alert-info" '"validation_box"
        Public Const Success As String = "alert alert-success" '"success_box"
        Public Const Errors As String = "alert alert-error" '"error_box"
        Public Const Warning As String = "alert alert-block" '"warning_box"
        Public Const Info As String = "alert alert-info" '"info_box"
    End Class

    Private Enum TransMode
        Mod01SelectExistingCommissionWithPaging = 1
        Mod02SelectDataOfSelectedContract = 2
        Mod03SelectVoucherDetails = 3
        Mod04SelectVoucherDetailsRpt = 4
        Mod99ForPrintRpt = 99
    End Enum
#End Region

#Region "     User Define Subs and Functions     "
#Region "   Procedures  "
    Private Sub PopulateSalesList(ByVal _pintPageNumber As Integer)
        Dim _dimStrErrorMessage As String = ""

        '>>---- Populate Grid View  -------
       Try
            _pridtsTable = New DataSet

            If fnblnQueryData(TransMode.Mod01SelectExistingCommissionWithPaging, _
                              _dimStrErrorMessage, , _
                              _pridtsTable, , , , , , _
                              txbSearch.Text, _
                              _pintPageNumber, _
                              ddlPageMaxSize.SelectedValue) = False Then

                ShowMsgBox(_dimStrErrorMessage, MsgCss.Errors)

                Exit Sub
            End If

            grdSalesList.DataSource = _pridtsTable.Tables(0)
            grdSalesList.DataBind()
            '<<----- Populate Grid View  -------

            'Hide Report Panel
            pnlPrintPreview.Visible = False

            ' Hide Page Info
            If _pridtsTable.Tables(0).Rows.Count = 0 Then
                divPagination.Visible = False
                divButtons.Visible = False

                Exit Sub
            Else
                ddlPageNumberList.Visible = True
                divButtons.Visible = True
            End If

            '>>---- Populate Page Number List  -------
            If _pintPageNumber = 1 Then
                ddlPageNumberList.DataSource = _pridtsTable.Tables(1)
                ddlPageNumberList.DataTextField = "PageNumber"
                ddlPageNumberList.DataValueField = "PageNumber"
                ddlPageNumberList.DataBind()
            End If
            '<<---- Populate Page Number List  -------

            '>>---- Display Table result info  -------
            If _pridtsTable.Tables(2).Rows.Count > 0 Then
                spanTableInfo.InnerHtml = _pridtsTable.Tables(2).Rows(0).Item(0)
            Else
                spanTableInfo.InnerHtml = ""
            End If
            '<<---- Display Table result info   -------
        Catch ex As Exception
            ShowMsgBox(MyExceptionNotice, MsgCss.Errors)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _pridtsTable IsNot Nothing Then _pridtsTable.Dispose()
        End Try

    End Sub

    ' Display error message
    Private Sub ShowMsgBox(ByVal _parstrMessage As String, _
                           ByVal _parstrMsgCss As String, _
                           Optional ByVal _parVldSumm As Control = Nothing, _
                           Optional ByVal _parstrHref As String = "")
        If _parVldSumm Is Nothing And _parstrMessage = "" Then
            divErrorMsgBox.Visible = False

            Exit Sub
        End If

        If _parstrHref <> "" Then
            _parstrHref = String.Format("<a href='#{0}'> see item</a>", _parstrHref)
        End If

        If _parstrMessage = "" Then
            _parstrHref = ""
        End If

        divErrorMsgBox.Visible = True
        divErrorMsgBox.Attributes("class") = _parstrMsgCss.ToString
        divErrorMsg.InnerHtml = _parstrMessage & _parstrHref

        If _parVldSumm IsNot Nothing Then _
            divErrorMsg.Controls.Add(_parVldSumm)
    End Sub
#End Region

#Region "   Functions  "
    ' Place your Functions here
    Private Function fnblnQueryData(ByVal _parintMode As TransMode, _
                                    Optional ByRef _parstrErrorMessage As String = "", _
                                    Optional ByRef _parDttOutput As DataTable = Nothing, _
                                    Optional ByRef _parDtsOutput As DataSet = Nothing, _
                                    Optional ByVal _parstrCompanyCode As String = "", _
                                    Optional ByVal _parstrContractNumber As String = "", _
                                    Optional ByVal _parstrAgentCode As String = "", _
                                    Optional ByVal _parstrVoucherNum As String = "", _
                                    Optional ByVal _parstrSearchField As String = "", _
                                    Optional ByVal _parstrSearchValue As String = "", _
                                    Optional ByVal _parintPageNo As Integer = 1, _
                                    Optional ByVal _parintMaxPageSize As Integer = 10) As Boolean
        fnblnQueryData = False

        Try
            _priclsCommissReport = New clsCommissReport
            _priclsClsConnection = New clsConnection
            _priclsGlobalFunctions = New clsGlobalFunctions

            If _priclsClsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ComKioskConnectionString) = False Then
                _parstrErrorMessage = "Error while connecting to server"

                Exit Function
            End If
            _priclsCommissReport.SQLConnection = _priclsClsConnection.MSSQLServer2000Connection

            If _priclsCommissReport.ExeCuteAko(clsCommissReport.ExecuteCommand.ExecuteDataAdapter, _
                                               _priclsGlobalFunctions.fnDeCrypt(Session("sesSelAgentCode")), _
                                               _parintMode, _
                                               _parstrCompanyCode, _
                                               _parstrContractNumber, _
                                               _parstrAgentCode, _
                                               _parstrVoucherNum, _
                                               _parstrSearchField, _
                                               _parstrSearchValue, _
                                               _parintPageNo, _
                                               _parintMaxPageSize) = False Then
                _parstrErrorMessage = _priclsCommissReport.SQLMessage

                Exit Function
            End If

            If _parDttOutput IsNot Nothing Then
                _priclsCommissReport.SQLDataAdapter.Fill(_parDttOutput)
            ElseIf _parDtsOutput IsNot Nothing Then
                _priclsCommissReport.SQLDataAdapter.Fill(_parDtsOutput)
            End If

            If _priclsCommissReport.SQLDataAdapter.SelectCommand.Parameters("@pintErrorNumber").Value <> 8888 Then
                _parstrErrorMessage = _priclsCommissReport.SQLDataAdapter.SelectCommand.Parameters("@pstrErrorMessage").Value

                Exit Function
            End If

            fnblnQueryData = True
        Catch ex As Exception
            _parstrErrorMessage = ex.Message.ToString
            fnblnQueryData = False

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsCommissReport IsNot Nothing Then _priclsCommissReport.Dispose()
            If _priclsClsConnection IsNot Nothing Then _priclsClsConnection.Dispose()
        End Try
    End Function
#End Region
#End Region

#Region "     Events of Object     "
    ' Group the events of every control

#Region "Form"
    ' Events of Main form
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Initialize error message box
            ShowMsgBox("", "")

            _priclsGlobalFunctions = New clsGlobalFunctions

            SessionAPI.RefreshSessions(CurrentUser)

            If Not _priclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|commissionreport.aspx|") Then
                _priclsGlobalFunctions = Nothing
                Response.Redirect("errorPage.aspx")

                Exit Sub
            Else
                If Not (Page.IsPostBack) Then
                    'populate grid view sales list, start at page 1
                    Call PopulateSalesList(1)
                End If
            End If

            _priclsGlobalFunctions = Nothing
        Catch ex As Exception
            ShowMsgBox(MyExceptionNotice, MsgCss.Errors)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Page.Validate("vdgHold")
    End Sub 'Page_PreRender

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    SqlConnection.ClearAllPools()
    'End Sub 'Page_Unload
#End Region 'Form

#Region "Button"
    ' Events in all Buttons
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call PopulateSalesList(1)
    End Sub

    Protected Sub ancPrevPage_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ancPrevPage.ServerClick
        pnlPrintPreview.Visible = False

        If ddlPageNumberList.SelectedIndex = 0 Then Exit Sub

        ddlPageNumberList.SelectedIndex = ddlPageNumberList.SelectedValue - 2

        Call PopulateSalesList(ddlPageNumberList.SelectedValue)
    End Sub

    Protected Sub ancNextPage_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ancNextPage.ServerClick
        pnlPrintPreview.Visible = False

        If ddlPageNumberList.SelectedValue = ddlPageNumberList.Items.Count Then Exit Sub

        ddlPageNumberList.SelectedIndex = ddlPageNumberList.SelectedValue

        Call PopulateSalesList(ddlPageNumberList.SelectedValue)
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim _dimstrContractNumber As String
        Dim _dimstrCompanyCode As String
        Dim _dimstrAgentCode As String
        Dim _dimblnIsCheckExists As Boolean = False

        For _forCtr As Integer = 0 To grdSalesList.Rows.Count - 1
            If grdSalesList.Rows(_forCtr).Cells(0).Visible = False Then Continue For
            If CType(grdSalesList.Rows(_forCtr).Cells(0).FindControl("CheckBoxPrint"), CheckBox).Checked = False Then Continue For

            _dimblnIsCheckExists = True
        Next

        If _dimblnIsCheckExists = False Then
            pnlPrintPreview.Visible = False
            ShowMsgBox("No checked contract", MsgCss.Errors)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "tag", "var aTag = $(""a[name='aTagAlert']""); $('html,body,window').animate({scrollTop: aTag.offset().top}, 1000);", True)

            Exit Sub
        End If

        Dim _dimStrErrorMessage As String = ""

        _pridtsTableMain = New DataSet

        Dim _dimrptVoucherDetails As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        Try
            For _forCtr As Integer = 0 To grdSalesList.Rows.Count - 1
                If grdSalesList.Rows(_forCtr).Cells(0).Visible = False Then Continue For
                If CType(grdSalesList.Rows(_forCtr).Cells(0).FindControl("CheckBoxPrint"), CheckBox).Checked = False Then Continue For

                _dimstrCompanyCode = grdSalesList.Rows(_forCtr).Cells(2).Text ' 2 : CoCode
                _dimstrContractNumber = grdSalesList.Rows(_forCtr).Cells(3).Text ' 3 : Acct. Code
                _dimstrAgentCode = grdSalesList.Rows(_forCtr).Cells(4).Text ' 4 : AgentCode

                _pridtsTable = New DataSet

                If fnblnQueryData(TransMode.Mod99ForPrintRpt, _
                                  _dimStrErrorMessage, , _
                                  _pridtsTable, _
                                  _dimstrCompanyCode, _
                                  _dimstrContractNumber, _
                                  _dimstrAgentCode) = False Then
                    ShowMsgBox(_dimStrErrorMessage, MsgCss.Errors)

                    Exit Sub
                End If

                If _pridtsTableMain.Tables.Count = 0 Then
                    _pridtsTableMain = _pridtsTable.Copy
                Else
                    '' Copy all rows to designated tables
                    For _forTableCtr As Integer = 0 To _pridtsTableMain.Tables.Count - 1
                        For _forRowCtr As Integer = 0 To _pridtsTable.Tables(_forTableCtr).Rows.Count - 1
                            _pridtsTableMain.Tables(_forTableCtr).ImportRow(_pridtsTable.Tables(_forTableCtr).Rows(_forRowCtr))
                        Next
                    Next
                End If
            Next

            If _pridtsTableMain.Tables.Count > 0 Then
                pnlPrintPreview.Visible = True

                _dimrptVoucherDetails.Load(Server.MapPath("Reports\selrptCommissionReport.rpt"), CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy)
                _dimrptVoucherDetails.Subreports("Documents").SetDataSource(_pridtsTableMain.Tables(1).Copy)
                _dimrptVoucherDetails.Subreports("CommissionsProccessed").SetDataSource(_pridtsTableMain.Tables(2).Copy)
                _dimrptVoucherDetails.Subreports("CommissionMileStone").SetDataSource(_pridtsTableMain.Tables(3).Copy)
                _dimrptVoucherDetails.Subreports("BuyerPayment").SetDataSource(_pridtsTableMain.Tables(4).Copy)
                _dimrptVoucherDetails.Subreports("LackingRequirements").SetDataSource(_pridtsTableMain.Tables(5).Copy)
                _dimrptVoucherDetails.SetDataSource(_pridtsTableMain.Tables(0).Copy)

                Session("rptCommissionDetails") = _dimrptVoucherDetails.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
                ifrmPrintPreview.Attributes("src") = "selPDFLoader.aspx?SessionID=rptCommissionDetails"

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "tag", "var aTag = $(""a[name='aTagPrintPanel']""); $('html,body,window').animate({scrollTop: aTag.offset().top}, 1000);", True)
            End If

        Catch ex As Exception
            ShowMsgBox(MyExceptionNotice, MsgCss.Errors)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _pridtsTable IsNot Nothing Then _pridtsTable.Dispose()
            If _pridtsTableMain IsNot Nothing Then _pridtsTableMain.Dispose()
            If _dimrptVoucherDetails IsNot Nothing Then _dimrptVoucherDetails.Dispose()
        End Try
    End Sub

    Protected Sub btnCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckAll.Click
        For _forCtr As Integer = 0 To grdSalesList.Rows.Count - 1
            If grdSalesList.Rows(_forCtr).Cells(0).FindControl("CheckBoxPrint").Visible = False Then Continue For

            CType(grdSalesList.Rows(_forCtr).Cells(0).FindControl("CheckBoxPrint"), CheckBox).Checked = True
        Next

        pnlPrintPreview.Visible = False
    End Sub

    Protected Sub btnClearCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearCheck.Click
        For _forCtr As Integer = 0 To grdSalesList.Rows.Count - 1
            If grdSalesList.Rows(_forCtr).Cells(0).FindControl("CheckBoxPrint").Visible = False Then Continue For

            CType(grdSalesList.Rows(_forCtr).Cells(0).FindControl("CheckBoxPrint"), CheckBox).Checked = False
        Next

        pnlPrintPreview.Visible = False
    End Sub

#End Region 'Button

#Region "Grid"
    ' Events in all Grid 
    Protected Sub grdSalesList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSalesList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes("onclick") = String.Format("javascript:__doPostBack('{0}','Select${1}')", grdSalesList.UniqueID, e.Row.RowIndex)
            e.Row.Attributes("style") = e.Row.Cells(5).Text ' 5 : style
            e.Row.Cells(0).FindControl("CheckBoxPrint").Visible = CBool(e.Row.Cells(6).Text) ' 6 : is main unit
        End If
    End Sub
#End Region 'Grid

#Region "Dropdown List"
    ' Events in all Dropdown List
    Protected Sub ddlPageMaxSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageMaxSize.SelectedIndexChanged
        Call PopulateSalesList(1)
    End Sub

    Protected Sub ddlPageNumberList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumberList.SelectedIndexChanged
        Call PopulateSalesList(ddlPageNumberList.SelectedValue)
    End Sub
#End Region 'Dropdown List

#Region "CheckBOx"


#End Region

    'etc...
#End Region
End Class
