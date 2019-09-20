using System.Collections.Generic;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Request
{
    [XmlRoot(ElementName = "transaction_data")]
    public class TransactionData
    {
        [XmlElement(ElementName = "type")]
        public TransactionDataType Type { get; set; }
        [XmlElement(ElementName = "source_team_key")]
        public string SourceTeamKey { get; set; }
        [XmlElement(ElementName = "destination_team_key")]
        public string DestinationTeamKey { get; set; }
    }

    [XmlRoot(ElementName = "player")]
    public class Player
    {
        [XmlElement(ElementName = "player_key")]
        public string PlayerKey { get; set; }
        [XmlElement(ElementName = "transaction_data")]
        public TransactionData TransactionData { get; set; }
    }

    public abstract class BaseTransaction
    {
        [XmlElement(ElementName = "type")]
        public TransactionType Type { get; set; }
    }

    [XmlRoot(ElementName = "transaction")]
    public class SinglePlayerTransaction : BaseTransaction
    {
        [XmlElement(ElementName = "player")]
        public Player Player { get; set; }
    }

    [XmlRoot(ElementName = "transaction")]
    public class MultiPlayerTransaction : BaseTransaction
    {
        [XmlArray(ElementName = "players")]
        [XmlArrayItem(ElementName = "player")]
        public List<Player> PlayerList { get; set; }
    }

    public enum TransactionType
    {
        [XmlEnum(Name = "add")]
        Add,
        [XmlEnum(Name = "drop")]
        Drop,
        [XmlEnum(Name = "add/drop")]
        AddDrop
    }

    public enum TransactionDataType
    {
        [XmlEnum(Name = "add")]
        Add,
        [XmlEnum(Name = "drop")]
        Drop
    }
}
