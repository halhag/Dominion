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
        public void CreateNewResult()
        {
            var guid = Guid.NewGuid();
            var result = new Contracts.Result
            {
                Date = new DateTime(2011,10,11, 7,0,0),
                Id = guid,
                Name = "First Game",
                Scores = new List<Score>
                {
                    new Score {Player = "Geir", Points = 27},
                    new Score {Player = "Tore", Points = 25},
                    new Score {Player = "Halvard", Points = 24},
                    new Score {Player = "Fritjof", Points = 27},
                    new Score {Player = "Arne", Points = 21},
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
