using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServerCommerceSite.Models.Catalog
{
    [CatalogContentType(
       GUID = "994DB22C-487A-4CAF-8B93-8EF085B8DD83"
       , MetaClassName = "Fashion_Variation")]
    public class FashionVariation : VariationContent
    {
        [CultureSpecific]
        [Searchable]
        [Tokenize]
        public virtual XhtmlString MainBody { get; set; }

        [Searchable]
        public virtual string Size { get; set; }


        [Searchable]
        public virtual string Color { get; set; }

        [Searchable]
        public virtual bool CanBeMonogrammed { get; set; }
    }
}