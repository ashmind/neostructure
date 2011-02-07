using System.Web.Routing;

// http://goneale.wordpress.com/2008/12/19/lowercase-route-urls-in-aspnet-mvc/

namespace AshMind.Web.Mvc.Routing { 
    public class LowerCaseRoute : Route {
        public LowerCaseRoute(string url, IRouteHandler routeHandler)  : base(url, routeHandler) { }  
        public LowerCaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)  
            : base(url, defaults, routeHandler) { }  
        public LowerCaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)  
            : base(url, defaults, constraints, routeHandler) { }
        public LowerCaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)  
            : base(url, defaults, constraints, dataTokens, routeHandler) { }  

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {  
            var path = base.GetVirtualPath(requestContext, values);
            if (path != null) {
                var parts = path.VirtualPath.Split('?');
                path.VirtualPath = parts[0].ToLowerInvariant();
                if (parts.Length > 1)
                    path.VirtualPath += "?" + parts[1];
            }

            return path;  
        }  
    }
}