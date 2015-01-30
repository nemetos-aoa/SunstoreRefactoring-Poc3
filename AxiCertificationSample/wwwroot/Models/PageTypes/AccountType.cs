using EPiServer.Core;
using EPiServer.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServerCommerceSite.Models.PageTypes
{
    [ContentType(DisplayName = "AccountType", GUID = "29921536-7522-41ab-bfc2-709568c5da41", Description = "")]
    public class AccountType : PageData
    {
        /*
                 [CultureSpecific]
                 [Editable(true)]
                 [Display(
                     Name = "Main body",
                     Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                     GroupName = SystemTabNames.Content,
                     Order = 1)]
                 public virtual XhtmlString MainBody { get; set; }
          */
    }
}