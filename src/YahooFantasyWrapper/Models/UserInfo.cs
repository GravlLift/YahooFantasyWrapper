using System;
//using System.Text.Json.Serialization;

namespace YahooFantasyWrapper.Models
{
    [Serializable]
    public class UserInfo
    {
        // [JsonPropertyName("profile")]
        public Profile Profile { get; set; }
    }
    [Serializable]
    public class Profile
    {
        // [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        // [JsonPropertyName("guid")]
        public string Guid { get; set; }

        // [JsonPropertyName("bdRestricted")]
        public bool BdRestricted { get; set; }

        // [JsonPropertyName("ageCategory")]
        public string AgeCategory { get; set; }

        // [JsonPropertyName("cache")]
        public bool Cache { get; set; }

        // [JsonPropertyName("isConnected")]
        public bool IsConnected { get; set; }

        // [JsonPropertyName("image")]
        public Image Image { get; set; }

        // [JsonPropertyName("jurisdiction")]
        public string Jurisdiction { get; set; }

        // [JsonPropertyName("profileStatus")]
        public string ProfileStatus { get; set; }

        // [JsonPropertyName("profileMode")]
        public string ProfileMode { get; set; }

        // [JsonPropertyName("profileHidden")]
        public bool ProfileHidden { get; set; }

        // [JsonPropertyName("profilePermission")]
        public string ProfilePermission { get; set; }

        // [JsonPropertyName("searchable")]
        public bool Searchable { get; set; }

        // [JsonPropertyName("profileUrl")]
        public string ProfileUrl { get; set; }

        // [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }
    [Serializable]
    public class Image
    {
        // [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        // [JsonPropertyName("height")]
        public long Height { get; set; }

        // [JsonPropertyName("size")]
        public string Size { get; set; }

        // [JsonPropertyName("width")]
        public long Width { get; set; }
    }

}
