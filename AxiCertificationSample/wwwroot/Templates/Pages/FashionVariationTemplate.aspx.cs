using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using EPiServerCommerceSite.BaseClasses;
using EPiServerCommerceSite.Models.Catalog;
using Mediachase.Commerce.Catalog.Objects;
using Mediachase.Commerce.Catalog.Managers;
using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce.Website.Helpers;
using Mediachase.Commerce.Orders;
using EPiServerCommerceSite.Models.PageTypes;
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Inventory;

namespace EPiServerCommerceSite.Templates.Pages
{
    [TemplateDescriptor(Path = "~/Templates/Pages/FashionVariationTemplate.aspx")]
    public partial class FashionVariationTemplate : RendererBase<FashionVariation>
    {
        protected Injected<IContentLoader> currentContentLoader;
        public Injected<ICatalogSystem> _catalogSystem;
        public Injected<IWarehouseInventoryService> _warehouseInventoryService;

        public Entry entry { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var ecfEntry = CurrentContent.LoadEntry(
                CatalogEntryResponseGroup.ResponseGroup.CatalogEntryFull);

            this.entry = ecfEntry;

        }

        private void GetImages()
        {
            var media = CurrentContent.CommerceMediaCollection; // 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

      
        protected void AddToCart_Click(object sender, EventArgs e)
        {
            CartHelper ch = new CartHelper(Cart.DefaultName);
            int quantity = int.Parse(txtQuantity.Text);
            ch.AddEntry(entry, quantity, false, new CartHelper[] { });

            StartPageType home = (StartPageType)GetPage(ContentReference.StartPage);
            var cartPage = home.Settings.CartPage;
            
            string cartPageUrl = UrlResolver.Current.GetUrl(cartPage);
            Response.Redirect(cartPageUrl);
        }

    }
}