using System;
using System.Collections.Generic;
using Contracts;
using Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void CreateNewResult()
        {
            var guid = Guid.NewGuid();
            var result = new Contracts.Result
            {
                Id = guid,
                Date = new DateTime(2012,11,13),
                GameNumber = 2,
                Name = "Size Distortion",
                Scores = new List<Score>
                {
                    //new Score {Player = "Anders", Points = 12},
                    //new Score {Player = "Arne", Points = 24},
                    //new Score {Player = "Bjørn", Points = 27},
                    //new Score {Player = "Celina", Points = 27},
                    //new Score {Player = "Christopher", Points = 18},
                    //new Score {Player = "Geir", Points =22},
                    //new Score {Player = "Fritjof", Points = 26},
                    new Score {Player = "Halvard", Points = 48},
                    //new Score {Player = "Jovanka", Points = 19},
                    //new Score {Player = "Julia", Points = 44},
                    //new Score {Player = "Monica", Points = 41},
                    //new Score {Player = "Ole Jonny", Points = 33},
                    //new Score {Player = "Rasmus", Points = 46},
                    new Score {Player = "Ruben", Points = 26},
                    //new Score {Player = "Stephanie", Points = 9},
                    //new Score {Player = "Tore", Points = 34},
                    //new Score {Player = "Torill", Points = 21},
                    new Score {Player = "Øyvind", Points = 49},
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
