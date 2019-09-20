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

        public GameResourceManager GameResourceManager => new GameResourceManager(client);

        public GameCollectionsManager GameCollectionsManager => new GameCollectionsManager(client);

        public UserResourceManager UserResourceManager => new UserResourceManager(client);

        public LeagueResourceManager LeagueResourceManager => new LeagueResourceManager(client);

        public LeaguesCollectionManager LeaguesCollectionManager => new LeaguesCollectionManager(client);

        public PlayerResourceManager PlayerResourceManager => new PlayerResourceManager(client);

        public PlayersCollectionManager PlayersCollectionManager => new PlayersCollectionManager(client);

        public RosterResourceManager RosterResourceManager => new RosterResourceManager(client);

        public TeamResourceManager TeamResourceManager => new TeamResourceManager(client);

        public TeamsCollectionManager TeamsCollectionManager => new TeamsCollectionManager(client);

        public TransactionResourceManager TransactionResourceManager => new TransactionResourceManager(client);

        public TransactionsCollectionManager TransactionsCollectionManager => new TransactionsCollectionManager(client);
    }
}