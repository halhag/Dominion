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
            var sortedResults = calculatedRatings.OrderByDescending(x => x.Number).ToList();

            foreach (var sortedResult in sortedResults)
            {
                var ratingModel = new RatingModel
                {
                    Name = sortedResult.Player,
                    Rating = Math.Round(sortedResult.Number, 1),
                    LastPlayed = sortedResult.LastPlayed.ToShortDateString(),
                    Wins = sortedResult.NumberOfWonGames,
                    Draws = sortedResult.NumberOfDrawnGames,
                    Losses = sortedResult.NumberOfLostGames
                };
                ratingListModel.RatingModels.Add(ratingModel);
            }

            return View(ratingListModel);
        }
    }
}