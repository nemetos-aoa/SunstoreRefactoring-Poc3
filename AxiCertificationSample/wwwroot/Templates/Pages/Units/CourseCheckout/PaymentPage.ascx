<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.ascx.cs" Inherits="EPiServer.Commerce.Sample.Templates.Sample.Units.CourseCheckout.PaymentPage" %>
Payment and Place Order<br />
    <table border="1">
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
    Shipping Total: <asp:Literal ID="litShippingTotal" runat="server"></asp:Literal><br />
    Total: <asp:Literal ID="litTotal" runat="server"></asp:Literal>


<table>
    <tr>
        <td>Card Number: </td><td><asp:Textbox ID="txtCreditCardNumber" runat="server" placeholder="Credit Card Number"></asp:Textbox></td>
    </tr>
    <tr>
        <td>Security Code: </td><td><asp:TextBox ID="txtSecurityCode" runat="server" placeholder="SecurityCode"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Expiration Date (Month/Year)</td>
        <td>
            <asp:DropDownList ID = "ddlMonth" runat="server">
                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                <asp:ListItem Text="December" Value="12"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID = "ddlYear" runat="server">
                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<asp:Button ID="btnPrevious" runat="server" Text="Back To Shipping Method" OnClick="btnPrevious_Click" />
<asp:Button ID="btnNext" runat="server" Text="Place Order" OnClick="btnNext_Click" />
