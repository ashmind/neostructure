using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AshMind.Web.Mvc {
    public static class HtmlExtensions {
        public static T FormValue<T>(this HtmlHelper html, string name) {
            Contract.Requires<ArgumentNullException>(html != null);
            Contract.Requires<ArgumentException>(html.ViewData != null);
            Contract.Requires<ArgumentException>(html.ViewData.ModelState != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));

            ModelState state;
            if (html.ViewData.ModelState.TryGetValue(name, out state)) {
                return (T)state.Value.ConvertTo(typeof(T), null);
            }
            return default(T);
        }
    }
}
