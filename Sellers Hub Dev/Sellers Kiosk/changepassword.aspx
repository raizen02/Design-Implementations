<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="changepassword.aspx.vb"
    Inherits="changepassword"
    Title="Change Password | Sellers' HUB"%>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="conContent" ContentPlaceHolderID="cphContent" runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">Change Password</a> </li>
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
                    <i class="icon-lock"></i> Change Password</h2>
            </div>
            <div class="box-content form-horizontal">
                <fieldset>
                    <div class="alert alert-info">
                        Enter your current password and then choose your new password.
                        <br />
                        Click Save when you are done.
                    </div>
                    <div class="control-group" id="divOldPassword">
                        <label class="control-label" for="txbOldPassword">
                            Old Password</label>
                        <div class="controls">
                            <asp:TextBox
                                ID="txbOldPassword"
                                runat="server"
                                ToolTip="Old Password"
                                TextMode="Password"
                                CssClass="input-xlarge focused"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvOldPassword"
                                runat="server"
                                ControlToValidate="txbOldPassword"
                                Display="Dynamic"
                                ErrorMessage="Input your old password!" 
                                CssClass="help-inline"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group" id="divNewPassword">
                        <label class="control-label" for="txbNewPassword">
                            New Password</label>
                        <div class="controls">
                            <asp:TextBox
                                ID="txbNewPassword"
                                runat="server"
                                ToolTip="New Password"
                                TextMode="Password"
                                CssClass="input-xlarge" ></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvNewPassword"
                                runat="server"
                                ControlToValidate="txbNewPassword"
                                Display="Dynamic"
                                ErrorMessage="Input your new password!" 
                                CssClass="help-inline"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group" id="divConfirmPassword">
                        <label class="control-label" for="txbConfirmPassword">
                            Confirm Password</label>
                        <div class="controls">
                            <asp:TextBox
                                ID="txbConfirmPassword"
                                runat="server"
                                ToolTip="Confirm Password"
                                TextMode="Password"
                                CssClass="input-xlarge" ></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvConfirmPassword"
                                runat="server"
                                ControlToValidate="txbConfirmPassword"
                                Display="Dynamic"
                                ErrorMessage="Please confirm your new password"
                                CssClass="help-inline"></asp:RequiredFieldValidator>
                            <asp:CompareValidator
                                ID="cmvConfirmPassword"
                                runat="server"
                                ControlToCompare="txbNewPassword"
                                ControlToValidate="txbConfirmPassword"
                                Display="Dynamic"
                                ErrorMessage="Please confirm your new password"
                                CssClass="help-inline"></asp:CompareValidator>
                        </div>
                    </div>
                    <recaptcha:RecaptchaControl
                        ID="rccReCaptcha"
                        runat="server"
                        data-theme="light" />

                    <div class="form-actions">
                        <asp:Button
                            ID="btnSave"
                            runat="server"
                            Text="Save changes"
                            CssClass="btn btn-primary" />
                        <asp:Button
                            ID="btnCancel"
                            runat="server"
                            Text="Cancel"
                            CausesValidation="False"
                            EnableTheming="False"
                            EnableViewState="False"
                            UseSubmitBehavior="False"
                            CssClass="btn"/>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</div>
</asp:Content>
