using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Autofac;

namespace X.Domain.Services {
    public class DomainServicesModule : Autofac.Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
