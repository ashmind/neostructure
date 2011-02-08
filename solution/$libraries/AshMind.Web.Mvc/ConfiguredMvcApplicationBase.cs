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
    [ContractClass(typeof(ConfiguredMvcApplicationContract))]
    public abstract class ConfiguredMvcApplicationBase : HttpApplication {
        //private static IContainerProvider containerProvider;
        private static IContainer container;
        private static AutofacDependencyResolver dependencyResolver;

        private static Exception startFailure;

        public static ILifetimeScope RequestScope {
            get { return dependencyResolver.RequestLifetimeScope; }
        }

        protected static IContainer Container {
            get { return container; }
        }

        protected void Application_Start() {
            Start();
        }

        protected void Start() {
            try {
                this.RegisterFirst();

                AreaRegistration.RegisterAllAreas();

                Contract.Assume(RouteTable.Routes != null);
                this.RegisterRoutes(RouteTable.Routes);
                this.RegisterContainer();

                this.RegisterLast();

                startFailure = null;
            }
            catch (Exception ex) {
                startFailure = ex;
            }
        }

        private void Restart() {
            Contract.Assume(RouteTable.Routes != null);
            RouteTable.Routes.Clear();
            this.Start();
        }

        protected virtual void RegisterFirst() {
        }

        protected abstract void RegisterRoutes(RouteCollection routes);
        protected virtual void RegisterContainer() {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray());
            DiscoverAllModules(builder);

            container = builder.Build();
            dependencyResolver = new AutofacDependencyResolver(container);

            Contract.Assume(ControllerBuilder.Current != null);
            DependencyResolver.SetResolver(dependencyResolver);
        }

        protected virtual void RegisterLast() {
        }

        protected virtual void DiscoverAllModules(ContainerBuilder builder) {
            Contract.Assume(Server != null);
            var path = Server.MapPath("~/bin");

            foreach (var file in Directory.GetFiles(path, "*.dll")) {
                if (!ShouldDiscoverModulesIn(file))
                    continue;

                var assembly = Assembly.LoadFrom(file);
                Contract.Assume(assembly != null);
                var modules = from type in assembly.GetTypes()
                              where typeof(IModule).IsAssignableFrom(type)
                              select (IModule)Activator.CreateInstance(type);

                modules.ForEach(builder.RegisterModule);
            }
        }

        protected abstract bool ShouldDiscoverModulesIn(string path);

        protected void Application_BeginRequest(object sender, EventArgs e) {
            if (startFailure != null)
                this.Restart();

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            if (startFailure != null) {
                Response.ContentType = "text/plain";
                Response.Output.WriteLine("Critical failure while starting up the application.");
                Response.Output.WriteLine();
                Response.Output.Write(startFailure.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}
