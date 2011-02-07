using System.Linq;

using Neostructure.Infrastructure.Interfaces;
using Neostructure.Infrastructure.Repositories.Internal;

namespace Neostructure.Infrastructure.Repositories {
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