using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// https://developer.yahoo.com/fantasysports/guide/#games-collection
    /// With the Games API, you can obtain information from a collection of games simultaneously. 
    /// Each element beneath the Games Collection will be a Game Resource
    /// </summary>
    public class GameCollectionsManager : Fantasy.Manager
    {
        public GameCollectionsManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Gets Games Collection based on supplied Keys
        /// Attaches Requested SubResources
        /// Applies Filters if included
        /// </summary>
        /// <param name="gameKey">Game Key to return Resources for </param>
        /// <param name="auth">Token for request</param>
        /// <returns>Games Collection (List of Game Resources)</returns>
        public async Task<List<Game>> GetGames(string gameKey, AuthModel auth)
        {
            return await Utils.GetCollection<Game>(client, ApiEndpoints.GameEndPoint(gameKey), auth, "game");
        }

        /// <summary>
        /// Gets Games Collection based on supplied Keys
        /// Attaches Requested SubResources
        /// Applies Filters if included
        /// </summary>
        /// <param name="gameKeys">Game Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Game Resource</param>
        /// <param name="auth">Token for request</param>
        /// <param name="filters">Custom Filter Object on Game</param>
        /// <returns>Games Collection (List of Game Resources)</returns>
        public async Task<List<Game>> GetGames(string[] gameKeys, AuthModel auth, EndpointSubResourcesCollection subresources = null, GameCollectionFilters filters = null)
        {
            return await Utils.GetCollection<Game>(client, ApiEndpoints.GamesEndPoint(gameKeys, subresources, filters), auth, "game");
        }

        /// <summary>
        /// Gets Games Collection based on supplied Keys for logged in user
        /// Attaches Requested SubResources
        /// Applies Filters if included
        /// </summary>
        /// <param name="gameKeys">Game Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Game Resource</param>
        /// <param name="auth">Token for request</param>
        /// <param name="filters">Custom Filter Object on Game</param>
        /// <returns>Games Collection (List of Game Resources)</returns>
        public async Task<List<Game>> GetGamesUsers(AuthModel auth, string[] gameKeys = null, EndpointSubResourcesCollection subresources = null, GameCollectionFilters filters = null)
        {
            return await Utils.GetCollection<Game>(client, ApiEndpoints.UserGamesEndPoint(gameKeys, subresources, filters), auth, "game");
        }
    }
}
