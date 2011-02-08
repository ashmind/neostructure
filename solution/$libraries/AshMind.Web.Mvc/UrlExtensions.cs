using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AshMind.Web.Mvc {
    [ContractVerification(false)]
    public static class UrlExtensions {
        public static Uri GetBaseUrl(this UrlHelper url) {
            Contract.Requires<ArgumentNullException>(url != null);
            Contract.Requires<ArgumentException>(url.RequestContext != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext.Request != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext.Request.Url != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext.Request.RawUrl != null);
            Contract.Ensures(Contract.Result<Uri>() != null);

            var contextUri = new Uri(
                url.RequestContext.HttpContext.Request.Url,
                url.RequestContext.HttpContext.Request.RawUrl
            );
            var realmUri = new UriBuilder(contextUri) {
                Path = url.RequestContext.HttpContext.Request.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return realmUri.Uri;
        }

        public static string ActionAbsolute(this UrlHelper url, string actionName, string controllerName, object routeValues) {
            Contract.Requires<ArgumentNullException>(url != null);
            Contract.Requires<ArgumentException>(url.RequestContext != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext.Request != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext.Request.Url != null);
            Contract.Requires<ArgumentException>(url.RequestContext.HttpContext.Request.RawUrl != null);

            var relativeUrl = url.Action(actionName, controllerName, routeValues);
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            return new Uri(GetBaseUrl(url), relativeUrl).AbsoluteUri;
        }
    }
}
