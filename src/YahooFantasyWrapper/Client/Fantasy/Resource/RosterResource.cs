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
    /// https://developer.yahoo.com/fantasysports/guide/#roster-resource
    /// Players on a team are organized into rosters corresponding to certain weeks, in NFL, or certain dates, in MLB, NBA, and NHL. 
    /// Each player on a roster will be assigned a position if they’re in the starting lineup, or will be on the bench. 
    /// You can only receive credit for stats accumulated by players in your starting lineup.
    /// 
    /// You can use this API to edit your lineup by PUTting up new positions for the players on a roster. 
    /// You can also add/drop players from your roster by `POSTing new transactions to the league’s transactions collection.
    /// </summary>
    public class RosterResourceManager : Fantasy.Manager
    {
        public RosterResourceManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Get Roster Resource with Player Subresource
        /// https://fantasysports.yahooapis.com/fantasy/v2/team/{teamKey}/roster/players
        /// </summary>
        /// <param name="teamKey">Team Key to Query</param>
        /// <param name="auth">Access Token from Auth Api</param>
        /// <param name="week">Optional Week to get Roster at particular time</param>
        /// <param name="date">Optional Date to get Roster at particular time</param>
        /// <returns>Player Resource</returns>
        public async Task<Roster> GetPlayers(string teamKey, int? week, DateTime? date, AuthModel auth)
        {
            return await Utils.GetResource<Roster>(client, ApiEndpoints.RosterEndPoint(teamKey, week, date), auth, "roster");
        }
    }
}
