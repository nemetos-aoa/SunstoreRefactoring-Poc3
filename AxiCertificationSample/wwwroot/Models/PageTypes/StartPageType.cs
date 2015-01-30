using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
// added for course
using EPiServer.Commerce;



namespace EPiServerCommerceSite.Models.PageTypes
{
    [ContentType(DisplayName = "StartPageType", GUID = "28dca13c-d6d0-4cd8-b807-e60a58637ebb", Description = "This is the startPage")]
    public class StartPageType : PageData
    {
        [Searchable(false)]
        [Display(
            Name = "HomeMarkup",
            Description = "HomeMarkup",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString HomeMarkup { get; set; }

        [Searchable(false)]
        [Display(
            Name = "BodyMarkup",
            Description = "BodyMarkup",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual XhtmlString BodyMarkup { get; set; }

        [UIHint(UIHint.CatalogContent)]
        [Display(
            Name = "CatalogEntryPoint",
            Description = "Where shopping starts",
            GroupName = SystemTabNames.Settings,
            Order = 10)]
        public virtual ContentReference CatalogEntryPoint { get; set; }

        [Display(
           Name = "SiteSettings",
           Description = "Global settings for this site.",
           GroupName = SystemTabNames.Settings,
           Order = 0)]
        public virtual SettingsBlock Settings { get; set; }
        
    }
}