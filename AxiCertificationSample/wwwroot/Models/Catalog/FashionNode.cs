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
    [CatalogContentType(GUID = "7427EF9D-6C78-4DD1-8E7B-7C7855360A5F", MetaClassName = "Fashion_Node")]
    public class FashionNode : NodeContent
    {
        [CultureSpecific]
        [Searchable]
        [Tokenize]
        public virtual XhtmlString MainBody { get; set; }

    }
}