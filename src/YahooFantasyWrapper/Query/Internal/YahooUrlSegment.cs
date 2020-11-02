using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Query.Internal
{
    internal class YahooUrlSegment
    {
        public Type ResourceType { get; set; }
        // The key and values to be comma separated
        // For a filter, this implies OR
        // For an out, this implies sub resources
        public Dictionary<string, HashSet<string>> Modifiers = new Dictionary<string, HashSet<string>>();
        public bool IsCollection { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var xmlRootAttribute = (XmlRootAttribute)Attribute
                .GetCustomAttribute(ResourceType, typeof(XmlRootAttribute));
            sb.Append(xmlRootAttribute.ElementName);

            if (IsCollection == false)
            {
                var keyFilter = Modifiers.SingleOrDefault(m => m.Key.EndsWith("_key"));
                if (string.IsNullOrEmpty(keyFilter.Key))
                {
                    // Key is not the only filter, this is a collection
                    IsCollection = true;
                }
                else
                {
                    if (keyFilter.Value.Count > 1)
                    {
                        // Filtering for multiple keys, this is a collection
                        IsCollection = true;
                    }
                    else
                    {
                        sb.Append($"/{keyFilter.Value.Single()}");
                    }
                }
            }

            if (IsCollection == true)
            {
                sb.Append("s");
                if (Modifiers.Count > 0)
                {
                    sb.Append(";");

                    foreach (var modifier in Modifiers)
                    {
                        sb.Append($"{modifier.Key}={string.Join(",", modifier.Value)}");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
