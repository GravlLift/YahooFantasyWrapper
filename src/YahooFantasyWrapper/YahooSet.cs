using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YahooFantasyWrapper.Models.Response;
using YahooFantasyWrapper.Query.Internal;

namespace YahooFantasyWrapper
{
    public class YahooSet<TYahooEntity> : IYahooQueryable<TYahooEntity, TYahooEntity>
        where TYahooEntity : IYahooEntity
    {
        public YahooSet(YahooQueryProvider queryProvider)
        {
            Provider = queryProvider;
            Expression = Expression.Constant(this);
        }

        internal YahooSet(IQueryProvider provider, Expression expression)
        {
            Provider = provider;
            Expression = expression;
        }

        public Type ElementType
            => typeof(TYahooEntity);

        public Expression Expression { get; private set; }

        public IQueryProvider Provider { get; private set; }

        public IEnumerator<TYahooEntity> GetEnumerator()
            => Provider.Execute<List<TYahooEntity>>(Expression).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException();
    }
}
