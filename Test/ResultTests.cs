using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Test
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void GetAllResults()
        {
            var repository = new SqlServer();
            var results = repository.GetAllResults();
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        [Ignore]
        public void DeleteResult()
        {
            var repository = new SqlServer();
            repository.DeleteResult(Guid.Parse("9dd7e2c3-e58a-4470-b61e-f8d1d72f7f92"));
        }

        [TestMethod]
        [Ignore]
        public void UpdateName()
        {
            var repository = new SqlServer();
            var allDbResults = repository.GetAllResults();
            var results = allDbResults.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            //foreach (var result in results)
            //{
            //    //if (result.Name == "Guilding the Lily")
            //    //{
            //    //    result.Name = "Gilding the Lily";
            //    //    repository.UpdateResult(result.Id, result.ToJson());
            //    //}
            //    foreach (var score in result.Scores)
            //    {
            //        if (score.Player == "Hikolaj")
            //        {
            //            score.Player = "Nikolaj";
            //        }
            //    }
            //    repository.UpdateResult(result.Id, result.ToJson());
            //}
            foreach (var result in results)
            {
                if (result.Id == Guid.Parse("97B68413-B79D-4BD0-A08C-D372309A2567"))
                {
                    result.GameNumber = 1;
                    repository.UpdateResult(result.Id, result.ToJson());
                }
            }

            
        }

        [TestMethod]
        [Ignore]
        public void CreateNewResult()
        {
            var guid = Guid.NewGuid();
            var result = new Contracts.Result
            {
                Id = guid,
                Date = new DateTime(2017,1,10),
                GameNumber = 2,
                Name = "All Along the Watchtower 2nd ed",
                Scores = new List<Score>
                {
                    //new Score {Player = "Anders", Points = 9},
                    //new Score {Player = "Anders X", Points = 33},
                    //new Score {Player = "Anna", Points = 2},
                    //new Score {Player = "Arash", Points = 23},
                    //new Score {Player = "Arne", Points = 30},
                    //new Score {Player = "Arne J", Points = 20},
                    //new Score {Player = "Bjørn", Points = 34},
                    //new Score {Player = "Celina", Points = 9},
                    //new Score {Player = "Chris", Points = 10},
                    //new Score {Player = "Christopher", Points = 18},
                    //new Score {Player = "Dinesh", Points = 15},
                    new Score {Player = "Endre", Points = 26},
                    //new Score {Player = "Geir", Points = -3},
                    //new Score {Player = "Fritjof", Points = 30},
                    new Score {Player = "Halvard", Points = 22},
                    //new Score {Player = "Heather", Points = 22},
                    //new Score {Player = "Håvard", Points = 19},
                    //new Score {Player = "Jakob", Points = 21},
                    //new Score {Player = "Jovanka", Points = 19},
                    //new Score {Player = "Julia", Points = 44},
                    //new Score {Player = "Jørn X", Points = 39},
                    new Score {Player = "Kenneth", Points = 24},
                    //new Score {Player = "Kristian", Points = 22},
                    //new Score {Player = "Lars", Points = 16},
                    //new Score {Player = "Lars X", Points = 25},
                    //new Score {Player = "Miriam", Points = 23},
                    //new Score {Player = "Monica", Points = 41},
                    //new Score {Player = "Mick", Points = 25},
                    //new Score {Player = "Nikolaj", Points = 19},
                    //new Score {Player = "Nina", Points = 14},
                    //new Score {Player = "Ole Jonny", Points = 9},
                    new Score {Player = "Per Henrik", Points = 30},
                    //new Score {Player = "Petter", Points = 35},
                    //new Score {Player = "Rasmus", Points = 23},
                    new Score {Player = "Ruben", Points = 18},
                    //new Score {Player = "Stephanie", Points = 15},
                    //new Score {Player = "Sverre", Points = 17},
                    //new Score {Player = "Tore", Points = -2},
                    //new Score {Player = "Torill", Points = 17},
                    //new Score {Player = "Tron", Points = 9},
                    //new Score {Player = "Ørjan", Points = 25},
                    //new Score {Player = "Øyvind", Points = 29},
                    //new Score {Player = "Åse", Points = 13},
                }
            };

            var dbResult = new Repository.Entities.Result
            {
                Date = result.Date,
                ResultAsJson = result.ToJson(),
                ResultId = guid
            };
            var repository = new SqlServer();
            repository.SaveResult(dbResult);
            
        }
    }
}
