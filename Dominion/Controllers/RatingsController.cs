using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Repository;

namespace Dominion.Controllers
{
    public class RatingsController : ApiController
    {
        // GET api/values
        public IEnumerable<Contracts.Result> Get()
        {
            var repository = new SqlServer();
            var allDbResults = repository.GetAllResults();
            return allDbResults.Select(dbResult => JsonConvert.DeserializeObject<Contracts.Result>(dbResult.ResultAsJson)).ToList();
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
