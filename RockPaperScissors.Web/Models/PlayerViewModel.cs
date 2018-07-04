using static RockPaperScissors.Web.Infrustructure.ApplicationConstants;

namespace RockPaperScissors.Web.Models
{
    public class PlayerViewModel : UserViewModel
    {
        public string PlayerType { get; set; }

        public Actions Action { get; set; }

        public PlayerScoreSheetViewModel ScoreSheet { get; set; }
    }
}
