﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    [XmlRoot(ElementName = "transaction_data")]
    public class TransactionData
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "destination_team_key")]
        public string DestinationTeamKey { get; set; }
    }

    [XmlRoot(ElementName = "transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "faab_bid")]
        public string FaabBid { get; set; }
        [XmlArray(ElementName = "players")]
        [XmlArrayItem(ElementName = "player")]
        public List<Player> Players { get; set; }
    }
}
