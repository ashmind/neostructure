using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Autofac;

using AshMind.Extensions;

using AshMind.Web.Mvc;
using AshMind.Web.Mvc.Routing;

using Neostructure.Infrastructure.Interfaces;
using AshMind.Web.Mvc.KeyModel;

namespace Neostructure.Web {
    public class MvcApplication : ConfiguredMvcApplicationBase {
        protected override void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapLowerCaseRoute(
                "Root",
                "",
                new { controller = "home", action = "index" }
            );

            routes.MapLowerCaseRoute(
                "Keyed",
                "{controller}/{key}/{action}",
                new { action = "view" },
                new { key = ".{10,}" }
            );

            routes.MapLowerCaseRoute(
                "Default",
                "{controller}/{action}"
            );
        }

        protected override void RegisterContainer() {
            base.RegisterContainer();
            RequestScope.Resolve<IApplicationSetup[]>().ForEach(s => s.Setup());
            UseKeyProviderAttribute.KeyProvider = entity => RequestScope.Resolve<IKeyProvider>().GetKey(entity);
        }

        protected override bool ShouldDiscoverModulesIn(string path) {
            return path.Contains("Neostructure.");
        }
    }
}