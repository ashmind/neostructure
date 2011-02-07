using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Engine;
using NHibernate.Proxy;

namespace X.Infrastructure.Repositories.Internal {
    internal static class SessionExtensions {
        public static object GetIdentifierOrNull(this ISession session, object obj) {
            // I can't use NH GetIndetifier because it throws on not-found

            var proxy = obj as INHibernateProxy;
            if (proxy != null) {
                var hibernateLazyInitializer = proxy.HibernateLazyInitializer;
                if (hibernateLazyInitializer.Session != session)
                    return null;

                return hibernateLazyInitializer.Identifier;
            }
            var entry = ((ISessionImplementor)session).PersistenceContext.GetEntry(obj);
            if (entry == null)
                return null;

            return entry.Id;
        }
    }
}
