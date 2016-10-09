<%@ Page Language="C#" MasterPageFile="~/FilinvestAccountMasterPage.master" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphForm" Runat="Server">
      <div class="container-fluid">
            <div class="row-fluid">
				<div class="box span12">
					<div class="box-header well" style="cursor:auto;">
						<h2><i class="icon-edit"></i> Sign Up for a Filinvest Account</h2>						
					</div>
					<div class="box-content">					
                        <fieldset>
                            <legend>This will serve as your account for Seller's Hub and eCRM</legend>
                            <asp:ValidationSummary ID="vsServerError" runat="server" ValidationGroup="vg2" CssClass="alert alert-error" EnableClientScript="false" ForeColor="#bd4247" DisplayMode="List" />
                            <asp:CustomValidator ID="cvServerError" runat="server" ValidationGroup="vg2" Display="None"></asp:CustomValidator>
                            
                            <asp:ValidationSummary ID="vsSuccess" runat="server" ValidationGroup="vg3" CssClass="alert alert-success" EnableClientScript="false" ForeColor="#669533" DisplayMode="List" />
                            <asp:CustomValidator ID="cvSuccess" runat="server" ValidationGroup="vg3" Display="None"></asp:CustomValidator>
                            
                            <asp:Panel ID="pnlFields" runat="server">
                                <div class="control-group">
                                <label class="control-label">Username</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbUsername" CssClass="input-xlarge"  runat="server"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbUsername" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Username is requred."  Display="Dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>
                                     <asp:CustomValidator ID="cvUserName" CssClass="help-inline" ControlToValidate="txbUsername" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                        ErrorMessage="Username already in use."></asp:CustomValidator>                                     
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbPassword" CssClass="input-xlarge" runat="server" TextMode="Password" autocomplete="off"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txbPassword" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Password is requred." EnableClientScript="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Confirm Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbConfirmPassword" CssClass="input-xlarge" TextMode="Password" runat="server" autocomplete="off"/>                                    
                                   <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txbConfirmPassword" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Password confimation is requred." Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <asp:CompareValidator id="CompareValidator1" CssClass="help-inline" runat="server" ControlToValidate="txbConfirmPassword" ControlToCompare="txbPassword"
                                       ValidationGroup="vg1" Display="Dynamic" ForeColor="#bd4247" ErrorMessage="Passwords did not match." ></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Email Address</label>
                                <div class="controls">
                                    <asp:TextBox ID="txbEmailAddr" CssClass="input-xlarge"  runat="server"/>
                                    <asp:RequiredFieldValidator CssClass="help-inline" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txbEmailAddr" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ErrorMessage="Email address is requred." Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="help-inline" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txbEmailAddr" 
                                        ValidationGroup="vg1" ForeColor="#bd4247" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid email format."  Display="Dynamic"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="cvEmail" CssClass="help-inline" runat="server" ValidationGroup="vg1" Display="Dynamic" ForeColor="#BD4247" 
                                        ControlToValidate="txbEmailAddr"></asp:CustomValidator>
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
                            <div class="form-actions">
                                <asp:button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text ="Submit" OnClick="btnSubmit_Click" ValidationGroup="vg1" />
                                <asp:button ID="btnClear" runat="server" CssClass="btn" Text ="Clear" OnClick="btnClear_Click" />
                            </div>
                            </asp:Panel>                           
                        </fieldset>
			            <asp:HyperLink id="hlLogin" runat="server" Visible="false" CssClass="btn btn-primary" Text="LOGIN HERE!!!"></asp:HyperLink>
			            <asp:HyperLink id="hlSignup" runat="server" NavigateUrl="signup.aspx" Visible="false" CssClass="btn btn-primary" Text="SIGN UP HERE!!!"></asp:HyperLink>
					</div>
				</div><!--/span-->
			</div><!--/row-->
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphJava" Runat="Server">
    <script type="text/javascript">
                
        $(function(){          
            $("#<%= txbTIN.ClientID %>").mask("***-***-***");
            
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
        
        function ValidateUserInputs_KjseoD() {
            var parentDiv;
        
            for (CtlCtr = 1; CtlCtr <= Page_Validators.length - 1;CtlCtr++)
            {
                if (Page_Validators[CtlCtr].isvalid == "False")
                {
                    parentDiv = $("#" + Page_Validators[CtlCtr].id).closest('div[class*="control-group"]');
                    parentDiv.addClass("error");
                }    
            }
        };
        
        
    </script>
</asp:Content>

