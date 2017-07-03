using Dominion.Models;
using Logic;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dominion.Controllers
{
    public class DuelController : Controller
    {
        // GET: Duel
        public ActionResult Index(DuelModel duelModel)
        {
            var repository = new SqlServer();
            var ratings = repository.GetAllResults();
            var results = ratings.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            results = results.Where(x => x.GameType == GameType.Dominion).ToList();
            var calculator = new RatingCalculator();
            var calculatedRatings = calculator.Calculate(results);
            var sortedResults = calculatedRatings.OrderBy(x => x.Player).ToList();
            foreach (var result in sortedResults)
            {
                duelModel.PlayersFirst.Add(new SelectListItem { Text = result.Player, Value = result.Player });
                duelModel.PlayersSecond.Add(new SelectListItem { Text = result.Player, Value = result.Player });
            }

            if (duelModel.IsDuel)
            {
                var duel = calculator.Calculate(duelModel.SelectedFirst, duelModel.SelectedSecond, results);
                duelModel.MessageFirstShooter = duelModel.SelectedFirst + " has " + duel.Wins + " wins.";
                duelModel.MessageSecondShooter = duelModel.SelectedSecond + " has " + duel.Losses + " wins.";
                duelModel.MessageDraws = "They have " + duel.Draws + " draws.";
                duelModel.IsDuel = false;
                duelModel.SelectedFirst = null;
                duelModel.SelectedSecond = null;
            }

            return View(duelModel);
        }


        public ActionResult Shoot(DuelModel model)
        {
            if (model.SelectedFirst == model.SelectedSecond)
            {
                model.ErrorMessage = "You tried to trick me!  You can't duel against yourself.";
            }
            else
            {
                model.IsDuel = true;
            }
            return RedirectToAction("Index", model);
        }
    }
}