using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models.Response
{
    /// <summary>
    /// Yahoo error response
    /// </summary>
    [XmlRoot("error", Namespace = "http://yahooapis.com/v1/base.rng")]
    public class Error
    {
        /// <summary>
        /// Error description
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
    }
}
