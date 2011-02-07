using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace X.Infrastructure {
    public interface IRepositoryQueryable<T> : IQueryable<T> {
        IRepositoryQueryable<T> Include(Expression<Func<T, object>> propertyPath);
    }
}
