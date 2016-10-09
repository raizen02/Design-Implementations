<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <section class="container">
    <div class="login">
      <h1>Login to Web App</h1>
      <form id="form1" runat="server">
        <p><asp:TextBox ID="Username" runat="server" placeholder="Username or Email"/></p>
        <p><asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="Password"/></p>
        <p class="remember_me">
          <label>
            <input type="checkbox" name="remember_me" id="remember_me" />
            Remember me on this computer
          </label>
        </p>
       <asp:Button id="btnLogin" runat="server"  Text="Login" 
            onclick="btnLogin_Click" />
      </form>
    </div>

    <div class="login-help">
      <p>Forgot your password?</p>
    </div>
    <section class="about">
        <asp:Label id="Msg" runat="server"></asp:Label>
    </section>
  </section>  
</body>
</html>
