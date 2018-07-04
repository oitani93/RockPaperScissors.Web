using System.ComponentModel.DataAnnotations;

namespace RockPaperScissors.Web.Models
{
    public class UserViewModel
    {
        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(60)]
        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }
    }
}
