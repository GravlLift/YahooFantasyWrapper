using System;

namespace YahooFantasyWrapper.Models
{
    public class UserInfo
    {
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public string Nickname { get; set; }

        public string Guid { get; set; }

        public bool BdRestricted { get; set; }

        public string AgeCategory { get; set; }

        public bool Cache { get; set; }

        public bool IsConnected { get; set; }

        public Image Image { get; set; }

        public string Jurisdiction { get; set; }

        public string ProfileStatus { get; set; }

        public string ProfileMode { get; set; }

        public bool ProfileHidden { get; set; }

        public string ProfilePermission { get; set; }

        public bool Searchable { get; set; }

        public string ProfileUrl { get; set; }

        public string Uri { get; set; }
    }

    public class Image
    {
        public string ImageUrl { get; set; }

        public long Height { get; set; }

        public string Size { get; set; }

        public long Width { get; set; }
    }
}
