using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{

    [XmlRoot(ElementName = "fantasy_content")]
    public class FantasyContent<T>
    {
        public T Content { get; set; }
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
        [XmlAttribute(AttributeName = "uri", Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public string Uri { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "copyright")]
        public string Copyright { get; set; }
        [XmlAttribute(AttributeName = "refresh_rate")]
        public string RefreshRate { get; set; }
        [XmlAttribute(AttributeName = "yahoo", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Yahoo { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
