using System;

namespace Contracts
{
    public class Rating
    {
        public Rating()
        {
            Trend = new Trend();
        }

        public string Player { get; set; }
        public int NumberOfRatedGames { get; set; }
        public double Number { get; set; }
        public DateTime LastPlayed { get; set; }
        public int NumberOfLostGames { get; set; }
        public int NumberOfDrawnGames { get; set; }
        public int NumberOfWonGames { get; set; }
        public double Highest { get; set; }
        public double Lowest { get; set; }
        public Trend Trend { get; set; }
    }
}
