<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RetrieveBills.aspx.cs" Inherits="RetrieveBills" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Retrieve Last N Bills</h2>

  <div class="input-row">
    <asp:Label runat="server" Text="Enter Last 'N' Number of Bills To Generate"></asp:Label>
    <asp:TextBox ID="txtN" runat="server"></asp:TextBox>
    <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
  </div>

  <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>

  <h3>Details of last ‘N’ bills:</h3>
  <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="false">
    <Columns>
      <asp:BoundField DataField="ConsumerNumber" HeaderText="Consumer No" />
      <asp:BoundField DataField="ConsumerName" HeaderText="Name" />
      <asp:BoundField DataField="UnitsConsumed" HeaderText="Units" />
      <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" DataFormatString="{0:F0}" />
    </Columns>
  </asp:GridView>

  <h3>Summary</h3>
  <asp:BulletedList ID="bltSummary" runat="server"></asp:BulletedList>
</asp:Content>
