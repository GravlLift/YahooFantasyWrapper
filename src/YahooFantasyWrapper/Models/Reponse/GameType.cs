using System;
using System.Collections.Generic;
using System.Text;

namespace YahooFantasyWrapper.Models.Response
{
    public enum GameType
    {
        Full,
        PickemTeam,
        PickemGroup,
        PickemTeamList
    }
    public static class GameTypeExtensions
    {
        public static string ToFriendlyString(this GameType me)
        {
            return me switch
            {
                GameType.Full => "full",
                GameType.PickemGroup => "pickem-group",
                GameType.PickemTeam => "pickem-team",
                GameType.PickemTeamList => "pickem-team-list",
                _ => "",
            };
        }
    }
}
