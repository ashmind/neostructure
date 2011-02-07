using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace AshMind.Web.Mvc {
    public static class HtmlSecurityExtensions {
        #region ControllerActionFinder Class

        private class ControllerActionFinder : ControllerActionInvoker {
            public ActionDescriptor FindAction(ControllerContext controllerContext, string actionName) {
                return base.FindAction(controllerContext, this.GetControllerDescriptor(controllerContext), actionName);
            }
        }

        #endregion

        public static bool IsActionAuthorized(this HtmlHelper html, string actionName) {
            Contract.Requires<ArgumentNullException>(html != null);
            Contract.Requires<ArgumentException>(html.ViewContext != null);
            Contract.Requires<ArgumentException>(html.ViewContext.Controller != null);
            Contract.Requires<ArgumentException>(html.ViewContext.Controller.ControllerContext != null);

            return IsActionAuthorized(actionName, html.ViewContext.Controller);
        }

        public static bool IsActionAuthorized(this HtmlHelper html, string actionName, string controllerName) {
            Contract.Requires<ArgumentNullException>(html != null);
            Contract.Requires<ArgumentException>(html.ViewContext != null);
            Contract.Requires<ArgumentException>(html.ViewContext.HttpContext != null);

            var requestContext = new RequestContext(html.ViewContext.HttpContext, html.ViewContext.RouteData);
            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, controllerName);
            controller.ControllerContext = new ControllerContext(requestContext, controller);

            return IsActionAuthorized(actionName, controller);
        }

        private static bool IsActionAuthorized(string actionName, ControllerBase controller) {
            Contract.Requires<ArgumentNullException>(controller != null);
            Contract.Requires<ArgumentException>(controller.ControllerContext != null);

            var finder = new ControllerActionFinder();
            var action = finder.FindAction(controller.ControllerContext, actionName);

            // TODO: verify! This line was replaced in order to upgrade to MVC 3
            // var filters = action.GetFilters().AuthorizationFilters;
            var filters = FilterProviders.Providers.GetFilters(controller.ControllerContext, action).OfType<IAuthorizationFilter>();
            
            var authorizationContext = new AuthorizationContext(controller.ControllerContext, action);
            foreach (var authorize in filters) {
                authorize.OnAuthorization(authorizationContext);
                if (authorizationContext.Result is HttpUnauthorizedResult)
                    return false;
            }

            return true;
        }
    }
}
