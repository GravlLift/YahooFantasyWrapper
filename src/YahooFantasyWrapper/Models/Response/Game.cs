using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    [XmlRoot(ElementName = "game_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class GameWeek
    {
        [XmlElement(ElementName = "week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Week { get; set; }
        [XmlElement(ElementName = "start", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Start { get; set; }
        [XmlElement(ElementName = "end", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string End { get; set; }
    }

    [XmlRoot(ElementName = "manager", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Manager : IYahooCollection
    {
        [XmlElement(ElementName = "manager_id", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Manager_id { get; set; }
        [XmlElement(ElementName = "nickname", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Nickname { get; set; }
        [XmlElement(ElementName = "guid", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Guid { get; set; }
        [XmlElement(ElementName = "is_commissioner", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsCommissioner { get; set; }
        [XmlElement(ElementName = "is_current_login", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsCurrentLogin { get; set; }
        [XmlElement(ElementName = "email", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Email { get; set; }
        [XmlElement(ElementName = "image_url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string ImageUrl { get; set; }
    }

    [XmlRoot(ElementName = "game", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Game : IYahooCollection
    {
        [XmlElement(ElementName = "game_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string GameKey { get; set; }
        [XmlElement(ElementName = "game_id", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int GameId { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Name { get; set; }
        [XmlElement(ElementName = "code", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Code { get; set; }
        [XmlElement(ElementName = "type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Type { get; set; }
        [XmlElement(ElementName = "url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Url { get; set; }
        [XmlElement(ElementName = "season", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Season { get; set; }
        [XmlElement(ElementName = "is_registration_over", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsRegistrationOver { get; set; }
        [XmlElement(ElementName = "is_game_over", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsGameOver { get; set; }
        [XmlElement(ElementName = "is_offseason", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsOffseason { get; set; }
        [XmlArray(ElementName = "game_weeks", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "game_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<GameWeek> GameWeeks { get; set; }
        [XmlArray(ElementName = "leagues", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "league", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<League> Leagues { get; set; }
        [XmlArray(ElementName = "players", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "player", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Player> Players { get; set; }
        [XmlArray(ElementName = "stat_categories", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "stat", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Stat> StatCategories { get; set; }
        [XmlArray(ElementName = "position_types", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "position_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<PositionType> PositionTypes { get; set; }
        [XmlArray(ElementName = "roster_positions", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "roster_position", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<RosterPosition> RosterPositions { get; set; }
        [XmlArray(ElementName = "teams", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "team", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Team> Teams { get; set; }
    }

    [XmlRoot(ElementName = "user", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class User : IYahooCollection
    {
        [XmlElement(ElementName = "guid", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Guid { get; set; }
        [XmlArray(ElementName = "games", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "game", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Game> Games { get; set; }
        [XmlArray(ElementName = "teams", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "team", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Team> Teams { get; set; }
    }

    public enum GameCode
    {
        [XmlEnum(Name = "nfl")]
        nfl = 0,
        [XmlEnum(Name = "pnfl")]
        pnfl = 1,
        [XmlEnum(Name = "mlb")]
        mlb = 2,
        [XmlEnum(Name = "pmlb")]
        pmlb = 3,
        [XmlEnum(Name = "nba")]
        nba = 4,
        [XmlEnum(Name = "nhl")]
        nhl = 5,
        [XmlEnum(Name = "yahoops")]
        yahoops = 6,
        [XmlEnum(Name = "nflp")]
        nflp = 7
    }
}
