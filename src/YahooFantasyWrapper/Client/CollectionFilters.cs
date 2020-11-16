using System;
using System.Collections.Generic;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class YahooFilterAttribute : Attribute
    {
        public string Key { get; set; }
    }

    public class MatchupCollectionFilters
    {
        public IEnumerable<int> Weeks { get; set; }
    }

    public class RosterCollectionFilters
    {
        public int? Week { get; set; }
    }

    /// <summary>
    /// Filter Object for Game Collection
    /// </summary>
    public class GameCollectionFilters
    {
        [YahooFilter(Key = "is_available")]
        public bool? IsAvailable { get; set; }
        public IEnumerable<int> Seasons { get; set; }
        [YahooFilter(Key = "game_codes")]
        public IEnumerable<GameCode> GameCodes { get; set; }
        [YahooFilter(Key = "game_types")]
        public IEnumerable<GameType> GameTypes { get; set; }
    }

    /// <summary>
    /// Filter Object for Player Collection
    /// </summary>
    public class PlayerCollectionFilters
    {
        [YahooFilter(Key = "player_keys")]
        public IEnumerable<string> PlayerKeys { get; set; }
        public IEnumerable<FantasyPosition> Position { get; set; }
        public IEnumerable<PlayerOwnershipStatus> Status { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        [YahooFilter(Key = "sort_type")]
        public PlayerSortType SortType { get; set; }
        [YahooFilter(Key = "sort_season")]
        public int? SortSeason { get; set; }
        [YahooFilter(Key = "sort_week")]
        public int? SortWeek { get; set; }
        public int? Start { get; set; }
        public int? Count { get; set; }
    }

    public class LeagueCollectionFilters
    {
        [YahooFilter(Key = "is_active")]
        public bool? IsActive { get; set; }
    }

    public class UserCollectionFilters
    {
        [YahooFilter(Key = "use_login")]
        public bool? UseLogin { get; set; }
    }

    public class TeamCollectionFilters
    {
        [YahooFilter(Key = "use_login")]
        public bool? UseLogin { get; set; }
    }
}
