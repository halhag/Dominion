using System;
using System.Linq;
using System.Web.Mvc;
using Dominion.Models;
using Logic;
using Newtonsoft.Json;
using Repository;
using System.Collections.Generic;

namespace Dominion.Controllers
{
    public class RatingListController : Controller
    {
        // GET: RatingList
        public ActionResult Index(RatingListModel ratingListModel)
        {
            var repository = new SqlServer();
            var ratings = repository.GetAllResults();
            var results = ratings.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            results = results.Where(x => x.GameType == GameType.Dominion).ToList();
            var calculator = new RatingCalculator();

            var calculatedRatings = new List<Contracts.Rating>();
            var sortedResults = new List<Contracts.Rating>();
            if (string.IsNullOrEmpty(ratingListModel.YearsSelected) || ratingListModel.YearsSelected == "ALL" )
            {
                calculatedRatings = calculator.Calculate(results);
                sortedResults = calculatedRatings.Where(x => x.LastPlayed > DateTime.Today.AddDays(-90)).OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2011")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2011, 1, 1), new DateTime(2012, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2012")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2012, 1, 1), new DateTime(2013, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2013")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2013, 1, 1), new DateTime(2014, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2014")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2014, 1, 1), new DateTime(2015, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2015")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2015, 1, 1), new DateTime(2016, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2016")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2016, 1, 1), new DateTime(2017, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }
            else if (ratingListModel.YearsSelected == "2017")
            {
                calculatedRatings = calculator.Calculate(results, new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));
                sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();
            }

            int position = 1;
            foreach (var sortedResult in sortedResults)
            {
                var ratingModel = new RatingModel
                {
                    Number = position++,
                    Name = sortedResult.Player,
                    Rating = Math.Round(sortedResult.Number, 1),
                    LastPlayed = GetLastPlayed(sortedResult.LastPlayed),
                    LastPlayedValue = sortedResult.LastPlayed.ToString("yyyyMMdd"),
                    Wins = sortedResult.NumberOfWonGames,
                    Draws = sortedResult.NumberOfDrawnGames,
                    Losses = sortedResult.NumberOfLostGames,
                    HighestRating = Math.Round(sortedResult.Highest, 1),
                    LowestRating = Math.Round(sortedResult.Lowest, 1),
                    Trend = sortedResult.Trend.GetEndring()
                };
                ratingListModel.RatingModels.Add(ratingModel);
            }
            ratingListModel.Years.Add(new SelectListItem { Text = "All years", Value = "ALL" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2011", Value = "2011" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2012", Value = "2012" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2013", Value = "2013" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2014", Value = "2014" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2015", Value = "2015" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2016", Value = "2016" });
            ratingListModel.Years.Add(new SelectListItem { Text = "2017", Value = "2017" });

            return View(ratingListModel);
        }

        public ActionResult Recalculate(RatingListModel model)
        {
            return RedirectToAction("Index", model);
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