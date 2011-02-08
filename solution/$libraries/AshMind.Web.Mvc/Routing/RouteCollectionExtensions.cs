using System;
using System.Web.Mvc;
using System.Web.Routing;

using System.Diagnostics.Contracts;

namespace AshMind.Web.Mvc.Routing {
    public static class RouteCollectionExtensions {
        public static void MapLowerCaseRoute(this RouteCollection routes, string name, string url) {
            Contract.Requires<ArgumentNullException>(routes != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));

            routes.Add(name, new LowerCaseRoute(url, new MvcRouteHandler()));
        }

        public static void MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults) {
            Contract.Requires<ArgumentNullException>(routes != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));

            routes.MapLowerCaseRoute(name, url, defaults, null);
        }

        public static void MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults, object constraints) {
            Contract.Requires<ArgumentNullException>(routes != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            
            routes.Add(name, new LowerCaseRoute(url, new MvcRouteHandler()) {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            });
        }
    }
}