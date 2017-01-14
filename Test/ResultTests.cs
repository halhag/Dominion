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
            foreach (var result in results)
            {
                if (result.Name == "Size Distortions")
                {
                    result.Name = "Size Distortion";
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
                Date = new DateTime(2015,11,29),
                GameNumber = 1,
                Name = "WC 2014 (1)",
                Scores = new List<Score>
                {
                    //new Score {Player = "Anders", Points = 9},
                    //new Score {Player = "Anders X", Points = 33},
                    //new Score {Player = "Anna", Points = 2},
                    new Score {Player = "Arne", Points = 21},
                    //new Score {Player = "Arne J", Points = 20},
                    //new Score {Player = "Bjørn", Points = 28},
                    //new Score {Player = "Celina", Points = 25},
                    //new Score {Player = "Christopher", Points = 18},
                    //new Score {Player = "Geir", Points = 28},
                    //new Score {Player = "Fritjof", Points = 28},
                    new Score {Player = "Halvard", Points = 24},
                    //new Score {Player = "Heather", Points = 1},
                    //new Score {Player = "Håvard", Points = 13},
                    //new Score {Player = "Jakob", Points = 1},
                    //new Score {Player = "Jovanka", Points = 19},
                    //new Score {Player = "Julia", Points = 44},
                    //new Score {Player = "Jørn X", Points = 39},
                    //new Score {Player = "Lars", Points = 16},
                    //new Score {Player = "Lars X", Points = 32},
                    //new Score {Player = "Miriam", Points = 7},
                    //new Score {Player = "Monica", Points = 41},
                    //new Score {Player = "Mick", Points = 25},
                    //new Score {Player = "Hikolaj", Points = 7},
                    //new Score {Player = "Nina", Points = 14},
                    //new Score {Player = "Ole Jonny", Points = 9},
                    new Score {Player = "Rasmus", Points = 18},
                    //new Score {Player = "Ruben", Points = 39},
                    //new Score {Player = "Stephanie", Points = 15},
                    //new Score {Player = "Sverre", Points = 17},
                    //new Score {Player = "Tore", Points = 22},
                    new Score {Player = "Torill", Points = 24},
                    //new Score {Player = "Øyvind", Points = 25},
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
