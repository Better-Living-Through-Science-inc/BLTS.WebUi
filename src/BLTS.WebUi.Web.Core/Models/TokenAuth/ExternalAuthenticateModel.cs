using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BLTS.WebUi.Models.TokenAuth
{
    public class ExternalAuthenticateModel
    {
        [Required]
        public string AuthProvider { get; set; }

        [Required]
        public string ProviderKey { get; set; }

        [Required]
        public string ProviderAccessCode { get; set; }
    }
}
