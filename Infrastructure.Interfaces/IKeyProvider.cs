using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using Neostructure.Infrastructure.Interfaces.Contracts;

namespace Neostructure.Infrastructure.Interfaces {
    [ContractClass(typeof(KeyProviderContract))]
    public interface IKeyProvider {
        object GetKey(object entity);
    }
}