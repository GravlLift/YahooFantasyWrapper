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
    /// https://developer.yahoo.com/fantasysports/guide/#game-resource
    /// With the Game API, you can obtain the fantasy game related information, like the fantasy game name, the Yahoo! game code, and season.
    /// </summary>
    public class GameResourceManager : Fantasy.Manager
    {
        public GameResourceManager(HttpClient client) : base(client) { }

        /// <summary>
        /// Get Game Resource with Meta Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/metadata
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetMeta(string gameKey, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GameEndPoint(gameKey, EndpointSubResources.MetaData), auth, "game");
        }
        /// <summary>
        /// Get Game Resource with Leagues Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/leagues
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="leagueKeys"></param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetLeagues(string gameKey, string[] leagueKeys, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GameLeaguesEndPoint(gameKey, leagueKeys), auth, "game");
        }
        /// <summary>
        /// Get Game Resource with Players Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/players
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="playerKeys"></param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetPlayers(string gameKey, string[] playerKeys, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GamePlayersEndPoint(gameKey, playerKeys), auth, "game");
        }
        /// <summary>
        /// Get Game Resource with GameWeeks Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/game_weeks
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetGameWeeks(string gameKey, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GameEndPoint(gameKey, EndpointSubResources.GameWeeks), auth, "game");
        }
        /// <summary>
        /// Get Game Resource with Stat Categories Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/stat_categories
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetStatCategories(string gameKey, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GameEndPoint(gameKey, EndpointSubResources.StatCategories), auth, "game");
        }
        /// <summary>
        /// Get Game Resource with Position Types Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/position_types
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetPositionTypes(string gameKey, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GameEndPoint(gameKey, EndpointSubResources.PositionTypes), auth, "game");
        }
        /// <summary>
        /// Get Game Resource with Roster Positions Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/game/{gameKey}/roster_positions
        /// </summary>
        /// <param name="gameKey">GameKey to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>Game Resource</returns>
        public async Task<Game> GetRosterPositions(string gameKey, AuthModel auth)
        {
            return await Utils.GetResource<Game>(client, ApiEndpoints.GameEndPoint(gameKey, EndpointSubResources.RosterPositions), auth, "game");
        }
    }
}
