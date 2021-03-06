﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Query.Internal
{
    internal class YahooUrlSegment
    {
        public Type ResourceType { get; set; }
        // The key and values to be comma separated
        // For a filter, this implies OR
        // For an out, this implies sub resources
        public Dictionary<string, HashSet<string>> Modifiers = new();
        public bool IsCollection { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var xmlRootAttribute =
                (XmlRootAttribute)Attribute.GetCustomAttribute(
                    ResourceType,
                    typeof(XmlRootAttribute)
                );
            sb.Append(xmlRootAttribute.ElementName);

            // Check if this can be labeled as a collection, basically only a key
            // selector
            if (typeof(IYahooResource).IsAssignableFrom(ResourceType))
            {
                // Resources can only ever be resources
                IsCollection = false;
            }
            else
            {
                // Collections can be collections or resources
                if (Modifiers.Count == 1)
                {
                    // Force collection if we are trying to filter by multiple keys at once
                    var modifier = Modifiers.Single();
                    if (!modifier.Key.EndsWith("_key") || modifier.Value.Count != 1)
                    {
                        IsCollection = true;
                    }
                }
                // Or if we have multiple non-out modifiers
                else if (Modifiers.Where(m => m.Key != "out").Count() > 1)
                {
                    IsCollection = true;
                }
            }

            if (IsCollection)
            {
                sb.Append("s");
                if (Modifiers.Count > 0)
                {
                    foreach (var modifier in Modifiers)
                    {
                        sb.Append($";{modifier.Key}={string.Join(",", modifier.Value)}");
                    }
                }
            }
            else
            {
                if (
                    Modifiers.Count == 1
                    && Modifiers.First().Key.EndsWith("_key")
                    && Modifiers.First().Value.Count == 1
                ) {
                    sb.Append($"/{Modifiers.First().Value.Single()}");
                }
                else
                {
                    foreach (var modifier in Modifiers)
                    {
                        sb.Append($";{modifier.Key}={string.Join(",", modifier.Value)}");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
