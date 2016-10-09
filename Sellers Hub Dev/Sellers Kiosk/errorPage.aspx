<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPageErr.master"
    AutoEventWireup="false"
    CodeFile="errorPage.aspx.vb"
    Inherits="errorPage"
    Title="Unauthorized Page | Sellers' HUB"%>
    
<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" Runat="Server">
<div style="text-align:justify">
    <div>
        <ul class="breadcrumb">
            <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
            <li><a href="#">Error Page</a> </li>
        </ul>
    </div>
    <div class="alert alert-error" id="divErrorMsgBox" runat="server">
        <div id="divErrorMsg" runat="server">
            Sorry, you are not authorized!
        </div>
    </div>
    <blockquote>
        <div class="box-content" id="divError403" runat="server" visible="false">
            <div class="row-fluid ">
                <div class="span6">
                    <h2>Access is denied</h2>
                    <p><%=Request.QueryString("aspxerrorpath")%></p>
                    <br />
                    <p>Sorry, you do not have permission to view this directory or page using the credentials that you supplied.</p>
                </div>
            </div>
        </div>
        <div class="box-content" id="divError404" runat="server" visible="false">
            <div class="row-fluid ">
                <div class="span6">
                    <h2>Page Not Found</h2>
                    <p><%=Request.QueryString("aspxerrorpath")%></p>
                    <br />
                    <p>Sorry, but the page you were looking for can’t be found. See below for what you can do about that.</p>
                    <blockquote>
                    <dl>
                        <dt>Most likely causes:</dt><ul>
                            <li>The directory or file specified does not exist on the Web server.</li>
                            <li>The URL contains a typographical error.</li>
                            <li>A custom filter or module, such as URLScan, restricts access to the file.</li>
                        </ul>
                    </dl>
                    </blockquote>
                </div>
            </div>
        </div>
        <div class="box-content" id="divError500" runat="server" visible="false">
            <div class="row-fluid ">
                <div class="span6">
                    <h2>
                        Exception Notice</h2>
                    <p><%=Request.QueryString("aspxerrorpath")%></p>
                    <br />
                    <p>Sorry, but the event that has just occurred was not expected by this system.</p>
                    <blockquote>
                    <dl>
                        <di></di>
                        An email message has already been sent to our Technical Services and Support team,
                        notifying them of the issue. If this problem is not solved within the next two hours,
                        please contact your system administrator to escalate its priority.
                    </dl>
                    </blockquote>
                </div>
            </div>
        </div>
    </blockquote>
</div>
</asp:Content>

