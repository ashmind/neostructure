using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Neostructure.Repositories.UserTypes {
    /// <summary>
    /// The abstract base class for the NHibernate user-defined types.
    /// </summary>
    /// <typeparam name="T">Underlying type.</typeparam>
    public abstract class UserTypeBase<T> : IUserType {
        /// <summary>
        /// Reconstructs an object from the cacheable representation.<para />
        /// At the very least this method should perform a deep copy if the type is mutable.
        /// </summary>
        /// <remarks>
        /// Optional operation
        /// </remarks>
        /// <param name="cached">The object to be cached.</param>
        /// <param name="owner">The owner of the cached object.</param>
        /// <returns>A reconstructed object from the cacheable representation.</returns>
        public virtual T Assemble(T cached, object owner) {
            return this.DeepCopy(cached);
        }

        object IUserType.Assemble(object value, object owner) {
            return this.Assemble((T)value, owner);
        }

        /// <summary>
        /// Returns a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">Generally a collection element or entity field.</param>
        /// <returns>A deep copy of the persistent state.</returns>
        public abstract T DeepCopy(T value);

        object IUserType.DeepCopy(object value) {
            return this.DeepCopy((T)value);
        }

        /// <summary>
        /// Transforms the object into its cacheable representation. At the very least<para />
        /// this method should perform a deep copy if the type is mutable. That may not<para />
        /// be enough for some implementations, however; for example, associations must<para />
        /// be cached as identifier values.
        /// </summary>
        /// <remarks>
        /// Optional operation
        /// </remarks>
        /// <param name="value">The object to be cached.</param>
        /// <returns>A cacheable representation of the object.</returns>
        public virtual T Disassemble(T value) {
            return this.DeepCopy(value);
        }

        object IUserType.Disassemble(object value) {
            return this.Disassemble((T)value);
        }

        /// <summary>
        /// Gets a hashcode for the instance, consistent with persistence "equality".
        /// </summary>
        /// <param name="x">An object.</param>
        /// <returns>A hashcode.</returns>
        public virtual int GetHashCode(T x) {
            return x.GetHashCode();
        }

        int IUserType.GetHashCode(object x) {
            return this.GetHashCode((T)x);
        }

        /// <summary>
        /// Compares two instances of the class mapped by this type for persistent "equality"<para />
        /// ie. equality of persistent state.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>Comparison result.</returns>
        public virtual bool Equals(T x, T y) {
            if (x == null)
                return Object.Equals(x, y);

            return x.Equals(y);
        }

        bool IUserType.Equals(object x, object y) {
            return this.Equals((T)x, (T)y);
        }

        /// <summary>
        /// Gets a value indicating whether object of this type is mutable.
        /// </summary> 
        /// <value>True if object of this type is mutable, otherwise false.</value>
        public abstract bool IsMutable { get; }

        /// <summary>
        /// Gets the type returned by NullSafeGet().
        /// </summary>        
        /// <value>The type returned by the NullSafeGet().</value>
        public Type ReturnedType {
            get { return typeof(T); }
        }

        /// <summary>
        /// Retrieves an instance of the mapped class from a JDBC resultset. Implementors<para />
        /// should handle possibility of null values.
        /// </summary>
        /// <param name="rs">A IDataReader.</param>
        /// <param name="names">Column names.</param>
        /// <param name="owner">The containing entity.</param>
        /// <returns>An instance of the mapped class.</returns>        
        public abstract T NullSafeGet(IDataReader rs, string[] names, object owner);

        object IUserType.NullSafeGet(IDataReader rs, string[] names, object owner) {
            return this.NullSafeGet(rs, names, owner);
        }

        /// <summary>
        /// Writes an instance of the mapped class to a prepared statement. Implementors<para />
        /// should handle possibility of null values. A multi-column type should be<para />
        /// written to parameters starting from index.
        /// </summary>
        /// <param name="cmd">A IDbCommand.</param>
        /// <param name="value">The object to write.</param>
        /// <param name="index">Command parameter index.</param>
        public abstract void NullSafeSet(IDbCommand cmd, T value, int index);

        void IUserType.NullSafeSet(IDbCommand cmd, object value, int index) {
            this.NullSafeSet(cmd, (T)value, index);
        }

        /// <summary>
        /// During merge, replaces the existing (target) value in the entity we are merging<para />
        /// to with a new (original) value from the detached entity we are merging. For<para />
        /// immutable objects, or null values, it is safe to simply return the first<para />
        /// parameter. For mutable objects, it is safe to return a copy of the first<para />
        /// parameter. For objects with component values, it might make sense to recursively<para />
        /// replace component values.
        /// </summary>
        /// <param name="original">The value from the detached entity being merged.</param>
        /// <param name="target">The value in the managed entity.</param>
        /// <param name="owner">The managed entity.</param>
        /// <returns>The value to be merged.</returns>
        public virtual object Replace(T original, T target, object owner) {
            return this.DeepCopy(original);
        }

        object IUserType.Replace(object original, object target, object owner) {
            return this.Replace((T)original, (T)target, owner);
        }

        /// <summary>
        /// Gets the SQL types for the columns mapped by this type.
        /// </summary>
        /// <value>SQL types for the columns mapped by this type.</value>
        public abstract SqlType[] SqlTypes { get; }
    }
}
