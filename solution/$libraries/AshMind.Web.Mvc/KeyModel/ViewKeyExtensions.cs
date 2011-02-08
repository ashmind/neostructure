using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AshMind.Web.Mvc.KeyModel {
    public static class ViewKeyExtensions {
        public static object GetKey(this ViewContext context, object model) {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(model != null);

            return ((Func<object, object>)context.ViewData["ViewContextExtensions.GetKey"])(model);
        }

        public static void SetKeyProvider(this ViewDataDictionary viewData, Func<object, object> keyProvider) {
            Contract.Requires<ArgumentNullException>(viewData != null);
            Contract.Requires<ArgumentNullException>(keyProvider != null);

            viewData["ViewContextExtensions.GetKey"] = keyProvider;
        }
    }
}