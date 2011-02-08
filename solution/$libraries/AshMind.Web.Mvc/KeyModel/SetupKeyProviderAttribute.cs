using System;
using System.Web.Mvc;
using System.Diagnostics.Contracts;

namespace AshMind.Web.Mvc.KeyModel {
    public class UseKeyProviderAttribute : ActionFilterAttribute {
        public static Func<object, object> KeyProvider { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            var view = filterContext.Result as ViewResultBase;
            if (view != null) {
                if (KeyProvider == null)
                    throw new InvalidOperationException("UseKeyProviderAttribute.KeyProvider should be set if [UseKeyProvider] filter used.");

                Contract.Assume(view.ViewData != null);
                view.ViewData.SetKeyProvider(KeyProvider);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}


