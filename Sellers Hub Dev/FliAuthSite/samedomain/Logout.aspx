﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="LogoutPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <section class="about" style="font-size: 12pt;">
            <div>Username: <asp:Label ID="Username" runat="server"></asp:Label></div>
            <div>LastName: <asp:Label ID="Password" runat="server"></asp:Label></div>
            <div>Url: <asp:Label ID="Url" runat="server"></asp:Label></div>
        <div><asp:Button ID="Logout" runat="server" Text="Logout" onclick="Logout_Click" /></div>
    </section>
    </div>
    </form>
</body>
</html>
