using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client.Fantasy
{
    public class YahooFantasyXmlSerializer<TContent> : XmlSerializer
    {
        public YahooFantasyXmlSerializer()
            : base(typeof(FantasyContent<TContent>), attributeOverides, Array.Empty<Type>(), null, "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")
        { }

        private static XmlAttributeOverrides attributeOverides
        {
            get
            {
                var attrOverrides = new XmlAttributeOverrides();
                attrOverrides.Add<FantasyContent<TContent>>(f => f.Content, OverrideContentAttribute);
                return attrOverrides;
            }
        }

        private static XmlAttributes OverrideContentAttribute
        {
            get
            {
                XmlAttributes attrs = new XmlAttributes();
                if (typeof(IEnumerable).IsAssignableFrom(typeof(TContent)) ||
                    typeof(IAsyncEnumerable<>).IsAssignableFromGenericType(typeof(TContent)))
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
