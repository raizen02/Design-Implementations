<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="profileinfo.aspx.vb"
    Inherits="profileinfo"
    Title="Profile Info | Sellers' HUB"%>

<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">Profile</a> </li>
        </ul>
    </div>
    <div class="alert alert-error" id="divErrorMsgBox" runat="server" visible="false">
		<button type="button" class="close" data-dismiss="alert">×</button>
	    <div id="divErrorMsg" runat="server"></div>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-user"></i>&nbsp;My Profile</h2>
            </div>
            <div class="box-content form-horizontal">
                <fieldset>
                    <legend>Personal Information</legend>
                    <div class="control-group">
                        <label class="control-label">
                            Full Name :</label>
                        <div class="controls">
                            <p style="padding-top: 5px;" id="spnFullName" runat="server"></p>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Date of Birth :</label>
                        <div class="controls">
                            <p style="padding-top: 5px;" id="spnDateBirth" runat="server"></p>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Gender :</label>
                        <div class="controls">
                            <p style="padding-top: 5px;" id="spnGender" runat="server"></p>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Marital Status :</label>
                        <div class="controls">
                            <p style="padding-top: 5px;" id="spnMaritalStatus" runat="server"></p>
                        </div>
                    </div>
                    <legend>Contact Information</legend>
                    <div id="divContactInfo" runat="server">
                        <h4>
                            Contacts</h4>
                        <div class="control-group">
                            <label class="control-label">
                                Mobile Number</label>
                            <div class="controls">
                                <span class="input-xxlarge uneditable-input"></span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Telephone Number</label>
                            <div class="controls">
                                <span class="input-xxlarge uneditable-input"></span>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Email Address</label>
                            <div class="controls">
                                <span class="input-xxlarge uneditable-input"></span>
                            </div>
                        </div>
                    </div>
                    <div id="divAddressInfo" runat="server">
                        <h4>
                            Address</h4>
                        <div class="control-group">
                            <label class="control-label">
                                Home Address</label>
                            <div class="controls">
                                <textarea class="input-xxlarge autogrow" readonly></textarea>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Office Address</label>
                            <div class="controls">
                                <textarea class="input-xxlarge autogrow" readonly></textarea>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</div>
</asp:Content>
