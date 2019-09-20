using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models.Request;

namespace YahooFantasyWrapper.Client.Fantasy
{
    internal class YahooFantasyXmlSerializer<TContent>
    {
        private readonly XmlSerializer xmlSerializer;

        public YahooFantasyXmlSerializer()
        {
            var attrOverrides = new XmlAttributeOverrides();
            attrOverrides.Add<FantasyContent<TContent>>(f => f.Content, OverrideContentAttribute);
            xmlSerializer = new XmlSerializer(typeof(FantasyContent<TContent>), attrOverrides);
        }

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
            xmlSerializer.Serialize(writer, fantasyWrapper, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
        }
    }
}
