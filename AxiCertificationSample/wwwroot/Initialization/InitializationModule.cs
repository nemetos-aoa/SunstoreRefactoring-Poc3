using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EPiServerCommerceSite.Initialization
{
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class InitializationModule : IConfigurableModule
    {
        public void Initialize(EPiServer.Framework.Initialization.InitializationEngine context)
        {
            CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, false);


        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(EPiServer.Framework.Initialization.InitializationEngine context)
        {
        }

        // Example of customization
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // pointing to "my own" ... after the provided initialization is done... like overriding
            // context.Container.Configure(ce => ce.For<ICurrentMarket>().Use<CurrentMarketImpl>());

        }

        public static void DumpRoutes(string stage)
        {
            Debug.WriteLine(stage);
            int i = 0;
            foreach (var r in RouteTable.Routes)
            {
                Debug.WriteLine(i.ToString() + ": " + r.ToString());
                i++;
            }
            Debug.WriteLine("---- End of route dump ----");
        }

    }
}