using EPiServer;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServerCommerceSite.BaseClasses
{
    public class RendererBase<TContent> : ContentPageBase<TContent> where TContent : CatalogContentBase
    {

        private static Injected<IContentLoader> _contentLoader;
        private static Injected<UrlResolver> _urlResolver;
        private static Injected<IPermanentLinkMapper> _linkMapper;
        private static Injected<ILinksRepository> _linksRepository;

        protected IContentLoader contentLoader
        {
            get { return _contentLoader.Service; }
        }
        protected UrlResolver urlResolver
        {
            get { return _urlResolver.Service; }
        }
        protected IPermanentLinkMapper linkMapper
        {
            get { return _linkMapper.Service; }
        }

        protected ILinksRepository linksRepository
        {
            get { return _linksRepository.Service; }
        }


        protected string GetMediaUrl(CatalogContentBase content) // ok for making lab of it
        {

            IList<CommerceMedia> mediaList = new List<CommerceMedia>();

            // check the model type and fill up with te right stuff
            if (content.ContentType == CatalogContentType.CatalogNode)
            {
                NodeContent node = content as NodeContent;
                mediaList = node.CommerceMediaCollection;
            }

            if (content.ContentType == CatalogContentType.CatalogEntry)
            {
                EntryContentBase entry = content as EntryContentBase;
                mediaList = entry.CommerceMediaCollection; // 
            }

            // just get the first
            CommerceMedia media = mediaList.FirstOrDefault(); // ...do it easy in this lab

            if (media == null)
            {
                return String.Empty;
            }

            // load the image url, if it exist... and return it
            var contentLinkMap = linkMapper.Find(new Guid(media.AssetKey));
            if (contentLinkMap == null)
            {
                return String.Empty;
            }

            return contentLinkMap.MappedUrl.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            bool hasCatalogEditingAccess =
               PrincipalInfo.Current.Principal.IsInRole(CatalogSecurityDescriptor.CommerceAdminsRoleName);
            if (!hasCatalogEditingAccess && new FilterContentForVisitor().ShouldFilter(CurrentContent))
            {
                AccessDenied();
            }
        }

        protected string GetUrl(IContent contentLink)
        {
            return urlResolver.GetUrl(contentLink.ContentLink);
        }
    }
}