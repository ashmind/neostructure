using System;
using System.Collections.Generic;
using System.Linq;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Neostructure.Infrastructure.Repositories {
    public static class Database {
        public static void Create() {
            new SchemaExport(Configure()).Create(false, true);
        }

        public static void Update() {
            new SchemaUpdate(Configure()).Execute(false, true);
        }
        
        internal static Configuration Configure() {
            var configuration = new Configuration();
            configuration.Configure();

            return configuration;
        }
    }
}