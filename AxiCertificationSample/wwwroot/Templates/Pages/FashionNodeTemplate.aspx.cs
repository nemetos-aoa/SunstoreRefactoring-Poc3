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
using EPiServer.Filters;
using EPiServer.Commerce.Catalog.ContentTypes;

namespace EPiServerCommerceSite.Templates.Pages
{
    [TemplateDescriptor(Inherited=true, Path = "~/Templates/Pages/FashionNodeTemplate.aspx")]
    public partial class FashionNodeTemplate : RendererBase<FashionNode>
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            rptCategoryList.DataSource =
                FilterForVisitor.Filter(base.contentLoader.GetChildren<NodeContent>(CurrentContent.ContentLink));

            // Variations
            rptVariants.DataSource =
                FilterForVisitor.Filter(base.contentLoader.GetChildren<VariationContent>(CurrentContent.ContentLink));

            // Products
            rptProducts.DataSource =
                FilterForVisitor.Filter(base.contentLoader.GetChildren<ProductContent>(CurrentContent.ContentLink));

            // specific type... could have several
            rptTypeSpecific.DataSource = FilterForVisitor.Filter(base.contentLoader.GetChildren<FashionVariation>(CurrentContent.ContentLink));

            // Bind for all...
            DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}