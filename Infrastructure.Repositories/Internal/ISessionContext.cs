using System;
using System.Collections.Generic;
using System.Linq;

using NHibernate;

namespace X.Infrastructure.Repositories.Internal {
    public interface ISessionContext : IDisposable {
        ISession Session { get; }

        event EventHandler Reconnected;
        void Reconnect();
    }
}
