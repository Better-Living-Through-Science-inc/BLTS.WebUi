using System.ComponentModel.DataAnnotations;

namespace BLTS.WebUi.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}