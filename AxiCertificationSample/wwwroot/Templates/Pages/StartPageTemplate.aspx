<%@ Page Language="c#" Inherits="EPiServerCommerceSite.Templates.Pages.StartPageTemplate" CodeBehind="StartPageTemplate.aspx.cs" MasterPageFile="~/Templates/MasterPages/RootMaster.Master"  %>
<%@ Import Namespace="Mediachase.Commerce.Core" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content" runat="server">
    <div class="row C_Page-header">

    </div>
    <div>
        <div class="span12">
            <h2>Commerce Fundamentals Course Lab Site</h2>
            <hr />

            <asp:Literal ID="fashionUrl" runat="server"></asp:Literal>
            <asp:Literal ID="carsUrl" runat="server"></asp:Literal>
           <asp:Literal ID="autoUrl" runat="server"></asp:Literal>

            <a href="<%= Url(CurrentPage.Settings.FashionCategory,SiteContext.Current.LanguageName) %>">Fashion</a> 
            <a href="<%= Url(CurrentPage.Settings.CarsCategory,SiteContext.Current.LanguageName) %>">Cars</a>
               <a href="<%= Url(CurrentPage.Settings.AutoCategory,SiteContext.Current.LanguageName) %>">Auto</a>
            
           
            <%--Put the listing "new style" here--%>
            <div>
                <%--Catalog entry point is a node/category--%>
                <EPiServer:Property runat="server" ID="Body" PropertyName="HomeMarkup" />
                <a href="<%= Url(CurrentPage.CatalogEntryPoint) %>">Shortcut to our clothing store filled up with nice clothes</a>
            </div>
            <p>
                This set of labs will help you better understand the platform APIs and quickly get your project, demo or proof of concept off the ground and then easily continue development in order to get the project to production ready standard.
            </p>
            <div class="row">
                <div class="span6">
                    <ul>
                        <li>
                            <h4>Responsive Design / Multi-channel </h4>
                        </li>
                        <li>
                            <h4>Multiple Shopping Experiences </h4>
                        </li>
                        <li>
                            <h4>Search, Facets, and Filtering </h4>
                        </li>
                    </ul>
                </div>
                <div class="span6">
                    <ul>
                        <li>
                            <h4>Content Management &amp; Re-Use </h4>
                        </li>
                        <li>
                            <h4>Cart, Promotions, and Discounts </h4>
                        </li>
                        <li>
                            <h4>Customer Self Service </h4>
                        </li>
                    </ul>
                </div>
                <div class="span12">
                    <hr />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
