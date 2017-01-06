using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Logic;
using Newtonsoft.Json;
using Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class RatingCalculatorTests
    {
        //[Ignore("Gets data from database and is therefore an integration test")]
        [TestMethod]
        public void FullCalculation()
        {
            log4net.Config.XmlConfigurator.Configure();

            var repository = new SqlServer();
            var allDbResults = repository.GetAllResults();
            var results = allDbResults.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            var ratingCalculator = new RatingCalculator();
            var calculatedResults = ratingCalculator.Calculate(results);
            Assert.IsTrue(calculatedResults != null);
        }

        [TestMethod]
        public void NoGames()
        {
            var results = new List<Result>();
            var ratingCalculator = new RatingCalculator();
            var calculatedResults = ratingCalculator.Calculate(results);
            Assert.IsTrue(calculatedResults.Count == 0);
        }

        [TestMethod]
        public void OneGame()
        {
            var results = new List<Result>();
            results.Add(new Result
            {
                Date = DateTime.Today,
                Id = Guid.NewGuid(),
                Name = "First Game",
                Scores = new List<Score>
                {
                    new Score { Player = "Halvard", Points = 24},
                    new Score {Player = "Ruben", Points = 2}
                }
            });
            var ratingCalculator = new RatingCalculator();
            var calculatedResults = ratingCalculator.Calculate(results);
            Assert.IsTrue(calculatedResults.Count == 2);
            Assert.AreEqual(1810, calculatedResults[0].Number);
            Assert.AreEqual(1790, calculatedResults[1].Number);
        }

        [TestMethod]
        public void MultipleGames()
        {
            var results = new List<Result>();
            results.Add(new Result
            {
                Date = DateTime.Today.AddDays(-2),
                Id = Guid.NewGuid(),
                Name = "First Game",
                Scores = new List<Score>
                {
                    new Score {Player = "Halvard", Points = 0},
                    new Score {Player = "Bjørn", Points = 2},
                    new Score {Player = "Øyvind", Points = 3},
                    new Score {Player = "Arne", Points = -12},
                    new Score {Player = "Geir", Points = -1},
                    new Score {Player = "Tore", Points = -1},
                    new Score {Player = "Fritjof", Points = -3},
                }
            });
            results.Add(new Result
            {
                Date = DateTime.Today.AddDays(-1),
                Id = Guid.NewGuid(),
                Name = "First Game",
                Scores = new List<Score>
                {
                    new Score {Player = "Halvard", Points = 1},
                    new Score {Player = "Bjørn", Points = 5},
                    new Score {Player = "Øyvind", Points = 3},
                    new Score {Player = "Arne", Points = -3},
                    new Score {Player = "Geir", Points = 10},
                    new Score {Player = "Tore", Points = -4},
                    new Score {Player = "Fritjof", Points = -4},
                }
            });
            results.Add(new Result
            {
                Date = DateTime.Today.AddDays(0),
                Id = Guid.NewGuid(),
                Name = "First Game",
                GameNumber = 2,
                Scores = new List<Score>
                {
                    new Score {Player = "Halvard", Points = 40},
                    new Score {Player = "Bjørn", Points = 39},
                    new Score {Player = "Øyvind", Points = 29},
                    new Score {Player = "Arne", Points = 30},
                    new Score {Player = "Geir", Points = 32},
                    new Score {Player = "Tore", Points = 34},
                    new Score {Player = "Fritjof", Points = 42},
                }
            });
            results.Add(new Result
            {
                Date = DateTime.Today.AddDays(0),
                Id = Guid.NewGuid(),
                Name = "First Game",
                GameNumber = 1,
                Scores = new List<Score>
                {
                    new Score {Player = "Halvard", Points = 30},
                    new Score {Player = "Bjørn", Points = 39},
                    new Score {Player = "Øyvind", Points = 29},
                    new Score {Player = "Arne", Points = 30},
                    new Score {Player = "Geir", Points = 32},
                    new Score {Player = "Tore", Points = 34},
                    new Score {Player = "Fritjof", Points = 42},
                }
            });
            var ratingCalculator = new RatingCalculator();
            var calculatedResults = ratingCalculator.Calculate(results);
            Assert.IsTrue(calculatedResults.Count == 7);
        }
    }
}
