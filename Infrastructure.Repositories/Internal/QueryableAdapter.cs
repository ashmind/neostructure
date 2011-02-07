using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Linq;

using System.Linq.Expressions;

namespace X.Infrastructure.Repositories.Internal {
    internal class QueryableAdapter<T> : IRepositoryQueryable<T> {
        private readonly NhQueryable<T> inner;

        public QueryableAdapter(NhQueryable<T> inner) {
            this.inner = inner;
            this.Provider = new KeyEnabledQueryProvider(this.inner.Provider);
        }

        public IRepositoryQueryable<T> Include(Expression<Func<T, object>> propertyPath) {
            this.inner.Fetch(propertyPath);
            return this;
        }

        public IEnumerator<T> GetEnumerator() {
            return this.inner.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public Type ElementType {
            get { return this.inner.ElementType; }
        }

        public Expression Expression {
            get { return this.inner.Expression; }
        }

        public IQueryProvider Provider { get; private set; }
    }
}
