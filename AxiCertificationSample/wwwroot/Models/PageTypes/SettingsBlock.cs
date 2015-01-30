using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Commerce;

namespace EPiServerCommerceSite.Models.PageTypes
{
    [ContentType(
       GUID = "CB8034DF-0DF2-47BA-9043-F19D57361D0B",
       AvailableInEditMode = false,        // Just for settings, not able to add from edit mode
       DisplayName = "Settings Data",
       Description = "Contains global settings data for this site.",
       GroupName = "CourseSite",
       Order = 1)]
    public class SettingsBlock : BlockData
    {
        [Searchable(false)]
        [Display(
            Name = "CartPage",
            Description = "The page that displays the shopping cart.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual PageReference CartPage { get; set; }

        [Searchable(false)]
        [Display(
            Name = "CheckoutPage",
            Description = "The checkout page to complete your order.",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual PageReference CheckoutPage { get; set; }

        [UIHint(UIHint.CatalogContent)]
        [Display(
            Name = "Fashion Category",
            Description = "The checkout page to complete your order.",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual ContentReference FashionCategory { get; set; }

         [UIHint(UIHint.CatalogContent)]
        [Display(
            Name = "Cars Category",
            Description = "The checkout page to complete your order.",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        public virtual ContentReference CarsCategory { get; set; }

      [ UIHint(UIHint.CatalogContent)]
        [Display(
            Name = "Auto Category",
            Description = "The checkout page to complete your order.",
            GroupName = SystemTabNames.Content,
            Order = 60)]
        public virtual ContentReference AutoCategory { get; set; }
    }
}