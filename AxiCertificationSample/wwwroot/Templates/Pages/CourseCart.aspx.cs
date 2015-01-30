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
using EPiServerCommerceSite.Models.PageTypes;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Website.Helpers;
using CommerceTraining.Templates.Pages;

namespace EPiServerCommerceSite.Templates.Pages
{
    [TemplateDescriptor(Path = "~/Templates/Pages/CourseCart.aspx")]
    public partial class CourseCart : TemplatePage<CartViewType>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CartHelper cartHelper = new CartHelper(Cart.DefaultName);
            cartHelper.Cart.ProviderId = "frontend";
            string returnMessage = LabStoreHelper.RunWorkflowAndReturnFormattedMessage(cartHelper.Cart, "CartValidate");

            if (!string.IsNullOrEmpty(returnMessage))
            {
                litCartMessages.Text = returnMessage;
            }

            cartHelper.Cart.AcceptChanges();

            BindData(cartHelper.Cart);
        }

        private void BindData(Cart cart)
        {
            if (cart.OrderForms[0].LineItems.Count > 0)
            {
                rptLineItems.DataSource = cart.OrderForms[0].LineItems;
                rptLineItems.DataBind();
            }
            else
            {
                litEmptyCart.Visible = true;
            }

            litTotal.Text = cart.Total.ToString("C");
        }	

        protected void rptLineItems_OnDataBound(object sender, RepeaterItemEventArgs e)
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
                    litPrice.Text = li.ListPrice.ToString();
                    litQuantity.Text = li.Quantity.ToString();
                    litExtendedPrice.Text = li.ExtendedPrice.ToString();
                }
            }
        }

        protected void btnGotoCheckout_Click(object sender, EventArgs e)
        {
           Response.Redirect(CourseCheckoutParentPage.GetCheckoutPageUrl(GetPage(ContentReference.StartPage), CourseCheckoutParentPage.StepNameAddress));
        }
    }
}