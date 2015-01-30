using CommerceTraining.Templates.Pages;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServerCommerceSite.Models.PageTypes;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPiServer.Commerce.Sample.Templates.Sample.Units.CourseCheckout
{
    public partial class OrderAddressPage : UserControlBase
    {
        public CartHelper CartHelper { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CountryDto countriesDto = CountryManager.GetCountries(false);
            if (countriesDto.Country != null && countriesDto.Country.Count > 0)
            {
                foreach (var country in countriesDto.Country)
                {
                    ddlCountries.Items.Add(new ListItem(country.Name, country.Code));
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            OrderAddress add = SaveAddress();
            AddShipment(add);

            Response.Redirect(CourseCheckoutParentPage.GetCheckoutPageUrl(GetPage(ContentReference.StartPage), CourseCheckoutParentPage.StepNameShippingMethod));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            OrderAddress add = SaveAddress();
            AddShipment(add);

            StartPageType home = (StartPageType)GetPage(ContentReference.StartPage);
            var cartPage = home.Settings.CartPage;
            string cartPageUrl = RouteTable.Routes.GetVirtualPath(cartPage, ContentLanguage.PreferredCulture.Name).GetUrl();
            Response.Redirect(cartPageUrl);
        }

        private OrderAddress SaveAddress()
        {
            foreach (OrderAddress oldaddress in CartHelper.Cart.OrderAddresses)
            {
                oldaddress.Delete();
            }

            OrderAddress address = CartHelper.Cart.OrderAddresses.AddNew();
            address.Name = "ShippingAddress";
            address.Line1 = txtAddress1.Text;
            address.Line2 = txtAddress2.Text;
            address.City = txtCity.Text;
            address.State = txtState.Text;
            address.CountryCode = ddlCountries.SelectedItem.Value;
            address.CountryName = ddlCountries.SelectedItem.Text;
            CartHelper.Cart.OrderAddresses.Add(address);

            return address;
        }

        private void AddShipment(OrderAddress address)
        {
            foreach (Shipment ship in CartHelper.Cart.OrderForms[0].Shipments)
            {
                ship.Delete();
            }

            Shipment shipment = CartHelper.Cart.OrderForms[0].Shipments.AddNew();
            shipment.ShippingAddressId = address.Name;

            for (int i = 0; i < CartHelper.Cart.OrderForms[0].LineItems.Count; i++)
            {
                LineItem item = CartHelper.Cart.OrderForms[0].LineItems[i];
                shipment.AddLineItemIndex(i, item.Quantity);
            }

            CartHelper.Cart.AcceptChanges();
        }
    }
}