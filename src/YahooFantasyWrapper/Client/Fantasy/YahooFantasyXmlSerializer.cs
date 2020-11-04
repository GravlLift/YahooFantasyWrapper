using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client.Fantasy
{
    public class YahooFantasyXmlSerializer<TContent> : XmlSerializer
    {
        private static XmlAttributeOverrides attributeOverides
        {
            get
            {
                var attrOverrides = new XmlAttributeOverrides();
                attrOverrides.Add<FantasyContent<TContent>>(f => f.Content, OverrideContentAttribute);
                return attrOverrides;
            }
        }

        public YahooFantasyXmlSerializer()
            : base(typeof(FantasyContent<TContent>), attributeOverides, Array.Empty<Type>(), null, "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")
        { }

        private static XmlAttributes OverrideContentAttribute
        {
            get
            {
                XmlRootAttribute attribute = typeof(TContent).GetCustomAttribute<XmlRootAttribute>();

                XmlAttributes attrs = new XmlAttributes();
                attrs.XmlElements.Add(new XmlElementAttribute { ElementName = attribute.ElementName });

                return attrs;
            }
        }

        public void Serialize(XmlWriter writer, TContent o)
        {
            var fantasyWrapper = new FantasyContent<TContent>
            {
                Content = o
            };
            Serialize(writer, fantasyWrapper, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
        }
    }
}
