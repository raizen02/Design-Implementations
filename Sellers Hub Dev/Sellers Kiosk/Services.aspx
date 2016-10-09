<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Services.aspx.vb" Inherits="Services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
<script type = "text/javascript">
    function ShowCurrentTime() {
        $.ajax({
            type: "POST",
            url: "Services.aspx/GetCurrentTime",
            data: '{name: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function(response) {
                alert(response.d);
            }
        });
    }
    function OnSuccess(response) {
        alert(response.d);
    }
</script> 
</head>
<body style = "font-family:Arial; font-size:10pt">
<form id="form1" runat="server">
<div>
    Your Name : 
    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <input id="btnGetTime" type="button" value="Show Current Time" onclick = "ShowCurrentTime()" />
</div>
</form>
</body>
    
        <script src="js/jquery-1.7.2.min.js"></script>

        <!-- jQuery UI -->

        <script src="js/jquery-ui-1.8.21.custom.min.js"></script>

        <!-- transition / effect library -->

        <script src="js/bootstrap-transition.js"></script>

        <!-- alert enhancer library -->

        <script src="js/bootstrap-alert.js"></script>

        <!-- modal / dialog library -->

        <script src="js/bootstrap-modal.js"></script>

        <!-- custom dropdown library -->

        <script src="js/bootstrap-dropdown.js"></script>

        <!-- scrolspy library -->

        <script src="js/bootstrap-scrollspy.js"></script>

        <!-- library for creating tabs -->

        <script src="js/bootstrap-tab.js"></script>

        <!-- library for advanced tooltip -->

        <script src="js/bootstrap-tooltip.js"></script>

        <!-- popover effect library -->

        <script src="js/bootstrap-popover.js"></script>

        <!-- button enhancer library -->

        <script src="js/bootstrap-button.js"></script>

        <!-- accordion library (optional, not used in demo) -->

        <script src="js/bootstrap-collapse.js"></script>

        <!-- carousel slideshow library (optional, not used in demo) -->

        <script src="js/bootstrap-carousel.js"></script>

        <!-- autocomplete library -->

        <script src="js/bootstrap-typeahead.js"></script>

        <!-- tour library -->

        <script src="js/bootstrap-tour.js"></script>

        <!-- library for cookie management -->

        <script src="js/jquery.cookie.js"></script>

        <!-- calander plugin -->

        <script src='js/fullcalendar.min.js'></script>

        <!-- data table plugin -->

        <script src='js/jquery.dataTables.min.js'></script>

        <!-- chart libraries start -->

        <script src="js/excanvas.js"></script>

        <script src="js/jquery.flot.min.js"></script>

        <script src="js/jquery.flot.pie.min.js"></script>

        <script src="js/jquery.flot.stack.js"></script>

        <script src="js/jquery.flot.resize.min.js"></script>

        <!-- chart libraries end -->
        <!-- select or dropdown enhancer -->

        
        <script src="js/chosen.jquery.js"></script>

        <!-- checkbox, radio, and file input styler -->

        <script src="js/jquery.uniform.min.js"></script>

        <!-- plugin for gallery image view -->

        <script src="js/jquery.colorbox.min.js"></script>

        <!-- rich text editor library -->

        <script src="js/jquery.cleditor.min.js"></script>

        <!-- notification plugin -->

        <script src="js/jquery.noty.js"></script>

        <!-- file manager library -->

        <script src="js/jquery.elfinder.min.js"></script>

        <!-- star rating plugin -->

        <script src="js/jquery.raty.min.js"></script>

        <!-- for iOS style toggle switch -->

        <script src="js/jquery.iphone.toggle.js"></script>

        <!-- autogrowing textarea plugin -->

        <script src="js/jquery.autogrow-textarea.js"></script>

        <!-- multiple file upload plugin -->

        <script src="js/jquery.uploadify-3.1.min.js"></script>

        <!-- history.js for cross-browser state change on ajax -->

        <script src="js/jquery.history.js"></script>

        <!-- application script for Charisma demo -->

        <script src="js/charisma.js"></script>
        
</html>
