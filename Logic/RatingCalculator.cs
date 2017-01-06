using System;
using Contracts;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Logic
{
    public class RatingCalculator
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RatingCalculator));

        public Duel Calculate(string player, string opponent, List<Result> results)
        {
            var duel = new Duel { Player = player, Opponent = opponent };
            foreach (var result in results)
            {
                var firstPlayer = result.Scores.SingleOrDefault(p => p.Player == player);
                if (firstPlayer == null)
                    continue;
                var secondPlayer = result.Scores.SingleOrDefault(p => p.Player == opponent);
                if (secondPlayer == null)
                    continue;
                if (firstPlayer.Points > secondPlayer.Points)
                    duel.Wins++;
                if (firstPlayer.Points == secondPlayer.Points)
                    duel.Draws++;
                if (firstPlayer.Points < secondPlayer.Points)
                    duel.Losses++;
            }
            return duel;
        }

        public List<Rating> Calculate(List<Result> results)
        {
            results = results.OrderBy(x => x.Date).ThenBy(x => x.GameNumber).ToList();
            var ratings = new List<Rating>();
            foreach (var result in results)
            {
                Logger.Info("---------------------------------------------------------------------------------------------");
                Logger.InfoFormat("GAME: Date = {0}, Number = {1}, Name = {2}", result.Date, result.GameNumber, result.Name);

                bool hasExperiencedPlayer = false;
                foreach (var score in result.Scores)
                {
                    var existingRating = ratings.SingleOrDefault(r => r.Player == score.Player);
                    if (existingRating?.NumberOfRatedGames > 100)
                        hasExperiencedPlayer = true;
                }
                foreach (var score in result.Scores)
                {
                    if (!ratings.Select(r => r.Player).Contains(score.Player))
                    {
                        if (hasExperiencedPlayer)
                        {
                            ratings.Add(new Rating { Player = score.Player, Number = 1700, Highest = 1700, Lowest = 1700 });
                        }
                        else
                        {
                            ratings.Add(new Rating { Player = score.Player, Number = 1800, Highest = 1800, Lowest = 1800 });
                        }
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
                Logger.Debug("");
                Logger.Debug("  Calculation:");
                foreach (var score in result.Scores)
                {
                    var existingRating = ratings.Single(x => x.Player == score.Player);
                    double wins = 0, losses = 0;
                    int numberOfWins = 0, numberOfDraws = 0, numberOfLosses = 0;
                    foreach (var opponentScore in result.Scores.Where(x => x.Player != score.Player))
                    {
                        if (opponentScore.Points == score.Points)
                        {
                            wins += 0.5;
                            losses += 0.5;
                            numberOfDraws++;
                        }
                        if (opponentScore.Points < score.Points)
                        {
                            wins++;
                            numberOfWins++;
                        }
                        if (opponentScore.Points > score.Points)
                        {
                            losses++;
                            numberOfLosses++;
                        }
                    }
                    var averageRatingOpponents = (gameRatings.Where(p => p.Player != score.Player).Sum(x => x.Number)) / numberOfOpponents;
                    var newRating = (existingRating.Number + 10 * (wins - losses + ((averageRatingOpponents - existingRating.Number) / 400)));
                    Logger.DebugFormat("    {0} : existing rating = {1}, wins = {2}, losses = {3}, average rating opponents = {4}, new rating = {5}", score.Player, existingRating.Number, wins, losses, averageRatingOpponents, newRating);
                    tempRatings.Add(new Rating
                    {
                        Number = newRating,
                        Player = existingRating.Player,
                        NumberOfRatedGames = existingRating.NumberOfRatedGames + numberOfOpponents,
                        NumberOfDrawnGames = existingRating.NumberOfDrawnGames + numberOfDraws,
                        NumberOfLostGames = existingRating.NumberOfLostGames + numberOfLosses,
                        NumberOfWonGames = existingRating.NumberOfWonGames + numberOfWins,
                        LastPlayed = result.Date,
                        Highest = (newRating > existingRating.Number) ? newRating : existingRating.Number,
                        Lowest = (newRating < existingRating.Number) ? newRating : existingRating.Number
                    });
                }
                foreach (var rating in tempRatings)
                {
                    var existingRating = ratings.Single(x => x.Player == rating.Player);
                    existingRating.Number = rating.Number;
                    existingRating.NumberOfRatedGames = rating.NumberOfRatedGames;
                    existingRating.NumberOfDrawnGames = rating.NumberOfDrawnGames;
                    existingRating.NumberOfLostGames = rating.NumberOfLostGames;
                    existingRating.NumberOfWonGames = rating.NumberOfWonGames;
                    existingRating.LastPlayed = rating.LastPlayed;
                    existingRating.Highest = rating.Highest;
                    existingRating.Lowest = rating.Lowest;
                }

                
                Logger.Debug("");
                Logger.Debug("  Scores:");
                foreach (var score in result.Scores)
                {
                    Logger.DebugFormat("    {0} : {1}", score.Player, score.Points);
                }
                Logger.Debug("");
                Logger.Debug("  New ratings:");
                foreach (var score in result.Scores)
                {
                    var rating = ratings.Single(x => x.Player == score.Player);
                    Logger.DebugFormat("    {0} : {1}", rating.Player, Math.Round(rating.Number, 1));
                }
                Logger.Info("---------------------------------------------------------------------------------------------");
            }
            
            return ratings;
        }
    }
}
