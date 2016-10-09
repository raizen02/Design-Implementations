'**************************************************************
'Programmer Name		: Nestor S. Garais Jr
'Date Created			: 2013-12-16
'Finished Date          : 2014-02-03
'Program Name           : commissionkiosk
'Program Description    : 
'Stored Procedure       : 
'Updates Information
'                       * Nestor Garais Jr | 2014-02-03 | (JO# XXXXXXXX)
'						  DEV - 2014-02-03 | PROD 2014-02-19
'							- Apply smooth scroll on focus.. 
'							- Remove Column ProjGroup
'							- Remove Column CoCode
'							- Reset the paging if select another contract
'							- Hide the commission details upon loading
'							- Hide and reset the voucher details selection change
'							- No response on the paging of voucher
'							- Hide the main paging if no result found.
'							- Add background color to inactive contract

'						* Malvin Reyes | 2014-03-04 | JO# JYYXXXXX
'						  DEV - 2014-03-04 | PROD 2014-05-13
'							- Implement the SSO process.
'                           - Include the "Inherits AuthenticatedPageBase" 
'                           - Include the "SessionAPI.RefreshSessions(CurrentUser)"
'                           - Remove the Session("sesSelVerified") checking

'						* Malvin Reyes | 2014-04-28 | JO# JYYXXXXX
'						  DEV - 2014-04-28 | PROD 2014-05-13
'							- Change the Title for Google Analytics

'Updates Information	: Nestor Garais Jr | (JO# XXXXXXXX)
'Date Updated		    : 2016-06-23
'Changes			    : - Add try/catch on every function and procedure then dispose all disposable object on try/finally
'Remarks			    : DEV - 2016-06-23 | PROD - 
'***********************************************************************

Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports FliAuthLib

Partial Class commissionkiosk
    Inherits AuthenticatedPageBase


#Region "     Declaring Variables     "
    ' Your constant variables here
    Private _priclsCommissKiosk As clsCommissKiosk
    Private _priclsClsConnection As clsConnection
    Private _priclsGlobalFunctions As clsGlobalFunctions
    Private _pridtsTable As DataSet
    Private _pridttTable As DataTable

    Private Class MsgCss
        Public Const Validation As String = "alert alert-info" '"validation_box"
        Public Const Success As String = "alert alert-success" '"success_box"
        Public Const Errors As String = "alert alert-error" '"error_box"
        Public Const Warning As String = "alert alert-block" '"warning_box"
        Public Const Info As String = "alert alert-info" '"info_box"
    End Class

    Private Enum TransMode
        Mod00SelectExistingCommission = 0
        Mod01SelectExistingCommissionWithPaging = 1
        Mod02SelectDataOfSelectedContract = 2
        Mod03SelectVoucherDetails = 3
        Mod04SelectVoucherDetailsRpt = 4
    End Enum

    Private Enum TabPages
        Buyer = 1
        Commission = 2
        Voucher = 3
        Documents = 4
    End Enum
#End Region

