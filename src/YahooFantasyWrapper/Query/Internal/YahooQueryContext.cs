using System;
using System.Linq.Expressions;
using System.Net.Http;
using YahooFantasyWrapper.Query.Internal;

namespace YahooFantasyWrapper.Query.Internal
{
    internal class YahooQueryContext
    {
        internal static TResult Execute<TResult>(Expression expression)
        {
            var client = new HttpClient { BaseAddress = new Uri("https://fantasysports.yahooapis.com/fantasy/v2") };
            var urlString = new YahooExpressionVisitor(expression).ToString();
            return default;
        }
    }
}
