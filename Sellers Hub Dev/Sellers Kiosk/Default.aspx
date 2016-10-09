<%@ Page Language="VB"
    AutoEventWireup="false"
    CodeFile="Default.aspx.vb"
    Inherits="DefaultLogin"
    Title="Login Page | Sellers' HUB"%>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="frmHead" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Filinvest Sellers' Hub" />
    <meta name="author" content="Filinvest Land, Inc." />
    <!-- The styles -->
    <link class="bscss" href="css/bootstrap-cerulean.css" rel="stylesheet" />
    <style type="text/css">
	  body {
		padding-bottom: 40px;
		background-image:url('img/loginbg.jpg');
		background-repeat:no-repeat;
		background-size:cover;
		background-attachment:fixed
	  }
	  
	  .sidebar-nav {
		padding: 9px 0;
	  }
	</style>
    <link href="css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/charisma-app.css" rel="stylesheet" />

    <!-- Google Analytics -->
    <script>

      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,'script','//www.google-analytics.com/analytics.js','ga');
     
      ga('create', '<%=MyTrackingCode %>', 'filinvest.com.ph');
      var SellerName = 'Guest';
      var SellerType = ''; 
      ga('set', 'dimension1', SellerName); // dimension1 = Seller Name
      ga('set', 'dimension2', SellerType);  //  dimension2 = Seller Type
      ga('set', 'dimension3', SellerName);  //  dimension3 = Seller Name / Seller Type
      ga('send', 'pageview'); 

    </script>
    
    <!-- The HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
	  <script src="js/html5shiv.js"></script>
	<![endif]-->
    <!-- The fav icon -->
    <!-- <link rel="shortcut icon" href="img/favicon.ico"> -->
</head>
<body>
<form id="frmMain" runat="server">
    <asp:ScriptManager ID="scmCallback" runat="server" />

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(
            function () {
                docReady();
            });
    </script>

    <asp:UpdateProgress ID="uprLoading" AssociatedUpdatePanelID="updUpdatePanel" DynamicLayout="true"
        DisplayAfter="100" runat="server">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <img alt="Loading..." src="images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="navbar" style="margin-bottom: 2px;">
        <div class="navbar-inner">
            <!-- container-fluid starts -->
            <div class="container-fluid">
                <a class="brand" href="#">
                    <img alt="Filinvest Logo" src="img/logo.png" /></a>
            </div>
            <!-- container-fluid ends -->
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span7">
                <center>
                    <img border="0" src="img/imglogin.png">
                </center>
            </div>
            <div class="span5">
                <center>
                    <img border="0" src="img/imgsellershub.gif">
                </center>
                <div class="row-fluid">
                    <div class="center span8" style="color: #548cae">
                        <p>
                            <b>Welcome Filinvest Dream Builder!</b>
                        </p>
                        <p>
                            This is the online hub of the Filinvest Sales Network. Sign up or log in to your
                            account and be part of the growing number of Filinvest dream builders who are making
                            their own dreams come true.
                        </p>
                    </div>
			        <div class="well center span8 login-box">
				        <div class="alert alert-block">
					        LOG IN TO YOUR ACCOUNT
				        </div>
                        <asp:UpdatePanel ID="updUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                            <ContentTemplate>
				                <fieldset>
					                <div class="input-prepend" title="Username" data-rel="tooltip" style="text-align: left; margin-left: 15px;">
						                <span class="add-on"><i class="icon-user"></i></span><asp:TextBox ID="txbUsername" runat="server" placeholder="Username" CssClass="input-large span10" autofocus></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txbUsername"
                                            Display="Dynamic" ErrorMessage="<br/>Please enter username." SetFocusOnError="True"
                                            ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
					                </div>
					                <div class="clearfix"></div>

					                <div class="input-prepend" title="Password" data-rel="tooltip" style="text-align: left; margin-left: 15px;">
						                <span class="add-on"><i class="icon-lock"></i></span><asp:TextBox ID="txbPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="input-large span10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txbPassword"
                                            Display="Dynamic" ErrorMessage="<br/>Please enter password." SetFocusOnError="True"
                                            ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cmvErrorMsg" runat="server" ErrorMessage="CustomValidator" ValidationGroup="vgLogin" SetFocusOnError="True"></asp:CustomValidator>
					                </div>
					                <p class="span5">
                                        <asp:Button ID="btnSignin" runat="server" Text="Login" CssClass="btn btn-primary" ValidationGroup="vgLogin" Style="margin-top:0px;"/>
					                </p>
<%--					                <div class="clearfix"></div>--%>

					                <p style="text-align: right; padding-top:2px;">
					                <a href="<%=MySSOSite%>forgotpassword.aspx">Can't access your account?</a>
					                </p>
				                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
			        </div><!--/span-->
                    <div class="center span8">
                        <p style="text-align: right">
                            <a href="<%=MySSOSite%>signup.aspx">Don't have a seller account yet?</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
            </div>
        </div>
	    <div style="background-image:url('img/horizontalline.jpg'); height:2px">
	    </div>
	    <footer>
		    <p class="pull-left">&copy;2013 Filinvest. All Rights Reserved.</p>
		    <div class="pull-right" style="margin-top: 3px;">
                <!--- DO NOT EDIT - GlobalSign SSL Site Seal Code - DO NOT EDIT --->
                <table width="90" border="0" cellspacing="0" cellpadding="0" title="CLICK TO VERIFY: This site uses a GlobalSign SSL Certificate to secure your personal information.">
                    <tr>
                        <td>
                            <span id="ss_img_wrapper_gmogs_image_90-35_en_dblue">
                                <a href="https://www.globalsign.com/" target="_blank" title="SSL">
                                    <img alt="SSL" border="0" id="ss_img" src="//seal.globalsign.com/SiteSeal/images/gmogs_image_90-35_en_dblue.png">
                                </a>
                            </span>
                            <script type="text/javascript" src="//seal.globalsign.com/SiteSeal/gmogs_image_90-35_en_dblue.js">
                            </script></td>
                    </tr>
                </table>
                <!--- DO NOT EDIT - GlobalSign SSL Site Seal Code - DO NOT EDIT --->
		    </div>
	    </footer>
    </div>
</form>
	<!-- external javascript
	================================================== -->
	<!-- Placed at the end of the document so the pages load faster -->

	<!-- jQuery -->
	<script src="js/jquery-1.7.2.min.js"></script>
	<!-- library for advanced tooltip -->
	<script src="js/bootstrap-tooltip.js"></script>
	<!-- library for cookie management -->
	<script src="js/jquery.cookie.js"></script>
	<!-- application script for Charisma -->
	<script src="js/charisma_login.js"></script>
</body>
</html>
