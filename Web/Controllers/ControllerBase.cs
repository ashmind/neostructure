using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;

using AshMind.Web.Mvc.KeyModel;

namespace X.Web.Controllers {
    [HandleError]
    [UseKeyProvider]
    public abstract class ControllerBase : Controller {
        [ContractInvariantMethod]
        private void CheckInvariants() {
            Contract.Invariant(this.ModelState != null);
        }
    }
}
