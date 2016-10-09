<%@ Page Language="C#" MasterPageFile="~/FilinvestAccountMasterPage.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepasswordcs" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphForm" Runat="Server">
     <div class="container-fluid">
            <div class="row-fluid">
				<div class="box span12">
					<div class="box-header well" style="cursor:auto;">
						<h2><i class="icon-edit"></i>Filinvest Account Change Password</h2>						
					</div>
					<div class="box-content">					
                        <fieldset>
                            <legend>Change account password</legend>
                            <asp:ValidationSummary ID="vsServerError" runat="server" ValidationGroup="vg2" CssClass="alert alert-error" EnableClientScript="false" ForeColor="#bd4247" DisplayMode="List" />
                            <asp:CustomValidator ID="cvServerError" runat="server" ValidationGroup="vg2" Display="None"></asp:CustomValidator>
                            
                            <asp:ValidationSummary ID="vsSuccess" runat="server" ValidationGroup="vg3" CssClass="alert alert-success" EnableClientScript="false" ForeColor="#669533" DisplayMode="List" />
                            <asp:CustomValidator ID="cvSuccess" runat="server" ValidationGroup="vg3" Display="None"></asp:CustomValidator>
                            
                            <asp:Panel ID="pnlFields" runat="server">
                                 <div class="control-group">
                                <label class="control-label">Username</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbUsername" CssClass="input-xlarge"  runat="server" />
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbUsername" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Username is requred."  Display="Dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>                                     
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Old Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbOldPassword" CssClass="input-xlarge" runat="server" TextMode="Password" autocomplete="off"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txbOldPassword" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Old Password is requred." EnableClientScript="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">New Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbNewPassword" CssClass="input-xlarge" runat="server" TextMode="Password"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txbNewPassword" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="New Password is requred." EnableClientScript="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Confirm Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbConfirmPassword" CssClass="input-xlarge" TextMode="Password" runat="server"/>                                    
                                   <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txbConfirmPassword" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Password confimation is requred." Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <asp:CompareValidator id="CompareValidator1" CssClass="help-inline" runat="server" ControlToValidate="txbConfirmPassword" ControlToCompare="txbNewPassword"
                                       ValidationGroup="vg1" Display="Dynamic" ForeColor="#bd4247" ErrorMessage="Passwords did not match." ></asp:CompareValidator>
                                </div>
                            </div>
                            <recaptcha:RecaptchaControl
                                ID="rccReCaptcha"
                                runat="server" OnInit="rccReCaptcha_Init"/>
                            <div class="form-actions">
                                <asp:button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text ="Submit" ValidationGroup="vg1" OnClick="btnSubmit_Click" />
                                <asp:button ID="btnClear" runat="server" CssClass="btn" Text ="Clear" OnClick="btnClear_Click" />
                            </div>
                            </asp:Panel>                           
                        </fieldset>		
			            
					</div>
				</div><!--/span-->
			</div><!--/row-->
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJava" Runat="Server">
    <script type="text/javascript">
        $(function(){
            if (typeof ValidatorUpdateDisplay != 'undefined') 
            {
                var originalValidatorUpdateDisplay = ValidatorUpdateDisplay;

                ValidatorUpdateDisplay = function (val) {
                    
                    var parentDiv = $("#" + val.controltovalidate).closest('div[class*="control-group"]');
                    
                    $("#" + val.id).css('color','');
                    
                    if (!val.isvalid) {
                        parentDiv.addClass("error");
                    }
                    else
                    {
                        parentDiv.removeClass("error");
                        
                        for (CtlCtr = 1; CtlCtr <= Page_Validators.length - 1;CtlCtr++)
                        {
                            if (val.controltovalidate == Page_Validators[CtlCtr].controltovalidate && val != Page_Validators[CtlCtr])
                            {
                                if (!Page_Validators[CtlCtr].isvalid)
                                {
                                    parentDiv.addClass("error");
                                    break;
                                }                                
                            }
                        }
                    }

                    originalValidatorUpdateDisplay(val);
                }
            }
        });
    </script >
</asp:Content>

