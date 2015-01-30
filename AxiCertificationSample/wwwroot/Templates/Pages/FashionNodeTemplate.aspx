<%@ Page Language="c#" Inherits="EPiServerCommerceSite.Templates.Pages.FashionNodeTemplate" CodeBehind="FashionNodeTemplate.aspx.cs" MasterPageFile="~/Templates/MasterPages/RootMaster.Master" %>


<%@ Import Namespace="EPiServer.Commerce.Catalog.ContentTypes" %>
<%@ Import Namespace="EPiServer.Core.Html" %>
<%@ Import Namespace="EPiServer.Core" %>
<%@ Import Namespace="EPiServerCommerceSite.Models.Catalog" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
  
    <%--Node/Category repeater--%>
    <div class="span12">
        <asp:Repeater EnableViewState="False" runat="server" ID="rptCategoryList">
            <HeaderTemplate>
                <h4>Child Nodes</h4>
            </HeaderTemplate>
            <ItemTemplate>
                <%--...write your code here--%>
                <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                    <img src="<%# GetMediaUrl((NodeContent)Container.DataItem) %>" alt="dx Asset" />
                </a>
                <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                    <%# WebStringHelper.EncodeForWebString(((IContent)Container.DataItem).Name) %>
                </a>
            </ItemTemplate>
        </asp:Repeater>

    </div>
    <%--...just a line break for the lab execise--%>
    <div class="span12">
        <hr />
    </div>


    <%--Variation repeater--%>
    <div class="span12">
        <asp:Repeater EnableViewState="False" runat="server" ID="rptVariants">
            <HeaderTemplate>
                <h4>Variants</h4>
            </HeaderTemplate>
            <ItemTemplate>

                <div class="thumbnail">
                    <%--...write your code here--%>
                    <div>
                        <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                            <%# WebStringHelper.EncodeForWebString(((IContent)Container.DataItem).Name) %>
                        </a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

    <%--Products repeater--%>
    <div class="span12">
        <asp:Repeater EnableViewState="False" runat="server" ID="rptProducts">
            <HeaderTemplate>
                <h4>Products</h4>
            </HeaderTemplate>
            <ItemTemplate>
                <%--...write your code here--%>
                <a href='<%# GetUrl((ProductContent)Container.DataItem) %>'>
                    <img src="<%# GetMediaUrl((ProductContent)Container.DataItem) %>" alt="dx Asset" />
                </a>
                <div class="span3">

                    <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                        <%# WebStringHelper.EncodeForWebString(((IContent)Container.DataItem).Name) %>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>


    <%-- Specific type repeater--%>
    <div class="span12">
        <asp:Repeater EnableViewState="False" runat="server" ID="rptTypeSpecific">
            <HeaderTemplate>
                <h4>Type Specific</h4>
            </HeaderTemplate>
            <ItemTemplate>
                <%--...write your code here--%>
                <div class="thumbnail">
                    <div class="caption">
                        <a href='<%# GetUrl((IContent)Container.DataItem) %>'>
                            <%# WebStringHelper.EncodeForWebString(((IContent)Container.DataItem).Name) %>
                        </a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
