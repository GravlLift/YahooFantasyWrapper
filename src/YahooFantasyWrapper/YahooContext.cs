using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper
{
    public class YahooContext
    {
        public YahooSet<Game> Games { get; set; } = new YahooSet<Game>();
        public YahooSet<League> Leagues { get; set; } = new YahooSet<League>();
        public YahooSet<Team> Teams { get; set; } = new YahooSet<Team>();
    }
}
