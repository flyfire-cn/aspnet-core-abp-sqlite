using System.ComponentModel.DataAnnotations;

namespace flyfire.IMS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}