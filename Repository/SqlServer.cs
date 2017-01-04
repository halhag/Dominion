using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using Repository.Entities;
using System;

namespace Repository
{
    public class SqlServer
    {
        private readonly DataContext _dataContext;

        public SqlServer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _dataContext = new DataContext(connectionString);
        }

        public void SaveResult(Result result)
        {
            var results = _dataContext.GetTable<Result>();
            results.InsertOnSubmit(result);
            _dataContext.SubmitChanges();
        }

        public List<Result> GetAllResults()
        {
            var results = _dataContext.GetTable<Result>();
            return results.ToList();
        }

        public void DeleteResult(Guid guid)
        {
            var results = _dataContext.GetTable<Result>();
            var result = results.SingleOrDefault(x => x.ResultId == guid);
            if (result == null)
                return;
            results.DeleteOnSubmit(result);
            _dataContext.SubmitChanges();
        }
    }
}
