using CommerceTraining.Templates.Pages;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Website;
using Mediachase.Commerce.Website.Controls;
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
    public partial class SetShippingMethodPage : UserControlBase
    {
        public CartHelper CartHelper { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetShippingMethods();
        }

        private IEnumerable<ShippingMethodAndRate> GetShippingMethodsWithRates(OrderAddress orderAddress)
        {
            ShippingMethodDto shippingMethods = ShippingManager.GetShippingMethods(CurrentPage.LanguageID, false);
            List<ShippingMethodAndRate> shippingMethodsList = new List<ShippingMethodAndRate>();
            string outputMessage = string.Empty;
            string shippingMethodAndPrice = string.Empty;

            //get the one shipment in the order
            Shipment ship = null;
            if (CartHelper.Cart.OrderForms[0].Shipments != null && CartHelper.Cart.OrderForms[0].Shipments.Count > 0)
            {
                ship = CartHelper.Cart.OrderForms[0].Shipments[0];
            }

            if (ship != null)
            {
                // request rates, make sure we request rates not bound to selected delivery method
                foreach (ShippingMethodDto.ShippingMethodRow row in shippingMethods.ShippingMethod)
                {
                    shippingMethodsList.Add(GetShippingRateInfo(row, ship));
                }
            }

            return shippingMethodsList;

        }

        private void SetShippingMethods()
        {
            if (rptShippingMethods.Items.Count == 0)
            {
                OrderAddress address = null;
                if (CartHelper.Cart.OrderAddresses != null && CartHelper.Cart.OrderAddresses.Count > 0)
                {
                    address = CartHelper.Cart.OrderAddresses[0];
                }

                if (address != null)
                {
                    IEnumerable<ShippingMethodAndRate> methodsAndRates = GetShippingMethodsWithRates(address);
                    rptShippingMethods.DataSource = methodsAndRates.OrderBy(x => x.ShippingRate);
                    rptShippingMethods.DataBind();
                }
            }
        }

        private void SaveSetShippingMethod()
        {
            if (CartHelper.Cart.OrderForms[0].Shipments != null && CartHelper.Cart.OrderForms[0].Shipments.Count > 0)
            {
                Shipment shipment = CartHelper.Cart.OrderForms[0].Shipments[0];
                ShippingMethodAndRate selectedShippingMethod = GetSelectedShippingMethod();

                if (selectedShippingMethod != null)
                {
                    shipment.ShippingMethodId = selectedShippingMethod.ShippingMethodId;
                    shipment.ShippingMethodName = selectedShippingMethod.ShippingMethodName;
                    CartHelper.Cart.AcceptChanges();
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            SaveSetShippingMethod();
            Response.Redirect(CourseCheckoutParentPage.GetCheckoutPageUrl(GetPage(ContentReference.StartPage), CourseCheckoutParentPage.StepNamePaymentMethod));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            SaveSetShippingMethod();
            Response.Redirect(CourseCheckoutParentPage.GetCheckoutPageUrl(GetPage(ContentReference.StartPage), CourseCheckoutParentPage.StepNameAddress));
        }

        protected void ShippingMethods_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Literal displayText = e.Item.FindControl("litShippingNameAndPrice") as Literal;
            HiddenField hiddenMethodId = e.Item.FindControl("hiddenShippingMethodId") as HiddenField;
            HiddenField hiddenMethodName = e.Item.FindControl("hiddenShippingMethodName") as HiddenField;
            HiddenField hiddenRate = e.Item.FindControl("hiddenRate") as HiddenField;
            ShippingMethodAndRate dataItem = e.Item.DataItem as ShippingMethodAndRate;

            if (displayText != null && hiddenMethodId != null && hiddenMethodName != null && dataItem != null)
            {
                displayText.Text = dataItem.ShippingMethodNameAndRate;
                hiddenMethodId.Value = dataItem.ShippingMethodId.ToString();
                hiddenMethodName.Value = dataItem.ShippingMethodName;
                hiddenRate.Value = dataItem.ShippingRate.ToString();
            }
        }

        private ShippingMethodAndRate GetSelectedShippingMethod()
        {
            ShippingMethodAndRate selectedShippingMethod = null;

            foreach (RepeaterItem shippingMethod in rptShippingMethods.Items)
            {
                GlobalRadioButton rdo = shippingMethod.FindControl("rdoChooseShipping") as GlobalRadioButton;
                if (rdo != null && rdo.Checked)
                {
                    //this is the selected method. Get the related shipping method id and return
                    HiddenField hiddenShippingMethodId = shippingMethod.FindControl("hiddenShippingMethodId") as HiddenField;
                    HiddenField hiddenShippingMethodName = shippingMethod.FindControl("hiddenShippingMethodName") as HiddenField;
                    HiddenField hiddenRate = shippingMethod.FindControl("hiddenRate") as HiddenField;
                    if (hiddenShippingMethodId != null && hiddenShippingMethodName != null)
                    {
                        Guid methodId = new Guid(hiddenShippingMethodId.Value);
                        string methodName = hiddenShippingMethodName.Value;
                        decimal rate = 0m;

                        if (decimal.TryParse(hiddenRate.Value, out rate))
                        {
                            selectedShippingMethod = new ShippingMethodAndRate(methodName, string.Empty, rate, methodId);
                        }
                        break;
                    }
                }
            }

            return selectedShippingMethod;
        }

        private ShippingMethodAndRate GetShippingRateInfo(ShippingMethodDto.ShippingMethodRow row, Shipment shipment)
        {
            ShippingMethodAndRate returnRate = null;
            string nameAndRate = string.Empty;

            // Check if package contains shippable items, if it does not use the default shipping method instead of the one specified
            Type type = Type.GetType(row.ShippingOptionRow.ClassName);
            if (type == null)
            {
                throw new TypeInitializationException(row.ShippingOptionRow.ClassName, null);
            }

            string outputMessage = string.Empty;
            IShippingGateway provider = (IShippingGateway)Activator.CreateInstance(type);

            if (shipment != null)
            {
                ShippingRate rate = provider.GetRate(row.ShippingMethodId, shipment, ref outputMessage);
                nameAndRate = string.Format("{0} : {1}", row.Name, rate.Money.Amount.ToString("C"));
                returnRate = new ShippingMethodAndRate(row.Name, nameAndRate, rate.Money.Amount, row.ShippingMethodId);
            }

            return returnRate;
        }
    }

    public class ShippingMethodAndRate
    {
        public string ShippingMethodNameAndRate;
        public string ShippingMethodName;
        public Guid ShippingMethodId;
        public decimal ShippingRate;

        public ShippingMethodAndRate(string name, string nameAndPrice, decimal shippingRate, Guid id)
        {
            ShippingMethodName = name;
            ShippingMethodNameAndRate = nameAndPrice;
            ShippingMethodId = id;
            ShippingRate = shippingRate;
        }
    }
}