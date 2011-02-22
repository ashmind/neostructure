using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AshMind.Web.Mvc.Filters {
    public class PreserveModelStateOnRedirectAttribute : FilterAttribute, IActionFilter {
        private const string TempDataKey = "PreserveModelStateOnRedirect.ModelState";

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            if (!filterContext.Controller.TempData.ContainsKey(TempDataKey))
                return;

            var preservedModelState = (ModelStateDictionary)filterContext.Controller.TempData[TempDataKey];
            var modelState = filterContext.Controller.ViewData.ModelState;
            foreach (var pair in preservedModelState) {
                modelState.Add(pair.Key, pair.Value);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            if (!(filterContext.Result is RedirectResult) && !(filterContext.Result is RedirectToRouteResult))
                return;

            var modelState = filterContext.Controller.ViewData.ModelState;
            if (modelState.IsValid)
                return;

            var tempData = filterContext.Controller.TempData;
            tempData[TempDataKey] = modelState;
        }
    }
}
