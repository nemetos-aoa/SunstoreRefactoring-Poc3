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
    [ContentType(DisplayName = "CatalogStartType", GUID = "b33a9802-c667-484e-ad97-c96eb582551d", Description = "CatalogStartType")]
    public class CatalogStartType : PageData
    {

        [Searchable(false)]
        [Display(
            Name = "CatalogHomeMarkup",
            Description = "CatalogHomeMarkup",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString CatalogHomeMarkup { get; set; }

    }
}