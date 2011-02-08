using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;

using AshMind.Extensions;
using System.Diagnostics.Contracts;

namespace AshMind.Web.Mvc {
    [ContractClassFor(typeof(ConfiguredMvcApplicationBase))]
    internal abstract class ConfiguredMvcApplicationContract : ConfiguredMvcApplicationBase {
        protected override void RegisterRoutes(RouteCollection routes) {
            Contract.Requires<ArgumentNullException>(routes != null);
            throw new NotSupportedException();
        }

        protected override bool ShouldDiscoverModulesIn(string path) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(path));
            throw new NotSupportedException();
        }
    }
}
