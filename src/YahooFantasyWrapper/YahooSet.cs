using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using YahooFantasyWrapper.Query.Internal;

namespace YahooFantasyWrapper
{
    public class YahooSet<TEntity> : IQueryable<TEntity>
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
            => typeof(TEntity);

        public Expression Expression { get; private set; }

        public IQueryProvider Provider { get; private set; }

        public IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
