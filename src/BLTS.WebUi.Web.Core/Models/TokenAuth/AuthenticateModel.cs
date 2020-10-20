using System.ComponentModel.DataAnnotations;

namespace BLTS.WebUi.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [Required]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
        
        public bool RememberClient { get; set; }
    }
}
