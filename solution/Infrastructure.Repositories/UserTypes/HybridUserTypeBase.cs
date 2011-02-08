using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using NHibernate.Engine;
using NHibernate.Type;
using NHibernate.UserTypes;

namespace Neostructure.Repositories.UserTypes {
    /// <summary>
    /// The abstract base class for the hybrids between NHibernate composite and user types.
    /// </summary>
    /// <typeparam name="T">Underlying type.</typeparam>
    public abstract class HybridUserTypeBase<T> : UserTypeBase<T>, ICompositeUserType {
        /// <summary>
        /// Gets the "property names" that may be used in a query.
        /// </summary>
        /// <value>An array of property names.</value>
        public abstract string[] PropertyNames { get; }

        /// <summary>
        /// Gets the "property types" corresponding to <see cref="PropertyNames" />.
        /// </summary>
        /// <value>An array of property types.</value>
        public abstract IType[] PropertyTypes { get; }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <param name="component">An instance of class mapped by this type.</param>
        /// <param name="property">The index of the property.</param>
        /// <returns>The property value.</returns>
        public abstract object GetPropertyValue(T component, int property);

        /// <summary>
        /// Set the value of a property.
        /// </summary>
        /// <param name="component">An instance of class mapped by this type.</param>
        /// <param name="property">The index of the property.</param>
        /// <param name="value">The value to set.</param>
        public abstract void SetPropertyValue(T component, int property, object value);

        #region ICompositeUserType Members

        object ICompositeUserType.Assemble(object cached, ISessionImplementor session, object owner) {
            return this.Assemble((T)cached, owner);
        }

        object ICompositeUserType.DeepCopy(object value) {
            return this.DeepCopy((T)value);
        }

        object ICompositeUserType.Disassemble(object value, ISessionImplementor session) {
            return this.Disassemble((T)value);
        }

        bool ICompositeUserType.Equals(object x, object y) {
            return this.Equals((T)x, (T)y);
        }

        int ICompositeUserType.GetHashCode(object x) {
            return this.GetHashCode((T)x);
        }

        object ICompositeUserType.GetPropertyValue(object component, int property) {
            return this.GetPropertyValue((T)component, property);
        }

        object ICompositeUserType.NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner) {
            return this.NullSafeGet(dr, names, owner);
        }

        void ICompositeUserType.NullSafeSet(IDbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session) {
            this.NullSafeSet(cmd, (T)value, index);
        }

        object ICompositeUserType.Replace(object original, object target, ISessionImplementor session, object owner) {
            return this.Replace((T)original, (T)target, owner);
        }

        Type ICompositeUserType.ReturnedClass {
            get { return this.ReturnedType; }
        }

        void ICompositeUserType.SetPropertyValue(object component, int property, object value) {
            this.SetPropertyValue((T)component, property, value);
        }

        #endregion
    }
}
