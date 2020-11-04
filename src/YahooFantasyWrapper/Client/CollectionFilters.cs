﻿using System;
using System.Collections.Generic;
using System.Text;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// Filter Object for Game Collection
    /// </summary>
    public class GameCollectionFilters
    {
        public bool? IsAvailable { get; set; }
        public ICollection<int> Seasons { get; set; }
        public ICollection<GameCode> GameCodes { get; set; }
        public ICollection<GameType> GameTypes { get; set; }
    }

    /// <summary>
    /// Filter Object for Player Collection
    /// </summary>
    public class PlayerCollectionFilters
    {
        public ICollection<string> Positions { get; set; }
        public ICollection<PlayerOwnershipStatus> Statuses { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public PlayerSortType SortType { get; set; }
        public int? SortSeason { get; set; }
        public int? SortWeek { get; set; }
        public int? StartIndex { get; set; }
        public int? Count { get; set; }
    }
}
