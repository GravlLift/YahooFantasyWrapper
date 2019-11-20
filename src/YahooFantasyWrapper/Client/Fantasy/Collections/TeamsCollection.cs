using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// https://developer.yahoo.com/fantasysports/guide/#teams-collection
    /// With the Teams API, you can obtain information from a collection of teams simultaneously. 
    /// The teams collection is qualified in the URI by a particular league to obtain information about teams within the league, 
    /// or by a particular user (and optionally, a game) to obtain information about the teams owned by the user.
    /// Each element beneath the Teams Collection will be a Team Resource
    /// </summary>
    public class TeamsCollectionManager : Fantasy.Manager
    {
        public TeamsCollectionManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Gets Teams Collection based on supplied Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="teamKeys">Teams Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Team Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Team Collection (List of Team Resources)</returns>
        public async Task<List<Team>> GetTeams(string[] teamKeys, EndpointSubResourcesCollection subresources, AuthModel auth)
        {
            return await Utils.GetCollection<Team>(client, ApiEndpoints.TeamsEndPoint(teamKeys, subresources), auth, "team");
        }

        /// <summary>
        /// Gets Teams Collection based on supplied league Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="leagueKeys">League Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Team Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Team Collection (List of Team Resources)</returns>
        public async Task<List<League>> GetLeagueTeams(AuthModel auth, string[] leagueKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            return await Utils.GetCollection<League>(client, ApiEndpoints.TeamsLeagueEndPoint(leagueKeys, subresources), auth, "league");
        }

        /// <summary>
        /// Gets Teams Collection based on supplied league Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="leagueKeys">League Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Team Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Team Collection (List of Team Resources)</returns>
        public async Task<List<League>> GetLeagueRosters(AuthModel auth, string[] leagueKeys = null, int[] weeks = null, EndpointSubResourcesCollection subresources = null)
        {
            return await Utils.GetCollection<League>(client, ApiEndpoints.RosterLeagueEndPoint(leagueKeys, subresources, weeks), auth, "league");
        }

        /// <summary>
        /// Gets Teams Collection based on supplied game Keys for logged in user
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="leagueKeys">League Keys to return resources for </param>
        /// <param name="subresources">SubResources to include with Team Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Team Collection (List of Team Resources)</returns>
        public async Task<List<Team>> GetUserTeams(AuthModel auth, string[] leagueKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            if (leagueKeys != null)
                return await Utils.GetCollection<Team>(
                    client,
                    ApiEndpoints.UserLeaguesEndPoint(leagueKeys, EndpointSubResourcesCollection.BuildResourceList(EndpointSubResources.Teams)),
                    auth,
                    "team");
            else
                return await Utils.GetCollection<Team>(
                    client,
                    ApiEndpoints.UserTeamsEndPoint(subresources),
                    auth,
                    "team");
        }
    }
}
