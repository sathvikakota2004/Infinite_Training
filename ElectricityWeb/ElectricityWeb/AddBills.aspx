<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddBills.aspx.cs" Inherits="AddBills" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Add Electricity Bills</h2>

  <div class="input-row">
    <asp:Label runat="server" Text="Number of Bills to be Added"></asp:Label>
    <asp:TextBox ID="txtCount" runat="server"></asp:TextBox>
    <asp:Button ID="btnSetCount" runat="server" Text="Set" OnClick="btnSetCount_Click" />
  </div>

  <asp:Panel ID="pnlEntry" runat="server" Visible="false">
    <div class="input-row">
      <asp:Label runat="server" Text="Consumer Number (e.g., EB12345)"></asp:Label>
      <asp:TextBox ID="txtConsumerNo" runat="server" MaxLength="20"></asp:TextBox>
    </div>
    <div class="input-row">
      <asp:Label runat="server" Text="Consumer Name"></asp:Label>
      <asp:TextBox ID="txtConsumerName" runat="server" MaxLength="50"></asp:TextBox>
    </div>
    <div class="input-row">
      <asp:Label runat="server" Text="Units Consumed"></asp:Label>
      <asp:TextBox ID="txtUnits" runat="server"></asp:TextBox>
    </div>
    <div class="actions">
      <asp:Button ID="btnAdd" runat="server" Text="Add Bill" OnClick="btnAdd_Click" />
      <asp:Button ID="btnFinish" runat="server" Text="Finish" OnClick="btnFinish_Click" />
    </div>
    <asp:Label ID="lblEntryMsg" runat="server"></asp:Label>
  </asp:Panel>

  <hr />
  <h3>Added (this session)</h3>
  <asp:GridView ID="gvAdded" runat="server" AutoGenerateColumns="false">
    <Columns>
      <asp:BoundField DataField="ConsumerNumber" HeaderText="Consumer No" />
      <asp:BoundField DataField="ConsumerName" HeaderText="Name" />
      <asp:BoundField DataField="UnitsConsumed" HeaderText="Units" />
      <asp:BoundField DataField="BillAmount" HeaderText="Amount" DataFormatString="{0:F2}" />
    </Columns>
  </asp:GridView>

  <asp:Label ID="lblCount" runat="server"></asp:Label>
  <asp:Label ID="lblDone" runat="server" CssClass="success"></asp:Label>
</asp:Content>
