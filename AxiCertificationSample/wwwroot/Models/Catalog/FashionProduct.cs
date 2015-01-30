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
    [CatalogContentType(GUID = "B9F91D4F-757E-4163-A9B5-DB67B11C7306", MetaClassName = "Fashion_Product")]
    public class FashionProduct : ProductContent
    {
        [CultureSpecific]
        [Searchable]
        [Tokenize]
        public virtual XhtmlString MainBody { get; set; }

        [Searchable]
        public virtual string ClothesType { get; set; }

        [Searchable]
        public virtual string Brand { get; set; }
    }
}