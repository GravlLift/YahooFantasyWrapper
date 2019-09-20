using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// https://developer.yahoo.com/fantasysports/guide/#leagues-collection
    /// With the Leagues API, you can obtain information from a collection of leagues simultaneously.
    /// Each element beneath the Leagues Collection will be a League Resource
    /// </summary>
    public class LeaguesCollectionManager : Fantasy.Manager
    {
        public LeaguesCollectionManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Gets Leagues Collection based on supplied Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="leagueKeys">League Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with League Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Leagues Collection (List of League Resources)</returns>
        public async Task<List<League>> GetLeagues(AuthModel auth, string[] leagueKeys, EndpointSubResourcesCollection subresources = null)
        {
            return await Utils.GetCollection<League>(client, ApiEndpoints.LeaguesEndPoint(leagueKeys, subresources), auth, "league");
        }
    }
}
