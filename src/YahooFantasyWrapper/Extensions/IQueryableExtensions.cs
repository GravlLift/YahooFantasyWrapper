using System.Linq;
using System.Linq.Expressions;
using YahooFantasyWrapper.Models.Response;

namespace System.Collections.Generic
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TYahooSubResource> SubResource<TYahooEntity, TYahooSubResource>(
            this TYahooEntity yahooResource,
            Expression<Func<TYahooEntity, TYahooSubResource>> expression)
            where TYahooEntity : IYahooEntity
            where TYahooSubResource : IYahooResource
        {
            throw new NotImplementedException();
        }

        public static IQueryable<TYahooSubCollection> SubResource<TYahooEntity, TYahooSubCollection>(
            this TYahooEntity yahooResource,
            Expression<Func<TYahooEntity, IEnumerable<TYahooSubCollection>>> expression)
            where TYahooEntity : IYahooEntity
            where TYahooSubCollection : IYahooCollection
        {
            throw new NotImplementedException();
        }
    }
}
