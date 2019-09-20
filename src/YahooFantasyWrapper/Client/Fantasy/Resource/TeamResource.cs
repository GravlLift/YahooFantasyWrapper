using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// https://developer.yahoo.com/fantasysports/guide/#team-resource
    /// The Team APIs allow you to retrieve information about a team within our fantasy games. The team is the basic unit for keeping track of a roster of players, 
    /// and can be managed by either one or two managers (the second manager being called a co-manager). With the Team APIs, you can obtain team-related information, 
    /// like the team name, managers, logos, stats and points, and rosters for particular weeks. Teams only exist in the context of a particular League, 
    /// although you can request a Team Resource as the base of your URI by using the global ````. 
    /// A particular user can only retrieve data about a team if that team is part of a private league of which the user is a member, or if it’s in a public league.
    /// </summary>
    public class TeamResourceManager : Fantasy.Manager
    {
        public TeamResourceManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Get Team Resource with Meta Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/metadata
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Team Resource</returns>
        public async Task<Team> GetMeta(string teamKey, AuthModel auth)
        {
            return await Utils.GetResource<Team>(client, ApiEndpoints.TeamEndPoint(teamKey, EndpointSubResources.MetaData), auth, "game");
        }

        /// <summary>
        /// Get Team Resource with Stats Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/stats
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Team Resource</returns>
        public async Task<Team> GetStats(string teamKey, AuthModel auth)
        {
            return await Utils.GetResource<Team>(client, ApiEndpoints.TeamEndPoint(teamKey, EndpointSubResources.Stats), auth, "game");
        }

        /// <summary>
        /// Get Team Resource with Standings Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/standings
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Team Resource</returns>
        public async Task<Team> GetStandings(string teamKey, AuthModel auth)
        {
            return await Utils.GetResource<Team>(client, ApiEndpoints.TeamEndPoint(teamKey, EndpointSubResources.Standings), auth, "game");
        }

        /// <summary>
        /// Get Team Resource with Roster Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/roster
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Team Resource</returns>
        public async Task<Team> GetRoster(string teamKey, AuthModel auth)
        {
            return await Utils.GetResource<Team>(client, ApiEndpoints.TeamEndPoint(teamKey, EndpointSubResources.Roster), auth, "game");
        }

        /// <summary>
        /// Get Team Resource with Draft Results Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/draft_results
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Team Resource</returns>
        public async Task<Team> GetDraftResults(string teamKey, AuthModel auth)
        {
            return await Utils.GetResource<Team>(client, ApiEndpoints.TeamEndPoint(teamKey, EndpointSubResources.DraftResults), auth, "game");
        }
        /// <summary>
        /// Get Team Resource with Matchups Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/matchups
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Team Resource</returns>
        public async Task<Team> GetMatchups(string teamKey, AuthModel auth)
        {
            return await Utils.GetResource<Team>(client, ApiEndpoints.TeamEndPoint(teamKey, EndpointSubResources.Matchups), auth, "game");
        }
    }
}
