using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models.Response;
using YahooFantasyWrapper.Query;
using YahooFantasyWrapper.Query.Internal;

namespace System.Collections.Generic
{
    public static class QueryableExtensions
    {
        private sealed class YahooCollection<TYahooCollection, TYahooSubResource> : IYahooQueryable<TYahooCollection, TYahooSubResource>, IAsyncEnumerable<TYahooCollection>
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

        internal static readonly MethodInfo TeamFilterMethodInfo
            = typeof(QueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Filter))
                .Single(mi => mi.ReturnType.GenericTypeArguments[1] == typeof(Team));
        public static IYahooQueryable<TYahooEntity, Team> Filter<TYahooEntity>(this IYahooQueryable<TYahooEntity, Team> source,
            TeamCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => new YahooCollection<TYahooEntity, Team>(
                source.Provider is YahooQueryProvider
                    ? source.Provider.CreateQuery<TYahooEntity>(
                        Expression.Call(instance: null,
                            method: TeamFilterMethodInfo.MakeGenericMethod(typeof(TYahooEntity)),
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
                    mi.GetGenericArguments()[2].GetGenericParameterConstraints()[0] == typeof(IYahooResource));
        public static IYahooQueryable<TSource, TYahooSubResource> SubResource<TSource, TPrevSubCollection, TYahooSubResource>(
            this IYahooQueryable<TSource, TPrevSubCollection> source,
            Expression<Func<TPrevSubCollection, TYahooSubResource>> expression)
            where TSource : IYahooEntity
            where TPrevSubCollection : IYahooEntity
            where TYahooSubResource : IYahooResource
            => new YahooCollection<TSource, TYahooSubResource>(
                source.Provider.CreateQuery<TSource>(
                    Expression.Call(instance: null,
                        method: ResourceNextSubResourceMethodInfo
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
                    mi.GetGenericArguments()[2].GetGenericParameterConstraints()[0] == typeof(IYahooCollection));
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

        #region First/FirstOrDefault

        /// <summary>
        ///     Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the first element in <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<TSource> FirstAsync<TSource>(
            this IYahooQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            where TSource : IYahooCollection
        {
            return ExecuteAsync<TSource, TSource>(QueryableMethods.FirstWithoutPredicate, source, cancellationToken);
        }

        /// <summary>
        ///     Asynchronously returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the first element in <paramref name="source" /> that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <para>
        ///         No element satisfies the condition in <paramref name="predicate" />
        ///     </para>
        ///     <para>
        ///         -or -
        ///     </para>
        ///     <para>
        ///         <paramref name="source" /> contains no elements.
        ///     </para>
        /// </exception>
        public static Task<TSource> FirstAsync<TSource>(
            this IYahooQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            where TSource : IYahooCollection
        {
            return ExecuteAsync<TSource, TSource>(QueryableMethods.FirstWithPredicate,
                source, predicate, cancellationToken);
        }

        /// <summary>
        ///     Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="default" /> ( <typeparamref name="TSource" /> ) if
        ///     <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IYahooQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            where TSource : IYahooCollection
        {
            return ExecuteAsync<TSource, TSource>(QueryableMethods.FirstOrDefaultWithoutPredicate,
                source, cancellationToken);
        }

        /// <summary>
        ///     Asynchronously returns the first element of a sequence that satisfies a specified condition
        ///     or a default value if no such element is found.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="default" /> ( <typeparamref name="TSource" /> ) if <paramref name="source" />
        ///     is empty or if no element passes the test specified by <paramref name="predicate" /> ; otherwise, the first
        ///     element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IYahooQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            where TSource : IYahooCollection
        {
            return ExecuteAsync<TSource, TSource>(QueryableMethods.FirstOrDefaultWithPredicate,
                source, predicate, cancellationToken);
        }

        #endregion

        #region ToList/Array

        /// <summary>
        ///     Asynchronously creates a <see cref="List{T}" /> from an <see cref="IQueryable{T}" /> by enumerating it
        ///     asynchronously.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create a list from.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static async Task<List<TSource>> ToListAsync<TSource>(
            this IYahooQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            where TSource : IYahooCollection
        {
            var list = new List<TSource>();
            await foreach (var element in source.AsAsyncEnumerable().WithCancellation(cancellationToken))
            {
                list.Add(element);
            }

            return list;
        }

        /// <summary>
        ///     Asynchronously creates an array from an <see cref="IQueryable{T}" /> by enumerating it asynchronously.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create an array from.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains an array that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static async Task<TSource[]> ToArrayAsync<TSource>(
            this IYahooQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            where TSource : IYahooCollection
            => (await source.ToListAsync(cancellationToken).ConfigureAwait(false)).ToArray();

        #endregion

        #region AsAsyncEnumerable

        /// <summary>
        ///     Returns an <see cref="IAsyncEnumerable{T}" /> which can be enumerated asynchronously.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to enumerate.
        /// </param>
        /// <returns> The query results. </returns>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is not a <see cref="IAsyncEnumerable{T}" />.
        /// </exception>
        public static IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(
            this IYahooQueryable<TSource> source)
            where TSource : IYahooCollection
        {
            if (source is IAsyncEnumerable<TSource> asyncEnumerable)
            {
                return asyncEnumerable;
            }

            throw new InvalidOperationException($"The source 'IQueryable' doesn't implement 'IAsyncEnumerable<{typeof(TSource)}>'. Only sources that implement 'IAsyncEnumerable' can be used for asynchronous operations.");
        }

        #endregion

        #region Impl.

        private static async Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            Expression expression,
            CancellationToken cancellationToken = default)
        {
            if (source.Provider is IAsyncQueryProvider provider)
            {
                if (operatorMethodInfo.IsGenericMethod)
                {
                    operatorMethodInfo
                        = operatorMethodInfo.GetGenericArguments().Length == 2
                            ? operatorMethodInfo.MakeGenericMethod(typeof(TSource), typeof(TResult).GetGenericArguments().Single())
                            : operatorMethodInfo.MakeGenericMethod(typeof(TSource));
                }

                return await provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        instance: null,
                        method: operatorMethodInfo,
                        arguments: expression == null
                            ? new[] { source.Expression }
                            : new[] { source.Expression, expression }),
                    cancellationToken);
            }

            throw new InvalidOperationException($"The source 'IQueryable' doesn't implement 'IAsyncEnumerable<{typeof(TSource)}>'. Only sources that implement 'IAsyncEnumerable' can be used for asynchronous operations.");
        }

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            LambdaExpression expression,
            CancellationToken cancellationToken = default)
            => ExecuteAsync<TSource, TResult>(
                operatorMethodInfo, source, Expression.Quote(expression), cancellationToken);

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => ExecuteAsync<TSource, TResult>(
                operatorMethodInfo, source, (Expression)null, cancellationToken);

        #endregion
    }
}
