using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{
    public enum FantasyPosition
    {
        QB,
        WR,
        RB,
        TE,
        [XmlEnum("W/R/T")]
        WRT,
        K,
        DEF,
        BN,
        IR
    }
}
