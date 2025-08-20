<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="webformassignment.ValidatorPage" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Validator Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>User Information</h2>

            Name: <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
            Family Name: <asp:TextBox ID="txtFamilyName" runat="server"></asp:TextBox><br />
            Address: <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br />
            City: <asp:TextBox ID="txtCity" runat="server"></asp:TextBox><br />
            Zip Code: <asp:TextBox ID="txtZip" runat="server"></asp:TextBox><br />
            Phone: <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox><br />
            Email: <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
            <br /><br />
            <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>

            <hr />
            <asp:HyperLink ID="hlProducts" runat="server" NavigateUrl="~/Products.aspx">Go to Products Page</asp:HyperLink>
        </div>
    </form>
</body>
</html>
