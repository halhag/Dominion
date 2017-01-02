using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Logic;
using Newtonsoft.Json;
using NUnit.Framework;
using Repository;

namespace Test
{
    [TestFixture]
    public class RatingCalculatorTests
    {
        //[Ignore("Gets data from database and is therefore an integration test")]
        [Test]
        public void FullCalculation()
        {
            var repository = new SqlServer();
            var allDbResults = repository.GetAllResults();
            var results = allDbResults.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            var ratingCalculator = new RatingCalculator();
            var calculatedResults = ratingCalculator.Calculate(results);
            Assert.IsTrue(calculatedResults != null);
        }

        [Test]
        public void NoGames()
        {
            var results = new List<Result>();
            var ratingCalculator = new RatingCalculator();
            var calculatedResults = ratingCalculator.Calculate(results);
            Assert.IsTrue(calculatedResults.Count == 0);
        }

        [Test]
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

        [Test]
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
