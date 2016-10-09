<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="reqindicator.aspx.vb"
    Inherits="reqindicator"
    Title="Buyer's Requirements | Sellers' HUB"%>

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
    <asp:UpdateProgress ID="uprLoading" AssociatedUpdatePanelID="updUpdatePanel" DynamicLayout="true"
        DisplayAfter="100" runat="server">
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
                        <li><a href="#">&nbsp;Buyer's Requirements</a> </li>
                    </ul>
                </div>
                <div class="alert alert-error" id="divErrorMsgBox" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">
                        ×</button>
                    <div id="divErrorMsg" runat="server">
                    </div>
                </div>
                <div class="row-fluid" style="display: none;">
                    <div class="box span12">
                        <div class="box-header well">
                            <h2>
                                <i class="icon-calendar"></i><a href="#" class="btn-mintoggle">&nbsp;Complete Checklist</a></h2>
                        </div>
                        <div class="box-togglecontent form-horizontal box-content" style="display: none">
                            <div class="form-actions">
                                <asp:HyperLink ID="btnChecklist" NavigateUrl="BuyersRequirements/CheckList/" runat="server" Target="_blank" CssClass="btn btn-primary">Show Checklist</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="box span12">
                        <div class="box-header well">
                            <h2>
                                <i class="icon-inbox"></i><a href="#" class="btn-mintoggle">&nbsp;Search per Contract Type</a></h2>
                        </div>
                        <div class="box-togglecontent form-horizontal box-content">
                            <br />
                            <div class="control-group">
                                <label class="control-label">
                                    Application Type</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="input-xxlarge" AutoPostBack="True" data-rel="chosen">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="Individual">Individual</asp:ListItem>
                                        <asp:ListItem Value="Corporate">Corporate</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    Contract Type</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlContractType" runat="server" CssClass="input-xxlarge" AutoPostBack="True" data-rel="chosen">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="BankFin">Bank Financing</asp:ListItem>
                                        <asp:ListItem Value="InHouseFin">In-House Financing</asp:ListItem>
                                        <asp:ListItem Value="CashSale">Cash Sale</asp:ListItem>
                                        <asp:ListItem Value="DeferredCash">Deferred Cash</asp:ListItem>
                                        <asp:ListItem Value="HDMF">HDMF</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-actions">
                                <asp:HyperLink ID="btnGenerate" runat="server" Target="_blank" CssClass="btn btn-primary">Generate</asp:HyperLink>
                                <%--<asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
