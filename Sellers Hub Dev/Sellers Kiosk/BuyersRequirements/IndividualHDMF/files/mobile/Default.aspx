<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="BuyersRequirements_IndividualHDMF_files_mobile_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Individual HDMF (Mobile) | Buyer's Requirements</title>

    <meta http-equiv="Content-Type" content="application/xhtml+xml; charset=utf-8" />
    <meta id="viewMeta" name="viewport" content="width=device-width, user-scalable=no, maximum-scale=1.0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link href="styles/style.css" type="text/css" rel="stylesheet" />
    <link rel="apple-touch-icon" href="../res/mobile/page0001_i1.jpg" />
    <script type="text/javascript">var dir = "./files";
			      STYLES_SRC = "styles/";
				  var assetsFolder = "./res";</script><script type="text/javascript" src="javascript/book.js"></script>

    <!-- Google Analytics -->
    <script>

      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,'script','//www.google-analytics.com/analytics.js','ga');
     
      ga('create', '<%=MyTrackingCode %>', 'filinvest.com.ph');
      var SellerName = '<%= ViewState("Fullname")%>';
      var SellerType = '<%= Session("sesSelSellerType") %>'; 
      ga('set', 'dimension1', SellerName); // dimension1 = Seller Name
      ga('set', 'dimension2', SellerType);  //  dimension2 = Seller Type
      ga('set', 'dimension3', SellerName + ' / ' + SellerType );  //  dimension3 = Seller Name / Seller Type
      ga('send', 'pageview'); 

    </script>
</head>
<body>
    <form id="frmMain" runat="server">
    </form>
    <div id="scaler">
      <div id="body" class="body"><div class="mainFrame" id="mainFrame"></div></div>
    </div>
</body>
</html>
