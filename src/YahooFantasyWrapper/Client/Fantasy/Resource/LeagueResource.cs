﻿using System;
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
    /// https://developer.yahoo.com/fantasysports/guide/#league-resource
    /// When users join a Fantasy Football, Baseball, Basketball, or Hockey draft and trade game, 
    /// they are organized into leagues with a limited number of friends or other Yahoo! users, with each user managing a Team. 
    /// With the League API, you can obtain the league related information, like the league name, the number of teams, the draft status, et cetera. 
    /// Leagues only exist in the context of a particular Game, although you can request a League Resource as the base of your URI by using the global ````. 
    /// A particular user can only retrieve data for private leagues of which they are a member, or for public leagues.
    /// </summary>
    public class LeagueResourceManager : Fantasy.Manager
    {
        public LeagueResourceManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Get League Resource with Meta Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/metadata
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetMeta(string leagueKey, AuthModel auth)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.MetaData), auth, "league");
        }
        /// <summary>
        /// Get League Resource with Settings Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/settings
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetSettings(string leagueKey, AuthModel auth)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.Settings), auth, "league");
        }
        /// <summary>
        /// Get League Resource with Standings Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/standings
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetStandings(string leagueKey, AuthModel auth)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.Standings), auth, "league");
        }

        /// <summary>
        /// Get League Resource with Scoreboard Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/scoreboard
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <param name="weeks">Weeks to get the scoreboards for</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetScoreboard(string leagueKey, AuthModel auth, int[] weeks = null)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.Scoreboard, weeks), auth, "league");
        }
        /// <summary>
        /// Get League Resource with Teams Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/teams
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetTeams(string leagueKey, AuthModel auth)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.Teams), auth, "league");
        }

        /// <summary>
        /// Get League Resource with DraftResults Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/draft_results
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetDraftResults(string leagueKey, AuthModel auth)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.DraftResults), auth, "league");
        }

        /// <summary>
        /// Get League Resource with Transactions Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/league/{leagueKey}/transactions
        /// </summary>
        /// <param name="leagueKey">LeagueKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>League Resource</returns>
        public async Task<League> GetTransactions(string leagueKey, AuthModel auth)
        {
            return await Utils.GetResource<League>(client, ApiEndpoints.LeagueEndPoint(leagueKey, EndpointSubResources.Transactions), auth, "league");
        }
    }
}
