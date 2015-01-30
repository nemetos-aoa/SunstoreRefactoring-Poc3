using CommerceTraining.Templates.Pages;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using Mediachase.Commerce.Engine;
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
    public partial class PaymentPage : UserControlBase
    {
        public CartHelper CartHelper { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //run the validate workflow to update the totals
            if (CartHelper.LineItems.Count() > 0)
            {
                WorkflowResults results = CartHelper.Cart.RunWorkflow("CartPrepare");
                CartHelper.Cart.AcceptChanges();
            }
            LoadData();
        }

        private void LoadData()
        {
            Cart cart = CartHelper.Cart;

            if (cart != null && cart.OrderForms != null && cart.OrderForms.Count > 0 && cart.OrderForms[0].LineItems != null && cart.OrderForms[0].LineItems.Count > 0)
            {
                rptLineItems.DataSource = cart.OrderForms[0].LineItems;
                rptLineItems.DataBind();

                litTotal.Text = cart.Total.ToString("C");
                litShippingTotal.Text = cart.ShippingTotal.ToString("C");
            }
            else
            {
                litEmptyCart.Visible = true;
            }
        }

        private void Checkout()
        {
            CreditCardPayment newPayment = null;
            OrderAddress address = null;

            //first delete existing payment
            if (CartHelper.Cart.OrderForms[0].Payments != null && CartHelper.Cart.OrderForms[0].Payments.Count > 0)
            {
                foreach (Payment pay in CartHelper.Cart.OrderForms[0].Payments)
                {
                    pay.Delete();
                }
            }

            //Get a reference to the address in the cart - assumes shipping and billing address same
            if (CartHelper.Cart.OrderAddresses != null && CartHelper.Cart.OrderAddresses.Count > 0)
            {
                address = CartHelper.Cart.OrderAddresses[0];
            }

            //update the cart to get the update to date total, including shipping charges

            PaymentMethodDto method = PaymentManager.GetPaymentMethodBySystemName("CoursePayment", CurrentPage.LanguageID);
            if (method.PaymentMethod != null && method.PaymentMethod.Count == 1)
            {
                newPayment = new CreditCardPayment();
                newPayment.CreditCardNumber = txtCreditCardNumber.Text;
                newPayment.CreditCardSecurityCode = txtSecurityCode.Text;
                newPayment.PaymentMethodId = method.PaymentMethod[0].PaymentMethodId;
                newPayment.PaymentMethodName = method.PaymentMethod[0].Name;
                newPayment.Amount = CartHelper.Cart.Total;

                int datePart;
                if (int.TryParse(ddlYear.SelectedItem.Value, out datePart))
                {
                    newPayment.ExpirationYear = datePart;
                }

                if (int.TryParse(ddlMonth.SelectedItem.Value, out datePart))
                {
                    newPayment.ExpirationMonth = datePart;
                }

                if (address != null)
                {
                    newPayment.BillingAddressId = address.Name;
                }

                CartHelper.Cart.OrderForms[0].Payments.Add(newPayment);

                //process payment
                try
                {
                    CartHelper.Cart.RunWorkflow("CartCheckout");

                    //empty out the payment object of details so that they're not saved in the database
                    newPayment.CreditCardNumber = string.Empty;
                    newPayment.CreditCardSecurityCode = string.Empty;
                    newPayment.ExpirationMonth = 1;
                    newPayment.ExpirationYear = 1;
                    CartHelper.Cart.AcceptChanges();

                    //Convert the cart to a purchaseorder
                    PurchaseOrder po = CartHelper.Cart.SaveAsPurchaseOrder();
                    CartHelper.Cart.Delete();

                    //put the order results in session
                    Session[CourseCheckoutParentPage.SessionOrderIdKey] = po.TrackingNumber;

                }
                catch (Exception ex)
                {
                    //empty out the payment object of details so that they're not saved in the database
                    newPayment.CreditCardNumber = string.Empty;
                    newPayment.CreditCardSecurityCode = string.Empty;
                    newPayment.ExpirationMonth = 1;
                    newPayment.ExpirationYear = 1;
                    newPayment.Status = ex.Message;
                }
                CartHelper.Cart.AcceptChanges();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Checkout();
            Response.Redirect(CourseCheckoutParentPage.GetCheckoutPageUrl(GetPage(ContentReference.StartPage), CourseCheckoutParentPage.StepNameCompleteMethod));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect(CourseCheckoutParentPage.GetCheckoutPageUrl(GetPage(ContentReference.StartPage), CourseCheckoutParentPage.StepNameShippingMethod));
        }

        protected void rptLineItems_OnDataBound(object obj, RepeaterItemEventArgs e)
        {
            Literal litName;
            Literal litPrice;
            Literal litQuantity;
            Literal litExtendedPrice;
            LineItem li;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                litName = e.Item.FindControl("ProductName") as Literal;
                litPrice = e.Item.FindControl("Price") as Literal;
                litQuantity = e.Item.FindControl("Quantity") as Literal;
                litExtendedPrice = e.Item.FindControl("ExtendedPrice") as Literal;

                li = e.Item.DataItem as LineItem;

                if (li != null)
                {
                    litName.Text = li.DisplayName;
                    litPrice.Text = li.PlacedPrice.ToString("C");
                    litQuantity.Text = li.Quantity.ToString("N0");
                    litExtendedPrice.Text = li.ExtendedPrice.ToString("C");
                }
            }
        }
    }
}