using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// Subresrources handler class
    /// </summary>
    public class EndpointSubResourcesCollection
    {
        /// <summary>
        /// List of chosen subresources
        /// </summary>
        public IList<EndpointSubResources> Resources { get; set; }

        /// <summary>
        /// Builds list of subresrources to pass onto Api
        /// </summary>
        /// <param name="resources">subresources for api</param>
        /// <returns></returns>
        public static EndpointSubResourcesCollection BuildResourceList(params EndpointSubResources[] resources)
        {
            var collection = new EndpointSubResourcesCollection
            {
                Resources = resources.ToList()
            };
            return collection;
        }
    }

    public enum EndpointSubResources
    {
        MetaData, // "metadata"
        GameWeeks,// "game_weeks"
        PositionTypes,//  "position_types"
        RosterPositions,//  "roster_positions"
        StatCategories,// "stat_categories"



        DraftResults,// "draftresults"
        Players,// "players"
        Settings,// "settings"
        Standings,// "standings"
        Scoreboard,// "scoreboard"
        Teams,// "teams"
        Transactions,// "transactions"

        Stats,// "stats"
        PercentOwned,// "percent_owned"
        DraftAnalysis,// "draft_analysis"

        Roster,// "roster"
        Matchups,// "matchups"

        Ownership,// "ownership"

        Leagues// "leagues"
    }

    public static class EndpointSubResourcesExtensions
    {
        /// <summary>
        /// Passes Enumeration and maps to friendly string for api
        /// </summary>
        /// <param name="resource">resoure to map to friendly name</param>
        /// <returns></returns>
        public static string ToFriendlyString(this EndpointSubResources resource)
        {
            return resource switch
            {
                EndpointSubResources.MetaData => "metadata",
                EndpointSubResources.GameWeeks => "game_weeks",
                EndpointSubResources.PositionTypes => "position_types",
                EndpointSubResources.StatCategories => "stat_categories",
                EndpointSubResources.DraftAnalysis => "draft_analysis",
                EndpointSubResources.DraftResults => "draftresults",
                EndpointSubResources.Players => "players",
                EndpointSubResources.Settings => "settings",
                EndpointSubResources.Standings => "standings",
                EndpointSubResources.Scoreboard => "scoreboard",
                EndpointSubResources.Teams => "teams",
                EndpointSubResources.Transactions => "transactions",
                EndpointSubResources.Stats => "stats",
                EndpointSubResources.PercentOwned => "percent_owned",
                EndpointSubResources.Roster => "roster",
                EndpointSubResources.Matchups => "matchups",
                EndpointSubResources.Ownership => "ownership",
                EndpointSubResources.Leagues => "leagues",
                EndpointSubResources.RosterPositions => "roster_positions",
                _ => "",
            };
        }

        /// <summary>
        /// Check if List of SubResources is null or empty
        /// </summary>
        /// <param name="subresources">List of SubResources</param>
        /// <returns>if it is empty or not</returns>
        public static bool IsNotEmpty(this EndpointSubResourcesCollection subresources)
        {
            return subresources != null && subresources.Resources.Count > 0;
        }
    }
}
