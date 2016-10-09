<%@ Page Language="VB" MasterPageFile="~/sellersHubMasterPage.master" AutoEventWireup="false" CodeFile="downloads.aspx.vb" Inherits="downloads" title="Downloads | Sellers' HUB" %>

<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" Runat="Server">
    <div style="text-align:justify">
        <div>
            <ul class="breadcrumb">
                <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                <li><a href="#">&nbsp;Downloads</a> </li>
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
                        <i class="icon-download-alt"></i>&nbsp;Downloadable Forms</h2>
                </div>
                <div style="text-align: justify" class="row-fluid">
                    <div class="span12">
                        <div class="box-content">
                            <ul style="padding-left: 40px;">
                                <li><a href="downloads/afftforeignsp.pdf">Affidavit of Foreign Spouse</a></li>
                                <li><a href="downloads/afftlostsp.pdf">Affidavit of Lost Spouse</a></li>
                                <li><a href="downloads/intlbuyerrepinfo.pdf">International Buyer Authorized Representative Information Sheet</a></li>
                                <li><a href="downloads/provreserv.pdf">Provisional Reservation</a></li>
                                <li><a href="downloads/swornstatement.pdf">Sworn Statement</a></li>
                                <li><a href="downloads/universalspa.pdf">Universal Special Power of Attorney</a></li>
                                <li><a href="downloads/certificateengagement.pdf">Certificate of Engagement</a></li>
                                <li>Insurance Form
                                <ul>
                                    <li><a href="downloads/SL_9June2015.pdf">Sun Life Financial</a></li>
                                    <li><a href="downloads/SLGFIDebtorsApp_9June2015.pdf">Sun Life GREPA Financial</a></li>
                                </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
