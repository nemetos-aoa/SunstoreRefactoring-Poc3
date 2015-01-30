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
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation;
using EPiServerCommerceSite.Models.PageTypes;
using Mediachase.Commerce.Core;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServerCommerceSite.Models.Catalog;

namespace EPiServerCommerceSite.Templates.Pages
{
    [TemplateDescriptor(Path = "~/Templates/Pages/StartPageTemplate.aspx")]
    public partial class StartPageTemplate : EPiServer.TemplatePage<StartPageType>
    {
        Injected<IContentLoader> _contentLoader;
        Injected<UrlResolver> _urlResolver;

        protected void Page_Load(object sender, EventArgs e)
        {
            var firstCatalog = _contentLoader.Service.Get<IContent>(CurrentPage.Settings.FashionCategory);
            var secondCatalog = _contentLoader.Service.Get<IContent>(CurrentPage.Settings.CarsCategory);
            var thirdCatalog = _contentLoader.Service.Get<IContent>(CurrentPage.Settings.AutoCategory);

            fashionUrl.Text = GetUrl(firstCatalog);
            autoUrl.Text = GetUrl(secondCatalog);
            carsUrl.Text = GetUrl(thirdCatalog);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

       
        protected string Url(ContentReference contentLink, string language)
        {
            return _urlResolver.Service.GetUrl(contentLink,language);
        }

        protected string Url(ContentReference contentLink)
        {
            return _urlResolver.Service.GetUrl(contentLink);
        }

        protected string GetUrl(IContent contentLink)
        {
            return _urlResolver.Service.GetUrl(contentLink.ContentLink);
        }
    }
}