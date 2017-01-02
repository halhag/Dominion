using System;

namespace Contracts
{
    public class Rating
    {
        public string Player { get; set; }
        public int NumberOfRatedGames { get; set; }
        public double Number { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
