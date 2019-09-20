using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// https://developer.yahoo.com/fantasysports/guide/#players-collection
    /// With the Players API, you can obtain information from a collection of players simultaneously. 
    /// To obtains general players information, the players collection can be qualified in the URI by a particular game, league or team. 
    /// To obtain specific league or team related information, the players collection is qualified by the relevant league or team. 
    /// Each element beneath the Players Collection will be a Player Resource
    /// </summary>
    public class PlayersCollectionManager : Fantasy.Manager
    {
        public PlayersCollectionManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Gets Players Collection based on supplied Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="playerKeys">Players Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Player Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Players Collection (List of Player Resources)</returns>
        public async Task<List<Player>> GetPlayers(string[] playerKeys, EndpointSubResourcesCollection subresources, AuthModel auth)
        {
            return await Utils.GetCollection<Player>(client, ApiEndpoints.PlayersEndPoint(playerKeys, subresources), auth, "player");
        }

        /// <summary>
        /// Gets Players Collection based on supplied league Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="leagueKeys">League Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Player Resource</param>
        /// <param name="filters">Criteria to filter players on</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Players Collection (List of Player Resources)</returns>
        public async Task<List<League>> GetLeaguePlayers(string[] leagueKeys, AuthModel auth, EndpointSubResourcesCollection subresources = null, PlayerCollectionFilters filters = null)
        {
            return await Utils.GetCollection<League>(client, ApiEndpoints.PlayersLeagueEndPoint(leagueKeys, subresources, filters), auth, "league");
        }

        /// <summary>
        /// Gets Players Collection based on supplied team Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="teamKeys">Team Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Player Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Players Collection (List of Player Resources)</returns>
        public async Task<List<Team>> GetTeamPlayers(AuthModel auth, string[] teamKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            return await Utils.GetCollection<Team>(client, ApiEndpoints.PlayersTeamEndPoint(teamKeys, subresources), auth, "team");
        }
    }
}
