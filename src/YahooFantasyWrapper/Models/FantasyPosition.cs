using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{
    public enum FantasyPosition
    {
        QB,
        WR,
        RB,
        TE,
        [Display(Name = "W/R/T")]
        [XmlEnum("W/R/T")]
        WRT,
        K,
        DEF,
        BN,
        IR
    }
}
