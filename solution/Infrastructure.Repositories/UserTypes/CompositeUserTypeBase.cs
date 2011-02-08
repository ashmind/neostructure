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
    /// The abstract base class for the NHibernate composite types.
    /// </summary>
    /// <typeparam name="T">Underlying type.</typeparam>
    public abstract class CompositeUserTypeBase<T> : ICompositeUserType {
        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <param name="component">An instance of class mapped by this type.</param>
        /// <param name="property">The index of the property.</param>
        /// <returns>The property value.</returns>
        public abstract object GetPropertyValue(T component, int property);

        object ICompositeUserType.GetPropertyValue(object component, int property) {
            return this.GetPropertyValue((T)component, property);
        }

        /// <summary>
        /// Set the value of a property.
        /// </summary>
        /// <param name="component">An instance of class mapped by this type.</param>
        /// <param name="property">The index of the property.</param>
        /// <param name="value">The value to set.</param>
        public abstract void SetPropertyValue(T component, int property, object value);

        void ICompositeUserType.SetPropertyValue(object component, int property, object value) {
            this.SetPropertyValue((T)component, property, value);
        }

        /// <summary>
        /// Retrieve an instance of the mapped class from a <see cref="IDataReader" />. Implementors
        /// should handle possibility of null values.
        /// </summary>
        /// <param name="dr">The <see cref="IDataReader" /> to read from.</param>
        /// <param name="names">The column names.</param>
        /// <param name="session">The current session.</param>
        /// <param name="owner">The containing entity.</param>
        /// <returns>
        /// An instance of the <see cref="ReturnedClass" />.
        /// </returns>
        public abstract T NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner);

        object ICompositeUserType.NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner) {
            return this.NullSafeGet(dr, names, session, owner);
        }

        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        /// Implementors should handle possibility of null values.
        /// A multi-column type should be written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">The <see cref="IDbCommand" /> to set parameters to.</param>
        /// <param name="value">The instance of the mapped class.</param>
        /// <param name="index">The index from which to start adding parameters.</param>
        /// <param name="session">The current session.</param>
        public abstract void NullSafeSet(IDbCommand cmd, T value, int index, ISessionImplementor session);

        void ICompositeUserType.NullSafeSet(IDbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session) {
            this.NullSafeSet(cmd, (T)value, index, session);
        }

        /// <summary>
        /// Return a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">Generally a collection element or entity field.</param>
        /// <returns>
        /// A deep copy of <paramref name="value" />.
        /// </returns>
        public abstract T DeepCopy(T value);

        object ICompositeUserType.DeepCopy(object value) {
            return this.DeepCopy((T)value);
        }

        /// <summary>
        /// Transform the object into its cacheable representation.
        /// At the very least this method should perform a deep copy.
        /// That may not be enough for some implementations, method should perform a deep copy.
        /// That may not be enough for some implementations, however; for example, associations
        /// must be cached as identifier values.
        /// </summary>
        /// <param name="value">The object to be cached.</param>
        /// <param name="session">The current session.</param>
        /// <returns>
        /// The cacheable representation of <paramref name="value" />.
        /// </returns>
        public virtual T Disassemble(T value, ISessionImplementor session) {
            return this.DeepCopy(value);
        }

        object ICompositeUserType.Disassemble(object value, ISessionImplementor session) {
            return this.Disassemble((T)value, session);
        }

        /// <summary>
        /// Reconstruct an object from the cacheable representation.
        /// At the very least this method should perform a deep copy. 
        /// </summary>
        /// <param name="cached">The cached object.</param>
        /// <param name="session">The current session.</param>
        /// <param name="owner">The containing entity.</param>
        /// <returns>
        /// The object reconstructed from <paramref name="cached" /> value.
        /// </returns>
        public virtual T Assemble(T cached, ISessionImplementor session, object owner) {
            return this.DeepCopy(cached);
        }

        object ICompositeUserType.Assemble(object cached, ISessionImplementor session, object owner) {
            return this.Assemble((T)cached, session, owner);
        }

        /// <summary>
        /// During merge, replace the existing (target) value in the entity we are merging to
        /// with a new (original) value from the detached entity we are merging. For immutable
        /// objects, or null values, it is safe to simply return the first parameter. For
        /// mutable objects, it is safe to return a copy of the first parameter. However, since
        /// composite user types often define component values, it might make sense to recursively
        /// replace component values in the target object.
        /// </summary>
        /// <param name="original">Original value from the detached entity.</param>
        /// <param name="target">Current value in the entity we are merging to.</param>
        /// <param name="session">The current session.</param>
        /// <param name="owner">The containing entity.</param>
        /// <returns>
        /// The result of merge operation between <paramref name="original" /> and <paramref name="target" />.
        /// </returns>
        public virtual T Replace(T original, T target, ISessionImplementor session, object owner) {
            return this.DeepCopy(original);
        }

        object ICompositeUserType.Replace(object original, object target, ISessionImplementor session, object owner) {
            return this.Replace((T)original, (T)target, session, owner);
        }

        /// <summary>
        /// Compare two instances of the class mapped by this type for persistence "equality",
        /// ie. equality of persistent state.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>
        /// <c>true</c> if the instances are equal; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(T x, T y) {
            return object.Equals(x, y);
        }

        bool ICompositeUserType.Equals(object x, object y) {
            return this.Equals((T)x, (T)y);
        }

        /// <summary>
        /// Get a hashcode for the instance, consistent with persistence "equality".
        /// </summary>
        /// <param name="x">The instance to get hash code for.</param>
        /// <returns>
        /// A hash code for instance <paramref name="x" />. 
        /// </returns>
        public virtual int GetHashCode(T x) {
            return x.GetHashCode();
        }

        int ICompositeUserType.GetHashCode(object x) {
            return this.GetHashCode((T)x);
        }

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
        /// Gets the <see cref="Type" /> of instance returned by <see cref="NullSafeGet" />.
        /// </summary>
        /// <value>
        /// The <see cref="Type" /> of instance returned by <see cref="NullSafeGet" />.
        /// </value>
        public virtual Type ReturnedClass {
            get { return typeof(T); }
        }

        /// <summary>
        /// Gets a value indicating whether instances of mapped class are mutable.
        /// </summary>
        /// <value>
        /// <c>True</c> if the instances of mapped class are mutable; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsMutable { get; }
    }
}