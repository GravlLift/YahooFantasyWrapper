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
    /// https://developer.yahoo.com/fantasysports/guide/#player-resource
    /// With the Player API, you can obtain the player (athlete) related information, such as their name, professional team, and eligible positions. 
    /// The player is identified in the context of a particular game, and can be requested as the base of your URI by using the global ````.
    /// </summary>
    public class PlayerResourceManager : Fantasy.Manager
    {
        public PlayerResourceManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Get Player Resource with Meta Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/player/{playerKey}/metadata
        /// </summary>
        /// <param name="playerKey">Player Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Player Resource</returns>
        public async Task<Player> GetMeta(string playerKey, AuthModel auth)
        {
            return await Utils.GetResource<Player>(client, ApiEndpoints.PlayerEndPoint(playerKey, EndpointSubResources.MetaData), auth, "player");
        }

        /// <summary>
        /// Get Player Resource with Stats Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/player/{playerKey}/stats
        /// </summary>
        /// <param name="playerKey">Player Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Player Resource</returns>
        public async Task<Player> GetStats(string playerKey, AuthModel auth)
        {
            return await Utils.GetResource<Player>(client, ApiEndpoints.PlayerEndPoint(playerKey, EndpointSubResources.Stats), auth, "game");
        }

        /// <summary>
        /// Get Player Resource with Ownership Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/player/{playerKey}/ownership
        /// </summary>
        /// <param name="playerKeys"></param>
        /// <param name="leagueKeys"></param>
        /// <param name="playerKey">Player Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Player Resource</returns>
        public async Task<Player> GetOwnership(string[] playerKeys, string leagueKeys, AuthModel auth)
        {
            return await Utils.GetResource<Player>(client, ApiEndpoints.PlayerOwnershipEndPoint(playerKeys, leagueKeys), auth, "player");
        }

        /// <summary>
        /// Get Player Resource with Percent Owned Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/player/{playerKey}/precent_owned
        /// </summary>
        /// <param name="playerKey">Player Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Player Resource</returns>
        public async Task<Player> GetPercentOwned(string playerKey, AuthModel auth)
        {
            return await Utils.GetResource<Player>(client, ApiEndpoints.PlayerEndPoint(playerKey, EndpointSubResources.PercentOwned), auth, "game");
        }

        /// <summary>
        /// Get Player Resource with Draft Analysis Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/player/{playerKey}/draft_analysis
        /// </summary>
        /// <param name="playerKey">Player Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Player Resource</returns>
        public async Task<Player> GetDraftAnalysis(string playerKey, AuthModel auth)
        {
            return await Utils.GetResource<Player>(client, ApiEndpoints.PlayerEndPoint(playerKey, EndpointSubResources.DraftAnalysis), auth, "game");
        }
    }
}
