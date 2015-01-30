using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPiServerCommerceSite.Models.PageTypes
{
    [ContentType(DisplayName = "CartView"
        , GUID = "6b63fe0d-11d7-498a-ae00-76d92eaececf"
        , Description = "Type for viewing the cart (basket)")]
    public class CartViewType : PageData
    {

        [Searchable(false)]
        [Display(
            Name = "HomeMarkup",
            Description = "HomeMarkup",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString HomeMarkup { get; set; }
    }
}