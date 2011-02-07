using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Linq.Visitors;
using NHibernate.Mapping;

namespace X.Infrastructure.Repositories.Internal {
    public class KeyMethodToIdRewritingVisitor : ExpressionVisitor {
        #region FakePropertyInfo

        private class FakePropertyInfo : PropertyInfo {
            private readonly string name;
            private readonly Type declaringType;
            private readonly Type propertyType;

            public FakePropertyInfo(string name, Type declaringType, Type propertyType) {
                this.name = name;
                this.declaringType = declaringType;
                this.propertyType = propertyType;
            }

            public override object[] GetCustomAttributes(bool inherit) {
                throw new NotImplementedException();
            }

            public override bool IsDefined(Type attributeType, bool inherit) {
                throw new NotImplementedException();
            }

            public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture) {
                throw new NotImplementedException();
            }

            public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture) {
                throw new NotImplementedException();
            }

            public override MethodInfo[] GetAccessors(bool nonPublic) {
                throw new NotImplementedException();
            }

            public override MethodInfo GetGetMethod(bool nonPublic) {
                return (MethodInfo)MethodBase.GetCurrentMethod(); // M-M-M-M-M-MONSTERHACK!!
            }

            public override MethodInfo GetSetMethod(bool nonPublic) {
                throw new NotImplementedException();
            }

            public override ParameterInfo[] GetIndexParameters() {
                throw new NotImplementedException();
            }

            public override string Name {
                get { return this.name; }
            }

            public override Type DeclaringType {
                get { return this.declaringType; }
            }

            public override Type ReflectedType {
                get { return this.declaringType; }
            }

            public override Type PropertyType {
                get { return this.propertyType; }
            }

            public override PropertyAttributes Attributes {
                get { throw new NotImplementedException(); }
            }

            public override bool CanRead {
                get { return true; }
            }

            public override bool CanWrite {
                get { return false; }
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit) {
                throw new NotImplementedException();
            }
        }

        #endregion

        protected override Expression VisitMethodCall(MethodCallExpression m) {
            if (m.Method.Name == "GetKey" && m.Arguments.Count == 1) {
                var fakeProperty = new FakePropertyInfo(RootClass.DefaultIdentifierColumnName, m.Arguments[0].Type, m.Type);
                return Expression.Property(m.Arguments[0], fakeProperty);
            }

            return base.VisitMethodCall(m);
        }
    }
}