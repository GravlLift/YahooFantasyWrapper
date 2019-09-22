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
    /// https://developer.yahoo.com/fantasysports/guide/#user-resource
    /// With the User API, you can retrieve fantasy information for a particular Yahoo! user. Most usefully, you can see which games a user is playing, 
    /// and which leagues they belong to and teams that they own within those games. 
    /// Because you can currently only view user information for the logged in user, you would generally want to use the Users collection, 
    /// passing along the use_login flag, instead of trying to request a User resource directly from the URI.
    /// </summary>
    public class UserResourceManager : Fantasy.Manager
    {
        public UserResourceManager(HttpClient client) : base(client)
        {
        }
        #region Users
        /// <summary>
        /// Get Team Resource with Meta Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/users;use_login=1
        /// </summary>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <returns>User Resource</returns>
        public async Task<User> GetUser(AuthModel auth)
        {
            return await Utils.GetResource<User>(client, ApiEndpoints.UserGamesEndPoint(), auth, "user");
        }

        /// <summary>
        /// Get User Resource with Game/League Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/users;use_login=1/games{gameKeys}/leagues
        /// </summary>
        /// <param name="gameKeys">Game Keys to get League Resource for</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <param name="subresources">SubResources to include with League Resource</param>
        /// <returns>User Resource</returns>
        public async Task<User> GetUserGameLeagues(AuthModel auth, string[] gameKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            return await Utils.GetResource<User>(client, ApiEndpoints.UserGameLeaguesEndPoint(gameKeys, subresources), auth, "user");
        }

        /// <summary>
        /// Get User Resource with Game/Teams Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/users;use_login=1/games{gameKeys}/leagues
        /// </summary>
        /// <param name="gameKeys">Game Keys to get Team Resource for</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <param name="subresources">SubResources to include with League Resource</param>
        /// <returns>User Resource</returns>
        public async Task<User> GetUserGamesTeamsEndPoint(AuthModel auth, string[] gameKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            return await Utils.GetResource<User>(client, ApiEndpoints.UserGamesTeamsEndPoint(gameKeys, subresources), auth, "user");
        }

        #endregion
    }
}
