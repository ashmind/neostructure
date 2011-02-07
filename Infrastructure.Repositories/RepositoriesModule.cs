using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Autofac;

using X.Infrastructure.Interfaces;
using X.Infrastructure.Repositories.Internal;

namespace X.Infrastructure.Repositories {
    public class RepositoriesModule : Autofac.Module {
        protected override void Load(ContainerBuilder builder) {
            var sessionFactory = Database.Configure().BuildSessionFactory();

            builder.RegisterInstance(sessionFactory).SingleInstance();
            builder.RegisterType<SessionContext>()
                   .As<ISessionContext>()
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<KeyProvider>()
                   .As<IKeyProvider>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<TransactionSupport>()
                   .As<ITransactionSupport>()
                   .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
