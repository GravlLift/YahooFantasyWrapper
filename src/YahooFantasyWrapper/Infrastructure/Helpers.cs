using System;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Infrastructure
{
    public static class Enum<T> where T : Enum
    {
        public static string GetName(T obj)
        {
            return Enum.GetName(typeof(T), obj);
        }
    }

    public static class XmlHelpers
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
