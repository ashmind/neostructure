using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

namespace AshMind.Web.Mvc.Validation {
    public static class ValidationExtensions {
        public static void ValidateParameter(this Controller controller, string name, bool isValid, string errorMessage) {
            Contract.Requires<ArgumentNullException>(controller != null);
            Contract.Requires<ArgumentException>(controller.ModelState != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            
            if (isValid)
                return;

            controller.ModelState.AddModelError(name, errorMessage);
        }
    }
}
