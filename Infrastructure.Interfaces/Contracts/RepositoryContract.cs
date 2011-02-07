using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace X.Infrastructure.Interfaces.Contracts {
    [ContractClassFor(typeof(IRepository<>))]
    internal abstract class RepositoryContract<T> : IRepository<T> {
        public event EventHandler Reconnected;
                
        public object GetKey(T entity) {
            Contract.Requires<ArgumentNullException>(entity != null);
            Contract.Ensures(Contract.Result<object>() != null);
            throw new NotSupportedException();
        }
        
        public object Save(T entity) {
            Contract.Requires<ArgumentNullException>(entity != null);
            throw new NotSupportedException();
        }

        public void Delete(T entity) {
            Contract.Requires<ArgumentNullException>(entity != null);
            throw new NotSupportedException();
        }

        T IRepository<T>.Load(object key) {
            Contract.Requires<ArgumentNullException>(key != null);
            throw new NotSupportedException();
        }

        IRepositoryQueryable<T> IRepository<T>.Query() {
            Contract.Ensures(Contract.Result<IRepositoryQueryable<T>>() != null);
            throw new NotSupportedException();
        }

        public IList<T> Query(string query, object arguments, int limit) {
            Contract.Requires<ArgumentNullException>(query != null);
            Contract.Requires<ArgumentException>(query.Length > 0);
            Contract.Requires<ArgumentException>(limit >= 0);
            Contract.Ensures(Contract.Result<IList<T>>() != null);
            throw new NotSupportedException();
        }

        public IList<TResult> Query<TResult>(string query, object arguments, int limit) {
            Contract.Requires<ArgumentNullException>(query != null);
            Contract.Requires<ArgumentException>(query.Length > 0);
            Contract.Requires<ArgumentException>(limit >= 0);
            Contract.Ensures(Contract.Result<IList<TResult>>() != null);
            throw new NotSupportedException();
        }

        public void ChangeMany(string query, object arguments) {
            Contract.Requires<ArgumentNullException>(query != null);
            Contract.Requires<ArgumentException>(query.Length > 0);
            throw new NotSupportedException();
        }

        public void Reconnect() {
            this.Reconnected(this, EventArgs.Empty);
            throw new NotSupportedException();
        }
    }
}
