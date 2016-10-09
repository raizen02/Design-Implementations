<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="dashboard.aspx.vb"
    Inherits="dashboard"
    Title="Dashboard | Sellers' HUB" %>

<asp:Content ID="cphHead" ContentPlaceHolderID="cphHead" runat="Server">
    <link href='css/dashboard.css' rel='stylesheet' type='text/css' />
    <!-- Respomsive slider -->
    <link href="css/responsive-slider.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
    <div id="content" class="center span11">
            <div style="text-align: justify" class="row-fluid">
                <div class="span7">
                    <div class="row-fluid">
                        <div class="span12">
                            <!-- Responsive slider - START -->
                            <div class="responsive-slider" data-spy="responsive-slider" data-autoplay="true">
                                <div class="slides" data-group="slides">
                                    <ul>
                                        <asp:Repeater ID="rptSlides" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <a href='<%# iif(IsDbNull(DataBinder.Eval(Container.DataItem, "URL")) = FALSE, DataBinder.Eval(Container.DataItem, "URL").ToString(), "#")%>' target="_blank">
                                                        <div class="slide-body" data-group="slide">
                                                            <img src='img/slider/<%#DataBinder.Eval(Container.DataItem, "ImageFileName")%>' alt="">
                                                            <asp:Repeater ID="rptSlideCaption" runat="server">
                                                                <ItemTemplate>
                                                                    <div class="caption" data-animate='<%#DataBinder.Eval(Container.DataItem, "AnimationDataKeyword")%>'
                                                                        data-delay='<%#DataBinder.Eval(Container.DataItem, "ShowDelay")%>' 
                                                                        style='top:<%#DataBinder.Eval(Container.DataItem, "PositionTop")%>%;left:<%#DataBinder.Eval(Container.DataItem, "PositionLeft")%>%;width:<%#DataBinder.Eval(Container.DataItem, "Width")%>%;'>
                                                                        <img src='img/slider/<%#DataBinder.Eval(Container.DataItem, "ImageFileName")%>' alt="">
                                                                    </div>
                                                                </Itemtemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </a>
                                                </li>
                                            </itemtemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                               <asp:Repeater ID="rptSlidePage" runat="server">
                                    <HeaderTemplate>
                                        <div class="pages">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a class="page" href="#" data-jump-to='<%#DataBinder.Eval(Container.DataItem, "Page")%>'></a>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid" id="divIcons" runat="server">
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="ecrmqa.filinvest.com.ph">
                                <img class="center" src="img/eCRM.png" /></a>
                        </div>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="availabilitychart.aspx">
                                <img class="center" src="img/AvailabilityChart.png" /></a>
                        </div>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="#">
                                <img class="center" src="Others/SellerIncentive2016/images/2016SIC_main.jpg" /></a>
                        </div>
                    </div>
                </div>
                <div class="span5">
                    <div class="row-fluid">
                        <div style="margin-top: 0px" class="box span12">
                            <div style="min-height: 144px" class="box-content">
                                <div style="font-size: 10pt; color: mediumvioletred;text-align: left; max-height: 36px">
                                    <strong><a href="#" id="hrfCategoryID1" runat="server"><%=ViewState("vwsstrTitle")%><asp:Label ID="lblCategoryID1" runat="server" Text="What's New"></asp:Label></a></strong>
                                    <br />&nbsp;
                                </div>
                                <asp:Panel id="pnlNoWhatsNew" runat='server' CssClass="alert alert-info" Visible="false">
							        No items available.
						        </asp:Panel>
						        <div class="row-fluid">
					                <div style="margin-top: 0px;min-height: 90px" class="span6" id="divWhatsNew1" runat="server" visible="false">
                                        <asp:HyperLink ID="btnLinkWhatsNew1" runat="server">
                                            <img id="imgWhatsNew1" runat="server" class="dashboard-avatar" alt="" src="img/thumbnail.jpg" />
                                            <div id="divWhatsNewTitle1" runat="server" style="margin-bottom: 7px;text-align: left;"></div>
								            <p id="parWhatsNew1" runat="server" style="font-size: 8pt;">read this post</p>
								        </asp:HyperLink>
					                </div>
					                <div style="margin-top: 0px;min-height: 90px" class="span6" id="divWhatsNew2" runat="server" visible="false">
                                        <asp:HyperLink ID="btnLinkWhatsNew2" runat="server">
                                            <img id="imgWhatsNew2" runat="server" class="dashboard-avatar" alt="" src="img/thumbnail.jpg" />
                                            <div id="divWhatsNewTitle2" runat="server" style="margin-bottom: 7px;text-align: left;"></div>
								            <p id="parWhatsNew2" runat="server" style="font-size: 8pt;">read this post</p>
								        </asp:HyperLink>
					                </div>
                                    <div style="text-align: right;padding-right: 5px;">
                                        <asp:HyperLink id="hplWhatsNewMore" runat="server" NavigateUrl="promosandevents.aspx?newstitle=whatsnew" Text="more..." Visible="false">
                                        </asp:HyperLink>
                                    </div>
						        </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div style="margin-top: 0px;min-height: 150px;" class="box span6">
                            <div style="min-height: 324px" class="box-content">
                                <div style="font-size: 10pt; text-align: left; max-height: 36px">
                                    <strong><a href="#" id="hrfCategoryID3" runat="server"><asp:Label ID="lblCategoryID3" runat="server" Text="Buyer Promos"></asp:Label></a></strong>
                                    <br />&nbsp;
                                </div>
                                <asp:Panel id="pnlNoBuyerPromo" runat='server' CssClass="alert alert-info" Visible="false">
							        No items available.
						        </asp:Panel>
                                <asp:Repeater ID="rprBuyerPromos" runat="server">
                                <ItemTemplate>
    						        <div class="row-fluid">
                                        <div style="margin-top: 0px;min-height: 90px" class="span12">
                                            <asp:HyperLink ID="btnLinkBuyerPromos" runat="server" NavigateUrl='<%#"whatsnew.aspx?newsid=" & Eval("ArtId")%>'>
                                                <img class="dashboard-avatar" alt="" src='<%#Eval("Thumbnail")%>'>
                                                <div style="margin-bottom: 7px;text-align: left;"><%#Eval("Title")%></div>
								                <p style="font-size: 8pt;"><%#CDate(Eval("DateFrom")).ToString("MMMM dd, yyyy") %></p>
								            </asp:HyperLink>
								        </div>
                                    </div>
                                </ItemTemplate>
                                </asp:Repeater>
                                <div style="text-align: right;padding-right: 5px;">
                                    <asp:HyperLink id="hplBuyersPromoMore" runat="server" NavigateUrl="promosandevents.aspx?newstitle=buyerpromos" Text="more..." Visible="false">
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>

                        <div style="margin-top: 0px;min-height: 150px;" class="box span6">
                            <div style="min-height: 324px" class="box-content">
                                <div style="font-size: 10pt; text-align: left; max-height: 36px">
                                    <strong><a href="#" id="hrfCategoryID2" runat="server"><asp:Label ID="lblCategoryID2" runat="server" Text="Seller Promos"></asp:Label></a></strong>
                                    <br />&nbsp;
                                </div>
                                 <asp:Panel id="pnlNoSellerPromo" runat='server' CssClass="alert alert-info" Visible="false">
							        No items available.
						        </asp:Panel>
                                <asp:Repeater ID="rprSellerPromos" runat="server">
                                <ItemTemplate>
    						        <div class="row-fluid">
                                        <div style="margin-top: 0px;min-height: 90px" class="span12">
                                            <asp:HyperLink ID="btnLinkSellerPromo" runat="server" NavigateUrl='<%#"whatsnew.aspx?newsid=" & Eval("ArtId")%>'>
                                                <img class="dashboard-avatar" alt="" src='<%#Eval("Thumbnail")%>'>
                                                <div style="margin-bottom: 7px;text-align: left;"><%#Eval("Title")%></div>
								                <p style="font-size: 8pt;"><%#CDate(Eval("DateFrom")).ToString("MMMM dd, yyyy") %></p>
								            </asp:HyperLink>
								        </div>
                                    </div>
                                </ItemTemplate>
                                </asp:Repeater>
                                <div style="text-align: right;padding-right: 5px;">
                                    <asp:HyperLink id="hplMoreSellersPromo" runat="server" NavigateUrl="promosandevents.aspx?newstitle=sellerpromos" Text="more..."  Visible="false">
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>

    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/jquery.event.move.js"></script>
    <script src="js/responsive-slider.js"></script>
</asp:Content>
