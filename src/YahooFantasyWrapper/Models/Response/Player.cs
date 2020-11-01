using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    [XmlRoot(ElementName = "name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Name
    {
        [XmlElement(ElementName = "full", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Full { get; set; }
        [XmlElement(ElementName = "first", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string First { get; set; }
        [XmlElement(ElementName = "last", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Last { get; set; }
        [XmlElement(ElementName = "ascii_first", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string AsciiFirstName { get; set; }
        [XmlElement(ElementName = "ascii_last", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string AsciiLastName { get; set; }

        public override string ToString() => Full;

        public static implicit operator string(Name name) { return name.ToString(); }
    }

    [XmlRoot(ElementName = "headshot", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Headshot
    {
        [XmlElement(ElementName = "url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Url { get; set; }
        [XmlElement(ElementName = "size", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Size { get; set; }
    }

    [XmlRoot(ElementName = "eligible_positions", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class EligiblePositions
    {
        [XmlElement(ElementName = "position", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<string> Positions { get; set; }
        public override string ToString() => Positions == null ? null : $"{string.Join(",", Positions)}";
    }

    [XmlRoot(ElementName = "player", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Player
    {
        [XmlElement(ElementName = "player_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PlayerKey { get; set; }
        [XmlElement(ElementName = "player_id", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PlayerId { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Name Name { get; set; }
        [XmlElement(ElementName = "editorial_player_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EditorialPlayerKey { get; set; }
        [XmlElement(ElementName = "editorial_team_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EditorialTeamKey { get; set; }
        [XmlElement(ElementName = "editorial_team_full_name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EditorialTeamFullName { get; set; }
        [XmlElement(ElementName = "editorial_team_abbr", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EditorialTeamAbbr { get; set; }
        [XmlArray(ElementName = "bye_weeks", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "bye_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<int> ByeWeeks { get; set; }
        [XmlElement(ElementName = "uniform_number", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string UniformNumber { get; set; }
        [XmlElement(ElementName = "display_position", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DisplayPosition { get; set; }
        [XmlElement(ElementName = "headshot", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Headshot Headshot { get; set; }
        [XmlElement(ElementName = "image_url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string ImageUrl { get; set; }
        [XmlElement(ElementName = "is_undroppable", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsUndroppable { get; set; }
        [XmlElement(ElementName = "position_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PositionType { get; set; }
        [XmlElement(ElementName = "eligible_positions", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public EligiblePositions EligiblePositions { get; set; }
        [XmlElement(ElementName = "status", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Status { get; set; }
        [XmlElement(ElementName = "status_full", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string StatusFull { get; set; }
        [XmlElement(ElementName = "injury_note", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string InjuryNote { get; set; }
        [XmlElement(ElementName = "has_player_notes", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string HasPlayerNotes { get; set; }
        [XmlElement(ElementName = "transaction_data")]
        public TransactionData TransactionData { get; set; }
        [XmlElement(ElementName = "selected_position")]
        public SelectedPosition SelectedPosition { get; set; }
        [XmlElement(ElementName = "player_stats", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public PlayerStats PlayerStats { get; set; }
        [XmlElement(ElementName = "ownership", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public PlayerOwnership Ownership { get; set; }

    }

    [XmlRoot(ElementName = "ownership", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class PlayerOwnership
    {
        [XmlElement(ElementName = "ownership_type")]
        public PlayerOwnershipType OwnershipType { get; set; }
        [XmlElement(ElementName = "owner_team_key")]
        public string OwnerTeamKey { get; set; }
        [XmlElement(ElementName = "owner_team_name")]
        public string OwnerTeamName { get; set; }
        [XmlArray(ElementName = "teams")]
        [XmlArrayItem(ElementName = "team")]
        public List<Team> Teams { get; set; }
    }

    [XmlRoot(ElementName = "player_stats", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class PlayerStats
    {
        [XmlElement(ElementName = "coverage_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string CoverageType { get; set; }
        [XmlElement(ElementName = "season", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Season { get; set; }
        [XmlArray(ElementName = "stats", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "stat", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Stat> Stats { get; set; }
    }


    public enum PlayerOwnershipType
    {
        [XmlEnum(Name = "team")]
        Team,
        [XmlEnum(Name = "waivers")]
        Waivers,
        [XmlEnum(Name = "freeagents")]
        [Display(Name = "Free Agent")]
        FreeAgent
    }

    public class PlayerOwnershipStatus : StringEnum
    {
        private PlayerOwnershipStatus(string value) : base(value) { }

        public static PlayerOwnershipStatus Available => new PlayerOwnershipStatus("A");
        public static PlayerOwnershipStatus FreeAgent => new PlayerOwnershipStatus("FA");
        public static PlayerOwnershipStatus Waivers => new PlayerOwnershipStatus("W");
        public static PlayerOwnershipStatus Taken => new PlayerOwnershipStatus("T");
        public static PlayerOwnershipStatus Keepers => new PlayerOwnershipStatus("K");
    }

    public class PlayerSortType : StringEnum
    {
        private PlayerSortType(string value) : base(value) { }

        public static PlayerSortType Season => new PlayerSortType("season");
        public static PlayerSortType Week => new PlayerSortType("week");
        public static PlayerSortType LastMonth => new PlayerSortType("lastmonth");
    }
}
