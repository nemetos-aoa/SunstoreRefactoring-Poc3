<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderAddressPage.ascx.cs" Inherits="EPiServer.Commerce.Sample.Templates.Sample.Units.CourseCheckout.OrderAddressPage" %>
Shipping/Billing Address
<table>
    <tr>
        <td>Address Line 1:</td> <td><asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Address Line 2:</td><td> <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>City:</td><td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>State/Province/Region:</td> <td><asp:TextBox ID="txtState" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Country:</td> <td><asp:DropDownList ID="ddlCountries" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Postal Code:</td> <td><asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox></td>
    </tr>
</table>
<br />
<asp:Button ID="btnPrevious" runat="server" Text="Back To Cart" OnClick="btnPrevious_Click" />
<asp:Button ID="btnNext" runat="server" Text="Select Shipping Method" OnClick="btnNext_Click" />
