﻿namespace Dominion.Models
{
    public class RatingModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string LastPlayed { get; set; }
        public string LastPlayedValue { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public double HighestRating { get; set; }
        public double LowestRating { get; set; }
        public string Trend { get; set; }
    }
}