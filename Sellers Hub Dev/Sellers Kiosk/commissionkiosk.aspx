<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="commissionkiosk.aspx.vb"
    Inherits="commissionkiosk"
    MaintainScrollPositionOnPostback="true"
    Title="Commission Kiosk | Sellers' HUB"%>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:ScriptManager ID="scmCallback" runat="server" />

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(
            function () {
                docReady();
            }); 
             
    </script>
 


    <script type="text/javascript">
        function ScrollDiv(aid)
            {
               var aTag = $("a[name='"+ aid +"']");
               $('html,body,window').animate({scrollTop: aTag.offset().top}, 1000);
            }
    </script>

    <asp:UpdateProgress ID="uprLoading" AssociatedUpdatePanelID=""  DynamicLayout="true"
        DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <img alt="Loading..." src="images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updUpdatePanel" runat="server"  UpdateMode="Always" ChildrenAsTriggers="True">
        <ContentTemplate>
            <div style="text-align: justify">
                <div>
                    <ul class="breadcrumb">
                        <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                        <li><a href="#">Kiosk</a> </li>
                    </ul>
                </div>
                <div class="alert alert-error" id="divErrorMsgBox" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">
                        ×</button>
                    <div id="divErrorMsg" runat="server">
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="box span12">
                        <div class="box-header well">
                            <h2>
                                <i class="icon-list-alt"></i>&nbsp;Commission Kiosk</h2>
                        </div>
                        <div class="box-content form-horizontal">
                            <div class="control-group">
                                <div class="span6">
                                    <asp:DropDownList Width="60" ID="ddlPageMaxSize" runat="server" AutoPostBack="true">
                                        <asp:ListItem Selected="True">10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                    records per page
                                </div>
                                <div class="span6">
                                    Buyer's Name
                                    <div class="input-append">
                                        <asp:TextBox ID="txbSearch" runat="server" /><asp:Button ID="btnSearch" runat="server"
                                            Text="Find" CssClass="btn" />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlSalesList" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="grdSalesList" runat="server" AllowPaging="false" CssClass="table table-bordered table-condensed"
                                    EmptyDataText="No Contract found." AutoGenerateColumns="False">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="true" HeaderStyle-CssClass="visibility" ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="ImageLevel" HeaderText=" " HtmlEncode="false"></asp:BoundField>
                                        <asp:BoundField DataField="CoCode" HeaderText="CoCode" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility"></asp:BoundField>
                                        <asp:BoundField DataField="Acct. Code" HeaderText="Contract Number"></asp:BoundField>
                                        <asp:BoundField DataField="AgentCode" HeaderText="AgentCode" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="Style" HeaderText="Style" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="ProjGroup" HeaderText="ProjGroup" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility"></asp:BoundField>
                                        <asp:BoundField DataField="Seller Name" HeaderText="Seller Name"></asp:BoundField>
                                        <asp:BoundField DataField="Position" HeaderText="Position"></asp:BoundField>
                                        <asp:BoundField DataField="Seller Desc." HeaderText="Seller Desc."></asp:BoundField>
                                        <asp:BoundField DataField="Buyers Name" HeaderText="Buyers Name"></asp:BoundField>
                                        <asp:BoundField DataField="Project Name" HeaderText="Project Name"></asp:BoundField>
                                        <asp:BoundField DataField="Phase" HeaderText="Phase"></asp:BoundField>
                                        <asp:BoundField DataField="Block" HeaderText="Block"></asp:BoundField>
                                        <asp:BoundField DataField="Lot" HeaderText="Lot"></asp:BoundField>
                                        <asp:BoundField DataField="Date Reserved" HeaderText="Date Reserved"></asp:BoundField>
                                        <asp:BoundField DataField="ContractEnd" HeaderText="ContractEnd" HtmlEncode="false">
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <div id="divPagination" runat="server" class="control-group">
                                <div class="span12 center">
                                    <div class="pagination">
                                        <ul>
                                            <li><a><span id="spanTableInfo" runat="server"></span></a></li>
                                            <li class="prev"><a href="#" id="ancPrevPage" runat="server">← Previous</a> </li>
                                            <a>
                                                <asp:DropDownList Width="60" ID="ddlPageNumberList" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Selected="True">1</asp:ListItem>
                                                </asp:DropDownList>
                                            </a>
                                            <li class="next"><a href="#" id="ancNextPage" runat="server">Next →</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <a name="aTagTabControl"></a>
                            <asp:Panel ID="pnlTabControl" runat="server" Visible="false">
                                <ul class="nav nav-tabs" id="myTab" runat="server">
                                    <li id="liTabBuyer" runat="server"><a href="#tabBuyer" id="ancTabBuyers" runat="server"
                                        onserverclick="ancTabBuyer_ServerClick">Buyer's Payment</a></li>
                                    <li id="liTabCommission" runat="server"><a href="#tabCommission" id="ancTabComm"
                                        runat="server" onserverclick="ancTabComm_ServerClick">Commission</a></li>
                                    <li id="liTabVouchers" runat="server"><a href="#tabVouchers" id="ancTabVoucher" runat="server"
                                        onserverclick="ancTabVouchers_ServerClick">Vouchers</a></li>
                                    <li id="liTabDocuments" runat="server"><a href="#tabDocuments" id="ancTabDoc" runat="server"
                                        onserverclick="ancTabDoc_ServerClick">Documents</a></li>
                                </ul>
                                <div id="myTabContent" class="tab-content">
                                    <%--START : BUYER PAYMENT INFO--%>
                                    <div class="tab-pane active" id="tabBuyer" runat="server">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Buyer's name</label>
                                            <div class="controls">
                                                <span class="input-xxlarge  uneditable-input" id="spnBuyBuyerName" runat="server">Test
                                                    Name</span>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Product Type</label>
                                            <div class="controls">
                                                <span class="input-xxlarge  uneditable-input" id="spnBuyProductType" runat="server">
                                                    Test Name</span>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Payment Mode</label>
                                            <div class="controls">
                                                <span class="input-xxlarge  uneditable-input" id="spnBuyPaymentMode" runat="server">
                                                    Test Name</span>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                TCP</label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="txbBuyTCP" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Basis of Commission</label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="txbBuyBasisofCommission" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlBuyersPayment" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="grdBuyersPayment" runat="server" AllowPaging="True" EnableSortingAndPagingCallbacks="true"
                                                CssClass="table table-bordered table-striped table-condensed" EmptyDataText="No records found."
                                                AutoGenerateColumns="True">
                                                <PagerSettings Mode="NumericFirstLast" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <div class="alert alert-info ">
                                            All checks are subject to clearing as follows :
                                            <ul>
                                                <li>Local Checks-7 Calendar days</li>
                                                <li>Regional Checks-15 Calendar days</li>
                                                <li>Out of Town Checks-45 Calendar days</li>
                                            </ul>
                                        </div>
                                    </div>
                                    <%--END : BUYER PAYMENT INFO--%>
                                    <%--START : COMMISSION INFO--%>
                                    <div class="tab-pane" id="tabCommission" runat="server">
                                        <div class="control-group">
                                            <h5>
                                                COMMISSION FROM SALES</h5>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Seller's name</label>
                                            <div class="controls">
                                                <span class="input-xxlarge  uneditable-input" id="spnCommSellerName" runat="server">
                                                    Test Name</span>
                                            </div>
                                        </div>
                                        <div class="alert alert-info ">
                                            Seller's commission is&nbsp;<span id="spnCommShareRate" runat="server">-</span>
                                            of Php&nbsp;<span id="spnCommCommissionableAmount" runat="server">-</span>
                                        </div>
                                        <asp:Panel ID="pnlCommissionReleases" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="grdCommissionReleases" runat="server" AllowPaging="True" CssClass="table table-bordered table-striped table-condensed"
                                                EmptyDataText="No records found." AutoGenerateColumns="True">
                                                <PagerSettings Mode="NumericFirstLast" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Total Paid:</label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="txbCommTotalPaid" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Processed not yet released:</label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="txbCommProcessed" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label">
                                                Total Unpaid:</label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="txbCommTotalUnpaid" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <hr class="controls" style="border: 1px solid #dddddd;">
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label ">
                                                Total :
                                            </label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="spnCommTotalComm" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="control-group">
                                            <h5>
                                                COMMISSION MILESTONE</h5>
                                        </div>
                                        <asp:Panel ID="pnlCommissionMilestone" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="grdCommissionMilestone" runat="server" AllowPaging="True" CssClass="table table-bordered table-striped table-condensed"
                                                EmptyDataText="No records found." AutoGenerateColumns="True">
                                                <PagerSettings Mode="NumericFirstLast" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <div class="control-group ">
                                            <label class="control-label">
                                                Total Paid:
                                            </label>
                                            <div class="controls">
                                                <div class="input-prepend">
                                                    <span class="add-on">₱</span>
                                                    <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                        ID="txbCommMileTotalPaid" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--END : COMMISSION INFO--%>
                                    <%--START : VOUCHERS INFO--%>
                                    <div class="tab-pane" id="tabVouchers" runat="server">
                                        <div class="control-group">
                                            <h5>
                                                LIST OF VOUCHER'S CONTRACT
                                            </h5>
                                        </div>
                                        <div class="control-group">
                                            <label class="control-label ">
                                                Seller Name :
                                            </label>
                                            <div class="controls">
                                                <span class="input-xxlarge  uneditable-input"><span id="spnVouchListSellerName" runat="server">
                                                    -</span>&nbsp;-&nbsp;<span id="spnVouchListAgentCode" runat="server"> -</span> </span>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlVoucherList" runat="server" ScrollBars="Auto">
                                            <label>
                                                Select voucher number to view details
                                            </label>
                                            <asp:GridView ID="grdVoucherList" runat="server" AllowPaging="True" CssClass="table table-bordered table-striped table-condensed"
                                                EmptyDataText="No records found." AutoGenerateColumns="True">
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="true" HeaderStyle-CssClass="visibility" ItemStyle-CssClass="visibility" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlVoucherBreakDown" runat="server" Visible="false">
                                            <div class="control-group">
                                                <h5>
                                                    DETAILED COMMISSION VOUCHER
                                                </h5>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    PAYEE
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large  uneditable-input" id="spnVouchSellername" runat="server">-</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    VOUCHER NO.
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large  uneditable-input" id="spnVouchVoucherNumber" runat="server">
                                                        -</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DATE
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large  uneditable-input" id="spnVouchVoucherDate" runat="server">
                                                        -</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <h6>
                                                    COMMISSION DETAILS
                                                </h6>
                                            </div>
                                            <asp:Panel ID="pnlVoucherCommissionDetails" runat="server" ScrollBars="Auto">
                                                <asp:GridView ID="grdVoucherCommissionDetails" runat="server" AllowPaging="True"
                                                    CssClass="table table-bordered table-striped table-condensed" EmptyDataText="No records found."
                                                    AutoGenerateColumns="True">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Total :
                                                </label>
                                                <div class="controls">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                            ID="txbVouchCommDetailsTotal" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <h6>
                                                    VOUCHER DEDUCTIONS
                                                </h6>
                                            </div>
                                            <asp:Panel ID="pnlVoucherDeduction" runat="server" ScrollBars="Auto">
                                                <asp:GridView ID="grdVoucherDeduction" runat="server" AllowPaging="True" CssClass="table table-bordered table-striped table-condensed"
                                                    EmptyDataText="No voucher deduction found." AutoGenerateColumns="True">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            <div class="alert alert-info ">
                                                Breakdown of Deductions other than withholding tax for Voucher Number # <span id="spnVouchDeductionNumber"
                                                    runat="server">-</span>
                                            </div>
                                            <div class="control-group">
                                                <h6>
                                                    PAYMENT DETAILS
                                                </h6>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Total Gross :
                                                </label>
                                                <div class="controls">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                            ID="txbVouchTotalGross" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Withholding Tax Payable :
                                                </label>
                                                <div class="controls">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                            ID="txbVouchTotalTax" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Other Deductions :
                                                </label>
                                                <div class="controls">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                            ID="txbVouchOtherDeductions" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <hr class="controls" style="border: 1px solid #dddddd;">
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    NET CASH PAYABLE :
                                                </label>
                                                <div class="controls">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox CssClass="input-large uneditable-input" ReadOnly="true" Style="text-align: right;"
                                                            ID="txbVouchNetCashPayable" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="alert alert-info ">
                                                Please be informed that a total amount of <span id="spnVouchTotalAmountInWords" style="font-weight: bold"
                                                    runat="server">-</span>has been credited to your account representing payment
                                                of net commission.
                                            </div>
                                            <div class="form-actions">
                                                <asp:Button ID="btnVoucherPrint" CssClass="btn" runat="server" Text="Print Preview" />
                                            </div>
                                        </asp:Panel>
                                        <%-- REPORT --%>
                                        <asp:Panel ID="pnlVoucherPrintPreview" runat="server" Visible="False" Height="98%"
                                            ScrollBars="Auto">
                                            <iframe id="ifrmPrintPreview" style="width: 98%; height: 98%;" src="" runat="server"
                                                scrolling="no" frameborder="0">IFrame </iframe>
                                        </asp:Panel>
                                    </div>
                                    <%--END : VOUCHERS INFO--%>
                                    <%--START : DOCUMENTS INFO--%>
                                    <div class="tab-pane" id="tabDocuments" runat="server">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Buyer's Name :</label>
                                            <div class="controls">
                                                <span id="spnDocBuyerName" class="input-xxlarge  uneditable-input" runat="server">Test
                                                    Name</span>
                                            </div>
                                        </div>
                                        <div class="alert alert-info " id="divDocDeadline" runat="server">
                                            <span id="spnDocDeadline" runat="server">Deadline for submission of all requirements
                                                is on -</span>
                                        </div>
                                        <asp:Panel ID="pnlDocuments" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="grdDocumentLists" runat="server" AllowPaging="false" CssClass="table table-bordered table-striped table-condensed"
                                                EmptyDataText="No records found." AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="Documents" HeaderText="Documents" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Date Submitted" HeaderText="Date Submitted" HtmlEncode="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <%--END :  DOCUMENTS INFO--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
