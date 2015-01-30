using CommerceTraining.Templates.Pages;
using Mediachase.Commerce.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPiServer.Commerce.Sample.Templates.Sample.Units.CourseCheckout
{
    public partial class OrderCompletePage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CourseCheckoutParentPage.SessionOrderIdKey] != null)
            {
                litOrderNumber.Text = Session[CourseCheckoutParentPage.SessionOrderIdKey].ToString();
            }
        }
    }
}