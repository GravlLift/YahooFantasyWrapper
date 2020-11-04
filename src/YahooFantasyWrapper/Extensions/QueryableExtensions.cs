using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models.Response;

namespace System.Collections.Generic
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static class QueryableExtensions
    {
        private sealed class YahooCollection<TYahooCollection, TYahooSubResource> : IYahooCollection<TYahooCollection, TYahooSubResource>
            where TYahooCollection : IYahooEntity
            where TYahooSubResource : IYahooEntity
        {
            private readonly IQueryable<TYahooCollection> _queryable;

            public YahooCollection(IQueryable<TYahooCollection> queryable)
            {
                _queryable = queryable;
            }

            public Expression Expression
                => _queryable.Expression;

            public Type ElementType
                => _queryable.ElementType;

            public IQueryProvider Provider
                => _queryable.Provider;

            public IAsyncEnumerator<TYahooCollection> GetAsyncEnumerator(CancellationToken cancellationToken = default)
                => ((IAsyncEnumerable<TYahooCollection>)_queryable).GetAsyncEnumerator(cancellationToken);

            public IEnumerator<TYahooCollection> GetEnumerator()
                => _queryable.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        internal static readonly MethodInfo FilterMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Filter))
                .Single();

        public static IYahooCollection<TYahooEntity, Player> Filter<TYahooEntity>(this IYahooCollection<TYahooEntity, Player> source,
            PlayerCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => new YahooCollection<TYahooEntity, Player>(
                source.Provider.CreateQuery<TYahooEntity>(
                    Expression.Call(instance: null,
                        method: FilterMethodInfo.MakeGenericMethod(typeof(TYahooEntity)),
                        arguments: new[] { source.Expression, Expression.Constant(filters) })));

        internal static readonly MethodInfo ResourceSubResourceMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(SubResource))
                .Single(mi => mi.GetGenericArguments()[1]
                    .GetGenericParameterConstraints()[0] == typeof(IYahooResource));
        public static IYahooCollection<TYahooEntity, TYahooSubResource> SubResource<TYahooEntity, TYahooSubResource>(
            this IQueryable<TYahooEntity> source,
            Expression<Func<TYahooEntity, TYahooSubResource>> expression)
            where TYahooEntity : IYahooEntity
            where TYahooSubResource : IYahooResource
            => new YahooCollection<TYahooEntity, TYahooSubResource>(
                source.Provider.CreateQuery<TYahooEntity>(
                    Expression.Call(instance: null,
                        method: ResourceSubResourceMethodInfo.MakeGenericMethod(typeof(TYahooEntity), typeof(TYahooSubResource)),
                        arguments: new[] { source.Expression, Expression.Quote(expression) })));


        internal static readonly MethodInfo CollectionSubResourceMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(SubResource))
                .Single(mi => mi.GetGenericArguments()[1]
                    .GetGenericParameterConstraints()[0] == typeof(IYahooCollection));
        public static IYahooCollection<TYahooEntity, TYahooSubCollection> SubResource<TYahooEntity, TYahooSubCollection>(
            this IQueryable<TYahooEntity> source,
            Expression<Func<TYahooEntity, IEnumerable<TYahooSubCollection>>> expression)
            where TYahooEntity : IYahooEntity
            where TYahooSubCollection : IYahooCollection
            => new YahooCollection<TYahooEntity, TYahooSubCollection>(
                source.Provider.CreateQuery<TYahooEntity>(
                    Expression.Call(instance: null,
                        method: CollectionSubResourceMethodInfo.MakeGenericMethod(typeof(TYahooEntity), typeof(TYahooSubCollection)),
                        arguments: new[] { source.Expression, Expression.Quote(expression) })));

    }
#pragma warning restore IDE0060 // Remove unused parameter

}
