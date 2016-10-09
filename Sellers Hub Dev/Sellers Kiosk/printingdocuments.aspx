<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    MaintainScrollPositionOnPostback="true"
    CodeFile="printingdocuments.aspx.vb"
    Inherits="printingdocuments"
    EnableEventValidation="false"
    Title="Printing of Documents | Sellers' HUB"%>

<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:ScriptManager ID="scmCallback" runat="server" />
    <asp:UpdateProgress ID="uprLoading" AssociatedUpdatePanelID="updUpdatePanel" DynamicLayout="true"
        runat="server" DisplayAfter="100">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <img alt="Loading..." src="images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="text-align:justify">
                <div>
                    <ul class="breadcrumb">
                        <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                        <li><a href="#">Documents Printing</a> </li>
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
                                <i class="icon-print"></i>&nbsp;Printing of Documents</h2>
                        </div>
                        <div class="box-content form-horizontal">
                            <div class="control-group">
                                <div class="controls">
                                    <div class="input-append">
                                        <asp:TextBox ID="txbSearch" runat="server" /><asp:Button ID="btnSearch" runat="server"
                                            Text="Find" CssClass="btn" />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlContracts" runat="server">
                                <div class="alert alert-info">
                                    Please click CONTRACT # to view list of documents
                                </div>
                                <div class="page-header">
                                    <h3>
                                        Accounts for Printing</h3>
                                </div>
                                <asp:Panel ID="pnlContractList" runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="grdContractList" runat="server" AllowPaging="True" CssClass="table table-bordered table-striped table-condensed"
                                        EmptyDataText="No Contract found." AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="locCompanyCode" HeaderText="Company Code" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="locContractNumber" HeaderText="Contract #" />
                                            <asp:BoundField DataField="locCustomerName" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="locProjectName" HeaderText="Project" />
                                            <asp:BoundField DataField="locPhaseName" HeaderText="Phase" />
                                            <asp:BoundField DataField="locBlockName" HeaderText="Block #" />
                                            <asp:BoundField DataField="locLotName" HeaderText="Lot #" />
                                            <asp:BoundField DataField="locPrintedStatus" HeaderText="Printed" />
                                            <asp:BoundField DataField="locPrintedDate" HeaderText="LPD" DataFormatString="{0: MM/dd/yyyy hh:mm:ss tt}" />
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </asp:GridView>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel ID="pnlDocumentLoader" runat="server">
                                <div class="alert alert-info">
                                    Please click Report Document to view the report.</div>
                                <div class="page-header">
                                    <h3>
                                        Documents for Printing</h3>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Contract Number</label>
                                    <div class="controls">
                                        <span class="input-xxlarge uneditable-input" id="spnContractNumber" runat="server"></span>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Customer Name</label>
                                    <div class="controls">
                                        <span class="input-xxlarge uneditable-input" id="spnCustomerName" runat="server"></span>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Project</label>
                                    <div class="controls">
                                        <span class="input-xxlarge uneditable-input" id="spnProject" runat="server"></span>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Phase/Building</label>
                                    <div class="controls">
                                        <span class="input-xxlarge uneditable-input" id="spnPhase" runat="server"></span>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Block/Floor/Cluster</label>
                                    <div class="controls">
                                        <span class="input-xxlarge uneditable-input" id="spnBlock" runat="server"></span>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Lot/Unit/Share</label>
                                    <div class="controls">
                                        <span class="input-xxlarge uneditable-input" id="spnLot" runat="server"></span>
                                    </div>
                                </div>
                                <hr width="100%" />
                                <asp:Panel ID="pnlOtherDocuments" runat="server" ScrollBars="Auto">
                                    <h4>
                                        Other Documents</h4>
                                    <p>
                                    </p>
                                    <asp:GridView ID="grdOtherDocuments" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="rptFileName" HeaderText="Report Code" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rptPrintStatus" HeaderText="Print Status" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rptReportDesc" HeaderText="Document Desc" />
                                            <asp:BoundField DataField="rptDatePrinted" HeaderText="Date Printed" DataFormatString="{0: MM/dd/yyyy hh:mm:ss tt}" />
                                            <asp:CommandField ShowSelectButton="True" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <br />
                                <asp:Panel ID="pnlDocuments" runat="server" ScrollBars="Auto">
                                    <h4>
                                        Documents</h4>
                                    <p>
                                    </p>
                                    <asp:GridView ID="grdDocuments" runat="server" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="rptFileName" HeaderText="Report Code" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rptPrintStatus" HeaderText="Print Status" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rptReportDesc" HeaderText="Document Desc" />
                                            <asp:BoundField DataField="rptDatePrinted" HeaderText="Date Printed" DataFormatString="{0: MM/dd/yyyy hh:mm:ss tt}" />
                                            <asp:CommandField ShowSelectButton="True" >
                                                <HeaderStyle CssClass="visibility" />
                                                <ItemStyle CssClass="visibility" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <div class="alert alert-info">
                                    <strong>Note:</strong> Documents that have already been PRINTED will be removed
                                    from the list within 2 week from the date of printing.
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="row-fluid" id="divPrintResult" runat="server">
                    <div class="box span12">
                        <div class="box-header well">
                            <asp:Button ID="btnPrint" runat="server" Text="Click to Print" CssClass="btn btn-primary" />
                        </div>
                        <div class="box-content form-horizontal">
                            <asp:Panel ID="pnlPDFLoader" runat="server">
                                <div id="divPDFLoader" runat="server">
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlCrystalReport" runat="server" ScrollBars="Auto">
                               <%-- <CR:CrystalReportViewer ID="rptLoader" runat="server" AutoDataBind="True" Height="470px"
                                    Width="505px" BestFitPage="False" BorderStyle="Inset" DisplayGroupTree="False"
                                    EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasSearchButton="False"
                                    HasToggleGroupTreeButton="False" HasViewList="False" HasPrintButton="False" HasExportButton="False" />--%>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
