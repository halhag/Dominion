using System;

namespace Dominion.Models
{
    public class RatingModel
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public string LastPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}