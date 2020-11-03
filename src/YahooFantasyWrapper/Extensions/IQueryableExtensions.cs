using System.Linq;
using System.Linq.Expressions;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models.Response;

namespace System.Collections.Generic
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static class IQueryableExtensions
    {
        public static IYahooCollection<TYahooEntity, Player> Filter<TYahooEntity>(this IYahooCollection<TYahooEntity, Player> query,
            PlayerCollectionFilters filters)
            where TYahooEntity : IYahooEntity
            => throw new NotImplementedException();

        public static IYahooCollection<TYahooEntity, TYahooSubResource> SubResource<TYahooEntity, TYahooSubResource>(
            this TYahooEntity yahooResource,
            Expression<Func<TYahooEntity, TYahooSubResource>> expression)
            where TYahooEntity : IYahooEntity
            where TYahooSubResource : IYahooResource
            => throw new NotImplementedException();


        public static IYahooCollection<TYahooEntity, TYahooSubCollection> SubResource<TYahooEntity, TYahooSubCollection>(
            this TYahooEntity yahooResource,
            Expression<Func<TYahooEntity, IEnumerable<TYahooSubCollection>>> expression)
            where TYahooEntity : IYahooEntity
            where TYahooSubCollection : IYahooCollection
            => throw new NotImplementedException();

    }
#pragma warning restore IDE0060 // Remove unused parameter
}
