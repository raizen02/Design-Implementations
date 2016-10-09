<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="BentaTrenta.aspx.vb"
    Inherits="BentaTrenta"
    title="Oplan Benta Trenta 2014 | Sellers' HUB" %>
    
<asp:Content ID="conContent" ContentPlaceHolderID="cphContent" Runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">Oplan Benta Trenta 2014</a> </li>
        </ul>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2>
                    <i class="icon-tasks"></i>&nbsp;Oplan Benta Trenta 2014</h2>
            </div>
            <div class="box-content">
                    <div class="row-fluid" id="divIcons1" runat="server">
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="AnnualCluster/" target="_blank">
                                <img class="center" src="images/AnnualCluster.png" /></a>
                        </div>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="SalesIncentivesContest/" target="_blank">
                                <img class="center" src="images/SalesIncentivesContest.png" /></a>
                        </div>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="CorporateAwards/" target="_blank">
                                <img class="center" src="images/CorporateAwards.png" /></a>
                        </div>
                    </div>
                    <div class="row-fluid" id="divIcons2" runat="server">
<%--                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="GoldAwardsFil/" target="_blank">
                                <img class="center" src="images/GoldAwardsFil.png" /></a>
                        </div>--%>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="GroupTour/" target="_blank">
                                <img class="center" src="images/GroupTour.png" /></a>
                        </div>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="RaffleAwards/" target="_blank">
                                <img class="center" src="images/RaffleAwards.png" /></a>
                        </div>
                        <div class="span4 feat" style="margin-bottom: 10px">
                            <a href="SpecialAwards/" target="_blank">
                                <img class="center" src="images/SpecialAwards.png" /></a>
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

