using System;
using System.Collections.Generic;
using System.Linq;

namespace Neostructure.Infrastructure {
    public interface ITransactionSupport {
        void DoInTransaction(Action action);
    }
}