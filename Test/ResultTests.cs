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
                Date = new DateTime(2011,10,23, 8,0,0),
                Id = guid,
                Name = "First Game",
                Scores = new List<Score>
                {
                    //new Score {Player = "Arne", Points = 16},
                    new Score {Player = "Bjørn", Points = 31},
                    new Score {Player = "Geir", Points = 21},
                    new Score {Player = "Fritjof", Points = 21},
                    new Score {Player = "Halvard", Points = 18},
                    //new Score {Player = "Tore", Points = 15},
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
