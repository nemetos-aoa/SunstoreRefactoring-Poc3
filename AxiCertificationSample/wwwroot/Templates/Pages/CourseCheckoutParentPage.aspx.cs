using EPiServer;
using EPiServer.Commerce.Sample.Templates.Sample.Units.CourseCheckout;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using EPiServerCommerceSite.Models.PageTypes;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Security;
using Mediachase.Commerce.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommerceTraining.Templates.Pages
{
    public partial class CourseCheckoutParentPage : TemplatePage<CourseCheckoutType>
    {
        #region Fields
        public const string StepNameAddress = "address";
        public const string StepNameShippingMethod = "shipping";
        public const string StepNamePaymentMethod = "payment";
        public const string StepNameCompleteMethod = "complete";
        public const string SessionOrderIdKey = "OrderNumber";

        private string _step = Mediachase.Commerce.Website.CommonHelper.GetValueFromQueryString("step", StepNameAddress);
        private StringDictionary _controlMap = new StringDictionary();
        #endregion
        #region Properties

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            CartHelper ch = new CartHelper(Cart.DefaultName);
            if ((ch.Cart.OrderForms == null || ch.Cart.OrderForms.Count == 0 || ch.Cart.OrderForms[0].LineItems == null || ch.Cart.OrderForms[0].LineItems.Count == 0) && !_step.Equals(StepNameCompleteMethod))
            {
                string homePageUrl = GetPage(ContentReference.StartPage).LinkURL;
                Response.Redirect(homePageUrl);
            }
            SetControlMap();
            LoadStepControl();
        }

        #region Helper Methods
        /// <summary>
        /// Sets the control map.
        /// </summary>
        private void SetControlMap()
        {
            _controlMap.Add(StepNameAddress, "~/Templates/Pages/Units/CourseCheckout/OrderAddressPage.ascx");
            _controlMap.Add(StepNameShippingMethod, "~/Templates/Pages/Units/CourseCheckout/SetShippingMethodPage.ascx");
            _controlMap.Add(StepNamePaymentMethod, "~/Templates/Pages/Units/CourseCheckout/PaymentPage.ascx");
            _controlMap.Add(StepNameCompleteMethod, "~/Templates/Pages/Units/CourseCheckout/OrderCompletePage.ascx");
        }

        /// <summary>
        /// Loads the step control.
        /// </summary>
        private void LoadStepControl()
        {
            //check whether the step needs to be changed because keepsakes don't need an address
            CartHelper ch = new CartHelper(Cart.DefaultName);

            switch (_step)
            {
                case StepNameAddress:
                    OrderAddressPage address = this.LoadControl(_controlMap[_step]) as OrderAddressPage;
                    if (address != null)
                    {
                        address.CartHelper = ch;
                        controlHolder.Controls.Add(address);
                    }
                    break;
                case StepNameShippingMethod:
                    SetShippingMethodPage shipmentMethod = this.LoadControl(_controlMap[_step]) as SetShippingMethodPage;
                    if (shipmentMethod != null)
                    {
                        shipmentMethod.CartHelper = ch;
                        controlHolder.Controls.Add(shipmentMethod);
                    }
                    break;
                case StepNamePaymentMethod:
                    PaymentPage payment = this.LoadControl(_controlMap[_step]) as PaymentPage;
                    if (payment != null)
                    {
                        payment.CartHelper = ch;
                        controlHolder.Controls.Add(payment);
                    }
                    break;
                case StepNameCompleteMethod:
                    OrderCompletePage complete = this.LoadControl(_controlMap[_step]) as OrderCompletePage;
                    if (complete != null)
                    {
                        controlHolder.Controls.Add(complete);
                    }
                    break;
            }
        }

        public static string GetCheckoutPageUrl(PageData startPage, string stepName)
        {
            StartPageType home = (StartPageType)startPage;
            var checkoutPage = home.Settings.CheckoutPage;
            string checkoutPageUrl = RouteTable.Routes.GetVirtualPath(checkoutPage, ContentLanguage.PreferredCulture.Name).GetUrl();

            UrlBuilder bldr = new UrlBuilder(checkoutPageUrl);
            bldr.QueryCollection.Add("step", stepName);
            return bldr.ToString(); ;
        }
        #endregion
    }
}