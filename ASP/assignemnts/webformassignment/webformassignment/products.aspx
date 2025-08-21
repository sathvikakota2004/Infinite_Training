<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="webformassignment.products" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Products Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Select a Product</h2>

            <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />

            <asp:Image ID="imgProduct" runat="server" Width="200" Height="200" />
            <br /><br />

            <asp:Button ID="btnGetPrice" runat="server" Text="Get Price"
                OnClick="btnGetPrice_Click" />
            <br /><br />

            <asp:Label ID="lblPrice" runat="server" ForeColor="Blue"></asp:Label>

            <hr />
            <asp:HyperLink ID="hlValidator" runat="server" NavigateUrl="~/Validator.aspx">
                Go to Validator Page
            </asp:HyperLink>
        </div>
    </form>
</body>
</html>
