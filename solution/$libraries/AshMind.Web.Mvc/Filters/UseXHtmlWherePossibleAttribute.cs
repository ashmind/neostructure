using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AshMind.Web.Mvc.Filters {
    public class UseXHtmlWherePossibleAttribute : FilterAttribute, IResultFilter {
        public void OnResultExecuting(ResultExecutingContext filterContext) {
            var response = filterContext.HttpContext.Response;
            if (response.ContentType != "text/html")
                return;

            if (filterContext.HttpContext.Request.Browser.Type.ToUpper().Contains("IE"))
                return;

            response.ContentType = "application/xhtml+xml";
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
        }
    }
}
