<%@ Page Language="c#" Inherits="EPiServerCommerceSite.Templates.Pages.FashionProductTemplate" CodeBehind="FashionProductTemplate.aspx.cs" MasterPageFile="~/Templates/MasterPages/RootMaster.Master" %>

<%@ Import Namespace="EPiServer.Commerce.Catalog.ContentTypes" %>
<%@ Import Namespace="EPiServer.Core.Html" %>
<%@ Import Namespace="EPiServer.Core" %>
<%@ Import Namespace="CommerceTraining.Models.Catalog" %>

<asp:content contentplaceholderid="MainContent" id="content" runat="server">
     <div>
        <asp:Repeater EnableViewState="False" runat="server" ID="rptVariantsOfProducts">
            <HeaderTemplate>
                <h4>Variants of a Product (Name: <%=CurrentContent.Name %>)</h4>
                <div><hr /></div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="span12">
                    <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                        <img src="<%# GetMediaUrl((CatalogContentBase)Container.DataItem) %>" alt="dx Asset" />
                    </a>
                    <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                        <%# WebStringHelper.EncodeForWebString(((VariationContent)Container.DataItem).DisplayName) %>
                    </a>
                    </div>
                <div><hr /></div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:content>