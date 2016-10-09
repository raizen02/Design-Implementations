<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="promoCalc.aspx.vb"
    Inherits="promoCalc"
    Title="PROMO Calculator | Sellers' HUB"%>

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
                        <li><a href="#">&nbsp;Promo Calculator</a> </li>
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
                                <i class="icon-print"></i>&nbsp;Spot DP Promo Calculator <%=ViewState("vwsstrOldTCP") %></h2>
                        </div>
                        <div style="text-align: justify" class="row-fluid">
                            <div class="span6">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="box-content form-horizontal">
                                            <div class="alert alert-info">
                                                This facility allows the user to compute the Spot DP Promo based on TCP.
                                            </div>
                                            <div class="control-group">
                                                <label class="controlcustom-label">
                                                    Total Contract Price (TCP)</label>
                                                <div class="controlscustom">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox
                                                            ID="txbTCP"
                                                            runat="server"
                                                            ToolTip="Total Contract Price (TCP)"
                                                            CssClass="input-large"
                                                            AutoPostBack="true" CausesValidation="True"
                                                            Style="text-align: right;" EnableTheming="True"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="rfvTCP"
                                                            runat="server"
                                                            ControlToValidate="txbTCP"
                                                            CssClass="help-inline"
                                                            ErrorMessage="<br />* TCP is a required field"
                                                            Display="Dynamic"
                                                            SetFocusOnError="False"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="controlcustom-label">
                                                    Reservation Fee</label>
                                                <div class="controlscustom">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
                                                        <asp:TextBox
                                                            ID="txbReservationFee"
                                                            runat="server"
                                                            ToolTip="Reservation Fee"
                                                            CssClass="input-large"
                                                            AutoPostBack="true" CausesValidation="true"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="controlcustom-label">
                                                    Discounted 5% spot DP</label>
                                                <div class="controlscustom">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
<%--                                                        <span class="input-large uneditable-input" style="text-align: right;" id="spnDiscount" runat="server"></span>--%>
                                                        <asp:TextBox
                                                            ID="txbDiscount"
                                                            runat="server"
                                                            ToolTip="Discounted 5% spot DP"
                                                            CssClass="input-large uneditable-input"
                                                            ReadOnly="true" 
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="controlcustom-label">
                                                    YOU SAVED!</label>
                                                <div class="controlscustom">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
<%--                                                        <span class="input-large uneditable-input" style="text-align: right;" id="spnYouSaved" runat="server"></span>--%>
                                                        <asp:TextBox
                                                            ID="txbYouSaved"
                                                            runat="server"
                                                            ToolTip="YOU SAVED!"
                                                            CssClass="input-large uneditable-input"
                                                            ReadOnly="true" 
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="controlcustom-label">
                                                    Monthly installment of 5% DP balance payable over 6 months</label>
                                                <div class="controlscustom">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
<%--                                                        <span class="input-large uneditable-input" style="text-align: right;" id="spnDPBalance" runat="server"></span>--%>
                                                        <asp:TextBox
                                                            ID="txbDPBalance"
                                                            runat="server"
                                                            ToolTip="DP balance payable over 6 months"
                                                            CssClass="input-large uneditable-input"
                                                            ReadOnly="true"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="controlcustom-label">
                                                    TCP balance for bank financing application</label>
                                                <div class="controlscustom">
                                                    <div class="input-prepend">
                                                        <span class="add-on">₱</span>
<%--                                                        <span class="input-large uneditable-input" style="text-align: right;" id="spnTCPBalance" runat="server"></span>--%>
                                                        <asp:TextBox
                                                            ID="txbTCPBalance"
                                                            runat="server"
                                                            ToolTip="Balance for Bank Financing"
                                                            CssClass="input-large uneditable-input"
                                                            ReadOnly="true" 
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                            <div class="form-actions">
                                                <asp:Button
                                                    ID="btnReset"
                                                    runat="server"
                                                    Text="Reset"
                                                    CausesValidation="False"
                                                    EnableTheming="False"
                                                    EnableViewState="False"
                                                    UseSubmitBehavior="False"
                                                    CssClass="btn"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span6">
                                <div class="row-fluid">
                                    <div class="span11" style="margin-top: 15px">
                                        <img class="center" src="https://kiosk.filinvest.com.ph/img/slider/slide-94b0d8.jpg" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
