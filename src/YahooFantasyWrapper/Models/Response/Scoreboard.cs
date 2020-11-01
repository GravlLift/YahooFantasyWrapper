using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    [XmlRoot(ElementName = "stat_winner")]
    public class StatWinner
    {
        [XmlElement(ElementName = "stat_id")]
        public string StatId { get; set; }
        [XmlElement(ElementName = "winner_team_key")]
        public string WinnerTeamKey { get; set; }
    }

    [XmlRoot(ElementName = "team_stats")]
    public class TeamStats
    {
        [XmlElement(ElementName = "coverage_type")]
        public string CoverageType { get; set; }
        [XmlElement(ElementName = "week")]
        public int Week { get; set; }
        [XmlArray(ElementName = "stats")]
        [XmlArrayItem(ElementName = "stat")]
        public List<Stat> Stats { get; set; }
    }

    [XmlRoot(ElementName = "team_points")]
    public class TeamWeekPoints
    {
        [XmlElement(ElementName = "coverage_type")]
        public string CoverageType { get; set; }
        [XmlElement(ElementName = "week")]
        public int Week { get; set; }
        [XmlElement(ElementName = "total")]
        public float Total { get; set; }
    }

    [XmlRoot(ElementName = "team")]
    public class ScoreboardTeam : TeamBase
    {
        [XmlElement(ElementName = "team_points")]
        public TeamWeekPoints TeamPoints { get; set; }

        [XmlElement(ElementName = "team_stats")]
        public TeamStats TeamStats { get; set; }
    }

    [XmlRoot(ElementName = "matchup")]
    public class Matchup
    {
        [XmlElement(ElementName = "week")]
        public int Week { get; set; }
        [XmlElement(ElementName = "week_start")]
        public DateTime WeekStart { get; set; }
        [XmlElement(ElementName = "week_end")]
        public DateTime WeekEnd { get; set; }
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "is_playoffs")]
        public bool IsPlayoffs { get; set; }
        [XmlElement(ElementName = "is_consolation")]
        public bool IsConsolation { get; set; }
        [XmlElement(ElementName = "is_tied")]
        public bool IsTied { get; set; }
        [XmlElement(ElementName = "winner_team_key")]
        public string WinnerTeamKey { get; set; }
        [XmlArray(ElementName = "stat_winners")]
        [XmlArrayItem(ElementName = "stat_winner")]
        public List<StatWinner> StatWinners { get; set; }
        [XmlArray(ElementName = "teams")]
        [XmlArrayItem(ElementName = "team")]
        public List<ScoreboardTeam> Teams { get; set; }
    }

    [XmlRoot(ElementName = "scoreboard")]
    public class Scoreboard
    {
        [XmlElement(ElementName = "week")]
        public string Week { get; set; }
        [XmlArray(ElementName = "matchups")]
        [XmlArrayItem(ElementName = "matchup")]
        public List<Matchup> Matchups { get; set; }
    }

}