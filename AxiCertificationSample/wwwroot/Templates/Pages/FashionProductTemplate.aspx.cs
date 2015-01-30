using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using EPiServerCommerceSite.BaseClasses;
using EPiServerCommerceSite.Models.Catalog;
using System.Collections.Generic;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.ServiceLocation;

namespace EPiServerCommerceSite.Templates.Pages
{
    [TemplateDescriptor(Inherited=true,Path = "~/Templates/Pages/FashionProductTemplate.aspx")]
    public partial class FashionProductTemplate : RendererBase<FashionProduct>
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            IEnumerable<ProductVariation> relationsBySource =
                linksRepository.GetRelationsBySource(CurrentContent.ContentLink).OfType<ProductVariation>();

            ILanguageSelector selector = ServiceLocator.Current.GetInstance<ILanguageSelector>();
            // LanguageSelector.AutoDetect() // is also valid argument for the method below

            IEnumerable<FashionVariation> variations =
                base.contentLoader.GetItems(relationsBySource.Select(r => r.Target), selector)
                .OfType<FashionVariation>();

            rptVariantsOfProducts.DataSource = variations;
            rptVariantsOfProducts.DataBind();

            // can also do like this
            // var relations = CurrentContent.GetVariantRelations().ToList();

        }
    }
}