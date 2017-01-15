using System.Collections.Generic;
using System.Web.Mvc;

namespace Dominion.Models
{
    public class RatingListModel
    {
        public RatingListModel()
        {
            RatingModels = new List<RatingModel>();
            Years = new List<SelectListItem>();
        }
        public List<RatingModel> RatingModels { get; set; }
        public List<SelectListItem> Years { get; set; }
        public string YearsSelected { get; set; }
    }
}