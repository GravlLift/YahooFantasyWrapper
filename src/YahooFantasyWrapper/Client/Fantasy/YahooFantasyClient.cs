using System.Threading.Tasks;
using System.Net;
using YahooFantasyWrapper.Models;
using System.Reflection;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Infrastructure;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;


namespace YahooFantasyWrapper.Client
{
    public class YahooFantasyClient : IYahooFantasyClient
    {
        private readonly HttpClient client;

        public YahooFantasyClient(HttpClient client)
        {
            this.client = client;
        }

        public TransactionsCollectionManager TransactionsCollectionManager => new TransactionsCollectionManager(client);
    }
}
