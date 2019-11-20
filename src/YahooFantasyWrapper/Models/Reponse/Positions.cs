using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{

    [XmlRoot(ElementName = "position_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class PositionType
    {
        [XmlElement(ElementName = "type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Type { get; set; }
        [XmlElement(ElementName = "display_name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DisplayName { get; set; }
    }

    [XmlRoot(ElementName = "roster_position", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class RosterPosition
    {
        [XmlElement(ElementName = "position", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Position { get; set; }
        [XmlElement(ElementName = "abbreviation", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Abbreviation { get; set; }
        [XmlElement(ElementName = "display_name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DisplayName { get; set; }
        [XmlElement(ElementName = "position_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string PositionType { get; set; }
        [XmlElement(ElementName = "is_bench", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public bool IsBench { get; set; }
        [XmlElement(ElementName = "count", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public int Count { get; set; }
    }

    [XmlRoot(ElementName = "selected_position")]
    public class SelectedPosition
    {
        [XmlElement(ElementName = "coverage_type")]
        public string CoverageType { get; set; }
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "position")]
        public string Position { get; set; }
    }
}
