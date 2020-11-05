using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models.Response;
using YahooFantasyWrapper.Query.Internal;

namespace System.Collections.Generic
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static class QueryableExtensions
    {
        private sealed class YahooCollection<TYahooCollection, TYahooSubResource> : IYahooQueryable<TYahooCollection, TYahooSubResource>
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

        #region Filter

        internal static readonly MethodInfo PlayerFilterMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Filter))
                .Single(mi => mi.ReturnType.GenericTypeArguments[1] == typeof(Player));
        public static IYahooQueryable<TYahooEntity, Player> Filter<TYahooEntity>(this IYahooQueryable<TYahooEntity, Player> source,
            PlayerCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => new YahooCollection<TYahooEntity, Player>(
                source.Provider is YahooQueryProvider
                    ? source.Provider.CreateQuery<TYahooEntity>(
                        Expression.Call(instance: null,
                            method: PlayerFilterMethodInfo.MakeGenericMethod(typeof(TYahooEntity)),
                            arguments: new[] { source.Expression, Expression.Constant(filters) }))
                    : source);

        internal static readonly MethodInfo GameFilterMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Filter))
                .Single(mi => mi.ReturnType.GenericTypeArguments[1] == typeof(Game));
        public static IYahooQueryable<TYahooEntity, Game> Filter<TYahooEntity>(this IYahooQueryable<TYahooEntity, Game> source,
            GameCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => new YahooCollection<TYahooEntity, Game>(
                source.Provider is YahooQueryProvider
                    ? source.Provider.CreateQuery<TYahooEntity>(
                        Expression.Call(instance: null,
                            method: GameFilterMethodInfo.MakeGenericMethod(typeof(TYahooEntity)),
                            arguments: new[] { source.Expression, Expression.Constant(filters) }))
                    : source);

        internal static readonly MethodInfo LeagueFilterMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Filter))
                .Single(mi => mi.ReturnType.GenericTypeArguments[1] == typeof(League));
        public static IYahooQueryable<TYahooEntity, League> Filter<TYahooEntity>(this IYahooQueryable<TYahooEntity, League> source,
            LeagueCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => new YahooCollection<TYahooEntity, League>(
                source.Provider is YahooQueryProvider
                    ? source.Provider.CreateQuery<TYahooEntity>(
                        Expression.Call(instance: null,
                            method: LeagueFilterMethodInfo.MakeGenericMethod(typeof(TYahooEntity)),
                            arguments: new[] { source.Expression, Expression.Constant(filters) }))
                    : source);

        internal static readonly MethodInfo UserFilterMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Filter))
                .Single(mi => mi.ReturnType.GenericTypeArguments[1] == typeof(User));
        public static IYahooQueryable<TYahooEntity, User> Filter<TYahooEntity>(this IYahooQueryable<TYahooEntity, User> source,
            UserCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => new YahooCollection<TYahooEntity, User>(
                source.Provider is YahooQueryProvider
                    ? source.Provider.CreateQuery<TYahooEntity>(
                        Expression.Call(instance: null,
                            method: UserFilterMethodInfo.MakeGenericMethod(typeof(TYahooEntity)),
                            arguments: new[] { source.Expression, Expression.Constant(filters) }))
                    : source);
        #endregion

        #region SubResource

        internal static readonly MethodInfo ResourceSubResourceMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(SubResource))
                .Single(mi => mi.GetGenericArguments().Length == 2 &&
                    mi.GetGenericArguments()[1].GetGenericParameterConstraints()[0] == typeof(IYahooResource));
        public static IYahooQueryable<TSource, TYahooSubResource> SubResource<TSource, TYahooSubResource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TYahooSubResource>> expression)
            where TSource : IYahooEntity
            where TYahooSubResource : IYahooResource
            => new YahooCollection<TSource, TYahooSubResource>(
                source.Provider.CreateQuery<TSource>(
                    Expression.Call(instance: null,
                        method: ResourceSubResourceMethodInfo
                            .MakeGenericMethod(typeof(TSource), typeof(TYahooSubResource)),
                        arguments: new[] { source.Expression, Expression.Quote(expression) })));

        internal static readonly MethodInfo ResourceNextSubResourceMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(SubResource))
                .Single(mi => mi.GetGenericArguments().Length == 3 &&
                    mi.GetGenericArguments()[1].GetGenericParameterConstraints()[0] == typeof(IYahooCollection));
        public static IYahooQueryable<TSource, TYahooSubResource> SubResource<TSource, TPrevSubCollection, TYahooSubResource>(
            this IYahooQueryable<TSource, TPrevSubCollection> source,
            Expression<Func<TPrevSubCollection, TYahooSubResource>> expression)
            where TSource : IYahooEntity
            where TPrevSubCollection : IYahooEntity
            where TYahooSubResource : IYahooResource
            => new YahooCollection<TSource, TYahooSubResource>(
                source.Provider.CreateQuery<TSource>(
                    Expression.Call(instance: null,
                        method: CollectionNextSubResourceMethodInfo
                            .MakeGenericMethod(typeof(TSource), typeof(TPrevSubCollection), typeof(TYahooSubResource)),
                        arguments: new[] { source.Expression, Expression.Quote(expression) })));


        internal static readonly MethodInfo CollectionSubResourceMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(SubResource))
                .Single(mi => mi.GetGenericArguments().Length == 2 &&
                    mi.GetGenericArguments()[1].GetGenericParameterConstraints()[0] == typeof(IYahooCollection));
        public static IYahooQueryable<TSource, TYahooSubCollection> SubResource<TSource, TYahooSubCollection>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, IEnumerable<TYahooSubCollection>>> expression)
            where TSource : IYahooEntity
            where TYahooSubCollection : IYahooCollection
            => new YahooCollection<TSource, TYahooSubCollection>(
                source.Provider.CreateQuery<TSource>(
                    Expression.Call(instance: null,
                        method: CollectionSubResourceMethodInfo.MakeGenericMethod(typeof(TSource), typeof(TYahooSubCollection)),
                        arguments: new[] { source.Expression, Expression.Quote(expression) })));

        internal static readonly MethodInfo CollectionNextSubResourceMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(SubResource))
                .Single(mi => mi.GetGenericArguments().Length == 3 &&
                    mi.GetGenericArguments()[1].GetGenericParameterConstraints()[0] == typeof(IYahooCollection));
        public static IYahooQueryable<TSource, TSubCollection> SubResource<TSource, TPrevSubCollection, TSubCollection>(
            this IYahooQueryable<TSource, TPrevSubCollection> source,
            Expression<Func<TPrevSubCollection, IEnumerable<TSubCollection>>> expression)
            where TSource : IYahooEntity
            where TPrevSubCollection : IYahooEntity
            where TSubCollection : IYahooCollection
            => new YahooCollection<TSource, TSubCollection>(
                source.Provider.CreateQuery<TSource>(
                    Expression.Call(instance: null,
                        method: CollectionNextSubResourceMethodInfo
                            .MakeGenericMethod(typeof(TSource), typeof(TPrevSubCollection), typeof(TSubCollection)),
                        arguments: new[] { source.Expression, Expression.Quote(expression) })));
        #endregion

    }
#pragma warning restore IDE0060 // Remove unused parameter

}
