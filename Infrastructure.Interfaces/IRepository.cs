using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using X.Infrastructure.Interfaces.Contracts;

namespace X.Infrastructure.Interfaces {
    [ContractClass(typeof(RepositoryContract<>))]
    public interface IRepository<T> {
        object Save(T entity);

        void Delete(T entity);

        T Load(object key);
        object GetKey(T entity);

        IRepositoryQueryable<T> Query();
        IList<T> Query(string query, object arguments, int limit);
        IList<TResult> Query<TResult>(string query, object arguments, int limit);
        void ChangeMany(string query, object arguments);

        void Reconnect();
        event EventHandler Reconnected;
    }
}
