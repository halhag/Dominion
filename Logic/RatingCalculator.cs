using Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class RatingCalculator
    {
        public List<Rating> Calculate(List<Result> results)
        {
            var ratings = new List<Rating>();
            foreach (var result in results)
            {
                foreach (var score in result.Scores)
                {
                    if (!ratings.Select(r => r.Player).Contains(score.Player))
                    {
                        ratings.Add(new Rating {Player = score.Player, Number = 1800});
                    }
                }

                var gameRatings = new List<Rating>();
                foreach (var score in result.Scores)
                {
                    var existingRating = ratings.Single(x => x.Player == score.Player);
                    gameRatings.Add(new Rating
                    {
                        Number = existingRating.Number,
                        Player = existingRating.Player
                    });
                }
                var tempRatings = new List<Rating>();
                var numberOfOpponents = result.Scores.Count - 1;
                foreach (var score in result.Scores)
                {
                    var existingRating = ratings.Single(x => x.Player == score.Player);
                    double wins = 0, losses = 0;
                    foreach (var opponentScore in result.Scores.Where(x => x.Player != score.Player))
                    {
                        if (opponentScore.Points == score.Points)
                        {
                            wins += 0.5;
                            losses += 0.5;
                        }
                        if (opponentScore.Points < score.Points)
                            wins++;
                        if (opponentScore.Points > score.Points)
                            losses++;
                    }
                    var averageRatingOpponents = (gameRatings.Where(p => p.Player != score.Player).Sum(x => x.Number)) / numberOfOpponents;
                    var newRating = (existingRating.Number + 10 * (wins - losses - (averageRatingOpponents - existingRating.Number) / 400));
                    tempRatings.Add(new Rating
                    {
                        Number = newRating,
                        Player = existingRating.Player,
                        NumberOfRatedGames = existingRating.NumberOfRatedGames += numberOfOpponents
                    });
                }
                foreach (var rating in tempRatings)
                {
                    var existingRating = ratings.Single(x => x.Player == rating.Player);
                    existingRating.Number = rating.Number;
                    existingRating.NumberOfRatedGames = rating.NumberOfRatedGames;
                }
            }
            
            return ratings;
        }
    }
}
