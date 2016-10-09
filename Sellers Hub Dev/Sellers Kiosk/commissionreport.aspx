<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="commissionreport.aspx.vb"
    Inherits="commissionreport"
    MaintainScrollPositionOnPostback="true"
    Title="Commission Kiosk Report | Sellers' HUB"%>

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

    <asp:UpdateProgress ID="uprLoading" AssociatedUpdatePanelID="" DynamicLayout="true"
        DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <img alt="Loading..." src="images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
        <ContentTemplate>
            <div style="text-align: justify">
                <div>
                    <a name="aTagAlert"></a>
                    <ul class="breadcrumb">
                        <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                        <li><a href="#">Kiosk Report</a> </li>
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
                                <i class="icon-list-alt"></i>&nbsp;Commission Report</h2>
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
                                        <%--<asp:CommandField ShowSelectButton="true" HeaderStyle-CssClass="visibility" ItemStyle-CssClass="visibility" />--%>
                                        <asp:TemplateField HeaderText="Print" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxPrint" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ImageLevel" HeaderText=" " HtmlEncode="false"></asp:BoundField>
                                        <asp:BoundField DataField="CoCode" HeaderText="CoCode" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="Acct. Code" HeaderText="Contract Number"></asp:BoundField>
                                        <asp:BoundField DataField="AgentCode" HeaderText="AgentCode" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="Style" HeaderText="Style" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="IsMainUnit" HeaderText="IsMainUnit" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
                                        <asp:BoundField DataField="ProjGroup" HeaderText="ProjGroup" HeaderStyle-CssClass="visibility"
                                            ItemStyle-CssClass="visibility" />
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
                            <div id="divButtons" runat="server" class="form-actions">
                                <asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print Preview" />
                                <asp:Button ID="btnCheckAll" CssClass="btn" runat="server" Text="Check All" />
                                <asp:Button ID="btnClearCheck" CssClass="btn" runat="server" Text="Clear Checked" />
                            </div>
                            <a name="aTagPrintPanel"></a>
                            <asp:Panel ID="pnlPrintPreview" runat="server" Visible="False" ScrollBars="Auto">
                                <iframe id="ifrmPrintPreview" style="width: 100%; height: 500px;" src="" runat="server"
                                    scrolling="no" frameborder="0">IFrame </iframe>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
