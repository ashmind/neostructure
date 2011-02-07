using System;

using X.Infrastructure.Interfaces;
using X.Infrastructure.Repositories.Internal;

namespace X.Infrastructure.Repositories {
    internal class TransactionSupport : ITransactionSupport {
        private readonly ISessionContext context;

        public TransactionSupport(ISessionContext context) {
            this.context = context;
        }

        public void DoInTransaction(Action action) {
            var transaction = this.context.Session.BeginTransaction();
            try {
                action();
                transaction.Commit();
            }
            catch {
                transaction.Rollback();
                throw;
            }
         }
    }
}