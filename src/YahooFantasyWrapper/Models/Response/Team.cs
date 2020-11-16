using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    [XmlRoot(ElementName = "team_logo")]
    public class TeamLogo
    {
        [XmlElement(ElementName = "size")]
        public string Size { get; set; }
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }

    [XmlRoot(ElementName = "team_logos")]
    public class TeamLogos
    {
        [XmlElement(ElementName = "team_logo")]
        public TeamLogo TeamLogo { get; set; }
        [XmlElement(ElementName = "size", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Size { get; set; }
        [XmlElement(ElementName = "url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Url { get; set; }
    }

    [XmlRoot(ElementName = "roster_adds")]
    public class RosterAdds
    {
        [XmlElement(ElementName = "coverage_type")]
        public string CoverageType { get; set; }
        [XmlElement(ElementName = "coverage_value")]
        public string CoverageValue { get; set; }
        [XmlElement(ElementName = "value")]
        public string Value { get; set; }
    }

    public abstract class TeamBase : IYahooCollection, IComparable
    {
        [XmlElement(ElementName = "team_key")]
        public string TeamKey { get; set; }
        [XmlElement(ElementName = "team_id")]
        public string TeamId { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "is_owned_by_current_login")]
        public bool IsOwnedByCurrentLogin { get; set; }
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "team_logos")]
        public TeamLogos TeamLogos { get; set; }
        [XmlElement(ElementName = "waiver_priority")]
        public string WaiverPriority { get; set; }
        [XmlElement(ElementName = "faab_balance")]
        public string FaabBalance { get; set; }
        [XmlElement(ElementName = "number_of_moves")]
        public string NumberOfMoves { get; set; }
        [XmlElement(ElementName = "number_of_trades")]
        public string NumberOfTrades { get; set; }
        [XmlElement(ElementName = "roster_adds")]
        public RosterAdds RosterAdds { get; set; }
        [XmlElement(ElementName = "league_scoring_type")]
        public string LeagueScoringType { get; set; }
        [XmlElement(ElementName = "has_draft_grade")]
        public bool HasDraftGrade { get; set; }
        [XmlElement(ElementName = "draft_grade")]
        public string DraftGrade { get; set; }
        [XmlElement(ElementName = "draft_recap_url")]
        public string DraftRecapUrl { get; set; }
        [XmlArray(ElementName = "managers")]
        [XmlArrayItem(ElementName = "manager")]
        public List<Manager> Managers { get; set; }
        [XmlElement(ElementName = "clinched_playoffs")]
        public bool ClinchedPlayoffs { get; set; }
        [XmlArray(ElementName = "matchups")]
        [XmlArrayItem(ElementName = "matchup")]
        public List<Matchup> Matchups { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is TeamBase team)
            {
                return team.TeamKey.CompareTo(TeamKey);
            }
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (obj is TeamBase teamBase)
            {
                return TeamKey == teamBase.TeamKey;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return TeamKey.GetHashCode();
        }

        public static bool operator ==(TeamBase left, TeamBase right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(TeamBase left, TeamBase right)
        {
            return !(left == right);
        }

        public static bool operator <(TeamBase left, TeamBase right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(TeamBase left, TeamBase right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(TeamBase left, TeamBase right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(TeamBase left, TeamBase right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }

    [XmlRoot(ElementName = "team")]
    public class Team : TeamBase
    {

        [XmlElement(ElementName = "roster")]
        public Roster Roster { get; set; }


        [XmlElement(ElementName = "team_points")]
        public TeamPoints TeamPoints { get; set; }
        [XmlElement(ElementName = "team_standings")]
        public TeamStandings TeamStandings { get; set; }
    }


    [XmlRoot(ElementName = "roster", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Roster : IYahooResource
    {
        [XmlElement(ElementName = "coverage_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string CoverageType { get; set; }
        [XmlElement(ElementName = "week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int Week { get; set; }
        [XmlElement(ElementName = "is_editable", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsEditable { get; set; }
        [XmlArray(ElementName = "players", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        [XmlArrayItem(ElementName = "player", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<Player> Players { get; set; }
    }
}
