using System;
using System.Data.Linq.Mapping;

namespace Repository.Entities
{
    [Table(Name = "Result")]
    public class Result
    {
        [Column(IsPrimaryKey = true)]
        public Guid ResultId;

        [Column(CanBeNull = false)]
        public string ResultAsJson;

        [Column(CanBeNull = false)]
        public DateTime Date;
    }
}
