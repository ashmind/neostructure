using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using NHibernate;

namespace X.Infrastructure.Repositories.Internal {
    public class SessionContext : ISessionContext {
        public event EventHandler Reconnected = delegate {};
        private readonly ISessionFactory sessionFactory;

        public SessionContext(ISessionFactory sessionFactory) {
            Contract.Requires<ArgumentNullException>(sessionFactory != null);

            this.sessionFactory = sessionFactory;
            this.Session = this.sessionFactory.OpenSession();
        }

        public ISession Session { get; private set; }

        public void Reconnect() {
            this.Session.Dispose();
            this.Session = this.sessionFactory.OpenSession();
            this.Reconnected(this, EventArgs.Empty);
        }

        [ContractInvariantMethod]
        private void CheckInvariants() {
            Contract.Invariant(this.sessionFactory != null);
            Contract.Invariant(this.Session != null);
        }

        #region IDisposable Members

        void IDisposable.Dispose() {
            this.Session.Dispose();
        }

        #endregion
    }
}
