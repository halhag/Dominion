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
                Date = new DateTime(2011,12,30),
                GameNumber = 2,
                Name = "Interaction",
                Scores = new List<Score>
                {
                    //new Score {Player = "Arne", Points = 25},
                    //new Score {Player = "Bjørn", Points = 14},
                    new Score {Player = "Celina", Points = 21},
                    //new Score {Player = "Geir", Points = 5},
                    //new Score {Player = "Fritjof", Points = 42},
                    new Score {Player = "Halvard", Points = 27},
                    new Score {Player = "Ole Jonny", Points = 33},
                    new Score {Player = "Rasmus", Points = 22},
                    //new Score {Player = "Tore", Points = 29},
                    new Score {Player = "Torill", Points = 27},
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
