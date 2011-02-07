using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using X.Infrastructure.Interfaces.Contracts;

namespace X.Infrastructure.Interfaces {
    [ContractClass(typeof(KeyProviderContract))]
    public interface IKeyProvider {
        object GetKey(object entity);
    }
}