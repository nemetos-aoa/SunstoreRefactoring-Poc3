using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServerCommerceSite.Models.Media
{
    [ContentType(DisplayName = "ImageType"
        , GUID = "9bfe4e73-452d-4941-a875-c61384d00b84"
        , Description = "")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageType : ImageData
    {
        public virtual string Copyright { get; set; }
    }
}