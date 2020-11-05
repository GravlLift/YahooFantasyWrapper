using System;
using System.Collections;
using System.Collections.Generic;
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

        private static Type fantasyContentType
        {
            get
            {
                if (typeof(IEnumerable).IsAssignableFrom(typeof(TContent)))
                {
                    return typeof(FantasyContent<>)
                        .MakeGenericType(typeof(List<>)
                            .MakeGenericType(typeof(TContent).GetGenericArguments()[0]));
                }
                else
                {
                    return typeof(FantasyContent<TContent>);
                }
            }
        }

        public YahooFantasyXmlSerializer()
            : base(fantasyContentType, attributeOverides, Array.Empty<Type>(), null, "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")
        { }

        private static XmlAttributes OverrideContentAttribute
        {
            get
            {
                XmlAttributes attrs = new XmlAttributes();
                if (typeof(IEnumerable).IsAssignableFrom(typeof(TContent)))
                {
                    XmlRootAttribute attribute = typeof(TContent).GetGenericArguments()[0]
                        .GetCustomAttribute<XmlRootAttribute>();
                    attrs.XmlArray = new XmlArrayAttribute($"{attribute.ElementName}s");
                    attrs.XmlArrayItems.Add(new XmlArrayItemAttribute { ElementName = attribute.ElementName });
                }
                else
                {
                    XmlRootAttribute attribute = typeof(TContent).GetCustomAttribute<XmlRootAttribute>();
                    attrs.XmlElements.Add(new XmlElementAttribute { ElementName = attribute.ElementName });
                }

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
