<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="contactus.aspx.vb"
    Inherits="contactus"
    Title="Seller Inquiries | Sellers' HUB"%>
    
<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" Runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">Contact Us</a> </li>
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
                    <i class="icon-envelope"></i> Inquiry</h2>
            </div>
            <div class="box-content form-horizontal">
                <fieldset>
                    <div class="control-group">
                        <label class="control-label">
                            Date</label>
                        <div class="controls">
                            <span class="input-small uneditable-input" id="spnDate" runat="server"></span>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Name</label>
                        <div class="controls">
                            <span class="input-xxlarge uneditable-input" id="spnCustomerName" runat="server"></span>
                        </div>
                    </div>
                    <div class="control-group" id="divTelNum">
                        <label class="control-label">
                            Telephone Number</label>
                        <div class="controls">
                            <asp:TextBox
                                ID="txbTelNum"
                                runat="server"
                                ToolTip="Telephone Number"
                                CssClass="input-xxlarge focused"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvTelNum"
                                runat="server"
                                ControlToValidate="txbTelNum"
                                Display="Dynamic"
                                ErrorMessage="* Required Field"
                                CssClass="help-inline"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group" id="divEmailAdd">
                        <label class="control-label">
                            Email Address</label>
                        <div class="controls">
                            <asp:TextBox
                                ID="txbEmailAdd"
                                runat="server"
                                ToolTip="Email Address"
                                CssClass="input-xxlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvEmailAdd"
                                runat="server"
                                ControlToValidate="txbEmailAdd"
                                Display="Dynamic"
                                ErrorMessage="* Required Field"
                                CssClass="help-inline"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revEmailAdd"
                                runat="server"
                                ControlToValidate="txbEmailAdd"
                                ErrorMessage="* Invalid Email Address"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">Queries on :</label>
                        <div class="controls">
                            <label class="radio">
                                <asp:RadioButton ID="rdbAffiliation" runat="server" GroupName="grpQueries" />Affiliation/Accreditation
                            </label>
                            <div style="clear:both"></div>
                            <label class="radio">
                                <asp:RadioButton ID="rdbStatus" runat="server" GroupName="grpQueries" />Status of Previous Request
                            </label>
                            <div style="clear:both"></div>
                            <label class="radio">
                                <asp:RadioButton ID="rdbCommRelated" runat="server" GroupName="grpQueries" />Commission Related
                            </label>
                            <div style="clear:both"></div>
                            <label class="radio">
                                <asp:RadioButton ID="rdbPayments" runat="server" GroupName="grpQueries" />Payments
                            </label>
                            <div style="clear:both"></div>
                            <label class="radio">
                                <asp:RadioButton ID="rdbDocuRelated" runat="server" GroupName="grpQueries" />Documents Related
                            </label>
                            <div style="clear:both"></div>
                            <label class="radio">
                                <asp:RadioButton ID="rdbOthers" runat="server" Checked="True" GroupName="grpQueries" />Others
                            </label>
                        </div>
                    </div>
                    <div class="control-group"  id="divMessage">
                        <label class="control-label">
                            Message</label>
                        <div class="controls">
                            <asp:TextBox
                                ID="txbMessage"
                                runat="server"
                                TextMode="MultiLine"
                                ToolTip="Please insert message here..."
                                CssClass="input-xxlarge autogrow"
                                placeholder="Please insert message here..." ></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvMessage"
                                runat="server"
                                ControlToValidate="txbMessage"
                                Display="Dynamic"
                                ErrorMessage="* Required Field"
                                CssClass="help-inline"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    <div class="form-actions">
                        <asp:Button
                            ID="btnSubmit"
                            runat="server"
                            Text="Submit Form"
                            CssClass="btn btn-primary" />
                        <asp:Button
                            ID="btnReset"
                            runat="server"
                            Text="Reset"
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