#Region "     User Define Subs and Functions     "
#Region "   Procedures  "
    Private Sub PopulateSalesList(ByVal _pintPageNumber As Integer)
        Dim _dimstrErrorMessage As String = ""

        '>>---- Populate Grid View  -------
        Try
            _pridtsTable = New DataSet

            If fnblnQueryData(TransMode.Mod01SelectExistingCommissionWithPaging, _
                              _dimstrErrorMessage, , _
                              _pridtsTable, , , , , , _
                              txbSearch.Text, _
                              _pintPageNumber, _
                              ddlPageMaxSize.SelectedValue) = False Then
                ShowMsgBox(_dimstrErrorMessage, MsgCss.Errors)

                Exit Sub
            End If

            grdSalesList.DataSource = _pridtsTable.Tables(0)
            grdSalesList.DataBind()
            '<<----- Populate Grid View  -------

            ' Hide tab pages
            pnlTabControl.Visible = False

            ' Hide Page Info
            If _pridtsTable.Tables(0).Rows.Count = 0 Then
                divPagination.Visible = False

                Exit Sub
            Else
                ddlPageNumberList.Visible = True
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
                           Optional ByVal _parctrVldSumm As Control = Nothing, _
                           Optional ByVal _parstrHref As String = "")
        If _parctrVldSumm Is Nothing And _parstrMessage = "" Then
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

        If _parctrVldSumm IsNot Nothing Then _
            divErrorMsg.Controls.Add(_parctrVldSumm)

        'divErrorMsgBox.Focus()
    End Sub

    Protected Sub FocusTab(ByVal _parIntTabPage As Integer)
        liTabBuyer.Attributes("class") = ""
        liTabCommission.Attributes("class") = ""
        liTabDocuments.Attributes("class") = ""
        liTabVouchers.Attributes("class") = ""

        tabBuyer.Attributes("class") = "tab-pane"
        tabCommission.Attributes("class") = "tab-pane"
        tabVouchers.Attributes("class") = "tab-pane"
        tabDocuments.Attributes("class") = "tab-pane"

        Select Case _parIntTabPage
            Case TabPages.Buyer
                liTabBuyer.Attributes("class") = "active"
                tabBuyer.Attributes("class") = "tab-pane active"
            Case TabPages.Commission
                liTabCommission.Attributes("class") = "active"
                tabCommission.Attributes("class") = "tab-pane active"
            Case TabPages.Documents
                liTabDocuments.Attributes("class") = "active"
                tabDocuments.Attributes("class") = "tab-pane active"
            Case TabPages.Voucher
                liTabVouchers.Attributes("class") = "active"
                tabVouchers.Attributes("class") = "tab-pane active"
        End Select

        pnlVoucherPrintPreview.Visible = False
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
            _priclsCommissKiosk = New clsCommissKiosk
            _priclsClsConnection = New clsConnection
            _priclsGlobalFunctions = New clsGlobalFunctions

            If _priclsClsConnection.OpenConnection(clsConnection.Database.MSSQLServer2000, MyMSSQLServer2000ComKioskConnectionString) = False Then
                _parstrErrorMessage = "Error while connecting to server"

                Exit Function
            End If

            _priclsCommissKiosk.SQLConnection = _priclsClsConnection.MSSQLServer2000Connection

            If _priclsCommissKiosk.ExeCuteAko(clsCommissKiosk.ExecuteCommand.ExecuteDataAdapter, _
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
                _parstrErrorMessage = _priclsCommissKiosk.SQLMessage

                Exit Function
            End If

            If _parDttOutput IsNot Nothing Then
                _priclsCommissKiosk.SQLDataAdapter.Fill(_parDttOutput)
            ElseIf _parDtsOutput IsNot Nothing Then
                _priclsCommissKiosk.SQLDataAdapter.Fill(_parDtsOutput)
            End If

            If _priclsCommissKiosk.SQLDataAdapter.SelectCommand.Parameters("@pintErrorNumber").Value <> 8888 Then
                _parstrErrorMessage = _priclsCommissKiosk.SQLDataAdapter.SelectCommand.Parameters("@pstrErrorMessage").Value

                Exit Function
            End If

            fnblnQueryData = True
        Catch ex As Exception
            _parstrErrorMessage = MyExceptionNotice
            fnblnQueryData = False

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _priclsCommissKiosk IsNot Nothing Then _priclsCommissKiosk.Dispose()
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

            If Not _priclsGlobalFunctions.fnDeCrypt(Session("sesSelRole")).Contains("|commissionkiosk.aspx|") Then
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
        pnlTabControl.Visible = False

        If ddlPageNumberList.SelectedIndex = 0 Then Exit Sub

        ddlPageNumberList.SelectedIndex = ddlPageNumberList.SelectedValue - 2

        Call PopulateSalesList(ddlPageNumberList.SelectedValue)
    End Sub

    Protected Sub ancNextPage_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ancNextPage.ServerClick
        pnlTabControl.Visible = False

        If ddlPageNumberList.SelectedValue = ddlPageNumberList.Items.Count Then Exit Sub

        ddlPageNumberList.SelectedIndex = ddlPageNumberList.SelectedValue

        Call PopulateSalesList(ddlPageNumberList.SelectedValue)
    End Sub

    Protected Sub ancTabDoc_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Call FocusTab(TabPages.Documents)
    End Sub

    Protected Sub ancTabBuyer_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Call FocusTab(TabPages.Buyer)
    End Sub

    Protected Sub ancTabComm_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Call FocusTab(TabPages.Commission)
    End Sub

    Protected Sub ancTabVouchers_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Call FocusTab(TabPages.Voucher)
    End Sub

    Protected Sub btnVoucherPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoucherPrint.Click
        Dim _dimstrVoucherNumber As String
        Dim _dimstrAgentCode As String

        _dimstrVoucherNumber = spnVouchVoucherNumber.InnerHtml
        _dimstrAgentCode = spnVouchListAgentCode.InnerHtml

        Dim _dimstrErrorMessage As String = ""
        Dim _dimrptVoucherDetails As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        Try
            _pridttTable = New DataTable
            If fnblnQueryData(TransMode.Mod04SelectVoucherDetailsRpt, _
                              _dimstrErrorMessage, _
                              _pridttTable, _
                              , _
                              , _
                              , _
                              _dimstrAgentCode, _
                              _dimstrVoucherNumber) = False Then
                ShowMsgBox(_dimstrErrorMessage, MsgCss.Errors)

                Exit Sub
            End If

            _dimrptVoucherDetails.Load(Server.MapPath("Reports\selrptVoucherDetails.rpt"), CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy)
            _dimrptVoucherDetails.Subreports("CommissionDetails").SetDataSource(_pridttTable.Copy)
            _dimrptVoucherDetails.SetDataSource(_pridttTable.Copy)

            Session("rptVoucherDetails") = _dimrptVoucherDetails.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            ifrmPrintPreview.Attributes("src") = "selPDFLoader.aspx?SessionID=rptVoucherDetails"
            pnlVoucherPrintPreview.Visible = True
        Catch ex As Exception
            ShowMsgBox(MyExceptionNotice, MsgCss.Errors)

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        Finally
            If _dimrptVoucherDetails IsNot Nothing Then _dimrptVoucherDetails.Dispose()
            If _pridttTable IsNot Nothing Then _pridttTable.Dispose()
        End Try
    End Sub
#End Region 'Button

#Region "Grid"
    ' Events in all Grid
    Protected Sub grdSalesList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSalesList.RowCommand
        If e.CommandName = "Select" Then
            Dim _dimRowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' >>---- Populate Data of selected contract and agent

            Dim _dimstrContractNumber As String
            Dim _dimstrCompanyCode As String
            Dim _dimstrAgentCode As String

            _dimstrCompanyCode = grdSalesList.Rows(_dimRowIndex).Cells(2).Text ' 2 : CoCode
            _dimstrContractNumber = grdSalesList.Rows(_dimRowIndex).Cells(3).Text ' 3 : Acct. Code
            _dimstrAgentCode = grdSalesList.Rows(_dimRowIndex).Cells(4).Text ' 4 : AgentCode

            _pridtsTable = New DataSet

            Dim _dimstrErrorMessage As String = ""

            Try
                If fnblnQueryData(TransMode.Mod02SelectDataOfSelectedContract, _
                                  _dimstrErrorMessage, _
                                  , _
                                  _pridtsTable, _
                                  _dimstrCompanyCode, _
                                  _dimstrContractNumber, _
                                  _dimstrAgentCode) = False Then
                    ShowMsgBox(_dimstrErrorMessage, MsgCss.Errors)

                    Exit Sub
                End If

                ' Hide tab pages
                pnlTabControl.Visible = Not _pridtsTable.Tables.Count = 0

                ' >>---- TAB Buyer ---- 
                '   Display buyer info
                If _pridtsTable.Tables(0).Rows.Count > 0 Then
                    With _pridtsTable.Tables(0).Rows(0)
                        spnBuyProductType.InnerHtml = .Item("ProductType")
                        spnBuyPaymentMode.InnerHtml = .Item("PaymentMode")
                        txbBuyTCP.Text = .Item("TCP")
                        txbBuyBasisofCommission.Text = .Item("CommAmount")
                        spnBuyBuyerName.InnerHtml = .Item("BuyersName")
                    End With
                End If

                'Populate Buyers Payment
                ViewState("vwsBuyersPayment") = _pridtsTable.Tables(1)
                grdBuyersPayment.DataSource = _pridtsTable.Tables(1)
                grdBuyersPayment.DataBind()
                ' <<---- TAB Buyer ----  

                ' >>---- TAB Documents ---- 
                ' Display Documents Info 
                If _pridtsTable.Tables(2).Rows.Count > 0 Then
                    With _pridtsTable.Tables(2).Rows(0)
                        spnDocBuyerName.InnerHtml = .Item("BuyerName")
                        spnDocDeadline.InnerHtml = .Item("DeadlineDate")
                        divDocDeadline.Visible = Not .Item("DeadlineDate").ToString.Trim = ""
                    End With
                End If

                'Populate Documents Details
                ViewState("vwsDocuments") = _pridtsTable.Tables(3)
                grdDocumentLists.DataSource = _pridtsTable.Tables(3)
                grdDocumentLists.DataBind()
                ' <<---- TAB Documents ----  

                ' >>---- TAB Commission ---- 
                'Display Commission Info 
                If _pridtsTable.Tables(4).Rows.Count > 0 Then
                    With _pridtsTable.Tables(4).Rows(0)
                        spnCommCommissionableAmount.InnerHtml = .Item("CommissionAmount")
                        txbCommMileTotalPaid.Text = .Item("TotalPaid")
                        txbCommProcessed.Text = .Item("TotalUnRelease")
                        spnCommSellerName.InnerHtml = .Item("SellerName")
                        spnCommShareRate.InnerHtml = .Item("Share")
                        spnCommTotalComm.Text = .Item("TotalCommission")
                        txbCommTotalPaid.Text = .Item("TotalPaid")
                        txbCommTotalUnpaid.Text = .Item("TotalUnPaid")
                    End With
                End If

                'Populate Commission Releases
                ViewState("vwsCommissionReleases") = _pridtsTable.Tables(5)
                grdCommissionReleases.DataSource = _pridtsTable.Tables(5)
                grdCommissionReleases.DataBind()

                'Populate Commission Milestone
                ViewState("vwsCommissionMilestone") = _pridtsTable.Tables(6)
                grdCommissionMilestone.DataSource = _pridtsTable.Tables(6)
                grdCommissionMilestone.DataBind()

                'Display Commission Total Milestone 
                If _pridtsTable.Tables(7).Rows.Count > 0 Then
                    With _pridtsTable.Tables(7).Rows(0)
                        txbCommMileTotalPaid.Text = .Item(0)
                    End With
                End If
                ' <<---- TAB Commission ----  

                ' >>---- TAB VOUCHER ----  
                'Populate Commission Milestone
                ViewState("vwsVoucherList") = _pridtsTable.Tables(8)
                grdVoucherList.DataSource = _pridtsTable.Tables(8)
                grdVoucherList.DataBind()

                spnVouchListSellerName.InnerHtml = grdSalesList.Rows(_dimRowIndex).Cells(7).Text ' 7 :Seller Name
                spnVouchListAgentCode.InnerHtml = grdSalesList.Rows(_dimRowIndex).Cells(4).Text ' 4 : AgentCode
                spnVouchSellername.InnerHtml = grdSalesList.Rows(_dimRowIndex).Cells(7).Text ' 7 :Seller Name
                ' <<---- TAB VOUCHER ----   

                Call FocusTab(TabPages.Buyer)

                pnlVoucherBreakDown.Visible = False
                pnlVoucherPrintPreview.Visible = False

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "tag", "var aTag = $(""a[name='aTagTabControl']""); $('html,body,window').animate({scrollTop: aTag.offset().top}, 1000);", True)
            Catch ex As Exception
                ShowMsgBox(MyExceptionNotice, MsgCss.Errors)

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Finally
                If _pridtsTable IsNot Nothing Then _pridtsTable.Dispose()
            End Try
        End If
    End Sub

    Protected Sub grdSalesList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSalesList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = String.Format("javascript:__doPostBack('{0}','Select${1}')", grdSalesList.UniqueID, e.Row.RowIndex) '& ";ScrollDiv('aTagTabControl');"
            e.Row.Attributes("style") = "cursor:pointer;" & e.Row.Cells(5).Text ' 5 : style
        End If
    End Sub

    Protected Sub grdBuyersPayment_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdBuyersPayment.PageIndexChanging
        grdBuyersPayment.PageIndex = e.NewPageIndex

        If ViewState("vwsBuyersPayment") IsNot Nothing Then
            grdBuyersPayment.DataSource = ViewState("vwsBuyersPayment")
            grdBuyersPayment.DataBind()
        End If
    End Sub

    Protected Sub grdDocumentLists_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDocumentLists.PageIndexChanging
        grdDocumentLists.PageIndex = e.NewPageIndex

        If ViewState("vwsDocuments") IsNot Nothing Then
            grdDocumentLists.DataSource = ViewState("vwsDocuments")
            grdDocumentLists.DataBind()
        End If
    End Sub

    Protected Sub grdCommissionReleases_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCommissionReleases.PageIndexChanging
        grdCommissionReleases.PageIndex = e.NewPageIndex

        If ViewState("vwsCommissionReleases") IsNot Nothing Then
            grdCommissionReleases.DataSource = ViewState("vwsCommissionReleases")
            grdCommissionReleases.DataBind()
        End If
    End Sub

    Protected Sub grdCommissionMilestone_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCommissionMilestone.PageIndexChanging
        grdCommissionMilestone.PageIndex = e.NewPageIndex

        If ViewState("vwsCommissionMilestone") IsNot Nothing Then
            grdCommissionMilestone.DataSource = ViewState("vwsCommissionMilestone")
            grdCommissionMilestone.DataBind()
        End If
    End Sub

    Protected Sub grdVoucherList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdVoucherList.PageIndexChanging
        grdVoucherList.PageIndex = e.NewPageIndex

        If ViewState("vwsVoucherList") IsNot Nothing Then
            grdVoucherList.DataSource = ViewState("vwsVoucherList")
            grdVoucherList.DataBind()
            pnlVoucherBreakDown.Visible = False
            pnlVoucherPrintPreview.Visible = False
        End If
    End Sub

    Protected Sub grdVoucherList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdVoucherList.RowCommand
        If e.CommandName = "Select" Then
            Dim _dimRowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim _dimstrVoucherNumber As String
            Dim _dimstrAgentCode As String

            _dimstrVoucherNumber = grdVoucherList.Rows(_dimRowIndex).Cells(1).Text
            _dimstrAgentCode = spnVouchListAgentCode.InnerHtml

            _pridtsTable = New DataSet
            Dim _dimstrErrorMessage As String = ""

            Try
                If fnblnQueryData(TransMode.Mod03SelectVoucherDetails, _
                                  _dimstrErrorMessage, _
                                  , _
                                  _pridtsTable, _
                                  , _
                                  , _
                                  _dimstrAgentCode, _
                                  _dimstrVoucherNumber) = False Then
                    ShowMsgBox(_dimstrErrorMessage, MsgCss.Errors)

                    Exit Sub
                End If

                pnlVoucherPrintPreview.Visible = False

                spnVouchVoucherDate.InnerHtml = grdVoucherList.Rows(_dimRowIndex).Cells(2).Text
                spnVouchVoucherNumber.InnerHtml = _dimstrVoucherNumber
                spnVouchDeductionNumber.InnerHtml = _dimstrVoucherNumber

                pnlVoucherBreakDown.Visible = True
                '--(0) 
                '-- Voucher Deduction
                '--(1)
                '--Commission details	
                '--(2)
                '--Voucher Totals Amounts 		    

                'Populate Deduction list
                ViewState("vwsVoucherDeduc") = _pridtsTable.Tables(0)
                grdVoucherDeduction.DataSource = _pridtsTable.Tables(0)
                grdVoucherDeduction.DataBind()

                'Populate Commission details	
                ViewState("vwsVoucherCommDetails") = _pridtsTable.Tables(1)
                grdVoucherCommissionDetails.DataSource = _pridtsTable.Tables(1)
                grdVoucherCommissionDetails.DataBind()

                'Display Commission Info 
                If _pridtsTable.Tables(2).Rows.Count > 0 Then
                    With _pridtsTable.Tables(2).Rows(0)
                        txbVouchCommDetailsTotal.Text = .Item("TotalGross")
                        txbVouchNetCashPayable.Text = .Item("TotalAmount")
                        spnVouchTotalAmountInWords.InnerHtml = .Item("TotalAmountInWords")
                        txbVouchTotalGross.Text = .Item("TotalGross")
                        txbVouchTotalTax.Text = .Item("TotalTax")
                        txbVouchOtherDeductions.Text = .Item("TotalOtherDeduction")
                    End With
                End If
            Catch ex As Exception
                ShowMsgBox(MyExceptionNotice, MsgCss.Errors)

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Finally
                If _pridtsTable IsNot Nothing Then _pridtsTable.Dispose()
            End Try
        End If
    End Sub

    Protected Sub grdVoucherList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdVoucherList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = String.Format("javascript:__doPostBack('{0}','Select${1}')", grdVoucherList.UniqueID, e.Row.RowIndex)
            e.Row.Attributes("style") = "cursor:pointer"
        End If
    End Sub

    Protected Sub grdVoucherCommissionDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdVoucherCommissionDetails.PageIndexChanging
        grdVoucherCommissionDetails.PageIndex = e.NewPageIndex

        If ViewState("vwsVoucherCommDetails") IsNot Nothing Then
            grdVoucherCommissionDetails.DataSource = ViewState("vwsVoucherCommDetails")
            grdVoucherCommissionDetails.DataBind()
            pnlVoucherPrintPreview.Visible = False
        End If
    End Sub

    Protected Sub grdVoucherDeduction_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdVoucherDeduction.PageIndexChanging
        grdVoucherDeduction.PageIndex = e.NewPageIndex

        If ViewState("vwsVoucherDeduc") IsNot Nothing Then
            grdVoucherDeduction.DataSource = ViewState("vwsVoucherDeduc")
            grdVoucherDeduction.DataBind()
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
