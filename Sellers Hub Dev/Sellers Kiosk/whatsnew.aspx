<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="whatsnew.aspx.vb"
    Inherits="whatsnew"
    title="What's New | Sellers' Hub" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="conContent" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:ScriptManager ID="scmCallback" runat="server" />

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(
            function () {
                docReady();
            });
             
    </script>

    <asp:UpdateProgress ID="uprLoading"
        AssociatedUpdatePanelID="updUpdatePanel"
        DynamicLayout="true"
        DisplayAfter="0"
        runat="server">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <img alt="Loading..." src="images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<<-----Loading Panel--%>
    <asp:UpdatePanel ID="updUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
        <ContentTemplate>
        <center>
            <div style="text-align: justify; max-width: 1005px">
                <div>
                    <ul class="breadcrumb">
                        <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                        <li><a href="#"><%=ViewState("vwsstrTitle")%></a> </li>
                    </ul>
                </div>
                <div class="row-fluid">
                    <div class="box span9">
                        <div class="box-header well" data-original-title>
                            <h2>
                                <asp:Label ID="lblTitleAnouncement" runat="server" Text="No article available."></asp:Label></h2>
                        </div>
                        <div class="box-content">
                            <div class="row-fluid ">
                                <div class="span12" id="divArticleContent" runat="server">
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <!--/span-->
                    <div class="span3">
                        <div class="box-content" style="text-align: left;">
                            <strong><span style="font-size: 10pt; color: red">
                            <%=ViewState("vwsstrTitle").ToUpper%></span></strong>
                            <br />&nbsp;
                            <ul class="dashboard-list">
                            <asp:Repeater ID="rprWhatsNew" runat="server">
                                <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="btnLinkWhatsNew" runat="server" OnClick="btnLinkWhatsNew_Click" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ArtId")%>'>
                                        <div style="min-height: 73px">
                                            <img class="dashboard-avatar" alt="" src='<%#DataBinder.Eval(Container.DataItem, "Thumbnail")%>'>
                                            <div style="margin-bottom: 7px;text-align: left;"><%#DataBinder.Eval(Container.DataItem, "Title")%></div>
								            <p style="font-size: 8pt;"><%#CDate(DataBinder.Eval(Container.DataItem, "DateFrom")).ToString("MMMM dd, yyyy")%></p>
									    </div>
									</asp:LinkButton>
								</li>
                                </ItemTemplate>
                            </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <!--/span-->
                </div>
                <!--/row-->
                <!-- content ends -->
            </div>
        </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
