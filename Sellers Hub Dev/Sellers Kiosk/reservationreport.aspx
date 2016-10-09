<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="reservationreport.aspx.vb"
    Inherits="reservationreport" %>

<asp:Content ID="conContent" ContentPlaceHolderID="cphContent" Runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">Online Reservation Reports</a> </li>
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
            <div class="box-header well" data-original-title>
                <h2>
                    <i class="icon-book"></i>&nbsp;<span  id="spnReportTitle" runat="server" >Reports</span></h2>
            </div>
            <div class="box-content">
                <br />
                <div class="row-fluid">
                    <div class="span12" id="divPDFLoader" runat="server">
                    </div>
                </div>
                <br />
            </div>
        </div>
        <!--/span-->
    </div>
    <!--/row-->
    <!-- content ends -->
</div>
</asp:Content>



