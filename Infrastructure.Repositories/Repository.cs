using System;
using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Impl;
using NHibernate.Linq;

using X.Infrastructure.Interfaces;
using X.Infrastructure.Repositories.Internal;

namespace X.Infrastructure.Repositories {
    public class Repository<T> : IRepository<T> {
        public event EventHandler Reconnected = delegate {}; 

        internal ISessionContext Context { get; private set; }
        protected ISession Session {
            get { return this.Context.Session; }
        }

        public Repository(ISessionContext context) {
            this.Context = context;
            this.Context.Reconnected += Context_Reconnected;
        }

        void Context_Reconnected(object sender, EventArgs e) {
            this.Reconnected(this, e);
        }

        public T Load(object key) {
            return this.Session.Load<T>(key);
        }

        public object GetKey(T entity) {
            return this.Session.GetIdentifierOrNull(entity);
        }

        public object Save(T entity) {
            var key = this.Session.Save(entity);
            this.Session.Flush(); // HACK?
            return key;
        }

        //public object Merge(T entity, object key) {
        //    if (key == null) {
        //        this.Session.Save(entity);
        //        return entity;
        //    }

        //    return this.Session.SaveOrUpdateCopy(entity, key);
        //}

        public void Delete(T entity) {
            this.Session.Delete(entity);
            //this.Session.CreateQuery("delete " + typeof(T).Name + " entity where entity.id = :id")
            //            .SetParameter("id", GetKey(entity))
            //            .ExecuteUpdate();
        }

        public IRepositoryQueryable<T> Query() {
            var query = this.Session.Query<T>();
            return new QueryableAdapter<T>((NhQueryable<T>)query);
        }

        // this is only to workaround Linq-to-NH bugs
        public IList<T> Query(string query, object arguments, int limit) {
            return Query<T>(query, arguments, limit);
        }

        public IList<TResult> Query<TResult>(string query, object arguments, int limit) {
            if (!query.Contains(" "))
                return NamedQuery<TResult>(query, arguments, limit);

            var queryObject = this.Session.CreateQuery(query);
            ApplyTo(queryObject, arguments, limit);

            return queryObject.List<TResult>();
        }

        // and this is to workaround HQL limitations
        private IList<TResult> NamedQuery<TResult>(string queryName, object arguments, int limit) {
            var queryObject = this.Session.GetNamedQuery(queryName);
            ApplyTo(queryObject, arguments, limit);

            return queryObject.List<TResult>();
        }

        private void ApplyTo(IQuery query, object arguments, int limit) {
            if (arguments != null)
                query.SetProperties(arguments);

            if (limit < int.MaxValue)
                query.SetMaxResults(limit);            
        }

        public void ChangeMany(string query, object arguments) {
            this.Session.CreateQuery(query)
                        .SetProperties(arguments)
                        .ExecuteUpdate();
        }

        public void Reconnect() {
            this.Context.Reconnect();
        }

        public void Dispose() {
            this.Context.Reconnected -= Context_Reconnected;
        }
    }
}