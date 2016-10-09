<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="sdp.aspx.vb"
    Inherits="sdp"
    Title="SDP | Sellers' HUB"%>

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
                        <li><a href="#">Site Development Plan (SDP)</a> </li>
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
                                <i class="icon-print"></i>&nbsp;Site Development Plan (SDP)</h2>
                        </div>
                        <div class="box-content form-horizontal">
                            <div class="alert alert-info">
                                This facility allows the user to either Print and/or Export the Site Development Plan (SDP) to a
                                PDF file.
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    Project</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlProjects" runat="server" CssClass="input-xxlarge" AutoPostBack="True" data-rel="chosen">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">
                                    Phase/Building</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlPhaseBuilding" runat="server" CssClass="input-xxlarge" AutoPostBack="True" data-rel="chosen">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnProceed" runat="server" Text="Generate SDP" CssClass="btn btn-primary" />
                            </div>
                            <div class="box-content form-horizontal">
                                <a name="aTagResult"></a>
                                <div id="divSDPReport" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
