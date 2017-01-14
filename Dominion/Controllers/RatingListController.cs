using System;
using System.Linq;
using System.Web.Mvc;
using Dominion.Models;
using Logic;
using Newtonsoft.Json;
using Repository;

namespace Dominion.Controllers
{
    public class RatingListController : Controller
    {
        // GET: RatingList
        public ActionResult Index()
        {
            var ratingListModel = new RatingListModel();

            var repository = new SqlServer();
            var ratings = repository.GetAllResults();
            var results = ratings.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            var calculator = new RatingCalculator();
            var calculatedRatings = calculator.Calculate(results);
            var sortedResults = calculatedRatings.Where(x => x.LastPlayed > DateTime.Today.AddDays(-90)).OrderByDescending(x => x.Number).ToList();

            int position = 1;
            foreach (var sortedResult in sortedResults)
            {
                var ratingModel = new RatingModel
                {
                    Number = position++,
                    Name = sortedResult.Player,
                    Rating = Math.Round(sortedResult.Number, 1),
                    LastPlayed = GetLastPlayed(sortedResult.LastPlayed),
                    Wins = sortedResult.NumberOfWonGames,
                    Draws = sortedResult.NumberOfDrawnGames,
                    Losses = sortedResult.NumberOfLostGames,
                    HighestRating = Math.Round(sortedResult.Highest, 1),
                    LowestRating = Math.Round(sortedResult.Lowest, 1),
                };
                ratingListModel.RatingModels.Add(ratingModel);
            }

            return View(ratingListModel);
        }

        private string GetLastPlayed(DateTime dateTime)
        {
            DateTime today = DateTime.Today;
            var differenceInDays = (today - dateTime).Days;

            if (differenceInDays == 0)
            {
                return "Today";
            }
            if (differenceInDays == 1)
            {
                return "Yesterday";
            }
            if (differenceInDays > 1 && differenceInDays < 7)
            {
                return differenceInDays + " days ago";
            }
            if (differenceInDays > 6 && differenceInDays < 14)
            {
                return "A week ago";
            }
            if (differenceInDays > 13 && differenceInDays < 21)
            {
                return "Two weeks ago";
            }
            if (differenceInDays > 20 && differenceInDays < 30)
            {
                return "Some weeks ago";
            }
            if (differenceInDays > 29 && differenceInDays < 60)
            {
                return "A month ago";
            }
            if (differenceInDays > 59 && differenceInDays < 90)
            {
                return "Two months ago";
            }
            if (differenceInDays > 89 && differenceInDays < 365)
            {
                return "Months ago";
            }
            if (differenceInDays > 364 && differenceInDays < 730)
            {
                return "A year ago";
            }
            if (differenceInDays > 729)
            {
                return "Forever ago";
            }
            return "Huh?";
        }
    }
}