using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RockPaperScissors.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static RockPaperScissors.Web.Infrustructure.ApplicationConstants;

namespace RockPaperScissors.Web.Models
{
    public class PlayersHomePageViewModel : IValidatableObject
    {
        [ModelBinder(BinderType = typeof(CustomModelBinder))]
        [Display(Name = "Player One:")]
        public PlayerViewModel PlayerOne { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder))]
        [Display(Name ="Player Two:")]
        public PlayerViewModel PlayerTwo { get; set; }

        public IEnumerable<SelectListItem> Players { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationList = new List<ValidationResult>();

            if (PlayerOne == null || PlayerOne.PlayerType != PlayerType.USER || string.IsNullOrEmpty(PlayerOne.UserName))
            {
                validationList.Add(new ValidationResult("Player One Invalid, Try Again", new string[] { "PlayerOne.UserName" }));
            }

            if (PlayerTwo == null || PlayerTwo.PlayerType != PlayerType.COMPUTER || string.IsNullOrEmpty(PlayerTwo.UserName))
            {
                validationList.Add(new ValidationResult("Player Two Invalid, Try Again", new string[] { "PlayerTwo" }));
            }

            return validationList;
        }
    }
}
