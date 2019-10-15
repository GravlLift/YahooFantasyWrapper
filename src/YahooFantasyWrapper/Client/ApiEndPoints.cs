using System.Linq;
using System;
using System.Text;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    internal static class ApiEndpoints
    {
        #region Const
        /// <summary>
        /// Api Url for Yahoo Fantasy Api :https://fantasysports.yahooapis.com/fantasy/v2
        /// </summary>
        private const string BaseApiUrl = "https://fantasysports.yahooapis.com/fantasy/v2";

        /// <summary>
        /// QS Parameter to specify to use the authenticated users scope
        /// </summary>
        private const string LoginString = ";use_login=1";

        #endregion

        #region Game

        internal static EndPoint GameEndPoint(string gameKey)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/game/{gameKey}"
            };
        }

        internal static EndPoint GameEndPoint(string gameKey, EndpointSubResources resource)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/game/{gameKey}/{resource.ToFriendlyString()}"
            };
        }

        internal static EndPoint GameLeaguesEndPoint(string gameKey, string[] leagueKeys)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/game/{gameKey}/leagues;league_keys={string.Join(",", leagueKeys)}"
            };
        }

        internal static EndPoint GamePlayersEndPoint(string gameKey, EndpointSubResourcesCollection subresources = null, PlayerCollectionFilters filters = null)
        {
            string playerFilter = "";

            if (filters?.StartIndex != null)
            {
                playerFilter += $";start={filters.StartIndex.Value}";
            }
            if (filters?.Count != null)
            {
                playerFilter += $";count={filters.Count.Value}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/game/{gameKey}/players{playerFilter}{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint GamesEndPoint(string[] gameKeys, EndpointSubResourcesCollection subresources = null, GameCollectionFilters filters = null)
        {
            string games = "";
            if (gameKeys.Length > 0)
            {
                games = $";game_keys={ string.Join(",", gameKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/games{games}{BuildGameFiltersList(filters)}{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint UserGamesEndPoint(string[] gameKeys = null, EndpointSubResourcesCollection subresources = null, GameCollectionFilters filters = null)
        {
            string games = "";
            if (gameKeys != null && gameKeys.Length > 0)
            {
                games = $";game_keys={ string.Join(",", gameKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/users{LoginString}/games{games}{BuildGameFiltersList(filters)}{BuildSubResourcesList(subresources)}"
            };
        }



        #endregion

        #region User

        internal static EndPoint UserGameLeaguesEndPoint(string[] gameKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            string games = "";
            if (gameKeys != null && gameKeys.Length > 0)
            {
                games = $";game_keys={ string.Join(",", gameKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/users{LoginString}/games{games}/leagues{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint UserTeamsEndPoint(EndpointSubResourcesCollection subresources = null)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/users{LoginString}/teams{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint UserGamesTeamsEndPoint(string[] gameKeys, EndpointSubResourcesCollection subresources)
        {
            string games = "";
            if (gameKeys.Length > 0)
            {
                games = $";game_keys={ string.Join(",", gameKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/users{LoginString}/games{games}/teams{BuildSubResourcesList(subresources)}"
            };
        }

        #endregion

        #region League

        internal static EndPoint LeagueEndPoint(string leagueKey, EndpointSubResources resource, int?[] weeks = null)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/league/{leagueKey}/{resource.ToFriendlyString()}{BuildWeekList(weeks)}"
            };
        }

        internal static EndPoint LeaguesEndPoint(string[] leagueKeys, EndpointSubResourcesCollection subresources = null)
        {
            string leagues = "";
            if (leagueKeys.Length > 0)
            {
                leagues = $";league_keys={ string.Join(",", leagueKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/leagues{leagues}{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint LeagueTeamsEndPoint(string[] leagueKeys, EndpointSubResourcesCollection subresources)
        {
            string leagues = "";
            if (leagueKeys.Length > 0)
            {
                leagues = $";league_keys={ string.Join(",", leagueKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/leagues{leagues}/teams{BuildSubResourcesList(subresources)}"
            };

        }

        internal static EndPoint LeaguePlayersEndPoint(string[] leagueKeys, EndpointSubResourcesCollection subresources, PlayerCollectionFilters filters = null)
        {
            string leagues = "";
            if (leagueKeys.Length > 0)
            {
                leagues = $";league_keys={ string.Join(",", leagueKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/leagues{leagues}/players{BuildSubResourcesList(subresources)}"
            };

        }
        #endregion

        #region Player

        internal static EndPoint PlayerOwnershipEndPoint(string[] playerKeys, string leagueKey)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/league/{leagueKey}/players;player_keys={string.Join(",", playerKeys)}/ownership"
            };
        }

        internal static EndPoint PlayerEndPoint(string playerKey, EndpointSubResources resource)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/player/{playerKey}/{resource.ToFriendlyString()}"
            };
        }

        internal static EndPoint PlayersEndPoint(string[] playerKeys, EndpointSubResourcesCollection subresources = null)
        {
            string players = "";
            if (playerKeys.Length > 0)
            {
                players = $";player_keys={ string.Join(",", playerKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/players{players}{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint PlayersLeagueEndPoint(string[] leagueKeys, EndpointSubResourcesCollection subresources = null, PlayerCollectionFilters filters = null)
        {
            string leagues = "";
            if (leagueKeys.Length > 0)
            {
                leagues = $";league_keys={ string.Join(",", leagueKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/leagues{leagues}/players{BuildSubResourcesList(subresources)}{BuildPlayersFiltersList(filters)}"
            };
        }

        internal static EndPoint PlayersTeamEndPoint(string[] teamKeys, EndpointSubResourcesCollection subresources = null, PlayerCollectionFilters filters = null)
        {
            string teams = "";
            if (teamKeys.Length > 0)
            {
                teams = $";team_keys={ string.Join(",", teamKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/teams{teams}/players{BuildSubResourcesList(subresources)}{BuildPlayersFiltersList(filters)}"
            };
        }
        #endregion

        #region Roster
        internal static EndPoint RosterEndPoint(string teamKey, int? week = null, DateTime? date = null)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/team/{teamKey}/roster/{BuildWeekList(new int?[] { week })}{BuildDate(date)}"
            };

        }

        #endregion

        #region Team

        internal static EndPoint TeamEndPoint(string teamKey, EndpointSubResources resource)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/team/{teamKey}/{resource.ToFriendlyString()}"
            };
        }
        internal static EndPoint TeamsEndPoint(string[] teamKeys, EndpointSubResourcesCollection subresources = null)
        {
            string teams = "";
            if (teamKeys.Length > 0)
            {
                teams = $";team_keys={ string.Join(",", teamKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/teams{teams}{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint TeamsLeagueEndPoint(string[] leagueKeys, EndpointSubResourcesCollection subresources = null)
        {
            string leagues = "";
            if (leagueKeys.Length > 0)
            {
                leagues = $";league_keys={ string.Join(",", leagueKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/leagues{leagues}/teams{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint UserLeaguesEndPoint(string[] leagueKeys, EndpointSubResourcesCollection subresources = null)
        {

            if (leagueKeys.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(leagueKeys));
            }
            string leagueFilter = $";league_keys={ string.Join(",", leagueKeys)}";

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/users{LoginString}/leagues{leagueFilter}{BuildSubResourcesList(subresources)}"
            };
        }

        #endregion

        #region Transaction

        internal static EndPoint TransactionEndpoint(string transactionKey, EndpointSubResources resource)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/transaction/{transactionKey}/{resource.ToFriendlyString()}"
            };
        }

        internal static EndPoint TransactionsEndPoint(string[] transactionKeys = null, EndpointSubResourcesCollection subresources = null)
        {
            string transactions = "";
            if (transactionKeys != null && transactionKeys.Length > 0)
            {
                transactions = $";transaction_keys={ string.Join(",", transactionKeys)}";
            }

            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/transactions{transactions}{BuildSubResourcesList(subresources)}"
            };
        }

        internal static EndPoint TransactionsLeagueEndPoint(string leagueKey, EndpointSubResourcesCollection subresources = null)
        {
            return new EndPoint
            {
                BaseUri = BaseApiUrl,
                Resource = $"/league/{leagueKey}/transactions{BuildSubResourcesList(subresources)}"
            };
        }

        #endregion

        private static string BuildSubResourcesList(EndpointSubResourcesCollection subresources)
        {
            string subs = "";
            if (subresources != null && subresources.Resources.Count > 0)
            {
                subs = $";out={ string.Join(",", subresources.Resources.Select(a => a.ToFriendlyString()))}";

            }
            return subs;
        }

        private static string BuildGameFiltersList(GameCollectionFilters filters)
        {
            if (filters == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            if (filters.IsAvailable != null)
            {
                sb.Append($";is_available={Convert.ToInt32(filters.IsAvailable.Value)}");
            }

            if (filters.Seasons != null && filters.Seasons.Length > 0)
            {
                sb.Append($";seasons={string.Join(",", filters.Seasons)}");
            }

            if (filters.GameCodes != null && filters.GameCodes.Length > 0)
            {
                sb.Append($";game_codes={string.Join(",", filters.GameCodes.Select(a => Enum<GameCode>.GetName(a)))}");
            }

            if (filters.GameTypes != null && filters.GameTypes.Length > 0)
            {
                sb.Append($";game_types={ string.Join(",", filters.GameTypes.Select(a => a.ToFriendlyString()))}");

            }
            return sb.ToString();
        }

        private static string BuildPlayersFiltersList(PlayerCollectionFilters filters)
        {
            if (filters == null)
                return string.Empty;



            StringBuilder sb = new StringBuilder();

            if (filters.Positions != null && filters.Positions.Length > 0)
            {
                sb.Append($";position={string.Join(",", filters.Positions)}");
            }

            if (filters.Statuses != null && filters.Statuses.Length > 0)
            {
                sb.Append($";status={string.Join(",", filters.Statuses.Select(s => s.ToString()))}");
            }

            if (filters.Search != null && filters.Search.Length > 0)
            {
                sb.Append($";search={ filters.Search}");
            }

            if (filters.Sort != null && filters.Sort.Length > 0)
            {
                sb.Append($";sort={ filters.Sort}");
            }

            if (filters.SortType != null)
            {
                sb.Append($";sort_type={ filters.SortType}");
            }

            if (filters.SortSeason != null)
            {
                sb.Append($";sort_season={ filters.SortSeason}");
            }

            if (filters.SortWeek != null)
            {
                sb.Append($";sort_week={ filters.SortWeek}");
            }

            if (filters.StartIndex != null)
            {
                sb.Append($";start={ filters.StartIndex}");
            }

            if (filters.Count != null)
            {
                sb.Append($";count={ filters.Count}");
            }

            return sb.ToString();
        }

        private static string BuildWeekList(int?[] weeks)
        {
            string weekString = "";
            if (weeks != null && weeks.Length > 0)
            {
                weekString = $";week={ string.Join(",", weeks.Select(a => a.ToString()))}";
            }
            return $"{weekString}";
        }

        private static string BuildDate(DateTime? date)
        {
            string dt = "";
            if (date != null)
            {
                dt = $";date={ date.Value.ToString("yyyy-M-dd") }";
            }

            return dt;
        }
    }
}
