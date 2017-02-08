using System;
using System.Collections.Generic;

namespace Contracts
{
    public class Result
    {
        public string GameType { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Score> Scores { get; set; }
        public DateTime Date { get; set; }
        public int GameNumber { get; set; }
    }
}
