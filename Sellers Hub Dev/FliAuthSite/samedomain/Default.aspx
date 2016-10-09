<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <section class="about" style="font-size:10pt;">
        <div>Username: <asp:Label ID="Username" runat="server"></asp:Label></div>
        <div>LastName: <asp:Label ID="LastName" runat="server"></asp:Label></div>
        <div>FirstName: <asp:Label ID="FirstName" runat="server"></asp:Label></div>
        <div>Token: <asp:Label ID="lblToken" runat="server"></asp:Label></div>
        

            <div>Expires On: <asp:Label ID="ExpiresOn" runat="server"></asp:Label></div>
            <div><asp:Button ID="PostBack" runat="server" Text="PostBack" /></div>
         

        <div>Url: <asp:Label ID="Url" runat="server"></asp:Label></div>
        <div><asp:HyperLink ID="Logout2" runat="server" Text="Logout w/ Confirm" NavigateUrl="Logout.aspx"></asp:HyperLink></div>
        <div><asp:HyperLink ID="Logout1" runat="server" Text="Logout Instant" NavigateUrl="Logout.aspx?Action=Logout"></asp:HyperLink></div>
        
    </section>
    </form>
</body>
</html>
