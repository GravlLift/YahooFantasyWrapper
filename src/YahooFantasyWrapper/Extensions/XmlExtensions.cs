using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Xml.Serialization
{
    public static class XmlExtensions
    {
        public static void Add<T>(this XmlAttributeOverrides overrides, XmlAttributes attributes)
        {
            overrides.Add(typeof(T), attributes);
        }
        public static void Add<T>(this XmlAttributeOverrides overrides, Expression<Func<T, object>> selector, XmlAttributes attributes)
        {
            if (!(selector.Body is MemberExpression body))
            {
                UnaryExpression ubody = (UnaryExpression)selector.Body;
                body = ubody.Operand as MemberExpression;
            }

            overrides.Add(typeof(T), body.Member.Name, attributes);
        }
    }
}
