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
using YahooFantasyWrapper.Models.Request;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// https://developer.yahoo.com/fantasysports/guide/#transactions-collection
    /// With the Transactions API, you can obtain information via GET from a collection of transactions simultaneously. 
    /// The transactions collection is qualified in the URI by a particular league.
    /// Each element beneath the Transactions Collection will be a Transaction Resource
    /// </summary>
    public class TransactionsCollectionManager
    {
        private readonly HttpClient client;

        public TransactionsCollectionManager(HttpClient client)
        {
            this.client = client;
        }
        /// <summary>
        /// Add/Drops Players
        /// </summary>
        /// <returns></returns>
        public async Task AddPlayerAsync(
            AuthModel auth,
            string leagueKey,
            SinglePlayerTransaction transaction
        ) {
            await Utils.PostCollection(
                client,
                ApiEndpoints.TransactionsLeagueEndPoint(leagueKey),
                auth,
                transaction
            );
        }
        /// <summary>
        /// Add/Drops Players
        /// </summary>
        /// <returns></returns>
        public async Task AddAndDropPlayerAsync(
            AuthModel auth,
            string leagueKey,
            MultiPlayerTransaction transaction
        ) {
            await Utils.PostCollection(
                client,
                ApiEndpoints.TransactionsLeagueEndPoint(leagueKey),
                auth,
                transaction
            );
        }
    }
}
