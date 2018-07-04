using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Web.Models
{
    public class UserInputViewModel : UserViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationList = new List<ValidationResult>();

            if (string.IsNullOrEmpty(UserName))
            {
                validationList.Add(new ValidationResult("Username cannot be empty", new string[] { "PlayerOne.UserName" }));
            }

            if (string.IsNullOrEmpty(EmailAddress))
            {
                validationList.Add(new ValidationResult("Email Address cannot be empty", new string[] { "PlayerOne.EmailAddress" }));
            }

            return validationList;
        }
    }
}
