<%@ Page Language="c#" Inherits="EPiServerCommerceSite.Templates.Pages.CourseCart" CodeBehind="CourseCart.aspx.cs" MasterPageFile="~/Templates/MasterPages/RootMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="1">
        <asp:Literal id="litCartMessages" runat="server"></asp:Literal>
        <asp:Repeater ID="rptLineItems" runat="server" EnableViewState ="true" OnItemDataBound="rptLineItems_OnDataBound">
            <HeaderTemplate>
                <tr>
                    <td>Product Name</td>
                    <td>Price</td>
                    <td>Quantity</td>
                    <td>Total</td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ProductName" runat="server"></asp:Literal> </td>
                    <td><asp:Literal ID="Price" runat="server"></asp:Literal> </td>
                    <td><asp:Literal ID="Quantity" runat="server"></asp:Literal> </td>
                    <td><asp:Literal ID="ExtendedPrice" runat="server"></asp:Literal> </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <asp:Literal ID="litEmptyCart" runat="server" Visible="false" Text="No Items In Your Cart"></asp:Literal>
    <BR />
    <asp:Literal ID="litTotalLabel" runat="server" Text="Total : "></asp:Literal>
    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
    <br />
    <asp:Button ID="btnGotoCheckout" runat="server" Text="Checkout" OnClick="btnGotoCheckout_Click" />
</asp:Content>