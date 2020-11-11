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
}
