using System.Linq;

using X.Infrastructure.Interfaces;
using X.Infrastructure.Repositories.Internal;

namespace X.Infrastructure.Repositories {
    public class KeyProvider : IKeyProvider {
        protected ISessionContext Context { get; private set; }

        public KeyProvider(ISessionContext context) {
            this.Context = context;
        }

        public object GetKey(object entity) {
            return this.Context.Session.GetIdentifierOrNull(entity);
        }
    }
}