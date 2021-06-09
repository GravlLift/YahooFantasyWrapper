using System;
using System.Linq;
using System.Text;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    internal static class ApiEndpoints
    {
        #region Const
        /// <summary>
        /// Api Url for Yahoo Fantasy Api :https://fantasysports.yahooapis.com/fantasy/v2
        /// </summary>
        private const string BaseApiUrl = "https://fantasysports.yahooapis.com/fantasy/v2";
        #endregion


        internal static EndPoint TransactionsLeagueEndPoint(
            string leagueKey,
            EndpointSubResourcesCollection subresources = null
        ) {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/league/{leagueKey}/transactions{BuildSubResourcesList(subresources)}"
            };
        }

        private static string BuildSubResourcesList(EndpointSubResourcesCollection subresources)
        {
            string subs = "";
            if (subresources != null && subresources.Resources.Count > 0)
            {
                subs = $";out={string.Join(",", subresources.Resources.Select(a => a.ToFriendlyString()))}";
            }
            return subs;
        }
    }
}
