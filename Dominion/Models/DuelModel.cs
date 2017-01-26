using System.Collections.Generic;
using System.Web.Mvc;

namespace Dominion.Models
{
    public class DuelModel
    {
        public DuelModel()
        {
            PlayersFirst = new List<SelectListItem>();
            PlayersSecond = new List<SelectListItem>();
        }

        public List<SelectListItem> PlayersFirst { get; set; }
        public List<SelectListItem> PlayersSecond { get; set; }
        public string SelectedFirst { get; set; }
        public string SelectedSecond { get; set; }
        public string ErrorMessage { get; set; }
        public string MessageFirstShooter { get; set; }
        public string MessageSecondShooter { get; set; }
        public string MessageDraws { get; set; }
        public bool IsDuel { get; set; }
    }
}