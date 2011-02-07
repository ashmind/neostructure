using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace X.Infrastructure.Interfaces.Contracts {
    [ContractClassFor(typeof(IKeyProvider))]
    internal abstract class KeyProviderContract : IKeyProvider {
        public object GetKey(object entity) {
            Contract.Requires<ArgumentNullException>(entity != null);
            throw new NotSupportedException();
        }
    }
}
