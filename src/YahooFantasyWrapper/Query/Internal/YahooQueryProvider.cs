using System;
using System.Linq;
using System.Linq.Expressions;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Query.Internal
{
    public class YahooQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
        {
            return new YahooSet<TEntity>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return Execute<IYahooCollection>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return YahooQueryContext.Execute<TResult>(expression);
        }
    }
}
