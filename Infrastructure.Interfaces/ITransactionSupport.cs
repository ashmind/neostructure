using System;
using System.Collections.Generic;
using System.Linq;

namespace X.Infrastructure {
    public interface ITransactionSupport {
        void DoInTransaction(Action action);
    }
}