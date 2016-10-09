<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="SellerIncentive2016.aspx.vb"
    Inherits="Others_SellerIncentive2016"
    title="2016 Incentives & Contest Deck | Sellers' HUB" %>
    
<asp:Content ID="conContent" ContentPlaceHolderID="cphContent" Runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">2016 Incentives & Contest Deck</a> </li>
        </ul>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2>
                    <i class="icon-tasks"></i>&nbsp;2016 Incentives & Contest Deck</h2>
            </div>
            <div class="box-content">
                    <div class="row-fluid" id="divIcons1" runat="server">
                        <div class="span6 feat" style="margin-bottom: 10px">
                            <a href="Others/SellerIncentive2016/sic_p1/" target="_blank">
                                <img class="center" src="Others/SellerIncentive2016/images/2016SIC_p1.jpg" /></a>
                        </div>
                        <div class="span6 feat" style="margin-bottom: 10px">
                            <a href="Others/SellerIncentive2016/sic_p2/" target="_blank">
                                <img class="center" src="Others/SellerIncentive2016/images/2016SIC_p2.jpg" /></a>
                        </div>
                    </div>
                    <div class="row-fluid" id="divIcons2" runat="server">
<%--                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="GoldAwardsFil/" target="_blank">
                                <img class="center" src="images/GoldAwardsFil.png" /></a>
                        </div>--%>
                        <div class="span6 feat" style="margin-bottom: 10px">
                            <a href="Others/SellerIncentive2016/sic_p3/" target="_blank">
                                <img class="center" src="Others/SellerIncentive2016/images/2016SIC_p3.jpg" /></a>
                        </div>
                        <div class="span6 feat" style="margin-bottom: 10px">
                            <a href="Others/SellerIncentive2016/sic_p4/" target="_blank">
                                <img class="center" src="Others/SellerIncentive2016/images/2016SIC_p4.jpg" /></a>
                        </div>
                    </div>
<%--                    <div class="row-fluid" id="divIcons3" runat="server">
                    </div>--%>
                </div>
        </div>
        <!--/span-->
    </div>
    <!--/row-->
    <!-- content ends -->
</div>
</asp:Content>

