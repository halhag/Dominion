using System;

namespace Contracts
{
    public class Rating
    {
        public string Player { get; set; }
        public int NumberOfRatedGames { get; set; }
        public double Number { get; set; }
        public DateTime LastPlayed { get; set; }
        public int NumberOfLostGames { get; set; }
        public int NumberOfDrawnGames { get; set; }
        public int NumberOfWonGames { get; set; }
    }
}
