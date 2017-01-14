using System.Collections.Generic;

namespace Dominion.Models
{
    public class RatingListModel
    {
        public RatingListModel()
        {
            RatingModels = new List<RatingModel>();
        }
        public List<RatingModel> RatingModels { get; set; }
    }
}