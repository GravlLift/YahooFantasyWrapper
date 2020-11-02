using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Net.Http.Xml;
using YahooFantasyWrapper.Models.Response;
using System.Threading.Tasks;

namespace YahooFantasyWrapper.Query.Internal
{
    public class YahooQueryProvider : IQueryProvider
    {
        private static readonly MethodInfo _genericCreateQueryMethod
            = typeof(YahooQueryProvider).GetRuntimeMethods()
                .Single(m => (m.Name == "CreateQuery") && m.IsGenericMethod);
        private static readonly MethodInfo _genericExecuteMethod
            = typeof(YahooQueryProvider).GetRuntimeMethods()
                .Single(m => (m.Name == "Execute") && m.IsGenericMethod);

        private readonly HttpClient httpClient;

        public YahooQueryProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IQueryable CreateQuery(Expression expression)
            => (IQueryable)_genericCreateQueryMethod
                .MakeGenericMethod(expression.Type.GetSequenceType())
                .Invoke(this, new object[] { expression });

        public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
        {
            return new YahooSet<TEntity>(this, expression);
        }

        public object Execute(Expression expression)
            => _genericExecuteMethod.MakeGenericMethod(expression.Type)
                .Invoke(this, new object[] { expression });

        public TResult Execute<TResult>(Expression expression)
        {
            var urlString = new YahooExpressionVisitor(expression).ToString();
            return httpClient.GetFromXmlAsync<TResult>(urlString).GetAwaiter().GetResult();
        }
    }
}
