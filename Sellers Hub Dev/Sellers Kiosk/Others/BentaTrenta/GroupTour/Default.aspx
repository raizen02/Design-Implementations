<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="BentaTrenta_GroupTour_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html style="height:100%; width:100%; margin: 0; overflow: hidden;" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group Tour Rewards | Oplan Benta Trenta 2014</title>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="Keywords" content="" />
		<meta name="Description" content="" />
		<meta name="Generator" content="Microsoft FrontPage 6.0" />
		<script type="text/javascript" src="files/js/swfobject.js"></script>
		<script type="text/javascript" src="files/js/swfaddress.js"></script>

    <script type="text/javascript">

        var assetsFolder = 'res';
        var mobileFolder = 'mobile';

        var changeURL = function(){
                                if(document.getElementById('hrefMobile'))document.getElementById('hrefMobile').href = dir + mobileFolder + '/';
                                delete changeURL;
                                }
                                
        if (document.addEventListener){
                                    document.addEventListener("DOMContentLoaded", changeURL, false);
                                    } else {
		                            document.attachEvent("onDOMContentLoaded", changeURL);
                                    }

        function getURLParam()
        {
            var returnObject = {};
            var href = window.location.href;
            
            if ( href.indexOf("?") > -1 )
            {
                var param = href.substr(href.indexOf("?"));
			    var arrayParam = param.split("&");
	  
			    for ( var i = 0; i < arrayParam.length; i++ )
                {
                    var value = arrayParam[i].split("=");
                    
                    returnObject[value[0]] = value[1];
			    }
            }
            
			returnObject['res'] = assetsFolder;
			return returnObject;
        }
		
		var dir = "./files/";
	
        var getURI = function()
                    {
                        var URIArray = document.location.href.split('/');
                        URIArray.length = URIArray.length-1;
                        
                        var URIstr = URIArray.join('/');
                        URIArray = null;
        
                        var URIarr = dir.split('/');
                        URIarr[0] = URIarr[0]=='.'?'':URIarr[0];

                        var dirStr = URIarr.join('/');
                        URIstr = URIstr+dirStr;

                        return URIstr;
                    }
			
        var ua = navigator.userAgent.toLowerCase(),
            platform = navigator.platform.toLowerCase(),
            UA = ua.match(/(opera|ie|firefox|chrome|version)[\s\/:]([\w\d\.]+)?.*?(safari|version[\s\/:]([\w\d\.]+)|$)/) || [null, 'unknown', 0],
            mode = UA[1] == 'ie' && document.documentMode;

        var Browser = {
                        extend: Function.prototype.extend,
                        name: (UA[1] == 'version') ? UA[3] : UA[1],
                        version: mode || parseFloat((UA[1] == 'opera' && UA[4]) ? UA[4] : UA[2]),
                        Platform: {
                                    name: ua.match(/ip(?:ad|od|hone)/) ? 'ios' : (ua.match(/(?:webos|android|bada|symbian|palm|blackberry)/) || platform.match(/mac|win|linux/) || ['other'])[0]
                                },
                        Features: {
                                    xpath: !!(document.evaluate),
                                    air: !!(window.runtime),
                                    query: !!(document.querySelector),
                                    json: !!(window.JSON)
                                },
                        Plugins: {}
                        };
		
        var page = parseInt(window.location.hash.substring(2, window.location.hash.length-1));
        page = page?'#'+page:'';

        if(Browser.Platform.name == 'android' || Browser.Platform.name == 'ios') window.location =dir+mobileFolder+"/"+page;
        if(Browser.Platform.name == 'webos' || Browser.Platform.name == 'bada' || Browser.Platform.name == 'symbian' || Browser.Platform.name == 'palm' || Browser.Platform.name == 'blackberry') window.location = dir+'html/'+"/"; 
    
        var dir = "./files/";
    	var jsfolder = "js/";
    	var swffile = "book.swf";
    	
        var flashvars = {};
        var params = {
                        menu: "false",
				        scale: "noScale",
				        allowfullscreen: "true",
				        allowscriptaccess: "always",
				        bgcolor: "#ffffff",
				        wmode:"transparent"
			        };
        var attributes = {id: "magazine", name:"magazine"};
        swfobject.embedSWF(dir+swffile, "magazine", "100%", "100%", "10.1.0", dir+jsfolder+"expressInstall.swf", flashvars, params, attributes);
    </script>

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
<body style="padding: 0px; margin: 0px; height:100%; width:100%;">
    <form id="frmMain" runat="server">
    </form>
        <div id="magazine">
            <h1>
                Requires FlashPlayer</h1>
            <p>
                <a href="http://get.adobe.com/flashplayer/">
                    <img src="images/get_adobe_flash_player.png"
                        alt="Get Adobe Flash Player" /></a></p>
            <p>
                Please try the above link first. If you still encounter problems after installing
                the Flash Player, try this one:</p>
            <p>
                <a href="http://get.adobe.com/shockwave/">
                    <img src="images/get_adobe_shockwave_player.png"
                        alt="Get Adobe Shockwave Player" /></a></p>
            <p>
                <a id="hrefMobile" href="files/mobile/">Mobile version</a></p>
            <p>
                <a href="files/html/">HTML Version</a></p>
        </div>
</body>
</html>
