using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

using NHibernate.Linq;
using Neostructure.Infrastructure.Repositories.Internal;

namespace Neostructure.Infrastructure.Repositories.Internal {
    public class KeyEnabledQueryProvider : IQueryProvider {
        private readonly IQueryProvider inner;

        public KeyEnabledQueryProvider(IQueryProvider inner) : base() {
            Contract.Requires<ArgumentNullException>(inner != null);

            this.inner = inner;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) {
            return this.inner.CreateQuery<TElement>(Rewrite(expression));
        }

        public IQueryable CreateQuery(Expression expression) {
            return this.inner.CreateQuery(Rewrite(expression));
        }

        public TResult Execute<TResult>(Expression expression) {
            return this.inner.Execute<TResult>(Rewrite(expression));
        }

        public object Execute(Expression expression) {
            return this.inner.Execute(Rewrite(expression));
        }
        
        private static Expression Rewrite(Expression expression) {
            return new KeyMethodToIdRewritingVisitor().Visit(expression);
        }

        [ContractInvariantMethod]
        private void CheckInvariants() {
            Contract.Invariant(this.inner != null);
        }
    }
}