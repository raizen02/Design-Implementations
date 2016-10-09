<%@ Page Language="VB" AutoEventWireup="false" CodeFile="selSliderPreview.aspx.vb"
    Inherits="selSliderPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slider Preview</title>
     <link href='http://fonts.googleapis.com/css?family=Economica' rel='stylesheet' type='text/css'>
    <!-- Respomsive slider -->
    <link href="css/responsive-slider.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="js/jquery.event.move.js" type="text/javascript"></script>
    
    <style type="text/css">
        .alert { padding: 19px 15px; color: #fefefe; position: relative; font: 14px/20px Museo300Regular, Helvetica, Arial, sans-serif; font-weight:bold;}
        .alert .msg { padding: 0 20px 0 40px;}
        .notice-box {	background: #f6ca2f url(images/msgwarning.png) no-repeat 14px 14px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="max-width: 725px;">
        <asp:Panel id="pnlSession" runat="server" CssClass="alert notice-box">
            <div class="msg">Session for preview is not available. Please restart preview.</div>
        </asp:Panel>
        <div class="responsive-slider" data-spy="responsive-slider" data-autoplay="true">
            <div class="slides" data-group="slides">
                <ul>
                    <asp:Repeater ID="rptSlides" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# iif(IsDbNull(DataBinder.Eval(Container.DataItem, "URL")) = FALSE, DataBinder.Eval(Container.DataItem, "URL").ToString(), "#")%>' target="_blank">
                                    <div class="slide-body" data-group="slide">
                                        <img src='img/slider/preview/<%#DataBinder.Eval(Container.DataItem, "ImageFileName")%>' alt="">
                                        <asp:Repeater ID="rptSlideCaption" runat="server">
                                            <ItemTemplate>
                                                <div class="caption" data-animate='<%#DataBinder.Eval(Container.DataItem, "AnimationDataKeyword")%>'
                                                    data-delay='<%#DataBinder.Eval(Container.DataItem, "ShowDelay")%>' 
                                                    style='top:<%#DataBinder.Eval(Container.DataItem, "PositionTop")%>%;left:<%#DataBinder.Eval(Container.DataItem, "PositionLeft")%>%;width:<%#DataBinder.Eval(Container.DataItem, "Width")%>%;'>
                                                    <img src='img/slider/preview/<%#DataBinder.Eval(Container.DataItem, "ImageFileName")%>' alt="">
                                                </div>
                                            </Itemtemplate>
                                        </asp:Repeater>
                                    </div>
                                </a>
                            </li>
                        </itemtemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <%--<a class="slider-control left" href="#" data-jump="prev"><</a>
                <a class="slider-control right" href="#" data-jump="next">></a>--%>            
            <asp:Repeater ID="rptSlidePage" runat="server">
                <HeaderTemplate>
                    <div class="pages">
                </HeaderTemplate>
                <ItemTemplate>
                    <a class="page" href="#" data-jump-to='<%#DataBinder.Eval(Container.DataItem, "Page")%>'></a>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <script src="js/responsive-slider.js" type="text/javascript"></script>
        <script type="text/javascript">
            slider = $('[data-spy="responsive-slider"]')
            slider.responsiveSlider({
                autoplay: true,
                interval: 3000
            });
        </script>
    </form>
</body>
</html>
