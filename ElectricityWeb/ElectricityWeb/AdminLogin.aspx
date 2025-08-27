<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Admin Login</h2>
  <div class="input-row">
    <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
    <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
  </div>
  <div class="input-row">
    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
  </div>
  <div class="actions">
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
  </div>
  <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
</asp:Content>
