<%@ Page Language="C#" MasterPageFile="~/FilinvestAccountMasterPage.master" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphForm" Runat="Server">
    <div class="container-fluid">
            <div class="row-fluid">
				<div class="box span12">
					<div class="box-header well" style="cursor:auto;">
						<h2><i class="icon-edit"></i> Forgot Password for a Seller Account</h2>
					</div>
					<div class="box-content">
                        <fieldset>
                            <legend>Fill up info and recover your account</legend>
                            <asp:ValidationSummary ID="vsServerError" runat="server" ValidationGroup="vg2" CssClass="alert alert-error" EnableClientScript="false" ForeColor="#bd4247" DisplayMode="List" />
                            <asp:CustomValidator ID="cvServerError" runat="server" ValidationGroup="vg2" Display="None"></asp:CustomValidator>
                            
                            <asp:ValidationSummary ID="vsSuccess" runat="server" ValidationGroup="vg3" CssClass="alert alert-success" EnableClientScript="false" ForeColor="#669533" DisplayMode="List" />
                            <asp:CustomValidator ID="cvSuccess" runat="server" ValidationGroup="vg3" Display="None"></asp:CustomValidator>
                            
                            <asp:Panel ID="pnlFields" runat="server">
                            <div class="control-group">
                                <label class="control-label">Username</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbUsername" CssClass="input-xlarge"  runat="server" />
                                    <asp:CustomValidator ID="CustomValidator2" CssClass="help-inline" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                        ControlToValidate="txbUsername" ErrorMessage="" ClientValidationFunction="RequiredUserOrEmail" ValidateEmptyText="true"></asp:CustomValidator>
                                    <asp:CustomValidator ID="CustomValidator4" CssClass="help-inline" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                         ControlToValidate="txbUsername" ClientValidationFunction="SelectOne"  ValidateEmptyText="true"></asp:CustomValidator>
                                    <asp:CustomValidator ID="cvUserName" CssClass="help-inline" ControlToValidate="txbUsername" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                        ErrorMessage="Username already in use."></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls input-xlarge">
                                    <div class="center">
                                        OR
                                    </div>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Email Address</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbEmail" CssClass="input-xlarge"  runat="server" />
                                    <asp:CustomValidator ID="CustomValidator1" CssClass="help-inline" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                         ControlToValidate="txbEmail" ErrorMessage="Username or Email Address is required." ClientValidationFunction="RequiredUserOrEmail"  ValidateEmptyText="true"></asp:CustomValidator>
                                    <asp:CustomValidator ID="CustomValidator3" CssClass="help-inline" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                         ControlToValidate="txbEmail" ErrorMessage="Cannot have both Username And Email Address with entries for validation." ClientValidationFunction="SelectOne"  ValidateEmptyText="true"></asp:CustomValidator>
                                     <asp:CustomValidator ID="cvEmail" CssClass="help-inline" ControlToValidate="txbEmail" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                        ErrorMessage="Email Address already in use."></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Seller Code</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbSellerCode" CssClass="input-xlarge"  runat="server"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txbSellerCode" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Seller code is requred." Display="Dynamic" ></asp:RequiredFieldValidator>
                                     <asp:CustomValidator ID="cvSellers" CssClass="help-inline" ControlToValidate="txbSellerCode" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247"></asp:CustomValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Birthday</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbBirthday" CssClass="input-xlarge datepicker"  runat="server"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txbBirthday" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Birthday is requred." Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <asp:CompareValidator id="CompareValidator2" CssClass="help-inline" runat="server" ControlToValidate="txbBirthday" Operator="GreaterThan" ValueToCompare="1/1/1900" Type="Date"
                                       ValidationGroup="vg1" Display="Dynamic" ForeColor="#bd4247" ErrorMessage="Invalid date format. Format: MM/dd/yyyy" ></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">TIN</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbTIN" CssClass="input-xlarge"  runat="server"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txbTIN" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="TIN is requred." Display="Dynamic" ></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                     <recaptcha:RecaptchaControl
                                        ID="rccReCaptcha"
                                        runat="server" OnInit="rccReCaptcha_Init"/>
                                </div>
                            </div>                            
                            <div class="form-actions">
                                <asp:button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text ="Submit" OnClick="btnSubmit_Click" ValidationGroup="vg1" />
                                <asp:button ID="btnClear" runat="server" CssClass="btn" Text ="Clear" />
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
        function RequiredUserOrEmail(source, args)
        {   
            if ($("#<%=txbUsername.ClientID %>").val() == "" &&
                    $("#<%=txbEmail.ClientID %>").val() == "")
            {
                args.IsValid = false;
            }
        }
        
        function SelectOne(source, args)
        {   
            if ($("#<%=txbUsername.ClientID %>").val() != "" &&
                    $("#<%=txbEmail.ClientID %>").val() != "")
            {
                args.IsValid = false;
            }
        }
        

        $(function(){
            $("#<%= txbTIN.ClientID %>").mask("***-***-***");
            $("#recaptcha_widget_div").css(
                {'position':'relative',
                    'left':'-3px'});
            
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

