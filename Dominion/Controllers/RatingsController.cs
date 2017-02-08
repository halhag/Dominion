using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Logic;
using Newtonsoft.Json;
using Repository;

namespace Dominion.Controllers
{
    [RoutePrefix("api/ratings")]
    public class RatingsController : ApiController
    {
        [HttpGet]
        [AcceptVerbs("GET")]
        public string Get()
        {
            return "Angi gameType for å få svar!";
        }

        // GET api/values
        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("{gameType}")]
        public IEnumerable<Contracts.Rating> Get(string gameType)
        {
            var repository = new SqlServer();
            var allDbResults = repository.GetAllResults();
            var results = allDbResults.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
            results = results.Where(x => x.GameType == gameType).ToList();
            var calculator = new RatingCalculator();
            var ratings = calculator.Calculate(results);
            return ratings;
        }
        

        // POST api/values
        public void Post([FromBody]Contracts.Result result)
        {
            var dbResult = new Repository.Entities.Result
            {
                Date = DateTime.Now,
                ResultAsJson = result.ToJson(),
                ResultId = result.Id
            };
            var repository = new SqlServer();
            repository.SaveResult(dbResult);
        }
    }
}
