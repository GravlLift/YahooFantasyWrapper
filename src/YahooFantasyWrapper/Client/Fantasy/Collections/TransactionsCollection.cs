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
    public class TransactionsCollectionManager : Fantasy.Manager
    {
        public TransactionsCollectionManager(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Gets Transactions Collection based on supplied Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="transactionKeys">Transaction Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Transaction Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Transaction Collection (List of Transaction Resources)</returns>
        public async Task<List<Models.Response.Transaction>> GetTransactions(string[] transactionKeys, EndpointSubResourcesCollection subresources, AuthModel auth)
        {
            return await Utils.GetCollection<Models.Response.Transaction>(client, ApiEndpoints.TransactionsEndPoint(transactionKeys, subresources), auth, "transaction");
        }

        /// <summary>
        /// Gets Transactions Collection based on supplied league Keys
        /// Attaches Requested SubResources
        /// </summary>
        /// <param name="leagueKey">League Keys to return Resources for </param>
        /// <param name="subresources">SubResources to include with Transaction Resource</param>
        /// <param name="auth">Token for request</param>
        /// <returns>Transaction Collection (List of Transaction Resources)</returns>
        public async Task<List<Models.Response.Transaction>> GetTransactionsLeagues(string leagueKey, EndpointSubResourcesCollection subresources, AuthModel auth)
        {
            return await Utils.GetCollection<Models.Response.Transaction>(client, ApiEndpoints.TransactionsLeagueEndPoint(leagueKey, subresources), auth, "transaction");
        }
        /// <summary>
        /// Add/Drops Players
        /// </summary>
        /// <returns></returns>
        public async Task AddOrDropPlayerAsync(AuthModel auth, string leagueKey, MultiPlayerTransaction transaction)
        {
            await Utils.PostCollection(client, ApiEndpoints.TransactionsLeagueEndPoint(leagueKey), auth, transaction);
        }
    }
}
