<%@ Page Language="c#" Inherits="EPiServerCommerceSite.Templates.Pages.FashionVariationTemplate" CodeBehind="FashionVariationTemplate.aspx.cs" MasterPageFile="~/Templates/MasterPages/RootMaster.Master" %>



<asp:content contentplaceholderid="MainContent" id="content" runat="server">
     <div>
        <%-- AddToCart--%>
        <asp:Button ID="AddToCart" runat="server" OnClick="AddToCart_Click" Text="Add To Cart" />
        <asp:TextBox ID="txtQuantity" runat="server" Text="0"></asp:TextBox>

       <%-- <asp:DropDownList ID="WarehouseDropDown" runat="server"
            OnSelectedIndexChanged="WarehouseDropDown_SelectedIndexChanged">
        </asp:DropDownList>

        <asp:DropDownList ID="MarketDropDown" runat="server"
            OnSelectedIndexChanged="MarketDropDown_SelectedIndexChanged">
        </asp:DropDownList>--%>

    </div>
    <div>
        <h2>
            <EPiServer:Property ID="Property1" PropertyName="MainBody" runat="server" />
        </h2>
        <hr />
    </div>
    <div>

        <div class="span3 C_Product-Images">
            First image 
            <%--Add the image property here--%>
          <EPiServer:Property ID="Property3" runat="server" PropertyName="CommerceMediaCollection" CssClass="thumbnail"></EPiServer:Property>


        </div>
        <div class="span4">
            Product Description:
            <hr />
            <EPiServer:Property runat="server" ID="MainBody" PropertyName="MainBody"></EPiServer:Property>

        </div>
        <div class="span3">
            <%--Add the price here--%>
            Price: 
            <%=Mediachase.Commerce.Website.Helpers.StoreHelper.GetDiscountPrice(entry).Money.Amount.ToString() %>
        </div>

    </div>

    <div class="span12">
        <hr />
    </div>
    <div runat="server" id="OutPutDiv"></div>

</asp:content>