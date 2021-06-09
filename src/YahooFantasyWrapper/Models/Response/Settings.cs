using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    [XmlRoot(
        ElementName = "settings",
        Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Settings : IYahooResource
    {
        [XmlElement(
            ElementName = "draft_type",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DraftType { get; set; }
        [XmlElement(
            ElementName = "is_auction_draft",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsAuctionDraft { get; set; }
        [XmlElement(
            ElementName = "scoring_type",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string ScoringType { get; set; }
        [XmlElement(
            ElementName = "persistent_url",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PersistentUrl { get; set; }
        [XmlElement(
            ElementName = "uses_playoff",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool UsesPlayoff { get; set; }
        [XmlElement(
            ElementName = "has_playoff_consolation_games",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool HasPlayoffConsolationGames { get; set; }
        [XmlElement(
            ElementName = "playoff_start_week",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int PlayoffStartWeek { get; set; }
        [XmlElement(
            ElementName = "uses_playoff_reseeding",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool UsesPlayoffReseeding { get; set; }
        [XmlElement(
            ElementName = "uses_lock_eliminated_teams",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool UsesLockEliminatedTeams { get; set; }
        [XmlElement(
            ElementName = "num_playoff_teams",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int NumPlayoffTeams { get; set; }
        [XmlElement(
            ElementName = "num_playoff_consolation_teams",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int NumPlayoffConsolationTeams { get; set; }
        [XmlElement(
            ElementName = "has_multiweek_championship",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool HasMultiweekChampionship { get; set; }
        [XmlElement(
            ElementName = "waiver_type",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string WaiverType { get; set; }
        [XmlElement(
            ElementName = "waiver_rule",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string WaiverRule { get; set; }
        [XmlElement(
            ElementName = "uses_faab",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool UsesFaab { get; set; }
        [XmlElement(
            ElementName = "draft_time",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DraftTime { get; set; }
        [XmlElement(
            ElementName = "draft_pick_time",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DraftPickTime { get; set; }
        [XmlElement(
            ElementName = "post_draft_players",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PostDraftPlayers { get; set; }
        [XmlElement(
            ElementName = "max_teams",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int? MaxTeams { get; set; }
        [XmlElement(
            ElementName = "waiver_time",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int WaiverTime { get; set; }
        [XmlElement(
            ElementName = "trade_end_date",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public DateTime TradeEndDate { get; set; }
        [XmlElement(
            ElementName = "trade_ratify_type",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string TradeRatifyType { get; set; }
        [XmlElement(
            ElementName = "trade_reject_time",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string TradeRejectTime { get; set; }
        [XmlElement(
            ElementName = "player_pool",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PlayerPool { get; set; }
        [XmlElement(
            ElementName = "cant_cut_list",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string CantCutList { get; set; }
        [XmlElement(
            ElementName = "is_publicly_viewable",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsPubliclyViewable { get; set; }
        [XmlElement(
            ElementName = "can_trade_draft_picks",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool CanTradeDraftPicks { get; set; }
        [XmlArray(
            ElementName = "roster_positions",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(
            ElementName = "roster_position",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<RosterPosition> RosterPositions { get; set; }
        [XmlElement(
            ElementName = "stat_categories",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public StatsEnumerable StatCategories { get; set; }
        [XmlElement(
            ElementName = "stat_modifiers",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public StatsEnumerable StatModifiers { get; set; }
        [XmlElement(
            ElementName = "pickem_enabled",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool PickemEnabled { get; set; }
        [XmlElement(
            ElementName = "uses_fractional_points",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool UsesFractionalPoints { get; set; }
        [XmlElement(
            ElementName = "uses_negative_points",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool UsesNegativePoints { get; set; }

        public List<Stat> GetModifiedStats()
        {
            return StatCategories.Stats.Select(
                    stat =>
                    {
                        var modifier = StatModifiers.Stats.First(s => s.StatId == stat.StatId);
                        stat.ValueText = modifier.ValueText;
                        return stat;
                    }
                )
                .ToList();
        }
        public float GetModfiedStatValue(int id)
        {
            var stat = StatCategories.Stats.First(s => s.StatId == id);
            var modifier = StatModifiers.Stats.First(s => s.StatId == stat.StatId);
            return modifier.Value.Value;
        }
    }

    public class StatsEnumerable
    {
        [XmlArray(
            ElementName = "stats",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(
            ElementName = "stat",
            Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Stat> Stats { get; set; }
    }
}
