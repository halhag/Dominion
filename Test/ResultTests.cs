using System;
using System.Collections.Generic;
using Contracts;
using NUnit.Framework;
using Repository;

namespace Test
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void CreateNewResult()
        {
            var guid = Guid.NewGuid();
            var result = new Contracts.Result
            {
                Date = new DateTime(2011,10,11, 6,0,0),
                Id = guid,
                Name = "First Game",
                Scores = new List<Score>
                {
                    new Score {Player = "Geir", Points = 12},
                    new Score {Player = "Tore", Points = 19},
                    new Score {Player = "Halvard", Points = 12},
                    new Score {Player = "Fritjof", Points = 18},
                    new Score {Player = "Arne", Points = 27},
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
