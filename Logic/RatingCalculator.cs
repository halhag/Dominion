﻿using Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class RatingCalculator
    {
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
            var ratings = new List<Rating>();
            foreach (var result in results)
            {
                bool hasExperiencedPlayer = false;
                foreach (var score in result.Scores)
                {
                    var existingRating = ratings.SingleOrDefault(r => r.Player == score.Player);
                    if (existingRating != null)
                    {
                        if (existingRating.NumberOfRatedGames > 100)
                            hasExperiencedPlayer = true;
                    }
                }
                foreach (var score in result.Scores)
                {
                    if (!ratings.Select(r => r.Player).Contains(score.Player))
                    {
                        if (hasExperiencedPlayer)
                        {
                            ratings.Add(new Rating { Player = score.Player, Number = 1800 });
                        }
                        else
                        {
                            ratings.Add(new Rating { Player = score.Player, Number = 1700 });
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
                foreach (var score in result.Scores)
                {
                    var existingRating = ratings.Single(x => x.Player == score.Player);
                    double wins = 0, losses = 0, draws = 0;
                    foreach (var opponentScore in result.Scores.Where(x => x.Player != score.Player))
                    {
                        if (opponentScore.Points == score.Points)
                        {
                            wins += 0.5;
                            losses += 0.5;
                            draws++;
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
                        NumberOfRatedGames = existingRating.NumberOfRatedGames += numberOfOpponents,
                        NumberOfDrawnGames = existingRating.NumberOfDrawnGames + (int)draws,
                        NumberOfLostGames = existingRating.NumberOfLostGames + (int)losses,
                        NumberOfWonGames = existingRating.NumberOfWonGames + (int) wins
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