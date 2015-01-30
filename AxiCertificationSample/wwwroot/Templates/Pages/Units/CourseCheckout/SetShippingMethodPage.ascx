<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetShippingMethodPage.ascx.cs" Inherits="EPiServer.Commerce.Sample.Templates.Sample.Units.CourseCheckout.SetShippingMethodPage" %>
Shipping Method
<br /><br />
<asp:Repeater ID="rptShippingMethods" runat="server" OnItemDataBound="ShippingMethods_ItemDataBound">
    <ItemTemplate>
		<med:GlobalRadioButton GroupName="ChooseShipping" runat="server" ID="rdoChooseShipping" />
        <asp:Literal ID="litShippingNameAndPrice" runat="server"></asp:Literal>
        <asp:HiddenField id="hiddenShippingMethodId" runat="server" /><br />
        <asp:HiddenField id="hiddenShippingMethodName" runat="server" /><br />
        <asp:HiddenField id="hiddenRate" runat="server" /><br />
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Button ID="btnPrevious" runat="server" Text="Back To Address" OnClick="btnPrevious_Click" />
<asp:Button ID="btnNext" runat="server" Text="Goto Payment" OnClick="btnNext_Click" />
