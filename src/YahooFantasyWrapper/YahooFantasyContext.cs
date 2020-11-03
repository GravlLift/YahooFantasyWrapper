﻿using System.Net.Http;
using YahooFantasyWrapper.Models.Response;
using YahooFantasyWrapper.Query.Internal;

namespace YahooFantasyWrapper
{
    public class YahooFantasyContext
    {
        public YahooSet<Game> Games { get; private set; }
        public YahooSet<League> Leagues { get; private set; }
        public YahooSet<Team> Teams { get; private set; }
        public YahooSet<Player> Players { get; private set; }

        public YahooFantasyContext(YahooQueryProvider provider)
        {
            Games = new YahooSet<Game>(provider);
            Leagues = new YahooSet<League>(provider);
            Teams = new YahooSet<Team>(provider);
            Players = new YahooSet<Player>(provider);
        }
    }
}